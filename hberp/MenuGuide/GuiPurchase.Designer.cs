namespace LXMS
{
    partial class GuiPurchase
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OPA_VENDOR_ORDER_LIST = new LXMS.ButtonEx();
            this.userPictureBox5 = new LXMS.PictureBoxEx();
            this.ACC_CREDENCE_LIST = new LXMS.ButtonEx();
            this.OPA_PURCHASE_LIST = new LXMS.ButtonEx();
            this.toolStripEx1 = new LXMS.ToolStripEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.QRY_PURCHASE_DETAIL = new System.Windows.Forms.LinkLabel();
            this.QRY_PURCHASE_SUMMARY = new System.Windows.Forms.LinkLabel();
            this.MTN_VENDOR_TYPE = new System.Windows.Forms.LinkLabel();
            this.MTN_VENDOR_LIST = new System.Windows.Forms.LinkLabel();
            this.toolStripEx2 = new LXMS.ToolStripEx();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox5)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            this.toolStripEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OPA_VENDOR_ORDER_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.userPictureBox5);
            this.splitContainer1.Panel1.Controls.Add(this.ACC_CREDENCE_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.OPA_PURCHASE_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripEx1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.QRY_PURCHASE_DETAIL);
            this.splitContainer1.Panel2.Controls.Add(this.QRY_PURCHASE_SUMMARY);
            this.splitContainer1.Panel2.Controls.Add(this.MTN_VENDOR_TYPE);
            this.splitContainer1.Panel2.Controls.Add(this.MTN_VENDOR_LIST);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripEx2);
            this.splitContainer1.Size = new System.Drawing.Size(704, 405);
            this.splitContainer1.SplitterDistance = 507;
            this.splitContainer1.TabIndex = 15;
            // 
            // OPA_VENDOR_ORDER_LIST
            // 
            this.OPA_VENDOR_ORDER_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.OPA_VENDOR_ORDER_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OPA_VENDOR_ORDER_LIST.Image = global::LXMS.Properties.Resources.largeItemOutput;
            this.OPA_VENDOR_ORDER_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OPA_VENDOR_ORDER_LIST.Location = new System.Drawing.Point(70, 68);
            this.OPA_VENDOR_ORDER_LIST.Name = "OPA_VENDOR_ORDER_LIST";
            this.OPA_VENDOR_ORDER_LIST.Size = new System.Drawing.Size(146, 77);
            this.OPA_VENDOR_ORDER_LIST.TabIndex = 49;
            this.OPA_VENDOR_ORDER_LIST.Text = "Vendor Order List";
            this.OPA_VENDOR_ORDER_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OPA_VENDOR_ORDER_LIST.UseVisualStyleBackColor = false;
            this.OPA_VENDOR_ORDER_LIST.Click += new System.EventHandler(this.OPA_VENDOR_ORDER_LIST_Click);
            // 
            // userPictureBox5
            // 
            this.userPictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.userPictureBox5.Image = global::LXMS.Properties.Resources.ARROW_H1;
            this.userPictureBox5.Location = new System.Drawing.Point(232, 252);
            this.userPictureBox5.Name = "userPictureBox5";
            this.userPictureBox5.Size = new System.Drawing.Size(66, 16);
            this.userPictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPictureBox5.TabIndex = 48;
            this.userPictureBox5.TabStop = false;
            this.userPictureBox5.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // ACC_CREDENCE_LIST
            // 
            this.ACC_CREDENCE_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.ACC_CREDENCE_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ACC_CREDENCE_LIST.Image = global::LXMS.Properties.Resources.largeProcessGrant;
            this.ACC_CREDENCE_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ACC_CREDENCE_LIST.Location = new System.Drawing.Point(317, 220);
            this.ACC_CREDENCE_LIST.Name = "ACC_CREDENCE_LIST";
            this.ACC_CREDENCE_LIST.Size = new System.Drawing.Size(146, 77);
            this.ACC_CREDENCE_LIST.TabIndex = 47;
            this.ACC_CREDENCE_LIST.Text = "Account Credence List";
            this.ACC_CREDENCE_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ACC_CREDENCE_LIST.UseVisualStyleBackColor = false;
            this.ACC_CREDENCE_LIST.Click += new System.EventHandler(this.ACC_CREDENCE_LIST_Click);
            // 
            // OPA_PURCHASE_LIST
            // 
            this.OPA_PURCHASE_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.OPA_PURCHASE_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OPA_PURCHASE_LIST.Image = global::LXMS.Properties.Resources.largeItemEpiboly;
            this.OPA_PURCHASE_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OPA_PURCHASE_LIST.Location = new System.Drawing.Point(70, 220);
            this.OPA_PURCHASE_LIST.Name = "OPA_PURCHASE_LIST";
            this.OPA_PURCHASE_LIST.Size = new System.Drawing.Size(146, 77);
            this.OPA_PURCHASE_LIST.TabIndex = 14;
            this.OPA_PURCHASE_LIST.Text = "Purchase List";
            this.OPA_PURCHASE_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OPA_PURCHASE_LIST.UseVisualStyleBackColor = false;
            this.OPA_PURCHASE_LIST.Click += new System.EventHandler(this.OPA_PURCHASE_LIST_Click);
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Size = new System.Drawing.Size(505, 25);
            this.toolStripEx1.TabIndex = 10;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel1.Text = "Purchase";
            // 
            // QRY_PURCHASE_DETAIL
            // 
            this.QRY_PURCHASE_DETAIL.AutoSize = true;
            this.QRY_PURCHASE_DETAIL.LinkColor = System.Drawing.Color.Black;
            this.QRY_PURCHASE_DETAIL.Location = new System.Drawing.Point(20, 129);
            this.QRY_PURCHASE_DETAIL.Name = "QRY_PURCHASE_DETAIL";
            this.QRY_PURCHASE_DETAIL.Size = new System.Drawing.Size(95, 12);
            this.QRY_PURCHASE_DETAIL.TabIndex = 54;
            this.QRY_PURCHASE_DETAIL.TabStop = true;
            this.QRY_PURCHASE_DETAIL.Text = "Purchase Detail";
            this.QRY_PURCHASE_DETAIL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.QRY_PURCHASE_DETAIL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.QRY_PURCHASE_DETAIL_LinkClicked);
            // 
            // QRY_PURCHASE_SUMMARY
            // 
            this.QRY_PURCHASE_SUMMARY.AutoSize = true;
            this.QRY_PURCHASE_SUMMARY.LinkColor = System.Drawing.Color.Black;
            this.QRY_PURCHASE_SUMMARY.Location = new System.Drawing.Point(20, 103);
            this.QRY_PURCHASE_SUMMARY.Name = "QRY_PURCHASE_SUMMARY";
            this.QRY_PURCHASE_SUMMARY.Size = new System.Drawing.Size(101, 12);
            this.QRY_PURCHASE_SUMMARY.TabIndex = 53;
            this.QRY_PURCHASE_SUMMARY.TabStop = true;
            this.QRY_PURCHASE_SUMMARY.Text = "Purchase Summary";
            this.QRY_PURCHASE_SUMMARY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.QRY_PURCHASE_SUMMARY.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.QRY_PURCHASE_SUMMARY_LinkClicked);
            // 
            // MTN_VENDOR_TYPE
            // 
            this.MTN_VENDOR_TYPE.AutoSize = true;
            this.MTN_VENDOR_TYPE.LinkColor = System.Drawing.Color.Black;
            this.MTN_VENDOR_TYPE.Location = new System.Drawing.Point(20, 42);
            this.MTN_VENDOR_TYPE.Name = "MTN_VENDOR_TYPE";
            this.MTN_VENDOR_TYPE.Size = new System.Drawing.Size(71, 12);
            this.MTN_VENDOR_TYPE.TabIndex = 50;
            this.MTN_VENDOR_TYPE.TabStop = true;
            this.MTN_VENDOR_TYPE.Text = "Vendor Type";
            this.MTN_VENDOR_TYPE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MTN_VENDOR_TYPE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MTN_VENDOR_TYPE_LinkClicked);
            // 
            // MTN_VENDOR_LIST
            // 
            this.MTN_VENDOR_LIST.AutoSize = true;
            this.MTN_VENDOR_LIST.LinkColor = System.Drawing.Color.Black;
            this.MTN_VENDOR_LIST.Location = new System.Drawing.Point(20, 64);
            this.MTN_VENDOR_LIST.Name = "MTN_VENDOR_LIST";
            this.MTN_VENDOR_LIST.Size = new System.Drawing.Size(71, 12);
            this.MTN_VENDOR_LIST.TabIndex = 49;
            this.MTN_VENDOR_LIST.TabStop = true;
            this.MTN_VENDOR_LIST.Text = "Vendor List";
            this.MTN_VENDOR_LIST.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MTN_VENDOR_LIST.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MTN_VENDOR_LIST_LinkClicked);
            // 
            // toolStripEx2
            // 
            this.toolStripEx2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripEx2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.toolStripEx2.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx2.Name = "toolStripEx2";
            this.toolStripEx2.Size = new System.Drawing.Size(191, 25);
            this.toolStripEx2.TabIndex = 11;
            this.toolStripEx2.Text = "toolStripEx2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel2.Text = "Other";
            // 
            // GuiPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 405);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GuiPurchase";
            this.Text = "GuiPurchase";
            this.Load += new System.EventHandler(this.GuiPurchase_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox5)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.toolStripEx2.ResumeLayout(false);
            this.toolStripEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ButtonEx OPA_PURCHASE_LIST;
        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private ToolStripEx toolStripEx2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.LinkLabel MTN_VENDOR_TYPE;
        private System.Windows.Forms.LinkLabel MTN_VENDOR_LIST;
        private System.Windows.Forms.LinkLabel QRY_PURCHASE_DETAIL;
        private System.Windows.Forms.LinkLabel QRY_PURCHASE_SUMMARY;
        private ButtonEx ACC_CREDENCE_LIST;
        private PictureBoxEx userPictureBox5;
        private ButtonEx OPA_VENDOR_ORDER_LIST;
    }
}