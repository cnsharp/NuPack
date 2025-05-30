namespace CnSharp.VisualStudio.NuPack.Controls
{
    partial class NuGetPackOptionsControl
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
            this.panelNuGetPackArgs = new System.Windows.Forms.Panel();
            this.checkBoxSymbols = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeReferencedProjects = new System.Windows.Forms.CheckBox();
            this.panelNuGetPackArgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNuGetPackArgs
            // 
            this.panelNuGetPackArgs.Controls.Add(this.checkBoxSymbols);
            this.panelNuGetPackArgs.Controls.Add(this.checkBoxIncludeReferencedProjects);
            this.panelNuGetPackArgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNuGetPackArgs.Location = new System.Drawing.Point(0, 0);
            this.panelNuGetPackArgs.Name = "panelNuGetPackArgs";
            this.panelNuGetPackArgs.Size = new System.Drawing.Size(442, 26);
            this.panelNuGetPackArgs.TabIndex = 25;
            // 
            // checkBoxSymbols
            // 
            this.checkBoxSymbols.AutoSize = true;
            this.checkBoxSymbols.Location = new System.Drawing.Point(3, 3);
            this.checkBoxSymbols.Name = "checkBoxSymbols";
            this.checkBoxSymbols.Size = new System.Drawing.Size(72, 16);
            this.checkBoxSymbols.TabIndex = 23;
            this.checkBoxSymbols.Text = "-Symbols";
            this.checkBoxSymbols.UseVisualStyleBackColor = true;
            this.checkBoxSymbols.CheckedChanged += new System.EventHandler(this.checkBoxSymbols_CheckedChanged);
            // 
            // checkBoxIncludeReferencedProjects
            // 
            this.checkBoxIncludeReferencedProjects.AutoSize = true;
            this.checkBoxIncludeReferencedProjects.Location = new System.Drawing.Point(176, 3);
            this.checkBoxIncludeReferencedProjects.Name = "checkBoxIncludeReferencedProjects";
            this.checkBoxIncludeReferencedProjects.Size = new System.Drawing.Size(180, 16);
            this.checkBoxIncludeReferencedProjects.TabIndex = 22;
            this.checkBoxIncludeReferencedProjects.Text = "-IncludeReferencedProjects";
            this.checkBoxIncludeReferencedProjects.UseVisualStyleBackColor = true;
            // 
            // NuGetPackOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelNuGetPackArgs);
            this.Name = "NuGetPackOptionsControl";
            this.Size = new System.Drawing.Size(442, 26);
            this.panelNuGetPackArgs.ResumeLayout(false);
            this.panelNuGetPackArgs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNuGetPackArgs;
        private System.Windows.Forms.CheckBox checkBoxSymbols;
        private System.Windows.Forms.CheckBox checkBoxIncludeReferencedProjects;
    }
}
