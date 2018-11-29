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
    public partial class ACC_SUBJECT_LIST : Form
    {
        public static modAccSubjectList _mod = new modAccSubjectList();
        dalAccSubjectList _dal = new dalAccSubjectList();
        public ACC_SUBJECT_LIST()
        {
            InitializeComponent();
        }

        private void ACC_SUBJECT_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            //toolSelect.Visible = false;
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Add Brother"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddBrother_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Add Child"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddChild_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Edit"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuEdit_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Add Brother"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddBrother_Click));
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Add Child"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddChild_Click));
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Edit"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuEdit_Click));
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
            LoadData();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        public void ShowHideVisible(bool visible)
        {
            toolSelect.Visible = visible;            
        }

        private void toolSelect_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (DBGrid.CurrentRow == null) return;                
                _mod = (modAccSubjectList)DBGrid.CurrentRow.DataBoundItem;
            }
            else
            {
                if (DBGrid2.CurrentRow == null) return;
                _mod = (modAccSubjectList)DBGrid2.CurrentRow.DataBoundItem;
            }
            if (_mod.SelectFlag == 1)
            {
                _mod.SubjectName = _mod.SubjectName.Replace(".", "").Trim();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
                MessageBox.Show("SelectFlag=0,该科目不能作为一般凭证的科目!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                List<modAccSubjectList> list1 = _dal.GetIList("1", true, out Util.emsg);
                DBGrid.DataSource = list1;

                List<modAccSubjectList> list2 = _dal.GetIList("5", true, out Util.emsg);
                DBGrid2.DataSource = list2;
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

        private void Find(string subjectid)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (DBGrid.RowCount == 0) return;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells["SubjectId"].Value.ToString() == subjectid)
                    {
                        DBGrid.CurrentCell = DBGrid.Rows[i].Cells["SubjectId"];
                        break;
                    }
                }
            }
            else
            {
                if (DBGrid2.RowCount == 0) return;
                for (int i = 0; i < DBGrid2.RowCount; i++)
                {
                    if (DBGrid2.Rows[i].Cells["SubjectId"].Value.ToString() == subjectid)
                    {
                        DBGrid2.CurrentCell = DBGrid2.Rows[i].Cells["SubjectId"];
                        break;
                    }
                }
            }
        }

        private void mnuAddBrother_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (DBGrid.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid.CurrentRow.DataBoundItem;
                EditSubjectList frm = new EditSubjectList();
                frm.AddItem(mod.PSubjectId, mod.AdFlag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }            
            }
            else
            {
                if (DBGrid2.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid2.CurrentRow.DataBoundItem;
                EditSubjectList frm = new EditSubjectList();
                frm.AddItem(mod.PSubjectId, mod.AdFlag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }            
            }
        }

        private void mnuAddChild_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (DBGrid.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid.CurrentRow.DataBoundItem;
                if (mod.HasChildren==0)
                {
                    MessageBox.Show("该科目不能添加子科目!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                EditSubjectList frm = new EditSubjectList();
                frm.AddItem(mod.SubjectId, mod.AdFlag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }
            }
            else
            {
                if (DBGrid2.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid2.CurrentRow.DataBoundItem;
                if (mod.HasChildren == 0)
                {
                    MessageBox.Show("该科目不能添加子科目!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                EditSubjectList frm = new EditSubjectList();
                frm.AddItem(mod.SubjectId, mod.AdFlag);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }
            }
        }

        private void mnuEdit_Click(object sender, EventArgs e)
        {
            if (tab1.SelectedIndex == 0)
            {
                if (DBGrid.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid.CurrentRow.DataBoundItem;
                EditSubjectList frm = new EditSubjectList();
                frm.EditItem(mod);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }
            }
            else
            {
                if (DBGrid2.CurrentRow == null) return;
                modAccSubjectList mod = (modAccSubjectList)DBGrid2.CurrentRow.DataBoundItem;
                EditSubjectList frm = new EditSubjectList();
                frm.EditItem(mod);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    Find(Util.retValue1);
                }
            }
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                if (tab1.SelectedIndex == 0)
                {
                    if (DBGrid.CurrentRow == null) return;
                    if (DBGrid.CurrentRow.Cells["LockFlag"].Value.ToString() == "1")
                    {
                        MessageBox.Show("您不能删除该科目!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bool ret = _dal.Delete(DBGrid.CurrentRow.Cells["SubjectId"].Value.ToString(), out Util.emsg);
                    if (ret)
                    {
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (DBGrid2.CurrentRow == null) return;
                    if (DBGrid2.CurrentRow.Cells["LockFlag"].Value.ToString() == "1")
                    {
                        MessageBox.Show("您不能删除该科目!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bool ret = _dal.Delete(DBGrid2.CurrentRow.Cells["SubjectId"].Value.ToString(), out Util.emsg);
                    if (ret)
                    {
                        LoadData();
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

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(toolSelect.Visible)
                toolSelect_Click(null, null);
        }

        private void DBGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (toolSelect.Visible)
                toolSelect_Click(null, null);
        }
    }
}
