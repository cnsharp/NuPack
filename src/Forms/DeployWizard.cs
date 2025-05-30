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
using NuGet.Packaging;
using NuGet.Versioning;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.Forms
{
    public partial class DeployWizard : Form
    {
        private readonly bool _isSdkBased;
        private readonly DirectoryBuildProps _directoryBuildProps;
        private readonly DTE2 _dte;
        private readonly ManifestMetadata _metadata;
        private readonly ManifestMetadataViewModel _metadataVM;
        private readonly PackageMetadataControl _metadataControl;
        private readonly PackageProjectProperties _ppp;
        private readonly PackOptionsControl _optionsControl;
        private readonly Project _project;
        private readonly ProjectAssemblyInfo _assemblyInfo;
        private readonly string _projectDir;
        private readonly string _slnDir;
        private string _outputDir;
        private string _nuspecFile;
        private string _nupkgPath;

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

        public DeployWizard(DTE2 dte) : this()
        {
            _dte = dte;
            var project = dte.GetActiveProject();
            _directoryBuildProps = dte.Solution.GetDirectoryBuildProps();
            _isSdkBased = project.IsSdkBased();
            if (_isSdkBased)
                _ppp = project.GetPackageProjectProperties();
            _nuspecFile = project.GetNuspecFilePath();
            if (File.Exists(_nuspecFile))
            {
                _metadata = NuGetExtensions.LoadFromNuspecFile(_nuspecFile);
                _assemblyInfo = project.HasAssemblyInfo() ? project.GetProjectAssemblyInfo() : null;
                if (_assemblyInfo != null)
                    _metadata = _metadata.CopyFromAssemblyInfo(_assemblyInfo);
            }
            else
            {
                if (_ppp != null)
                {
                    InitProjectPackageInfo(_ppp, project);
                    _metadata = _ppp.ToManifestMetadata();
                }
                if (_directoryBuildProps != null)
                    _metadata = _metadata.CopyFromManifestMetadata(_directoryBuildProps);
            }

            _metadataVM = new ManifestMetadataViewModel(_metadata);
            _slnDir = Path.GetDirectoryName(dte.Solution.FileName);
            _project = _dte.GetActiveProject();
            _metadataControl.Project = _project;
            _projectDir = _project.GetDirectory();
            var outputDir = Path.Combine(_projectDir, "bin", Common.ProductName);
            _optionsControl.SdkBased = _isSdkBased;
            _optionsControl.LoadConfig(_projectDir, outputDir);
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
            {
                _metadataControl.Focus();
            }
            else if (stepWizardControl.SelectedPage == wizardPageOptions)
            {
                _optionsControl.Focus();
            }
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
           
            if (_metadataVM.Version == null)
            {
                //todo:product version in AssemblyInfo
                var ver = _ppp?.AssemblyVersion ?? _assemblyInfo?.Version.ToSemanticVersion() ?? "1.0.0";
                _metadataVM.Version = new NuGetVersion(ver);
            }

            if (_metadataVM.Title.IsEmptyOrPlaceHolder())
                _metadataVM.Title = _metadataVM.Id;
            _metadataControl.ManifestMetadata = _metadataVM;
        }

        public void SaveAndBuild()
        {
            var needPush = NeedPush();
            if (needPush && _optionsControl.PackArgs.NoBuild)
            {
                var dr = MessageBoxHelper.ShowQuestionMessageBox("You are going to deploy the package with --nobuild,do you confirm?");
                if(dr != DialogResult.Yes) return;
            }
            ActiveOutputWindow();
            OutputMessage("start..." + Environment.NewLine);
            OutputMessage(Environment.NewLine + "Save package info to project.");
            SavePackageInfo();
            EnsureOutputDir();
            if (!_optionsControl.PackArgs.NoBuild)
            {
                OutputMessage(Environment.NewLine + "Building...");
                if (!Host.Instance.Solution2.BuildRelease())
                    return;
            }
            OutputMessage(Environment.NewLine + "Do packaging...");
            if (!Pack()) return;
            // ShowPackages();
            OutputMessage(Environment.NewLine + "Save project config.");
            SaveProjectConfig();


            _nupkgPath = Path.Combine(_outputDir, $"{_metadataVM.Id}.{_metadataVM.VersionString}.nupkg");
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
            _project.Save();
            _directoryBuildProps?.Save();

            var newIcon = LinkIcon();
            var newReadme = LinkReadme();

            if (File.Exists(_nuspecFile))
            {
                var removingFiles = new List<string>();
                var files = new List<string>();
                if (newIcon != null)
                {
                    files.Add(newIcon);
                    removingFiles.Add(_metadata.Icon);
                }

                if (newReadme != null)
                {
                    files.Add(newReadme);
                    removingFiles.Add(_metadata.Readme);
                }

                SaveNuspec(removingFiles, files);
            }
            else if(_ppp != null)
            {
                _metadataVM.SyncToPackageProjectProperties(_ppp);
                var skipProps = _directoryBuildProps?.GetValuedProperties()?.ToArray();
                _project.SavePackageProjectProperties(_ppp, skipProps);
            }

            if (_assemblyInfo != null)
                SaveAssemblyInfo();
        }

        private void SaveAssemblyInfo()
        {
            _assemblyInfo.Company = _metadataVM.Owners != null ? string.Join(",", _metadataVM.Owners) : string.Empty;
            _assemblyInfo.Save(true);
        }

        private void SaveNuspec(List<string> removingFiles, List<string> files)
        {
            _project.UpdateNuspec(_metadataVM);
            if (files?.Any() == true)
                NuspecWriter.ChangeFiles(_project.GetNuspecFilePath(), removingFiles, files);
        }

        private string LinkIcon()
        {
            if (!_metadataVM.IconChanged) return null;
            _metadataVM.Icon = AddOrUpdatePackFile(_metadataVM.IconUrlString, _metadata.Icon);
            return _metadataVM.Icon;
        }

        private string LinkReadme()
        {
            if (!_metadataVM.ReadmeChanged) return null;
            _metadataVM.Readme = AddOrUpdatePackFile(_metadataVM.Readme, _metadata.Readme);
            return _metadataVM.Readme;
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
            try
            {
                var existsItem = _project.ProjectItems.Item(fileName);
                if (existsItem != null)
                {
                    existsItem.Remove();
                }
            }
            catch
            {
                //ignored, exception may occur in .NET framework project
            }

            AddToProject(absolutePath);

            return fileName;
        }

        private void AddToProject(string filePath)
        {
            if (_isSdkBased)
            {
                var item = _project.ProjectItems.AddFromFile(filePath);
                _project.SetItemAttribute(item, "Pack", true.ToString());
                _project.SetItemAttribute(item, "PackagePath", "\\");
            }
            else
            {
                //nuget pack will copy the file to output directory,it needs file to be in project directory
                var physicalPath = Path.Combine(_projectDir, Path.GetFileName(filePath));
                if (physicalPath != filePath)
                {
                    File.Copy(filePath, physicalPath, true);
                }
                var item = _project.ProjectItems.AddFromFile(physicalPath);
                item.Properties.Item("CopyToOutputDirectory").Value = 2;//copy if newer
            }
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
            if (_isSdkBased)
                return RunDotnetPack();
            return RunNuGetPack();
        }

        private bool RunDotnetPack()
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
            if (File.Exists(_nuspecFile))
                script.AppendFormat(" -p:NuspecFile=\"{0}\" ", _nuspecFile);
            OutputMessage("dotnet "+ script);
            return CmdUtil.RunDotnet(script.ToString(), OutputMessage, OutputMessage);
        }

        private bool RunNuGetPack()
        {
            var args = _optionsControl.PackArgs;
            var script = new StringBuilder();
            script.AppendFormat(
                //NU5048: iconUrl warning
                //ignore it
                @"nuget pack ""{0}"" -Build -Version ""{1}"" -ForceEnglishOutput -Properties  ""Configuration=Release;NoWarn=NU5048""  -OutputDirectory ""{2}"" -MSBuildPath ""{3}"" ", 
                _project.FileName, _metadataVM.VersionString, _outputDir.TrimEnd('\\'), GetMsBuildPath());//nuget pack path shouldn't end with slash

            if (args.IncludeReferencedProjects)
                script.Append(" -IncludeReferencedProjects ");
            if (args.IncludeSymbols)
                script.Append(" -Symbols ");
            OutputMessage(script.ToString());
            // nuget output is too verbose, we need to suppress it
            return CmdUtil.RunCmd(script.ToString(), null, OutputMessage);
        }

        private void ActiveOutputWindow()
        {
            _dte.ToolWindows.OutputWindow.Parent.Activate();
        }

        private void OutputMessage(string message)
        { 
            _dte.OutputMessage(Common.ProductName, Environment.NewLine + message);
        }

        private string GetMsBuildPath()
        {
            //C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE
            var dir = new DirectoryInfo(Application.StartupPath);
            //C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin
            return Path.Combine(dir.Parent.Parent.FullName, "MSBuild", "Current", "Bin"); 
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
                    $"nuget push \"{_nupkgPath}\" -s \"{args.Source}\" ");
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
            if (File.Exists(_nupkgPath)) 
                Process.Start("explorer.exe", $"/select,\"{_nupkgPath}\"");
        }

        private void EnsureOutputDir()
        {
            _outputDir = _optionsControl.PackArgs.OutputDirectory.Trim().Replace("/", "\\");
            if (string.IsNullOrWhiteSpace(_outputDir)) return;
            if (!Directory.Exists(_outputDir))
                Directory.CreateDirectory(_outputDir);
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