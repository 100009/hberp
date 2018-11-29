namespace LXMS
{
    partial class ACC_SUBJECT_EXPENSE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_SUBJECT_EXPENSE));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolOK = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.DBGrid = new LXMS.RowMergeView();
            this.toolNew = new System.Windows.Forms.ToolStripButton();
            this.toolDel = new System.Windows.Forms.ToolStripButton();
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
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolNew,
            this.toolDel,
            this.toolStripSeparator1,
            this.toolOK,
            this.toolCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(621, 40);
            this.toolStrip1.TabIndex = 262;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolOK
            // 
            this.toolOK.Image = global::LXMS.Properties.Resources.OK;
            this.toolOK.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOK.Name = "toolOK";
            this.toolOK.Size = new System.Drawing.Size(30, 37);
            this.toolOK.Text = "OK";
            this.toolOK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolOK.Click += new System.EventHandler(this.toolOK_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolCancel
            // 
            this.toolCancel.Image = global::LXMS.Properties.Resources.Clear;
            this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(50, 37);
            this.toolCancel.Text = "Cancel";
            this.toolCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 395);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(621, 25);
            this.statusStrip1.TabIndex = 286;
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
            this.Status1.Size = new System.Drawing.Size(100, 20);
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
            this.Status3.Size = new System.Drawing.Size(338, 20);
            this.Status3.Spring = true;
            this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.splitContainer1.Panel1.Controls.Add(this.rb3);
            this.splitContainer1.Panel1.Controls.Add(this.rb2);
            this.splitContainer1.Panel1.Controls.Add(this.rb1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DBGrid);
            this.splitContainer1.Size = new System.Drawing.Size(621, 355);
            this.splitContainer1.SplitterDistance = 39;
            this.splitContainer1.TabIndex = 287;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(446, 11);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(71, 16);
            this.rb3.TabIndex = 2;
            this.rb3.TabStop = true;
            this.rb3.Text = "财务费用";
            this.rb3.UseVisualStyleBackColor = true;
            this.rb3.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(269, 11);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(71, 16);
            this.rb2.TabIndex = 1;
            this.rb2.TabStop = true;
            this.rb2.Text = "销售费用";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(69, 12);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(71, 16);
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Text = "管理费用";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
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
            this.DBGrid.RowTemplate.Height = 40;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(621, 312);
            this.DBGrid.TabIndex = 60;
            this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
            // 
            // toolNew
            // 
            this.toolNew.Image = global::LXMS.Properties.Resources.new_med1;
            this.toolNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNew.Name = "toolNew";
            this.toolNew.Size = new System.Drawing.Size(38, 37);
            this.toolNew.Text = "New";
            this.toolNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolNew.Click += new System.EventHandler(this.toolNew_Click);
            // 
            // toolDel
            // 
            this.toolDel.Image = global::LXMS.Properties.Resources.Delete;
            this.toolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new System.Drawing.Size(31, 37);
            this.toolDel.Text = "Del";
            this.toolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // ACC_SUBJECT_EXPENSE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 420);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ACC_SUBJECT_EXPENSE";
            this.Text = "Expense Subject";
            this.Load += new System.EventHandler(this.ACC_SUBJECT_EXPENSE_Load);
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
        private System.Windows.Forms.ToolStripButton toolOK;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private RowMergeView DBGrid;
        private System.Windows.Forms.ToolStripButton toolNew;
        private System.Windows.Forms.ToolStripButton toolDel;
    }
}