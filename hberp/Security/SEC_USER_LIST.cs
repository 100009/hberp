using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class SEC_USER_LIST : BaseFormEdit
    {
        dalUserList _dal = new dalUserList();
        string _roleid = string.Empty;
        public SEC_USER_LIST()
        {
            InitializeComponent();
        }
        
        private void SEC_USER_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Reset Current Password"), null, new System.EventHandler(ResetCurrentPassword_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Reset All Password"), null, new System.EventHandler(ResetAllPassword_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Set User Privilege"), null, new System.EventHandler(SetUserPrivilege_Click));
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillRoleList(cboRoleId, false);
            LoadTree();
        }

        private void LoadTree()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                tvLeft.AllowDrop = true;
                tvLeft.Nodes.Clear();
                tvLeft.ImageList = Util.GetImageList();
                dalRoleList bll = new dalRoleList();
                BindingCollection<modRoleList> list = bll.GetIList(true, out Util.emsg);
                if (list != null)
                {
                    foreach (modRoleList mod in list)
                    {
                        tvLeft.Nodes.Add(mod.RoleId, mod.RoleDesc, 0, 1);
                    }
                    if (tvLeft.Nodes.Count > 0)
                        tvLeft.SelectedNode = tvLeft.Nodes[0];
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

        private void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tvLeft.SelectedNode.Level == 0)
                {
                    _roleid = tvLeft.SelectedNode.Name;
                    BindingCollection<modUserList> list = _dal.GetIList(_roleid, out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list != null && list.Count > 0)
                    {
                        DBGrid.Columns["Password"].Visible = false;
                        AddComboBoxColumns();                        
                        Status1 = DBGrid.Rows.Count.ToString();
                        Status2 = clsTranslate.TranslateString("Refresh");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //else
                        //    MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                    DBGrid.DataSource = null;
                Status3 = _roleid;
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
            checkboxColumn.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modUserList mod = (modUserList)DBGrid.Rows[i].DataBoundItem;
                if (mod.UserId.CompareTo(FindText) == 0 || mod.UserName.IndexOf(FindText) > 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    DBGrid_SelectionChanged(null, null);
                    return;
                }
            }
        }

        protected override void Refresh()
        {
            LoadTree();
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            Util.EmptyFormBox(this);
            cboStatus.SelectedIndex = 1;
            cboRoleId.SelectedValue = _roleid;
            txtUserId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            txtUserId.ReadOnly = true;
            txtUserName.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtUserId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("store id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("store name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return false;
            }
            if (cboRoleId.SelectedValue==null)
            {
                MessageBox.Show(clsTranslate.TranslateString("Role Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboRoleId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(_roleid))
            {
                MessageBox.Show(clsTranslate.TranslateString("Role Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tvLeft.Focus();
                return false;
            }
            int? status = cboStatus.SelectedIndex;
            string password = Util.Encrypt(txtUserId.Text.Trim(), Util.PWD_MASK);
            
            modUserList mod = new modUserList(txtUserId.Text.Trim(), txtUserName.Text.Trim(), status, password, cboRoleId.SelectedValue.ToString(), Util.UserId, DateTime.Now, txtEmail.Text.Trim());
            bool ret = false;
            if (_status == 1)
                ret = _dal.Insert(mod, out Util.emsg);
            else if (_status == 2)
                ret = _dal.Update(txtUserId.Text, mod, out Util.emsg);
            if (ret)
            {
                Util.ChangeStatus(this, true);
                LoadData();
                FindText = mod.UserId;
                Find();
            }
            return ret;
        }

        protected override void Cancel()
        {
            Util.ChangeStatus(this, true);
            DBGrid_SelectionChanged(null, null);
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtUserId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtUserId.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modUserList mod = (modUserList)DBGrid.CurrentRow.DataBoundItem;
                txtUserId.Text = mod.UserId;
                txtUserName.Text = mod.UserName;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboRoleId.SelectedValue = mod.RoleId;
                txtEmail.Text = mod.Email;                
                FindText = mod.UserId;
            }
            else
            {
                Util.EmptyFormBox(this);
            }
        }

        private void tvLeft_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                Point p = tvLeft.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = tvLeft.HitTest(p);
                if (index.Node != null)
                {
                    DataGridViewRow dr = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                    bool ret = _dal.UpdateRole(dr.Cells[0].Value.ToString(), index.Node.Name.ToString(), out Util.emsg);
                    if (!ret)
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        tvLeft.SelectedNode = index.Node;
                        TreeViewEventArgs ea = new TreeViewEventArgs(index.Node);
                        tvLeft_AfterSelect(null, ea);
                        tvLeft.Focus();
                    }
                }
            }
        }

        private void DBGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtUserId.ReadOnly == true && e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo info = DBGrid.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    DataGridViewRow dr = (DataGridViewRow)DBGrid.Rows[info.RowIndex];
                    if (dr != null)
                        DBGrid.DoDragDrop(dr, DragDropEffects.Copy);
                }
            }
        }

        private void tvLeft_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void ResetCurrentPassword_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;

                DialogResult result = MessageBox.Show("Do you really want to reset password?", DBGrid.CurrentRow.Cells[1].Value.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    string password = Util.Encrypt(DBGrid.CurrentRow.Cells[0].Value.ToString(), Util.PWD_MASK);
                    bool ret = _dal.ResetPassword(DBGrid.CurrentRow.Cells[0].Value.ToString(), password, out Util.emsg);
                    if (!ret)
                    {
                        MessageBox.Show(Util.emsg,clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Reset success", clsTranslate.TranslateString("Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ResetAllPassword_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to init password that is null?", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                bool ret = _dal.InitPassword(out Util.emsg);
                if (!ret)
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Reset all password success!", clsTranslate.TranslateString("Success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }      
        }

        private void SetUserPrivilege_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            if (txtUserName.ReadOnly == false) return;
            UserPrivilegeEdit frm = new UserPrivilegeEdit();
            frm.UserId = txtUserId.Text.Trim();
            frm.UserName = txtUserName.Text.Trim();
            frm.ShowDialog();
        }
    }
}