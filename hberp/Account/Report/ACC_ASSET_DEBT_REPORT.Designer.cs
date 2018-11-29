namespace LXMS
{
    partial class ACC_ASSET_DEBT_REPORT
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_ASSET_DEBT_REPORT));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.DBGrid1 = new LXMS.RowMergeView();
			this.DBGrid2 = new LXMS.RowMergeView();
			this.toolStrip1 = new LXMS.ToolStripEx();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.cboAccName = new System.Windows.Forms.ToolStripComboBox();
			this.toolRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolExport = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolExit = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 458);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(828, 25);
			this.statusStrip1.TabIndex = 13;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// Status1
			// 
			this.Status1.AutoSize = false;
			this.Status1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.Status1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.Status1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Status1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Status1.Name = "Status1";
			this.Status1.Size = new System.Drawing.Size(300, 20);
			this.Status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Status2
			// 
			this.Status2.AutoSize = false;
			this.Status2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.Status2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.Status2.Name = "Status2";
			this.Status2.Size = new System.Drawing.Size(300, 20);
			this.Status2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Status3
			// 
			this.Status3.AutoSize = false;
			this.Status3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.Status3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.Status3.Name = "Status3";
			this.Status3.Size = new System.Drawing.Size(208, 20);
			this.Status3.Spring = true;
			this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 47);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.DBGrid1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.DBGrid2);
			this.splitContainer1.Size = new System.Drawing.Size(828, 411);
			this.splitContainer1.SplitterDistance = 398;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 14;
			// 
			// DBGrid1
			// 
			this.DBGrid1.AllowUserToAddRows = false;
			this.DBGrid1.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
			this.DBGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.DBGrid1.BackgroundColor = System.Drawing.Color.White;
			this.DBGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DBGrid1.DefaultCellStyle = dataGridViewCellStyle2;
			this.DBGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DBGrid1.Location = new System.Drawing.Point(0, 0);
			this.DBGrid1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DBGrid1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid1.MergeColumnNames")));
			this.DBGrid1.MultiSelect = false;
			this.DBGrid1.Name = "DBGrid1";
			this.DBGrid1.ReadOnly = true;
			this.DBGrid1.RowChanged = false;
			this.DBGrid1.RowHeadersWidth = 30;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid1.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.DBGrid1.RowTemplate.Height = 23;
			this.DBGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid1.Size = new System.Drawing.Size(398, 411);
			this.DBGrid1.TabIndex = 57;
			this.DBGrid1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGrid1_CellDoubleClick);
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
			this.DBGrid2.Location = new System.Drawing.Point(0, 0);
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
			this.DBGrid2.RowTemplate.Height = 23;
			this.DBGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid2.Size = new System.Drawing.Size(425, 411);
			this.DBGrid2.TabIndex = 57;
			this.DBGrid2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGrid2_CellDoubleClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboAccName,
            this.toolRefresh,
            this.toolStripSeparator2,
            this.toolExport,
            this.toolStripSeparator3,
            this.toolExit});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(828, 47);
			this.toolStrip1.TabIndex = 12;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(83, 44);
			this.toolStripLabel1.Text = "Acc Name";
			// 
			// cboAccName
			// 
			this.cboAccName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAccName.Name = "cboAccName";
			this.cboAccName.Size = new System.Drawing.Size(199, 47);
			this.cboAccName.SelectedIndexChanged += new System.EventHandler(this.cboAccName_SelectedIndexChanged);
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
			// toolExport
			// 
			this.toolExport.Image = global::LXMS.Properties.Resources.Export;
			this.toolExport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolExport.Name = "toolExport";
			this.toolExport.Size = new System.Drawing.Size(61, 44);
			this.toolExport.Text = "Export";
			this.toolExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolExport.Click += new System.EventHandler(this.toolExport_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
			// 
			// toolExit
			// 
			this.toolExit.Image = global::LXMS.Properties.Resources.Close;
			this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolExit.Name = "toolExit";
			this.toolExit.Size = new System.Drawing.Size(39, 44);
			this.toolExit.Text = "Exit";
			this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
			// 
			// ACC_ASSET_DEBT_REPORT
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(828, 483);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "ACC_ASSET_DEBT_REPORT";
			this.Text = "Account Asset Debt Report";
			this.Load += new System.EventHandler(this.ACC_ASSET_DEBT_REPORT_Load);
			this.Shown += new System.EventHandler(this.ACC_ASSET_DEBT_REPORT_Shown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboAccName;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RowMergeView DBGrid1;
        private RowMergeView DBGrid2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExport;
    }
}