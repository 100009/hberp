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

namespace LXMS
{    
    public partial class EditSubjectList : Form
    {
        string _action;
        dalAccSubjectList _dal = new dalAccSubjectList();
        public EditSubjectList()
        {
            InitializeComponent();
        }

        private void EditSubjectList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if(string.IsNullOrEmpty(txtSubjectId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSubjectId.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSubjectName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSubjectName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAssistantCode.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Assistant Code") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAssistantCode.Focus();
                    return;
                }
                if (txtPSubjectId.Text.Trim()=="9135" && txtSubjectId.Text.Trim().IndexOf(txtPSubjectId.Text.Trim())<0)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Id") + clsTranslate.TranslateString(" is not correct!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSubjectId.Focus();
                    return;
                }
                if (txtSubjectId.Text.Trim().Length - txtPSubjectId.Text.Length != 2)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Id") + " 长度必须为：" + (txtPSubjectId.Text.Length+2).ToString() +" 位", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSubjectId.Focus();
                    return;
                }
                modAccSubjectList mod = new modAccSubjectList();
                mod.PSubjectId = txtPSubjectId.Text.Trim();
                mod.SubjectId = txtSubjectId.Text.Trim();
                mod.SubjectName = txtSubjectName.Text.Trim();
                mod.AssistantCode = txtAssistantCode.Text.Trim();
                mod.CheckCurrency = chkCheckCurrency.Checked ? 1 : 0;
                mod.AdFlag = Convert.ToInt32(cboAdFlag.Text);
                mod.IsQuantity = 0;
                mod.IsTradecompany = 0;
                mod.HasChildren = 0;
                mod.CheckFlag = 0;
                bool ret=false;
                if(_action=="ADD")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(txtSubjectId.Text.Trim(), mod, out Util.emsg);
                if (ret)
                {
                    Util.retValue1 = txtSubjectId.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        public void AddItem(string psubjectid, int? adflag)
        {
            _action = "ADD";
            txtPSubjectId.Text = psubjectid;
            txtSubjectId.Text = psubjectid;
            cboAdFlag.SelectedIndex = adflag == 1 ? 0 : 1;
            txtPSubjectId.ReadOnly = true;

        }

        public void EditItem(modAccSubjectList mod)
        {
            _action = "EDIT";
            txtPSubjectId.Text = mod.PSubjectId;
            txtSubjectId.Text = mod.SubjectId;
            txtSubjectName.Text = mod.SubjectName.Replace(".","").Trim();
            txtAssistantCode.Text = mod.AssistantCode;
            chkCheckCurrency.Checked = mod.CheckCurrency == 1 ? true : false;
            cboAdFlag.SelectedIndex = mod.AdFlag == 1 ? 0 : 1;
            txtPSubjectId.ReadOnly = true;
            txtSubjectId.ReadOnly = true;
            //chkCheckCurrency.Enabled = false;
        }

        private void txtSubjectName_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSubjectName.Text.Trim()))
            {
                txtAssistantCode.Text = Util.GetChineseSpell(txtSubjectName.Text.Trim());
            }
        }
    }
}
