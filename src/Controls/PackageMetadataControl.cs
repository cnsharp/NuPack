using CnSharp.VisualStudio.Extensions;
using CnSharp.VisualStudio.NuPack.Models;
using EnvDTE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CnSharp.VisualStudio.Extensions.Projects;
using CnSharp.VisualStudio.Extensions.Util;
using CnSharp.VisualStudio.NuPack.Util;

namespace CnSharp.VisualStudio.NuPack.Controls
{
    public partial class PackageMetadataControl : UserControl
    {
        private static readonly List<string> CommonLicenses = new List<string>
        {
            "0BSD",
            "Apache-2.0",
            "BSD-3-Clause",
            "GPL-3.0-or-later",
            "LGPL-3.0-or-later",
            "MIT",
            "MPL-2.0"
        };
        private ManifestMetadataViewModel _manifestMetadata;
        private Project _project;
        private string _projectDir;
        private bool _isSdkBased;
        private string _oldIconFilePath;
        private string _oldReadmeFilePath;

        public PackageMetadataControl()
        {
            InitializeComponent();

            ActiveControl = textBoxVersion;

            MakeTextBoxRequired(textBoxId);
            MakeTextBoxRequired(textBoxDescription);
            MakeTextBoxRequired(textBoxAuthors);
            MakeTextBoxRequired(textBoxTitle);
            MakeTextBoxRequired(textBoxVersion);
            MakeTextBoxRequired(textBoxReleaseNotes);


            checkBoxRLA.CheckedChanged += (sender, args) =>
            {
                ManifestMetadata.RequireLicenseAcceptance = checkBoxRLA.Checked;
            };

            openIconFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            BindingLicenses();
            labelLicense.AttachHelpButton("https://spdx.org/licenses/");
            labelIcon.AttachHelpButton("https://go.microsoft.com/fwlink/?linkid=2147134");
        }

        public Project Project
        {
            set
            {
                _project = value;
                if (_project != null)
                {
                    openIconFileDialog.InitialDirectory = openReadmeDialog.InitialDirectory = Path.GetDirectoryName(_project.FullName);
                    _projectDir = Path.GetDirectoryName(_project.FullName);
                    _isSdkBased = _project.IsSdkBased();
                }
            }
        }

        public string SolutionDirectory { get; set; }

        private void BindingLicenses()
        {
           CommonLicenses.ForEach(x => licenseBox.Items.Add(x));
           licenseBox.TextChanged += (sender, e) =>
           {
               checkBoxRLA.Enabled = !string.IsNullOrWhiteSpace(licenseBox.Text);
               if (!checkBoxRLA.Enabled && checkBoxRLA.Checked)
                   checkBoxRLA.Checked = false;
           };
        }


        public ManifestMetadataViewModel ManifestMetadata
        {
            get => _manifestMetadata;
            set
            {
                _manifestMetadata = value;
                foreach (Control control in Controls)
                {
                    var box = control as TextBox;
                    if (!string.IsNullOrWhiteSpace(box?.Tag?.ToString()))
                    {
                        box.DataBindings.Clear();
                        box.DataBindings.Add("Text", _manifestMetadata, box.Tag.ToString(), true, DataSourceUpdateMode.OnPropertyChanged);
                    }
                }

                licenseBox.DataBindings.Add("Text", _manifestMetadata,
                    nameof(_manifestMetadata.PackageLicenseExpression), true, DataSourceUpdateMode.OnPropertyChanged);
                checkBoxRLA.Checked = _manifestMetadata.RequireLicenseAcceptance;
            }
        }

        public ErrorProvider ErrorProvider { get; set; }
        
        private void MakeTextBoxRequired(TextBox box)
        {
            box.Validating += TextBoxValidating;
            box.Validated += TextBoxValidated;
        }

        private void TextBoxValidated(object sender, EventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            ErrorProvider.SetError(box, null);
        }

