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
    public partial class ACC_CURRENCY_LIST : BaseFormEdit
    {
        dalAccCurrencyList _dal = new dalAccCurrencyList();
        public ACC_CURRENCY_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void ACC_CURRENCY_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modAccCurrencyList> list = _dal.GetIList(out Util.emsg);
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
            checkboxColumn.HeaderText = clsTranslate.TranslateString("OwnerFlag");
            checkboxColumn.DataPropertyName = "OwnerFlag";
            checkboxColumn.Name = "OwnerFlag";
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

            Util.retValue1 = DBGrid.CurrentRow.Cells["currency"].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells["exchangerate"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modAccCurrencyList mod = (modAccCurrencyList)DBGrid.Rows[i].DataBoundItem;
                if (mod.Currency.CompareTo(FindText) == 0)
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
            txtCurrency.Focus();
        }

        protected override void Edit()
        {            
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtCurrency.ReadOnly = true;
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtCurrency.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtCurrency.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Currency") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCurrency.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtExchangeRate.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Exchange rate") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCurrency.Focus();
                    return false;
                }
                if (!Util.IsNumeric(txtExchangeRate.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Exchange rate") + clsTranslate.TranslateString(" must be numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtExchangeRate.Focus();
                    return false;
                }
                modAccCurrencyList mod = new modAccCurrencyList(txtCurrency.Text.Trim(), Convert.ToDecimal(txtExchangeRate.Text), 0, Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtCurrency.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.Currency;
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
                modAccCurrencyList mod = (modAccCurrencyList)DBGrid.CurrentRow.DataBoundItem;
                txtCurrency.Text = mod.Currency;
                txtExchangeRate.Text = mod.ExchangeRate.ToString();
                FindText = mod.Currency;
                if (txtExchangeRate.Text.Trim() == "1")
                {
                    EditVisible = false;
                    DelVisible = false;
                }
                else
                {
                    EditVisible = true;
                    DelVisible = true;
                }
            }
        }

        private void txtCurrency_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }
    }
}