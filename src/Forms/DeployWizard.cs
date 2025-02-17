using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AeroWizard;
using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.NuPack.Config;
using CnSharp.VisualStudio.NuPack.Controls;
using CnSharp.VisualStudio.NuPack.Extensions;
using CnSharp.VisualStudio.NuPack.Models;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using NuGet.Packaging;
using NuGet.Versioning;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.Forms
{
    public partial class DeployWizard : Form
    {
        private readonly DTE2 _dte;
        private readonly ManifestMetadata _metadata;
        private readonly ManifestMetadataViewModel _metadataVM;
        private readonly AsyncPackage _package;
        private readonly PackageMetadataControl _metadataControl;
        private readonly PackOptionsControl _optionsControl;
        private readonly PackageProjectProperties _ppp;
        private readonly Project _project;
        private readonly string _projectDir;
        private readonly string _slnDir;
        private string _outputDir;

        private DeployWizard()
        {
            InitializeComponent();

            _metadataControl = new PackageMetadataControl();
            _metadataControl.Dock = DockStyle.Fill;
            _metadataControl.ErrorProvider = errorProvider;
            wizardPageMetadata.Controls.Add(_metadataControl);
            ActiveControl = _metadataControl;

            _optionsControl = new PackOptionsControl();
            _optionsControl.Dock = DockStyle.Fill;
            _optionsControl.ErrorProvider = errorProvider;
            wizardPageOptions.Controls.Add(_optionsControl);

            stepWizardControl.SelectedPageChanged += StepWizardControl_SelectedPageChanged;
            stepWizardControl.Finished += StepWizardControl_Finished;
            wizardPageMetadata.Commit += WizardPageCommit;
            wizardPageOptions.Commit += WizardPageCommit;

            BindSources();
        }

        public DeployWizard(DTE2 dte, AsyncPackage package) : this()
        {
            _dte = dte;
            _package = package;
            var project = dte.GetActiveProject();
            _ppp = project.GetPackageProjectProperties();
            InitProjectPackageInfo(_ppp, project);
            _metadata = _ppp.ToManifestMetadata();
            _metadataVM = new ManifestMetadataViewModel(_metadata);
            _slnDir = Path.GetDirectoryName(dte.Solution.FileName);
            _project = _dte.GetActiveProject();
            _metadataControl.Project = _project;
            _projectDir = _project.GetDirectory();
            var releaseDir = Path.Combine(_projectDir, "bin", "Release");
            _optionsControl.LoadConfig(_projectDir, releaseDir);
        }

        private void InitProjectPackageInfo(PackageProjectProperties ppp, Project project)
        {
            if (ppp.PackageId == null)
                ppp.PackageId = project.Name;
            if (ppp.Version == null)
                ppp.Version = "1.0.0";
        }

        private void BindSources()
        {
            var sources = new NuGetConfigReader(_slnDir, _projectDir).GetNuGetSources();
            _optionsControl.Sources = sources;
        }

        private void WizardPageCommit(object sender, WizardPageConfirmEventArgs e)
        {
            var wp = sender as WizardPage;
            if (Validation.HasValidationErrors(wp.Controls)) e.Cancel = true;
        }

        private void StepWizardControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (stepWizardControl.SelectedPage == wizardPageMetadata)
                _metadataControl.Focus();
            else if (stepWizardControl.SelectedPage == wizardPageOptions) _optionsControl.Focus();
        }

        private void StepWizardControl_Finished(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void DeployWizard_Load(object sender, EventArgs e)
        {
            BindingMetaData();
        }

        private void BindingMetaData()
        {
            var ver = _ppp.AssemblyVersion;
            if (_metadataVM.Version == null)
                _metadataVM.Version = new NuGetVersion(ver);
            if (_metadataVM.Title.IsEmptyOrPlaceHolder())
                _metadataVM.Title = _metadataVM.Id;
            _metadataControl.ManifestMetadata = _metadataVM;
        }

        public void SaveAndBuild()
        {
            var needPush = NeedPush();
            if (needPush && _optionsControl.PackArgs.NoBuild)
            {
                var yes = _package.ShowQuestion("You are going to deploy the package with --nobuild,do you confirm?",
                        "Warning");
                if(!yes) return;
            }
            ActiveOutputWindow();
            OutputMessage("start packaging..." + Environment.NewLine);
            OutputMessage(Environment.NewLine + "Save package info to project.");
            SavePackageInfo();
            EnsureOutputDir();
            OutputMessage(Environment.NewLine + "Do packaging...");
            if (!Pack()) return;
            // ShowPackages();
            OutputMessage(Environment.NewLine + "Save project config.");
            SaveProjectConfig();

            if (!needPush)
            {
                ShowPackages();
                OutputMessage(Environment.NewLine + "All Done!");
                return;
            }
            OutputMessage(Environment.NewLine+ "Pushing nupkg..." );
            var pushResult = Push();
            if (!pushResult) return;
            OutputMessage(Environment.NewLine + "All Done!");
        }

        private void SavePackageInfo()
        {
            LinkIcon();
            LinkReadme();
            _project.Save();
            _metadataVM.SyncToPackageProjectProperties(_ppp);
            _project.SavePackageProjectProperties(_ppp);
        }

        private void LinkIcon()
        {
            if (!_metadataVM.IconChanged) return;
            _metadataVM.Icon = AddOrUpdatePackFile(_metadataVM.IconUrlString, _metadata.Icon);
        }

        private void LinkReadme()
        {
            if (!_metadataVM.ReadmeChanged) return;
            _metadataVM.Readme = AddOrUpdatePackFile(_metadataVM.Readme, _metadata.Readme);
        }

        private string AddOrUpdatePackFile(string filePath, string oldFile)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }
            var absolutePath = ToAbsolutePath(filePath);
            if (!string.IsNullOrEmpty(absolutePath) && !File.Exists(absolutePath))
                throw new FileNotFoundException($"File `{filePath}` not found");
            if (!string.IsNullOrWhiteSpace(oldFile))
            {
                var oldFilePath = Path.Combine(_projectDir, oldFile);
                if (oldFilePath == absolutePath)
                {
                    //not changed
                    return oldFile;
                }
                //remove old item
                var oldItem = _project.ProjectItems.Item(oldFile);
                if (oldItem != null)
                {
                    oldItem.Remove();
                    if (File.Exists(oldFilePath))
                        File.Delete(oldFilePath);
                }
            }
           
            var fileName = Path.GetFileName(absolutePath);
            var existsItem = _project.ProjectItems.Item(fileName);
            if (existsItem != null)
            {
                existsItem.Remove();
            }

            var item = _project.ProjectItems.AddFromFile(absolutePath);
            _project.SetItemAttribute(item, "Pack", true.ToString());
            _project.SetItemAttribute(item, "PackagePath", "\\");
            return fileName;
        }

        private string ToAbsolutePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path.Contains(":\\"))
            {
                return path;
            }
            Directory.SetCurrentDirectory(Path.GetDirectoryName(_project.FileName));
            return Path.GetFullPath(path);
        }

        private string GetRelativePath(string path, string baseDir)
        {
            var uri = new Uri(path);
            var exeUri = new Uri(baseDir);
            var relativePath = exeUri.MakeRelativeUri(uri);
            return relativePath.ToString();
        }

        private bool Pack()
        {
            var args = _optionsControl.PackArgs;
            var script =
                new StringBuilder(
                    $"pack \"{_project.FullName}\" -c Release -p:PackageVersion={_metadataVM.VersionString}");
            if (!string.IsNullOrWhiteSpace(args.OutputDirectory))
                script.Append($" -o \"{args.OutputDirectory}\"");
            if (args.IncludeSymbols)
                script.Append(" --include-symbols ");
            if (args.IncludeSource)
                script.Append(" --include-source ");
            if(args.NoBuild)
                script.Append(" --no-build ");
            if (args.NoDependencies)
                script.Append(" --no-dependencies ");
            // if (File.Exists(_nuspecFile))
            //     script.AppendFormat(" -p:NuspecFile=\"{0}\" ", _nuspecFile);
            return CmdUtil.RunDotnet(script.ToString(), OutputMessage, OutputMessage);
        }

        private void ActiveOutputWindow()
        {
            _dte.ToolWindows.OutputWindow.Parent.Activate();
        }

        private void OutputMessage(string message)
        { 
            _dte.OutputMessage(Common.ProductName, message);
        }

        private bool NeedPush()
        {
            var args = _optionsControl.PushArgs;
            return !string.IsNullOrWhiteSpace(args.Source) || !string.IsNullOrWhiteSpace(args.SymbolSource);
        }

        private bool Push()
        {
            var args = _optionsControl.PushArgs;
            var script = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(args.Source))
            {
                script.Append(
                    $"nuget push \"{_outputDir}{_metadataVM.Id}.{_metadataVM.VersionString}.nupkg\" -s \"{args.Source}\" ");
                if (!string.IsNullOrWhiteSpace(args.ApiKey))
                    script.Append($" -k \"{args.ApiKey}\"");
            }

            if (!string.IsNullOrWhiteSpace(args.SymbolSource))
            {
                script.Append($" -ss \"{args.SymbolSource}\" ");
                if (!string.IsNullOrWhiteSpace(args.SymbolApiKey))
                    script.Append($" -sk \"{args.SymbolApiKey}\"");
            }

            if (script.Length == 0)
                return true;

            return CmdUtil.RunDotnet(script.ToString(), OutputMessage, OutputMessage);
        }

        private void ShowPackages()
        {
            var pkg = $"{_outputDir}{_metadataVM.Id}.{_metadataVM.VersionString}.nupkg";
            if (File.Exists(pkg)) 
                Process.Start("explorer.exe", $"/select,\"{pkg}\"");
        }

        private void EnsureOutputDir()
        {
            _outputDir = _optionsControl.PackArgs.OutputDirectory.Trim().Replace("/", "\\");
            if (string.IsNullOrWhiteSpace(_outputDir)) return;
            if (!Directory.Exists(_outputDir))
                Directory.CreateDirectory(_outputDir);
            if (!_outputDir.EndsWith("\\"))
                _outputDir += "\\";
        }

        private void SaveProjectConfig()
        {
            var config = _optionsControl.Config;
            if (config.SymbolServers?.Any() == true)
            {
                if (!string.IsNullOrWhiteSpace(_optionsControl.PushArgs.SymbolSource))
                {
                    config.SymbolServers.Add(_optionsControl.PushArgs.SymbolSource);
                }
                config.SymbolServers =
                    config.SymbolServers.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_optionsControl.PushArgs.SymbolSource))
                {
                    config.SymbolServers = new List<string>
                    {
                        _optionsControl.PushArgs.SymbolSource
                    };
                }
            }

            new NuPackConfigHelper(_projectDir).Save(config);
        }
    }
}