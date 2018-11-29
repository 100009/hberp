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
    public partial class ACC_BANK_ACCOUNT : BaseFormEdit
    {
        dalAccBankAccount _dal = new dalAccBankAccount();
        public ACC_BANK_ACCOUNT()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void ACC_BANK_ACCOUNT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillCurrency(cboCurrency, false, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modAccBankAccount> list = _dal.GetIList(out Util.emsg);
            DBGrid.DataSource = list;
            DBGrid.Enabled = true;
            if (list != null)
            {
                Status1 = DBGrid.Rows.Count.ToString();
                Status2 = clsTranslate.TranslateString("Refresh");
            }
            else
            {
                DBGrid.DataSource = null;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells["accountno"].Value.ToString();   //AccountNo
            Util.retValue2 = DBGrid.CurrentRow.Cells["currency"].Value.ToString();   //Currency 
            Util.retValue3 = DBGrid.CurrentRow.Cells["exchangerate"].Value.ToString();   //exchangerate 
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modAccBankAccount mod = (modAccBankAccount)DBGrid.Rows[i].DataBoundItem;
                if (mod.AccountNo.CompareTo(FindText) == 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    DBGrid_SelectionChanged(null, null);
                    return;
                }
            }
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            Util.EmptyFormBox(this);
            cboCurrency.SelectedIndex = 0;
            txtAccountNo.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtAccountNo.ReadOnly = true;
            txtBankName.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtAccountNo.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Account No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccountNo.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Bank Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtBankName.Focus();
                    return false;
                }
                if (cboCurrency.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Currency") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCurrency.Focus();
                    return false;
                }
                if (cboTaxFlag.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Tax Flag") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboTaxFlag.Focus();
                    return false;
                }
                modAccBankAccount mod = new modAccBankAccount();
                mod.AccountNo = txtAccountNo.Text.Trim();
                mod.BankName = txtBankName.Text.Trim();
                mod.Currency = cboCurrency.SelectedValue.ToString();
                mod.TaxFlag = cboTaxFlag.SelectedIndex == 0 ? 1 : 0;
                mod.UpdateUser = Util.UserId;
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtAccountNo.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.AccountNo;
                    Find();
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void Cancel()
        {
            Util.ChangeStatus(this, true);
            DBGrid.Enabled = true;
            DBGrid_SelectionChanged(null, null);
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modAccBankAccount mod = (modAccBankAccount)DBGrid.CurrentRow.DataBoundItem;
                txtAccountNo.Text = mod.AccountNo;
                txtBankName.Text = mod.BankName;
                cboCurrency.SelectedValue = mod.Currency;
                cboTaxFlag.SelectedIndex = mod.TaxFlag == 1 ? 0 : 1;
                FindText = mod.AccountNo;
            }
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency.SelectedValue == null) return;
            if (cboCurrency.SelectedValue.ToString().CompareTo("New...") != 0) return;

            ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
            frm.ShowDialog();
            FillControl.FillCurrency(cboCurrency, false, true);
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }
    }
}