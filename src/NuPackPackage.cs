using Microsoft.VisualStudio.Shell;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Commands;
using CnSharp.VisualStudio.NuPack.Extensions;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace CnSharp.VisualStudio.NuPack
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideUIContextRule(
        contextGuid: PackageGuids.guidMigrateNuspecToProjectUIRuleString,
        name: "package.nuspec file",
        expression: "package.nuspec",
        termNames: new []{"package.nuspec"},
        termValues: new []{"HierSingleSelectionName:package.nuspec$"})]
    public sealed class NuPackPackage : AsyncPackage
    {
        /// <summary>
        /// NuPack 2022 Package GUID string.
        /// </summary>
        public const string PackageGuidString = PackageGuids.guidNuPackPackageString;

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var dte = GetGlobalService(typeof(DTE)) as DTE2;
            Host.Instance.DTE = dte;


            bool isSolutionLoaded = await IsSolutionLoadedAsync();

            if (isSolutionLoaded)
            {
                HandleOpenSolution();
            }

            // Listen for subsequent solution events
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += HandleOpenSolution;

            dte.Events.SolutionEvents.ProjectAdded += p =>
            {
                if (string.IsNullOrWhiteSpace(p.FileName) ||
                    !Common.SupportedProjectTypes.Any(t => p.FileName.EndsWith(t, StringComparison.OrdinalIgnoreCase)))
                    return;
                var sln = Host.Instance.Solution2;
                SolutionDataCache.Instance.TryGetValue(sln.FileName, out var sp);
                sp?.AddProject(p);
            };
            dte.Events.SolutionEvents.ProjectRemoved += p =>
            {
                var sln = Host.Instance.Solution2;
                SolutionDataCache.Instance.TryGetValue(sln.FileName, out var sp);
                sp?.RemoveProject(p);
            };

            await AddDirectoryBuildPropsCommand.InitializeAsync(this);
            await AddNuSpecCommand.InitializeAsync(this);
            await AssemblyInfoEditCommand.InitializeAsync(this);
            await DeployPackageCommand.InitializeAsync(this);
            await MigrateNuspecToProjectCommand.InitializeAsync(this);
            await BatchPushCommand.InitializeAsync(this);
        }


        private async Task<bool> IsSolutionLoadedAsync()
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();
            var solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            ErrorHandler.ThrowOnFailure(solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));

            return value is bool isSolOpen && isSolOpen;
        }

        private void HandleOpenSolution(object sender = null, EventArgs e = null)
        {
            var sln = Host.Instance.Solution2;
            var projects = Host.Instance.DTE.GetSolutionProjects()
                .Where(
                    p =>
                        !string.IsNullOrWhiteSpace(p.FileName) &&
                        Common.SupportedProjectTypes.Any(
                            t => p.FileName.EndsWith(t, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            var sp = new SolutionProperties
            {
                Projects = projects
            };
            SolutionDataCache.Instance.AddOrUpdate(sln.FileName, sp, (k, v) =>
            {
                v = sp;
                return v;
            });
        }

    }
}
