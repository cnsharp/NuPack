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
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBoxPackOptions = new System.Windows.Forms.GroupBox();
            this.panelPackOptions = new System.Windows.Forms.Panel();
            this.btnOpenOutputDir = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nuGetServerOptionsControl = new CnSharp.VisualStudio.NuPack.Controls.NuGetServerOptionsControl();
            this.groupBoxPackOptions.SuspendLayout();
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
            // groupBoxPackOptions
            // 
            this.groupBoxPackOptions.Controls.Add(this.panelPackOptions);
            this.groupBoxPackOptions.Controls.Add(this.label10);
            this.groupBoxPackOptions.Controls.Add(this.textBoxOutputDir);
            this.groupBoxPackOptions.Controls.Add(this.btnOpenOutputDir);
            this.groupBoxPackOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPackOptions.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPackOptions.Name = "groupBoxPackOptions";
            this.groupBoxPackOptions.Size = new System.Drawing.Size(731, 187);
            this.groupBoxPackOptions.TabIndex = 107;
            this.groupBoxPackOptions.TabStop = false;
            this.groupBoxPackOptions.Text = "Pack Options";
            // 
            // panelPackOptions
            // 
            this.panelPackOptions.Location = new System.Drawing.Point(132, 81);
            this.panelPackOptions.Name = "panelPackOptions";
            this.panelPackOptions.Size = new System.Drawing.Size(537, 100);
            this.panelPackOptions.TabIndex = 22;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nuGetServerOptionsControl);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(731, 198);
            this.groupBox2.TabIndex = 108;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Options";
            // 
            // nuGetServerOptionsControl
            // 
            this.nuGetServerOptionsControl.Location = new System.Drawing.Point(28, 34);
            this.nuGetServerOptionsControl.Name = "nuGetServerOptionsControl";
            this.nuGetServerOptionsControl.Size = new System.Drawing.Size(670, 142);
            this.nuGetServerOptionsControl.TabIndex = 0;
            // 
            // PackOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxPackOptions);
            this.Name = "PackOptionsControl";
            this.Size = new System.Drawing.Size(731, 385);
            this.groupBoxPackOptions.ResumeLayout(false);
            this.groupBoxPackOptions.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Button btnOpenOutputDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox groupBoxPackOptions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelPackOptions;
        private NuGetServerOptionsControl nuGetServerOptionsControl;
    }
}
