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
    public partial class EditActionScheduleStatus : Form
    {
        dalCrmActionSchedule _dal = new dalCrmActionSchedule();
        public EditActionScheduleStatus()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void EditActionScheduleStatus_Load(object sender, EventArgs e)
        {

        }

        public void InitData(modCrmActionSchedule mod)
        {
            txtActionMan.Text = mod.ActionMan;
            txtActionContent.Text = mod.ActionContent;
            txtActionDate.Text = mod.ActionDate.ToString("yyyy-MM-dd");
            txtStatusDesc.Text = mod.StatusDesc;
            switch (mod.Status)
            {
                case 0:
                    rbActive.Checked = true;
                    break;
                case 1:
                    rbCompleted.Checked = true;
                    break;
                case 7:
                    rbCancelled.Checked = true;
                    break;
            }
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status=0;
                if(rbActive.Checked)
                    status=0;
                else if(rbCompleted.Checked)
                    status=1;
                else if(rbCancelled.Checked)
                    status=7;
                else
                {
                    MessageBox.Show("请选择一个状态!",clsTranslate.TranslateString("Information"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                bool ret = _dal.UpdateStatus(txtActionMan.Text, Convert.ToDateTime(txtActionDate.Text), status, txtStatusDesc.Text.Trim(), Util.UserId, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
