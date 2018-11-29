namespace LXMS
{
    partial class QRY_ASSET_LOG
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRY_ASSET_LOG));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.tab1 = new LXMS.TabControlEx();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DBGrid1 = new LXMS.RowMergeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DBGrid2 = new LXMS.RowMergeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DBGrid3 = new LXMS.RowMergeView();
            this.toolStrip1.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid3)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator1,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(653, 38);
            this.toolStrip1.TabIndex = 50;
            this.toolStrip1.Text = "toolStrip1";
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
            // tab1
            // 
            this.tab1.Controls.Add(this.tabPage1);
            this.tab1.Controls.Add(this.tabPage2);
            this.tab1.Controls.Add(this.tabPage3);
            this.tab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab1.Location = new System.Drawing.Point(0, 38);
            this.tab1.Name = "tab1";
            this.tab1.SelectedIndex = 0;
            this.tab1.Size = new System.Drawing.Size(653, 399);
            this.tab1.TabIndex = 51;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DBGrid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(645, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Asset Depre List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DBGrid1
            // 
            this.DBGrid1.AllowUserToAddRows = false;
            this.DBGrid1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.DBGrid1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DBGrid1.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid1.DefaultCellStyle = dataGridViewCellStyle11;
            this.DBGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid1.Location = new System.Drawing.Point(3, 3);
            this.DBGrid1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid1.MergeColumnNames")));
            this.DBGrid1.MultiSelect = false;
            this.DBGrid1.Name = "DBGrid1";
            this.DBGrid1.ReadOnly = true;
            this.DBGrid1.RowChanged = false;
            this.DBGrid1.RowHeadersWidth = 30;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid1.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.DBGrid1.RowTemplate.Height = 23;
            this.DBGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid1.Size = new System.Drawing.Size(639, 363);
            this.DBGrid1.TabIndex = 61;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DBGrid2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(645, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Work Qty";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DBGrid2
            // 
            this.DBGrid2.AllowUserToAddRows = false;
            this.DBGrid2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.DBGrid2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DBGrid2.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid2.DefaultCellStyle = dataGridViewCellStyle2;
            this.DBGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid2.Location = new System.Drawing.Point(3, 3);
            this.DBGrid2.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid2.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid2.MergeColumnNames")));
            this.DBGrid2.MultiSelect = false;
            this.DBGrid2.Name = "DBGrid2";
            this.DBGrid2.ReadOnly = true;
            this.DBGrid2.RowChanged = false;
            this.DBGrid2.RowHeadersWidth = 30;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid2.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DBGrid2.RowTemplate.Height = 23;
            this.DBGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid2.Size = new System.Drawing.Size(639, 363);
            this.DBGrid2.TabIndex = 61;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DBGrid3);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(645, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Asset Evaluate";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DBGrid3
            // 
            this.DBGrid3.AllowUserToAddRows = false;
            this.DBGrid3.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(241)))), ((int)(((byte)(251)))));
            this.DBGrid3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DBGrid3.BackgroundColor = System.Drawing.Color.White;
            this.DBGrid3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DBGrid3.DefaultCellStyle = dataGridViewCellStyle5;
            this.DBGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DBGrid3.Location = new System.Drawing.Point(0, 0);
            this.DBGrid3.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.DBGrid3.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("DBGrid3.MergeColumnNames")));
            this.DBGrid3.MultiSelect = false;
            this.DBGrid3.Name = "DBGrid3";
            this.DBGrid3.ReadOnly = true;
            this.DBGrid3.RowChanged = false;
            this.DBGrid3.RowHeadersWidth = 30;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DBGrid3.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DBGrid3.RowTemplate.Height = 23;
            this.DBGrid3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DBGrid3.Size = new System.Drawing.Size(645, 369);
            this.DBGrid3.TabIndex = 61;
            // 
            // QRY_ASSET_LOG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 437);
            this.Controls.Add(this.tab1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "QRY_ASSET_LOG";
            this.Text = "Asset Log";
            this.Load += new System.EventHandler(this.QRY_ASSET_DATA_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tab1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DBGrid3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private TabControlEx tab1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private RowMergeView DBGrid1;
        private RowMergeView DBGrid2;
        private RowMergeView DBGrid3;
    }
}