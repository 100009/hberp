namespace LXMS
{
    partial class QRY_SALES_SHIPMENT_SUMMARY
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRY_SALES_SHIPMENT_SUMMARY));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DBGrid = new LXMS.RowMergeView();
            this.cboShipType = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkInvoice2 = new System.Windows.Forms.CheckBox();
            this.chkInvoice1 = new System.Windows.Forms.CheckBox();
            this.chkInvoice0 = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkPay1 = new System.Windows.Forms.CheckBox();
            this.chkPay0 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkAudited = new System.Windows.Forms.CheckBox();
            this.chkNotAudit = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstCustomer = new LXMS.LxListBox(this.components);
            this.gbSendDate = new System.Windows.Forms.GroupBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cboSalesMan = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbSendDate.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.DBGrid.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid.MergeColumnNames")));
            this.DBGrid.MultiSelect = false;
            this.DBGrid.Name = "DBGrid";
            this.DBGrid.ReadOnly = true;
            this.DBGrid.RowChanged = false;
            this.DBGrid.RowHeadersWidth = 30;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DBGrid.RowTemplate.Height = 23;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(852, 214);
            this.DBGrid.TabIndex = 5;
            this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
            // 
            // cboShipType
            // 
            this.cboShipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShipType.FormattingEnabled = true;
            this.cboShipType.Location = new System.Drawing.Point(518, 10);
            this.cboShipType.Name = "cboShipType";
            this.cboShipType.Size = new System.Drawing.Size(101, 20);
            this.cboShipType.TabIndex = 145;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboSalesMan);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.gbSendDate);
            this.splitContainer1.Panel1.Controls.Add(this.cboShipType);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
            this.splitContainer1.Size = new System.Drawing.Size(852, 325);
            this.splitContainer1.SplitterDistance = 107;
            this.splitContainer1.TabIndex = 45;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkInvoice2);
            this.groupBox6.Controls.Add(this.chkInvoice1);
            this.groupBox6.Controls.Add(this.chkInvoice0);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(341, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(106, 107);
            this.groupBox6.TabIndex = 168;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Invoice Status";
            // 
            // chkInvoice2
            // 
            this.chkInvoice2.AutoSize = true;
            this.chkInvoice2.Checked = true;
            this.chkInvoice2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvoice2.Location = new System.Drawing.Point(16, 68);
            this.chkInvoice2.Name = "chkInvoice2";
            this.chkInvoice2.Size = new System.Drawing.Size(60, 16);
            this.chkInvoice2.TabIndex = 2;
            this.chkInvoice2.Text = "已开票";
            this.chkInvoice2.UseVisualStyleBackColor = true;
            // 
            // chkInvoice1
            // 
            this.chkInvoice1.AutoSize = true;
            this.chkInvoice1.Checked = true;
            this.chkInvoice1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvoice1.Location = new System.Drawing.Point(16, 51);
            this.chkInvoice1.Name = "chkInvoice1";
            this.chkInvoice1.Size = new System.Drawing.Size(60, 16);
            this.chkInvoice1.TabIndex = 1;
            this.chkInvoice1.Text = "未开票";
            this.chkInvoice1.UseVisualStyleBackColor = true;
            // 
            // chkInvoice0
            // 
            this.chkInvoice0.AutoSize = true;
            this.chkInvoice0.Checked = true;
            this.chkInvoice0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvoice0.Location = new System.Drawing.Point(16, 33);
            this.chkInvoice0.Name = "chkInvoice0";
            this.chkInvoice0.Size = new System.Drawing.Size(60, 16);
            this.chkInvoice0.TabIndex = 0;
            this.chkInvoice0.Text = "不开票";
            this.chkInvoice0.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkPay1);
            this.groupBox5.Controls.Add(this.chkPay0);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox5.Location = new System.Drawing.Point(235, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(106, 107);
            this.groupBox5.TabIndex = 167;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Receive Status";
            // 
            // chkPay1
            // 
            this.chkPay1.AutoSize = true;
            this.chkPay1.Checked = true;
            this.chkPay1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPay1.Location = new System.Drawing.Point(15, 68);
            this.chkPay1.Name = "chkPay1";
            this.chkPay1.Size = new System.Drawing.Size(60, 16);
            this.chkPay1.TabIndex = 1;
            this.chkPay1.Text = "已收款";
            this.chkPay1.UseVisualStyleBackColor = true;
            // 
            // chkPay0
            // 
            this.chkPay0.AutoSize = true;
            this.chkPay0.Checked = true;
            this.chkPay0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPay0.Location = new System.Drawing.Point(15, 33);
            this.chkPay0.Name = "chkPay0";
            this.chkPay0.Size = new System.Drawing.Size(60, 16);
            this.chkPay0.TabIndex = 0;
            this.chkPay0.Text = "未收款";
            this.chkPay0.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkAudited);
            this.groupBox3.Controls.Add(this.chkNotAudit);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(136, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(99, 107);
            this.groupBox3.TabIndex = 166;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // chkAudited
            // 
            this.chkAudited.AutoSize = true;
            this.chkAudited.Checked = true;
            this.chkAudited.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAudited.Location = new System.Drawing.Point(15, 64);
            this.chkAudited.Name = "chkAudited";
            this.chkAudited.Size = new System.Drawing.Size(66, 16);
            this.chkAudited.TabIndex = 2;
            this.chkAudited.Text = "Audited";
            this.chkAudited.UseVisualStyleBackColor = true;
            // 
            // chkNotAudit
            // 
            this.chkNotAudit.AutoSize = true;
            this.chkNotAudit.Checked = true;
            this.chkNotAudit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotAudit.Location = new System.Drawing.Point(15, 33);
            this.chkNotAudit.Name = "chkNotAudit";
            this.chkNotAudit.Size = new System.Drawing.Size(78, 16);
            this.chkNotAudit.TabIndex = 0;
            this.chkNotAudit.Text = "Not Audit";
            this.chkNotAudit.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstCustomer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 107);
            this.groupBox1.TabIndex = 151;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer List";
            // 
            // lstCustomer
            // 
            this.lstCustomer.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.lstCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCustomer.FormattingEnabled = true;
            this.lstCustomer.ItemHeight = 12;
            this.lstCustomer.Location = new System.Drawing.Point(3, 17);
            this.lstCustomer.Name = "lstCustomer";
            this.lstCustomer.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCustomer.Size = new System.Drawing.Size(130, 87);
            this.lstCustomer.TabIndex = 147;
            // 
            // gbSendDate
            // 
            this.gbSendDate.Controls.Add(this.dtpTo);
            this.gbSendDate.Controls.Add(this.label10);
            this.gbSendDate.Controls.Add(this.dtpFrom);
            this.gbSendDate.Controls.Add(this.label11);
            this.gbSendDate.Location = new System.Drawing.Point(625, 3);
            this.gbSendDate.Name = "gbSendDate";
            this.gbSendDate.Size = new System.Drawing.Size(174, 61);
            this.gbSendDate.TabIndex = 148;
            this.gbSendDate.TabStop = false;
            this.gbSendDate.Text = "Ship Date";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM/dd/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(48, 37);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowCheckBox = true;
            this.dtpTo.ShowUpDown = true;
            this.dtpTo.Size = new System.Drawing.Size(117, 21);
            this.dtpTo.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM/dd/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(48, 13);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowCheckBox = true;
            this.dtpFrom.ShowUpDown = true;
            this.dtpFrom.Size = new System.Drawing.Size(117, 21);
            this.dtpFrom.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "From";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(453, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 144;
            this.label12.Text = "Ship Type";
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.AutoSize = false;
            this.StatusLabel4.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.StatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(405, 17);
            this.StatusLabel4.Spring = true;
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.AutoSize = false;
            this.StatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.StatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(131, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator1,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(852, 40);
            this.toolStrip1.TabIndex = 43;
            this.toolStrip1.Text = "toolStrip1";
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
            // StatusLabel2
            // 
            this.StatusLabel2.AutoSize = false;
            this.StatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(1, 17);
            this.StatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2,
            this.StatusLabel3,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 365);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 44;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = false;
            this.StatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.StatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(300, 17);
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSalesMan
            // 
            this.cboSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSalesMan.FormattingEnabled = true;
            this.cboSalesMan.Location = new System.Drawing.Point(518, 36);
            this.cboSalesMan.Name = "cboSalesMan";
            this.cboSalesMan.Size = new System.Drawing.Size(101, 20);
            this.cboSalesMan.TabIndex = 170;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(453, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 169;
            this.label1.Text = "Sales Man";
            // 
            // QRY_SALES_SHIPMENT_SUMMARY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 387);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "QRY_SALES_SHIPMENT_SUMMARY";
            this.Text = "Sales Shipment Summary";
            this.Load += new System.EventHandler(this.QRY_SALES_SHIPMENT_SUMMARY_Load);
            this.Shown += new System.EventHandler(this.QRY_SALES_SHIPMENT_SUMMARY_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.gbSendDate.ResumeLayout(false);
            this.gbSendDate.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RowMergeView DBGrid;
        private System.Windows.Forms.ComboBox cboShipType;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.GroupBox gbSendDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private LxListBox lstCustomer;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkInvoice2;
        private System.Windows.Forms.CheckBox chkInvoice1;
        private System.Windows.Forms.CheckBox chkInvoice0;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkPay1;
        private System.Windows.Forms.CheckBox chkPay0;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkAudited;
        private System.Windows.Forms.CheckBox chkNotAudit;
        private System.Windows.Forms.ComboBox cboSalesMan;
        private System.Windows.Forms.Label label1;
    }
}