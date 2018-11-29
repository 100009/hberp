namespace LXMS
{
    partial class SEC_SYSTEM_PARAMETERS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEC_SYSTEM_PARAMETERS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.widthToHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.widthToContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolLock = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DBGrid = new LXMS.RowMergeView();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
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
            // widthToHeaderToolStripMenuItem
            // 
            this.widthToHeaderToolStripMenuItem.Name = "widthToHeaderToolStripMenuItem";
            this.widthToHeaderToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.widthToHeaderToolStripMenuItem.Text = "Width to Header";
            this.widthToHeaderToolStripMenuItem.Click += new System.EventHandler(this.widthToHeaderToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 40);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2,
            this.StatusLabel3,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(689, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = false;
            this.StatusLabel1.Image = global::LXMS.Properties.Resources.Quilt;
            this.StatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(76, 17);
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.AutoSize = false;
            this.StatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(120, 17);
            this.StatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.AutoSize = false;
            this.StatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(104, 17);
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.AutoSize = false;
            this.StatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(374, 17);
            this.StatusLabel4.Spring = true;
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // widthToContentToolStripMenuItem
            // 
            this.widthToContentToolStripMenuItem.Image = global::LXMS.Properties.Resources.Limit;
            this.widthToContentToolStripMenuItem.Name = "widthToContentToolStripMenuItem";
            this.widthToContentToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.widthToContentToolStripMenuItem.Text = "Width to Grid";
            this.widthToContentToolStripMenuItem.Click += new System.EventHandler(this.widthToContentToolStripMenuItem_Click);
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
            this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLock,
            this.toolStripSeparator1,
            this.toolRefresh,
            this.toolStripSeparator3,
            this.toolStripSplitButton1,
            this.toolStripSeparator4,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(689, 40);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // DBGrid
            // 
            this.DBGrid.AllowUserToAddRows = false;
            this.DBGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.DBGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DBGrid.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 40);
            this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.ReadOnly = true;
            this.DBGrid.RowChanged = false;
            this.DBGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DBGrid.RowTemplate.Height = 23;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(689, 400);
            this.DBGrid.TabIndex = 7;
            this.DBGrid.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DBGrid_CellBeginEdit);
            this.DBGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DBGrid_DataError);
            this.DBGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DBGrid_RowValidating);
            // 
            // SEC_SYSTEM_PARAMETERS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 462);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SEC_SYSTEM_PARAMETERS";
            this.ShowInTaskbar = false;
            this.Text = "System Parameters";
            this.Load += new System.EventHandler(this.SEC_SYSTEM_PARAMETERS_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripMenuItem widthToHeaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private System.Windows.Forms.ToolStripMenuItem widthToContentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolLock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private RowMergeView DBGrid;
    }
}