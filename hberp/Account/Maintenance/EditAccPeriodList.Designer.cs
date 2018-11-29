namespace LXMS
{
    partial class EditAccPeriodList
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
            this.txtEmployeeCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAccMonth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx1 = new LXMS.ToolStripEx();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEmployeeCount
            // 
            this.txtEmployeeCount.Location = new System.Drawing.Point(136, 100);
            this.txtEmployeeCount.Name = "txtEmployeeCount";
            this.txtEmployeeCount.Size = new System.Drawing.Size(103, 21);
            this.txtEmployeeCount.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 73;
            this.label6.Text = "Employee Count";
            // 
            // txtAccMonth
            // 
            this.txtAccMonth.Location = new System.Drawing.Point(136, 76);
            this.txtAccMonth.Name = "txtAccMonth";
            this.txtAccMonth.Size = new System.Drawing.Size(103, 21);
            this.txtAccMonth.TabIndex = 68;
            this.txtAccMonth.TextChanged += new System.EventHandler(this.txtAccYear_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 67;
            this.label3.Text = "Acc Month";
            // 
            // txtAccYear
            // 
            this.txtAccYear.Location = new System.Drawing.Point(136, 52);
            this.txtAccYear.Name = "txtAccYear";
            this.txtAccYear.Size = new System.Drawing.Size(103, 21);
            this.txtAccYear.TabIndex = 66;
            this.txtAccYear.TextChanged += new System.EventHandler(this.txtAccYear_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 65;
            this.label2.Text = "Acc Year";
            // 
            // txtAccName
            // 
            this.txtAccName.Location = new System.Drawing.Point(136, 124);
            this.txtAccName.Name = "txtAccName";
            this.txtAccName.Size = new System.Drawing.Size(207, 21);
            this.txtAccName.TabIndex = 64;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 63;
            this.label1.Text = "Acc Name";
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
            // toolCancel
            // 
            this.toolCancel.Image = global::LXMS.Properties.Resources.Reject;
            this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(47, 35);
            this.toolCancel.Text = "Cancel";
            this.toolCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Size = new System.Drawing.Size(428, 40);
            this.toolStripEx1.TabIndex = 75;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::LXMS.Properties.Resources.Save;
            this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(39, 37);
            this.toolStripButton1.Text = "Save";
            this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::LXMS.Properties.Resources.Reject;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 37);
            this.toolStripButton2.Text = "Cancel";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // EditAccPeriodList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 166);
            this.Controls.Add(this.toolStripEx1);
            this.Controls.Add(this.txtEmployeeCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAccMonth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAccYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAccName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditAccPeriodList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account Period List";
            this.Load += new System.EventHandler(this.EditAccPeriodList_Load);
            this.Shown += new System.EventHandler(this.EditAccPeriodList_Shown);
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private System.Windows.Forms.TextBox txtEmployeeCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAccMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccName;
        private System.Windows.Forms.Label label1;
        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

    }
}