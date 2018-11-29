namespace LXMS
{

    partial class RowMergeView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterLarger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterLess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterNotEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterEqualLarger = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFilterEqualLess = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFilterLike = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStepPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStepNext = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExportOO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExportPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExportTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMath = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMathSum = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMathAvg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMathMax = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMathMin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolRowCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolForeColor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBackColor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolFontSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFrozen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCancelFrozen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolColsVisible = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCopy,
            this.toolFilter,
            this.toolStepPrevious,
            this.toolStepNext,
            this.toolRefresh,
            this.toolStripSeparator2,
            this.toolExport,
            this.toolMath,
            this.toolStripSeparator3,
            this.toolForeColor,
            this.toolBackColor,
            this.toolFontSize,
            this.toolStripSeparator7,
            this.toolFrozen,
            this.toolCancelFrozen,
            this.toolStripSeparator6,
            this.toolColsVisible});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 314);
            // 
            // toolCopy
            // 
            this.toolCopy.Name = "toolCopy";
            this.toolCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolCopy.Size = new System.Drawing.Size(190, 22);
            this.toolCopy.Text = "Copy";
            this.toolCopy.Click += new System.EventHandler(this.toolCopy_Click);
            // 
            // toolFilter
            // 
            this.toolFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFilterEqual,
            this.toolFilterLarger,
            this.toolFilterLess,
            this.toolFilterNotEqual,
            this.toolFilterEqualLarger,
            this.toolFilterEqualLess,
            this.toolStripSeparator4,
            this.toolFilterLike});
            this.toolFilter.Name = "toolFilter";
            this.toolFilter.Size = new System.Drawing.Size(190, 22);
            this.toolFilter.Text = "Filter";
            // 
            // toolFilterEqual
            // 
            this.toolFilterEqual.Name = "toolFilterEqual";
            this.toolFilterEqual.Size = new System.Drawing.Size(129, 22);
            this.toolFilterEqual.Text = "=";
            this.toolFilterEqual.Click += new System.EventHandler(this.toolFilterEqual_Click);
            // 
            // toolFilterLarger
            // 
            this.toolFilterLarger.Name = "toolFilterLarger";
            this.toolFilterLarger.Size = new System.Drawing.Size(129, 22);
            this.toolFilterLarger.Text = ">";
            this.toolFilterLarger.Click += new System.EventHandler(this.toolFilterLarger_Click);
            // 
            // toolFilterLess
            // 
            this.toolFilterLess.Name = "toolFilterLess";
            this.toolFilterLess.Size = new System.Drawing.Size(129, 22);
            this.toolFilterLess.Text = "<";
            this.toolFilterLess.Click += new System.EventHandler(this.toolFilterLess_Click);
            // 
            // toolFilterNotEqual
            // 
            this.toolFilterNotEqual.Name = "toolFilterNotEqual";
            this.toolFilterNotEqual.Size = new System.Drawing.Size(129, 22);
            this.toolFilterNotEqual.Text = "‡";
            this.toolFilterNotEqual.Click += new System.EventHandler(this.toolFilterNotEqual_Click);
            // 
            // toolFilterEqualLarger
            // 
            this.toolFilterEqualLarger.Name = "toolFilterEqualLarger";
            this.toolFilterEqualLarger.Size = new System.Drawing.Size(129, 22);
            this.toolFilterEqualLarger.Text = "≥";
            this.toolFilterEqualLarger.Click += new System.EventHandler(this.toolFilterEqualLarger_Click);
            // 
            // toolFilterEqualLess
            // 
            this.toolFilterEqualLess.Name = "toolFilterEqualLess";
            this.toolFilterEqualLess.Size = new System.Drawing.Size(129, 22);
            this.toolFilterEqualLess.Text = "≤";
            this.toolFilterEqualLess.Click += new System.EventHandler(this.toolFilterEqualLess_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(126, 6);
            // 
            // toolFilterLike
            // 
            this.toolFilterLike.Name = "toolFilterLike";
            this.toolFilterLike.Size = new System.Drawing.Size(129, 22);
            this.toolFilterLike.Text = "Like(%%)";
            this.toolFilterLike.Click += new System.EventHandler(this.toolFilterLike_Click);
            // 
            // toolStepPrevious
            // 
            this.toolStepPrevious.Image = global::LXMS.Properties.Resources.Previous;
            this.toolStepPrevious.Name = "toolStepPrevious";
            this.toolStepPrevious.Size = new System.Drawing.Size(190, 22);
            this.toolStepPrevious.Text = "< back";
            this.toolStepPrevious.Click += new System.EventHandler(this.toolStepPrevious_Click);
            // 
            // toolStepNext
            // 
            this.toolStepNext.Image = global::LXMS.Properties.Resources.Next;
            this.toolStepNext.Name = "toolStepNext";
            this.toolStepNext.Size = new System.Drawing.Size(190, 22);
            this.toolStepNext.Text = "next >";
            this.toolStepNext.Click += new System.EventHandler(this.toolStepNext_Click);
            // 
            // toolRefresh
            // 
            this.toolRefresh.Image = global::LXMS.Properties.Resources.Refresh;
            this.toolRefresh.Name = "toolRefresh";
            this.toolRefresh.Size = new System.Drawing.Size(190, 22);
            this.toolRefresh.Text = "Refresh";
            this.toolRefresh.Click += new System.EventHandler(this.toolRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // toolExport
            // 
            this.toolExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExportExcel,
            this.toolExportOO,
            this.toolExportPDF,
            this.toolExportTxt});
            this.toolExport.Image = global::LXMS.Properties.Resources.Export;
            this.toolExport.Name = "toolExport";
            this.toolExport.Size = new System.Drawing.Size(190, 22);
            this.toolExport.Text = "Export";
            // 
            // toolExportExcel
            // 
            this.toolExportExcel.Name = "toolExportExcel";
            this.toolExportExcel.Size = new System.Drawing.Size(146, 22);
            this.toolExportExcel.Text = "Excel";
            this.toolExportExcel.Click += new System.EventHandler(this.toolExportExcelMenuItem_Click);
            // 
            // toolExportOO
            // 
            this.toolExportOO.Name = "toolExportOO";
            this.toolExportOO.Size = new System.Drawing.Size(146, 22);
            this.toolExportOO.Text = "Open Office";
            this.toolExportOO.Click += new System.EventHandler(this.toolExportOOMenuItem_Click);
            // 
            // toolExportPDF
            // 
            this.toolExportPDF.Name = "toolExportPDF";
            this.toolExportPDF.Size = new System.Drawing.Size(146, 22);
            this.toolExportPDF.Text = "PDF";
            this.toolExportPDF.Click += new System.EventHandler(this.toolExportPdfMenuItem_Click);
            // 
            // toolExportTxt
            // 
            this.toolExportTxt.Name = "toolExportTxt";
            this.toolExportTxt.Size = new System.Drawing.Size(146, 22);
            this.toolExportTxt.Text = "Text";
            this.toolExportTxt.Click += new System.EventHandler(this.toolExportTextMenuItem_Click);
            // 
            // toolMath
            // 
            this.toolMath.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMathSum,
            this.toolMathAvg,
            this.toolStripSeparator1,
            this.toolMathMax,
            this.toolMathMin,
            this.toolStripSeparator9,
            this.toolRowCount});
            this.toolMath.Image = global::LXMS.Properties.Resources.largeTaskGrant;
            this.toolMath.Name = "toolMath";
            this.toolMath.Size = new System.Drawing.Size(190, 22);
            this.toolMath.Text = "Calculate";
            // 
            // toolMathSum
            // 
            this.toolMathSum.Name = "toolMathSum";
            this.toolMathSum.Size = new System.Drawing.Size(139, 22);
            this.toolMathSum.Text = "Sum";
            this.toolMathSum.Click += new System.EventHandler(this.toolMathSum_Click);
            // 
            // toolMathAvg
            // 
            this.toolMathAvg.Name = "toolMathAvg";
            this.toolMathAvg.Size = new System.Drawing.Size(139, 22);
            this.toolMathAvg.Text = "Average";
            this.toolMathAvg.Click += new System.EventHandler(this.toolMathAvg_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // toolMathMax
            // 
            this.toolMathMax.Name = "toolMathMax";
            this.toolMathMax.Size = new System.Drawing.Size(139, 22);
            this.toolMathMax.Text = "Max";
            this.toolMathMax.Click += new System.EventHandler(this.toolMathMax_Click);
            // 
            // toolMathMin
            // 
            this.toolMathMin.Name = "toolMathMin";
            this.toolMathMin.Size = new System.Drawing.Size(139, 22);
            this.toolMathMin.Text = "Min";
            this.toolMathMin.Click += new System.EventHandler(this.toolMathMin_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(136, 6);
            // 
            // toolRowCount
            // 
            this.toolRowCount.Name = "toolRowCount";
            this.toolRowCount.Size = new System.Drawing.Size(139, 22);
            this.toolRowCount.Text = "Row Count";
            this.toolRowCount.Click += new System.EventHandler(this.toolRowCount_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
            // 
            // toolForeColor
            // 
            this.toolForeColor.Image = global::LXMS.Properties.Resources.largeCustomerList;
            this.toolForeColor.Name = "toolForeColor";
            this.toolForeColor.Size = new System.Drawing.Size(190, 22);
            this.toolForeColor.Text = "Fore Color";
            this.toolForeColor.Click += new System.EventHandler(this.toolForeColor_Click);
            // 
            // toolBackColor
            // 
            this.toolBackColor.Image = global::LXMS.Properties.Resources.largeColorList;
            this.toolBackColor.Name = "toolBackColor";
            this.toolBackColor.Size = new System.Drawing.Size(190, 22);
            this.toolBackColor.Text = "Back Color";
            this.toolBackColor.Click += new System.EventHandler(this.toolBackColor_Click);
            // 
            // toolFontSize
            // 
            this.toolFontSize.Name = "toolFontSize";
            this.toolFontSize.Size = new System.Drawing.Size(190, 22);
            this.toolFontSize.Text = "Font Size";
            this.toolFontSize.Click += new System.EventHandler(this.toolFontSize_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(187, 6);
            // 
            // toolFrozen
            // 
            this.toolFrozen.Image = global::LXMS.Properties.Resources.Frozen;
            this.toolFrozen.Name = "toolFrozen";
            this.toolFrozen.Size = new System.Drawing.Size(190, 22);
            this.toolFrozen.Text = "Frozen";
            this.toolFrozen.Click += new System.EventHandler(this.toolFrozen_Click);
            // 
            // toolCancelFrozen
            // 
            this.toolCancelFrozen.Image = global::LXMS.Properties.Resources.CancelFrozen;
            this.toolCancelFrozen.Name = "toolCancelFrozen";
            this.toolCancelFrozen.Size = new System.Drawing.Size(190, 22);
            this.toolCancelFrozen.Text = "Cancel Frozen";
            this.toolCancelFrozen.Click += new System.EventHandler(this.toolCancelFrozen_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(187, 6);
            // 
            // toolColsVisible
            // 
            this.toolColsVisible.Image = global::LXMS.Properties.Resources.View;
            this.toolColsVisible.Name = "toolColsVisible";
            this.toolColsVisible.Size = new System.Drawing.Size(190, 22);
            this.toolColsVisible.Text = "Set Columns Visible";
            this.toolColsVisible.Click += new System.EventHandler(this.toolColsVisible_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1,
            this.StatusLabel2,
            this.StatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(200, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.AutoSize = false;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(80, 17);
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.AutoSize = false;
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(120, 17);
            // 
            // StatusLabel3
            // 
            this.StatusLabel3.AutoSize = false;
            this.StatusLabel3.Name = "StatusLabel3";
            this.StatusLabel3.Size = new System.Drawing.Size(23, 17);
            this.StatusLabel3.Spring = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RowMergeView
            // 
            this.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultCellStyle = dataGridViewCellStyle1;
            this.RowHeadersWidth = 30;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.RowTemplate.Height = 40;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DataSourceChanged += new System.EventHandler(this.RowMergeView_DataSourceChanged);
            this.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.RowMergeView_CellBeginEdit);
            this.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RowMergeView_CellEnter);
            this.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.RowMergeView_CellLeave);
            this.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.RowMergeView_ColumnWidthChanged);
            this.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.RowMergeView_DataBindingComplete);
            this.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.RowMergeView_DataError);
            this.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.RowMergeView_RowHeightChanged);
            this.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.RowMergeView_RowPostPaint);
            this.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.RowMergeView_RowValidated);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.RowMergeView_Scroll);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RowMergeView_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolExport;
        private System.Windows.Forms.ToolStripMenuItem toolExportExcel;
        private System.Windows.Forms.ToolStripMenuItem toolExportOO;
        private System.Windows.Forms.ToolStripMenuItem toolExportPDF;
        private System.Windows.Forms.ToolStripMenuItem toolExportTxt;
        private System.Windows.Forms.ToolStripMenuItem toolBackColor;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem toolMath;
        private System.Windows.Forms.ToolStripMenuItem toolMathSum;
        private System.Windows.Forms.ToolStripMenuItem toolMathAvg;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolMathMax;
        private System.Windows.Forms.ToolStripMenuItem toolMathMin;
        private System.Windows.Forms.ToolStripMenuItem toolColsVisible;
        private System.Windows.Forms.ToolStripMenuItem toolFilter;
        private System.Windows.Forms.ToolStripMenuItem toolFilterEqual;
        private System.Windows.Forms.ToolStripMenuItem toolFilterLarger;
        private System.Windows.Forms.ToolStripMenuItem toolFilterLess;
        private System.Windows.Forms.ToolStripMenuItem toolRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolFilterNotEqual;
        private System.Windows.Forms.ToolStripMenuItem toolFilterEqualLarger;
        private System.Windows.Forms.ToolStripMenuItem toolFilterEqualLess;
        private System.Windows.Forms.ToolStripMenuItem toolStepPrevious;
        private System.Windows.Forms.ToolStripMenuItem toolStepNext;
        private System.Windows.Forms.ToolStripMenuItem toolFrozen;
        private System.Windows.Forms.ToolStripMenuItem toolCancelFrozen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem toolRowCount;
        private System.Windows.Forms.ToolStripMenuItem toolForeColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolCopy;
        private System.Windows.Forms.ToolStripMenuItem toolFontSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolFilterLike;
        private System.Windows.Forms.Button button1;
    }
}