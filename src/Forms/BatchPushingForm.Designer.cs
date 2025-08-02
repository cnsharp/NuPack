namespace CnSharp.VisualStudio.NuPack.Forms
{
    partial class BatchPushingForm
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
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.nuGetServerOptionsControl = new CnSharp.VisualStudio.NuPack.Controls.NuGetServerOptionsControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPush = new System.Windows.Forms.ToolStripButton();
            this.groupBoxPackages = new System.Windows.Forms.GroupBox();
            this.listViewPackages = new System.Windows.Forms.ListView();
            this.columnHeaderPackage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCreatedAt = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderUploadStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripPackage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewInWindowsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxOptions.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBoxPackages.SuspendLayout();
            this.contextMenuStripPackage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.nuGetServerOptionsControl);
            this.groupBoxOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxOptions.Location = new System.Drawing.Point(0, 354);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(876, 180);
            this.groupBoxOptions.TabIndex = 0;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Server Options";
            // 
            // nuGetServerOptionsControl
            // 
            this.nuGetServerOptionsControl.IncludeSymbols = false;
            this.nuGetServerOptionsControl.Location = new System.Drawing.Point(48, 26);
            this.nuGetServerOptionsControl.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.nuGetServerOptionsControl.Name = "nuGetServerOptionsControl";
            this.nuGetServerOptionsControl.Size = new System.Drawing.Size(670, 142);
            this.nuGetServerOptionsControl.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPush});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(876, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPush
            // 
            this.toolStripButtonPush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPush.Image = global::CnSharp.VisualStudio.NuPack.Resource.Push;
            this.toolStripButtonPush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPush.Name = "toolStripButtonPush";
            this.toolStripButtonPush.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonPush.Text = "Push";
            this.toolStripButtonPush.Click += new System.EventHandler(this.toolStripButtonPush_Click);
            // 
            // groupBoxPackages
            // 
            this.groupBoxPackages.Controls.Add(this.listViewPackages);
            this.groupBoxPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPackages.Location = new System.Drawing.Point(0, 25);
            this.groupBoxPackages.Name = "groupBoxPackages";
            this.groupBoxPackages.Size = new System.Drawing.Size(876, 329);
            this.groupBoxPackages.TabIndex = 3;
            this.groupBoxPackages.TabStop = false;
            this.groupBoxPackages.Text = "Packages";
            // 
            // listViewPackages
            // 
            this.listViewPackages.CheckBoxes = true;
            this.listViewPackages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPackage,
            this.columnHeaderCreatedAt,
            this.columnHeaderUploadStatus,
            this.columnHeaderLocation});
            this.listViewPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPackages.HideSelection = false;
            this.listViewPackages.Location = new System.Drawing.Point(3, 17);
            this.listViewPackages.Name = "listViewPackages";
            this.listViewPackages.Size = new System.Drawing.Size(870, 309);
            this.listViewPackages.TabIndex = 0;
            this.listViewPackages.UseCompatibleStateImageBehavior = false;
            this.listViewPackages.View = System.Windows.Forms.View.Details;
            this.listViewPackages.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewPackages_ItemChecked);
            this.listViewPackages.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewPackages_MouseClick);
            // 
            // columnHeaderPackage
            // 
            this.columnHeaderPackage.Text = "Package";
            this.columnHeaderPackage.Width = 300;
            // 
            // columnHeaderCreatedAt
            // 
            this.columnHeaderCreatedAt.Text = "Created at";
            this.columnHeaderCreatedAt.Width = 160;
            // 
            // columnHeaderUploadStatus
            // 
            this.columnHeaderUploadStatus.Text = "Upload status";
            this.columnHeaderUploadStatus.Width = 200;
            // 
            // columnHeaderLocation
            // 
            this.columnHeaderLocation.Text = "Location";
            this.columnHeaderLocation.Width = 300;
            // 
            // contextMenuStripPackage
            // 
            this.contextMenuStripPackage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkToolStripMenuItem,
            this.viewInWindowsExplorerToolStripMenuItem});
            this.contextMenuStripPackage.Name = "contextMenuStripPackage";
            this.contextMenuStripPackage.Size = new System.Drawing.Size(228, 48);
            // 
            // checkToolStripMenuItem
            // 
            this.checkToolStripMenuItem.Name = "checkToolStripMenuItem";
            this.checkToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.checkToolStripMenuItem.Text = "Check";
            this.checkToolStripMenuItem.Click += new System.EventHandler(this.checkToolStripMenuItem_Click);
            // 
            // viewInWindowsExplorerToolStripMenuItem
            // 
            this.viewInWindowsExplorerToolStripMenuItem.Name = "viewInWindowsExplorerToolStripMenuItem";
            this.viewInWindowsExplorerToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.viewInWindowsExplorerToolStripMenuItem.Text = "View in Windows Explorer";
            this.viewInWindowsExplorerToolStripMenuItem.Click += new System.EventHandler(this.viewInWindowsExplorerToolStripMenuItem_Click);
            // 
            // BatchPushingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 534);
            this.Controls.Add(this.groupBoxPackages);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBoxOptions);
            this.Name = "BatchPushingForm";
            this.Text = "Pushing Packages";
            this.groupBoxOptions.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxPackages.ResumeLayout(false);
            this.contextMenuStripPackage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBoxPackages;
        private System.Windows.Forms.ListView listViewPackages;
        private System.Windows.Forms.ColumnHeader columnHeaderPackage;
        private System.Windows.Forms.ColumnHeader columnHeaderCreatedAt;
        private Controls.NuGetServerOptionsControl nuGetServerOptionsControl;
        private System.Windows.Forms.ToolStripButton toolStripButtonPush;
        private System.Windows.Forms.ColumnHeader columnHeaderUploadStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderLocation;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPackage;
        private System.Windows.Forms.ToolStripMenuItem viewInWindowsExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
    }
}