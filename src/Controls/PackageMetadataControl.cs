using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using CnSharp.VisualStudio.NuPack.Models;
using EnvDTE;

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

        public PackageMetadataControl()
        {
            InitializeComponent();

            ActiveControl = textBoxVersion;

            MakeTextBoxRequired(textBoxId);
            MakeTextBoxRequired(textBoxDescription);
            MakeTextBoxRequired(textBoxAuthors);
            MakeTextBoxRequired(textBoxOwners);
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
                }
            }
        }

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
                textBoxIconUrl.Text = openIconFileDialog.FileName;
            }
        }

        private void buttonOpenReadme_Click(object sender, EventArgs e)
        {
            if (openReadmeDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxReadme.Text = openReadmeDialog.FileName;
            }
        }
    }
}