        private void TextBoxValidating(object sender, CancelEventArgs e)
        {
            var box = sender as TextBox;
            if (box == null)
                return;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                ErrorProvider.SetError(box, "*");
                e.Cancel = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (textBoxDescription.Focused)
                {
                    InsertNewLine(textBoxDescription);
                    return true;
                }
                if (textBoxReleaseNotes.Focused)
                {
                    InsertNewLine(textBoxReleaseNotes);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void InsertNewLine(TextBox box)
        {
            var i = box.SelectionStart;
            box.Text = box.Text.Insert(i, Environment.NewLine);
            box.Select(i + 2, 0);
        }

        private void btnOpenIconDir_Click(object sender, EventArgs e)
        {
            if (openIconFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetFileName(openIconFileDialog.FileName) == _manifestMetadata.Icon) return;
                if(openIconFileDialog.FileName != _oldIconFilePath)
                {
                    ManifestMetadata.Icon = AddOrUpdatePackFile(openIconFileDialog.FileName, _manifestMetadata.Icon);
                    _oldIconFilePath = openIconFileDialog.FileName;
                    textBoxIconUrl.Text = openIconFileDialog.FileName;
                    UpdatePackageIconInProject();
                }
            }
        }

        private void UpdatePackageIconInProject()
        {
            if (!_isSdkBased) return;
            try
            {
                _project.Properties.Item(nameof(PackageProjectProperties.PackageIcon)).Value = ManifestMetadata.Icon;
            }
            catch(Exception ex)
            {
                MessageBoxHelper.ShowErrorMessageBox($"Failed to set PackageIcon: {ex.Message}.Please update it by Project Property Page or edit it in project file.");
            }
        }

        private void buttonOpenReadme_Click(object sender, EventArgs e)
        {
            if (openReadmeDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetFileName(openReadmeDialog.FileName) == _manifestMetadata.Readme) return;
                if (openIconFileDialog.FileName != _oldReadmeFilePath)
                {
                    ManifestMetadata.Readme = AddOrUpdatePackFile(openReadmeDialog.FileName, _manifestMetadata.Readme);
                    _oldReadmeFilePath = openReadmeDialog.FileName;
                    textBoxReadme.Text = openReadmeDialog.FileName;
                    UpdatePackageReadmeInProject();
                }
            }
        }

        private void UpdatePackageReadmeInProject()
        {
            if (!_isSdkBased) return;
            try
            {
                _project.Properties.Item(nameof(PackageProjectProperties.PackageReadmeFile)).Value = ManifestMetadata.Readme;
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowErrorMessageBox($"Failed to set PackageReadmeFile: {ex.Message}.Please update it by Project Property Page or edit it in project file.");
            }
        }

        private void ShowFileError(TextBox box)
        {
            var msg = $"File `{box.Text.Trim()}` not found";
            ErrorProvider.SetError(box, msg);
            MessageBoxHelper.ShowErrorMessageBox(msg);
        }

        private void textBoxReadme_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxReadme.Text.Trim() != openReadmeDialog.FileName)
            {
                if (!ValidateFilePath(textBoxReadme))
                {
                    ShowFileError(textBoxReadme);
                    e.Cancel = true;
                }
            }
        }

        private void textBoxReadme_Validated(object sender, EventArgs e)
        {
            ErrorProvider.SetError(textBoxReadme, null);
            ManifestMetadata.Readme = AddOrUpdatePackFile(textBoxReadme.Text.Trim(), _manifestMetadata.Readme);
            UpdatePackageReadmeInProject();
        }

        private void textBoxIconUrl_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxIconUrl.Text.Trim() != openIconFileDialog.FileName)
            {
                if (!ValidateFilePath(textBoxIconUrl))
                {
                    ShowFileError(textBoxIconUrl);
                    e.Cancel = true;
                }
            }
        }

        private void textBoxIconUrl_Validated(object sender, EventArgs e)
        {
            ErrorProvider.SetError(textBoxIconUrl, null);
            ManifestMetadata.Icon = AddOrUpdatePackFile(textBoxIconUrl.Text.Trim(), _manifestMetadata.Icon);
            UpdatePackageIconInProject();
        }

        private bool ValidateFilePath(TextBox box)
        {
            var filePath = box.Text.Trim();
            var absolutePath = PathUtil.ToAbsolutePath(filePath, _projectDir);
            if (filePath != absolutePath && _project.ProjectItems.Item(filePath) != null)
            {
                return true;
            }
            if (!File.Exists(absolutePath))
            {
                return false;
            }
           
            return true;
        }

        private string AddOrUpdatePackFile(string filePath, string oldFile)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }
            var absolutePath = PathUtil.ToAbsolutePath(filePath, _projectDir);
            if (filePath != absolutePath && _project.ProjectItems.Item(filePath) != null)
            {
                return filePath;
            }
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
            var gitDir = _project.GetGitDirectory();

            var fileName = Path.GetFileName(filePath);
            var destinationPath = Path.Combine(_projectDir, fileName);
            var isUnderControl = false;
            if (!destinationPath.Equals(filePath, StringComparison.OrdinalIgnoreCase))
            {
                var isUnderGit = gitDir != null && filePath.StartsWith(gitDir, StringComparison.OrdinalIgnoreCase);
                var isUnderSolution = filePath.StartsWith(SolutionDirectory, StringComparison.OrdinalIgnoreCase);
                isUnderControl = isUnderGit || isUnderSolution;
                if (!isUnderControl)
                    File.Copy(filePath, destinationPath, true);
            }
            var item = isUnderControl && !destinationPath.Equals(filePath, StringComparison.OrdinalIgnoreCase) ?
                _project.AddFromFileAsLink(filePath, fileName) :
                _project.AddFromFile(destinationPath);
            if (_isSdkBased)
            {
                _project.SetItemAttribute(item, "Pack", true.ToString());
                _project.SetItemAttribute(item, "PackagePath", "\\");
            }
            else
            {
                item.Properties.Item("CopyToOutputDirectory").Value = 2;//copy if newer
            }
        }

    }
}