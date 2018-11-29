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
    public partial class MTN_DEPT_LIST : BaseFormEdit
    {
        dalAdminDeptList _dal = new dalAdminDeptList();
        public MTN_DEPT_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_DEPT_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modAdminDeptList> list = _dal.GetIList(false, out Util.emsg);
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
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modAdminDeptList mod = (modAdminDeptList)DBGrid.Rows[i].DataBoundItem;
                if (mod.DeptId.CompareTo(FindText) == 0)
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
            txtDeptId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtDeptId.ReadOnly = true;
            txtDeptDesc.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtDeptId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtDeptId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("dept id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDeptId.Focus();
                    return false;
                }
                //if (string.IsNullOrEmpty(txtDeptDesc.Text.Trim()))
                //{
                //    MessageBox.Show(clsTranslate.TranslateString("dept desc") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    txtDeptDesc.Focus();
                //    return false;
                //}
                modAdminDeptList mod = new modAdminDeptList(txtDeptId.Text.Trim(), txtDeptDesc.Text.Trim(), cboStatus.SelectedIndex, Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtDeptId.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.DeptId;
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
                modAdminDeptList mod = (modAdminDeptList)DBGrid.CurrentRow.DataBoundItem;
                txtDeptId.Text = mod.DeptId;
                txtDeptDesc.Text = mod.DeptDesc;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                FindText = mod.DeptId;
            }
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtDeptId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtDeptId.Text.Trim();
                Find();
            }
            return ret;
        }

        private void txtDeptId_KeyPress(object sender, KeyPressEventArgs e)
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
