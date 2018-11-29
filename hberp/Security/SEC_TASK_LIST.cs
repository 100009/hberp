using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class SEC_TASK_LIST : BaseFormEdit
    {
        dalTaskList _dal = new dalTaskList();
        string _groupid = string.Empty;
        public SEC_TASK_LIST()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void SEC_TASK_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            cboTaskType.Items.Add("OPERATION");
            cboTaskType.Items.Add("MAINTENANCE");
            cboTaskType.Items.Add("QUERY");
            InactiveVisible = false;
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
                dalTaskGroup dal = new dalTaskGroup();
                BindingCollection<modTaskGroup> list = dal.GetIList(true, out Util.emsg);
                if (list != null)
                {
                    foreach (modTaskGroup mod in list)
                    {
                        tvLeft.Nodes.Add(mod.GroupId, mod.GroupId, 0, 1);
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
                    _groupid = tvLeft.SelectedNode.Name;
                    BindingCollection<modTaskList> list = _dal.GetIList(_groupid, true, false, out Util.emsg);
                    DBGrid.DataSource = list;                    
                    Status1 = DBGrid.Rows.Count.ToString();
                    Status2 = clsTranslate.TranslateString("Refresh");                    
                }
                else
                    DBGrid.DataSource = null;
                Status3 = _groupid;
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
        
        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modTaskList mod = (modTaskList)DBGrid.Rows[i].DataBoundItem;
                if (mod.TaskCode.CompareTo(FindText) == 0 || mod.TaskName.IndexOf(FindText) > 0)
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
            cboTaskType.Text = string.Empty;
            txtTaskCode.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            txtTaskCode.ReadOnly = true;
            txtTaskName.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtTaskCode.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            if (string.IsNullOrEmpty(txtTaskCode.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("store id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTaskCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTaskName.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("store name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTaskName.Focus();
                return false;
            }            
            if (string.IsNullOrEmpty(_groupid))
            {
                MessageBox.Show(clsTranslate.TranslateString("Group Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tvLeft.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtUrl.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("Url ") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUrl.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtFormName.Text.Trim()))
            {
                MessageBox.Show(clsTranslate.TranslateString("Form name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFormName.Focus();
                return false;
            }   
            int? status = cboStatus.SelectedIndex;
            string password = Util.Encrypt(txtTaskCode.Text.Trim(), Util.PWD_MASK);
            modTaskList mod = new modTaskList(txtTaskCode.Text.Trim(), txtTaskName.Text.Trim(), status, _groupid, cboTaskType.Text.Trim(), txtUrl.Text.Trim(), txtWebUrl.Text.Trim(), txtFormName.Text.Trim(), txtRemark.Text.Trim(), Util.UserId, DateTime.Now);
            bool ret = false;
            if (_status == 1)
                ret = _dal.Insert(mod, out Util.emsg);
            else if (_status == 2)
                ret = _dal.Update(txtTaskCode.Text, mod, out Util.emsg);
            if (ret)
            {
                Util.ChangeStatus(this, true);
                LoadData();
                FindText = mod.TaskCode;
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
                modTaskList mod = (modTaskList)DBGrid.CurrentRow.DataBoundItem;
                txtTaskCode.Text = mod.TaskCode;
                txtTaskName.Text = mod.TaskName;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboTaskType.Text = mod.TaskType;
                txtUrl.Text = mod.Url;
                txtWebUrl.Text = mod.WebUrl;
                txtFormName.Text = mod.FormName;
                FindText = mod.TaskCode;
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
                    bool ret = _dal.UpdateTaskGroup(dr.Cells[0].Value.ToString(), index.Node.Name.ToString(), out Util.emsg);
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
            if (txtTaskCode.ReadOnly == true && e.Button == MouseButtons.Left)
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

        private void txtTaskCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtUrl_Validated(object sender, EventArgs e)
        {
            txtFormName.Text = txtUrl.Text;
        }
    }
}