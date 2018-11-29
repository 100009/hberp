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
    public partial class ACC_SUBJECT_EXPENSE : Form
    {
        dalAccSubjectList _dal = new dalAccSubjectList();
        public ACC_SUBJECT_EXPENSE()
        {
            InitializeComponent();
        }

        private void ACC_SUBJECT_EXPENSE_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
            rb1.Checked = true;
            LoadData();
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            Util.retValue1 = string.Empty;
            Util.retValue2 = string.Empty;
            Util.retValue3 = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string subjectid=string.Empty;
                if (rb1.Checked)
                    subjectid = "913530";
                else if (rb2.Checked)
                    subjectid = "913535";
                else
                    subjectid = "913540";

                List<modAccSubjectList> list = _dal.GetIList(subjectid, false, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void toolOK_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;

            modAccSubjectList mod = (modAccSubjectList)DBGrid.CurrentRow.DataBoundItem;
            if (rb1.Checked)
                Util.retValue1 = "管理费用";
            else if (rb2.Checked)
                Util.retValue1 = "销售费用";
            else
                Util.retValue1 = "财务费用";

            Util.retValue2 = mod.SubjectId;
            Util.retValue3 = mod.SubjectName;
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            toolOK_Click(null, null);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string subjectid = string.Empty;
                if (rb1.Checked)
                    subjectid = "913530";
                else if (rb2.Checked)
                    subjectid = "913535";
                else
                    subjectid = "913540";
                EditSubjectList frm = new EditSubjectList();
                frm.AddItem(subjectid, -1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
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

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
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
    }
}
