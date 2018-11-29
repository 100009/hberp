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
    public partial class SYS_BULLETIN_FORM : BaseFormEdit
    {
        dalSysBulletin _dal = new dalSysBulletin();
        public SYS_BULLETIN_FORM()
        {
            InitializeComponent();
        }

        private void SYS_BULLETIN_FORM_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            InactiveVisible = false;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modSysBulletin> list = _dal.GetIList(true, out Util.emsg);
            DBGrid.DataSource = list;
            if (list != null)
            {
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
        
        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modSysBulletin mod = (modSysBulletin)DBGrid.Rows[i].DataBoundItem;
                if (mod.Id.ToString().CompareTo(FindText) == 0)
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
            txtId.Text = "0";
            txtId.ReadOnly = true;
            dtpStartTime.Value = DateTime.Now.AddMinutes(3);
            dtpEndTime.Value = DateTime.Now.AddDays(3);
            txtTitle.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            txtId.ReadOnly = true;
            txtTitle.Focus();
        }

        protected override bool Delete()
        {
            int? Id = Convert.ToInt32(txtId.Text);
            bool ret = _dal.Delete(Id, out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                int? Id = Convert.ToInt32(txtId.Text);
                dtpEndTime.Refresh();
                dtpStartTime.Refresh();
                modSysBulletin mod = new modSysBulletin(Id, txtTitle.Text.Trim(), txtMessage.Text.Trim(), txtAttachFile.Text.Trim(), dtpStartTime.Value, dtpEndTime.Value, Util.UserId, DateTime.Now);
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(Id, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    LoadData();
                    FindText = txtId.Text.Trim();
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
                modSysBulletin mod = (modSysBulletin)DBGrid.CurrentRow.DataBoundItem;
                txtId.Text = mod.Id.ToString();
                txtTitle.Text = mod.Title;
                txtMessage.Text = mod.Msg;
                txtAttachFile.Text = mod.AttachFile;
                dtpStartTime.Value = mod.StartTime;
                dtpEndTime.Value = mod.EndTime;
                FindText = mod.Id.ToString();
            }
        }

        private void btnImgPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtAttachFile.Text = ofd.FileName;
            }  
        }
    }
}