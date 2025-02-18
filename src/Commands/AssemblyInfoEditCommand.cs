﻿using System;
using System.ComponentModel.Design;
using System.Linq;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Extensions;
using CnSharp.VisualStudio.NuPack.Forms;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CnSharp.VisualStudio.NuPack.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AssemblyInfoEditCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("2dff605d-c360-4f36-9f5e-1b117fcd71f4");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoEditCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private AssemblyInfoEditCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AssemblyInfoEditCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in AssemblyInfoEditCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AssemblyInfoEditCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var dte = Host.Instance.Dte2;
            try
            {
                var sln = dte.Solution;
                var sp = SolutionDataCache.Instance.GetSolutionProperties(sln.FileName);
                var allProjects = sp.Projects;
                if (!allProjects.Any())
                {
                    package.ShowError("No project is opening.", Common.ProductName);
                    return;
                }

                var projectsWithAssemblyInfo = allProjects.Where(p => p.HasAssemblyInfo()).ToList();
                if (!projectsWithAssemblyInfo.Any())
                {
                    package.ShowError("No project with AssemblyInfo found.", Common.ProductName);
                    return;
                }
                new AssemblyInfoForm(projectsWithAssemblyInfo).ShowDialog();
            }
            catch (Exception exception)
            {
                package.ShowError(exception.Message, Common.ProductName);
            }
        }
    }
}