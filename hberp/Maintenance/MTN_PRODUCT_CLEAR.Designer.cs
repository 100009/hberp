namespace LXMS
{
    partial class MTN_PRODUCT_CLEAR
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MTN_PRODUCT_CLEAR));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabControl1 = new LXMS.TabControlEx();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.DBGrid = new LXMS.RowMergeView();
			this.toolStrip1 = new LXMS.ToolStripEx();
			this.toolRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolCheckAll = new System.Windows.Forms.ToolStripButton();
			this.toolCheckNone = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolClear = new System.Windows.Forms.ToolStripButton();
			this.toolClose = new System.Windows.Forms.ToolStripButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.DBGrid2 = new LXMS.RowMergeView();
			this.toolStripEx1 = new LXMS.ToolStripEx();
			this.toolRefresh2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolCheckAll2 = new System.Windows.Forms.ToolStripButton();
			this.toolCheckNone2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolRestore2 = new System.Windows.Forms.ToolStripButton();
			this.toolClose2 = new System.Windows.Forms.ToolStripButton();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).BeginInit();
			this.toolStripEx1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(747, 500);
			this.tabControl1.TabIndex = 63;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.DBGrid);
			this.tabPage1.Controls.Add(this.toolStrip1);
			this.tabPage1.Location = new System.Drawing.Point(4, 30);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage1.Size = new System.Drawing.Size(739, 466);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Product Hide";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// DBGrid
			// 
			this.DBGrid.AllowUserToAddRows = false;
			this.DBGrid.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
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
			this.DBGrid.Location = new System.Drawing.Point(4, 50);
			this.DBGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
			this.DBGrid.MultiSelect = false;
			this.DBGrid.Name = "DBGrid";
			this.DBGrid.ReadOnly = true;
			this.DBGrid.RowChanged = false;
			this.DBGrid.RowHeadersWidth = 30;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.DBGrid.RowTemplate.Height = 40;
			this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid.Size = new System.Drawing.Size(731, 413);
			this.DBGrid.TabIndex = 63;
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator2,
            this.toolCheckAll,
            this.toolCheckNone,
            this.toolStripSeparator1,
            this.toolClear,
            this.toolClose});
			this.toolStrip1.Location = new System.Drawing.Point(4, 3);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(731, 47);
			this.toolStrip1.TabIndex = 62;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolRefresh
			// 
			this.toolRefresh.Image = global::LXMS.Properties.Resources.Refresh;
			this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolRefresh.Name = "toolRefresh";
			this.toolRefresh.Size = new System.Drawing.Size(68, 44);
			this.toolRefresh.Text = "Refresh";
			this.toolRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
			// 
			// toolCheckAll
			// 
			this.toolCheckAll.Image = global::LXMS.Properties.Resources.Browser;
			this.toolCheckAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolCheckAll.Name = "toolCheckAll";
			this.toolCheckAll.Size = new System.Drawing.Size(32, 44);
			this.toolCheckAll.Text = "All";
			this.toolCheckAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolCheckAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolCheckAll.Click += new System.EventHandler(this.toolCheckAll_Click);
			// 
			// toolCheckNone
			// 
			this.toolCheckNone.Image = global::LXMS.Properties.Resources.View;
			this.toolCheckNone.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolCheckNone.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolCheckNone.Name = "toolCheckNone";
			this.toolCheckNone.Size = new System.Drawing.Size(53, 44);
			this.toolCheckNone.Text = "None";
			this.toolCheckNone.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolCheckNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolCheckNone.Click += new System.EventHandler(this.toolCheckNone_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
			// 
			// toolClear
			// 
			this.toolClear.Image = global::LXMS.Properties.Resources.Delete;
			this.toolClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolClear.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolClear.Name = "toolClear";
			this.toolClear.Size = new System.Drawing.Size(50, 44);
			this.toolClear.Text = "Clear";
			this.toolClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolClear.Click += new System.EventHandler(this.toolClear_Click);
			// 
			// toolClose
			// 
			this.toolClose.Image = global::LXMS.Properties.Resources.Close;
			this.toolClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolClose.Name = "toolClose";
			this.toolClose.Size = new System.Drawing.Size(53, 44);
			this.toolClose.Text = "Close";
			this.toolClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolClose.Click += new System.EventHandler(this.toolClose_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.DBGrid2);
			this.tabPage2.Controls.Add(this.toolStripEx1);
			this.tabPage2.Location = new System.Drawing.Point(4, 30);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage2.Size = new System.Drawing.Size(739, 466);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Product Restore";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// DBGrid2
			// 
			this.DBGrid2.AllowUserToAddRows = false;
			this.DBGrid2.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
			this.DBGrid2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.DBGrid2.BackgroundColor = System.Drawing.Color.White;
			this.DBGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DBGrid2.DefaultCellStyle = dataGridViewCellStyle5;
			this.DBGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DBGrid2.Location = new System.Drawing.Point(4, 50);
			this.DBGrid2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DBGrid2.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid2.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid2.MergeColumnNames")));
			this.DBGrid2.MultiSelect = false;
			this.DBGrid2.Name = "DBGrid2";
			this.DBGrid2.ReadOnly = true;
			this.DBGrid2.RowChanged = false;
			this.DBGrid2.RowHeadersWidth = 30;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid2.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.DBGrid2.RowTemplate.Height = 40;
			this.DBGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid2.Size = new System.Drawing.Size(731, 413);
			this.DBGrid2.TabIndex = 64;
			// 
			// toolStripEx1
			// 
			this.toolStripEx1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh2,
            this.toolStripSeparator3,
            this.toolCheckAll2,
            this.toolCheckNone2,
            this.toolStripSeparator4,
            this.toolRestore2,
            this.toolClose2});
			this.toolStripEx1.Location = new System.Drawing.Point(4, 3);
			this.toolStripEx1.Name = "toolStripEx1";
			this.toolStripEx1.Size = new System.Drawing.Size(731, 47);
			this.toolStripEx1.TabIndex = 63;
			this.toolStripEx1.Text = "toolStripEx1";
			// 
			// toolRefresh2
			// 
			this.toolRefresh2.Image = global::LXMS.Properties.Resources.Refresh;
			this.toolRefresh2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolRefresh2.Name = "toolRefresh2";
			this.toolRefresh2.Size = new System.Drawing.Size(68, 44);
			this.toolRefresh2.Text = "Refresh";
			this.toolRefresh2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolRefresh2.Click += new System.EventHandler(this.toolRefresh2_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
			// 
			// toolCheckAll2
			// 
			this.toolCheckAll2.Image = global::LXMS.Properties.Resources.Browser;
			this.toolCheckAll2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolCheckAll2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolCheckAll2.Name = "toolCheckAll2";
			this.toolCheckAll2.Size = new System.Drawing.Size(32, 44);
			this.toolCheckAll2.Text = "All";
			this.toolCheckAll2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolCheckAll2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolCheckAll2.Click += new System.EventHandler(this.toolCheckAll2_Click);
			// 
			// toolCheckNone2
			// 
			this.toolCheckNone2.Image = global::LXMS.Properties.Resources.View;
			this.toolCheckNone2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolCheckNone2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolCheckNone2.Name = "toolCheckNone2";
			this.toolCheckNone2.Size = new System.Drawing.Size(53, 44);
			this.toolCheckNone2.Text = "None";
			this.toolCheckNone2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolCheckNone2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolCheckNone2.Click += new System.EventHandler(this.toolCheckNone2_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 47);
			// 
			// toolRestore2
			// 
			this.toolRestore2.Image = global::LXMS.Properties.Resources.Cancel;
			this.toolRestore2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolRestore2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolRestore2.Name = "toolRestore2";
			this.toolRestore2.Size = new System.Drawing.Size(70, 44);
			this.toolRestore2.Text = "Restore";
			this.toolRestore2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolRestore2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolRestore2.Click += new System.EventHandler(this.toolRestore2_Click);
			// 
			// toolClose2
			// 
			this.toolClose2.Image = global::LXMS.Properties.Resources.Close;
			this.toolClose2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolClose2.Name = "toolClose2";
			this.toolClose2.Size = new System.Drawing.Size(53, 44);
			this.toolClose2.Text = "Close";
			this.toolClose2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolClose2.Click += new System.EventHandler(this.toolClose_Click);
			// 
			// MTN_PRODUCT_CLEAR
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 500);
			this.Controls.Add(this.tabControl1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "MTN_PRODUCT_CLEAR";
			this.Text = "Product Clear";
			this.Load += new System.EventHandler(this.MTN_PRODUCT_CLEAR_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).EndInit();
			this.toolStripEx1.ResumeLayout(false);
			this.toolStripEx1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private TabControlEx tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private RowMergeView DBGrid;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolCheckAll;
        private System.Windows.Forms.ToolStripButton toolCheckNone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolClear;
        private System.Windows.Forms.ToolStripButton toolClose;
        private System.Windows.Forms.TabPage tabPage2;
        private RowMergeView DBGrid2;
        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton toolRefresh2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolCheckAll2;
        private System.Windows.Forms.ToolStripButton toolCheckNone2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolRestore2;
        private System.Windows.Forms.ToolStripButton toolClose2;

    }
}