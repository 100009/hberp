namespace LXMS
{
	partial class ACC_PRODUCT_BOOK
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_PRODUCT_BOOK));
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cboProductType = new System.Windows.Forms.ComboBox();
			this.txtProduct = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cboEndAccName = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cboStartAccName = new System.Windows.Forms.ComboBox();
			this.DBGrid2 = new LXMS.RowMergeView();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.statusStrip1.Location = new System.Drawing.Point(0, 576);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1065, 25);
			this.statusStrip1.TabIndex = 67;
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
			this.Status3.Size = new System.Drawing.Size(709, 20);
			this.Status3.Spring = true;
			this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
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
			this.splitContainer1.Size = new System.Drawing.Size(1065, 576);
			this.splitContainer1.SplitterDistance = 289;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 69;
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
			this.DBGrid.Location = new System.Drawing.Point(0, 100);
			this.DBGrid.Margin = new System.Windows.Forms.Padding(4);
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
			this.DBGrid.Size = new System.Drawing.Size(289, 476);
			this.DBGrid.TabIndex = 68;
			this.DBGrid.SelectionChanged += new System.EventHandler(this.DBGrid_SelectionChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.txtProduct);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(289, 100);
			this.panel1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cboProductType);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(0, 45);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox2.Size = new System.Drawing.Size(289, 55);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Product Type";
			// 
			// cboProductType
			// 
			this.cboProductType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboProductType.FormattingEnabled = true;
			this.cboProductType.Location = new System.Drawing.Point(4, 22);
			this.cboProductType.Margin = new System.Windows.Forms.Padding(4);
			this.cboProductType.Name = "cboProductType";
			this.cboProductType.Size = new System.Drawing.Size(281, 23);
			this.cboProductType.TabIndex = 2;
			this.cboProductType.SelectedIndexChanged += new System.EventHandler(this.cboProductType_SelectedIndexChanged);
			// 
			// txtProduct
			// 
			this.txtProduct.Location = new System.Drawing.Point(77, 8);
			this.txtProduct.Name = "txtProduct";
			this.txtProduct.Size = new System.Drawing.Size(192, 25);
			this.txtProduct.TabIndex = 1;
			this.txtProduct.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProduct_KeyPress);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Product";
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.DBGrid2);
			this.splitContainer2.Size = new System.Drawing.Size(771, 576);
			this.splitContainer2.SplitterDistance = 75;
			this.splitContainer2.SplitterWidth = 5;
			this.splitContainer2.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.cboEndAccName);
			this.groupBox3.Location = new System.Drawing.Point(309, 11);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox3.Size = new System.Drawing.Size(248, 55);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "End Acc Name";
			// 
			// cboEndAccName
			// 
			this.cboEndAccName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboEndAccName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboEndAccName.FormattingEnabled = true;
			this.cboEndAccName.Location = new System.Drawing.Point(4, 22);
			this.cboEndAccName.Margin = new System.Windows.Forms.Padding(4);
			this.cboEndAccName.Name = "cboEndAccName";
			this.cboEndAccName.Size = new System.Drawing.Size(240, 23);
			this.cboEndAccName.TabIndex = 2;
			this.cboEndAccName.SelectedIndexChanged += new System.EventHandler(this.cboEndAccName_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cboStartAccName);
			this.groupBox1.Location = new System.Drawing.Point(15, 11);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(248, 55);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Start Acc Name";
			// 
			// cboStartAccName
			// 
			this.cboStartAccName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboStartAccName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStartAccName.FormattingEnabled = true;
			this.cboStartAccName.Location = new System.Drawing.Point(4, 22);
			this.cboStartAccName.Margin = new System.Windows.Forms.Padding(4);
			this.cboStartAccName.Name = "cboStartAccName";
			this.cboStartAccName.Size = new System.Drawing.Size(240, 23);
			this.cboStartAccName.TabIndex = 2;
			this.cboStartAccName.SelectedIndexChanged += new System.EventHandler(this.cboStartAccName_SelectedIndexChanged);
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
			this.DBGrid2.Margin = new System.Windows.Forms.Padding(4);
			this.DBGrid2.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
			this.DBGrid2.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid2.MergeColumnNames")));
			this.DBGrid2.MultiSelect = false;
			this.DBGrid2.Name = "DBGrid2";
			this.DBGrid2.ReadOnly = true;
			this.DBGrid2.RowChanged = false;
			this.DBGrid2.RowHeadersWidth = 30;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DBGrid2.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.DBGrid2.RowTemplate.Height = 48;
			this.DBGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.DBGrid2.Size = new System.Drawing.Size(771, 496);
			this.DBGrid2.TabIndex = 69;
			// 
			// ACC_PRODUCT_BOOK
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1065, 601);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ACC_PRODUCT_BOOK";
			this.Text = "财务商品帐本";
			this.Load += new System.EventHandler(this.ACC_PRODUCT_BOOK_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private RowMergeView DBGrid2;
		private System.Windows.Forms.TextBox txtProduct;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cboStartAccName;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox cboProductType;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cboEndAccName;
	}
}