namespace LXMS
{
    partial class SYS_GUIDE
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
            this.SYS_INIT = new LXMS.ButtonEx();
            this.ACC_START_DATA_FORM = new LXMS.ButtonEx();
            this.btnClose = new LXMS.ButtonEx();
            this.SuspendLayout();
            // 
            // SYS_INIT
            // 
            this.SYS_INIT.BackColor = System.Drawing.Color.AliceBlue;
            this.SYS_INIT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SYS_INIT.Image = global::LXMS.Properties.Resources.largeTransferReceive;
            this.SYS_INIT.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SYS_INIT.Location = new System.Drawing.Point(87, 24);
            this.SYS_INIT.Name = "SYS_INIT";
            this.SYS_INIT.Size = new System.Drawing.Size(138, 80);
            this.SYS_INIT.TabIndex = 78;
            this.SYS_INIT.Text = "System Initialize";
            this.SYS_INIT.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SYS_INIT.UseVisualStyleBackColor = false;
            this.SYS_INIT.Click += new System.EventHandler(this.SYS_INIT_Click);
            // 
            // ACC_START_DATA_FORM
            // 
            this.ACC_START_DATA_FORM.BackColor = System.Drawing.Color.AliceBlue;
            this.ACC_START_DATA_FORM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ACC_START_DATA_FORM.Image = global::LXMS.Properties.Resources.largeQuotation;
            this.ACC_START_DATA_FORM.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ACC_START_DATA_FORM.Location = new System.Drawing.Point(87, 131);
            this.ACC_START_DATA_FORM.Name = "ACC_START_DATA_FORM";
            this.ACC_START_DATA_FORM.Size = new System.Drawing.Size(138, 80);
            this.ACC_START_DATA_FORM.TabIndex = 79;
            this.ACC_START_DATA_FORM.Text = "Account Start Data Form";
            this.ACC_START_DATA_FORM.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ACC_START_DATA_FORM.UseVisualStyleBackColor = false;
            this.ACC_START_DATA_FORM.Click += new System.EventHandler(this.ACC_START_DATA_FORM_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.AliceBlue;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Image = global::LXMS.Properties.Resources.Close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(521, 329);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 32);
            this.btnClose.TabIndex = 80;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SYS_GUIDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 373);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ACC_START_DATA_FORM);
            this.Controls.Add(this.SYS_INIT);
            this.Name = "SYS_GUIDE";
            this.Text = "System Guide";
            this.Load += new System.EventHandler(this.OPA_SYSTEM_GUIDE_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonEx SYS_INIT;
        private ButtonEx ACC_START_DATA_FORM;
        private ButtonEx btnClose;
    }
}