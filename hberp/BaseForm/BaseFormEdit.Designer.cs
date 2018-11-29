namespace LXMS
{
    partial class BaseFormEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseFormEdit));
            this.toolStrip1 = new LXMS.ToolStripEx();
            this.toolRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSelect = new System.Windows.Forms.ToolStripButton();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolNew = new System.Windows.Forms.ToolStripButton();
            this.toolEdit = new System.Windows.Forms.ToolStripButton();
            this.toolDel = new System.Windows.Forms.ToolStripButton();
            this.sepDel = new System.Windows.Forms.ToolStripSeparator();
            this.toolSave = new System.Windows.Forms.ToolStripButton();
            this.toolCancel = new System.Windows.Forms.ToolStripButton();
            this.sepCancel = new System.Windows.Forms.ToolStripSeparator();
            this.toolInactive = new System.Windows.Forms.ToolStripButton();
            this.toolAudit = new System.Windows.Forms.ToolStripButton();
            this.toolReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefresh,
            this.toolStripSeparator1,
            this.toolSelect,
            this.txtFind,
            this.toolFind,
            this.toolStripSeparator3,
            this.toolNew,
            this.toolEdit,
            this.toolDel,
            this.sepDel,
            this.toolSave,
            this.toolCancel,
            this.sepCancel,
            this.toolInactive,
            this.toolAudit,
            this.toolReset,
            this.toolStripSeparator2,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 40);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolRefresh
            // 
            this.toolRefresh.Image = global::LXMS.Properties.Resources.Refresh;
            this.toolRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(56, 37);
            this.toolRefresh.Text = "Refresh";
            this.toolRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolSelect
            // 
            this.toolSelect.Image = global::LXMS.Properties.Resources.OK;
            this.toolSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSelect.Name = "toolSelect";
            this.toolSelect.Size = new System.Drawing.Size(46, 37);
            this.toolSelect.Text = "Select";
            this.toolSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSelect.Visible = false;
            this.toolSelect.Click += new System.EventHandler(this.toolSelect_Click);
            // 
            // txtFind
            // 
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(200, 40);
            this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
            // 
            // toolFind
            // 
            this.toolFind.Image = ((System.Drawing.Image)(resources.GetObject("toolFind.Image")));
            this.toolFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFind.Name = "toolFind";
            this.toolFind.Size = new System.Drawing.Size(36, 37);
            this.toolFind.Text = "Find";
            this.toolFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolFind.Click += new System.EventHandler(this.toolFind_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // toolNew
            // 
            this.toolNew.Image = global::LXMS.Properties.Resources.new_med1;
            this.toolNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNew.Name = "toolNew";
            this.toolNew.Size = new System.Drawing.Size(38, 37);
            this.toolNew.Text = "New";
            this.toolNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolNew.Click += new System.EventHandler(this.toolNew_Click);
            // 
            // toolEdit
            // 
            this.toolEdit.Image = global::LXMS.Properties.Resources.Edit;
            this.toolEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.Size = new System.Drawing.Size(34, 37);
            this.toolEdit.Text = "Edit";
            this.toolEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolEdit.Click += new System.EventHandler(this.toolEdit_Click);
            // 
            // toolDel
            // 
            this.toolDel.Image = global::LXMS.Properties.Resources.Delete;
            this.toolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDel.Name = "toolDel";
            this.toolDel.Size = new System.Drawing.Size(31, 37);
            this.toolDel.Text = "Del";
            this.toolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDel.Click += new System.EventHandler(this.toolDel_Click);
            // 
            // sepDel
            // 
            this.sepDel.Name = "sepDel";
            this.sepDel.Size = new System.Drawing.Size(6, 40);
            // 
            // toolSave
            // 
            this.toolSave.Image = global::LXMS.Properties.Resources.Save;
            this.toolSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSave.Name = "toolSave";
            this.toolSave.Size = new System.Drawing.Size(39, 37);
            this.toolSave.Text = "Save";
            this.toolSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSave.Click += new System.EventHandler(this.toolSave_Click);
            // 
            // toolCancel
            // 
            this.toolCancel.Image = global::LXMS.Properties.Resources.Cancel;
            this.toolCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCancel.Name = "toolCancel";
            this.toolCancel.Size = new System.Drawing.Size(50, 37);
            this.toolCancel.Text = "Cancel";
            this.toolCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCancel.Click += new System.EventHandler(this.toolCancel_Click);
            // 
            // sepCancel
            // 
            this.sepCancel.Name = "sepCancel";
            this.sepCancel.Size = new System.Drawing.Size(6, 40);
            // 
            // toolInactive
            // 
            this.toolInactive.Image = global::LXMS.Properties.Resources.Inactive;
            this.toolInactive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolInactive.Name = "toolInactive";
            this.toolInactive.Size = new System.Drawing.Size(56, 37);
            this.toolInactive.Text = "Inactive";
            this.toolInactive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolInactive.Click += new System.EventHandler(this.toolInactive_Click);
            // 
            // toolAudit
            // 
            this.toolAudit.Image = global::LXMS.Properties.Resources.Post;
            this.toolAudit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolAudit.Name = "toolAudit";
            this.toolAudit.Size = new System.Drawing.Size(42, 37);
            this.toolAudit.Text = "Audit";
            this.toolAudit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolAudit.Visible = false;
            this.toolAudit.Click += new System.EventHandler(this.toolAudit_Click);
            // 
            // toolReset
            // 
            this.toolReset.Image = global::LXMS.Properties.Resources.Inactive;
            this.toolReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReset.Name = "toolReset";
            this.toolReset.Size = new System.Drawing.Size(44, 37);
            this.toolReset.Text = "Reset";
            this.toolReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolReset.Visible = false;
            this.toolReset.Click += new System.EventHandler(this.toolReset_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolExit
            // 
            this.toolExit.Image = global::LXMS.Properties.Resources.Close;
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(32, 37);
            this.toolExit.Text = "Exit";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2,
            this.StatusLabel3,
            this.StatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 420);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = false;
            this.StatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(200, 17);
            this.StatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.AutoSize = false;
            this.StatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(150, 17);
            this.StatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.AutoSize = false;
            this.StatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(164, 17);
            // 
            // StatusLabel4
            // 
            this.StatusLabel4.AutoSize = false;
            this.StatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.StatusLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatusLabel4.Name = "StatusLabel4";
            this.StatusLabel4.Size = new System.Drawing.Size(275, 17);
            this.StatusLabel4.Spring = true;
            this.StatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BaseFormEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 442);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BaseFormEdit";
            this.Text = "BaseFormEdit";
            this.Load += new System.EventHandler(this.BaseFormEdit_Load);
            this.Shown += new System.EventHandler(this.BaseFormEdit_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton toolNew;
        private System.Windows.Forms.ToolStripSeparator sepDel;
        private System.Windows.Forms.ToolStripButton toolRefresh;
        private System.Windows.Forms.ToolStripSeparator sepCancel;
        private System.Windows.Forms.ToolStripButton toolDel;
        private System.Windows.Forms.ToolStripButton toolInactive;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolEdit;
        private System.Windows.Forms.ToolStripButton toolSave;
        private System.Windows.Forms.ToolStripButton toolCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel4;
        private System.Windows.Forms.ToolStripTextBox txtFind;
        private System.Windows.Forms.ToolStripButton toolFind;
        private System.Windows.Forms.ToolStripButton toolAudit;
        private System.Windows.Forms.ToolStripButton toolReset;
        private System.Windows.Forms.ToolStripButton toolSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}