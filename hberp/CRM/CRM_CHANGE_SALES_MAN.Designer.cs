namespace LXMS
{
    partial class CRM_CHANGE_SALES_MAN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRM_CHANGE_SALES_MAN));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboSalesMan = new System.Windows.Forms.ToolStripComboBox();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCheckAll = new System.Windows.Forms.ToolStripButton();
            this.toolCheckNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cboNewMan = new System.Windows.Forms.ToolStripComboBox();
            this.toolPost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.DBGrid = new LXMS.RowMergeView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboSalesMan,
            this.toolRefresh,
            this.toolStripSeparator2,
            this.toolCheckAll,
            this.toolCheckNone,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.cboNewMan,
            this.toolPost,
            this.toolStripSeparator1,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(701, 38);
            this.toolStrip1.TabIndex = 65;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(60, 35);
            this.toolStripLabel1.Text = "Sales Man";
            // 
            // cboSalesMan
            // 
            this.cboSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSalesMan.Name = "cboSalesMan";
            this.cboSalesMan.Size = new System.Drawing.Size(121, 38);
            this.cboSalesMan.SelectedIndexChanged += new System.EventHandler(this.cboSalesMan_SelectedIndexChanged);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolCheckAll
            // 
            this.toolCheckAll.Image = global::LXMS.Properties.Resources.Browser;
            this.toolCheckAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCheckAll.Name = "toolCheckAll";
            this.toolCheckAll.Size = new System.Drawing.Size(25, 35);
            this.toolCheckAll.Text = "All";
            this.toolCheckAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolCheckAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCheckAll.Click += new System.EventHandler(this.toolCheckAll_Click);
            // 
            // toolCheckNone
            // 
            this.toolCheckNone.Image = global::LXMS.Properties.Resources.View;
            this.toolCheckNone.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolCheckNone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCheckNone.Name = "toolCheckNone";
            this.toolCheckNone.Size = new System.Drawing.Size(40, 35);
            this.toolCheckNone.Text = "None";
            this.toolCheckNone.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolCheckNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCheckNone.Click += new System.EventHandler(this.toolCheckNone_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(87, 35);
            this.toolStripLabel2.Text = "New Sales Man";
            // 
            // cboNewMan
            // 
            this.cboNewMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewMan.Name = "cboNewMan";
            this.cboNewMan.Size = new System.Drawing.Size(121, 38);
            this.cboNewMan.SelectedIndexChanged += new System.EventHandler(this.cboNewMan_SelectedIndexChanged);
            // 
            // toolPost
            // 
            this.toolPost.Image = global::LXMS.Properties.Resources.Post;
            this.toolPost.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPost.Name = "toolPost";
            this.toolPost.Size = new System.Drawing.Size(34, 35);
            this.toolPost.Text = "Post";
            this.toolPost.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolPost.Click += new System.EventHandler(this.toolPost_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
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
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.DBGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid.Location = new System.Drawing.Point(0, 38);
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
            this.DBGrid.Size = new System.Drawing.Size(701, 407);
            this.DBGrid.TabIndex = 66;
            // 
            // CRM_CHANGE_SALES_MAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 445);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CRM_CHANGE_SALES_MAN";
            this.Text = "Change Sales Man";
            this.Load += new System.EventHandler(this.CRM_CHANGE_SALES_MAN_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolPost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboSalesMan;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cboNewMan;
        private RowMergeView DBGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolCheckAll;
        private System.Windows.Forms.ToolStripButton toolCheckNone;
    }
}