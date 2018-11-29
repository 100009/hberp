namespace LXMS
{
    partial class frmSelectGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectGrid));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolCheckAll = new System.Windows.Forms.ToolStripButton();
            this.toolCheckNone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSelect = new System.Windows.Forms.ToolStripButton();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.DBGrid = new LXMS.RowMergeView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCheckAll,
            this.toolCheckNone,
            this.toolStripSeparator1,
            this.toolSelect,
            this.toolCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(674, 40);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolCheckAll
            // 
            this.toolCheckAll.Image = global::LXMS.Properties.Resources.Browser;
            this.toolCheckAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolCheckAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCheckAll.Name = "toolCheckAll";
            this.toolCheckAll.Size = new System.Drawing.Size(26, 37);
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
            this.toolCheckNone.Size = new System.Drawing.Size(44, 37);
            this.toolCheckNone.Text = "None";
            this.toolCheckNone.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolCheckNone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCheckNone.Click += new System.EventHandler(this.toolCheckNone_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolSelect
            // 
            this.toolSelect.Image = global::LXMS.Properties.Resources.OK;
            this.toolSelect.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSelect.Name = "toolSelect";
            this.toolSelect.Size = new System.Drawing.Size(46, 37);
            this.toolSelect.Text = "Select";
            this.toolSelect.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSelect.Click += new System.EventHandler(this.toolSelect_Click);
            // 
            // toolCancel
            // 
            this.toolCancel.Image = global::LXMS.Properties.Resources.Delete;
            this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(50, 37);
            this.toolCancel.Text = "Cancel";
            this.toolCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
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
            this.DBGrid.RowTemplate.Height = 40;
            this.DBGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid.Size = new System.Drawing.Size(674, 385);
            this.DBGrid.TabIndex = 60;
            // 
            // frmSelectGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 425);
            this.Controls.Add(this.DBGrid);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSelectGrid";
            this.ShowInTaskbar = false;
            this.Text = "frmSelectGrid";
            this.Load += new System.EventHandler(this.frmSelectGrid_Load);
            this.Shown += new System.EventHandler(this.frmSelectGrid_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private RowMergeView DBGrid;
        private System.Windows.Forms.ToolStripButton toolCheckAll;
        private System.Windows.Forms.ToolStripButton toolCheckNone;
    }
}