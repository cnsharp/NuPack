namespace CnSharp.VisualStudio.NuPack.Forms
{
    partial class AssemblyInfoForm
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.button2 = new System.Windows.Forms.Button();
            this.groupBoxProjects = new System.Windows.Forms.GroupBox();
            this.projectGrid = new System.Windows.Forms.DataGridView();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCopyright = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTrademark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.projectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxCommonInfo = new System.Windows.Forms.GroupBox();
            this.txtTrademark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLink = new System.Windows.Forms.Button();
            this.btnNewOrChange = new System.Windows.Forms.Button();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openAssemblyInfoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveAssemblyInfoFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBoxCommonInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(164, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBoxProjects
            // 
            this.groupBoxProjects.Controls.Add(this.projectGrid);
            this.groupBoxProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxProjects.Location = new System.Drawing.Point(0, 97);
            this.groupBoxProjects.Name = "groupBoxProjects";
            this.groupBoxProjects.Size = new System.Drawing.Size(1170, 367);
            this.groupBoxProjects.TabIndex = 5;
            this.groupBoxProjects.TabStop = false;
            this.groupBoxProjects.Text = "Projects";
            // 
            // projectGrid
            // 
            this.projectGrid.AllowUserToAddRows = false;
            this.projectGrid.AllowUserToDeleteRows = false;
            this.projectGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.projectGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ColProjectName,
            this.ColFileVersion,
            this.ColVersion,
            this.ColProduct,
            this.ColCompany,
            this.ColCopyright,
            this.ColTrademark});
            this.projectGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectGrid.Location = new System.Drawing.Point(3, 16);
            this.projectGrid.Name = "projectGrid";
            this.projectGrid.RowHeadersVisible = false;
            this.projectGrid.RowTemplate.Height = 23;
            this.projectGrid.Size = new System.Drawing.Size(1164, 348);
            this.projectGrid.TabIndex = 0;
            this.projectGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.projectGrid_CellBeginEdit);
            this.projectGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectGrid_CellEndEdit);
            this.projectGrid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectGrid_CellValidated);
            this.projectGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.projectGrid_CellValidating);
            this.projectGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectGrid_CellValueChanged);
            this.projectGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.projectGrid_CurrentCellDirtyStateChanged);
            this.projectGrid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.projectGrid_DataBindingComplete);
            // 
            // colChecked
            // 
            this.colChecked.FillWeight = 40F;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            // 
            // ColProjectName
            // 
            this.ColProjectName.DataPropertyName = "ProjectName";
            this.ColProjectName.HeaderText = "Project Name";
            this.ColProjectName.Name = "ColProjectName";
            this.ColProjectName.ReadOnly = true;
            // 
            // ColFileVersion
            // 
            this.ColFileVersion.DataPropertyName = "FileVersion";
            this.ColFileVersion.FillWeight = 80F;
            this.ColFileVersion.HeaderText = "File Version";
            this.ColFileVersion.Name = "ColFileVersion";
            // 
            // ColVersion
            // 
            this.ColVersion.DataPropertyName = "Version";
            this.ColVersion.FillWeight = 80F;
            this.ColVersion.HeaderText = "Version";
            this.ColVersion.Name = "ColVersion";
            // 
            // ColProduct
            // 
            this.ColProduct.DataPropertyName = "Product";
            this.ColProduct.HeaderText = "Product Name";
            this.ColProduct.Name = "ColProduct";
            // 
            // ColCompany
            // 
            this.ColCompany.DataPropertyName = "Company";
            this.ColCompany.HeaderText = "Company";
            this.ColCompany.Name = "ColCompany";
            // 
            // ColCopyright
            // 
            this.ColCopyright.DataPropertyName = "Copyright";
            this.ColCopyright.FillWeight = 150F;
            this.ColCopyright.HeaderText = "Copyright";
            this.ColCopyright.Name = "ColCopyright";
            // 
            // ColTrademark
            // 
            this.ColTrademark.DataPropertyName = "Trademark";
            this.ColTrademark.HeaderText = "Trademark";
            this.ColTrademark.Name = "ColTrademark";
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.Location = new System.Drawing.Point(21, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(93, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Save and Exit";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1170, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // groupBoxCommonInfo
            // 
            this.groupBoxCommonInfo.Controls.Add(this.txtTrademark);
            this.groupBoxCommonInfo.Controls.Add(this.label5);
            this.groupBoxCommonInfo.Controls.Add(this.txtVersion);
            this.groupBoxCommonInfo.Controls.Add(this.label1);
            this.groupBoxCommonInfo.Controls.Add(this.btnLink);
            this.groupBoxCommonInfo.Controls.Add(this.btnNewOrChange);
            this.groupBoxCommonInfo.Controls.Add(this.txtCopyright);
            this.groupBoxCommonInfo.Controls.Add(this.label4);
            this.groupBoxCommonInfo.Controls.Add(this.txtProduct);
            this.groupBoxCommonInfo.Controls.Add(this.label3);
            this.groupBoxCommonInfo.Controls.Add(this.txtCompany);
            this.groupBoxCommonInfo.Controls.Add(this.label2);
            this.groupBoxCommonInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCommonInfo.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCommonInfo.Name = "groupBoxCommonInfo";
            this.groupBoxCommonInfo.Size = new System.Drawing.Size(1170, 97);
            this.groupBoxCommonInfo.TabIndex = 16;
            this.groupBoxCommonInfo.TabStop = false;
            this.groupBoxCommonInfo.Text = "Common Info";
            // 
            // txtTrademark
            // 
            this.txtTrademark.Location = new System.Drawing.Point(512, 66);
            this.txtTrademark.Name = "txtTrademark";
            this.txtTrademark.Size = new System.Drawing.Size(270, 20);
            this.txtTrademark.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(433, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Trademark";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(874, 25);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(184, 20);
            this.txtVersion.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(820, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Version";
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(968, 64);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(90, 23);
            this.btnLink.TabIndex = 6;
            this.btnLink.Text = "Link Exists ...";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // btnNewOrChange
            // 
            this.btnNewOrChange.Location = new System.Drawing.Point(874, 63);
            this.btnNewOrChange.Name = "btnNewOrChange";
            this.btnNewOrChange.Size = new System.Drawing.Size(88, 23);
            this.btnNewOrChange.TabIndex = 5;
            this.btnNewOrChange.Text = "New...";
            this.btnNewOrChange.UseVisualStyleBackColor = true;
            this.btnNewOrChange.Click += new System.EventHandler(this.btnNewOrChange_Click);
            // 
            // txtCopyright
            // 
            this.txtCopyright.Location = new System.Drawing.Point(111, 66);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.Size = new System.Drawing.Size(270, 20);
            this.txtCopyright.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Copyright *";
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(511, 27);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(271, 20);
            this.txtProduct.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Product *";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(111, 27);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(270, 20);
            this.txtCompany.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Company *";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 464);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 43);
            this.panel1.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(900, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 43);
            this.panel2.TabIndex = 9;
            // 
            // openAssemblyInfoFileDialog
            // 
            this.openAssemblyInfoFileDialog.DefaultExt = "*.cs|*.vb";
            this.openAssemblyInfoFileDialog.Title = "Open Common Assembly Info File";
            // 
            // AssemblyInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 529);
            this.Controls.Add(this.groupBoxProjects);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxCommonInfo);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblyInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Assembly Info.";
            this.Load += new System.EventHandler(this.FormVersionLoad);
            this.groupBoxProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.projectGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectBindingSource)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxCommonInfo.ResumeLayout(false);
            this.groupBoxCommonInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBoxProjects;
        private System.Windows.Forms.DataGridView projectGrid;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource projectBindingSource;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox groupBoxCommonInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCopyright;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNewOrChange;
        private System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.OpenFileDialog openAssemblyInfoFileDialog;
        private System.Windows.Forms.SaveFileDialog saveAssemblyInfoFileDialog;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTrademark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFileVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCopyright;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTrademark;
    }
}