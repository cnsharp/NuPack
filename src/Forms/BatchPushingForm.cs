using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Config;
using CnSharp.VisualStudio.NuPack.Util;
using EnvDTE;
using EnvDTE80;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using NuGet.Packaging;
using Process = System.Diagnostics.Process;

namespace CnSharp.VisualStudio.NuPack.Forms
{
    public partial class BatchPushingForm : Form
    {
        private readonly DTE2 _dte;
        public BatchPushingForm(DTE2 dte)
        {
            _dte = dte;
            InitializeComponent();
            Icon = Resource.logo;
            nuGetServerOptionsControl.ShowApiKeyPlaceHolder = true;
            nuGetServerOptionsControl.OnSourceChanged += (sender, args) => EnablePushButton();
            Binding(dte.Solution);
        }

        public void Binding(Solution solution)
        {
            var solutionDir = Path.GetDirectoryName(solution.FileName);
            nuGetServerOptionsControl.BindingSources(solutionDir, null);
            var projects = solution.Projects.Cast<Project>().Where(p => !string.IsNullOrWhiteSpace(p.FullName)).ToList();
            var symbolServers = new HashSet<string>();

            listViewPackages.BeginUpdate();
            listViewPackages.Groups.Clear();
            listViewPackages.Items.Clear();
            var containsSymbolsPackage = false;
            projects.ForEach(p =>
            {
                var packagesAdded = new HashSet<string>();
                listViewPackages.Groups.Add(p.FullName, p.Name);
                var projectDir = Path.GetDirectoryName(p.FileName);
                var config = new NuPackConfigHelper(projectDir).Read();
                if (config?.SymbolServers?.Any() == true)
                {
                    symbolServers.AddRange(config.SymbolServers);
                }
                var nupkgs = Directory
                    .GetFiles(projectDir, "*" + Common.NuGetPackageFileExt, SearchOption.AllDirectories)
                    .OrderBy(x => x).ToList();
                var projectPackages = new List<PackageViewModel>();
                string currentPackage = null;
                NuGetVersion maxVersion = null;
                foreach (var nupkg in nupkgs)
                {
                    // skip legacy symbols file
                    if (nupkg.EndsWith(Common.LegacyNuGetPackageSymbolsFileExt))
                        continue;
                    var fi = new FileInfo(nupkg);
                    if (packagesAdded.Contains(fi.Name))
                        continue;
                    packagesAdded.Add(fi.Name);
                    var fileNameSegments = fi.Name.Split('.');
                    var version =
                        NuGetVersion.Parse(Path.GetFileNameWithoutExtension(nupkg).Split(new[] { '.' }, 2)[1]);
                    var package = new PackageViewModel
                    {
                        Name = fi.Name,
                        PackageName = fileNameSegments[0],
                        Path = nupkg,
                        ProjectPath = p.FileName,
                        UploadStatus = UploadStatus.NotStarted,
                        Version = version.ToString(),
                        CreationTime = fi.CreationTime
                    };
                    projectPackages.Add(package);

                    //check .snupkg file
                    var snupkg = Path.Combine(Path.GetDirectoryName(nupkg), Path.GetFileNameWithoutExtension(nupkg) + Common.NuGetPackageSymbolsFileExt);
                    var snupkgFi = new FileInfo(snupkg);
                    if (File.Exists(snupkg))
                    {
                        containsSymbolsPackage = true;
                        var symbolPackage = new PackageViewModel
                        {
                            Name = snupkgFi.Name,
                            PackageName = fileNameSegments[0],
                            Path = snupkg,
                            ProjectPath = p.FileName,
                            UploadStatus = UploadStatus.NotStarted,
                            Version = version.ToString(),
                            CreationTime = snupkgFi.CreationTime
                        };
                        projectPackages.Add(symbolPackage);
                    }

                    //compare versions
                    if (currentPackage != package.PackageName)
                    {
                        if (currentPackage != null)
                        {
                            CheckLatest(projectPackages, currentPackage, maxVersion);
                        }

                        currentPackage = package.PackageName;
                        maxVersion = new NuGetVersion(package.Version);
                    }
                    else
                    {
                        var currentVersion = new NuGetVersion(package.Version);
                        if (currentVersion > maxVersion)
                        {
                            maxVersion = currentVersion;
                        }
                    }
                }

                if (currentPackage != null)
                {
                    CheckLatest(projectPackages, currentPackage, maxVersion);
                }

                projectPackages.ForEach(pp =>
                {
                    var item = new ListViewItem(pp.Name)
                    {
                        Tag = pp,
                        Group = listViewPackages.Groups[p.FullName],
                        Checked = pp.Checked,
                        UseItemStyleForSubItems = false
                    };
                    item.SubItems.Add(pp.CreationTime.ToString(CultureInfo.CurrentCulture));
                    item.SubItems.Add(pp.UploadStatus.ToString());
                    item.SubItems.Add(pp.Path.Substring(solutionDir.Length));
                    listViewPackages.Items.Add(item);
                });
                packagesAdded.Clear();
            });
            listViewPackages.EndUpdate();

            nuGetServerOptionsControl.IncludeSymbols = containsSymbolsPackage;
            nuGetServerOptionsControl.BindingSymbolServers(symbolServers.ToList());
        }

