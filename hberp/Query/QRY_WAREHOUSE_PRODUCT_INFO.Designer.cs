namespace LXMS
{
    partial class QRY_WAREHOUSE_PRODUCT_INFO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRY_WAREHOUSE_PRODUCT_INFO));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.miniToolStrip = new LXMS.ToolStripEx();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboAccName = new System.Windows.Forms.ToolStripComboBox();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbWarehouseProductSizeWip = new System.Windows.Forms.RadioButton();
            this.rbWarehouseProductWip = new System.Windows.Forms.RadioButton();
            this.rbProductSizeWip = new System.Windows.Forms.RadioButton();
            this.rbProductWip = new System.Windows.Forms.RadioButton();
            this.DBGrid = new LXMS.RowMergeView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(322, 8);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(660, 40);
            this.miniToolStrip.TabIndex = 60;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboAccName,
            this.toolRefresh,
            this.toolStripSeparator1,
            this.txtFind,
            this.toolFind,
            this.toolStripSeparator2,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 38);
            this.toolStrip1.TabIndex = 66;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 35);
            this.toolStripLabel1.Text = "Acc Name";
            // 
            // cboAccName
            // 
            this.cboAccName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccName.Name = "cboAccName";
            this.cboAccName.Size = new System.Drawing.Size(150, 38);
            this.cboAccName.SelectedIndexChanged += new System.EventHandler(this.cboAccName_SelectedIndexChanged);
            // 
            // toolRefresh
            // 
            this.toolRefresh.Image = global::LXMS.Properties.Resources.Refresh;
            this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(50, 35);
            this.toolRefresh.Text = "Refresh";
            this.toolRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // txtFind
            // 
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(200, 38);
            // 
            // toolFind
            // 
            this.toolFind.Image = ((System.Drawing.Image)(resources.GetObject("toolFind.Image")));
            this.toolFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFind.Name = "toolFind";
            this.toolFind.Size = new System.Drawing.Size(34, 35);
            this.toolFind.Text = "Find";
            this.toolFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolFind.Click += new System.EventHandler(this.toolFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolExit
            // 
            this.toolExit.Image = global::LXMS.Properties.Resources.Close;
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(29, 35);
            this.toolExit.Text = "Exit";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 38);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
            this.splitContainer1.Size = new System.Drawing.Size(770, 455);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 67;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbWarehouseProductSizeWip);
            this.groupBox1.Controls.Add(this.rbWarehouseProductWip);
            this.groupBox1.Controls.Add(this.rbProductSizeWip);
            this.groupBox1.Controls.Add(this.rbProductWip);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(770, 47);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // rbWarehouseProductSizeWip
            // 
            this.rbWarehouseProductSizeWip.AutoSize = true;
            this.rbWarehouseProductSizeWip.Location = new System.Drawing.Point(456, 20);
            this.rbWarehouseProductSizeWip.Name = "rbWarehouseProductSizeWip";
            this.rbWarehouseProductSizeWip.Size = new System.Drawing.Size(165, 17);
            this.rbWarehouseProductSizeWip.TabIndex = 3;
            this.rbWarehouseProductSizeWip.TabStop = true;
            this.rbWarehouseProductSizeWip.Text = "Warehouse Product Size Wip";
            this.rbWarehouseProductSizeWip.UseVisualStyleBackColor = true;
            this.rbWarehouseProductSizeWip.CheckedChanged += new System.EventHandler(this.rbProductWip_CheckedChanged);
            // 
            // rbWarehouseProductWip
            // 
            this.rbWarehouseProductWip.AutoSize = true;
            this.rbWarehouseProductWip.Location = new System.Drawing.Point(281, 20);
            this.rbWarehouseProductWip.Name = "rbWarehouseProductWip";
            this.rbWarehouseProductWip.Size = new System.Drawing.Size(142, 17);
            this.rbWarehouseProductWip.TabIndex = 2;
            this.rbWarehouseProductWip.TabStop = true;
            this.rbWarehouseProductWip.Text = "Warehouse Product Wip";
            this.rbWarehouseProductWip.UseVisualStyleBackColor = true;
            this.rbWarehouseProductWip.CheckedChanged += new System.EventHandler(this.rbProductWip_CheckedChanged);
            // 
            // rbProductSizeWip
            // 
            this.rbProductSizeWip.AutoSize = true;
            this.rbProductSizeWip.Location = new System.Drawing.Point(140, 20);
            this.rbProductSizeWip.Name = "rbProductSizeWip";
            this.rbProductSizeWip.Size = new System.Drawing.Size(107, 17);
            this.rbProductSizeWip.TabIndex = 1;
            this.rbProductSizeWip.TabStop = true;
            this.rbProductSizeWip.Text = "Product Size Wip";
            this.rbProductSizeWip.UseVisualStyleBackColor = true;
            this.rbProductSizeWip.CheckedChanged += new System.EventHandler(this.rbProductWip_CheckedChanged);
            // 
            // rbProductWip
            // 
            this.rbProductWip.AutoSize = true;
            this.rbProductWip.Location = new System.Drawing.Point(22, 20);
            this.rbProductWip.Name = "rbProductWip";
            this.rbProductWip.Size = new System.Drawing.Size(84, 17);
            this.rbProductWip.TabIndex = 0;
            this.rbProductWip.TabStop = true;
            this.rbProductWip.Text = "Product Wip";
            this.rbProductWip.UseVisualStyleBackColor = true;
            this.rbProductWip.CheckedChanged += new System.EventHandler(this.rbProductWip_CheckedChanged);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DBGrid.RowTemplate.Height = 40;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(770, 404);
            this.DBGrid.TabIndex = 60;
            this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // QRY_WAREHOUSE_PRODUCT_INFO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 493);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QRY_WAREHOUSE_PRODUCT_INFO";
            this.Text = "Warehouse Product Info";
            this.Load += new System.EventHandler(this.QRY_WAREHOUSE_PRODUCT_INFO_Load);
            this.Shown += new System.EventHandler(this.QRY_WAREHOUSE_PRODUCT_INFO_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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

        private LXMS.ToolStripEx miniToolStrip;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboAccName;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbWarehouseProductSizeWip;
        private System.Windows.Forms.RadioButton rbWarehouseProductWip;
        private System.Windows.Forms.RadioButton rbProductSizeWip;
        private System.Windows.Forms.RadioButton rbProductWip;
        private RowMergeView DBGrid;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton toolFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

    }
}