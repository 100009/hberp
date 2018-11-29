using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_EXPENSE_REPORT : Form
    {
        dalAccReport _dal = new dalAccReport();
        public ACC_EXPENSE_REPORT()
        {
            InitializeComponent();
        }

        private void ACC_EXPENSE_PROFIT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);
            LoadData();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.Rows.Clear();
                BindingCollection<modAccExpenseReport> list = _dal.GetAccExpenseReport(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                DBGrid.DataSource = list;
                if (list != null && list.Count > 0)
                {
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;                    
                    DBGrid.Columns["ThisMonth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["ThisYear"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["AdFlag"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    SetColor();
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

        private void SetColor()
        {
            if (DBGrid.RowCount > 0)
            {
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().Trim().Length == 6)
                    {
                        DBGrid.Rows[i].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (DBGrid.Rows[i].Cells[1].Value.ToString() == "-")
                    {
                        DBGrid.Rows[i].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                    }
                }
            }
        }

        private void ACC_PROFIT_REPORT_Shown(object sender, EventArgs e)
        {
            SetColor();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                list.Add(new modExcelRangeData(string.Format("{0}", clsLxms.GetParameterValue("COMPANY_NAME")), "C3", "D3"));
                modAccPeriodList modperiod=(modAccPeriodList)cboAccName.SelectedItem;
                list.Add(new modExcelRangeData(string.Format("{0}", modperiod.EndDate.ToString("yyyy年MM月dd日")), "F3", "F3"));
                int n = 0;
                bool nTT = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modAccExpenseReport modd = (modAccExpenseReport)DBGrid.Rows[i].DataBoundItem;
                    if (modd.SubjectId == "913530")
                    {
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "C33", "C33"));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "D33", "D33"));
                        n = 0;
                    }
                    else if (modd.SubjectId == "913535")
                    {
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "G33", "G33"));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "H33", "H33"));
                        n = 0;
                    }
                    else if (modd.SubjectId == "913540")
                    {
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "C49", "C49"));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "D49", "D49"));
                        n = 0;
                    }
                    else if (modd.SubjectId.StartsWith("913530") && modd.SubjectId.Length == 8)
                    {
                        list.Add(new modExcelRangeData(string.Format("{0}", modd.SubjectName), "B" + (i + 6).ToString(), "B" + (i + 6).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "C" + (i + 6).ToString(), "C" + (i + 6).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "D" + (i + 6).ToString(), "D" + (i + 6).ToString()));
                    }
                    else if (modd.SubjectId.StartsWith("913535") && modd.SubjectId.Length == 8)
                    {
                        list.Add(new modExcelRangeData(string.Format("{0}", modd.SubjectName), "F" + (n + 5).ToString(), "F" + (n + 5).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "G" + (n + 5).ToString(), "G" + (n + 5).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "H" + (n + 5).ToString(), "H" + (n + 5).ToString()));
                    }
                    else if (modd.SubjectId.StartsWith("913540") && modd.SubjectId.Length == 8)
                    {
                        list.Add(new modExcelRangeData(string.Format("{0}", modd.SubjectName), "B" + (n + 36).ToString(), "B" + (n + 36).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "C" + (n + 36).ToString(), "C" + (n + 36).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "D" + (n + 36).ToString(), "D" + (n + 36).ToString()));
                    }
                    else if (modd.SubjectId=="-")
                    {
                        if (!nTT)
                        {
                            n = 1;
                            nTT = true;
                        }
                        list.Add(new modExcelRangeData(string.Format("{0}", modd.SubjectName), "F" + (n + 36).ToString(), "F" + (n + 36).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisMonth), "G" + (n + 36).ToString(), "G" + (n + 36).ToString()));
                        list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ThisYear), "H" + (n + 36).ToString(), "H" + (n + 36).ToString()));
                    }
                    n++;
                }
                clsExport.ExportByTemplate(list, "费用统计表", 1, 49, 8, 1);
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

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                dalAccReport dal = new dalAccReport();
                modAccExpenseReport mod = (modAccExpenseReport)DBGrid.CurrentRow.DataBoundItem;
                if (mod.SubjectId.IndexOf("9135") == 0)
                {
                    BindingCollection<modAccCredenceDetail> list = new BindingCollection<modAccCredenceDetail>();
                    dal.GetCredenceDetail(true, cboAccName.ComboBox.SelectedValue.ToString(), mod.SubjectId, Util.IsTrialBalance, ref list, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        frmViewList frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, list);
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
    }
}
