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
    public partial class EditCustomerOrderList : Form
    {
        string _action;
        dalCustomerOrderList _dal = new dalCustomerOrderList();
        public EditCustomerOrderList()
        {
            InitializeComponent();
        }

        private void EditCustomerOrderList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtId.ReadOnly = true;
            txtSalesMan.ReadOnly = true;
            txtCustName.ReadOnly = true;
            txtProductName.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            if (clsLxms.ShowProductSize() == 1)
            {
                lblSize.Visible = true;
                lblSizeStar.Visible = true;
                txtSize.Visible = true;
            }
            else
            {
                lblSize.Visible = false;
                lblSizeStar.Visible = false;
                txtSize.Visible = false;
            }
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            dtpFormDate.Value = DateTime.Today;
            dtpRequireDate.Value = DateTime.Today;
            txtId.Text = "0";
            txtUnitNo.Text = "PCS";

            status4.Image = null;
        }

        public void EditItem(int id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                modCustomerOrderList mod = _dal.GetItem(id, out Util.emsg);
                if (mod != null)
                {
                    txtId.Text = id.ToString();
                    dtpFormDate.Value = mod.FormDate;
                    dtpRequireDate.Value = mod.RequireDate;
                    txtCustOrderNo.Text = mod.CustOrderNo;
                    txtCurrency.Text = mod.Currency;
                    txtCustName.Tag = mod.CustId;
                    txtCustName.Text = mod.CustName;
                    txtPayMethod.Text = mod.PayMethod;
                    txtSalesMan.Text = mod.SalesMan;
                    txtUnitNo.Text = mod.UnitNo;
                    txtQty.Text = mod.Qty.ToString();
                    txtPrice.Text = mod.Price.ToString();
                    txtProductName.Tag = mod.ProductId;
                    txtProductName.Text = mod.ProductName;
                    txtSize.Text = mod.Size.ToString();
                    txtRemark.Text = mod.Remark;                    
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

        public void CopyItem(modCustomerOrderList mod)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "NEW";
                txtId.Text = "0";
                dtpFormDate.Value = mod.FormDate;
                dtpRequireDate.Value = mod.RequireDate;
                txtCustOrderNo.Text = mod.CustOrderNo;
                txtCurrency.Text = mod.Currency;
                txtCustName.Tag = mod.CustId;
                txtCustName.Text = mod.CustName;
                txtPayMethod.Text = mod.PayMethod;
                txtSalesMan.Text = mod.SalesMan;
                txtUnitNo.Text = mod.UnitNo;
                //txtQty.Text = mod.Qty.ToString();
                //txtPrice.Text = mod.Price.ToString();
                //txtProductName.Tag = mod.ProductId;
                //txtProductName.Text = mod.ProductName;
                //txtSize.Text = mod.Size.ToString();
                txtRemark.Text = mod.Remark;                
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

        public void ImportItem(modVQuotationDetail mod)
        {
            _action = "NEW";
            dtpFormDate.Value = DateTime.Today;
            dtpRequireDate.Value = DateTime.Today;
            txtId.Text = "0";
            //txtCustOrderNo.Text = mod.CustOrderNo;
            txtCurrency.Text = mod.Currency;
            txtCustName.Tag = mod.CustId;
            txtCustName.Text = mod.CustName;
            dalCustomerList dalcust = new dalCustomerList();
            modCustomerList modcust = dalcust.GetItem(mod.CustId, out Util.emsg);
            if (modcust != null)
            {
                txtPayMethod.Text = modcust.PayMethod;
                txtSalesMan.Text = modcust.SalesMan;
            }
            txtUnitNo.Text = mod.UnitNo;
            txtQty.Text = mod.Qty.ToString();
            txtPrice.Text = mod.Price.ToString();
            txtProductName.Tag = mod.ProductId;
            txtProductName.Text = mod.ProductName;
            txtSize.Text = "1";
            txtRemark.Text = mod.Remark;
            
            status4.Image = null;
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
                if (string.IsNullOrEmpty(txtSize.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSize.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtSize.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSize.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtSize.Text.Trim()) <= 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSize.Focus();
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
                if (string.IsNullOrEmpty(txtPrice.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPrice.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtPrice.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPrice.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCustOrderNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Order No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustOrderNo.Focus();
                    return;
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
                if (txtProductName.Tag==null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductName.Focus();
                    return;
                }
                modCustomerOrderList mod = new modCustomerOrderList();
                mod.Id = Convert.ToInt32(txtId.Text);
                mod.FormDate = dtpFormDate.Value;
                mod.RequireDate = dtpRequireDate.Value;
                mod.CustOrderNo = txtCustOrderNo.Text.Trim();
                mod.CustId = txtCustName.Tag.ToString();
                mod.CustName = txtCustName.Text.Trim();
                mod.PayMethod = txtPayMethod.Text.Trim();
                mod.SalesMan = txtSalesMan.Text.Trim();
                mod.ProductId = txtProductName.Tag.ToString();
                mod.ProductName = txtProductName.Text.Trim();
                if (clsLxms.GetProductSizeFlag(mod.ProductId) == 0)
                    mod.Size = 1;
                else
                    mod.Size = Convert.ToDecimal(txtSize.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.Currency = txtCurrency.Text.Trim();
                mod.UnitNo = txtUnitNo.Text.Trim();
                mod.Qty = Convert.ToDecimal(txtQty.Text);
                mod.Price = Convert.ToDecimal(txtPrice.Text);
                mod.UpdateUser = Util.UserId;
                string detaillist = string.Empty;

                bool ret = false;
                if (_action == "ADD" || _action == "NEW")
                {
                    ret = _dal.Insert(mod, out Util.emsg);
                    if (ret)
                    {
                        if (MessageBox.Show("保存成功，是否继续添加？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            txtProductName.Tag = null;
                            txtProductName.Text = string.Empty;
                            txtSize.Text = string.Empty;
                            txtUnitNo.Text = string.Empty;
                            txtQty.Text = string.Empty;
                            txtPrice.Text = string.Empty;
                            txtRemark.Text = string.Empty;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
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
                        txtCurrency.Text = mod.Currency;                        
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

        private void btnProduct_Click(object sender, EventArgs e)
        {
            MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dalProductList dalpdt = new dalProductList();
                modProductList modpdt = dalpdt.GetItem(Util.retValue1, out Util.emsg);
                txtProductName.Tag = modpdt.ProductId;
                txtProductName.Text = modpdt.ProductName;
                txtUnitNo.Text = modpdt.UnitNo;
                if (modpdt.SizeFlag == 1)
                {
                    txtSize.Enabled = true;
                    txtSize.Text = "0";
                }
                else
                {
                    txtSize.Enabled = false;
                    txtSize.Text = "1";
                }
                txtQty.Focus();
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (Util.IsNumeric(txtQty.Text.Trim()) && Util.IsNumeric(txtPrice.Text.Trim()))
            {
                Status1.Text = "金额：" + (decimal.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text)).ToString();
            }
        }
    }
}
