using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Commands;
using EnvDTE;
using EnvDTE80;
using Task = System.Threading.Tasks.Task;

namespace CnSharp.VisualStudio.NuPack
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
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

            // await AssemblyInfoEditCommand.InitializeAsync(this);
            await DeployPackageCommand.InitializeAsync(this);
            await MigrateNuspecToProjectCommand.InitializeAsync(this);
        }
    }
}
