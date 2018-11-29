namespace LXMS
{
    partial class ACC_CHECK_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_CHECK_LIST));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.toolStrip1 = new LXMS.ToolStripEx();
			this.toolRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolExit = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.cboBank = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboStatus = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cboSubject = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboCheckType = new System.Windows.Forms.ComboBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.txtCheckNo = new System.Windows.Forms.TextBox();
			this.DBGrid = new LXMS.RowMergeView();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator2,
            this.toolExit});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1128, 47);
			this.toolStrip1.TabIndex = 63;
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
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 523);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1128, 25);
			this.statusStrip1.TabIndex = 64;
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
			this.Status3.Size = new System.Drawing.Size(772, 20);
			this.Status3.Spring = true;
			this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 47);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cboBank);
			this.splitContainer1.Panel1.Controls.Add(this.label3);
			this.splitContainer1.Panel1.Controls.Add(this.cboStatus);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.cboSubject);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.cboCheckType);
			this.splitContainer1.Panel1.Controls.Add(this.label16);
			this.splitContainer1.Panel1.Controls.Add(this.label29);
			this.splitContainer1.Panel1.Controls.Add(this.txtCheckNo);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
			this.splitContainer1.Size = new System.Drawing.Size(1128, 476);
			this.splitContainer1.SplitterDistance = 81;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 67;
			// 
			// cboBank
			// 
			this.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBank.FormattingEnabled = true;
			this.cboBank.Location = new System.Drawing.Point(111, 48);
			this.cboBank.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboBank.Name = "cboBank";
			this.cboBank.Size = new System.Drawing.Size(153, 23);
			this.cboBank.TabIndex = 281;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 51);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 15);
			this.label3.TabIndex = 280;
			this.label3.Text = "Bank Name";
			// 
			// cboStatus
			// 
			this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStatus.FormattingEnabled = true;
			this.cboStatus.Location = new System.Drawing.Point(111, 15);
			this.cboStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboStatus.Name = "cboStatus";
			this.cboStatus.Size = new System.Drawing.Size(153, 23);
			this.cboStatus.TabIndex = 279;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 19);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 15);
			this.label2.TabIndex = 278;
			this.label2.Text = "Status";
			// 
			// cboSubject
			// 
			this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSubject.FormattingEnabled = true;
			this.cboSubject.Location = new System.Drawing.Point(376, 15);
			this.cboSubject.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboSubject.Name = "cboSubject";
			this.cboSubject.Size = new System.Drawing.Size(153, 23);
			this.cboSubject.TabIndex = 277;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(281, 19);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 15);
			this.label1.TabIndex = 276;
			this.label1.Text = "Subject Id";
			// 
			// cboCheckType
			// 
			this.cboCheckType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCheckType.FormattingEnabled = true;
			this.cboCheckType.Location = new System.Drawing.Point(651, 15);
			this.cboCheckType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboCheckType.Name = "cboCheckType";
			this.cboCheckType.Size = new System.Drawing.Size(153, 23);
			this.cboCheckType.TabIndex = 275;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(556, 19);
			this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(87, 15);
			this.label16.TabIndex = 274;
			this.label16.Text = "Check Type";
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(281, 51);
			this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(71, 15);
			this.label29.TabIndex = 272;
			this.label29.Text = "Check No";
			// 
			// txtCheckNo
			// 
			this.txtCheckNo.Location = new System.Drawing.Point(376, 48);
			this.txtCheckNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtCheckNo.Name = "txtCheckNo";
			this.txtCheckNo.Size = new System.Drawing.Size(153, 25);
			this.txtCheckNo.TabIndex = 273;
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
			this.DBGrid.Location = new System.Drawing.Point(0, 0);
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
			this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid.Size = new System.Drawing.Size(1128, 390);
			this.DBGrid.TabIndex = 67;
			this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
			// 
			// ACC_CHECK_LIST
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1128, 548);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "ACC_CHECK_LIST";
			this.Text = "Account Check List";
			this.Load += new System.EventHandler(this.ACC_CHECK_LIST_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RowMergeView DBGrid;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtCheckNo;
        private System.Windows.Forms.ComboBox cboCheckType;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboSubject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.Label label3;
    }
}