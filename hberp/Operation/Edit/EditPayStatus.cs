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
    public partial class EditPayStatus : Form
    {
        string _idtype = string.Empty;
        string _idlist = string.Empty;        
        public EditPayStatus()
        {
            InitializeComponent();
        }

        private void EditPayStatus_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);            
            FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
            FillControl.FillBankAccount(cboAccountNo, true, false);
            txtInvoiceMny.Text = "0";
        }

        public void InitData(string idtype, string idlist)
        {
            _idtype = idtype;
            _idlist = idlist;
            if (_idtype == "SALE")
            {
                FillControl.FillReceiveStatus(cboPayStatus, false);
                lblPayStatus.Text = clsTranslate.TranslateString("Receive Status");
                this.Text = lblPayStatus.Text;
            }
            else
            {
                FillControl.FillPayStatus(cboPayStatus, false);
                lblPayStatus.Text = clsTranslate.TranslateString("Pay Status");
                this.Text = lblPayStatus.Text;
            }
        }

        private void btnPostPay_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboPayStatus.SelectedIndex == 1)
                {
                    if (cboAccountNo.SelectedValue == null)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Account no") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboAccountNo.Focus();
                        return;
                    }
                }
                if (_idtype == "SALE")
                {
                    dalSalesShipment dal = new dalSalesShipment();
                    bool ret = dal.UpdateReceiveStatus(true, false, _idlist, cboPayStatus.SelectedIndex, cboAccountNo.SelectedValue == null ? string.Empty : cboAccountNo.SelectedValue.ToString(), dtpPayDate.Value.ToString("yyyy-MM-dd"), 0, 0, string.Empty, Util.UserId, out Util.emsg);
                    if (ret)
                    {
                        this.DialogResult = DialogResult.OK;
                        //this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    dalPurchaseList dal = new dalPurchaseList();
                    bool ret = dal.UpdatePayStatus(true, false, _idlist, cboPayStatus.SelectedIndex, cboAccountNo.SelectedValue == null ? string.Empty : cboAccountNo.SelectedValue.ToString(), dtpPayDate.Value.ToString("yyyy/MM/dd"), 0, 0, string.Empty, Util.UserId, out Util.emsg);
                    if (ret)
                    {
                        this.DialogResult = DialogResult.OK;
                        //this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnPostInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                if (string.IsNullOrEmpty(txtInvoiceMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtInvoiceMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                if (cboInvoiceStatus.SelectedIndex == 2)
                {
                    if (Convert.ToDecimal(txtInvoiceMny.Text) == 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must >0 !"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceMny.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceNo.Focus();
                        return;
                    }
                }
                if (_idtype == "SALE")
                {
                    dalSalesShipment _dal = new dalSalesShipment();
                    bool ret = _dal.UpdateReceiveStatus(false, true, _idlist, 0, dtpPayDate.Value.ToString("yyyy/MM/dd"), string.Empty, cboInvoiceStatus.SelectedIndex, Convert.ToDecimal(txtInvoiceMny.Text), txtInvoiceNo.Text.Trim(), Util.UserId, out Util.emsg);
                    if (ret)
                    {
                        this.DialogResult = DialogResult.OK;
                        //this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    dalPurchaseList _dal = new dalPurchaseList();
                    bool ret = _dal.UpdatePayStatus(false, true, _idlist, 0, dtpPayDate.Value.ToString("yyyy/MM/dd"), string.Empty, cboInvoiceStatus.SelectedIndex, Convert.ToDecimal(txtInvoiceMny.Text), txtInvoiceNo.Text.Trim(), Util.UserId, out Util.emsg);
                    if (ret)
                    {
                        this.DialogResult = DialogResult.OK;
                        //this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }
    }
}
