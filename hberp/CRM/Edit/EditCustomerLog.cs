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
    public partial class EditCustomerLog : Form
    {
        dalCustomerLog _dal = new dalCustomerLog();
        string _action = string.Empty;
        public EditCustomerLog()
        {
            InitializeComponent();
        }

        private void EditCustomerLog_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillCustomerScoreRule(cboActionType, "1", false);
            txtCustId.ReadOnly = true;
            txtCustName.ReadOnly = true;
            txtActionMan.ReadOnly = true;
            txtId.ReadOnly = true;
        }

        public void AddItem(string custid, string custname)
        {
            _action = "NEW";
            txtCustId.Text = custid;
            txtCustName.Text = custname;
            txtActionMan.Text = Util.UserName;
            txtId.Text = "0";
            cboActionType.Focus();
        }

        public void EditItem(modCustomerLog mod)
        {
            txtCustId.Text = mod.CustId;
            txtCustName.Text = mod.CustName;
            txtId.Text = mod.Id.ToString();
            cboActionType.SelectedValue = mod.ActionCode;
            txtActionSubject.Text = mod.ActionSubject;
            txtActionMan.Text = mod.ActionMan;
            txtObjectName.Text = mod.ObjectName;
            txtVenue.Text = mod.Venue;
            dtpFromTime.Text = mod.FromTime;
            dtpToTime.Text = mod.ToTime;
            txtActionContent.Text = mod.ActionContent;
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dtpFromTime.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFromTime.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtActionMan.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Man") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActionMan.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtActionSubject.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Subject") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActionSubject.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtActionContent.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Content") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActionContent.Focus();
                    return;
                }
                if (Util.GetStrLength(txtActionContent.Text.Trim())<20)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Content") + clsTranslate.TranslateString(" length must >= 20!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActionContent.Focus();
                    return;
                }
                if (Util.GetStrLength(txtActionContent.Text.Trim()) > 1024)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Content") + clsTranslate.TranslateString(" length must <= 1024!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActionContent.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtVenue.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Venue") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtVenue.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtObjectName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Object Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObjectName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cboActionType.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Action Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboActionType.Focus();
                    return;
                }
                if (dtpToTime.Value <= dtpFromTime.Value)
                {
                    MessageBox.Show("截止时间必须大于起始时间！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpToTime.Focus();
                    return;
                }
                modCustomerLog mod = new modCustomerLog();
                mod.Id = Convert.ToInt32(txtId.Text);
                mod.CustId = txtCustId.Text.Trim();
                mod.CustName = txtCustName.Text.Trim();
                mod.ActionMan = txtActionMan.Text.Trim();
                mod.ActionCode = cboActionType.SelectedValue.ToString();
                mod.ActionType = cboActionType.Text.Replace("加分","");
                mod.ActionSubject = txtActionSubject.Text.Trim();
                mod.ActionContent = txtActionContent.Text.Trim();
                mod.ObjectName = txtObjectName.Text.Trim();
                mod.Venue = txtVenue.Text.Trim();
                mod.AdFlag = 1;
                mod.TraceFlag = 1;
                mod.FromTime = dtpFromTime.Text;
                mod.ToTime = dtpToTime.Text;
                mod.UpdateUser = Util.UserId;
                dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                modCustomerScoreRule modcsr = dalcsr.GetItem(mod.ActionCode, out Util.emsg);
                mod.Scores = modcsr.Scores;
                bool ret;
                if (_action == "ADD" || _action == "NEW")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(mod.Id, mod, out Util.emsg);
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
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void cboActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboActionType.SelectedValue == null) return;
            switch (cboActionType.SelectedValue.ToString())
            {
                case "CALL":
                    txtVenue.Text = "本公司";                    
                    break;
                case "EMAIL":
                    txtVenue.Text = "本公司";
                    break;
                case "QQ":
                    txtVenue.Text = "本公司";
                    break;
                case "WELCOME":
                    txtVenue.Text = "本公司";
                    break;
                case "VISIT":
                    txtVenue.Text = "客户的公司";
                    break;
            }
        }
    }
}
