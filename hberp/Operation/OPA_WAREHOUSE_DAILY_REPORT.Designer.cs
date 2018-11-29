namespace LXMS
{
	partial class OPA_WAREHOUSE_DAILY_REPORT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPA_WAREHOUSE_DAILY_REPORT));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRegen = new System.Windows.Forms.Button();
            this.btnGen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkChangedOnly = new System.Windows.Forms.CheckBox();
            this.dtpReportDate = new System.Windows.Forms.DateTimePicker();
            this.DBGrid = new LXMS.RowMergeView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(789, 25);
            this.statusStrip1.TabIndex = 69;
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
            this.Status1.Size = new System.Drawing.Size(168, 20);
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
            this.Status2.Size = new System.Drawing.Size(168, 20);
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
            this.Status3.Size = new System.Drawing.Size(438, 20);
            this.Status3.Spring = true;
            this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnExport);
            this.splitContainer1.Panel1.Controls.Add(this.btnRegen);
            this.splitContainer1.Panel1.Controls.Add(this.btnGen);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
            this.splitContainer1.Size = new System.Drawing.Size(789, 468);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 70;
            // 
            // btnExport
            // 
            this.btnExport.Image = global::LXMS.Properties.Resources.Export;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(518, 10);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 26);
            this.btnExport.TabIndex = 4;
            this.btnExport.Text = "导出报表";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRegen
            // 
            this.btnRegen.Image = global::LXMS.Properties.Resources.Inactive;
            this.btnRegen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegen.Location = new System.Drawing.Point(339, 10);
            this.btnRegen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegen.Name = "btnRegen";
            this.btnRegen.Size = new System.Drawing.Size(85, 26);
            this.btnRegen.TabIndex = 3;
            this.btnRegen.Text = "重新生成";
            this.btnRegen.UseVisualStyleBackColor = true;
            this.btnRegen.Click += new System.EventHandler(this.btnRegen_Click);
            // 
            // btnGen
            // 
            this.btnGen.Image = global::LXMS.Properties.Resources.Connect;
            this.btnGen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGen.Location = new System.Drawing.Point(260, 10);
            this.btnGen.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(66, 26);
            this.btnGen.TabIndex = 2;
            this.btnGen.Text = "生成";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkChangedOnly);
            this.groupBox1.Controls.Add(this.dtpReportDate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 55);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报告日期";
            // 
            // chkChangedOnly
            // 
            this.chkChangedOnly.AutoSize = true;
            this.chkChangedOnly.Checked = true;
            this.chkChangedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkChangedOnly.Location = new System.Drawing.Point(131, 16);
            this.chkChangedOnly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkChangedOnly.Name = "chkChangedOnly";
            this.chkChangedOnly.Size = new System.Drawing.Size(84, 16);
            this.chkChangedOnly.TabIndex = 18;
            this.chkChangedOnly.Text = "仅有变化的";
            this.chkChangedOnly.UseVisualStyleBackColor = true;
            this.chkChangedOnly.CheckedChanged += new System.EventHandler(this.chkChangedOnly_CheckedChanged);
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.CustomFormat = "MM/dd/yyyy";
            this.dtpReportDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReportDate.Location = new System.Drawing.Point(13, 15);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new System.Drawing.Size(107, 21);
            this.dtpReportDate.TabIndex = 17;
            this.dtpReportDate.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // DBGrid
            // 
            this.DBGrid.AllowUserToAddRows = false;
            this.DBGrid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.DBGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DBGrid.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 0);
            this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
            this.DBGrid.MultiSelect = false;
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.ReadOnly = true;
            this.DBGrid.RowChanged = false;
            this.DBGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DBGrid.RowTemplate.Height = 23;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(789, 409);
            this.DBGrid.TabIndex = 61;
            // 
            // OPA_WAREHOUSE_DAILY_REPORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 493);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "OPA_WAREHOUSE_DAILY_REPORT";
            this.Text = "Warehouse Daily Report";
            this.Load += new System.EventHandler(this.OPA_WAREHOUSE_DAILY_REPORT_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel Status1;
		private System.Windows.Forms.ToolStripStatusLabel Status2;
		private System.Windows.Forms.ToolStripStatusLabel Status3;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DateTimePicker dtpReportDate;
		private RowMergeView DBGrid;
		private System.Windows.Forms.Button btnRegen;
		private System.Windows.Forms.Button btnGen;
		private System.Windows.Forms.CheckBox chkChangedOnly;
		private System.Windows.Forms.Button btnExport;
	}
}