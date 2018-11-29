namespace LXMS
{
    partial class frmListSelect
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListSelect));
            this.lvSource = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDest = new System.Windows.Forms.Label();
            this.lvDest = new System.Windows.Forms.ListView();
            this.btnSelOne = new System.Windows.Forms.Button();
            this.btnSelAll = new System.Windows.Forms.Button();
            this.btnDelOne = new System.Windows.Forms.Button();
            this.btnDelAll = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvSource
            // 
            this.lvSource.AllowDrop = true;
            this.lvSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSource.ContextMenuStrip = this.contextMenuStrip1;
            this.lvSource.FullRowSelect = true;
            this.lvSource.Location = new System.Drawing.Point(12, 25);
            this.lvSource.Name = "lvSource";
            this.lvSource.Size = new System.Drawing.Size(228, 246);
            this.lvSource.TabIndex = 0;
            this.lvSource.UseCompatibleStateImageBehavior = false;
            this.lvSource.View = System.Windows.Forms.View.Details;
            this.lvSource.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvSource_ItemDrag);
            this.lvSource.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvSource_DragDrop);
            this.lvSource.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvSource_DragEnter);
            this.lvSource.DoubleClick += new System.EventHandler(this.btnSelOne_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFind});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 29);
            // 
            // txtFind
            // 
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 23);
            this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            // 
            // lblDest
            // 
            this.lblDest.AutoSize = true;
            this.lblDest.Location = new System.Drawing.Point(280, 9);
            this.lblDest.Name = "lblDest";
            this.lblDest.Size = new System.Drawing.Size(60, 13);
            this.lblDest.TabIndex = 2;
            this.lblDest.Text = "Destination";
            // 
            // lvDest
            // 
            this.lvDest.AllowDrop = true;
            this.lvDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDest.FullRowSelect = true;
            this.lvDest.Location = new System.Drawing.Point(283, 25);
            this.lvDest.Name = "lvDest";
            this.lvDest.Size = new System.Drawing.Size(228, 246);
            this.lvDest.TabIndex = 4;
            this.lvDest.UseCompatibleStateImageBehavior = false;
            this.lvDest.View = System.Windows.Forms.View.Details;
            this.lvDest.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvDest_ItemDrag);
            this.lvDest.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvDest_DragDrop);
            this.lvDest.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvDest_DragEnter);
            this.lvDest.DoubleClick += new System.EventHandler(this.btnDelOne_Click);
            // 
            // btnSelOne
            // 
            this.btnSelOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelOne.Location = new System.Drawing.Point(248, 118);
            this.btnSelOne.Name = "btnSelOne";
            this.btnSelOne.Size = new System.Drawing.Size(29, 26);
            this.btnSelOne.TabIndex = 5;
            this.btnSelOne.Text = ">";
            this.btnSelOne.UseVisualStyleBackColor = true;
            this.btnSelOne.Click += new System.EventHandler(this.btnSelOne_Click);
            // 
            // btnSelAll
            // 
            this.btnSelAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSelAll.Location = new System.Drawing.Point(248, 75);
            this.btnSelAll.Name = "btnSelAll";
            this.btnSelAll.Size = new System.Drawing.Size(29, 26);
            this.btnSelAll.TabIndex = 6;
            this.btnSelAll.Text = ">>";
            this.btnSelAll.UseVisualStyleBackColor = true;
            this.btnSelAll.Click += new System.EventHandler(this.btnSelAll_Click);
            // 
            // btnDelOne
            // 
            this.btnDelOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelOne.Location = new System.Drawing.Point(248, 150);
            this.btnDelOne.Name = "btnDelOne";
            this.btnDelOne.Size = new System.Drawing.Size(29, 26);
            this.btnDelOne.TabIndex = 7;
            this.btnDelOne.Text = "<";
            this.btnDelOne.UseVisualStyleBackColor = true;
            this.btnDelOne.Click += new System.EventHandler(this.btnDelOne_Click);
            // 
            // btnDelAll
            // 
            this.btnDelAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelAll.Location = new System.Drawing.Point(248, 194);
            this.btnDelAll.Name = "btnDelAll";
            this.btnDelAll.Size = new System.Drawing.Size(29, 26);
            this.btnDelAll.TabIndex = 8;
            this.btnDelAll.Text = "<<";
            this.btnDelAll.UseVisualStyleBackColor = true;
            this.btnDelAll.Click += new System.EventHandler(this.btnDelAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(172, 296);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 33);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(283, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 33);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmListSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 353);
            this.Controls.Add(this.lvSource);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblDest);
            this.Controls.Add(this.btnDelAll);
            this.Controls.Add(this.btnDelOne);
            this.Controls.Add(this.btnSelAll);
            this.Controls.Add(this.btnSelOne);
            this.Controls.Add(this.lvDest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListSelect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "List Select";
            this.Load += new System.EventHandler(this.frmListSelect_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDest;
        private System.Windows.Forms.ListView lvDest;
        private System.Windows.Forms.Button btnSelOne;
        private System.Windows.Forms.Button btnSelAll;
        private System.Windows.Forms.Button btnDelOne;
        private System.Windows.Forms.Button btnDelAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripTextBox txtFind;
    }
}