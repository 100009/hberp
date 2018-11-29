using System;
using System.Collections;
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
    public partial class SET_REMARK_LIST : BaseFormEdit
    {
        dalRemarkList _dal = new dalRemarkList();
        string _remarktype=string.Empty;
        public SET_REMARK_LIST()
        {
            InitializeComponent();
        }

        private void SET_REMARK_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            Util.retValue1 = string.Empty;
            EditVisible = false;
            InactiveVisible = false;
            DBGrid.Tag = this.Text;
            LoadTree();
        }

        public string RemarkType
        {
            get { return _remarktype; }
            set { _remarktype = value; }
        }

        private void LoadTree()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                tvLeft.AllowDrop = true;
                tvLeft.Nodes.Clear();
                tvLeft.ImageList = Util.GetImageList();                
                tvLeft.Nodes.Add("SPECIALMEMO", clsTranslate.TranslateString("SPECIALMEMO"), 0, 1);
                tvLeft.Nodes.Add("REMARK", clsTranslate.TranslateString("REMARK"), 0, 1);
                tvLeft.Nodes.Add("CUSTOMER_REQUIREMENT", clsTranslate.TranslateString("CUSTOMERREQUIREMENT"), 0, 1);
                tvLeft.SelectedNode = tvLeft.Nodes[0];
                
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
                    _remarktype = tvLeft.SelectedNode.Name;
                    BindingCollection<modRemarkList> list = _dal.GetIList(_remarktype, out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list != null && list.Count > 0)
                    {
                        DBGrid.AllowUserToAddRows = false;
                        DBGrid.AllowUserToDeleteRows = false;
                        DBGrid.ReadOnly = true;
                        DBGrid.MultiSelect = false;
                        DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        DBGrid.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
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
                Status3 = _remarktype;
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
                modRemarkList mod = (modRemarkList)DBGrid.Rows[i].DataBoundItem;
                if (mod.Remark.IndexOf(FindText)>=0)
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

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].ToString();
            this.Dispose();
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            Util.EmptyFormBox(this);            
            txtRemark.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(_remarktype, txtRemark.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("store id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRemark.Focus();
                    return false;
                }                
                if (string.IsNullOrEmpty(_remarktype))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Type Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tvLeft.Focus();
                    return false;
                }                
                modRemarkList mod = new modRemarkList(_remarktype, txtRemark.Text.Trim(), Util.UserId, DateTime.Now);
                bool ret = _dal.Insert(mod, out Util.emsg);               
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    LoadData();
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
            DBGrid_SelectionChanged(null, null);
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modRemarkList mod = (modRemarkList)DBGrid.CurrentRow.DataBoundItem;
                txtRemark.Text = mod.Remark;                
            }
            else
            {
                Util.EmptyFormBox(this);
            }
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRemark.Text.Trim())) return;
            
            Util.retValue1 = txtRemark.Text;
            this.Dispose();
        }
    }
}