        private static void CheckLatest(IEnumerable<PackageViewModel> projectPackages, string currentPackage, NuGetVersion maxVersion)
        {
             projectPackages
                 .Where(x => 
                    x.PackageName == currentPackage && x.Version == maxVersion.OriginalVersion)
                .ToList()
                .ForEach(pc => pc.Checked = true);
        }

        private void toolStripButtonPush_Click(object sender, EventArgs e)
        {
            var pushArgs = nuGetServerOptionsControl.PushArgs;
            if (string.IsNullOrWhiteSpace(pushArgs.Source))
                return;
            var i = 0;
            PackageViewModel previous = null;
            foreach (ListViewItem item in listViewPackages.CheckedItems)
            {
                item.SubItems[2].ForeColor = SystemColors.WindowText;
                var package = item.Tag as PackageViewModel;
                if (item.Text.EndsWith(Common.NuGetPackageSymbolsFileExt))
                {
                    if (previous != null && previous.PackageName == package.PackageName)
                    {
                        package.UploadStatus = previous.UploadStatus;
                        SetSubItemTextAndForeColor(item.SubItems[2], package.UploadStatus);
                    }
                    continue;
                }
               
                package.UploadStatus = UploadStatus.InProgress;
                item.SubItems[2].Text = package.UploadStatus.ToString();
                if (Push(package.Path))
                {
                    package.UploadStatus = UploadStatus.Completed;
                }
                else
                {
                    package.UploadStatus = UploadStatus.Failed;
                }
                SetSubItemTextAndForeColor(item.SubItems[2], package.UploadStatus);

                previous = package;

                i++;
            }
        }

        private void SetSubItemTextAndForeColor(ListViewItem.ListViewSubItem subItem, UploadStatus uploadStatus)
        {
            subItem.Text = uploadStatus.ToString();
            subItem.ForeColor = uploadStatus == UploadStatus.Completed ? Color.Green: Color.Red;
        }

        private bool Push(string packagePath)
        {
            var args = nuGetServerOptionsControl.PushArgs;
            var script = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(args.Source))
            {
                script.Append(
                    $"nuget push \"{packagePath}\" -s \"{args.Source}\" ");
                if (!string.IsNullOrWhiteSpace(args.ApiKey))
                    script.Append($" -k \"{args.ApiKey}\"");
            }

            if (IncludeSymbols(packagePath))
            {
                if (!string.IsNullOrWhiteSpace(args.SymbolSource))
                {
                    script.Append($" -ss \"{args.SymbolSource}\" ");
                    if (!string.IsNullOrWhiteSpace(args.SymbolApiKey))
                        script.Append($" -sk \"{args.SymbolApiKey}\"");
                }
            }
            else
            {
                script.Append(" -n"); //--no-symbols
            }

            if (script.Length == 0)
                return true;

            return CmdUtil.RunDotnet(script.ToString(), OutputMessage, OutputMessage);
        }

        private bool IncludeSymbols(string packagePath)
        {
            var snupkg = Path.Combine(Path.GetDirectoryName(packagePath), Path.GetFileNameWithoutExtension(packagePath) + Common.NuGetPackageSymbolsFileExt);
            if (!File.Exists(snupkg)) return false;
            var fileName = Path.GetFileName(snupkg);
            var item = listViewPackages.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Text == fileName);
            return item.Checked;
        }

        private void OutputMessage(string message)
        {
            _dte.OutputMessage(Common.ProductName, Environment.NewLine + message);
        }

        ListViewItem _lastHitItem;
        private void listViewPackages_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitInfo = listViewPackages.HitTest(e.X, e.Y);
                _lastHitItem = hitInfo.Item;
                if (_lastHitItem != null && hitInfo.SubItem != null && hitInfo.Item.SubItems.IndexOf(hitInfo.SubItem) == 0)
                {
                    checkToolStripMenuItem.Text = _lastHitItem.Checked ? "Uncheck" : "Check";
                    contextMenuStripPackage.Show(listViewPackages, e.Location);
                }
                else
                {
                    contextMenuStripPackage.Hide();
                }
            }
        }

        private void viewInWindowsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_lastHitItem != null && _lastHitItem.Tag is PackageViewModel package)
            {
                Process.Start("explorer.exe", $"/select,\"{package.Path}\"");
            }
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_lastHitItem != null)
                _lastHitItem.Checked = !_lastHitItem.Checked;
        }

        private void listViewPackages_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            EnablePushButton();
        }

        private void EnablePushButton()
        {
            toolStripButtonPush.Enabled = listViewPackages.CheckedItems.Count > 0 &&
                                          !string.IsNullOrWhiteSpace(nuGetServerOptionsControl.PushArgs.Source);
        }
    }


    public class PackageViewModel : INotifyPropertyChanged
    {
        private UploadStatus _uploadStatus;

        /// <summary>
        ///     Project file path.(For grouping)
        /// </summary>
        public string ProjectPath { get; set; }

        /// <summary>
        ///     The path of the package.
        /// </summary>
        public string Path { get; set; }

        public string Name { get; set; }

        public string PackageName { get; set; }

        public string Version { get; set; }

        public DateTime CreationTime { get; set; }

        public bool Checked { get; set; }

        public UploadStatus UploadStatus
        {
            get => _uploadStatus;
            set
            {
                if (_uploadStatus != value)
                {
                    _uploadStatus = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum UploadStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }
}