namespace LXMS
{
    partial class EditWorkQty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditWorkQty));
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAssetName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAssetId = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSave,
            this.toolStripSeparator1,
            this.toolCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(369, 38);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolSave
            // 
            this.toolSave.Image = global::LXMS.Properties.Resources.Save;
            this.toolSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(35, 35);
            this.toolSave.Text = "Save";
            this.toolSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // toolCancel
            // 
            this.toolCancel.Image = global::LXMS.Properties.Resources.Delete;
            this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(47, 35);
            this.toolCancel.Text = "Cancel";
            this.toolCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(262, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 18);
            this.label9.TabIndex = 325;
            this.label9.Text = "*";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(128, 109);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(128, 20);
            this.txtQty.TabIndex = 312;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 324;
            this.label10.Text = "Qty";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(262, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 18);
            this.label4.TabIndex = 323;
            this.label4.Text = "*";
            // 
            // txtAssetName
            // 
            this.txtAssetName.Location = new System.Drawing.Point(128, 80);
            this.txtAssetName.Name = "txtAssetName";
            this.txtAssetName.ReadOnly = true;
            this.txtAssetName.Size = new System.Drawing.Size(128, 20);
            this.txtAssetName.TabIndex = 311;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 322;
            this.label6.Text = "Asset Name";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(262, 59);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 18);
            this.label26.TabIndex = 316;
            this.label26.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 315;
            this.label1.Text = "Asset Id";
            // 
            // txtAssetId
            // 
            this.txtAssetId.Location = new System.Drawing.Point(128, 54);
            this.txtAssetId.Name = "txtAssetId";
            this.txtAssetId.ReadOnly = true;
            this.txtAssetId.Size = new System.Drawing.Size(128, 20);
            this.txtAssetId.TabIndex = 308;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(128, 135);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(128, 20);
            this.txtRemark.TabIndex = 314;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 319;
            this.label5.Text = "Remark";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status1,
            this.Status2,
            this.status3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 184);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(369, 25);
            this.statusStrip1.TabIndex = 326;
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
            // status3
            // 
            this.status3.AutoSize = false;
            this.status3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.status3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.status3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.status3.Name = "status3";
            this.status3.Size = new System.Drawing.Size(86, 20);
            this.status3.Spring = true;
            this.status3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // EditWorkQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 209);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAssetName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAssetId);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditWorkQty";
            this.ShowInTaskbar = false;
            this.Text = "Work Qty";
            this.Load += new System.EventHandler(this.EditWorkQty_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAssetName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAssetId;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel Status2;
        private System.Windows.Forms.ToolStripStatusLabel status3;
    }
}