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
    public partial class ACC_PROFIT_REPORT : Form
    {
        dalAccReport _dal = new dalAccReport();
        public ACC_PROFIT_REPORT()
        {
            InitializeComponent();
        }

        private void ACC_PROFIT_REPORT_Load(object sender, EventArgs e)
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
                BindingCollection<modAccProfitReport> list = _dal.GetAccProfitReport(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                DBGrid.DataSource = list;
                if (list != null && list.Count > 0)
                {
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid.Columns["ThisMonth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["ThisYear"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["AdFlag"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void ACC_PROFIT_REPORT_Shown(object sender, EventArgs e)
        {
            if (DBGrid.RowCount > 0)
            {
                DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modAccProfitReport modd = (modAccProfitReport)DBGrid.Rows[i].DataBoundItem;
                    switch (modd.SubjectId)
                    {
                        case "913505":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E6", "E6"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F6", "F6"));
                            break;
                        case "913510":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E7", "E7"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F7", "F7"));
                            break;
                        case "913515":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E8", "E8"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F8", "F8"));
                            break;
                        case "913518":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E9", "E9"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F9", "F9"));
                            break;
                        case "913520":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E15", "E15"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F15", "F15"));
                            break;
                        case "913525":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E16", "E16"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F16", "F16"));
                            break;
                        case "913530":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E17", "E17"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F17", "F17"));
                            break;
                        case "913535":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E18", "E18"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F18", "F18"));
                            break;
                        case "913540":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E19", "E19"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F19", "F19"));
                            break;
                        case "913545":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E26", "E26"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F26", "F26"));
                            break;
                        case "913550":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E27", "E27"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F27", "F27"));
                            break;
                        case "913555":
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisMonth), "E33", "E33"));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.ThisYear), "F33", "F33"));
                            break;
                    }
                }
                clsExport.ExportByTemplate(list, "利润表", 1, 46, 6, 1);
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
                modAccProfitReport mod = (modAccProfitReport)DBGrid.CurrentRow.DataBoundItem;
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
