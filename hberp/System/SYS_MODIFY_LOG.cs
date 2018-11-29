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
    public partial class SYS_MODIFY_LOG : BaseFormEdit
    {
        dalSysModifyLog _dal = new dalSysModifyLog();
        public SYS_MODIFY_LOG()
        {
            InitializeComponent();
        }

        private void SYS_MODIFY_LOG_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            InactiveVisible = false;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modSysModifyLog> list = _dal.GetIList(out Util.emsg);
            DBGrid.DataSource = list;
            if (list != null)
            {
                Status1 = DBGrid.Rows.Count.ToString();
                Status2 = clsTranslate.TranslateString("Refresh");
            }
            else
            {
                DBGrid.DataSource = null;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modSysModifyLog mod = (modSysModifyLog)DBGrid.Rows[i].DataBoundItem;
                if (mod.Version.ToString().CompareTo(FindText) == 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    return;
                }
            }
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            Util.EmptyFormBox(this);
            txtVersion.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            txtVersion.ReadOnly = true;
            txtTitle.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtVersion.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtVersion.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Version") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtVersion.Focus();
                    return false;
                }
                modSysModifyLog mod = new modSysModifyLog(txtVersion.Text.Trim(), txtTitle.Text.Trim(), txtContent.Text.Trim(), Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtVersion.Text.Trim(), mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    LoadData();
                    FindText = txtVersion.Text.Trim();
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
            DBGrid_SelectionChanged(null, null);
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modSysModifyLog mod = (modSysModifyLog)DBGrid.CurrentRow.DataBoundItem;
                txtVersion.Text = mod.Version.ToString();
                txtTitle.Text = mod.Title;
                txtContent.Text = mod.ModifyContent;
                FindText = mod.Version.ToString();
            }
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
    }
}