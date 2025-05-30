using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Extensions;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler for adding DirectoryBuild.Props file under solution folder
    /// </summary>
    internal sealed class AddDirectoryBuildPropsCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = PackageIds.cmdidAddDirectoryBuildPropsCommand;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = PackageGuids.guidAddDirectoryBuildPropsPackageCmdSet;

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        private static readonly string DirectoryBuildPropsTemplate =
            Resources.ReadEmbeddedResource("CnSharp.VisualStudio.NuPack.Resources.DirectoryBuildPropsTemplate.xml");

        public AddDirectoryBuildPropsCommand()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddDirectoryBuildPropsCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private AddDirectoryBuildPropsCommand(Package package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            //menuItem.BeforeQueryStatus += MenuItemOnBeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private void MenuItemOnBeforeQueryStatus(object sender, EventArgs e)
        {
            var sln = Host.Instance.DTE.Solution;
            if (sln == null) return;
            var cmd = (OleMenuCommand)sender;
            //cmd.Visible = sln.Projects.Cast<Project>().Any(p => !string.IsNullOrWhiteSpace(p.FileName) && p.IsSdkBased()) && !File.Exists(sln.GetDirectoryBuildPropsPath());
            cmd.Visible = SolutionDataCache.Instance.GetSolutionProperties(sln.FileName).HasSdkBasedProjects && !File.Exists(sln.GetDirectoryBuildPropsPath());
        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddDirectoryBuildPropsCommand Instance
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
            // Switch to the main thread - the call to AddCommand in MigrateNuspecToProjectCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AddDirectoryBuildPropsCommand(package, commandService);
        }

        public void Execute(object sender, EventArgs e)
        {
            var dte = Host.Instance.DTE;
            var sln = dte.Solution;
            if (sln == null) return;
            var file = sln.GetDirectoryBuildPropsPath();
            var sln2 = sln as Solution2;
            if (File.Exists(file))
            {
                try
                {
                    sln2.AddSolutionItem(file);
                }
                finally
                {
                    dte.ItemOperations.OpenFile(file);
                }

                var dr = MessageBoxHelper.ShowQuestionMessageBox($"File '{file}' already exists,do you want to overwrite it?");
                if (dr != DialogResult.Yes)
                    return;
            }

            using (var sw = new StreamWriter(file, false, Encoding.UTF8))
            {
                var temp = DirectoryBuildPropsTemplate;
                temp = temp.Replace("$author$", Environment.UserName);
                temp = temp.Replace("$company$", Common.GetOrganization());
                temp = temp.Replace("$product$", Path.GetFileNameWithoutExtension(sln.FileName));
                sw.Write(temp);
                sw.Flush();
                sw.Close();
            }

            sln2.AddSolutionItem(file);
            dte.ItemOperations.OpenFile(file);
        }
    }
}
