namespace LXMS
{
    partial class ACC_CREDENCE_LIST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACC_CREDENCE_LIST));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DBGrid = new LXMS.RowMergeView();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolBalance = new System.Windows.Forms.ToolStripButton();
            this.toolTrial = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboAccName = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.Status3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(895, 25);
            this.statusStrip1.TabIndex = 57;
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
            this.Status3.Size = new System.Drawing.Size(544, 20);
            this.Status3.Spring = true;
            this.Status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.DBGrid.Location = new System.Drawing.Point(0, 40);
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
            this.DBGrid.Size = new System.Drawing.Size(895, 356);
            this.DBGrid.TabIndex = 58;
            this.DBGrid.SelectionChanged += new System.EventHandler(this.DBGrid_SelectionChanged);
            this.DBGrid.DoubleClick += new System.EventHandler(this.DBGrid_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboAccName,
            this.toolRefresh,
            this.toolStripSeparator3,
            this.toolNew,
            this.toolEdit,
            this.toolDel,
            this.toolStripSeparator1,
            this.toolAudit,
            this.toolReset,
            this.toolStripSeparator2,
            this.toolExport,
            this.sepDel,
            this.toolExit,
            this.toolBalance,
            this.toolTrial});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(895, 40);
            this.toolStrip1.TabIndex = 11;
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
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
            // toolEdit
            // 
            this.toolEdit.Image = global::LXMS.Properties.Resources.Edit;
            this.toolEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.Size = new System.Drawing.Size(34, 37);
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
            this.toolDel.Size = new System.Drawing.Size(31, 37);
            this.toolDel.Text = "Del";
            this.toolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolAudit
            // 
            this.toolAudit.Image = global::LXMS.Properties.Resources.Post;
            this.toolAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAudit.Name = "toolAudit";
            this.toolAudit.Size = new System.Drawing.Size(42, 37);
            this.toolAudit.Text = "Audit";
            this.toolAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolAudit.Click += new System.EventHandler(this.toolAudit_Click);
            // 
            // toolReset
            // 
            this.toolReset.Image = global::LXMS.Properties.Resources.Inactive;
            this.toolReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReset.Name = "toolReset";
            this.toolReset.Size = new System.Drawing.Size(44, 37);
            this.toolReset.Text = "Reset";
            this.toolReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolReset.Click += new System.EventHandler(this.toolReset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolExport
            // 
            this.toolExport.Image = global::LXMS.Properties.Resources.Export;
            this.toolExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExport.Name = "toolExport";
            this.toolExport.Size = new System.Drawing.Size(50, 37);
            this.toolExport.Text = "Export";
            this.toolExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExport.Click += new System.EventHandler(this.toolExport_Click);
            // 
            // sepDel
            // 
            this.sepDel.Name = "sepDel";
            this.sepDel.Size = new System.Drawing.Size(6, 40);
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
            // toolBalance
            // 
            this.toolBalance.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolBalance.Image = global::LXMS.Properties.Resources.Property;
            this.toolBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBalance.Name = "toolBalance";
            this.toolBalance.Size = new System.Drawing.Size(108, 37);
            this.toolBalance.Text = "Monthly Balance";
            this.toolBalance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBalance.Click += new System.EventHandler(this.toolBalance_Click);
            // 
            // toolTrial
            // 
            this.toolTrial.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolTrial.Image = global::LXMS.Properties.Resources.Connect;
            this.toolTrial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTrial.Name = "toolTrial";
            this.toolTrial.Size = new System.Drawing.Size(86, 37);
            this.toolTrial.Text = "Trial Balance";
            this.toolTrial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTrial.Click += new System.EventHandler(this.toolTrial_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(67, 37);
            this.toolStripLabel1.Text = "Acc Name";
            // 
            // cboAccName
            // 
            this.cboAccName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccName.Name = "cboAccName";
            this.cboAccName.Size = new System.Drawing.Size(150, 40);
            this.cboAccName.SelectedIndexChanged += new System.EventHandler(this.cboAccName_SelectedIndexChanged);
            // 
            // ACC_CREDENCE_LIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 421);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ACC_CREDENCE_LIST";
            this.Text = "Account Credence List";
            this.Load += new System.EventHandler(this.ACC_CREDENCE_LIST_Load);
            this.Shown += new System.EventHandler(this.ACC_CREDENCE_LIST_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolNew;
        private System.Windows.Forms.ToolStripButton toolEdit;
        private System.Windows.Forms.ToolStripButton toolDel;
        private System.Windows.Forms.ToolStripSeparator sepDel;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolAudit;
        private System.Windows.Forms.ToolStripButton toolReset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel Status3;
        private RowMergeView DBGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExport;
        private System.Windows.Forms.ToolStripButton toolBalance;
        private System.Windows.Forms.ToolStripButton toolTrial;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboAccName;




    }
}