namespace LXMS
{
    partial class ACC_BOOK
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_BOOK));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.DBGrid = new LXMS.RowMergeView();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cboBookType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gbSendDate = new System.Windows.Forms.GroupBox();
			this.dtpTo = new System.Windows.Forms.DateTimePicker();
			this.label10 = new System.Windows.Forms.Label();
			this.dtpFrom = new System.Windows.Forms.DateTimePicker();
			this.label11 = new System.Windows.Forms.Label();
			this.DBGrid2 = new LXMS.RowMergeView();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.gbSendDate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 616);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1212, 25);
			this.statusStrip1.TabIndex = 65;
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
			this.Status3.Size = new System.Drawing.Size(856, 20);
			this.Status3.Spring = true;
			this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.DBGrid);
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1212, 616);
			this.splitContainer1.SplitterDistance = 331;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 66;
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
			this.DBGrid.Location = new System.Drawing.Point(0, 39);
			this.DBGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
			this.DBGrid.MultiSelect = false;
			this.DBGrid.Name = "DBGrid";
			this.DBGrid.ReadOnly = true;
			this.DBGrid.RowChanged = false;
			this.DBGrid.RowHeadersWidth = 30;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
			this.DBGrid.RowTemplate.Height = 23;
			this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DBGrid.Size = new System.Drawing.Size(331, 577);
			this.DBGrid.TabIndex = 68;
			this.DBGrid.SelectionChanged += new System.EventHandler(this.DBGrid_SelectionChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cboBookType);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(331, 39);
			this.panel1.TabIndex = 0;
			// 
			// cboBookType
			// 
			this.cboBookType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBookType.FormattingEnabled = true;
			this.cboBookType.Location = new System.Drawing.Point(101, 8);
			this.cboBookType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboBookType.Name = "cboBookType";
			this.cboBookType.Size = new System.Drawing.Size(217, 23);
			this.cboBookType.TabIndex = 1;
			this.cboBookType.SelectedIndexChanged += new System.EventHandler(this.cboBookType_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Book Type";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gbSendDate);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.DBGrid2);
			this.splitContainer2.Size = new System.Drawing.Size(876, 616);
			this.splitContainer2.SplitterDistance = 56;
			this.splitContainer2.SplitterWidth = 5;
			this.splitContainer2.TabIndex = 0;
			// 
			// gbSendDate
			// 
			this.gbSendDate.Controls.Add(this.dtpTo);
			this.gbSendDate.Controls.Add(this.label10);
			this.gbSendDate.Controls.Add(this.dtpFrom);
			this.gbSendDate.Controls.Add(this.label11);
			this.gbSendDate.Location = new System.Drawing.Point(17, 8);
			this.gbSendDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbSendDate.Name = "gbSendDate";
			this.gbSendDate.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbSendDate.Size = new System.Drawing.Size(464, 52);
			this.gbSendDate.TabIndex = 149;
			this.gbSendDate.TabStop = false;
			this.gbSendDate.Text = "Date";
			// 
			// dtpTo
			// 
			this.dtpTo.CustomFormat = "MM/dd/yyyy";
			this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpTo.Location = new System.Drawing.Point(288, 16);
			this.dtpTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dtpTo.Name = "dtpTo";
			this.dtpTo.Size = new System.Drawing.Size(155, 25);
			this.dtpTo.TabIndex = 3;
			this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(245, 24);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(23, 15);
			this.label10.TabIndex = 2;
			this.label10.Text = "To";
			// 
			// dtpFrom
			// 
			this.dtpFrom.CustomFormat = "MM/dd/yyyy";
			this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpFrom.Location = new System.Drawing.Point(64, 16);
			this.dtpFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dtpFrom.Name = "dtpFrom";
			this.dtpFrom.Size = new System.Drawing.Size(155, 25);
			this.dtpFrom.TabIndex = 1;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(16, 22);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(39, 15);
			this.label11.TabIndex = 0;
			this.label11.Text = "From";
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
			this.DBGrid2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.DBGrid2.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid2.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid2.MergeColumnNames")));
			this.DBGrid2.MultiSelect = false;
			this.DBGrid2.Name = "DBGrid2";
			this.DBGrid2.ReadOnly = true;
			this.DBGrid2.RowChanged = false;
			this.DBGrid2.RowHeadersWidth = 30;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid2.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.DBGrid2.RowTemplate.Height = 32;
			this.DBGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid2.Size = new System.Drawing.Size(876, 555);
			this.DBGrid2.TabIndex = 69;
			this.DBGrid2.DoubleClick += new System.EventHandler(this.DBGrid2_DoubleClick);
			// 
			// ACC_BOOK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1212, 641);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "ACC_BOOK";
			this.Text = "Account Book";
			this.Load += new System.EventHandler(this.ACC_BOOK_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.gbSendDate.ResumeLayout(false);
			this.gbSendDate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RowMergeView DBGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboBookType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gbSendDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label11;
        private RowMergeView DBGrid2;

    }
}