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
    public partial class MTN_VENDOR_LIST : BaseFormEdit
    {
        dalVendorList _dal = new dalVendorList();
        public MTN_VENDOR_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_VENDOR_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillVendorType(cboVendorType, false, true);
            FillControl.FillCurrency(cboCurrency, false, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modVendorList> list = _dal.GetIList(string.Empty, string.Empty, out Util.emsg);
            DBGrid.DataSource = list;
            DBGrid.Enabled = true;
            if (list != null)
            {
                AddComboBoxColumns();
                Status1 = DBGrid.Rows.Count.ToString();
                Status2 = clsTranslate.TranslateString("Refresh");
            }
            else
            {
                DBGrid.DataSource = null;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells[1].Value.ToString();
            Util.retValue3 = DBGrid.CurrentRow.Cells["Currency"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modVendorList mod = (modVendorList)DBGrid.Rows[i].DataBoundItem;
                if (mod.VendorName.CompareTo(FindText) == 0)
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
            cboStatus.SelectedIndex = 1;
            cboNeedInvoice.SelectedIndex = -1;
            cboVendorType.SelectedIndex = -1;
            cboCurrency.SelectedIndex = 0;
            txtVendorName.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtVendorName.ReadOnly = true;
            cboCurrency.Enabled = false;
            cboVendorType.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtVendorName.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtVendorName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("vendor name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtVendorName.Focus();
                    return false;
                }
                if (cboVendorType.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("vendor type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboVendorType.Focus();
                    return false;
                }
                if (Util.IsChina(txtNo.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Purchase no") + clsTranslate.TranslateString(" can not be chinese!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return false;
                }
                if (cboNeedInvoice.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Need invoice") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboNeedInvoice.Focus();
                    return false;
                }
                modVendorList mod = new modVendorList();
                mod.VendorName = txtVendorName.Text.Trim();
                mod.VendorType = cboVendorType.Text;
                mod.Status=cboStatus.SelectedIndex;
                mod.Currency = cboCurrency.SelectedValue.ToString();
                mod.No = txtNo.Text.Trim().Replace("*","");
                mod.Linkman=txtLinkman.Text.Trim();
                mod.Tel=txtTel.Text.Trim();
                mod.Fax=txtFax.Text.Trim();
                mod.Addr=txtAddr.Text.Trim();
                mod.Remark=txtRemark.Text.Trim();
                mod.NeedInvoice = cboNeedInvoice.SelectedIndex;
                mod.UpdateUser=Util.UserId;
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtVendorName.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.VendorName;
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

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtVendorName.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtVendorName.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modVendorList mod = (modVendorList)DBGrid.CurrentRow.DataBoundItem;
                txtVendorName.Text = mod.VendorName;
                cboVendorType.SelectedValue = mod.VendorType;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboCurrency.SelectedValue = mod.Currency;
                txtNo.Text = mod.No;
                txtLinkman.Text = mod.Linkman;
                txtTel.Text = mod.Tel;
                txtFax.Text = mod.Fax;
                txtAddr.Text = mod.Addr;
                txtRemark.Text = mod.Remark;
                cboNeedInvoice.SelectedIndex = mod.NeedInvoice;
                FindText = mod.VendorName;
            }
        }

        private void txtVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }

        private void cboVendorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboVendorType.SelectedValue == null) return;
            if (cboVendorType.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_VENDOR_TYPE frm = new MTN_VENDOR_TYPE();
            frm.ShowDialog();
            FillControl.FillVendorType(cboVendorType, false, true);
        }

        private void txtVendorName_Validated(object sender, EventArgs e)
        {
            if (txtVendorName.ReadOnly) return;
            if (!string.IsNullOrEmpty(txtVendorName.Text.Trim()))
            {
                txtNo.Text = Util.GetChineseSpell(txtVendorName.Text.Trim());
            }
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency.SelectedValue == null) return;
            if (cboCurrency.SelectedValue.ToString().CompareTo("New...") != 0) return;

            ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
            frm.ShowDialog();
            FillControl.FillCurrency(cboCurrency, false, true);
        }
    }
}