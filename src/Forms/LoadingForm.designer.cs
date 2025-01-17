namespace CnSharp.VisualStudio.NuPack.Forms
{
    partial class LoadingForm
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.closePicture = new System.Windows.Forms.PictureBox();
            this.reloadPicture = new System.Windows.Forms.PictureBox();
            this.loadingPicture = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 155);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(332, 25);
            this.progressBar.TabIndex = 0;
            this.progressBar.Visible = false;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ForeColor = System.Drawing.Color.Red;
            this.label.Location = new System.Drawing.Point(96, 99);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(54, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Loading...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.closePicture);
            this.panel1.Controls.Add(this.reloadPicture);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.loadingPicture);
            this.panel1.Location = new System.Drawing.Point(51, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 184);
            this.panel1.TabIndex = 2;
            // 
            // closePicture
            // 
            this.closePicture.Image = global::CnSharp.VisualStudio.NuPack.Resource.close;
            this.closePicture.Location = new System.Drawing.Point(285, 11);
            this.closePicture.Name = "closePicture";
            this.closePicture.Size = new System.Drawing.Size(44, 54);
            this.closePicture.TabIndex = 5;
            this.closePicture.TabStop = false;
            this.closePicture.Visible = false;
            this.closePicture.Click += new System.EventHandler(this.closePicture_Click);
            // 
            // reloadPicture
            // 
            this.reloadPicture.Image = global::CnSharp.VisualStudio.NuPack.Resource.reload32;
            this.reloadPicture.Location = new System.Drawing.Point(213, 11);
            this.reloadPicture.Name = "reloadPicture";
            this.reloadPicture.Size = new System.Drawing.Size(44, 54);
            this.reloadPicture.TabIndex = 4;
            this.reloadPicture.TabStop = false;
            this.reloadPicture.Visible = false;
            this.reloadPicture.Click += new System.EventHandler(this.ReloadPicture_Click);
            // 
            // loadingPicture
            // 
            this.loadingPicture.Image = global::CnSharp.VisualStudio.NuPack.Resource.loading32;
            this.loadingPicture.Location = new System.Drawing.Point(138, 11);
            this.loadingPicture.Name = "loadingPicture";
            this.loadingPicture.Size = new System.Drawing.Size(49, 54);
            this.loadingPicture.TabIndex = 3;
            this.loadingPicture.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Beige;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 377);
            this.panel2.TabIndex = 3;
            // 
            // WaitingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 377);
            this.Controls.Add(this.panel2);
            this.Name = "LoadingForm";
            this.Opacity = 0.75D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWait_FormClosing);
            this.Load += new System.EventHandler(this.FrmWait_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reloadPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox loadingPicture;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox reloadPicture;
        private System.Windows.Forms.PictureBox closePicture;
    }
}