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
    public partial class ACC_ACCOUNT_BALANCE : Form
    {
        dalAccReport _dal = new dalAccReport();
        public ACC_ACCOUNT_BALANCE()
        {
            InitializeComponent();
        }

        private void ACC_ACCOUNT_BALANCE_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Rebuild"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuRebuild_Click));
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);
            //LoadData();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
			try
			{
				this.Cursor = Cursors.WaitCursor;
				BindingCollection<modAccountBalance> list = _dal.GetAccountBalance(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);

                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid.Columns["TotalSum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["DetailSum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["Differ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["TotalSum"].Width = 200;
                    DBGrid.Columns["DetailSum"].Width = 200;
                    DBGrid.Columns["Differ"].Width = 200;
					//if (DBGrid.RowCount > 0)
					//{
					//	for (int i = 0; i < DBGrid.RowCount; i++)
					//	{
					//		DBGrid.Rows[i].Cells["TotalSum"].Value = clsMoney.ConvertToMoney(float.Parse(DBGrid.Rows[i].Cells["TotalSum"].Value.ToString()));
					//		DBGrid.Rows[i].Cells["DetailSum"].Value = clsMoney.ConvertToMoney(float.Parse(DBGrid.Rows[i].Cells["DetailSum"].Value.ToString()));
					//	}
					//}					
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                frmViewList frm = new frmViewList();
                modAccountBalance mod = (modAccountBalance)DBGrid.CurrentRow.DataBoundItem;
                switch (mod.SubjectId)
                {
                    case "1030":   //现金银行
                        BindingCollection<modAccCredenceDetail> listcash = _dal.GetCashAndBankDetail(Util.modperiod.AccName, Util.IsTrialBalance, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listcash);
                        break;
                    case "1235":   //库存商品
                        dalAccProductInout dalpdt = new dalAccProductInout();
                        BindingCollection<modAccProductSummary> listpdt = dalpdt.GetAccProductSummary(Util.modperiod.AccName, Util.IsTrialBalance, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listpdt);
                        break;
                    case "1055":   //应收帐款
                        dalAccReceivableList dalrec = new dalAccReceivableList();
                        BindingCollection<modCustReceivableSummary> listrec = dalrec.GetCustReceivableSummary(Util.modperiod.AccName, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listrec);
                        break;
                    case "5145":   //应付账款
                        dalAccPayableList dalpay = new dalAccPayableList();
                        BindingCollection<modVendorPayableSummary> listpay = dalpay.GetVendorPayableSummary(Util.modperiod.AccName, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listpay);
                        break;
                    case "1060":   //其它应收款
                        dalAccOtherReceivable dalorec = new dalAccOtherReceivable();
                        BindingCollection<modOtherReceivableSummary> listorec = dalorec.GetOtherReceivableSummary(Util.modperiod.AccName, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listorec);
                        break;
                    case "5155":   //其他应付款
                        dalAccOtherPayable dalopay = new dalAccOtherPayable();
                        BindingCollection<modOtherPayableSummary> listopay = dalopay.GetOtherPayableSummary(Util.modperiod.AccName, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listopay);
                        break;
                    case "1075":   //应收票据
                        dalAccCheckList dalcheckrec = new dalAccCheckList();
                        BindingCollection<modAccCheckList> listcheckrec = dalcheckrec.GetIList("0", string.Empty, mod.SubjectId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listcheckrec);
                        break;
                    case "5125":   //应付票据
                        dalAccCheckList dalcheckpay = new dalAccCheckList();
                        BindingCollection<modAccCheckList> listcheckpay = dalcheckpay.GetIList("0", string.Empty, mod.SubjectId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                        frm.InitViewList(mod.SubjectName + clsTranslate.TranslateString("Detail"), listcheckpay);
                        break;
                    default:
                        break;
                }
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

        private void mnuRebuild_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                dalAccPeriodList dal = new dalAccPeriodList();
                if (dal.RebuildStartData(cboAccName.ComboBox.SelectedValue.ToString(), DBGrid.CurrentRow.Cells[1].Value.ToString(), out Util.emsg))
                {
                    LoadData();
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
    }
}