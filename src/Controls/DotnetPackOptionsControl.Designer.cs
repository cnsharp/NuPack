namespace CnSharp.VisualStudio.NuPack.Controls
{
    partial class DotnetPackOptionsControl
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
            this.panelDotNetPackArgs = new System.Windows.Forms.Panel();
            this.checkBoxIncludeSymbols = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeSource = new System.Windows.Forms.CheckBox();
            this.checkBoxNoBuild = new System.Windows.Forms.CheckBox();
            this.checkBoxNoDependencies = new System.Windows.Forms.CheckBox();
            this.panelDotNetPackArgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDotNetPackArgs
            // 
            this.panelDotNetPackArgs.Controls.Add(this.checkBoxIncludeSymbols);
            this.panelDotNetPackArgs.Controls.Add(this.checkBoxIncludeSource);
            this.panelDotNetPackArgs.Controls.Add(this.checkBoxNoBuild);
            this.panelDotNetPackArgs.Controls.Add(this.checkBoxNoDependencies);
            this.panelDotNetPackArgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDotNetPackArgs.Location = new System.Drawing.Point(0, 0);
            this.panelDotNetPackArgs.Name = "panelDotNetPackArgs";
            this.panelDotNetPackArgs.Size = new System.Drawing.Size(421, 76);
            this.panelDotNetPackArgs.TabIndex = 24;
            // 
            // checkBoxIncludeSymbols
            // 
            this.checkBoxIncludeSymbols.AutoSize = true;
            this.checkBoxIncludeSymbols.Location = new System.Drawing.Point(3, 3);
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
            this.checkBoxIncludeSource.Location = new System.Drawing.Point(176, 3);
            this.checkBoxIncludeSource.Name = "checkBoxIncludeSource";
            this.checkBoxIncludeSource.Size = new System.Drawing.Size(120, 16);
            this.checkBoxIncludeSource.TabIndex = 3;
            this.checkBoxIncludeSource.Text = "--include-source";
            this.checkBoxIncludeSource.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoBuild
            // 
            this.checkBoxNoBuild.AutoSize = true;
            this.checkBoxNoBuild.Location = new System.Drawing.Point(3, 45);
            this.checkBoxNoBuild.Name = "checkBoxNoBuild";
            this.checkBoxNoBuild.Size = new System.Drawing.Size(84, 16);
            this.checkBoxNoBuild.TabIndex = 4;
            this.checkBoxNoBuild.Text = "--no-build";
            this.checkBoxNoBuild.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoDependencies
            // 
            this.checkBoxNoDependencies.AutoSize = true;
            this.checkBoxNoDependencies.Location = new System.Drawing.Point(176, 45);
            this.checkBoxNoDependencies.Name = "checkBoxNoDependencies";
            this.checkBoxNoDependencies.Size = new System.Drawing.Size(126, 16);
            this.checkBoxNoDependencies.TabIndex = 5;
            this.checkBoxNoDependencies.Text = "--no-dependencies";
            this.checkBoxNoDependencies.UseVisualStyleBackColor = true;
            // 
            // DotnetPackOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDotNetPackArgs);
            this.Name = "DotnetPackOptionsControl";
            this.Size = new System.Drawing.Size(421, 76);
            this.panelDotNetPackArgs.ResumeLayout(false);
            this.panelDotNetPackArgs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDotNetPackArgs;
        private System.Windows.Forms.CheckBox checkBoxIncludeSymbols;
        private System.Windows.Forms.CheckBox checkBoxIncludeSource;
        private System.Windows.Forms.CheckBox checkBoxNoBuild;
        private System.Windows.Forms.CheckBox checkBoxNoDependencies;
    }
}
