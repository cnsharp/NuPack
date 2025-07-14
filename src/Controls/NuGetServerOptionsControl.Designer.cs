namespace CnSharp.VisualStudio.NuPack.Controls
{
    partial class NuGetServerOptionsControl
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
            this.symbolServerBox = new System.Windows.Forms.ComboBox();
            this.sourceBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxApiKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSymbolServerApiKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // symbolServerBox
            // 
            this.symbolServerBox.FormattingEnabled = true;
            this.symbolServerBox.Location = new System.Drawing.Point(107, 81);
            this.symbolServerBox.Name = "symbolServerBox";
            this.symbolServerBox.Size = new System.Drawing.Size(559, 20);
            this.symbolServerBox.TabIndex = 112;
            // 
            // sourceBox
            // 
            this.sourceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceBox.FormattingEnabled = true;
            this.sourceBox.Location = new System.Drawing.Point(107, 3);
            this.sourceBox.Name = "sourceBox";
            this.sourceBox.Size = new System.Drawing.Size(559, 20);
            this.sourceBox.TabIndex = 110;
            this.sourceBox.SelectedIndexChanged += new System.EventHandler(this.sourceBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 113;
            this.label1.Text = "NuGet Server:";
            // 
            // textBoxApiKey
            // 
            this.textBoxApiKey.Location = new System.Drawing.Point(107, 39);
            this.textBoxApiKey.Name = "textBoxApiKey";
            this.textBoxApiKey.Size = new System.Drawing.Size(559, 21);
            this.textBoxApiKey.TabIndex = 111;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 114;
            this.label2.Text = "API Key:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 115;
            this.label5.Text = "Symbol Server:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 117;
            this.label3.Text = "API Key:";
            // 
            // textBoxSymbolServerApiKey
            // 
            this.textBoxSymbolServerApiKey.Location = new System.Drawing.Point(107, 118);
            this.textBoxSymbolServerApiKey.Name = "textBoxSymbolServerApiKey";
            this.textBoxSymbolServerApiKey.Size = new System.Drawing.Size(559, 21);
            this.textBoxSymbolServerApiKey.TabIndex = 116;
            // 
            // NuGetServerOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSymbolServerApiKey);
            this.Controls.Add(this.symbolServerBox);
            this.Controls.Add(this.sourceBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxApiKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Name = "NuGetServerOptionsControl";
            this.Size = new System.Drawing.Size(670, 142);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox symbolServerBox;
        private System.Windows.Forms.ComboBox sourceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxApiKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSymbolServerApiKey;
    }
}
