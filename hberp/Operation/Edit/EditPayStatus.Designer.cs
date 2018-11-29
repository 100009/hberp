namespace LXMS
{
    partial class EditPayStatus
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPostInvoice = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboAccountNo = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.dtpPayDate = new System.Windows.Forms.DateTimePicker();
            this.label23 = new System.Windows.Forms.Label();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtInvoiceMny = new System.Windows.Forms.TextBox();
            this.btnPostPay = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.cboInvoiceStatus = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cboPayStatus = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lblPayStatus = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPostInvoice);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.cboAccountNo);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.dtpPayDate);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.txtInvoiceNo);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.txtInvoiceMny);
            this.groupBox3.Controls.Add(this.btnPostPay);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.cboInvoiceStatus);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.cboPayStatus);
            this.groupBox3.Controls.Add(this.label30);
            this.groupBox3.Controls.Add(this.lblPayStatus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(480, 220);
            this.groupBox3.TabIndex = 278;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // btnPostInvoice
            // 
            this.btnPostInvoice.Image = global::LXMS.Properties.Resources.OK;
            this.btnPostInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPostInvoice.Location = new System.Drawing.Point(336, 108);
            this.btnPostInvoice.Name = "btnPostInvoice";
            this.btnPostInvoice.Size = new System.Drawing.Size(107, 22);
            this.btnPostInvoice.TabIndex = 7;
            this.btnPostInvoice.Text = "Post";
            this.btnPostInvoice.UseVisualStyleBackColor = true;
            this.btnPostInvoice.Click += new System.EventHandler(this.btnPostInvoice_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::LXMS.Properties.Resources.Close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(336, 162);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 33);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cboAccountNo
            // 
            this.cboAccountNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccountNo.FormattingEnabled = true;
            this.cboAccountNo.Location = new System.Drawing.Point(116, 55);
            this.cboAccountNo.Name = "cboAccountNo";
            this.cboAccountNo.Size = new System.Drawing.Size(107, 20);
            this.cboAccountNo.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(34, 59);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 216;
            this.label29.Text = "Account No";
            // 
            // dtpPayDate
            // 
            this.dtpPayDate.CustomFormat = "yyyy/MM/dd";
            this.dtpPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPayDate.Location = new System.Drawing.Point(115, 81);
            this.dtpPayDate.Name = "dtpPayDate";
            this.dtpPayDate.Size = new System.Drawing.Size(107, 21);
            this.dtpPayDate.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(34, 85);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 215;
            this.label23.Text = "Pay Date";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(335, 81);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(107, 21);
            this.txtInvoiceNo.TabIndex = 6;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(254, 84);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 213;
            this.label28.Text = "Invoice No";
            // 
            // txtInvoiceMny
            // 
            this.txtInvoiceMny.Location = new System.Drawing.Point(335, 54);
            this.txtInvoiceMny.Name = "txtInvoiceMny";
            this.txtInvoiceMny.Size = new System.Drawing.Size(107, 21);
            this.txtInvoiceMny.TabIndex = 5;
            // 
            // btnPostPay
            // 
            this.btnPostPay.Image = global::LXMS.Properties.Resources.OK;
            this.btnPostPay.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPostPay.Location = new System.Drawing.Point(115, 108);
            this.btnPostPay.Name = "btnPostPay";
            this.btnPostPay.Size = new System.Drawing.Size(107, 22);
            this.btnPostPay.TabIndex = 3;
            this.btnPostPay.Text = "Post";
            this.btnPostPay.UseVisualStyleBackColor = true;
            this.btnPostPay.Click += new System.EventHandler(this.btnPostPay_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(254, 59);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(71, 12);
            this.label34.TabIndex = 208;
            this.label34.Text = "Invoice Mny";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(449, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(14, 18);
            this.label33.TabIndex = 206;
            this.label33.Text = "*";
            // 
            // cboInvoiceStatus
            // 
            this.cboInvoiceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvoiceStatus.FormattingEnabled = true;
            this.cboInvoiceStatus.Location = new System.Drawing.Point(336, 29);
            this.cboInvoiceStatus.Name = "cboInvoiceStatus";
            this.cboInvoiceStatus.Size = new System.Drawing.Size(107, 20);
            this.cboInvoiceStatus.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(254, 32);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(89, 12);
            this.label32.TabIndex = 204;
            this.label32.Text = "Invoice Status";
            // 
            // cboPayStatus
            // 
            this.cboPayStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayStatus.FormattingEnabled = true;
            this.cboPayStatus.Location = new System.Drawing.Point(116, 29);
            this.cboPayStatus.Name = "cboPayStatus";
            this.cboPayStatus.Size = new System.Drawing.Size(107, 20);
            this.cboPayStatus.TabIndex = 0;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(229, 32);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 18);
            this.label30.TabIndex = 202;
            this.label30.Text = "*";
            // 
            // lblPayStatus
            // 
            this.lblPayStatus.AutoSize = true;
            this.lblPayStatus.Location = new System.Drawing.Point(34, 33);
            this.lblPayStatus.Name = "lblPayStatus";
            this.lblPayStatus.Size = new System.Drawing.Size(65, 12);
            this.lblPayStatus.TabIndex = 201;
            this.lblPayStatus.Text = "Pay Status";
            // 
            // EditPayStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 220);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditPayStatus";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pay Status";
            this.Load += new System.EventHandler(this.EditPayStatus_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboAccountNo;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.DateTimePicker dtpPayDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtInvoiceMny;
        private System.Windows.Forms.Button btnPostPay;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox cboInvoiceStatus;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cboPayStatus;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblPayStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPostInvoice;
    }
}