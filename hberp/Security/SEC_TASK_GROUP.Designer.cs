namespace LXMS
{
    partial class SEC_TASK_GROUP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEC_TASK_GROUP));
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolLock = new System.Windows.Forms.ToolStripButton();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolDel = new System.Windows.Forms.ToolStripButton();
            this.toolInactive = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cboExportType = new System.Windows.Forms.ToolStripComboBox();
            this.toolExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.widthToHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.widthToContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DBGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLock,
            this.toolRefresh,
            this.toolStripSeparator1,
            this.toolDel,
            this.toolInactive,
            this.toolStripSeparator2,
            this.cboExportType,
            this.toolExport,
            this.toolStripSeparator3,
            this.toolStripSplitButton1,
            this.toolStripSeparator4,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(614, 40);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolLock
            // 
            this.toolLock.Image = global::LXMS.Properties.Resources.Lock;
            this.toolLock.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolLock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolLock.Name = "toolLock";
            this.toolLock.Size = new System.Drawing.Size(54, 37);
            this.toolLock.Text = "Locked";
            this.toolLock.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolLock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolLock.Click += new System.EventHandler(this.toolLock_Click);
            // 
            // toolRefresh
            // 
            this.toolRefresh.Image = global::LXMS.Properties.Resources.Refresh;
            this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(56, 37);
            this.toolRefresh.Text = "Refresh";
            this.toolRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolDel
            // 
            this.toolDel.Image = global::LXMS.Properties.Resources.Delete;
            this.toolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new System.Drawing.Size(31, 37);
            this.toolDel.Text = "Del";
            this.toolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // toolInactive
            // 
            this.toolInactive.Image = global::LXMS.Properties.Resources.Inactive;
            this.toolInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolInactive.Name = "toolInactive";
            this.toolInactive.Size = new System.Drawing.Size(56, 37);
            this.toolInactive.Text = "Inactive";
            this.toolInactive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolInactive.Click += new System.EventHandler(this.toolInactive_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            this.toolStripSeparator2.Visible = false;
            // 
            // cboExportType
            // 
            this.cboExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportType.Items.AddRange(new object[] {
            "Excel(*.xls)",
            "Open Office(*.ods)",
            "Text File(*.txt)",
            "PDF(*.pdf)"});
            this.cboExportType.Name = "cboExportType";
            this.cboExportType.Size = new System.Drawing.Size(100, 40);
            // 
            // toolExport
            // 
            this.toolExport.Image = global::LXMS.Properties.Resources.Export;
            this.toolExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExport.Name = "toolExport";
            this.toolExport.Size = new System.Drawing.Size(50, 37);
            this.toolExport.Text = "Export";
            this.toolExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExport.Click += new System.EventHandler(this.toolExport_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            this.toolStripSeparator3.Visible = false;
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.widthToHeaderToolStripMenuItem,
            this.widthToContentToolStripMenuItem});
            this.toolStripSplitButton1.Image = global::LXMS.Properties.Resources.Limit;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(47, 37);
            this.toolStripSplitButton1.Text = "Size";
            this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSplitButton1.ToolTipText = "Width to content";
            this.toolStripSplitButton1.Click += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // widthToHeaderToolStripMenuItem
            // 
            this.widthToHeaderToolStripMenuItem.Name = "widthToHeaderToolStripMenuItem";
            this.widthToHeaderToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.widthToHeaderToolStripMenuItem.Text = "Width to Header";
            this.widthToHeaderToolStripMenuItem.Click += new System.EventHandler(this.widthToHeaderToolStripMenuItem_Click);
            // 
            // widthToContentToolStripMenuItem
            // 
            this.widthToContentToolStripMenuItem.Image = global::LXMS.Properties.Resources.Limit;
            this.widthToContentToolStripMenuItem.Name = "widthToContentToolStripMenuItem";
            this.widthToContentToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.widthToContentToolStripMenuItem.Text = "Width to Grid";
            this.widthToContentToolStripMenuItem.Click += new System.EventHandler(this.widthToContentToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // toolExit
            // 
            this.toolExit.Image = global::LXMS.Properties.Resources.Close;
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(32, 37);
            this.toolExit.Text = "Exit";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 393);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(614, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = false;
            this.StatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(76, 17);
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.AutoSize = false;
            this.StatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(523, 17);
            this.StatusLabel4.Spring = true;
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DBGrid
            // 
            this.DBGrid.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 40);
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.RowTemplate.Height = 23;
            this.DBGrid.Size = new System.Drawing.Size(614, 353);
            this.DBGrid.TabIndex = 7;
            this.DBGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DBGrid_CellBeginEdit);
            this.DBGrid.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DBGrid_DefaultValuesNeeded);
            this.DBGrid.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGrid_RowValidated);
            this.DBGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DBGrid_RowValidating);
            this.DBGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.DBGrid_UserDeletingRow);
            // 
            // SEC_TASK_GROUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 415);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SEC_TASK_GROUP";
            this.ShowInTaskbar = false;
            this.Text = "Task Group";
            this.Load += new System.EventHandler(this.SEC_USER_GROUP_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolLock;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem widthToHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem widthToContentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private System.Windows.Forms.DataGridView DBGrid;
        private System.Windows.Forms.ToolStripButton toolExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolDel;
        private System.Windows.Forms.ToolStripButton toolInactive;
        private System.Windows.Forms.ToolStripComboBox cboExportType;
    }
}