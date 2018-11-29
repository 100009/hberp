namespace LXMS
{
    public partial class MsgLabel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolHide = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolView,
            this.toolStripSeparator1,
            this.toolOptions,
            this.toolHide});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 76);
            // 
            // toolView
            // 
            this.toolView.Image = global::LXMS.Properties.Resources.CancelFrozen;
            this.toolView.Name = "toolView";
            this.toolView.Size = new System.Drawing.Size(116, 22);
            this.toolView.Text = "View";
            this.toolView.Click += new System.EventHandler(this.toolView_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // toolOptions
            // 
            this.toolOptions.Image = global::LXMS.Properties.Resources.Connect;
            this.toolOptions.Name = "toolOptions";
            this.toolOptions.Size = new System.Drawing.Size(116, 22);
            this.toolOptions.Text = "Options";
            this.toolOptions.Click += new System.EventHandler(this.toolSetting_Click);
            // 
            // toolHide
            // 
            this.toolHide.Name = "toolHide";
            this.toolHide.Size = new System.Drawing.Size(116, 22);
            this.toolHide.Text = "Hide";
            this.toolHide.Click += new System.EventHandler(this.toolHide_Click);
            // 
            // MsgLabel
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Size = new System.Drawing.Size(416, 64);
            this.MouseEnter += new System.EventHandler(this.MsgLabel_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MsgLabel_MouseLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolOptions;
        private System.Windows.Forms.ToolStripMenuItem toolHide;
        private System.Windows.Forms.ToolStripMenuItem toolView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
