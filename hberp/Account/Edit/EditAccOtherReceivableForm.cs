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
    public partial class EditAccOtherReceivableForm : Form
    {
        string _action;
        dalAccOtherReceivableForm _dal = new dalAccOtherReceivableForm();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditAccOtherReceivableForm()
        {
            InitializeComponent();
            FillControl.FillCheckType(cboCheckType, false, true);
            FillControl.FillBankList(cboBankName, false, true);
            FillControl.FillOtherReceivableType(cboFormType, false);
        }

        private void EditAccOtherReceivableForm_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);            
            txtId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_OTHER_RECEIVABLE_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            txtId.Text = "0";
            //dtpFormDate.Value = DateTime.Today;
            dtpFormDate.Value = Util.modperiod.EndDate <= DateTime.Today ? Util.modperiod.EndDate : DateTime.Today;
            txtCurrency.Text = Util.Currency;
            txtExchangeRate.Text = "1";
            txtObjectName.ReadOnly = true;
            txtSubjectId.ReadOnly = true;
            txtSubjectName.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            cboFormType.SelectedIndex = -1;
            status4.Image = null;
        }

        public void EditItem(int id)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";                
                modAccOtherReceivableForm mod = _dal.GetItem(id, out Util.emsg);
                if (mod != null)
                {
                    txtId.Text = id.ToString();
                    dtpFormDate.Value = mod.FormDate;
                    cboFormType.SelectedValue = mod.AdFlag.ToString();
                    txtNo.Text = mod.No;
                    txtObjectName.Text = mod.ObjectName;
                    txtGetMny.Text = mod.GetMny.ToString();
                    txtReceivable.Text = mod.ReceivableMny.ToString();
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
                        txtId.ReadOnly = true;
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
                    txtObjectName.ReadOnly = true;
                    txtSubjectId.ReadOnly = true;
                    txtSubjectName.ReadOnly = true;
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
                if (cboFormType.SelectedIndex==-1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Form type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboFormType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtGetMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Get mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtGetMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtGetMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Get mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtGetMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtReceivable.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Receivable mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReceivable.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtReceivable.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Receivable mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReceivable.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtObjectName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObjectName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSubjectId.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (clsLxms.GetParameterValue("NEED_OTHER_RECEIVABLE_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }

                modAccOtherReceivableForm mod = new modAccOtherReceivableForm();
                mod.Id = Convert.ToInt32(txtId.Text);
                mod.FormDate = dtpFormDate.Value;
                mod.FormType = cboFormType.Text;
                mod.AdFlag = Convert.ToInt32(cboFormType.SelectedValue);
                mod.No = txtNo.Text.Trim();
                mod.ObjectName = txtObjectName.Text.Trim();
                mod.GetMny = Convert.ToDecimal(txtGetMny.Text);
                mod.ReceivableMny = Convert.ToDecimal(txtReceivable.Text);
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
                    ShowHideCheckControl(true);
                }
                else
                {
                    ShowHideCheckControl(false);
                }
                mod.PromiseDate = dtpPromiseDate.Value;
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
                            txtObjectName.Text = string.Empty;
                            txtGetMny.Text = string.Empty;
                            txtReceivable.Text = string.Empty;
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
        private void ShowHideCheckControl(bool show)
        {
            lblCheckNo.Visible = show;
            txtCheckNo.Visible = show;
            lblCheckType.Visible = show;
            cboCheckType.Visible = show;
            lblBankName.Visible = show;
            cboBankName.Visible = show;
            lblPromiseDate.Visible = show;
            dtpPromiseDate.Visible = show;
        }
        private void txtObjectName_DoubleClick(object sender, EventArgs e)
        {
            btnObject_Click(null, null);
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

        private void btnObject_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                OTHER_RECEIVABLE_OBJECT frm = new OTHER_RECEIVABLE_OBJECT();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txtObjectName.Text = Util.retValue1;
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
                    //txtCheckNo.ReadOnly = true;
                    //cboCheckType.Enabled = false;
                    //cboBankName.Enabled = false;
                    //dtpPromiseDate.Enabled = false;
                    ShowHideCheckControl(false);
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
                        case "1075":   //应收票据
                        case "5125":   //应付票据
                            //txtCheckNo.ReadOnly = false;
                            //cboCheckType.Enabled = true;
                            //cboBankName.Enabled = true;
                            //dtpPromiseDate.Enabled = true;
                            ShowHideCheckControl(true);
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

        private void txtGetMny_TextChanged(object sender, EventArgs e)
        {
            if (txtGetMny.ReadOnly == false)
            {
                txtReceivable.Text = txtGetMny.Text;
            }
        }

        private void txtObjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnObject_Click(null, null);
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
    }
}