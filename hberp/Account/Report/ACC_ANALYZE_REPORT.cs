using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_ANALYZE_REPORT : Form
    {
        public ACC_ANALYZE_REPORT()
        {
            InitializeComponent();            
        }

        private void ACC_ANALYZE_REPORT_Shown(object sender, EventArgs e)
        {
            int dis = 240;
            splitProfit.SplitterDistance = dis;
            splitSales.SplitterDistance = dis;
            splitSalesDetail.SplitterDistance = dis;
            splitPurchase.SplitterDistance = dis;
            splitWaste.SplitterDistance = dis;
            splitProduct.SplitterDistance = dis;
            splitProductDetail.SplitterDistance = dis;
            btnReload1.Left = splitProfit.Width - btnReload1.Width - 10;
            btnReload2.Left = splitProfit.Width - btnReload2.Width - 10;
            btnReload3.Left = splitProfit.Width - btnReload3.Width - 10;
            btnReload4.Left = splitProfit.Width - btnReload4.Width - 10;
            btnReload5.Left = splitProfit.Width - btnReload5.Width - 10;            
        }

        private void ACC_ANALYZE_REPORT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);            
            txtYear1.Text = DateTime.Today.Year.ToString();
            rbMonth1.Checked = true;
            rbMonth1_CheckedChanged(null, null);
            chart1.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid1.MultiSelect = true;
            DBGrid1.ContextMenuStrip.Items.Add("-");
            DBGrid1.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart1_Click));

            cboShow2.SelectedIndex = 2;
            txtYear2.Text = DateTime.Today.Year.ToString();            
            rbMonth2.Checked = true;
            rbMonth2_CheckedChanged(null, null);            
            chart2.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid2.MultiSelect = true;
            DBGrid2.ContextMenuStrip.Items.Add("-");
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart2_Click));

            FillControl.FillPeriodList(cboAccName2A);
            if (cboAccName2A.Items.Count > 1)
                cboAccName2A.SelectedIndex = 1;
            chart2A.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid2A.MultiSelect = true;
            DBGrid2A.ContextMenuStrip.Items.Add("-");
            DBGrid2A.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart2A_Click));
            DBGrid2A.ContextMenuStrip.Items.Add("-");
            DBGrid2A.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Detail"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuDetail2A_Click));

            txtYear3.Text = DateTime.Today.Year.ToString();
            rbMonth3.Checked = true;
            rbMonth3_CheckedChanged(null, null);
            chart3.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid3.MultiSelect = true;
            DBGrid3.ContextMenuStrip.Items.Add("-");
            DBGrid3.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart3_Click));

            txtYear4.Text = DateTime.Today.Year.ToString();
            rbMonth4.Checked = true;
            rbMonth4_CheckedChanged(null, null);
            chart4.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid4.MultiSelect = true;
            DBGrid4.ContextMenuStrip.Items.Add("-");
            DBGrid4.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart4_Click));

            txtYear5.Text = DateTime.Today.Year.ToString();
            rbMonth5.Checked = true;
            rbMonth5_CheckedChanged(null, null);
            chart5.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid5.MultiSelect = true;
            DBGrid5.ContextMenuStrip.Items.Add("-");
            DBGrid5.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart5_Click));

            FillControl.FillPeriodList(cboAccName6);
            if (cboAccName6.Items.Count > 1)
                cboAccName6.SelectedIndex = 1;
            chart6.ChartAreas[0].BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid6.MultiSelect = true;
            DBGrid6.ContextMenuStrip.Items.Add("-");
            DBGrid6.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Refresh Chart"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuRefreshChart6_Click));
            DBGrid6.ContextMenuStrip.Items.Add("-");
            DBGrid6.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Detail"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuDetail6_Click));
        }

        #region Profit Analyze
        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbMonth1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth1.Checked)
                txtYear1.Visible = true;
            else
                txtYear1.Visible = false;
            InitGrid1();
            LoadData1();
        }

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            LoadData1();
        }

        private void btnReload1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeProfit dal = new dalAccAnalyzeProfit();
                bool ret = dal.Generate(modp.AccName, Util.IsTrialBalance, out Util.emsg);
                if (ret)
                {
                    InitGrid1();
                    LoadData1();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitGrid1()
        {
            DBGrid1.Rows.Clear();
            DBGrid1.Columns.Clear();
            DBGrid1.ReadOnly = true;
            DBGrid1.AllowUserToAddRows = false;
            DBGrid1.AllowUserToDeleteRows = false;
            DBGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("SubjectId");
            col.DataPropertyName = "SubjectId";
            col.Name = "SubjectId";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid1.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("SubjectName");
            col.DataPropertyName = "SubjectName";
            col.Name = "SubjectName";
            col.Width = 140;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid1.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("AdFlag");
            col.DataPropertyName = "AdFlag";
            col.Name = "AdFlag";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;            
            DBGrid1.Columns.Add(col);
            col.Dispose();

            DBGrid1.Columns[1].Frozen = true;

            if (rbMonth1.Checked)
            {
                for (int i = 1; i < 13; i++)
                {
                    string coltitle = i.ToString().Trim() + "月";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString(coltitle);
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid1.Columns.Add(col);
                    col.Dispose();
                }
            }
            else
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modf = dalp.GetItem(1, out Util.emsg);

                for (int i = modf.AccYear; i <= DateTime.Today.Year; i++)
                {
                    string coltitle=i.ToString() + "年";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = coltitle;
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid1.Columns.Add(col);
                    col.Dispose();
                }
            }

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("合计");
            col.DataPropertyName = "合计";
            col.Name = "合计";
            col.Width = 100;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid1.Columns.Add(col);
            col.Dispose();

            dalAccReport dal = new dalAccReport();
            BindingCollection<modAccProfitReport> list = dal.GetAccProfitReport(Util.modperiod.AccName, Util.IsTrialBalance, out Util.emsg);
            foreach (modAccProfitReport mod in list)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DBGrid1);
                row.Cells[0].Value = mod.SubjectId;
                row.Cells[1].Value = mod.SubjectName;
                row.Cells[2].Value = mod.AdFlag;
                if (mod.SubjectId == "合计")
                    row.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                DBGrid1.Rows.Add(row);
                row.Dispose();
            }
        }

        private void LoadData1()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                dalAccAnalyzeProfit dal = new dalAccAnalyzeProfit();
                if (rbMonth1.Checked)
                {
                    BindingCollection<modAnalyzeProfitMonth> list = dal.GetMonthlyReport(Convert.ToInt32(txtYear1.Text), out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeProfitMonth mod in list)
                        {
                            for(int i=0;i<DBGrid1.RowCount;i++)
                            {
                                if (mod.SubjectId == DBGrid1.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid1.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccMonth.ToString().Trim() == DBGrid1.Columns[j].Name.Replace("月","").Trim())
                                        {
                                            DBGrid1.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }                                
                            }
                        }
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }                    
                }
                else
                {
                    BindingCollection<modAnalyzeProfitYears> list = dal.GetYearsReport(out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeProfitYears mod in list)
                        {
                            for (int i = 0; i < DBGrid1.RowCount; i++)
                            {
                                if (mod.SubjectId == DBGrid1.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid1.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccYear.ToString().Trim() == DBGrid1.Columns[j].Name.Replace("年", "").Trim())
                                        {
                                            DBGrid1.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                for (int i = 0; i < DBGrid1.RowCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 3; j < DBGrid1.ColumnCount - 1; j++)
                    {
                        if (DBGrid1.Rows[i].Cells[j].Value != null)
                            sum += Convert.ToDecimal(DBGrid1.Rows[i].Cells[j].Value);
                    }
                    DBGrid1.Rows[i].Cells[DBGrid1.ColumnCount - 1].Value = sum;
                }

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart1.Series.Clear();
                for (int i = 0; i < DBGrid1.Rows.Count; i++)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid1.Rows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid1.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid1.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid1.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid1.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max!=0)
                    {
                        chart1.Series.Add(ser);
                    }                    
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart1.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        

        private void mnuRefreshChart1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid1.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart1.Series.Clear();                
                                
                for(int i=DBGrid1.SelectedRows.Count-1; i>=0; i--)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid1.SelectedRows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid1.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid1.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid1.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid1.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min == max && min == 0)
                    {
                        return;
                    }
                    chart1.Series.Add(ser);
                }
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart1.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid1.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart1.Series.Clear();
                Series ser = new Series();
                //ser.ChartType = SeriesChartType.Column;
                ser.LegendText = DBGrid1.CurrentRow.Cells[1].Value.ToString();
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 3; j < DBGrid1.Columns.Count - 1; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid1.CurrentRow.Cells[j].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid1.CurrentRow.Cells[j].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid1.Columns[j].HeaderText.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart1.Series.Add(ser);
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart1.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart1.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Sales Analyze
        private void rbMonth2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth2.Checked)
                txtYear2.Visible = true;
            else
                txtYear2.Visible = false;
            InitGrid2();
            LoadData2();
        }

        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void btnReload2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    InitGrid2();
                    LoadData2();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitGrid2()
        {
            DBGrid2.Rows.Clear();
            DBGrid2.Columns.Clear();
            DBGrid2.ReadOnly = true;
            DBGrid2.AllowUserToAddRows = false;
            DBGrid2.AllowUserToDeleteRows = false;
            DBGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBGrid2.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("CustId");
            col.DataPropertyName = "CustId";
            col.Name = "CustId";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid2.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("CustName");
            col.DataPropertyName = "CustName";
            col.Name = "CustName";
            col.Width = 140;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid2.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("AdFlag");
            col.DataPropertyName = "AdFlag";
            col.Name = "AdFlag";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid2.Columns.Add(col);
            col.Dispose();

            DBGrid2.Columns[1].Frozen = true;
            if (rbMonth2.Checked)
            {
                for (int i = 1; i < 13; i++)
                {
                    string coltitle = i.ToString().Trim() + "月";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString(coltitle);
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid2.Columns.Add(col);
                    col.Dispose();
                }
            }
            else
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modf = dalp.GetItem(1, out Util.emsg);

                for (int i = modf.AccYear; i <= DateTime.Today.Year; i++)
                {
                    string coltitle = i.ToString() + "年";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = coltitle;
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid2.Columns.Add(col);
                    col.Dispose();
                }
            }

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("合计");
            col.DataPropertyName = "合计";
            col.Name = "合计";
            col.Width = 100;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid2.Columns.Add(col);
            col.Dispose();

            dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
            if (rbMonth2.Checked)
            {
                Hashtable ht = dal.GetSalesCustomer(Convert.ToInt32(txtYear2.Text), out Util.emsg);
                if (ht != null && ht.Count > 0)
                {
                    foreach (DictionaryEntry item in ht)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid2);
                        row.Cells[0].Value = item.Key;
                        row.Cells[1].Value = item.Value;
                        row.Cells[2].Value = 1;
                        DBGrid2.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
            else
            {
                Hashtable ht = dal.GetSalesCustomer(out Util.emsg);
                if (ht != null && ht.Count > 0)
                {
                    foreach (DictionaryEntry item in ht)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid2);
                        row.Cells[0].Value = item.Key;
                        row.Cells[1].Value = item.Value;
                        row.Cells[2].Value = 1;
                        DBGrid2.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
        }

        private void LoadData2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
                if (rbMonth2.Checked)
                {
                    BindingCollection<modAnalyzeSalesMonth> list = dal.GetMonthlyReport(Convert.ToInt32(txtYear2.Text), out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeSalesMonth mod in list)
                        {
                            for (int i = 0; i < DBGrid2.RowCount; i++)
                            {
                                if (mod.CustId == DBGrid2.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid2.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccMonth.ToString().Trim() == DBGrid2.Columns[j].Name.Replace("月", "").Trim())
                                        {
                                            switch (cboShow2.Text)
                                            {
                                                case "销售额":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.SalesMny;
                                                    break;
                                                case "成本":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.CostMny;
                                                    break;
                                                case "利润":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.Profit;
                                                    break;
                                                case "利润率":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.ProfitRatio;
                                                    break;
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                else
                {
                    BindingCollection<modAnalyzeSalesYear> list = dal.GetYearsReport(out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeSalesYear mod in list)
                        {
                            for (int i = 0; i < DBGrid2.RowCount; i++)
                            {
                                if (mod.CustId == DBGrid2.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid2.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccYear.ToString().Trim() == DBGrid2.Columns[j].Name.Replace("年", "").Trim())
                                        {
                                            switch (cboShow2.Text)
                                            {
                                                case "销售额":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.SalesMny;
                                                    break;
                                                case "成本":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.CostMny;
                                                    break;
                                                case "利润":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.Profit;
                                                    break;
                                                case "利润率":
                                                    DBGrid2.Rows[i].Cells[j].Value = mod.ProfitRatio;
                                                    break;
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                for (int i = 0; i < DBGrid2.RowCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 3; j < DBGrid2.ColumnCount - 1; j++)
                    {
                        if (DBGrid2.Rows[i].Cells[j].Value != null)
                            sum += Convert.ToDecimal(DBGrid2.Rows[i].Cells[j].Value);
                    }
                    DBGrid2.Rows[i].Cells[DBGrid2.ColumnCount - 1].Value = sum;
                }

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2.Series.Clear();
                for (int i = 0; i < DBGrid2.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid2.Rows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid2.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid2.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid2.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart2.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart2.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart2.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart2.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Column;
                ser.LegendText = DBGrid2.CurrentRow.Cells[1].Value.ToString();
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 3; j < DBGrid2.Columns.Count - 1; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid2.CurrentRow.Cells[j].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2.CurrentRow.Cells[j].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid2.Columns[j].HeaderText.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart2.Series.Add(ser);
                chart2.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart2.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart2.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2.Series.Clear();

                for (int i = DBGrid2.SelectedRows.Count - 1; i >= 0; i--)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid2.SelectedRows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid2.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid2.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid2.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min == max && min == 0)
                    {
                        return;
                    }
                    chart2.Series.Add(ser);
                }
                chart2.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart2.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart2.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Sales Detail Analyze
        private void cboAccName2A_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData2A();
        }

        private void btnRefresh2A_Click(object sender, EventArgs e)
        {
            LoadData2A();
        }

        private void btnReload2A_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    LoadData2A();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadData2A()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string purchasetypelist = string.Empty;
                dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
                BindingCollection<modAnalyzeSalesMonth> list = dal.GetCustomerReport(cboAccName2A.SelectedValue.ToString(), out Util.emsg);
                DBGrid2A.DataSource = list;
                if (DBGrid2A.RowCount > 0)
                {
                    DBGrid2A.Columns["AccName"].Visible = false;
                    DBGrid2A.Columns["AccYear"].Visible = false;
                    DBGrid2A.Columns["AccMonth"].Visible = false;
                    DBGrid2A.Columns["CustId"].Visible = false;
                }
                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2A.Series.Clear();
                for (int i = 0; i < DBGrid2A.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Line;
                    ser.LegendText = DBGrid2A.Rows[i].Cells[4].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 5; j < DBGrid2A.Columns.Count; j++)
                    {
                        DBGrid2A.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        decimal cellvalue = 0;
                        if (DBGrid2A.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2A.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid2A.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart2A.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart2A.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart2A.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart2A.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2A.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid2A_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2A.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2A.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Line;
                ser.LegendText = DBGrid2A.Columns[DBGrid2A.CurrentCell.ColumnIndex].HeaderText;
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 0; j < DBGrid2A.RowCount; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid2A.Rows[j].Cells[DBGrid2A.CurrentCell.ColumnIndex].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2A.Rows[j].Cells[DBGrid2A.CurrentCell.ColumnIndex].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid2A.Rows[j].Cells[4].Value.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart2A.Series.Add(ser);
                chart2A.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart2A.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart2A.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2A.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart2A_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2A.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart2A.Series.Clear();
                for (int i = 0; i < DBGrid2A.SelectedRows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Line;
                    ser.LegendText = DBGrid2A.Rows[i].Cells[4].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 5; j < DBGrid2A.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid2A.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid2A.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid2A.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart2A.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart2A.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart2A.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart2A.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2A.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuDetail2A_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2A.CurrentRow == null) return;

                dalAccAnalyzeSales dal = new dalAccAnalyzeSales();
                BindingCollection<modAccAnalyzeSales> list = dal.GetAnalyzeSalesDetail(cboAccName2A.SelectedValue.ToString(), DBGrid2A.CurrentRow.Cells["CustId"].Value.ToString(), out Util.emsg);
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString(clsTranslate.TranslateString("Sales Profit Analyze")), list);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Purchase Analyze
        private void rbMonth3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth3.Checked)
                txtYear3.Visible = true;
            else
                txtYear3.Visible = false;
            InitGrid3();
            LoadData3();
        }

        private void btnRefresh3_Click(object sender, EventArgs e)
        {
            LoadData3();
        }

        private void btnReload3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzePurchase dal = new dalAccAnalyzePurchase();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    InitGrid3();
                    LoadData3();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitGrid3()
        {
            DBGrid3.Rows.Clear();
            DBGrid3.Columns.Clear();
            DBGrid3.ReadOnly = true;
            DBGrid3.AllowUserToAddRows = false;
            DBGrid3.AllowUserToDeleteRows = false;
            DBGrid3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBGrid3.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();            
            col.HeaderText = clsTranslate.TranslateString("VendorName");
            col.DataPropertyName = "VendorName";
            col.Name = "VendorName";
            col.Width = 140;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid3.Columns.Add(col);
            col.Dispose();

            DBGrid3.Columns[0].Frozen = true;

            if (rbMonth3.Checked)
            {
                for (int i = 1; i < 13; i++)
                {
                    string coltitle = i.ToString().Trim() + "月";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString(coltitle);
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid3.Columns.Add(col);
                    col.Dispose();
                }
            }
            else
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modf = dalp.GetItem(1, out Util.emsg);

                for (int i = modf.AccYear; i <= DateTime.Today.Year; i++)
                {
                    string coltitle = i.ToString() + "年";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = coltitle;
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid3.Columns.Add(col);
                    col.Dispose();
                }
            }

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("合计");
            col.DataPropertyName = "合计";
            col.Name = "合计";
            col.Width = 100;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid3.Columns.Add(col);
            col.Dispose();

            ArrayList arr = new ArrayList();
            dalAccAnalyzePurchase dal = new dalAccAnalyzePurchase();
            if (rbMonth3.Checked)
                arr = dal.GetPurchaseVendor(Convert.ToInt32(txtYear3.Text), out Util.emsg);
            else
                arr = dal.GetPurchaseVendor(out Util.emsg);
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(DBGrid3);
                    row.Cells[0].Value = arr[i];
                    DBGrid3.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void LoadData3()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string purchasetypelist = string.Empty;
                if (chkPurchase3.Checked)
                    purchasetypelist = "采购收货";

                if (chkReturn3.Checked)
                {
                    if (string.IsNullOrEmpty(purchasetypelist))
                        purchasetypelist = "采购退货";
                    else
                        purchasetypelist = "采购收货,采购退货";
                }

                dalAccAnalyzePurchase dal = new dalAccAnalyzePurchase();
                if (rbMonth3.Checked)
                {
                    BindingCollection<modAnalyzePurchaseMonth> list = dal.GetMonthlyReport(Convert.ToInt32(txtYear3.Text), purchasetypelist, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzePurchaseMonth mod in list)
                        {
                            for (int i = 0; i < DBGrid3.RowCount; i++)
                            {
                                if (mod.VendorName == DBGrid3.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 1; j < DBGrid3.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccMonth.ToString().Trim() == DBGrid3.Columns[j].Name.Replace("月", "").Trim())
                                        {
                                            DBGrid3.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                else
                {
                    BindingCollection<modAnalyzePurchaseYear> list = dal.GetYearsReport(purchasetypelist, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzePurchaseYear mod in list)
                        {
                            for (int i = 0; i < DBGrid3.RowCount; i++)
                            {
                                if (mod.VendorName == DBGrid3.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 1; j < DBGrid3.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccYear.ToString().Trim() == DBGrid3.Columns[j].Name.Replace("年", "").Trim())
                                        {
                                            DBGrid3.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                for (int i = 0; i < DBGrid3.RowCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 1; j < DBGrid3.ColumnCount - 1; j++)
                    {
                        if (DBGrid3.Rows[i].Cells[j].Value != null)
                            sum += Convert.ToDecimal(DBGrid3.Rows[i].Cells[j].Value);
                    }
                    DBGrid3.Rows[i].Cells[DBGrid3.ColumnCount - 1].Value = sum;
                }

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart3.Series.Clear();
                for (int i = 0; i < DBGrid3.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid3.Rows[i].Cells[0].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 1; j < DBGrid3.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid3.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid3.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid3.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart3.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart3.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart3.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart3.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart3.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid3.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart3.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Column;
                ser.LegendText = DBGrid3.CurrentRow.Cells[0].Value.ToString();
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 1; j < DBGrid3.Columns.Count - 1; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid3.CurrentRow.Cells[j].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid3.CurrentRow.Cells[j].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid3.Columns[j].HeaderText.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart3.Series.Add(ser);
                chart3.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart3.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart3.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart3.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid3.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart3.Series.Clear();

                for (int i = DBGrid3.SelectedRows.Count - 1; i >= 0; i--)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid3.SelectedRows[i].Cells[0].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid3.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid3.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid3.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid3.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min == max && min == 0)
                    {
                        return;
                    }
                    chart3.Series.Add(ser);
                }
                chart3.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart3.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart3.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart3.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Waste Analyze
        private void btnClose4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbMonth4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth4.Checked)
                txtYear4.Visible = true;
            else
                txtYear4.Visible = false;
            InitGrid4();
            LoadData4();
        }

        private void btnRefresh4_Click(object sender, EventArgs e)
        {
            LoadData4();
        }

        private void btnReload4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeWaste dal = new dalAccAnalyzeWaste();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    InitGrid4();
                    LoadData4();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitGrid4()
        {
            DBGrid4.Rows.Clear();
            DBGrid4.Columns.Clear();
            DBGrid4.ReadOnly = true;
            DBGrid4.AllowUserToAddRows = false;
            DBGrid4.AllowUserToDeleteRows = false;
            DBGrid4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBGrid4.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("SubjectId");
            col.DataPropertyName = "SubjectId";
            col.Name = "SubjectId";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid4.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("SubjectName");
            col.DataPropertyName = "SubjectName";
            col.Name = "SubjectName";
            col.Width = 140;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid4.Columns.Add(col);
            col.Dispose();

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("AdFlag");
            col.DataPropertyName = "AdFlag";
            col.Name = "AdFlag";
            col.Visible = false;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid4.Columns.Add(col);
            col.Dispose();

            DBGrid4.Columns[1].Frozen = true;
            if (rbMonth4.Checked)
            {
                for (int i = 1; i < 13; i++)
                {
                    string coltitle = i.ToString().Trim() + "月";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString(coltitle);
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid4.Columns.Add(col);
                    col.Dispose();
                }
            }
            else
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modf = dalp.GetItem(1, out Util.emsg);

                for (int i = modf.AccYear; i <= DateTime.Today.Year; i++)
                {
                    string coltitle = i.ToString() + "年";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = coltitle;
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid4.Columns.Add(col);
                    col.Dispose();
                }
            }

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("合计");
            col.DataPropertyName = "合计";
            col.Name = "合计";
            col.Width = 100;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid4.Columns.Add(col);
            col.Dispose();

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(DBGrid4);
            row.Cells[0].Value = "91353080";
            row.Cells[1].Value = "商品盈溢";
            row.Cells[2].Value = 1;            
            DBGrid4.Rows.Add(row);
            row.Dispose();

            row = new DataGridViewRow();
            row.CreateCells(DBGrid4);
            row.Cells[0].Value = "91353082";
            row.Cells[1].Value = "商品损耗";
            row.Cells[2].Value = 1;
            DBGrid4.Rows.Add(row);
            row.Dispose();

            row = new DataGridViewRow();
            row.CreateCells(DBGrid4);
            row.Cells[0].Value = "合计";
            row.Cells[1].Value = "合计";
            row.Cells[2].Value = 1;
            row.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid4.Rows.Add(row);
            row.Dispose();
        }

        private void LoadData4()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccAnalyzeWaste dal = new dalAccAnalyzeWaste();
                if (rbMonth4.Checked)
                {
                    BindingCollection<modAnalyzeWasteMonth> list = dal.GetMonthlyReport(Convert.ToInt32(txtYear4.Text), out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeWasteMonth mod in list)
                        {
                            for (int i = 0; i < DBGrid4.RowCount; i++)
                            {
                                if (mod.SubjectId == DBGrid4.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid4.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccMonth.ToString().Trim() == DBGrid4.Columns[j].Name.Replace("月", "").Trim())
                                        {
                                            DBGrid4.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                else
                {
                    BindingCollection<modAnalyzeWasteYears> list = dal.GetYearsReport(out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeWasteYears mod in list)
                        {
                            for (int i = 0; i < DBGrid4.RowCount; i++)
                            {
                                if (mod.SubjectId == DBGrid4.Rows[i].Cells[0].Value.ToString().Trim())
                                {
                                    for (int j = 3; j < DBGrid4.ColumnCount - 1; j++)
                                    {
                                        if (mod.AccYear.ToString().Trim() == DBGrid4.Columns[j].Name.Replace("年", "").Trim())
                                        {
                                            DBGrid4.Rows[i].Cells[j].Value = mod.SumMny;
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                for (int i = 0; i < DBGrid4.RowCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 3; j < DBGrid4.ColumnCount - 1; j++)
                    {
                        if (DBGrid4.Rows[i].Cells[j].Value != null)
                            sum += Convert.ToDecimal(DBGrid4.Rows[i].Cells[j].Value);
                    }
                    DBGrid4.Rows[i].Cells[DBGrid4.ColumnCount - 1].Value = sum;
                }

                for (int i = 3; i < DBGrid4.ColumnCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        if (DBGrid4.Rows[j].Cells[i].Value != null)
                            sum += Convert.ToDecimal(DBGrid4.Rows[j].Cells[i].Value) * Convert.ToDecimal(DBGrid4.Rows[j].Cells[2].Value);
                    }
                    DBGrid4.Rows[DBGrid4.RowCount - 1].Cells[i].Value = sum;
                }

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart4.Series.Clear();
                for (int i = 0; i < DBGrid4.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid4.Rows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid4.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid4.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid4.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid4.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart4.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart4.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart4.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart4.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart4.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid4_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid4.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart4.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Column;
                ser.LegendText = DBGrid4.CurrentRow.Cells[1].Value.ToString();
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 3; j < DBGrid4.Columns.Count - 1; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid4.CurrentRow.Cells[j].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid4.CurrentRow.Cells[j].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid4.Columns[j].HeaderText.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart4.Series.Add(ser);
                chart4.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart4.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart4.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart4.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid4.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart4.Series.Clear();

                for (int i = DBGrid4.SelectedRows.Count - 1; i >= 0; i--)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid4.SelectedRows[i].Cells[1].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid4.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid4.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid4.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid4.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min == max && min == 0)
                    {
                        return;
                    }
                    chart4.Series.Add(ser);
                }
                chart4.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart4.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart4.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart4.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Product Analyze
        private void rbMonth5_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonth5.Checked)
                txtYear5.Visible = true;
            else
                txtYear5.Visible = false;
            InitGrid5();
            LoadData5();
        }

        private void btnRefresh5_Click(object sender, EventArgs e)
        {
            LoadData5();
        }

        private void btnReload5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeProduct dal = new dalAccAnalyzeProduct();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    InitGrid5();
                    LoadData5();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitGrid5()
        {
            DBGrid5.Rows.Clear();
            DBGrid5.Columns.Clear();
            DBGrid5.ReadOnly = true;
            DBGrid5.AllowUserToAddRows = false;
            DBGrid5.AllowUserToDeleteRows = false;
            DBGrid5.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DBGrid5.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("Name");
            col.DataPropertyName = "Name";
            col.Name = "Name";
            col.Width = 140;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid5.Columns.Add(col);
            col.Dispose();

            DBGrid5.Columns[0].Frozen = true;

            if (rbMonth5.Checked)
            {
                for (int i = 1; i < 15; i++)
                {
                    string coltitle = i.ToString().Trim() + "月";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString(coltitle);
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid5.Columns.Add(col);
                    col.Dispose();
                }
            }
            else
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modf = dalp.GetItem(1, out Util.emsg);

                for (int i = modf.AccYear; i <= DateTime.Today.Year; i++)
                {
                    string coltitle = i.ToString() + "年";
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = coltitle;
                    col.DataPropertyName = coltitle;
                    col.Name = coltitle;
                    col.Width = 80;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid5.Columns.Add(col);
                    col.Dispose();
                }
            }

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = clsTranslate.TranslateString("合计");
            col.DataPropertyName = "合计";
            col.Name = "合计";
            col.Width = 100;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid5.Columns.Add(col);
            col.Dispose();

            string itemlist = "期初金额,采购金额,领用金额,生产金额,盈余金额,损耗金额,损耗率,销售成本,销售额,利润,利润率,结存金额";
            string[] items = itemlist.Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DBGrid5);
                row.Cells[0].Value = items[i];
                DBGrid5.Rows.Add(row);
                row.Dispose();
            }
        }

        private void LoadData5()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string purchasetypelist = string.Empty;
                dalAccAnalyzeProduct dal = new dalAccAnalyzeProduct();
                if (rbMonth5.Checked)
                {
                    BindingCollection<modAnalyzeProductMonth> list = dal.GetMonthlyReport(Convert.ToInt32(txtYear5.Text), out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        foreach (modAnalyzeProductMonth mod in list)
                        {
                            int j = mod.AccMonth;
                            for (int i = 0; i < DBGrid5.RowCount; i++)
                            {
                                switch (DBGrid5.Rows[i].Cells[0].Value.ToString())
                                {
                                    case "期初数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartQty;
                                        break;
                                    case "期初单价":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartPrice;
                                        break;
                                    case "期初金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartMny;
                                        break;
                                    case "采购数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchaseQty;
                                        break;
                                    case "采购单价":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchasePrice;
                                        break;
                                    case "采购金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchaseMny;
                                        break;
                                    case "领用数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.UsedQty;
                                        break;
                                    case "领用金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.UsedMny;
                                        break;
                                    case "生产数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProductionQty;
                                        break;
                                    case "生产金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProductionMny;
                                        break;
                                    case "盈余数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SurplusQty;
                                        break;
                                    case "盈余金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SurplusMny;
                                        break;
                                    case "损耗数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteQty;
                                        break;
                                    case "损耗金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteMny;
                                        break;
                                    case "损耗率":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteRatio;
                                        break;
                                    case "销售数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesQty;
                                        break;                                            
                                    case "销售成本":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.CostMny;
                                        break;
                                    case "销售价格":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesPrice;
                                        break;
                                    case "销售额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesMny;
                                        break;
                                    case "利润":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.Profit;
                                        break;
                                    case "利润率":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProfitRatio;
                                        break;
                                    case "结存数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipQty;
                                        break;
                                    case "价格":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipPrice;
                                        break;
                                    case "结存金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipMny;
                                        break;
                                }
                            }                          
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                else
                {
                    BindingCollection<modAnalyzeProductYears> list = dal.GetYearsReport(out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        ArrayList arrsub = new ArrayList();
                        int startyear = list[0].AccYear;
                        foreach (modAnalyzeProductYears mod in list)
                        {
                            int j = mod.AccYear - startyear + 1;
                            for (int i = 0; i < DBGrid5.RowCount; i++)
                            {
                                switch (DBGrid5.Rows[i].Cells[0].Value.ToString())
                                {
                                    case "期初数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartQty;
                                        break;
                                    case "期初单价":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartPrice;
                                        break;
                                    case "期初金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.StartMny;
                                        break;
                                    case "采购数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchaseQty;
                                        break;
                                    case "采购单价":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchasePrice;
                                        break;
                                    case "采购金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.PurchaseMny;
                                        break;
                                    case "领用数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.UsedQty;
                                        break;
                                    case "领用金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.UsedMny;
                                        break;
                                    case "生产数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProductionQty;
                                        break;
                                    case "生产金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProductionMny;
                                        break;
                                    case "盈余数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SurplusQty;
                                        break;
                                    case "盈余金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SurplusMny;
                                        break;
                                    case "损耗数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteQty;
                                        break;
                                    case "损耗金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteMny;
                                        break;
                                    case "损耗率":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WasteRatio;
                                        break;
                                    case "销售数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesQty;
                                        break;
                                    case "销售成本":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.CostMny;
                                        break;
                                    case "销售价格":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesPrice;
                                        break;
                                    case "销售额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.SalesMny;
                                        break;
                                    case "利润":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.Profit;
                                        break;
                                    case "利润率":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.ProfitRatio;
                                        break;
                                    case "结存数量":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipQty;
                                        break;
                                    case "价格":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipPrice;
                                        break;
                                    case "结存金额":
                                        DBGrid5.Rows[i].Cells[j].Value = mod.WipMny;
                                        break;
                                }
                            }                                
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
                for (int i = 0; i < DBGrid5.RowCount; i++)
                {
                    decimal sum = 0;
                    for (int j = 1; j < DBGrid5.ColumnCount - 1; j++)
                    {
                        if (DBGrid5.Rows[i].Cells[j].Value != null)
                            sum += Convert.ToDecimal(DBGrid5.Rows[i].Cells[j].Value);
                    }
                    DBGrid5.Rows[i].Cells[DBGrid5.ColumnCount - 1].Value = sum;
                }

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart5.Series.Clear();
                for (int i = 0; i < DBGrid5.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid5.Rows[i].Cells[0].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 1; j < DBGrid5.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid5.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid5.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid5.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart5.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart5.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart5.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart5.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart5.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid5.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart5.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Column;
                ser.LegendText = DBGrid5.CurrentRow.Cells[0].Value.ToString();
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 1; j < DBGrid5.Columns.Count - 1; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid5.CurrentRow.Cells[j].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid5.CurrentRow.Cells[j].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid5.Columns[j].HeaderText.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart5.Series.Add(ser);
                chart5.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart5.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart5.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart5.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid5.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart5.Series.Clear();

                for (int i = DBGrid5.SelectedRows.Count - 1; i >= 0; i--)
                {
                    Series ser = new Series();
                    //ser.ChartType = SeriesChartType.Column;
                    ser.LegendText = DBGrid5.SelectedRows[i].Cells[0].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 3; j < DBGrid5.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid5.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid5.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid5.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min == max && min == 0)
                    {
                        return;
                    }
                    chart5.Series.Add(ser);
                }
                chart5.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart5.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart5.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart5.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Product Detail Analyze
        private void cboAccName6_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void btnRefresh6_Click(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void btnReload6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(Util.modperiod.Seq - 1, out Util.emsg);
                if (modp == null) return;
                dalAccAnalyzeProduct dal = new dalAccAnalyzeProduct();
                bool ret = dal.Generate(modp.AccName, out Util.emsg);
                if (ret)
                {
                    LoadData6();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void LoadData6()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string purchasetypelist = string.Empty;
                dalAccAnalyzeProduct dal = new dalAccAnalyzeProduct();
                BindingCollection<modAnalyzeProductDetailMonth> list = dal.GetMonthlyDetailReport(cboAccName6.SelectedValue.ToString(), out Util.emsg);
                DBGrid6.DataSource = list;
                if (DBGrid6.RowCount > 0)
                {
                    DBGrid6.Columns["AccName"].Visible = false;
                    DBGrid6.Columns["AccYear"].Visible = false;
                    DBGrid6.Columns["AccMonth"].Visible = false;
                    DBGrid6.Columns["ProductId"].Visible = false;
                }
                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart6.Series.Clear();
                for (int i = 0; i < DBGrid6.Rows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Line;
                    ser.LegendText = DBGrid6.Rows[i].Cells[4].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 5; j < DBGrid6.Columns.Count; j++)
                    {
                        DBGrid6.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        decimal cellvalue = 0;
                        if (DBGrid6.Rows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid6.Rows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid6.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart6.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart6.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart6.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart6.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart6.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid6_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid6.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart6.Series.Clear();
                Series ser = new Series();
                ser.ChartType = SeriesChartType.Line;
                ser.LegendText = DBGrid6.Columns[DBGrid6.CurrentCell.ColumnIndex].HeaderText;
                ser.MarkerStyle = MarkerStyle.Cross;
                ser.MarkerSize = 6;
                ser.BorderColor = Color.Transparent;
                //ser.IsValueShownAsLabel = true;
                ser.LabelAngle = 60;
                for (int j = 0; j < DBGrid6.RowCount; j++)
                {
                    decimal cellvalue = 0;
                    if (DBGrid6.Rows[j].Cells[DBGrid6.CurrentCell.ColumnIndex].Value != null)
                    {
                        cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid6.Rows[j].Cells[DBGrid6.CurrentCell.ColumnIndex].Value), 1);
                        if (min > cellvalue)
                            min = cellvalue;
                        if (max == 0 || max < cellvalue)
                            max = cellvalue;
                    }
                    else
                    {
                        if (min > cellvalue)
                            min = cellvalue;

                        if (max < cellvalue)
                            max = cellvalue;
                    }
                    ser.Points.AddXY(DBGrid6.Rows[j].Cells[4].Value.ToString(), cellvalue);
                }
                if (min == max && min == 0)
                {
                    return;
                }
                chart6.Series.Add(ser);
                chart6.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                chart6.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                chart6.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart6.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuRefreshChart6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid6.CurrentRow == null) return;

                decimal min = Convert.ToDecimal("0.00001");
                decimal max = 0;
                chart6.Series.Clear();
                for (int i = 0; i < DBGrid6.SelectedRows.Count; i++)
                {
                    Series ser = new Series();
                    ser.ChartType = SeriesChartType.Line;
                    ser.LegendText = DBGrid6.Rows[i].Cells[4].Value.ToString();
                    ser.MarkerStyle = MarkerStyle.Cross;
                    ser.MarkerSize = 6;
                    ser.BorderColor = Color.Transparent;
                    //ser.IsValueShownAsLabel = true;
                    ser.LabelAngle = 60;
                    for (int j = 5; j < DBGrid6.Columns.Count - 1; j++)
                    {
                        decimal cellvalue = 0;
                        if (DBGrid6.SelectedRows[i].Cells[j].Value != null)
                        {
                            cellvalue = Decimal.Round(Convert.ToDecimal(DBGrid6.SelectedRows[i].Cells[j].Value), 1);
                            if (min > cellvalue)
                                min = cellvalue;
                            if (max == 0 || max < cellvalue)
                                max = cellvalue;
                        }
                        else
                        {
                            if (min > cellvalue)
                                min = cellvalue;

                            if (max < cellvalue)
                                max = cellvalue;
                        }
                        ser.Points.AddXY(DBGrid6.Columns[j].HeaderText.ToString(), cellvalue);
                    }
                    if (min != max || min != 0 || max != 0)
                    {
                        chart6.Series.Add(ser);
                    }
                }
                if (min != max || min != 0 || max != 0)
                {
                    chart6.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(max);
                    chart6.ChartAreas[0].AxisY.Minimum = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(min), 1));
                }
                chart6.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart6.Legends[0].BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void mnuDetail6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid6.CurrentRow == null) return;

                dalAccProductInout dal=new dalAccProductInout();
                BindingCollection<modAccProductInout> list = dal.GetIList(cboAccName6.SelectedValue.ToString(), DBGrid6.CurrentRow.Cells["ProductId"].Value.ToString(), Util.IsTrialBalance, out Util.emsg);
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Account Product Inout"), list);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
