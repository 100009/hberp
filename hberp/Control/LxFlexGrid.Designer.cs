namespace LXMS
{
    partial class LxFlexGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LxFlexGrid));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Filter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilterEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilterLarger = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilterLess = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy,
            this.mnuFind,
            this.toolStripSeparator1,
            this.Filter});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 79);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Image = global::LXMS.Properties.Resources.Copy;
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(160, 22);
            this.mnuCopy.Text = "Copy";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuFind
            // 
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.Size = new System.Drawing.Size(100, 23);
            this.mnuFind.ToolTipText = "Please input find text";
            this.mnuFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mnuFind_KeyPress);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // Filter
            // 
            this.Filter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFilterEqual,
            this.mnuFilterLarger,
            this.mnuFilterLess});
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(160, 22);
            this.Filter.Text = "mnuFilter";
            // 
            // mnuFilterEqual
            // 
            this.mnuFilterEqual.Name = "mnuFilterEqual";
            this.mnuFilterEqual.Size = new System.Drawing.Size(90, 22);
            this.mnuFilterEqual.Text = "=";
            this.mnuFilterEqual.Click += new System.EventHandler(this.mnuFilterEqual_Click);
            // 
            // mnuFilterLarger
            // 
            this.mnuFilterLarger.Name = "mnuFilterLarger";
            this.mnuFilterLarger.Size = new System.Drawing.Size(90, 22);
            this.mnuFilterLarger.Text = ">=";
            this.mnuFilterLarger.Click += new System.EventHandler(this.mnuFilterLarger_Click);
            // 
            // mnuFilterLess
            // 
            this.mnuFilterLess.Name = "mnuFilterLess";
            this.mnuFilterLess.Size = new System.Drawing.Size(90, 22);
            this.mnuFilterLess.Text = "<=";
            this.mnuFilterLess.Click += new System.EventHandler(this.mnuFilterLess_Click);
            // 
            // LxFlexGrid
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("$this.OcxState")));
            this.Size = new System.Drawing.Size(388, 310);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripTextBox mnuFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Filter;
        private System.Windows.Forms.ToolStripMenuItem mnuFilterEqual;
        private System.Windows.Forms.ToolStripMenuItem mnuFilterLarger;
        private System.Windows.Forms.ToolStripMenuItem mnuFilterLess;
    }
}
