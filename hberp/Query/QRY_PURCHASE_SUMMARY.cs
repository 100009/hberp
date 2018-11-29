using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.Model;
using LXMS.DAL;

namespace LXMS
{
    public partial class QRY_PURCHASE_SUMMARY : Form
    {
        dalPurchaseList _dal = new dalPurchaseList();
        public QRY_PURCHASE_SUMMARY()
        {
            InitializeComponent();
        }

        private void QRY_PURCHASE_SUMMARY_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillPurchaseType(cboPurchaseType, true);
            FillControl.FillVendorList(lstVendor, "1", string.Empty);
            lstVendor.BackColor = frmOptions.BACKCOLOR;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;

        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string statuslist = "9";
                if (chkNotAudit.Checked)
                    statuslist += ",0";
                if (chkAudited.Checked)
                    statuslist += ",1";

                string paystatuslist = string.Empty;
                if (chkPay0.Checked)
                    paystatuslist += ",0";
                if (chkPay1.Checked)
                    paystatuslist += ",1";

                string invoicestatuslist = string.Empty;
                if (chkInvoice0.Checked)
                    invoicestatuslist += ",0";
                if (chkInvoice1.Checked)
                    invoicestatuslist += ",1";
                if (chkInvoice2.Checked)
                    invoicestatuslist += ",2";

                string vendorlist = string.Empty;
                if (lstVendor.SelectedItems.Count > 0 && lstVendor.SelectedItems.Count < lstVendor.Items.Count)
                {
                    for (int i = 0; i < lstVendor.SelectedItems.Count; i++)
                    {
                        modVendorList mod = (modVendorList)lstVendor.SelectedItems[i];
                        if (i == 0)
                            vendorlist = mod.VendorName;
                        else
                            vendorlist += "," + mod.VendorName;
                    }
                }

                BindingCollection<modPurchaseSummary> list = _dal.GetPurchaseSummary(statuslist, cboPurchaseType.SelectedValue.ToString(), vendorlist, paystatuslist, invoicestatuslist, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (list.Count > 0)
                {
                    decimal sum = 0;
                    foreach (modPurchaseSummary mod in list)
                    {
                        sum += Convert.ToDecimal(mod.AdFlag) * (mod.DetailSum + mod.OtherMny - mod.KillMny);
                    }
                    ShowColor();
                    StatusLabel1.Text = "共 " + list.Count.ToString() + " 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
                    DBGrid.Columns["DetailSum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["OtherMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["KillMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["SumMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ShowColor()
        {
            if (DBGrid.RowCount == 0) return;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (Convert.ToInt32(DBGrid.Rows[i].Cells["AdFlag"].Value) == -1)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            string statuslist = "9";
            if (chkNotAudit.Checked)
                statuslist += ",0";
            if (chkAudited.Checked)
                statuslist += ",1";

            string paystatuslist = string.Empty;
            if (chkPay0.Checked)
                paystatuslist += ",0";
            if (chkPay1.Checked)
                paystatuslist += ",1";

            string invoicestatuslist = string.Empty;
            if (chkInvoice0.Checked)
                invoicestatuslist += ",0";
            if (chkInvoice1.Checked)
                invoicestatuslist += ",1";
            if (chkInvoice2.Checked)
                invoicestatuslist += ",2";

            string vendorlist = string.Empty;
            if (lstVendor.SelectedItems.Count > 0 && lstVendor.SelectedItems.Count < lstVendor.Items.Count)
            {
                for (int i = 0; i < lstVendor.SelectedItems.Count; i++)
                {
                    modVendorList mod = (modVendorList)lstVendor.SelectedItems[i];
                    if (i == 0)
                        vendorlist = mod.VendorName;
                    else
                        vendorlist += "," + mod.VendorName;
                }
            }

            BindingCollection<modVPurchaseDetail> list = new BindingCollection<modVPurchaseDetail>();
            if (DBGrid.CurrentRow.Index == DBGrid.RowCount - 1)
            {
                list = _dal.GetVDetail(statuslist, cboPurchaseType.SelectedValue.ToString(), vendorlist, string.Empty, string.Empty, paystatuslist, invoicestatuslist, string.Empty, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            else
            {
                modPurchaseSummary moditem = (modPurchaseSummary)DBGrid.CurrentRow.DataBoundItem;
                list = _dal.GetVDetail(string.Empty, moditem.PurchaseType, moditem.VendorName, string.Empty, string.Empty, paystatuslist, invoicestatuslist, moditem.Currency, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            if (list != null && list.Count > 0)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Purchase List"), list);
                frm.ShowDialog();
            }
        }

        private void QRY_PURCHASE_SUMMARY_Shown(object sender, EventArgs e)
        {
            ShowColor();
        }
    }
}