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
    public partial class EditAssetSale : Form
    {
        dalAssetSale _dal = new dalAssetSale();
        string _action;
        public EditAssetSale()
        {
            InitializeComponent();
            FillControl.FillCheckType(cboCheckType, false, true);
            FillControl.FillBankList(cboBankName, false, true);
        }

        private void EditAssetSale_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);            
            txtFormId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_ASSET_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
        }

        public void AddItem()
        {
            _action = "NEW";            
            dtpFormDate.Value = DateTime.Today;
            txtFormId.Text = _dal.GetNewId(dtpFormDate.Value);
            txtCurrency.Text = Util.Currency;
            txtExchangeRate.Text = "1";
            txtSubjectId.ReadOnly = true;
            txtSubjectName.ReadOnly = true;
            txtAssetId.ReadOnly = true;
            txtAssetName.ReadOnly = true;
            txtNetMny.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            status4.Image = null;
        }

        public void EditItem(string formid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";                
                modAssetSale mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;
                    txtNo.Text = mod.No;
                    txtAssetId.Text = mod.AssetId;
                    txtAssetName.Text = mod.AssetName;
                    txtNetMny.Text = mod.NetMny.ToString();
                    txtSaleMny.Text = mod.SaleMny.ToString();
                    txtSubjectId.Text = mod.SubjectId;
                    txtSubjectName.Text = mod.SubjectName;
                    txtDetailId.Text = mod.DetailId;
                    txtDetailName.Text = mod.DetailName;
                    txtCurrency.Text = mod.Currency;
                    txtExchangeRate.Text = mod.ExchangeRate.ToString();
                    txtCheckNo.Text = mod.CheckNo;
                    cboCheckType.Text = mod.CheckType;
                    cboBankName.Text = mod.BankName;
                    dtpPromiseDate.Value = mod.PromiseDate;
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
                        txtFormId.ReadOnly = true;
                        toolSave.Enabled = true;
                        if (mod.SubjectId == "1075" || mod.SubjectId == "5125")  //应收票据  应付票据
                        {
                            txtCheckNo.ReadOnly = false;
                            cboCheckType.Enabled = true;
                            cboBankName.Enabled = true;
                            dtpPromiseDate.Enabled = true;
                        }
                        else
                        {
                            txtCheckNo.ReadOnly = true;
                            cboCheckType.Enabled = false;
                            cboBankName.Enabled = false;
                            dtpPromiseDate.Enabled = false;
                        }
                    }
                    txtSubjectId.ReadOnly = true;
                    txtSubjectName.ReadOnly = true;
                    txtAssetId.ReadOnly = true;
                    txtAssetName.ReadOnly = true;
                    txtNetMny.ReadOnly = true;
                    txtCurrency.ReadOnly = true;
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
                if (string.IsNullOrEmpty(txtNetMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNetMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtNetMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNetMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSaleMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sale Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSaleMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtSaleMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sale Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSaleMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAssetName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Asset name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAssetName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSubjectId.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject FormId") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSubjectId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Currency") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCurrency.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtExchangeRate.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Exchange rate") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtExchangeRate.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtExchangeRate.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Exchange rate") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtExchangeRate.Focus();
                    return;
                }
                if (txtSubjectId.Text.Trim() == "1030" || txtSubjectId.Text.Trim() == "1055" || txtSubjectId.Text.Trim() == "1060" || txtSubjectId.Text.Trim() == "5145" || txtSubjectId.Text.Trim() == "5155")   //现金银行  应收 其它应收 应付 其它应付
                {
                    if (string.IsNullOrEmpty(txtDetailId.Text))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Detail id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDetailId.Focus();
                        return;
                    }
                }
                if (txtSubjectId.Text.Trim() == "1075" || txtSubjectId.Text.Trim() == "5125")  //应收票据  应付票据
                {
                    if (string.IsNullOrEmpty(txtCheckNo.Text))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Check no") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCheckNo.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(cboCheckType.Text))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Check type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboCheckType.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(cboBankName.Text))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Bank name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboBankName.Focus();
                        return;
                    }

                }
                if (clsLxms.GetParameterValue("NEED_ASSET_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }

                modAssetSale mod = new modAssetSale();
                mod.FormId = txtFormId.Text;
                mod.FormDate = dtpFormDate.Value;
                mod.No = txtNo.Text.Trim();
                mod.AssetId = txtAssetId.Text;
                mod.AssetName = txtAssetName.Text.Trim();
                mod.NetMny = Convert.ToDecimal(txtNetMny.Text);
                mod.SaleMny = Convert.ToDecimal(txtSaleMny.Text);
                mod.SubjectId = txtSubjectId.Text.Trim();
                mod.SubjectName = txtSubjectName.Text.Trim();
                mod.DetailId = txtDetailId.Text.Trim();
                mod.DetailName = txtDetailName.Text.Trim();
                mod.Currency = txtCurrency.Text.Trim();
                mod.ExchangeRate = Convert.ToDecimal(txtExchangeRate.Text);
                if (mod.SubjectId == "1075" || mod.SubjectId == "5125")
                {
                    mod.CheckNo = txtCheckNo.Text.Trim();
                    mod.CheckType = cboCheckType.Text.Trim();
                    mod.BankName = cboBankName.Text.Trim();
                }
                mod.PromiseDate = dtpPromiseDate.Value;
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                bool ret;
                if (_action == "ADD" || _action == "NEW")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(mod.FormId, mod, out Util.emsg);
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

        private void cboCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCheckType.SelectedValue == null) return;
            if (cboCheckType.SelectedValue.ToString().CompareTo("New...") != 0) return;

            ACC_CHECK_TYPE frm = new ACC_CHECK_TYPE();
            frm.ShowDialog();
            FillControl.FillCheckType(cboCheckType, false, true);
        }

        private void cboBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBankName.SelectedValue == null) return;
            if (cboBankName.SelectedValue.ToString().CompareTo("New...") != 0) return;

            ACC_BANK_LIST frm = new ACC_BANK_LIST();
            frm.ShowDialog();
            FillControl.FillBankList(cboBankName, false, true);
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ACC_SUBJECT_LIST frm = new ACC_SUBJECT_LIST();
                frm.ShowHideVisible(true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtSubjectId.Text = ACC_SUBJECT_LIST._mod.SubjectId;
                    txtSubjectName.Text = ACC_SUBJECT_LIST._mod.SubjectName;
                    txtCheckNo.ReadOnly = true;
                    cboCheckType.Enabled = false;
                    cboBankName.Enabled = false;
                    dtpPromiseDate.Enabled = false;
                    switch (ACC_SUBJECT_LIST._mod.SubjectId)
                    {
                        case "1030":   //现金银行
                            ACC_BANK_ACCOUNT frmaccount = new ACC_BANK_ACCOUNT();
                            frmaccount.SelectVisible = true;
                            if (frmaccount.ShowDialog() == DialogResult.OK)
                            {
                                txtDetailId.Text = Util.retValue1;
                                txtCurrency.Text = Util.retValue2;
                                txtExchangeRate.Text = Util.retValue3;
                            }
                            break;
                        case "5125":   //应付票据  
                        case "1075":   //应收票据                  
                            txtCheckNo.ReadOnly = false;
                            cboCheckType.Enabled = true;
                            cboBankName.Enabled = true;
                            dtpPromiseDate.Enabled = true;
                            break;
                        case "1055":   //应收帐款
                            MTN_CUSTOMER_LIST frmcust = new MTN_CUSTOMER_LIST();
                            frmcust.SelectVisible = true;
                            if (frmcust.ShowDialog() == DialogResult.OK)
                            {
                                txtDetailId.Text = Util.retValue1;
                                txtDetailName.Text = Util.retValue2;
                            }
                            break;
                        case "1060":   //其它应收款
                            OTHER_RECEIVABLE_OBJECT frmorec = new OTHER_RECEIVABLE_OBJECT();
                            frmorec.SelectVisible = true;
                            if (frmorec.ShowDialog() == DialogResult.OK)
                            {
                                txtDetailId.Text = Util.retValue1;
                            }
                            break;
                        case "5145":   //应付帐款
                            MTN_VENDOR_LIST frmvendor = new MTN_VENDOR_LIST();
                            frmvendor.SelectVisible = true;
                            if (frmvendor.ShowDialog() == DialogResult.OK)
                            {
                                txtDetailId.Text = Util.retValue1;
                            }
                            break;
                        case "5155":   //其它应付款
                            OTHER_PAYABLE_OBJECT frmopay = new OTHER_PAYABLE_OBJECT();
                            frmopay.SelectVisible = true;
                            if (frmopay.ShowDialog() == DialogResult.OK)
                            {
                                txtDetailId.Text = Util.retValue1;
                            }
                            break;
                        case "1235":   //库存商品
                            txtSubjectId.Text = string.Empty;
                            txtSubjectName.Text = string.Empty;
                            MessageBox.Show("不能选择这个科目", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                    }
                    cboCheckType.SelectedIndex = -1;
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

        private void txtSubjectId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnSubject_Click(null, null);
            }
        }

        private void btnCurrency_Click(object sender, EventArgs e)
        {
            ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtCurrency.Text = Util.retValue1;
                txtExchangeRate.Text = Util.retValue2;
            }
        }

        private void btnAssetId_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAssetList dal = new dalAssetList();
                BindingCollection<modAssetList> list = dal.GetIList("1", string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList("请选择资产：", list);
                    frm.Selection = true;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        modAssetList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                        if (mod != null)
                        {
                            txtAssetId.Text = mod.AssetId;
                            txtAssetName.Text = mod.AssetName;
                            txtNetMny.Text = mod.NetMny.ToString();
                        }
                        else
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
