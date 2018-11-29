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
    public partial class EditDesignForm : Form
    {
        string _action;
        dalSalesDesignForm _dal = new dalSalesDesignForm();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditDesignForm()
        {
            InitializeComponent();
            FillControl.FillBankAccount(cboAccountNo, true, false);
        }

        private void EditDesignForm_Load(object sender, EventArgs e)
        {            
            clsTranslate.InitLanguage(this);
            txtId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_SHIP_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;

            if (clsLxms.GetParameterValue("NEED_CUST_ORDER_NO").CompareTo("T") == 0)
                lblStarOrder.Visible = true;
            else
                lblStarOrder.Visible = false;
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            dtpFormDate.Value = DateTime.Today;
            txtId.Text = "0";
            txtInvoiceMny.Text = "0";
            txtSalesManMny.Text = "0";
            txtUnitNo.Text = "PCS";

            FillControl.FillDesignType(cboDesignType, false);
            FillControl.FillReceiveStatus(cboReceiveStatus, false);
            FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
            cboReceiveStatus.SelectedIndex = -1;
            cboInvoiceStatus.SelectedIndex = -1;            
            btnUpdate.Enabled = false;
            status4.Image = null;
        }

        public void EditItem(int id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                FillControl.FillDesignType(cboDesignType, false);
                FillControl.FillReceiveStatus(cboReceiveStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                modSalesDesignForm mod = _dal.GetItem(id, out Util.emsg);
                if (mod != null)
                {
                    txtId.Text = id.ToString();
                    dtpFormDate.Value = mod.FormDate;
                    cboDesignType.SelectedValue = mod.FormType;
                    txtCustOrderNo.Text = mod.CustOrderNo;
                    txtNo.Text = mod.No;
                    txtCurrency.Text = mod.Currency;
                    txtCustName.Tag = mod.CustId;
                    txtCustName.Text = mod.CustName;
                    txtPayMethod.Text = mod.PayMethod;
                    txtSalesMan.Text = mod.SalesMan;
                    txtUnitNo.Text = mod.UnitNo;
                    txtQty.Text = mod.Qty.ToString();
                    txtMny.Text = mod.Mny.ToString();
                    txtSalesManMny.Text = mod.SalesManMny.ToString();
                    txtProductName.Text = mod.ProductName;
                    txtRemark.Text = mod.Remark;
                    cboReceiveStatus.SelectedIndex = Convert.ToInt32(mod.ReceiveStatus);
                    cboAccountNo.SelectedValue = mod.AccountNo;
                    txtReceiveDate.Text = mod.ReceiveDate;
                    cboInvoiceStatus.SelectedIndex = mod.InvoiceStatus;
                    txtInvoiceMny.Text = mod.InvoiceMny.ToString();
                    txtInvoiceNo.Text = mod.InvoiceNo;
                    if (mod.Status == 1)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);                        
                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        toolSave.Visible = true;
                        Util.ChangeStatus(this, false);
                        txtId.ReadOnly = true;                        
                        toolSave.Enabled = true;
                    }
                    cboReceiveStatus.Enabled = true;
                    cboInvoiceStatus.Enabled = true;
                    cboAccountNo.Enabled = true;
                    txtInvoiceMny.ReadOnly = false;
                    txtInvoiceNo.ReadOnly = false;                    
                    btnUpdate.Enabled = true;
                    btnReceiveDate.Enabled = true;
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

        public void Import(int id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "NEW";
                FillControl.FillDesignType(cboDesignType, false);
                FillControl.FillReceiveStatus(cboReceiveStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                dalCustomerOrderList dal = new dalCustomerOrderList();
                modCustomerOrderList mod = dal.GetItem(id, out Util.emsg);
                if (mod != null)
                {
                    txtId.Text = "0";
                    dtpFormDate.Value = DateTime.Today;
                    //cboDesignType.SelectedValue = mod.FormType;
                    txtCustOrderNo.Text = mod.CustOrderNo;
                    txtNo.Text = _dal.GetNewNo(dtpFormDate.Value, mod.CustId);
                    txtCurrency.Text = mod.Currency;
                    txtCustName.Tag = mod.CustId;
                    txtCustName.Text = mod.CustName;
                    txtPayMethod.Text = mod.PayMethod;
                    txtSalesMan.Text = mod.SalesMan;
                    txtUnitNo.Text = mod.UnitNo;                    
                    txtQty.Text = mod.Qty.ToString();
                    txtMny.Text = (mod.Qty * mod.Price).ToString();
                    txtSalesManMny.Text = "0";
                    txtProductName.Text = mod.ProductName;
                    txtRemark.Text = mod.Remark;
                    txtInvoiceMny.Text = "0";
                    dalCustomerList dalcust = new DAL.dalCustomerList();
                    modCustomerList modcust = dalcust.GetItem(mod.CustId, out Util.emsg);
                    if (modcust != null)
                    {
                        //txtCustName.Tag = modcust.CustId;
                        //txtCustName.Text = modcust.CustName;
                        //txtPayMethod.Text = modcust.txtPayMethod;
                        //txtSalesMan.Text = modcust.SalesMan;                        
                        //txtCurrency.Text = mod.Currency;
                        if (modcust.NeedInvoice == 1)
                        {
                            cboReceiveStatus.SelectedIndex = 0;
                            cboInvoiceStatus.SelectedIndex = 1;
                        }
                        else
                        {
                            cboReceiveStatus.SelectedIndex = 1;
                            cboInvoiceStatus.SelectedIndex = 0;
                        }
                    }

                    status4.Image = null;
                    toolSave.Visible = true;
                    Util.ChangeStatus(this, false);
                    txtId.ReadOnly = true;
                    toolSave.Enabled = true;
                    
                    cboReceiveStatus.Enabled = true;
                    cboInvoiceStatus.Enabled = true;
                    cboAccountNo.Enabled = true;
                    txtInvoiceMny.ReadOnly = false;
                    txtInvoiceNo.ReadOnly = false;
                    btnUpdate.Enabled = true;
                    btnReceiveDate.Enabled = true;
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

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
                    return;
                }
                if (cboDesignType.SelectedValue == null || string.IsNullOrEmpty(cboDesignType.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Form Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboDesignType.Focus();
                    return;
                }
                if (cboReceiveStatus.SelectedValue == null || string.IsNullOrEmpty(cboReceiveStatus.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Receive Status") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboReceiveStatus.Focus();
                    return;
                }
                if (cboInvoiceStatus.SelectedValue == null || string.IsNullOrEmpty(cboInvoiceStatus.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Status") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboInvoiceStatus.Focus();
                    return;
                }  
                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtQty.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSalesManMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sales Man Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSalesManMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtSalesManMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sales Man Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSalesManMny.Focus();
                    return;
                }
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
                if (txtCustName.Tag == null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCustName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_SHIP_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_CUST_ORDER_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtCustOrderNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Order No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustOrderNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSalesMan.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sales Man") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSalesMan.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Unit No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUnitNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProductName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductName.Focus();
                    return;
                }
                if (!string.IsNullOrEmpty(txtCustOrderNo.Text.Trim()))
                {
                    dalCustomerOrderList dalorder = new dalCustomerOrderList();
                    if (!dalorder.Exists(txtCustName.Tag.ToString(), txtCustOrderNo.Text.Trim(), out Util.emsg))
                    {
                        if (MessageBox.Show("无法找到对应的客户订单号，您要继续保存吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                    }
                }
                modSalesDesignForm mod = new modSalesDesignForm();
                mod.Id = Convert.ToInt32(txtId.Text);
                mod.FormDate = dtpFormDate.Value;
                mod.FormType = cboDesignType.SelectedValue.ToString();
                mod.No = txtNo.Text.Trim();
                mod.AdFlag = cboDesignType.SelectedValue.ToString().IndexOf("退货") > 0 ? -1 : 1;
                mod.CustOrderNo = txtCustOrderNo.Text.Trim();
                mod.CustId = txtCustName.Tag.ToString();
                mod.CustName = txtCustName.Text.Trim();
                mod.PayMethod = txtPayMethod.Text.Trim();
                mod.SalesMan = txtSalesMan.Text.Trim();
                mod.ProductName = txtProductName.Text.Trim();
                mod.Remark = txtRemark.Text.Trim();
                mod.Currency = txtCurrency.Text.Trim();
                dalAccCurrencyList dalcur = new dalAccCurrencyList();
                modAccCurrencyList modcur = dalcur.GetItem(txtCurrency.Text.Trim(), out Util.emsg);
                mod.ExchangeRate = modcur.ExchangeRate;
                mod.UnitNo = txtUnitNo.Text.Trim();
                mod.Qty = Convert.ToDecimal(txtQty.Text);
                mod.Mny = Convert.ToDecimal(txtMny.Text);
                mod.SalesManMny = Convert.ToDecimal(txtSalesManMny.Text);
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                mod.ReceiveStatus = cboReceiveStatus.SelectedIndex;
                mod.AccountNo = cboAccountNo.SelectedValue == null ? string.Empty : cboAccountNo.SelectedValue.ToString();
                mod.ReceiveDate = txtReceiveDate.Text.Trim();
                mod.InvoiceStatus = cboInvoiceStatus.SelectedIndex;
                mod.InvoiceMny = Convert.ToDecimal(txtInvoiceMny.Text);
                mod.InvoiceNo = txtInvoiceNo.Text.Trim();
                string detaillist = string.Empty;

                bool ret = false;
                if(_action=="NEW"|| _action=="ADD")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(mod.Id, mod, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
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

        private void txtCustName_DoubleClick(object sender, EventArgs e)
        {
            btnCust_Click(null, null);
        }

        private void txtSalesMan_DoubleClick(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtSalesMan);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboAccountNo.SelectedValue == null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Account No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboAccountNo.Focus();
                    return;
                }
                if (cboReceiveStatus.SelectedIndex == 1)
                {
                    if (cboAccountNo.SelectedIndex <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Account No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboAccountNo.Focus();
                        return;
                    }
                }
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
                bool ret = _dal.UpdateReceiveStatus(true, true, txtId.Text.Trim(), cboReceiveStatus.SelectedIndex, cboAccountNo.SelectedValue.ToString(), txtReceiveDate.Text.Trim(), cboInvoiceStatus.SelectedIndex, Convert.ToDecimal(txtInvoiceMny.Text), txtInvoiceNo.Text.Trim(), Util.UserId, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
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

        private void btnCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_CUSTOMER_LIST frm = new MTN_CUSTOMER_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalCustomerList dal = new DAL.dalCustomerList();
                    modCustomerList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        txtCustName.Tag = mod.CustId;
                        txtCustName.Text = mod.CustName;
                        txtPayMethod.Text = mod.PayMethod;
                        txtSalesMan.Text = mod.SalesMan;
                        txtNo.Text = _dal.GetNewNo(dtpFormDate.Value, mod.CustId);
                        txtCurrency.Text = mod.Currency;                        
                        if (mod.NeedInvoice == 1)
                        {
                            cboReceiveStatus.SelectedIndex = 0;
                            cboInvoiceStatus.SelectedIndex = 1;
                        }
                        else
                        {
                            cboReceiveStatus.SelectedIndex = 1;
                            cboInvoiceStatus.SelectedIndex = 0;
                        }
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

        private void btnSalesMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtSalesMan);
        }

        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnCust_Click(null, null);
            }
        }

        private void txtSalesMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtSalesMan);
            }
        }

        private void btnReceiveDate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReceiveDate.Text.Trim()))
                txtReceiveDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }
}
