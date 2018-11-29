using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LXMS
{
    /// <summary>
    /// DataGridView行合并.请对属性MergeColumnNames 赋值既可
    /// </summary>
    public partial class RowMergeView : DataGridView
    {
        private Array _ShowButtonColumns;   //有按钮的列名称
        IList<modGridRowVisible> _listgrv = new List<modGridRowVisible>();
        INIClass ini = new INIClass(Util.INI_FILE);
        int _curstep = 0;
        #region 构造函数
        bool _rowChanged = false;
        public RowMergeView()
        {
            InitializeComponent();
            this.Controls.Add(button1);
            clsTranslate.TranslateMenu(contextMenuStrip1);            
            //frmOptions.LoadDefaultColor(false);
            this.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ReadOnly = true;
            this.BackgroundColor = Color.White;
            this.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
        public RowMergeView(bool altercolor)
        {
            InitializeComponent();
            this.Controls.Add(button1);
            clsTranslate.TranslateMenu(contextMenuStrip1);
            if (altercolor)
            {
                //frmOptions.LoadDefaultColor(false);
                this.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            }
            else
                this.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.ReadOnly = true;
            this.BackgroundColor = Color.Empty;
            this.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        /// <summary>  
        /// 设置要显示按钮的列  
        /// </summary>  
        /// <param name="ShowButtonColumns"></param>  
        public void SetParam(Array ShowButtonColumns)
        {
            _ShowButtonColumns = ShowButtonColumns;
        }

        /// <summary>  
        /// 数组中是否有与指定值相等的元素  
        /// </summary>  
        /// <param name="columnName"></param>  
        /// <param name="ShowButtonColumns"></param>  
        /// <returns></returns>  
        private bool IsShowButtonColumn(string columnName, Array ShowButtonColumns)
        {
            if (string.IsNullOrEmpty(columnName) || ShowButtonColumns == null || ShowButtonColumns.Length < 1) return false;

            foreach (string astr in ShowButtonColumns)
                if (astr == columnName) return true;

            return false;
        }

        //定义按钮的单击事件  
        public delegate void ButtonClick();
        public event ButtonClick ButtonSelectClick;

        private void button1_Click(object sender, EventArgs e)
        {
            this.ButtonSelectClick.DynamicInvoke(null);
        }
        #endregion

        #region 重写的事件

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    DrawCell(e);
                }
                else
                {
                    //二维表头
                    if (e.RowIndex == -1)
                    {
                        if (SpanRows.ContainsKey(e.ColumnIndex)) //被合并的列
                        {
                            //画边框

                            Graphics g = e.Graphics;
                            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                            int left = e.CellBounds.Left, top = e.CellBounds.Top + 2,
                            right = e.CellBounds.Right, bottom = e.CellBounds.Bottom;

                            switch (SpanRows[e.ColumnIndex].Position)
                            {
                                case 1:
                                    left += 2;
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    right -= 2;
                                    break;
                            }

                            //画上半部分底色

                            g.FillRectangle(new SolidBrush(this._mergecolumnheaderbackcolor), left, top,
                            right - left, (bottom - top) / 2);

                            //画中线

                            g.DrawLine(new Pen(this.GridColor), left, (top + bottom) / 2,
                            right, (top + bottom) / 2);

                            //写小标题
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            g.DrawString(e.Value + "", e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, (top + bottom) / 2, right - left, (bottom - top) / 2), sf);
                            left = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Left, true).Left - 2;

                            if (left < 0) left = this.GetCellDisplayRectangle(-1, -1, true).Width;
                            right = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Right, true).Right - 2;
                            if (right < 0) right = this.Width;

                            g.DrawString(SpanRows[e.ColumnIndex].Text, e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                            e.Handled = true;
                        }
                    }
                }
                base.OnCellPainting(e);
            }
            catch
            { }
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
        }
        #endregion

        #region 自定义方法

        /// <summary>
        /// 画单元格
        /// </summary>
        /// <param name="e"></param>
        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(this.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellwidth;
            //上面相同的行数

            int UpRows = 0;
            //下面相同的行数

            int DownRows = 0;
            //总行数

            int count = 0;
            if (this.MergeColumnNames.Contains(this.Columns[e.ColumnIndex].Name) && e.RowIndex != -1)
            {
                cellwidth = e.CellBounds.Width;
                Pen gridLinePen = new Pen(gridBrush);
                string curValue = e.Value == null ? "" : e.Value.ToString().Trim();
                string curSelected = this.CurrentRow.Cells[e.ColumnIndex].Value == null ? "" : this.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(curValue))
                {
                    #region 获取下面的行数

                    for (int i = e.RowIndex; i < this.Rows.Count; i++)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            DownRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    #region 获取上面的行数

                    for (int i = e.RowIndex; i >= 0; i--)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
                            UpRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    count = DownRows + UpRows - 1;
                    if (count < 2)
                    {
                        return;
                    }
                }
                if (this.Rows[e.RowIndex].Selected)
                {
                    backBrush.Color = e.CellStyle.SelectionBackColor;
                    fontBrush.Color = e.CellStyle.SelectionForeColor;
                }
                //以背景色填充
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
                //画字符串
                PaintingFont(e, cellwidth, UpRows, DownRows, count);
                if (DownRows == 1)
                {
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    count = 0;
                }
                // 画右边线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                e.Handled = true;
            }
        }
        /// <summary>
        /// 画字符串
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cellwidth"></param>
        /// <param name="UpRows"></param>
        /// <param name="DownRows"></param>
        /// <param name="count"></param>
        private void PaintingFont(System.Windows.Forms.DataGridViewCellPaintingEventArgs e, int cellwidth, int UpRows, int DownRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int fontheight = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int fontwidth = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int cellheight = e.CellBounds.Height;

            if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
        }
        #endregion

        #region 属性

        /// <summary>
        /// 设置或获取合并列的集合

        /// </summary>
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [Description("设置或获取合并列的集合"), Browsable(true), Category("单元格合并")]
        public List<string> MergeColumnNames
        {
            get
            {
                return _mergecolumnname;
            }
            set
            {
                _mergecolumnname = value;
            }
        }
        private List<string> _mergecolumnname = new List<string>();
        #endregion

        #region 二维表头
        private struct SpanInfo //表头信息
        {
            public SpanInfo(string Text, int Position, int Left, int Right)
            {
                this.Text = Text;
                this.Position = Position;
                this.Left = Left;
                this.Right = Right;
            }

            public string Text; //列主标题
            public int Position; //位置，1:左，2中，3右

            public int Left; //对应左行
            public int Right; //对应右行
        }
        private Dictionary<int, SpanInfo> SpanRows = new Dictionary<int, SpanInfo>();//需要2维表头的列

        /// <summary>
        /// 合并列

        /// </summary>
        /// <param name="ColIndex">列的索引</param>
        /// <param name="ColCount">需要合并的列数</param>
        /// <param name="Text">合并列后的文本</param>
        public void AddSpanHeader(int ColIndex, int ColCount, string Text)
        {
            if (ColCount < 2)
            {
                throw new Exception("行宽应大于等于2，合并1列无意义。");
            }
            //将这些列加入列表
            int Right = ColIndex + ColCount - 1; //同一大标题下的最后一列的索引
            SpanRows[ColIndex] = new SpanInfo(Text, 1, ColIndex, Right); //添加标题下的最左列
            SpanRows[Right] = new SpanInfo(Text, 3, ColIndex, Right); //添加该标题下的最右列
            for (int i = ColIndex + 1; i < Right; i++) //中间的列
            {
                SpanRows[i] = new SpanInfo(Text, 2, ColIndex, Right);
            }
        }
        /// <summary>
        /// 清除合并的列
        /// </summary>
        public void ClearSpanInfo()
        {
            SpanRows.Clear();
            //ReDrawHead();
        }
        private void DataGridViewEx_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)// && e.Type == ScrollEventType.EndScroll)
            {
                timer1.Enabled = false; timer1.Enabled = true;
            }
        }
        //刷新显示表头
        public void ReDrawHead()
        {
            foreach (int si in SpanRows.Keys)
            {
                this.Invalidate(this.GetCellDisplayRectangle(si, -1, true));
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ReDrawHead();
        }
        /// <summary>
        /// 二维表头的背景颜色

        /// </summary>
        [Description("二维表头的背景颜色"), Browsable(true), Category("二维表头")]
        public Color MergeColumnHeaderBackColor
        {
            get { return this._mergecolumnheaderbackcolor; }
            set { this._mergecolumnheaderbackcolor = value; }
        }
        private Color _mergecolumnheaderbackcolor = System.Drawing.SystemColors.Control;
        #endregion

        #region 右键菜单
        private void toolExportExcelMenuItem_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0) return;

            string filename = LXMS.clsExport.GetExportFilePath(0);
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(filename) == false)
            {
                LXMS.clsExport exp;
                exp = new LXMS.clsExport(this.Text, filename, 0, this);
                bool ret = exp.ExportGrid(out Util.emsg);
                if (!ret)
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }
        
        //private void ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.RowCount == 0) return;

        //    string filename = LXMS.clsExport.GetExportFilePath(4);
        //    this.Cursor = Cursors.WaitCursor;
        //    if (string.IsNullOrEmpty(filename) == false)
        //    {
        //        LXMS.clsExport exp;
        //        exp = new LXMS.clsExport(this.Text, filename, 4, this);
        //        bool ret = exp.ExportGrid(out Util.emsg);
        //        if (!ret)
        //            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    this.Cursor = Cursors.Default;
        //}

        private void toolExportOOMenuItem_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0) return;
            string filename = LXMS.clsExport.GetExportFilePath(1);
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(filename) == false)
            {
                LXMS.clsExport exp;
                exp = new LXMS.clsExport(this.Text, filename, 1, this);
                bool ret = exp.ExportGrid(out Util.emsg);
                if (!ret)
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolExportTextMenuItem_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0) return;
            string filename = LXMS.clsExport.GetExportFilePath(2);
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(filename) == false)
            {
                LXMS.clsExport exp;
                exp = new LXMS.clsExport(this.Text, filename, 2, this);
                bool ret = exp.ExportGrid(out Util.emsg);
                if (!ret)
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolExportPdfMenuItem_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0) return;
            string filename = LXMS.clsExport.GetExportFilePath(3);
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(filename) == false)
            {
                LXMS.clsExport exp;
                exp = new LXMS.clsExport(this.Text, filename, 3, this);
                bool ret = exp.ExportGrid(out Util.emsg);
                if (!ret)
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }
        
        private void toolHideNullColumn_Click(object sender, EventArgs e)
        {
            if (this.ColumnCount == 0) return;

            for (int j = 0; j < this.ColumnCount; j++)
            {
                if (this.Columns[j].Visible)
                {
                    bool empty = true;
                    for (int i = 0; i < this.RowCount; i++)
                    {
                        if (this.Rows[i].Visible && this.Rows[i].Cells[j].Value != null && !string.IsNullOrEmpty(this.Rows[i].Cells[j].Value.ToString()))
                        {
                            empty = false;
                            break;
                        }
                    }
                    if (empty)
                        this.Columns[j].Visible = false;
                }
            }
        }
        
        private void toolColsVisible_Click(object sender, EventArgs e)
        {
            if (this.Columns.Count == 0) return;
            if (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString()))
            {
                MessageBox.Show(clsTranslate.TranslateString("tag is null,can not get the grid name!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmGridColVisible frm = new frmGridColVisible();
            string cols = string.Empty;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (i == 0)
                    cols = this.Columns[i].Name;
                else
                    cols += "," + this.Columns[i].Name;
            }
            frm.InitColumn(this.Tag.ToString(), cols);
            if (frm.ShowDialog() == DialogResult.OK)
                RefreshVisible();
        }
        private void RowMergeView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (IsShowButtonColumn(this.Columns[this.CurrentCell.ColumnIndex].Name, _ShowButtonColumns))
            {
                Point p = new Point();

                if (this.button1.Height != this.Rows[this.CurrentCell.RowIndex].Height)
                {
                    this.button1.Height = this.Rows[this.CurrentCell.RowIndex].Height;
                }

                #region 获取X轴的位置
                if (this.RowHeadersVisible)
                {
                    //判断该类是否包含行标题,如果该列包含行标题，按钮的横坐标位置等于当前位置加上行标题  
                    p.X += this.RowHeadersWidth;
                }
                //FirstDisplayedCell表示左上角第一个单元格  
                for (int i = this.FirstDisplayedCell.ColumnIndex; i <= this.CurrentCell.ColumnIndex; i++)
                {
                    if (this.Columns[i].Visible)
                    {
                        //当前位置=单元格的宽度加上分隔符发宽度  
                        p.X += this.Columns[i].Width + this.Columns[i].DividerWidth;
                    }
                }

                p.X -= this.FirstDisplayedScrollingColumnHiddenWidth;
                p.X -= this.button1.Width;
                #endregion

                #region 获取Y轴位置

                if (this.ColumnHeadersVisible)
                {
                    //如果列表题可见，按钮的初始纵坐标位置等于当前位置加上列标题  
                    p.Y += this.ColumnHeadersHeight;
                }

                //获取或设置某一列的索引，该列是显示在 DataGridView 上的第一列  
                for (int i = this.FirstDisplayedScrollingRowIndex; i < this.CurrentCell.RowIndex; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        p.Y += this.Rows[i].Height + this.Rows[i].DividerHeight;
                    }
                }

                #endregion

                this.button1.Location = p;
                this.button1.Visible = true;
            }
            else
            {
                this.button1.Visible = false;
            }  
        }

        private void RowMergeView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if(CurrentCell!=null)
            //    this.CurrentCell.Style.ForeColor = Color.DarkCyan;
        }

        private void RowMergeView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {            
            StatusLabel1.Text = clsTranslate.TranslateString("Records: ") + this.Rows.Count;
        }

        private void RowMergeView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                this.RowHeadersWidth - 4,
                e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.RowHeadersDefaultCellStyle.Font,
                rectangle,
                this.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void toolMathSum_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            decimal total = 0;
            if (this.SelectedCells.Count > 1)
            {
                for (int i = 0; i < this.SelectedCells.Count; i++)
                {
                    if (this.SelectedCells[i].Visible)
                    {
                        if (this.SelectedCells[i].Value != null && !string.IsNullOrEmpty((this.SelectedCells[i].Value.ToString())) && Util.IsNumeric(this.SelectedCells[i].Value.ToString().Replace(",", "")))
                        {
                            total += Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace(",", ""));
                        }
                    }
                }
            }
            else
            {
                if (this.CurrentCell == null) return;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null && !string.IsNullOrEmpty((this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString())) && Util.IsNumeric(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace(",", "")))
                        {
                            total += Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace(",", ""));
                        }
                    }
                }
            }
            MessageBox.Show(total.ToString(), "Sum", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolMathAvg_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            decimal total = 0;
            int cnt = 0;
            if (this.SelectedCells.Count > 1)
            {
                for (int i = 0; i < this.SelectedCells.Count; i++)
                {
                    if (this.SelectedCells[i].Visible)
                    {
                        cnt++;
                        if (this.SelectedCells[i].Value != null && !string.IsNullOrEmpty((this.SelectedCells[i].Value.ToString())) && Util.IsNumeric(this.SelectedCells[i].Value.ToString().Replace("%", "")))
                        {
                            total += Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace("%", ""));
                        }
                    }
                }
            }
            else
            {
                if (this.CurrentCell == null) return;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        cnt++;
                        if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null && !string.IsNullOrEmpty((this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString())) && Util.IsNumeric(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "")))
                        {
                            total += Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", ""));
                        }
                    }
                }
            }
            if (cnt == 0)
                cnt = 1;
            total = Decimal.Round(total / cnt, 2);
            MessageBox.Show(total.ToString(), "Avg", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolMathMax_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            decimal max = 0;
            if (this.SelectedCells.Count > 1)
            {
                for (int i = 0; i < this.SelectedCells.Count; i++)
                {
                    if (this.SelectedCells[i].Visible)
                    {
                        if (max < Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace("%", "")))
                            max = Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace("%", ""));
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null && !string.IsNullOrEmpty((this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString())) && Util.IsNumeric(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "")))
                        {
                            if (max < Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "")))
                                max = Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", ""));
                        }
                    }
                }
            }
            MessageBox.Show(max.ToString(), "Max", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolMathMin_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            decimal min = 0;
            if (this.SelectedCells.Count > 1)
            {
                for (int i = 0; i < this.SelectedCells.Count; i++)
                {
                    if (this.SelectedCells[i].Visible)
                    {
                        if (min == 0 || min > Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace("%", "")))
                            min = Convert.ToDecimal(this.SelectedCells[i].Value.ToString().Replace("%", ""));
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Visible)
                    {
                        if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null && !string.IsNullOrEmpty((this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString())) && Util.IsNumeric(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "")))
                        {
                            if (min == 0 || min > Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "")))
                                min = Convert.ToDecimal(this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", ""));
                        }
                    }
                }
            }
            MessageBox.Show(min.ToString(), "Min", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        private void toolRowCount_Click(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            int rowcount = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                if (this.Rows[i].Visible)
                {
                    rowcount++;
                }
            }
            MessageBox.Show(rowcount.ToString(), clsTranslate.TranslateString("Row Count"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ToolStripMenuItem_Click_4(object sender, EventArgs e)
        {
            if (this.RowCount == 0)
                return;

            int rowcount = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                if (this.Rows[i].Visible)
                {
                    rowcount++;
                }
            }
            MessageBox.Show(rowcount.ToString(), clsTranslate.TranslateString("Row Count"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RowMergeView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //string errorText = string.Empty;
            //errorText += "Error，Location，Row:" + (e.RowIndex + 1).ToString() + "，Col" + (e.ColumnIndex + 1).ToString();
            //this.Rows[e.RowIndex].ErrorText = errorText;
            e.Cancel = true;
        }

        private void RowMergeView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void RowMergeView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _rowChanged = true;
        }

        public bool RowChanged
        {
            get { return _rowChanged; }
            set { _rowChanged = value; }
        }
        
        private void RowMergeView_DataSourceChanged(object sender, EventArgs e)
        {
            clsTranslate.TranslateDataGridView(this);
            RefreshVisible();
            _listgrv.Clear();
            toolStepNext.Enabled = false;
            toolStepPrevious.Enabled = false;
            GetVisibleList();
            this.MultiSelect = true;
            this.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            this.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            string fontsize = ini.IniReadValue("DBGRID", "FONT_SIZE");
            if (!string.IsNullOrEmpty(fontsize) && Util.IsNumeric(fontsize))
                this.RowsDefaultCellStyle.Font = new Font("宋体", Convert.ToSingle(fontsize));
            else
                this.RowsDefaultCellStyle.Font = new Font("宋体", Convert.ToSingle(11));
        }

        private void RefreshVisible()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.Tag != null && !string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    INIClass ini = new INIClass(Util.INI_FILE);
                    string cols = ini.IniReadValue("GRID_COLUMN", this.Tag.ToString());
                    if (!string.IsNullOrEmpty(cols))
                    {
                        string[] col = cols.Split(',');
                        for (int k = 0; k < this.Columns.Count; k++)
                        {
                            Columns[k].Visible = false;
                            for (int i = 0; i < col.Length; i++)
                            {
                                if (col[i].ToLower() == this.Columns[k].Name.ToLower())
                                {
                                    Columns[k].Visible = true;
                                    break;
                                }
                            }
                        }
                        this.Refresh();
                    }
                    string widths = ini.IniReadValue("GRID_WIDTH", this.Tag.ToString());
                    if (!string.IsNullOrEmpty(widths))
                    {
                        string[] wid = widths.Split(';');
                        for (int k = 0; k < this.Columns.Count; k++)
                        {
                            if (this.Columns[k].Visible)
                            {
                                for (int i = 0; i < wid.Length; i++)
                                {
                                    string[] temp = wid[i].Split(',');
                                    if (temp[0].ToLower() == this.Columns[k].Name.ToLower())
                                    {
                                        Columns[k].Width = Convert.ToInt32(temp[1]);
                                        break;
                                    }
                                }
                            }
                        }
                        this.Refresh();
                    }
                    else
                        Util.AutoSetColWidth(3, this, false);
                }
                else
                    Util.AutoSetColWidth(3, this, false);

                if (this.RowCount > 0 && this.Rows[0].Cells[0].Visible)
                    this.CurrentCell = this.Rows[0].Cells[0];
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MessageFilter()
        {
            if (this.RowCount == 0) return;

            int cnt = 0;
            for (int i = 0; i < this.RowCount; i++)
            {
                if (this.Rows[i].Visible)
                    cnt++;
            }

            MessageBox.Show(cnt.ToString() + clsTranslate.TranslateString(" Rows is filtered!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolFilterLike_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell == null) return;
            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString();
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString();
                        if (temp.IndexOf(cellvalue) < 0)
                            this.Rows[i].Visible = false;
                    }
                }
            }
            GetVisibleList();
        }

        private void toolFilterEqual_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell==null) return;

            DataGridViewCell curCell = this.CurrentCell;
            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;                    
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString();
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString();
                        if (temp.CompareTo(cellvalue) != 0)
                            this.Rows[i].Visible = false;
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
            if (curCell != null && curCell.Visible)
                this.CurrentCell = curCell;

        }

        private void toolFilterNotEqual_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell == null) return;

            if (this.CurrentCell.Value == null)
            {
                int col_index = this.CurrentCell.ColumnIndex;
                this.CurrentCell = null;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[col_index].Value == null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString();
                int col_index = this.CurrentCell.ColumnIndex;
                this.CurrentCell = null;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[col_index].Value == null)
                        this.Rows[i].Visible = true;
                    else
                    {
                        string temp = this.Rows[i].Cells[col_index].Value.ToString();
                        if (temp.CompareTo(cellvalue) == 0)
                            this.Rows[i].Visible = false;
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
        }

        private void toolFilterLarger_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell==null) return;

            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString().Replace("%", "");
                int col_index = this.CurrentCell.ColumnIndex;
                this.CurrentCell = null;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[col_index].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[col_index].Value.ToString().Replace("%", "");
                        if (Util.IsNumeric(cellvalue) && Util.IsNumeric(temp))
                        {
                            if (Convert.ToDecimal(temp) <= Convert.ToDecimal(cellvalue))
                                this.Rows[i].Visible = false;
                        }
                        else
                        {
                            if (temp.CompareTo(cellvalue) <= 0)
                                this.Rows[i].Visible = false;
                        }
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
        }

        private void toolFilterEqualLarger_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell == null) return;

            DataGridViewCell curCell = this.CurrentCell;
            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString().Replace("%", "");
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "");
                        if (Util.IsNumeric(cellvalue) && Util.IsNumeric(temp))
                        {
                            if (Convert.ToDecimal(temp) < Convert.ToDecimal(cellvalue))
                                this.Rows[i].Visible = false;
                        }
                        else
                        {
                            if (temp.CompareTo(cellvalue) < 0)
                                this.Rows[i].Visible = false;
                        }
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
            if (curCell != null && curCell.Visible)
                this.CurrentCell = curCell;
        }

        private void toolFilterLess_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell==null) return;
            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString().Replace("%", "");
                int col_index = this.CurrentCell.ColumnIndex;
                this.CurrentCell = null;
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[col_index].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[col_index].Value.ToString().Replace("%", "");
                        if (Util.IsNumeric(cellvalue) && Util.IsNumeric(temp))
                        {
                            if (Convert.ToDecimal(temp) >= Convert.ToDecimal(cellvalue))
                                this.Rows[i].Visible = false;
                        }
                        else
                        {
                            if (temp.CompareTo(cellvalue) >= 0)
                                this.Rows[i].Visible = false;
                        }
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
        }

        private void toolFilterEqualLess_Click(object sender, EventArgs e)
        {
            if (this.CurrentCell == null) return;

            DataGridViewCell curCell = this.CurrentCell;
            if (this.CurrentCell.Value == null)
            {
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value != null)
                        this.Rows[i].Visible = false;
                }
            }
            else
            {
                string cellvalue = this.CurrentCell.Value.ToString().Replace("%", "");
                for (int i = 0; i < this.RowCount; i++)
                {
                    if (this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value == null)
                        this.Rows[i].Visible = false;
                    else
                    {
                        string temp = this.Rows[i].Cells[this.CurrentCell.ColumnIndex].Value.ToString().Replace("%", "");
                        if (Util.IsNumeric(cellvalue) && Util.IsNumeric(temp))
                        {
                            if (Convert.ToDecimal(temp) > Convert.ToDecimal(cellvalue))
                                this.Rows[i].Visible = false;
                        }
                        else
                        {
                            if (temp.CompareTo(cellvalue) > 0)
                                this.Rows[i].Visible = false;
                        }
                    }
                }
            }
            GetVisibleList();
            MessageFilter();
            if (curCell != null && curCell.Visible)
                this.CurrentCell = curCell;
        }         

        private void toolRefresh_Click(object sender, EventArgs e)
        {            
            if (this.RowCount == 0) return;
            for (int i = 0; i < this.RowCount; i++)
            {
                this.Rows[i].Visible = true;
            }
            _listgrv.Clear();
            toolStepNext.Enabled = false;
            toolStepPrevious.Enabled = false;
            GetVisibleList();
        }

        private void GetVisibleList()
        {
            if (this.RowCount == 0) return;

            if (_listgrv.Count - 1 > _curstep)
            {
                for (int i = _listgrv.Count - 1; i > _curstep ; i--)
                    _listgrv.RemoveAt(i);
            }
            int stepid=0;
            if(_listgrv!=null)
                stepid=_listgrv.Count;

            string RowList = string.Empty;
            for (int i = 0; i < this.RowCount; i++)
            {
                if (i == 0)
                    RowList = i.ToString().Trim() + "," + (this.Rows[i].Visible ? "1" : "0");
                else
                    RowList += ";" + i.ToString().Trim() + "," + (this.Rows[i].Visible ? "1" : "0");
            }
            modGridRowVisible mod = new modGridRowVisible();
            mod.Stepid = stepid;
            mod.RowList = RowList;            
            _listgrv.Add(mod);
            _curstep = stepid;
            this.toolStepNext.Enabled = false;
            if (_curstep > 0)
                toolStepPrevious.Enabled = true;

        }

        private void SetVisibleList()
        {
            if (this.RowCount == 0) return;

            if (_curstep >= 0 && _curstep < _listgrv.Count)
            {
                this.CurrentCell = null;
                string RowList = _listgrv[_curstep].RowList;
                string[] rows = RowList.Split(';');
                for (int i = 0; i < rows.Length; i++)
                {                    
                    string[] rowdata = rows[i].Split(',');
                    {
                        int rowinx = Convert.ToInt32(rowdata[0]);
                        
                        if (rowinx < this.RowCount)
                        {
                            this.Rows[rowinx].Visible = Convert.ToInt32(rowdata[1]) == 1 ? true : false;
                        }
                    }                    
                }
            }
        }

        private void toolStepNext_Click(object sender, EventArgs e)
        {
            //next step
            DataGridViewCell curCell = null;
            if(this.CurrentCell!=null)
                curCell = this.CurrentCell;

            if (_curstep < _listgrv.Count - 1)
            {
                _curstep++;
                SetVisibleList();
                if (_curstep == _listgrv.Count - 1)
                    toolStepNext.Enabled = false;
                if (_curstep > 0)
                    toolStepPrevious.Enabled = true;
            }
            MessageFilter();
            if (curCell != null && curCell.Visible)
                this.CurrentCell = curCell;
        }

        private void toolStepPrevious_Click(object sender, EventArgs e)
        {
            DataGridViewCell curCell = null;
            if (this.CurrentCell != null)
                curCell = this.CurrentCell;
            //previous step
            if (_curstep > 0)
            {
                _curstep--;
                SetVisibleList();
                if (_curstep == 0)
                    toolStepPrevious.Enabled = false;
                if (_curstep < _listgrv.Count - 1)
                    toolStepNext.Enabled = true;
            }
            MessageFilter();
            if (curCell != null && curCell.Visible)
                this.CurrentCell = curCell;
        }

        private void toolFrozen_Click(object sender, EventArgs e)
        {
            //Frozen
            int iFrozen = this.CurrentCell.ColumnIndex;
            if (iFrozen < this.ColumnCount - 1)
            {
                for (int i = this.ColumnCount - 1; i >= 0; i--)
                {
                    if (this.Columns[i].Frozen)
                    {
                        this.Columns[i].Frozen = false;
                        this.Columns[i].DefaultCellStyle.BackColor = this.DefaultCellStyle.BackColor;
                    }
                }
                this.Refresh();

                this.Columns[iFrozen].Frozen = true;
                for (int i = 0; i <= iFrozen; i++)
                {
                    this.Columns[i].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                }
                this.Refresh();                
            }
        }

        public void toolCancelFrozen_Click(object sender, EventArgs e)
        {            
            for (int i = this.ColumnCount - 1; i >= 0; i--)
            {
                if (this.Columns[i].Frozen)
                {
                    this.Columns[i].Frozen = false;
                    this.Columns[i].DefaultCellStyle.BackColor = this.DefaultCellStyle.BackColor;                    
                }
            }
            this.Refresh();            
        }

        private void toolDisableAlternateColor_Click(object sender, EventArgs e)
        {
            this.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
        }

        private void ToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
            this.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR; ;
        }

        #endregion

        private void RowMergeView_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                if (this.Rows.Count >= 2)
                {
                    int h = e.Row.Height;
                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        this.Rows[i].Height = h;
                    }                    
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        private void RowMergeView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (this.Tag == null) return;

            INIClass ini = new INIClass(Util.INI_FILE);
            string widths = string.Empty;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (i == 0)
                    widths = this.Columns[i].Name.ToLower() + "," + this.Columns[i].Width.ToString();
                else
                    widths += ";" + this.Columns[i].Name.ToLower() + "," + this.Columns[i].Width.ToString();
            }

            ini.IniWriteValue("GRID_WIDTH", this.Tag.ToString(), widths);
        }

        private void toolBackColor_Click(object sender, EventArgs e)
        {
            if (this.Rows.Count==0) return;

            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (this.SelectedRows.Count == 0)
                    this.CurrentRow.DefaultCellStyle.BackColor = dlg.Color;
                else
                {
                    for (int i = 0; i < this.SelectedRows.Count; i++)
                    {
                        this.SelectedRows[i].DefaultCellStyle.BackColor = dlg.Color;
                    }
                }
            }
        }

        private void toolForeColor_Click(object sender, EventArgs e)
        {
            if (this.Rows.Count == 0) return;

            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (this.SelectedRows.Count == 0)
                    this.CurrentRow.DefaultCellStyle.ForeColor = dlg.Color;
                else
                {
                    for (int i = 0; i < this.SelectedRows.Count; i++)
                    {
                        this.SelectedRows[i].DefaultCellStyle.ForeColor = dlg.Color;
                    }
                }
            }
        }

        private void toolFind_Click(object sender, EventArgs e)
        {
            frmInputBox frm = new frmInputBox(clsTranslate.TranslateString("Please input find content"), string.Empty);
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(Util.retValue1))
            {
                if (this.CurrentRow == null) return;

                for (int i = 0; i < this.Rows.Count; i++)
                {
                    this.Rows[i].Visible = true;
                }

                int startindex = 0;
                if (this.CurrentRow.Index < this.RowCount - 1)
                    startindex = this.CurrentRow.Index + 1;

                string[] finds = Util.retValue1.ToUpper().Split('*');                
                bool find = false;
                for (int i = startindex; i < this.Rows.Count; i++)
                {                      
                    for (int j = 0; j < this.ColumnCount; j++)
                    {
                        if (this.Rows[i].Cells[j].Visible && this.Rows[i].Cells[j].Value != null)
                        {
                            bool found = true;
                            for (int k = 0; k < finds.Length; k++)
                            {
                                if (this.Rows[i].Cells[j].Value.ToString().ToUpper().IndexOf(finds[k].ToUpper()) < 0)
                                {
                                    found = false;
                                }
                            }
                            if (found)
                            {
                                this.CurrentCell = this.Rows[i].Cells[j];
                                find = true;
                                return;
                            }
                        }
                    }                        
                }
                if (!find)
                {
                    for (int i = 0; i < this.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.ColumnCount; j++)
                        {
                            if (this.Rows[i].Cells[j].Visible && this.Rows[i].Cells[j].Value != null)
                            {
                                bool found = true;
                                for (int k = 0; k < finds.Length; k++)
                                {
                                    if (this.Rows[i].Cells[j].Value.ToString().ToUpper().IndexOf(finds[k].ToUpper()) < 0)
                                    {
                                        found = false;
                                    }
                                }
                                if (found)
                                {
                                    this.CurrentCell = this.Rows[i].Cells[j];
                                    find = true;
                                    return;
                                }
                            }
                        }
                    }                    
                }
            }
        }

        private void RowMergeView_KeyDown(object sender, KeyEventArgs e)
        {
            string tempstr = string.Empty;            
            if (e.Modifiers.CompareTo(Keys.Control) == 0)
            {
                switch (e.KeyCode)
                {
                    //case Keys.C:
                    //    toolCopy_Click(null, null);
                    //    break;
                    case Keys.D:
                        frmInputBox frm = new frmInputBox(clsTranslate.TranslateString("Please input new value:"),string.Empty);
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if (this.SelectedCells.Count <= 1)
                            {
                                if (this.CurrentCell != null)
                                    this.CurrentCell.Value = Util.retValue1;
                            }
                            else
                            {
                                for (int i = 0; i < this.SelectedCells.Count; i++)
                                {
                                    if (this.SelectedCells[i].ReadOnly == false)
                                        this.SelectedCells[i].Value = Util.retValue1;
                                }
                            }
                        }
                        break;
                    case Keys.V:
                        tempstr = Clipboard.GetText();
                        if (string.IsNullOrEmpty(tempstr)) return;
                        if (this.SelectedCells.Count <= 1)
                        {
                            if (this.CurrentCell != null)
                                this.CurrentCell.Value = tempstr;
                        }
                        else
                        {
                            for (int i = 0; i < this.SelectedCells.Count; i++)
                            {
                                if (this.SelectedCells[i].ReadOnly == false)
                                    this.SelectedCells[i].Value = tempstr;
                            }
                        }
                        break;
                    case Keys.F:
                        toolFind_Click(null, null);
                        break;                                                                          
                    default:
                        break;
                }
            }
            if (e.KeyCode == Keys.F1)
            {
                //toolColsVisible_Click(null, null);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Ctrl+C:  复制 (Copy)");
                sb.AppendLine("Ctrl+V:  粘贴 (Pase)");
                sb.AppendLine("Ctrl+D:  更改内容");
                sb.AppendLine("Ctrl+F:  查找 (Find)");
                sb.AppendLine("");
                sb.AppendLine("F1:      查看快捷键");
                sb.AppendLine("F2:      导出Excel");
                sb.AppendLine("F3:      查找 (Find)");
                sb.AppendLine("F4:      设置可见列");
                sb.AppendLine("F5:      合计 (Sum)");
                sb.AppendLine("F6:      冻结列 (Frozen)");
                sb.AppendLine("F7:      取消冻结 (Cancel Frozen)");
                sb.AppendLine("F8:      刷新 (Refresh)");
                sb.AppendLine("F10:     筛选等于 (=)");
                sb.AppendLine("F11:     上一步");
                sb.AppendLine("F12:     下一步");
                frmViewText frm = new frmViewText(sb.ToString());
                frm.ShowDialog();
            }
            else if (e.KeyCode == Keys.F2)
                toolExportExcelMenuItem_Click(null, null);
            else if (e.KeyCode == Keys.F3)
                toolFind_Click(null, null);
            else if (e.KeyCode == Keys.F4)
                toolColsVisible_Click(null, null);
            else if (e.KeyCode == Keys.F5)
                toolMathSum_Click(null, null);
            else if (e.KeyCode == Keys.F6)
                toolFrozen_Click(null, null);
            else if (e.KeyCode == Keys.F7)
                toolCancelFrozen_Click(null, null);
            else if (e.KeyCode == Keys.F8)
                toolRefresh_Click(null, null);
            else if (e.KeyCode == Keys.F10)
                toolFilterEqual_Click(null, null);
            else if (e.KeyCode == Keys.F11)
                toolStepPrevious_Click(null, null);
            else if (e.KeyCode == Keys.F12)
                toolStepNext_Click(null, null);
        }

        private void toolCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(this.GetClipboardContent());
            }
            catch (Exception ex)
            {
            }
        }

        private void toolFontSize_Click(object sender, EventArgs e)
        {
            string fontsize = "10";
            if (this.RowsDefaultCellStyle.Font != null)
                fontsize = this.RowsDefaultCellStyle.Font.Size.ToString();
            frmInputBox frm = new frmInputBox("Please input font size", fontsize);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(Util.retValue1) && Util.IsNumeric(Util.retValue1))
                {
                    ini.IniWriteValue("DBGRID", "FONT_SIZE", Util.retValue1);
                    this.RowsDefaultCellStyle.Font = new Font("宋体", Convert.ToSingle(Util.retValue1));
                    this.Refresh();
                }
            }
        }

        private void RowMergeView_Scroll(object sender, ScrollEventArgs e)
        {
            this.button1.Visible = false;
        }

    }
    
    [Serializable]
    public class modGridRowVisible
    {
        int? _stepid;        
        string _rowlist;

        public modGridRowVisible() { }

        public modGridRowVisible(int? stepid, string row_list)
        {
            //Convert.ToInt32(rdr[0]),Convert.ToInt32(rdr[1]),Convert.ToString(rdr[2])
            //dalUtility.ConvertToInt(rdr[0]),dalUtility.ConvertToInt(rdr[1]),dalUtility.ConvertToString(rdr[2])
            //dalUtility.ConvertToInt(rdr["stepid"]),dalUtility.ConvertToInt(rdr["rowid"]),dalUtility.ConvertToString(rdr["visible"])
            //stepid,rowid,visible
            _stepid = stepid;
            _rowlist = row_list;            
        }

        public int? Stepid
        {
            get { return _stepid; }
            set { _stepid = value; }
        }
        public string RowList
        {
            get { return _rowlist; }
            set { _rowlist = value; }
        }        
    }
}