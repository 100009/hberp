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
    public partial class ACC_ASSET_DEBT_REPORT : Form
    {
        dalAccReport _dal = new dalAccReport();
        public ACC_ASSET_DEBT_REPORT()
        {
            InitializeComponent();
        }

        private void ACC_ASSET_DEBT_REPORT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);
            //LoadData();
        }

        private void ACC_ASSET_DEBT_REPORT_Shown(object sender, EventArgs e)
        {
            SetGridColor();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadData()
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
                dalAccPeriodList dalperiod = new dalAccPeriodList();
                modAccPeriodList modYearStartPeriod = dalperiod.GetYearStartItem(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
				//if(modYearStartPeriod == null)
				//	modYearStartPeriod = dalperiod.GetFirstItem(out Util.emsg);

                dalAccSubjectList dalsubject = new dalAccSubjectList();
                dalAccReport.staticSubjectList = dalsubject.GetAllList(true, out Util.emsg);

                dalAccReport.staticYearSubjectBalance = _dal.GetSubjectBalance(modYearStartPeriod.AccName, true, Util.IsTrialBalance, out Util.emsg);   //上月结存
                dalAccReport.staticEndSubjectBalance = _dal.GetSubjectBalance(cboAccName.ComboBox.SelectedValue.ToString(), false, Util.IsTrialBalance, out Util.emsg);

                List<modAccAssetDebtReport> list1 = new List<modAccAssetDebtReport>();
                _dal.GetAccAssetDebtReport(cboAccName.ComboBox.SelectedValue.ToString(), "1", Util.IsTrialBalance, ref list1, out Util.emsg);
                DBGrid1.DataSource = list1;
                if (list1 == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;                    
                    DBGrid1.Columns[0].Visible = false;
                    DBGrid1.Columns[1].Visible = false;
                    DBGrid1.Columns[3].Visible = false;
                    DBGrid1.Columns[6].Visible = false;
                    DBGrid1.Columns[7].Visible = false;
                    DBGrid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid1.Columns[4].Width = 168;
                    DBGrid1.Columns[5].Width = 168;
                    Status1.Text = "资产总计： " + list1[0].EndMny.ToString();
                }
                List<modAccAssetDebtReport> list2 = new List<modAccAssetDebtReport>();
                _dal.GetAccAssetDebtReport(cboAccName.ComboBox.SelectedValue.ToString(), "5", Util.IsTrialBalance, ref list2, out Util.emsg);
                DBGrid2.DataSource = list2;
                if (list2 == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid2.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid2.Columns[0].Visible = false;
                    DBGrid2.Columns[1].Visible = false;
                    DBGrid2.Columns[3].Visible = false;
                    DBGrid2.Columns[6].Visible = false;
                    DBGrid2.Columns[7].Visible = false;
                    DBGrid2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid2.Columns[4].Width = 168;
                    DBGrid2.Columns[5].Width = 168;
                    Status2.Text = "负债及权益总计： " + list2[0].EndMny.ToString();
                }
                SetGridColor();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void SetGridColor()
        {
            if (DBGrid1.RowCount > 0)
            {
                for (int i = 0; i < DBGrid1.RowCount; i++)
                {
                    if (DBGrid1.Rows[i].Cells["HasChildren"].Value.ToString() == "1")
                        DBGrid1.Rows[i].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    else
                        DBGrid1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
            if (DBGrid2.RowCount > 0)
            {
                for (int i = 0; i < DBGrid2.RowCount; i++)
                {
                    if (DBGrid2.Rows[i].Cells["HasChildren"].Value.ToString() == "1")
                        DBGrid2.Rows[i].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    else
                        DBGrid2.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                }
            }
        }
        
        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DBGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmViewList frm;
            modAccAssetDebtReport mod = (modAccAssetDebtReport)DBGrid1.CurrentRow.DataBoundItem;
            switch (mod.SubjectId)
            {
                case "1055":   //应收帐款
                    dalAccReceivableList dalrec = new dalAccReceivableList();
                    BindingCollection<modCustReceivableSummary> listrec = dalrec.GetCustReceivableSummary(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    if (listrec != null && listrec.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listrec);
                        frm.ShowDialog();
                    }
                    break;
                case "1060":   //其它应收款
                    dalAccOtherReceivable dalorec = new dalAccOtherReceivable();
                    BindingCollection<modOtherReceivableSummary> listorec = dalorec.GetOtherReceivableSummary(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    if (listorec != null && listorec.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listorec);
                        frm.ShowDialog();
                    }
                    break;
                case "1075":   //应收票据
                    dalAccCheckList dalcheck = new dalAccCheckList();
                    BindingCollection<modAccCheckList> listcheck = dalcheck.GetIList("0", string.Empty, mod.SubjectId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    frm = new frmViewList();
                    frm.InitViewList(mod.SubjectName, listcheck);
                    frm.ShowDialog();
                    break;
                case "1235":   //库存商品
                    dalAccProductInout dalio = new dalAccProductInout();
                    BindingCollection<modAccProductSummary> listio = dalio.GetAccProductSummary(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                    if (listio != null && listio.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listio);
                        frm.ShowDialog();
                    }
                    break;
                default:
                    dalAccReport dalrpt = new dalAccReport();
                    BindingCollection<modAccCredenceDetail> listrpt = new BindingCollection<modAccCredenceDetail>();
                    dalrpt.GetCredenceDetail(true, cboAccName.ComboBox.SelectedValue.ToString(), mod.SubjectId, Util.IsTrialBalance, ref listrpt, out Util.emsg);
                    if (listrpt != null && listrpt.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listrpt);
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
            }
        }

        private void DBGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmViewList frm;
            modAccAssetDebtReport mod = (modAccAssetDebtReport)DBGrid2.CurrentRow.DataBoundItem;
            switch (mod.SubjectId)
            {
                case "5145":   //应付帐款
                    dalAccPayableList dalpay = new dalAccPayableList();
                    BindingCollection<modVendorPayableSummary> listpay = dalpay.GetVendorPayableSummary(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    if (listpay != null && listpay.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listpay);
                        frm.ShowDialog();
                    }
                    break;
                case "5125":   //应付票据
                    dalAccCheckList dalcheck = new dalAccCheckList();
                    BindingCollection<modAccCheckList> listcheck = dalcheck.GetIList("0", string.Empty, mod.SubjectId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    frm = new frmViewList();
                    frm.InitViewList(mod.SubjectName, listcheck);
                    frm.ShowDialog();
                    break;                
                case "5155":   //其它应付款
                    dalAccOtherPayable dalopay = new dalAccOtherPayable();
                    BindingCollection<modOtherPayableSummary> listopay = dalopay.GetOtherPayableSummary(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    if (listopay != null && listopay.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listopay);
                        frm.ShowDialog();
                    }
                    break;       
                default:
                    dalAccReport dalrpt = new dalAccReport();
                    BindingCollection<modAccCredenceDetail> listrpt = new BindingCollection<modAccCredenceDetail>();
                    dalrpt.GetCredenceDetail(true, cboAccName.ComboBox.SelectedValue.ToString(), mod.SubjectId, Util.IsTrialBalance, ref listrpt, out Util.emsg);
                    if (listrpt != null && listrpt.Count > 0)
                    {
                        frm = new frmViewList();
                        frm.InitViewList(mod.SubjectName, listrpt);
                        frm.ShowDialog();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    break;
            }
        }

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid1.CurrentRow == null) return;
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                list.Add(new modExcelRangeData(modp.EndDate.ToString("yyyy年MM月dd日"), "D3", "F3"));
                for (int i = 0; i < DBGrid1.RowCount; i++)
                {
                    modAccAssetDebtReport modd = (modAccAssetDebtReport)DBGrid1.Rows[i].DataBoundItem;
                    list.Add(new modExcelRangeData(modd.SubjectName, "A" + (6 + i).ToString().Trim(), "A" + (6 + i).ToString().Trim()));
                    list.Add(new modExcelRangeData((i+1).ToString(), "B" + (6 + i).ToString().Trim(), "B" + (6 + i).ToString().Trim()));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.YearStartMny), "C" + (6 + i).ToString().Trim(), "C" + (6 + i).ToString().Trim()));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.EndMny), "D" + (6 + i).ToString().Trim(), "D" + (6 + i).ToString().Trim()));
                }

                int rowindex = -1;
                for (int i = 0; i < DBGrid2.RowCount; i++)
                {
                    modAccAssetDebtReport modd = (modAccAssetDebtReport)DBGrid2.Rows[i].DataBoundItem;
                    if (modd.SubjectId.IndexOf("9135") < 0 || modd.SubjectId.Length != 8)
                    {
                        rowindex++;
                        list.Add(new modExcelRangeData(modd.SubjectName, "F" + (6 + rowindex).ToString().Trim(), "F" + (6 + rowindex).ToString().Trim()));
                        list.Add(new modExcelRangeData((rowindex+1).ToString(), "G" + (6 + rowindex).ToString().Trim(), "G" + (6 + rowindex).ToString().Trim()));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.YearStartMny), "H" + (6 + rowindex).ToString().Trim(), "H" + (6 + rowindex).ToString().Trim()));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.EndMny), "I" + (6 + rowindex).ToString().Trim(), "I" + (6 + rowindex).ToString().Trim()));
                    }
                }
                clsExport.ExportByTemplate(list, "资产负债表", 1, Util.Max(DBGrid1.RowCount, rowindex) + 2, 9, 1);
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
