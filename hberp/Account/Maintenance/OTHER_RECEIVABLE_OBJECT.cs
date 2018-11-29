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
    public partial class OTHER_RECEIVABLE_OBJECT : BaseFormEdit
    {
        dalOtherReceivableObject _dal = new dalOtherReceivableObject();
        public OTHER_RECEIVABLE_OBJECT()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void OTHER_RECEIVABLE_OBJECT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillCurrency(cboCurrency, false, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modOtherReceivableObject> list = _dal.GetIList(false, out Util.emsg);
            DBGrid.DataSource = list;
            DBGrid.Enabled = true;
            if (list != null)
            {
                AddComboBoxColumns();
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
            DBGrid.Columns.RemoveAt(1);
            DBGrid.Columns.Insert(1, checkboxColumn);
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modOtherReceivableObject mod = (modOtherReceivableObject)DBGrid.Rows[i].DataBoundItem;
                if (mod.ObjectName.CompareTo(FindText) == 0)
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
            cboCurrency.SelectedIndex = 0;
            txtObjectName.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtObjectName.ReadOnly = true;
            cboCurrency.Enabled = false;
            cboStatus.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtObjectName.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtObjectName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("cust type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObjectName.Focus();
                    return false;
                }
                modOtherReceivableObject mod = new modOtherReceivableObject();
                mod.ObjectName = txtObjectName.Text.Trim();
                mod.Status = cboStatus.SelectedIndex;
                mod.Currency = cboCurrency.SelectedValue.ToString();
                mod.LinkMan = txtLinkMan.Text.Trim();
                mod.Addr = txtAddr.Text.Trim();
                mod.Tel = txtTel.Text.Trim();
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtObjectName.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.ObjectName;
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
            bool ret = _dal.Inactive(txtObjectName.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtObjectName.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modOtherReceivableObject mod = (modOtherReceivableObject)DBGrid.CurrentRow.DataBoundItem;
                txtObjectName.Text = mod.ObjectName;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboCurrency.SelectedValue = mod.Currency;
                txtLinkMan.Text = mod.LinkMan;
                txtAddr.Text = mod.Addr;
                txtTel.Text = mod.Tel;
                txtRemark.Text = mod.Remark;
                FindText = mod.ObjectName;
            }
        }

        private void txtObjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
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