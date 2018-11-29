namespace LXMS
{
    partial class frmSoftRegister
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
            this.txtRegisterCode = new System.Windows.Forms.TextBox();
            this.txtHostCode = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUnregister = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRegisterCode
            // 
            this.txtRegisterCode.Location = new System.Drawing.Point(82, 129);
            this.txtRegisterCode.Name = "txtRegisterCode";
            this.txtRegisterCode.Size = new System.Drawing.Size(242, 20);
            this.txtRegisterCode.TabIndex = 12;
            // 
            // txtHostCode
            // 
            this.txtHostCode.Location = new System.Drawing.Point(82, 91);
            this.txtHostCode.Name = "txtHostCode";
            this.txtHostCode.ReadOnly = true;
            this.txtHostCode.Size = new System.Drawing.Size(242, 20);
            this.txtHostCode.TabIndex = 11;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(154, 166);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 10;
            this.btnRegister.Text = "注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "注册码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "机器码";
            // 
            // btnUnregister
            // 
            this.btnUnregister.Location = new System.Drawing.Point(249, 166);
            this.btnUnregister.Name = "btnUnregister";
            this.btnUnregister.Size = new System.Drawing.Size(75, 23);
            this.btnUnregister.TabIndex = 13;
            this.btnUnregister.Text = "反注册";
            this.btnUnregister.UseVisualStyleBackColor = true;
            this.btnUnregister.Click += new System.EventHandler(this.btnUnregister_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(82, 27);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(242, 46);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "温馨提示：本软件提供六个月的试用期，如需长期使用本软件，请与本人联系，联系方式： QQ: 20207075     联系人： 鲁先生";
            // 
            // frmSoftRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 215);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnUnregister);
            this.Controls.Add(this.txtRegisterCode);
            this.Controls.Add(this.txtHostCode);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSoftRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Software Register";
            this.Load += new System.EventHandler(this.frmSoftRegister_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRegisterCode;
        private System.Windows.Forms.TextBox txtHostCode;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUnregister;
        private System.Windows.Forms.TextBox textBox1;
    }
}