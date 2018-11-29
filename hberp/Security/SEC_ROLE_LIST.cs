using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class SEC_ROLE_LIST : BaseFormEdit
    {
        dalRoleList _dal = new dalRoleList();
        public SEC_ROLE_LIST()
        {
            InitializeComponent();
        }

        private void SEC_USER_GROUP_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modRoleList> list = _dal.GetIList(false, out Util.emsg);
            DBGrid.DataSource = list;
            if (list != null)
            {
                AddComboBoxColumns();
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = false;
                DBGrid.ReadOnly = true;
                DBGrid.MultiSelect = false;
                //DBGrid.ReadOnly = true;
                DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DBGrid.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
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

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].ToString();
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modRoleList mod = (modRoleList)DBGrid.Rows[i].DataBoundItem;
                if (mod.RoleId.CompareTo(FindText) == 0)
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
            Util.EmptyFormBox(this);
            cboStatus.SelectedIndex = 1;
            txtRoleId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            txtRoleId.ReadOnly = true;
            txtRoleDesc.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtRoleId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            if (string.IsNullOrEmpty(txtRoleId.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("role id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRoleId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtRoleDesc.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("role desc") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRoleDesc.Focus();
                return false;
            }
            
            modRoleList mod = new modRoleList();
            mod.RoleId = txtRoleId.Text.Trim();
            mod.RoleDesc = txtRoleDesc.Text.Trim();
            mod.Status = cboStatus.SelectedIndex;
            mod.UpdateUser = Util.UserId;
            bool ret = false;
            if (_status == 1)
                ret = _dal.Insert(mod, out Util.emsg);
            else if (_status == 2)
                ret = _dal.Update(txtRoleId.Text, mod, out Util.emsg);
            if (ret)
            {
                Util.ChangeStatus(this, true);
                LoadData();
                FindText = mod.RoleId;
                Find();
            }

            return ret;
        }

        protected override void Cancel()
        {
            Util.ChangeStatus(this, true);
            DBGrid_SelectionChanged(null, null);
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modRoleList mod = (modRoleList)DBGrid.CurrentRow.DataBoundItem;
                txtRoleId.Text = mod.RoleId;
                txtRoleDesc.Text = mod.RoleDesc;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                FindText = mod.RoleId;
            }
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtRoleId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtRoleId.Text.Trim();
                Find();
            }
            return ret;
        }

        private void txtRoleId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
    }
}