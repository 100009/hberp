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
    public partial class ACC_COMMON_DIGEST : BaseFormEdit
    {
        dalAccCommonDigest _dal = new dalAccCommonDigest();
        public ACC_COMMON_DIGEST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void ACC_COMMON_DIGEST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            DBGrid.Tag = this.Text;
            FillControl.FillCommonDigestType(lstDigestType);
        }

        private void lstDigestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (lstDigestType.SelectedValue == null) return;
            BindingCollection<modAccCommonDigest> list = _dal.GetIList(lstDigestType.SelectedValue.ToString(), out Util.emsg);
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
            FillControl.FillCommonDigestType(lstDigestType);
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
                modAccCommonDigest mod = (modAccCommonDigest)DBGrid.Rows[i].DataBoundItem;
                if (mod.DigestType.CompareTo(FindText) == 0)
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
            txtDigest.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtDigest.ReadOnly = true;
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtDigest.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtDigest.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Digest") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDigest.Focus();
                    return false;
                }
                modAccCommonDigest mod = new modAccCommonDigest(txtDigest.Text.Trim(), lstDigestType.SelectedValue.ToString(), Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtDigest.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.DigestType;
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
                modAccCommonDigest mod = (modAccCommonDigest)DBGrid.CurrentRow.DataBoundItem;
                txtDigest.Text = mod.DigestType;
                FindText = mod.DigestType;
            }
        }

        private void txtDigest_KeyPress(object sender, KeyPressEventArgs e)
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