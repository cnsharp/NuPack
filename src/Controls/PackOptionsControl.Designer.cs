namespace CnSharp.VisualStudio.NuPack.Controls
{
    partial class PackOptionsControl
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
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.btnOpenOutputDir = new System.Windows.Forms.Button();
            this.checkBoxIncludeSymbols = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeSource = new System.Windows.Forms.CheckBox();
            this.checkBoxNoDependencies = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.sourceBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSymbolServerApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNoBuild = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.symbolServerBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(173, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "Package Output directory:(*)";
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Location = new System.Drawing.Point(132, 53);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(537, 21);
            this.textBoxOutputDir.TabIndex = 0;
            this.textBoxOutputDir.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxOutputDir_Validating);
            this.textBoxOutputDir.Validated += new System.EventHandler(this.textBoxOutputDir_Validated);
            // 
            // btnOpenOutputDir
            // 
            this.btnOpenOutputDir.FlatAppearance.BorderSize = 0;
            this.btnOpenOutputDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenOutputDir.Image = global::CnSharp.VisualStudio.NuPack.Resource.folder;
            this.btnOpenOutputDir.Location = new System.Drawing.Point(675, 53);
            this.btnOpenOutputDir.Name = "btnOpenOutputDir";
            this.btnOpenOutputDir.Size = new System.Drawing.Size(23, 23);
            this.btnOpenOutputDir.TabIndex = 1;
            this.btnOpenOutputDir.UseVisualStyleBackColor = true;
            this.btnOpenOutputDir.Click += new System.EventHandler(this.btnOpenOutputDir_Click);
            // 
            // checkBoxIncludeSymbols
            // 
            this.checkBoxIncludeSymbols.AutoSize = true;
            this.checkBoxIncludeSymbols.Location = new System.Drawing.Point(132, 90);
            this.checkBoxIncludeSymbols.Name = "checkBoxIncludeSymbols";
            this.checkBoxIncludeSymbols.Size = new System.Drawing.Size(126, 16);
            this.checkBoxIncludeSymbols.TabIndex = 2;
            this.checkBoxIncludeSymbols.Text = "--include-symbols";
            this.checkBoxIncludeSymbols.UseVisualStyleBackColor = true;
            this.checkBoxIncludeSymbols.CheckedChanged += new System.EventHandler(this.checkBoxIncludeSymbols_CheckedChanged);
            // 
            // checkBoxIncludeSource
            // 
            this.checkBoxIncludeSource.AutoSize = true;
            this.checkBoxIncludeSource.Location = new System.Drawing.Point(308, 90);
            this.checkBoxIncludeSource.Name = "checkBoxIncludeSource";
            this.checkBoxIncludeSource.Size = new System.Drawing.Size(120, 16);
            this.checkBoxIncludeSource.TabIndex = 3;
            this.checkBoxIncludeSource.Text = "--include-source";
            this.checkBoxIncludeSource.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoDependencies
            // 
            this.checkBoxNoDependencies.AutoSize = true;
            this.checkBoxNoDependencies.Location = new System.Drawing.Point(308, 118);
            this.checkBoxNoDependencies.Name = "checkBoxNoDependencies";
            this.checkBoxNoDependencies.Size = new System.Drawing.Size(126, 16);
            this.checkBoxNoDependencies.TabIndex = 5;
            this.checkBoxNoDependencies.Text = "--no-dependencies";
            this.checkBoxNoDependencies.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "NuGet Server:";
            // 
            // sourceBox
            // 
            this.sourceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Location = new System.Drawing.Point(132, 36);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(559, 20);
            this.sourceBox.TabIndex = 0;
            this.sourceBox.SelectedIndexChanged += new System.EventHandler(this.sourceBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 101;
            this.label2.Text = "API Key:";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(132, 72);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(559, 21);
            this.textBoxApiKey.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 103;
            this.label5.Text = "Symbol Server:";
            // 
            // textBoxSymbolServerApiKey
            // 
            this.textBoxSymbolServerApiKey.Location = new System.Drawing.Point(132, 153);
            this.textBoxSymbolServerApiKey.Name = "textBoxSymbolServerApiKey";
            this.textBoxSymbolServerApiKey.Size = new System.Drawing.Size(559, 21);
            this.textBoxSymbolServerApiKey.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 106;
            this.label3.Text = "API Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNoBuild);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxOutputDir);
            this.groupBox1.Controls.Add(this.btnOpenOutputDir);
            this.groupBox1.Controls.Add(this.checkBoxIncludeSymbols);
            this.groupBox1.Controls.Add(this.checkBoxIncludeSource);
            this.groupBox1.Controls.Add(this.checkBoxNoDependencies);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 151);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pack Options";
            // 
            // checkBoxNoBuild
            // 
            this.checkBoxNoBuild.AutoSize = true;
            this.checkBoxNoBuild.Location = new System.Drawing.Point(132, 118);
            this.checkBoxNoBuild.Name = "checkBoxNoBuild";
            this.checkBoxNoBuild.Size = new System.Drawing.Size(84, 16);
            this.checkBoxNoBuild.TabIndex = 4;
            this.checkBoxNoBuild.Text = "--no-build";
            this.checkBoxNoBuild.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.symbolServerBox);
            this.groupBox2.Controls.Add(this.sourceBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxApiKey);
            this.groupBox2.Controls.Add(this.textBoxSymbolServerApiKey);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 188);
            this.groupBox2.TabIndex = 108;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deploy Options";
            // 
            // symbolServerBox
            // 
            this.symbolServerBox.FormattingEnabled = true;
            this.symbolServerBox.Location = new System.Drawing.Point(132, 114);
            this.symbolServerBox.Name = "symbolServerBox";
            this.symbolServerBox.Size = new System.Drawing.Size(559, 20);
            this.symbolServerBox.TabIndex = 2;
            // 
            // PackOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PackOptionsControl";
            this.Size = new System.Drawing.Size(728, 339);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Button btnOpenOutputDir;
        private System.Windows.Forms.CheckBox checkBoxIncludeSymbols;
        private System.Windows.Forms.CheckBox checkBoxIncludeSource;
        private System.Windows.Forms.CheckBox checkBoxNoDependencies;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox sourceBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSymbolServerApiKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxNoBuild;
        private System.Windows.Forms.ComboBox symbolServerBox;
    }
}
