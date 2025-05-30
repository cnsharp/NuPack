using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Extensions;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CnSharp.VisualStudio.NuPack.Models;
using Package = Microsoft.VisualStudio.Shell.Package;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler for adding .nuspec file under project folder with the file name as project name
    /// </summary>
    internal sealed class AddNuSpecCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = PackageIds.cmdidAddNuSpecCommand;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = PackageGuids.guidAddNuSpecPackageCmdSet;

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        private static readonly string NuspecTemplate =
            Resources.ReadEmbeddedResource("CnSharp.VisualStudio.NuPack.Resources.NuSpecTemplate.xml");

        /// <summary>
        /// Initializes a new instance of the <see cref="AddNuSpecCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddNuSpecCommand(Package package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.MenuItemCallback, menuCommandID);
            menuItem.BeforeQueryStatus += MenuItemOnBeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public AddNuSpecCommand()
        {

        }

        private void MenuItemOnBeforeQueryStatus(object sender, EventArgs eventArgs)
        {
            var project = Host.Instance.Dte2.GetActiveProject();
            if (project == null) return;
            var cmd = (OleMenuCommand)sender;
            cmd.Visible = !File.Exists(project.GetNuspecFilePath());
            cmd.Text = project.Name + ".nuspec";
        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddNuSpecCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in DeployPackageCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AddNuSpecCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            Execute();
        }

        public void Execute(object arg = null)
        {
            var dte = Host.Instance.Dte2;
            var project = dte.GetActiveProject();
            var file = project.GetNuspecFilePath();
            if (File.Exists(file))
                return;
            var sdkBased = project.IsSdkBased();
            var assemblyInfo = project.GetProjectAssemblyInfo();
            var ppp = sdkBased ? project.GetPackageProjectProperties() : null;
            var targetFramework = GetTargetFrameworkVersion(project);
            var nuspecTargetFramework = targetFramework != null ? TargetFrameworkConverter.ConvertToNuGetTargetFramework(targetFramework) : null;
            var directoryBuildProps = dte.Solution.GetDirectoryBuildProps();
            using (var sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                var temp = NuspecTemplate;
                var dependencies = GatherProjectReferences(project);
                if (!string.IsNullOrWhiteSpace(dependencies))
                {
                    temp = temp.Replace("<!--  <dependency id=\"SampleDependency\" version=\"1.0.0\" /> -->",
                        dependencies);
                }
                temp = temp.Replace("$id$", ppp?.PackageId ?? assemblyInfo?.Title ?? project.Name);
                temp = temp.Replace("$version$",
                    ppp?.Version ??
                    assemblyInfo?.Version.ToSemanticVersion() ??
                      GetValueFromDirectoryBuildProps(directoryBuildProps, "$version$") ??
                      "1.0.0");
                temp = temp.Replace("$authors$",
                    ppp?.Company ?? 
                    ppp?.Authors ??
                    assemblyInfo?.Company ??
                     GetValueFromDirectoryBuildProps(directoryBuildProps, "$authors$") ??
                     Common.GetOrganization());
                temp = temp.Replace("$description$",
                    ppp?.Description ??
                    assemblyInfo?.Description ??
                    GetValueFromDirectoryBuildProps(directoryBuildProps, "$description$") ??
                    string.Empty);
                temp = temp.Replace("$copyright$",
                    ppp?.Copyright ??
                    assemblyInfo?.Copyright ??
                    GetValueFromDirectoryBuildProps(directoryBuildProps, "$copyright$") ??
                    $"copyright©{DateTime.Now.Year}{Common.GetOrganization() ?? Environment.UserName}");

                if (nuspecTargetFramework != null)
                    temp = temp.Replace("$targetFramework$", nuspecTargetFramework);

                sw.Write(temp);
                sw.Flush();
                sw.Close();
            }

            project.ProjectItems.AddFromFile(file);
            dte.ItemOperations.OpenFile(file);
        }

        private string GetValueFromDirectoryBuildProps(DirectoryBuildProps props, string placeholder)
        {
            var propertyName = NuspecMsbuildMapper.NuspecToMsBuildMap[placeholder.Trim('$')];
            var property = props.GetType().GetProperty(propertyName);
            if (property == null)
                return null;

            var value = property.GetValue(props);
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return null;
            return value.ToString();
        }

        private string GetTargetFrameworkVersion(Project project)
        {
            var sb = new StringBuilder();
            foreach (Property projectProperty in project.Properties)
            {
                try
                {
                    sb.AppendLine(projectProperty.Name + ": " + projectProperty.Value);
                }
                catch
                {

                }
            }
            var property = project.Properties.Item("TargetFrameworkMoniker");
            return property?.Value.ToString();
        }

        private static string GatherProjectReferences(Project project)
        {
            var sb = new StringBuilder();
            var references = project.GetReferenceProjects();
            foreach (var reference in references)
            {
                var nuspecFile = reference.GetNuspecFilePath();
                if (File.Exists(nuspecFile))
                {
                    var metadata = NuGetExtensions.LoadFromNuspecFile(nuspecFile);
                    sb.AppendLine($"<dependency id=\"{metadata.Id}\" version=\"{metadata.Version}\" />");
                }
            }

            return sb.ToString();
        }
    }
}