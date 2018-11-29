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
    public partial class ACC_CHECK_TYPE : BaseFormEdit
    {
        dalAccCheckType _dal = new dalAccCheckType();
        public ACC_CHECK_TYPE()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void ACC_CHECK_TYPE_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modAccCheckType> list = _dal.GetIList(out Util.emsg);
            DBGrid.DataSource = list;
            DBGrid.Enabled = true;
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
                modAccCheckType mod = (modAccCheckType)DBGrid.Rows[i].DataBoundItem;
                if (mod.CheckType.CompareTo(FindText) == 0)
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
            txtCheckType.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtCheckType.ReadOnly = true;
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtCheckType.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtCheckType.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Check type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCheckType.Focus();
                    return false;
                }
                modAccCheckType mod = new modAccCheckType(txtCheckType.Text.Trim(), Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtCheckType.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.CheckType;
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
                modAccCheckType mod = (modAccCheckType)DBGrid.CurrentRow.DataBoundItem;
                txtCheckType.Text = mod.CheckType;
                FindText = mod.CheckType;
            }
        }

        private void txtCheckType_KeyPress(object sender, KeyPressEventArgs e)
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