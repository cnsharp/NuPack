namespace CnSharp.VisualStudio.NuPack.Controls
{
    partial class PackageMetadataControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxAuthors = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxOwners = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxReleaseNotes = new System.Windows.Forms.TextBox();
            this.checkBoxRLA = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxTags = new System.Windows.Forms.TextBox();
            this.labelIcon = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxIconUrl = new System.Windows.Forms.TextBox();
            this.textBoxProjectUrl = new System.Windows.Forms.TextBox();
            this.labelLicense = new System.Windows.Forms.Label();
            this.textBoxCopyright = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.licenseBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRepositoryUrl = new System.Windows.Forms.TextBox();
            this.textBoxRepositoryType = new System.Windows.Forms.TextBox();
            this.btnOpenIconDir = new System.Windows.Forms.Button();
            this.openIconFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenReadme = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxReadme = new System.Windows.Forms.TextBox();
            this.openReadmeDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // textBoxId
            // 
            this.textBoxId.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxId.Location = new System.Drawing.Point(96, 3);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(246, 21);
            this.textBoxId.TabIndex = 0;
            this.textBoxId.Tag = "Id";
            // 
            // textBoxAuthors
            // 
            this.textBoxAuthors.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxAuthors.Location = new System.Drawing.Point(96, 30);
            this.textBoxAuthors.Name = "textBoxAuthors";
            this.textBoxAuthors.Size = new System.Drawing.Size(246, 21);
            this.textBoxAuthors.TabIndex = 1;
            this.textBoxAuthors.Tag = "AuthorsString";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "ID*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-2, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 50;
            this.label7.Text = "Authors*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(376, 33);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 53;
            this.label13.Text = "Owners*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(-2, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 51;
            this.label11.Text = "Description*";
            // 
            // textBoxOwners
            // 
            this.textBoxOwners.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxOwners.Location = new System.Drawing.Point(495, 30);
            this.textBoxOwners.Name = "textBoxOwners";
            this.textBoxOwners.Size = new System.Drawing.Size(246, 21);
            this.textBoxOwners.TabIndex = 2;
            this.textBoxOwners.Tag = "OwnersString";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxDescription.Location = new System.Drawing.Point(96, 60);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(247, 75);
            this.textBoxDescription.TabIndex = 3;
            this.textBoxDescription.Tag = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(376, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 48;
            this.label3.Text = "Release Notes*";
            // 
            // textBoxReleaseNotes
            // 
            this.textBoxReleaseNotes.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxReleaseNotes.Location = new System.Drawing.Point(495, 60);
            this.textBoxReleaseNotes.Multiline = true;
            this.textBoxReleaseNotes.Name = "textBoxReleaseNotes";
            this.textBoxReleaseNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReleaseNotes.Size = new System.Drawing.Size(244, 75);
            this.textBoxReleaseNotes.TabIndex = 4;
            this.textBoxReleaseNotes.Tag = "ReleaseNotes";
            // 
            // checkBoxRLA
            // 
            this.checkBoxRLA.AutoSize = true;
            this.checkBoxRLA.Enabled = false;
            this.checkBoxRLA.Location = new System.Drawing.Point(378, 217);
            this.checkBoxRLA.Name = "checkBoxRLA";
            this.checkBoxRLA.Size = new System.Drawing.Size(186, 16);
            this.checkBoxRLA.TabIndex = 9;
            this.checkBoxRLA.Tag = "RequireLicenseAcceptance ";
            this.checkBoxRLA.Text = "Require License Acceptance ";
            this.checkBoxRLA.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(-2, 336);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 63;
            this.label18.Text = "Tags:";
            // 
            // textBoxTags
            // 
            this.textBoxTags.Location = new System.Drawing.Point(96, 333);
            this.textBoxTags.Name = "textBoxTags";
            this.textBoxTags.Size = new System.Drawing.Size(631, 21);
            this.textBoxTags.TabIndex = 15;
            this.textBoxTags.Tag = "Tags";
            // 
            // labelIcon
            // 
            this.labelIcon.AutoSize = true;
            this.labelIcon.Location = new System.Drawing.Point(-1, 303);
            this.labelIcon.Name = "labelIcon";
            this.labelIcon.Size = new System.Drawing.Size(35, 12);
            this.labelIcon.TabIndex = 62;
            this.labelIcon.Text = "Icon:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-1, 272);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 61;
            this.label16.Text = "Project URL:";
            // 
            // textBoxIconUrl
            // 
            this.textBoxIconUrl.Location = new System.Drawing.Point(96, 300);
            this.textBoxIconUrl.Name = "textBoxIconUrl";
            this.textBoxIconUrl.Size = new System.Drawing.Size(631, 21);
            this.textBoxIconUrl.TabIndex = 13;
            this.textBoxIconUrl.Tag = "IconUrlString";
            // 
            // textBoxProjectUrl
            // 
            this.textBoxProjectUrl.Location = new System.Drawing.Point(96, 269);
            this.textBoxProjectUrl.Name = "textBoxProjectUrl";
            this.textBoxProjectUrl.Size = new System.Drawing.Size(631, 21);
            this.textBoxProjectUrl.TabIndex = 12;
            this.textBoxProjectUrl.Tag = "ProjectUrlString";
            // 
            // labelLicense
            // 
            this.labelLicense.AutoSize = true;
            this.labelLicense.Location = new System.Drawing.Point(-2, 211);
            this.labelLicense.Name = "labelLicense";
            this.labelLicense.Size = new System.Drawing.Size(77, 12);
            this.labelLicense.TabIndex = 60;
            this.labelLicense.Text = "License Exp.";
            // 
            // textBoxCopyright
            // 
            this.textBoxCopyright.Location = new System.Drawing.Point(96, 174);
            this.textBoxCopyright.Name = "textBoxCopyright";
            this.textBoxCopyright.Size = new System.Drawing.Size(631, 21);
            this.textBoxCopyright.TabIndex = 7;
            this.textBoxCopyright.Tag = "Copyright";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(-2, 177);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 59;
            this.label14.Text = "Copyright:";
            // 
            // textBoxVersion
            // 
            this.textBoxVersion.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxVersion.Location = new System.Drawing.Point(495, 0);
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.Size = new System.Drawing.Size(246, 21);
            this.textBoxVersion.TabIndex = 0;
            this.textBoxVersion.Tag = "VersionString";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 66;
            this.label1.Text = "Package Version*";
            // 
            // licenseBox
            // 
            this.licenseBox.FormattingEnabled = true;
            this.licenseBox.Location = new System.Drawing.Point(96, 213);
            this.licenseBox.Name = "licenseBox";
            this.licenseBox.Size = new System.Drawing.Size(246, 20);
            this.licenseBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 68;
            this.label2.Text = "Repository:";
            // 
            // textBoxRepositoryUrl
            // 
            this.textBoxRepositoryUrl.Location = new System.Drawing.Point(191, 243);
            this.textBoxRepositoryUrl.Name = "textBoxRepositoryUrl";
            this.textBoxRepositoryUrl.Size = new System.Drawing.Size(536, 21);
            this.textBoxRepositoryUrl.TabIndex = 11;
            this.textBoxRepositoryUrl.Tag = "RepositoryUrl";
            // 
            // textBoxRepositoryType
            // 
            this.textBoxRepositoryType.Location = new System.Drawing.Point(96, 243);
            this.textBoxRepositoryType.Name = "textBoxRepositoryType";
            this.textBoxRepositoryType.Size = new System.Drawing.Size(89, 21);
            this.textBoxRepositoryType.TabIndex = 10;
            this.textBoxRepositoryType.Tag = "RepositoryType";
            this.textBoxRepositoryType.Text = "git";
            // 
            // btnOpenIconDir
            // 
            this.btnOpenIconDir.FlatAppearance.BorderSize = 0;
            this.btnOpenIconDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenIconDir.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenIconDir.Location = new System.Drawing.Point(733, 300);
            this.btnOpenIconDir.Name = "btnOpenIconDir";
            this.btnOpenIconDir.Size = new System.Drawing.Size(23, 23);
            this.btnOpenIconDir.TabIndex = 14;
            this.btnOpenIconDir.UseVisualStyleBackColor = true;
            this.btnOpenIconDir.Click += new System.EventHandler(this.btnOpenIconDir_Click);
            // 
            // openIconFileDialog
            // 
            this.openIconFileDialog.Filter = "Image files|*.png;*.jpg;*.jpeg";
            // 
            // buttonOpenReadme
            // 
            this.buttonOpenReadme.FlatAppearance.BorderSize = 0;
            this.buttonOpenReadme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenReadme.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.buttonOpenReadme.Location = new System.Drawing.Point(732, 146);
            this.buttonOpenReadme.Name = "buttonOpenReadme";
            this.buttonOpenReadme.Size = new System.Drawing.Size(23, 23);
            this.buttonOpenReadme.TabIndex = 6;
            this.buttonOpenReadme.UseVisualStyleBackColor = true;
            this.buttonOpenReadme.Click += new System.EventHandler(this.buttonOpenReadme_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-2, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 71;
            this.label4.Text = "Readme:";
            // 
            // textBoxReadme
            // 
            this.textBoxReadme.Location = new System.Drawing.Point(95, 146);
            this.textBoxReadme.Name = "textBoxReadme";
            this.textBoxReadme.Size = new System.Drawing.Size(631, 21);
            this.textBoxReadme.TabIndex = 5;
            this.textBoxReadme.Tag = "Readme";
            // 
            // openReadmeDialog
            // 
            this.openReadmeDialog.Filter = "Markdown File|*.md";
            // 
            // PackageMetadataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.buttonOpenReadme);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxReadme);
            this.Controls.Add(this.btnOpenIconDir);
            this.Controls.Add(this.textBoxRepositoryType);
            this.Controls.Add(this.textBoxRepositoryUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.licenseBox);
            this.Controls.Add(this.textBoxVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxRLA);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBoxTags);
            this.Controls.Add(this.labelIcon);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxIconUrl);
            this.Controls.Add(this.textBoxProjectUrl);
            this.Controls.Add(this.labelLicense);
            this.Controls.Add(this.textBoxCopyright);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.textBoxAuthors);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxOwners);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxReleaseNotes);
            this.Name = "PackageMetadataControl";
            this.Size = new System.Drawing.Size(759, 360);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxAuthors;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxOwners;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxReleaseNotes;
        private System.Windows.Forms.CheckBox checkBoxRLA;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxTags;
        private System.Windows.Forms.Label labelIcon;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxIconUrl;
        private System.Windows.Forms.TextBox textBoxProjectUrl;
        private System.Windows.Forms.Label labelLicense;
        private System.Windows.Forms.TextBox textBoxCopyright;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox licenseBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRepositoryUrl;
        private System.Windows.Forms.TextBox textBoxRepositoryType;
        private System.Windows.Forms.Button btnOpenIconDir;
        private System.Windows.Forms.OpenFileDialog openIconFileDialog;
        private System.Windows.Forms.Button buttonOpenReadme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxReadme;
        private System.Windows.Forms.OpenFileDialog openReadmeDialog;
    }
}
