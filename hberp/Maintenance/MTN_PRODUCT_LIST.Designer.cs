namespace LXMS
{
    partial class MTN_PRODUCT_LIST
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MTN_PRODUCT_LIST));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tvLeft = new System.Windows.Forms.TreeView();
			this.DBGrid = new LXMS.RowMergeView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.DBGridPrice = new LXMS.RowMergeView();
			this.toolStripEx1 = new LXMS.ToolStripEx();
			this.toolSavePrice = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtBarcode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnBrand = new System.Windows.Forms.Button();
			this.txtBrand = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtMaxQty = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.txtMinQty = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.cboUnitNo = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.txtProductName = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.lblSizeStar = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.txtRemark = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.lblSize = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtSpecify = new System.Windows.Forms.TextBox();
			this.lblSpecify = new System.Windows.Forms.Label();
			this.cboSizeFlag = new System.Windows.Forms.ComboBox();
			this.txtProductId = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGridPrice)).BeginInit();
			this.toolStripEx1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 47);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(1095, 541);
			this.splitContainer1.SplitterDistance = 770;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 16;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tvLeft);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.DBGrid);
			this.splitContainer2.Size = new System.Drawing.Size(770, 541);
			this.splitContainer2.SplitterDistance = 215;
			this.splitContainer2.TabIndex = 1;
			// 
			// tvLeft
			// 
			this.tvLeft.AllowDrop = true;
			this.tvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvLeft.Location = new System.Drawing.Point(0, 0);
			this.tvLeft.Margin = new System.Windows.Forms.Padding(4);
			this.tvLeft.Name = "tvLeft";
			this.tvLeft.Size = new System.Drawing.Size(215, 541);
			this.tvLeft.TabIndex = 2;
			this.tvLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLeft_AfterSelect);
			this.tvLeft.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvLeft_DragDrop);
			this.tvLeft.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvLeft_DragEnter);
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
			this.DBGrid.Margin = new System.Windows.Forms.Padding(4);
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
			this.DBGrid.Size = new System.Drawing.Size(551, 541);
			this.DBGrid.TabIndex = 1;
			this.DBGrid.SelectionChanged += new System.EventHandler(this.DBGrid_SelectionChanged);
			this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
			this.DBGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DBGrid_MouseDown);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.DBGridPrice);
			this.panel2.Controls.Add(this.toolStripEx1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 378);
			this.panel2.Margin = new System.Windows.Forms.Padding(4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(320, 163);
			this.panel2.TabIndex = 265;
			// 
			// DBGridPrice
			// 
			this.DBGridPrice.AllowUserToAddRows = false;
			this.DBGridPrice.AllowUserToDeleteRows = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
			this.DBGridPrice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
			this.DBGridPrice.BackgroundColor = System.Drawing.Color.White;
			this.DBGridPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DBGridPrice.DefaultCellStyle = dataGridViewCellStyle5;
			this.DBGridPrice.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DBGridPrice.Location = new System.Drawing.Point(0, 47);
			this.DBGridPrice.Margin = new System.Windows.Forms.Padding(4);
			this.DBGridPrice.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGridPrice.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGridPrice.MergeColumnNames")));
			this.DBGridPrice.MultiSelect = false;
			this.DBGridPrice.Name = "DBGridPrice";
			this.DBGridPrice.ReadOnly = true;
			this.DBGridPrice.RowChanged = false;
			this.DBGridPrice.RowHeadersWidth = 30;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGridPrice.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.DBGridPrice.RowTemplate.Height = 23;
			this.DBGridPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGridPrice.Size = new System.Drawing.Size(320, 116);
			this.DBGridPrice.TabIndex = 12;
			// 
			// toolStripEx1
			// 
			this.toolStripEx1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSavePrice,
            this.toolStripLabel1});
			this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
			this.toolStripEx1.Name = "toolStripEx1";
			this.toolStripEx1.Size = new System.Drawing.Size(320, 47);
			this.toolStripEx1.TabIndex = 11;
			this.toolStripEx1.Text = "toolStripEx1";
			// 
			// toolSavePrice
			// 
			this.toolSavePrice.Image = global::LXMS.Properties.Resources.Save;
			this.toolSavePrice.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolSavePrice.Name = "toolSavePrice";
			this.toolSavePrice.Size = new System.Drawing.Size(47, 44);
			this.toolSavePrice.Text = "Save";
			this.toolSavePrice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.toolSavePrice.Click += new System.EventHandler(this.toolSavePrice_Click);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(141, 44);
			this.toolStripLabel1.Text = "Product Sale Price";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtBarcode);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.btnBrand);
			this.panel1.Controls.Add(this.txtBrand);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.txtMaxQty);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.txtMinQty);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.cboUnitNo);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.txtProductName);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.lblSizeStar);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.txtRemark);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.lblSize);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.txtSpecify);
			this.panel1.Controls.Add(this.lblSpecify);
			this.panel1.Controls.Add(this.cboSizeFlag);
			this.panel1.Controls.Add(this.txtProductId);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 378);
			this.panel1.TabIndex = 264;
			// 
			// txtBarcode
			// 
			this.txtBarcode.Location = new System.Drawing.Point(121, 336);
			this.txtBarcode.Margin = new System.Windows.Forms.Padding(4);
			this.txtBarcode.Name = "txtBarcode";
			this.txtBarcode.Size = new System.Drawing.Size(248, 25);
			this.txtBarcode.TabIndex = 290;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 340);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 15);
			this.label4.TabIndex = 291;
			this.label4.Text = "Barcode";
			// 
			// btnBrand
			// 
			this.btnBrand.Location = new System.Drawing.Point(379, 75);
			this.btnBrand.Margin = new System.Windows.Forms.Padding(4);
			this.btnBrand.Name = "btnBrand";
			this.btnBrand.Size = new System.Drawing.Size(28, 26);
			this.btnBrand.TabIndex = 289;
			this.btnBrand.Text = "..";
			this.btnBrand.UseVisualStyleBackColor = true;
			this.btnBrand.Click += new System.EventHandler(this.btnBrand_Click);
			// 
			// txtBrand
			// 
			this.txtBrand.Location = new System.Drawing.Point(121, 79);
			this.txtBrand.Margin = new System.Windows.Forms.Padding(4);
			this.txtBrand.Name = "txtBrand";
			this.txtBrand.Size = new System.Drawing.Size(248, 25);
			this.txtBrand.TabIndex = 287;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(13, 81);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(47, 15);
			this.label15.TabIndex = 288;
			this.label15.Text = "Brand";
			// 
			// txtMaxQty
			// 
			this.txtMaxQty.Location = new System.Drawing.Point(121, 269);
			this.txtMaxQty.Margin = new System.Windows.Forms.Padding(4);
			this.txtMaxQty.Name = "txtMaxQty";
			this.txtMaxQty.Size = new System.Drawing.Size(248, 25);
			this.txtMaxQty.TabIndex = 285;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(13, 274);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(63, 15);
			this.label14.TabIndex = 286;
			this.label14.Text = "Max Qty";
			// 
			// txtMinQty
			// 
			this.txtMinQty.Location = new System.Drawing.Point(121, 235);
			this.txtMinQty.Margin = new System.Windows.Forms.Padding(4);
			this.txtMinQty.Name = "txtMinQty";
			this.txtMinQty.Size = new System.Drawing.Size(248, 25);
			this.txtMinQty.TabIndex = 283;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(13, 240);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(63, 15);
			this.label13.TabIndex = 284;
			this.label13.Text = "Min Qty";
			// 
			// cboUnitNo
			// 
			this.cboUnitNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUnitNo.FormattingEnabled = true;
			this.cboUnitNo.Items.AddRange(new object[] {
            "True",
            "False"});
			this.cboUnitNo.Location = new System.Drawing.Point(121, 139);
			this.cboUnitNo.Margin = new System.Windows.Forms.Padding(4);
			this.cboUnitNo.Name = "cboUnitNo";
			this.cboUnitNo.Size = new System.Drawing.Size(160, 23);
			this.cboUnitNo.TabIndex = 268;
			this.cboUnitNo.SelectedIndexChanged += new System.EventHandler(this.cboUnitNo_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(291, 142);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 24);
			this.label5.TabIndex = 282;
			this.label5.Text = "*";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.Red;
			this.label9.Location = new System.Drawing.Point(379, 48);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(17, 24);
			this.label9.TabIndex = 281;
			this.label9.Text = "*";
			// 
			// txtProductName
			// 
			this.txtProductName.Location = new System.Drawing.Point(121, 45);
			this.txtProductName.Margin = new System.Windows.Forms.Padding(4);
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.Size = new System.Drawing.Size(248, 25);
			this.txtProductName.TabIndex = 265;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(13, 51);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(103, 15);
			this.label11.TabIndex = 280;
			this.label11.Text = "Product Name";
			// 
			// lblSizeStar
			// 
			this.lblSizeStar.AutoSize = true;
			this.lblSizeStar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSizeStar.ForeColor = System.Drawing.Color.Red;
			this.lblSizeStar.Location = new System.Drawing.Point(291, 175);
			this.lblSizeStar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSizeStar.Name = "lblSizeStar";
			this.lblSizeStar.Size = new System.Drawing.Size(24, 24);
			this.lblSizeStar.TabIndex = 277;
			this.lblSizeStar.Text = "**";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Red;
			this.label12.Location = new System.Drawing.Point(379, 18);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(17, 24);
			this.label12.TabIndex = 276;
			this.label12.Text = "*";
			// 
			// txtRemark
			// 
			this.txtRemark.Location = new System.Drawing.Point(121, 201);
			this.txtRemark.Margin = new System.Windows.Forms.Padding(4);
			this.txtRemark.Name = "txtRemark";
			this.txtRemark.Size = new System.Drawing.Size(248, 25);
			this.txtRemark.TabIndex = 270;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(13, 206);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(55, 15);
			this.label10.TabIndex = 275;
			this.label10.Text = "Remark";
			// 
			// lblSize
			// 
			this.lblSize.AutoSize = true;
			this.lblSize.Location = new System.Drawing.Point(13, 175);
			this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(79, 15);
			this.lblSize.TabIndex = 274;
			this.lblSize.Text = "Size Flag";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 145);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 15);
			this.label6.TabIndex = 273;
			this.label6.Text = "Unit No";
			// 
			// txtSpecify
			// 
			this.txtSpecify.Location = new System.Drawing.Point(121, 302);
			this.txtSpecify.Margin = new System.Windows.Forms.Padding(4);
			this.txtSpecify.Name = "txtSpecify";
			this.txtSpecify.Size = new System.Drawing.Size(248, 25);
			this.txtSpecify.TabIndex = 266;
			// 
			// lblSpecify
			// 
			this.lblSpecify.AutoSize = true;
			this.lblSpecify.Location = new System.Drawing.Point(13, 306);
			this.lblSpecify.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSpecify.Name = "lblSpecify";
			this.lblSpecify.Size = new System.Drawing.Size(63, 15);
			this.lblSpecify.TabIndex = 272;
			this.lblSpecify.Text = "Specify";
			// 
			// cboSizeFlag
			// 
			this.cboSizeFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSizeFlag.FormattingEnabled = true;
			this.cboSizeFlag.Items.AddRange(new object[] {
            "是",
            "否"});
			this.cboSizeFlag.Location = new System.Drawing.Point(121, 170);
			this.cboSizeFlag.Margin = new System.Windows.Forms.Padding(4);
			this.cboSizeFlag.Name = "cboSizeFlag";
			this.cboSizeFlag.Size = new System.Drawing.Size(160, 23);
			this.cboSizeFlag.TabIndex = 269;
			// 
			// txtProductId
			// 
			this.txtProductId.Location = new System.Drawing.Point(121, 15);
			this.txtProductId.Margin = new System.Windows.Forms.Padding(4);
			this.txtProductId.Name = "txtProductId";
			this.txtProductId.Size = new System.Drawing.Size(248, 25);
			this.txtProductId.TabIndex = 264;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 21);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 15);
			this.label1.TabIndex = 271;
			this.label1.Text = "Product Id";
			// 
			// MTN_PRODUCT_LIST
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelVisible = true;
			this.ClientSize = new System.Drawing.Size(1095, 616);
			this.Controls.Add(this.splitContainer1);
			this.DelVisible = true;
			this.EditVisible = true;
			this.InactiveVisible = true;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "MTN_PRODUCT_LIST";
			this.NewVisible = true;
			this.SaveVisible = true;
			this.Text = "Product List";
			this.Load += new System.EventHandler(this.MTN_PRODUCT_LIST_Load);
			this.Controls.SetChildIndex(this.splitContainer1, 0);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGridPrice)).EndInit();
			this.toolStripEx1.ResumeLayout(false);
			this.toolStripEx1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMaxQty;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtMinQty;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboUnitNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSizeStar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSpecify;
        private System.Windows.Forms.Label lblSpecify;
        private System.Windows.Forms.ComboBox cboSizeFlag;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton toolSavePrice;
        private RowMergeView DBGridPrice;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private RowMergeView DBGrid;
		private System.Windows.Forms.TreeView tvLeft;
	}
}