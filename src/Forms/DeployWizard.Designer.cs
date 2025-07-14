namespace CnSharp.VisualStudio.NuPack.Forms
{
    partial class DeployWizard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployWizard));
            this.stepWizardControl = new AeroWizard.StepWizardControl();
            this.wizardPageMetadata = new AeroWizard.WizardPage();
            this.wizardPageOptions = new AeroWizard.WizardPage();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openAssemblyInfoFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // stepWizardControl
            // 
            this.stepWizardControl.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl.Name = "stepWizardControl";
            this.stepWizardControl.Pages.Add(this.wizardPageMetadata);
            this.stepWizardControl.Pages.Add(this.wizardPageOptions);
            this.stepWizardControl.Size = new System.Drawing.Size(1088, 679);
            this.stepWizardControl.StepListFont = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.stepWizardControl.TabIndex = 27;
            this.stepWizardControl.Title = "Pack & Deploy";
            this.stepWizardControl.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("stepWizardControl.TitleIcon")));
            // 
            // wizardPageMetadata
            // 
            this.wizardPageMetadata.AllowBack = false;
            this.wizardPageMetadata.Name = "wizardPageMetadata";
            this.wizardPageMetadata.NextPage = this.wizardPageOptions;
            this.wizardPageMetadata.Size = new System.Drawing.Size(890, 523);
            this.stepWizardControl.SetStepText(this.wizardPageMetadata, "Metadata");
            this.wizardPageMetadata.TabIndex = 2;
            this.wizardPageMetadata.Text = "Metadata";
            // 
            // wizardPageOptions
            // 
            this.wizardPageOptions.Name = "wizardPageOptions";
            this.wizardPageOptions.Size = new System.Drawing.Size(890, 523);
            this.stepWizardControl.SetStepText(this.wizardPageOptions, "Build/Deploy");
            this.wizardPageOptions.TabIndex = 3;
            this.wizardPageOptions.Text = "Build/Deploy";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openAssemblyInfoFileDialog
            // 
            this.openAssemblyInfoFileDialog.DefaultExt = "*.cs|*.vb";
            this.openAssemblyInfoFileDialog.Title = "Open Common Assembly Info File";
            // 
            // DeployWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1088, 679);
            this.Controls.Add(this.stepWizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeployWizard";
            this.Text = "Deploy Wizard";
            this.Load += new System.EventHandler(this.DeployWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private AeroWizard.StepWizardControl stepWizardControl;
        private AeroWizard.WizardPage wizardPageMetadata;
        private AeroWizard.WizardPage wizardPageOptions;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.OpenFileDialog openAssemblyInfoFileDialog;
    }
}