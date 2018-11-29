namespace LXMS
{
    partial class GuiSecurity
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
            this.userPictureBox2 = new LXMS.PictureBoxEx();
            this.SEC_TASK_GRANT = new LXMS.ButtonEx();
            this.SEC_TASK_GROUP = new LXMS.ButtonEx();
            this.userPictureBox1 = new LXMS.PictureBoxEx();
            this.SEC_TASK_LIST = new LXMS.ButtonEx();
            this.SEC_ROLE_LIST = new LXMS.ButtonEx();
            this.userPictureBox5 = new LXMS.PictureBoxEx();
            this.SEC_USER_LIST = new LXMS.ButtonEx();
            this.toolStripEx1 = new LXMS.ToolStripEx();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.QRY_TASK_GRANT = new System.Windows.Forms.LinkLabel();
            this.SEC_SYSTEM_PARAMETERS = new System.Windows.Forms.LinkLabel();
            this.SEC_CHANGE_PASSWORD = new System.Windows.Forms.LinkLabel();
            this.toolStripEx2 = new LXMS.ToolStripEx();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox5)).BeginInit();
            this.toolStripEx1.SuspendLayout();
            this.toolStripEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.userPictureBox2);
            this.splitContainer1.Panel1.Controls.Add(this.SEC_TASK_GRANT);
            this.splitContainer1.Panel1.Controls.Add(this.SEC_TASK_GROUP);
            this.splitContainer1.Panel1.Controls.Add(this.userPictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.SEC_TASK_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.SEC_ROLE_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.userPictureBox5);
            this.splitContainer1.Panel1.Controls.Add(this.SEC_USER_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripEx1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.QRY_TASK_GRANT);
            this.splitContainer1.Panel2.Controls.Add(this.SEC_SYSTEM_PARAMETERS);
            this.splitContainer1.Panel2.Controls.Add(this.SEC_CHANGE_PASSWORD);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripEx2);
            this.splitContainer1.Size = new System.Drawing.Size(901, 496);
            this.splitContainer1.SplitterDistance = 704;
            this.splitContainer1.TabIndex = 14;
            // 
            // userPictureBox2
            // 
            this.userPictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.userPictureBox2.Image = global::LXMS.Properties.Resources.ARROW_H1;
            this.userPictureBox2.Location = new System.Drawing.Point(421, 302);
            this.userPictureBox2.Name = "userPictureBox2";
            this.userPictureBox2.Size = new System.Drawing.Size(79, 17);
            this.userPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPictureBox2.TabIndex = 42;
            this.userPictureBox2.TabStop = false;
            this.userPictureBox2.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // SEC_TASK_GRANT
            // 
            this.SEC_TASK_GRANT.BackColor = System.Drawing.Color.AliceBlue;
            this.SEC_TASK_GRANT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SEC_TASK_GRANT.Image = global::LXMS.Properties.Resources.largeTaskGrant;
            this.SEC_TASK_GRANT.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SEC_TASK_GRANT.Location = new System.Drawing.Point(516, 265);
            this.SEC_TASK_GRANT.Name = "SEC_TASK_GRANT";
            this.SEC_TASK_GRANT.Size = new System.Drawing.Size(102, 83);
            this.SEC_TASK_GRANT.TabIndex = 41;
            this.SEC_TASK_GRANT.Text = "Task Grant";
            this.SEC_TASK_GRANT.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SEC_TASK_GRANT.UseVisualStyleBackColor = false;
            this.SEC_TASK_GRANT.Click += new System.EventHandler(this.SEC_TASK_GRANT_Click);
            // 
            // SEC_TASK_GROUP
            // 
            this.SEC_TASK_GROUP.BackColor = System.Drawing.Color.AliceBlue;
            this.SEC_TASK_GROUP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SEC_TASK_GROUP.Image = global::LXMS.Properties.Resources.largeProcessList;
            this.SEC_TASK_GROUP.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SEC_TASK_GROUP.Location = new System.Drawing.Point(63, 265);
            this.SEC_TASK_GROUP.Name = "SEC_TASK_GROUP";
            this.SEC_TASK_GROUP.Size = new System.Drawing.Size(102, 83);
            this.SEC_TASK_GROUP.TabIndex = 40;
            this.SEC_TASK_GROUP.Text = "Task Group";
            this.SEC_TASK_GROUP.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SEC_TASK_GROUP.UseVisualStyleBackColor = false;
            this.SEC_TASK_GROUP.Click += new System.EventHandler(this.SEC_TASK_GROUP_Click);
            // 
            // userPictureBox1
            // 
            this.userPictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.userPictureBox1.Image = global::LXMS.Properties.Resources.ARROW_H1;
            this.userPictureBox1.Location = new System.Drawing.Point(188, 302);
            this.userPictureBox1.Name = "userPictureBox1";
            this.userPictureBox1.Size = new System.Drawing.Size(83, 17);
            this.userPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPictureBox1.TabIndex = 39;
            this.userPictureBox1.TabStop = false;
            this.userPictureBox1.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // SEC_TASK_LIST
            // 
            this.SEC_TASK_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.SEC_TASK_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SEC_TASK_LIST.Image = global::LXMS.Properties.Resources.largeProcessTrack;
            this.SEC_TASK_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SEC_TASK_LIST.Location = new System.Drawing.Point(301, 265);
            this.SEC_TASK_LIST.Name = "SEC_TASK_LIST";
            this.SEC_TASK_LIST.Size = new System.Drawing.Size(102, 83);
            this.SEC_TASK_LIST.TabIndex = 38;
            this.SEC_TASK_LIST.Text = "Task List";
            this.SEC_TASK_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SEC_TASK_LIST.UseVisualStyleBackColor = false;
            this.SEC_TASK_LIST.Click += new System.EventHandler(this.SEC_TASK_LIST_Click);
            // 
            // SEC_ROLE_LIST
            // 
            this.SEC_ROLE_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.SEC_ROLE_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SEC_ROLE_LIST.Image = global::LXMS.Properties.Resources.largeRoleList;
            this.SEC_ROLE_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SEC_ROLE_LIST.Location = new System.Drawing.Point(63, 98);
            this.SEC_ROLE_LIST.Name = "SEC_ROLE_LIST";
            this.SEC_ROLE_LIST.Size = new System.Drawing.Size(106, 83);
            this.SEC_ROLE_LIST.TabIndex = 37;
            this.SEC_ROLE_LIST.Text = "Role List";
            this.SEC_ROLE_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SEC_ROLE_LIST.UseVisualStyleBackColor = false;
            this.SEC_ROLE_LIST.Click += new System.EventHandler(this.SEC_ROLE_LIST_Click);
            // 
            // userPictureBox5
            // 
            this.userPictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.userPictureBox5.Image = global::LXMS.Properties.Resources.ARROW_H1;
            this.userPictureBox5.Location = new System.Drawing.Point(188, 134);
            this.userPictureBox5.Name = "userPictureBox5";
            this.userPictureBox5.Size = new System.Drawing.Size(83, 18);
            this.userPictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userPictureBox5.TabIndex = 36;
            this.userPictureBox5.TabStop = false;
            this.userPictureBox5.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // SEC_USER_LIST
            // 
            this.SEC_USER_LIST.BackColor = System.Drawing.Color.AliceBlue;
            this.SEC_USER_LIST.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SEC_USER_LIST.Image = global::LXMS.Properties.Resources.largeUserList;
            this.SEC_USER_LIST.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.SEC_USER_LIST.Location = new System.Drawing.Point(297, 98);
            this.SEC_USER_LIST.Name = "SEC_USER_LIST";
            this.SEC_USER_LIST.Size = new System.Drawing.Size(106, 83);
            this.SEC_USER_LIST.TabIndex = 31;
            this.SEC_USER_LIST.Text = "User List";
            this.SEC_USER_LIST.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SEC_USER_LIST.UseVisualStyleBackColor = false;
            this.SEC_USER_LIST.Click += new System.EventHandler(this.SEC_USER_LIST_Click);
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStripEx1.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Size = new System.Drawing.Size(702, 25);
            this.toolStripEx1.TabIndex = 10;
            this.toolStripEx1.Text = "toolStripEx1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(53, 22);
            this.toolStripLabel1.Text = "Security";
            // 
            // QRY_TASK_GRANT
            // 
            this.QRY_TASK_GRANT.AutoSize = true;
            this.QRY_TASK_GRANT.LinkColor = System.Drawing.Color.Black;
            this.QRY_TASK_GRANT.Location = new System.Drawing.Point(17, 98);
            this.QRY_TASK_GRANT.Name = "QRY_TASK_GRANT";
            this.QRY_TASK_GRANT.Size = new System.Drawing.Size(91, 13);
            this.QRY_TASK_GRANT.TabIndex = 14;
            this.QRY_TASK_GRANT.TabStop = true;
            this.QRY_TASK_GRANT.Text = "Query Task Grant";
            this.QRY_TASK_GRANT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.QRY_TASK_GRANT_LinkClicked);
            // 
            // SEC_SYSTEM_PARAMETERS
            // 
            this.SEC_SYSTEM_PARAMETERS.AutoSize = true;
            this.SEC_SYSTEM_PARAMETERS.LinkColor = System.Drawing.Color.Black;
            this.SEC_SYSTEM_PARAMETERS.Location = new System.Drawing.Point(17, 69);
            this.SEC_SYSTEM_PARAMETERS.Name = "SEC_SYSTEM_PARAMETERS";
            this.SEC_SYSTEM_PARAMETERS.Size = new System.Drawing.Size(97, 13);
            this.SEC_SYSTEM_PARAMETERS.TabIndex = 13;
            this.SEC_SYSTEM_PARAMETERS.TabStop = true;
            this.SEC_SYSTEM_PARAMETERS.Text = "System Parameters";
            this.SEC_SYSTEM_PARAMETERS.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SEC_SYSTEM_PARAMETERS_LinkClicked);
            // 
            // SEC_CHANGE_PASSWORD
            // 
            this.SEC_CHANGE_PASSWORD.AutoSize = true;
            this.SEC_CHANGE_PASSWORD.LinkColor = System.Drawing.Color.Black;
            this.SEC_CHANGE_PASSWORD.Location = new System.Drawing.Point(17, 40);
            this.SEC_CHANGE_PASSWORD.Name = "SEC_CHANGE_PASSWORD";
            this.SEC_CHANGE_PASSWORD.Size = new System.Drawing.Size(93, 13);
            this.SEC_CHANGE_PASSWORD.TabIndex = 12;
            this.SEC_CHANGE_PASSWORD.TabStop = true;
            this.SEC_CHANGE_PASSWORD.Text = "Change Password";
            this.SEC_CHANGE_PASSWORD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SEC_CHANGE_PASSWORD_LinkClicked);
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
            // GuiSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(901, 496);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GuiSecurity";
            this.Text = "GuiSecurity";
            this.Load += new System.EventHandler(this.GuiSecurity_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox5)).EndInit();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.toolStripEx2.ResumeLayout(false);
            this.toolStripEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private ButtonEx SEC_ROLE_LIST;
        private PictureBoxEx userPictureBox5;
        private ButtonEx SEC_USER_LIST;
        private ToolStripEx toolStripEx1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.LinkLabel SEC_SYSTEM_PARAMETERS;
        private System.Windows.Forms.LinkLabel SEC_CHANGE_PASSWORD;
        private ToolStripEx toolStripEx2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private PictureBoxEx userPictureBox2;
        private ButtonEx SEC_TASK_GRANT;
        private ButtonEx SEC_TASK_GROUP;
        private PictureBoxEx userPictureBox1;
        private ButtonEx SEC_TASK_LIST;
        private System.Windows.Forms.LinkLabel QRY_TASK_GRANT;
    }
}