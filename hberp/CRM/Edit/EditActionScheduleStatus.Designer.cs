namespace LXMS
{
    partial class EditActionScheduleStatus
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtStatusDesc = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.rbCompleted = new System.Windows.Forms.RadioButton();
            this.rbActive = new System.Windows.Forms.RadioButton();
            this.rbCancelled = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtActionContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtActionMan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtActionDate = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(44, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 43;
            this.label9.Text = "Status Desc";
            // 
            // txtStatusDesc
            // 
            this.txtStatusDesc.Location = new System.Drawing.Point(133, 231);
            this.txtStatusDesc.Name = "txtStatusDesc";
            this.txtStatusDesc.Size = new System.Drawing.Size(174, 20);
            this.txtStatusDesc.TabIndex = 42;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSave,
            this.toolStripSeparator1,
            this.toolCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(356, 38);
            this.toolStrip1.TabIndex = 44;
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
            // rbCompleted
            // 
            this.rbCompleted.AutoSize = true;
            this.rbCompleted.BackColor = System.Drawing.Color.Transparent;
            this.rbCompleted.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.rbCompleted.Location = new System.Drawing.Point(133, 208);
            this.rbCompleted.Name = "rbCompleted";
            this.rbCompleted.Size = new System.Drawing.Size(75, 17);
            this.rbCompleted.TabIndex = 46;
            this.rbCompleted.Text = "Completed";
            this.rbCompleted.UseVisualStyleBackColor = false;
            // 
            // rbActive
            // 
            this.rbActive.AutoSize = true;
            this.rbActive.Checked = true;
            this.rbActive.Location = new System.Drawing.Point(47, 208);
            this.rbActive.Name = "rbActive";
            this.rbActive.Size = new System.Drawing.Size(53, 17);
            this.rbActive.TabIndex = 45;
            this.rbActive.TabStop = true;
            this.rbActive.Text = "Going";
            this.rbActive.UseVisualStyleBackColor = true;
            // 
            // rbCancelled
            // 
            this.rbCancelled.AutoSize = true;
            this.rbCancelled.ForeColor = System.Drawing.Color.DarkGray;
            this.rbCancelled.Location = new System.Drawing.Point(235, 208);
            this.rbCancelled.Name = "rbCancelled";
            this.rbCancelled.Size = new System.Drawing.Size(72, 17);
            this.rbCancelled.TabIndex = 47;
            this.rbCancelled.Text = "Cancelled";
            this.rbCancelled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Action Content";
            // 
            // txtActionContent
            // 
            this.txtActionContent.Location = new System.Drawing.Point(133, 114);
            this.txtActionContent.Multiline = true;
            this.txtActionContent.Name = "txtActionContent";
            this.txtActionContent.ReadOnly = true;
            this.txtActionContent.Size = new System.Drawing.Size(174, 72);
            this.txtActionContent.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Action Man";
            // 
            // txtActionMan
            // 
            this.txtActionMan.Location = new System.Drawing.Point(133, 62);
            this.txtActionMan.Name = "txtActionMan";
            this.txtActionMan.ReadOnly = true;
            this.txtActionMan.Size = new System.Drawing.Size(174, 20);
            this.txtActionMan.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Action Date";
            // 
            // txtActionDate
            // 
            this.txtActionDate.Location = new System.Drawing.Point(133, 88);
            this.txtActionDate.Name = "txtActionDate";
            this.txtActionDate.ReadOnly = true;
            this.txtActionDate.Size = new System.Drawing.Size(174, 20);
            this.txtActionDate.TabIndex = 52;
            // 
            // EditActionScheduleStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 288);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtActionDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtActionMan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtActionContent);
            this.Controls.Add(this.rbCancelled);
            this.Controls.Add(this.rbCompleted);
            this.Controls.Add(this.rbActive);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtStatusDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditActionScheduleStatus";
            this.ShowInTaskbar = false;
            this.Text = "Action Schedule";
            this.Load += new System.EventHandler(this.EditActionScheduleStatus_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtStatusDesc;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private System.Windows.Forms.RadioButton rbCompleted;
        private System.Windows.Forms.RadioButton rbActive;
        private System.Windows.Forms.RadioButton rbCancelled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtActionContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtActionMan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtActionDate;
    }
}