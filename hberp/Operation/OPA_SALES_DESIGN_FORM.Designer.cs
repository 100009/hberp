﻿namespace LXMS
{
    partial class OPA_SALES_DESIGN_FORM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OPA_SALES_DESIGN_FORM));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpTo = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpFrom = new System.Windows.Forms.DateTimePicker();
			this.cboCustomer = new System.Windows.Forms.ComboBox();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.rbStatus1 = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.rbStatus0 = new System.Windows.Forms.RadioButton();
			this.DBGrid = new LXMS.RowMergeView();
			this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStrip1 = new LXMS.ToolStripEx();
			this.toolRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolImport = new System.Windows.Forms.ToolStripButton();
			this.toolNew = new System.Windows.Forms.ToolStripButton();
			this.toolEdit = new System.Windows.Forms.ToolStripButton();
			this.toolDel = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolAudit = new System.Windows.Forms.ToolStripButton();
			this.toolReset = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolExport = new System.Windows.Forms.ToolStripButton();
			this.sepDel = new System.Windows.Forms.ToolStripSeparator();
			this.toolExit = new System.Windows.Forms.ToolStripButton();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(243, 23);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 15);
			this.label1.TabIndex = 20;
			this.label1.Text = "To";
			// 
			// dtpTo
			// 
			this.dtpTo.CustomFormat = "MM/dd/yyyy";
			this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpTo.Location = new System.Drawing.Point(277, 20);
			this.dtpTo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dtpTo.Name = "dtpTo";
			this.dtpTo.Size = new System.Drawing.Size(141, 25);
			this.dtpTo.TabIndex = 19;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(31, 23);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 15);
			this.label2.TabIndex = 18;
			this.label2.Text = "From";
			// 
			// dtpFrom
			// 
			this.dtpFrom.CustomFormat = "MM/dd/yyyy";
			this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpFrom.Location = new System.Drawing.Point(79, 20);
			this.dtpFrom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dtpFrom.Name = "dtpFrom";
			this.dtpFrom.Size = new System.Drawing.Size(141, 25);
			this.dtpFrom.TabIndex = 17;
			// 
			// cboCustomer
			// 
			this.cboCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cboCustomer.FormattingEnabled = true;
			this.cboCustomer.Location = new System.Drawing.Point(383, 17);
			this.cboCustomer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cboCustomer.Name = "cboCustomer";
			this.cboCustomer.Size = new System.Drawing.Size(160, 23);
			this.cboCustomer.TabIndex = 4;
			// 
			// lblCustomer
			// 
			this.lblCustomer.AutoSize = true;
			this.lblCustomer.Location = new System.Drawing.Point(296, 21);
			this.lblCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.Size = new System.Drawing.Size(79, 15);
			this.lblCustomer.TabIndex = 3;
			this.lblCustomer.Text = "Cust Name";
			// 
			// rbStatus1
			// 
			this.rbStatus1.AutoSize = true;
			this.rbStatus1.Location = new System.Drawing.Point(148, 18);
			this.rbStatus1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.rbStatus1.Name = "rbStatus1";
			this.rbStatus1.Size = new System.Drawing.Size(84, 19);
			this.rbStatus1.TabIndex = 2;
			this.rbStatus1.Text = "Audited";
			this.rbStatus1.UseVisualStyleBackColor = true;
			this.rbStatus1.CheckedChanged += new System.EventHandler(this.rbStatus0_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.dtpTo);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.dtpFrom);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
			this.groupBox1.Location = new System.Drawing.Point(588, 0);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(441, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ship Date";
			this.groupBox1.Visible = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 47);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.cboCustomer);
			this.splitContainer1.Panel1.Controls.Add(this.lblCustomer);
			this.splitContainer1.Panel1.Controls.Add(this.rbStatus1);
			this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
			this.splitContainer1.Panel1.Controls.Add(this.rbStatus0);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
			this.splitContainer1.Size = new System.Drawing.Size(1029, 455);
			this.splitContainer1.SplitterDistance = 48;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 63;
			// 
			// rbStatus0
			// 
			this.rbStatus0.AutoSize = true;
			this.rbStatus0.Checked = true;
			this.rbStatus0.Location = new System.Drawing.Point(41, 18);
			this.rbStatus0.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.rbStatus0.Name = "rbStatus0";
			this.rbStatus0.Size = new System.Drawing.Size(84, 19);
			this.rbStatus0.TabIndex = 0;
			this.rbStatus0.TabStop = true;
			this.rbStatus0.Text = "Unaudit";
			this.rbStatus0.UseVisualStyleBackColor = true;
			this.rbStatus0.CheckedChanged += new System.EventHandler(this.rbStatus0_CheckedChanged);
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
			this.DBGrid.RowTemplate.Height = 23;
			this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid.Size = new System.Drawing.Size(1029, 402);
			this.DBGrid.TabIndex = 60;
			this.DBGrid.ButtonSelectClick += new LXMS.RowMergeView.ButtonClick(this.DBGrid_ButtonSelectClick);
			this.DBGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DBGrid_CellValueChanged);
			this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
			// 
			// Status3
			// 
			this.Status3.AutoSize = false;
			this.Status3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.Status3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
			this.Status3.Name = "Status3";
			this.Status3.Size = new System.Drawing.Size(673, 20);
			this.Status3.Spring = true;
			this.Status3.Text = "收款状况: 0-未收款  1-已收款     发票需求:  0-不开票   1-未开票  2-已开票";
			this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 502);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1029, 25);
			this.statusStrip1.TabIndex = 62;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator3,
            this.toolImport,
            this.toolNew,
            this.toolEdit,
            this.toolDel,
            this.toolStripSeparator1,
            this.toolAudit,
            this.toolReset,
            this.toolStripSeparator2,
            this.toolExport,
            this.sepDel,
            this.toolExit});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1029, 47);
			this.toolStrip1.TabIndex = 61;
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
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
			// 
			// toolImport
			// 
			this.toolImport.Image = global::LXMS.Properties.Resources.Import;
			this.toolImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolImport.Name = "toolImport";
			this.toolImport.Size = new System.Drawing.Size(63, 44);
			this.toolImport.Text = "Import";
			this.toolImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolImport.Click += new System.EventHandler(this.toolImport_Click);
			// 
			// toolNew
			// 
			this.toolNew.Image = global::LXMS.Properties.Resources.new_med1;
			this.toolNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolNew.Name = "toolNew";
			this.toolNew.Size = new System.Drawing.Size(46, 44);
			this.toolNew.Text = "New";
			this.toolNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolNew.Click += new System.EventHandler(this.toolNew_Click);
			// 
			// toolEdit
			// 
			this.toolEdit.Image = global::LXMS.Properties.Resources.Edit;
			this.toolEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.toolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolEdit.Name = "toolEdit";
			this.toolEdit.Size = new System.Drawing.Size(41, 44);
			this.toolEdit.Text = "Edit";
			this.toolEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.toolEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolEdit.Click += new System.EventHandler(this.toolEdit_Click);
			// 
			// toolDel
			// 
			this.toolDel.Image = global::LXMS.Properties.Resources.Delete;
			this.toolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolDel.Name = "toolDel";
			this.toolDel.Size = new System.Drawing.Size(37, 44);
			this.toolDel.Text = "Del";
			this.toolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
			// 
			// toolAudit
			// 
			this.toolAudit.Image = global::LXMS.Properties.Resources.Post;
			this.toolAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolAudit.Name = "toolAudit";
			this.toolAudit.Size = new System.Drawing.Size(53, 44);
			this.toolAudit.Text = "Audit";
			this.toolAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolAudit.Click += new System.EventHandler(this.toolAudit_Click);
			// 
			// toolReset
			// 
			this.toolReset.Image = global::LXMS.Properties.Resources.Inactive;
			this.toolReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolReset.Name = "toolReset";
			this.toolReset.Size = new System.Drawing.Size(54, 44);
			this.toolReset.Text = "Reset";
			this.toolReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolReset.Click += new System.EventHandler(this.toolReset_Click);
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
			// sepDel
			// 
			this.sepDel.Name = "sepDel";
			this.sepDel.Size = new System.Drawing.Size(6, 47);
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
			// OPA_SALES_DESIGN_FORM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1029, 527);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "OPA_SALES_DESIGN_FORM";
			this.Text = "Sales Design Form";
			this.Load += new System.EventHandler(this.OPA_SALES_DESIGN_FORM_Load);
			this.Shown += new System.EventHandler(this.OPA_SALES_DESIGN_FORM_Shown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private RowMergeView DBGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.RadioButton rbStatus1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton rbStatus0;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator sepDel;
        private System.Windows.Forms.ToolStripButton toolExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolReset;
        private System.Windows.Forms.ToolStripButton toolAudit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolDel;
        private System.Windows.Forms.ToolStripButton toolEdit;
        private System.Windows.Forms.ToolStripButton toolNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripButton toolExit;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolImport;
    }
}