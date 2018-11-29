namespace LXMS
{
    partial class LxListBox
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolSelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSelNone = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtFind = new System.Windows.Forms.ToolStripTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSelAll,
            this.toolSelNone,
            this.toolStripSeparator1,
            this.txtFind});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 54);
            // 
            // toolSelAll
            // 
            this.toolSelAll.Name = "toolSelAll";
            this.toolSelAll.Size = new System.Drawing.Size(117, 22);
            this.toolSelAll.Text = "Select All";
            this.toolSelAll.Click += new System.EventHandler(this.toolSelAllMenuItem_Click);
            // 
            // toolSelNone
            // 
            this.toolSelNone.Name = "toolSelNone";
            this.toolSelNone.Size = new System.Drawing.Size(117, 22);
            this.toolSelNone.Text = "Clear";
            this.toolSelNone.Click += new System.EventHandler(this.toolSelNoneMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
            // 
            // txtFind
            // 
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(100, 21);
            //this.txtFind.ToolTipText = "Please input find text";
            this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
            // 
            // LxListBox
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Size = new System.Drawing.Size(120, 95);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolSelAll;
        private System.Windows.Forms.ToolStripMenuItem toolSelNone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtFind;
    }
}
