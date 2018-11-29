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
    public partial class EditWarehouseInout : Form
    {
        string _action;
        dalWarehouseInoutForm _dal = new dalWarehouseInoutForm();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditWarehouseInout()
        {
            InitializeComponent();
        }

        private void EditWarehouseInout_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtId.ReadOnly = true;
            txtProductId.ReadOnly = true;
            txtWarehouseId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_INOUT_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
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
            FillControl.FillWarehouseInoutType(cboInoutType, 0, false, false);
            cboInoutType.SelectedIndex = -1;
            txtId.Text = "0";
            dtpInoutDate.Value = DateTime.Today;
            if (dtpInoutDate.Value > Util.modperiod.EndDate)
                dtpInoutDate.Value = Util.modperiod.EndDate;
            txtSize.Text = "1";
            txtWarehouseId.Text = clsLxms.GetDefaultWarehouseId();
            status4.Image = null;
        }

        public void EditItem(int id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                FillControl.FillWarehouseInoutType(cboInoutType, 0, false, true);
                modWarehouseInoutForm mod = _dal.GetItem(id, out Util.emsg);
                if (mod != null)
                {
                    txtId.Text = id.ToString();
                    dtpInoutDate.Value = mod.InoutDate;
                    cboInoutType.SelectedValue = mod.InoutType;
                    txtNo.Text = mod.No;
                    txtProductId.Text = mod.ProductId;
                    txtProductName.Text = mod.ProductName;
                    txtSize.Text = mod.Size.ToString();
                    txtQty.Text = mod.Qty.ToString();
                    txtWarehouseId.Text = mod.WarehouseId.ToString();
                    txtRemark.Text = mod.Remark;
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
                    dalProductList dalpdt = new dalProductList();
                    modProductList modpdt = dalpdt.GetItem(mod.ProductId, out Util.emsg);
                    if (modpdt.SizeFlag == 1)
                        txtSize.Enabled = true;
                    else
                        txtSize.Enabled = false;
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
                if (dtpInoutDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpInoutDate.Focus();
                    return;
                }
                if (cboInoutType.SelectedValue == null || string.IsNullOrEmpty(cboInoutType.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Inout Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboInoutType.Focus();
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
                else if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProductId.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtProductId.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtWarehouseId.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Warehouse id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWarehouseId.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_INOUT_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }
                
                modWarehouseInoutForm mod = new modWarehouseInoutForm();
                mod.Id = Convert.ToInt32(txtId.Text);
                mod.InoutDate = dtpInoutDate.Value;
                mod.InoutType = cboInoutType.SelectedValue.ToString();
                mod.No = txtNo.Text.Trim();
                mod.ProductId = txtProductId.Text.Trim();
                mod.ProductName = txtProductName.Text.Trim();
                if (clsLxms.GetProductSizeFlag(mod.ProductId) == 0)
                    mod.Size = 1;
                else
                    mod.Size = Convert.ToDecimal(txtSize.Text);
                mod.Qty = Convert.ToDecimal(txtQty.Text);
                mod.CostPrice = clsLxms.GetPrice(mod.ProductId);
                mod.WarehouseId = txtWarehouseId.Text.Trim();
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                bool ret;
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
                            txtQty.Text = string.Empty;
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

        private void txtProductId_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalProductList dal = new DAL.dalProductList();
                    modProductList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        txtProductId.Text = mod.ProductId;
                        txtProductName.Text = mod.ProductName;
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

        private void txtWarehouseId_DoubleClick(object sender, EventArgs e)
        {
            btnWarehouse_Click(null, null);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {            
            dalWarehouseProductInout dal=new dalWarehouseProductInout();
            BindingCollection<modProductSizeWip> list = dal.GetProductSizeWip(Util.modperiod.AccName, out Util.emsg);
            frmViewList frm = new frmViewList();
            frm.InitViewList("请选择产品及尺寸：", list);
            frm.Selection = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dalProductList dalpdt = new dalProductList();
                modProductList modpdt = dalpdt.GetItem(Util.retValue1, out Util.emsg);
                txtProductId.Text = Util.retValue1;
                txtProductName.Text = Util.retValue2;
                txtSize.Text = Util.retValue3;
                if (modpdt.SizeFlag == 1)
                    txtSize.Enabled = true;
                else
                    txtSize.Enabled = false;
                txtQty.Focus();
            }
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_WAREHOUSE_LIST frm = new MTN_WAREHOUSE_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtWarehouseId.Text = Util.retValue1;
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

        private void txtProductId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                txtProductId_DoubleClick(null, null);
            }
        }

        private void txtWarehouseId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnWarehouse_Click(null, null);
            }
        }

        private void txtProductId_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProductId.Text.Trim()) && string.IsNullOrEmpty(txtProductName.Text.Trim()))
            {
                dalProductList dalpdt = new dalProductList();
                modProductList modpdt = dalpdt.GetItem(txtProductId.Text.Trim(), out Util.emsg);
                if (modpdt != null)
                {
                    //txtProductId.Text = Util.retValue1;
                    txtProductName.Text = modpdt.ProductName;
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
                else
                {
                    MessageBox.Show("该产品编号不存在！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProductId.Focus();
                }
            }
        }
    }
}
