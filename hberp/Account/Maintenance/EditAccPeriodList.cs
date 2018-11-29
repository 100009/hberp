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
    public partial class EditAccPeriodList : Form
    {
        string _action = string.Empty;
        string _userid = string.Empty;
        dalAccPeriodList _dal = new dalAccPeriodList();
        public EditAccPeriodList()
        {
            InitializeComponent();
        }

        private void EditAccPeriodList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            if (Util.modperiod != null)
            {
                int accyear = Util.modperiod.AccYear;
                int accmonth = Util.modperiod.AccMonth + 1;
                if (accmonth == 13)
                {
                    accmonth = 1;
                    accyear++;
                }
                txtAccYear.Text = accyear.ToString();
                txtAccMonth.Text = accmonth.ToString();
            }
            else
            {
                txtAccYear.Text = DateTime.Today.Year.ToString();
                txtAccMonth.Text = DateTime.Today.Month.ToString();
            }
            dalAdminEmployeeList dal = new dalAdminEmployeeList();
            txtEmployeeCount.Text = dal.GetCount("1", string.Empty, out Util.emsg).ToString();
        }

        public void InitForm(DateTime accdate)
        {
            txtAccYear.Text = accdate.Year.ToString();
            txtAccMonth.Text = accdate.Month.ToString();
            txtAccName.Text = txtAccYear.Text.Trim() + "年" + txtAccMonth.Text.Trim().PadLeft(2, '0') + "月财务区间";
        }

        public void AddItem(string userid)
        {
            _action = "ADD";
            _userid = userid;            
        }

        public void EditItem(modAccPeriodList mod)
        {
            _action = "EDIT";
            txtAccName.Text = mod.AccName;
            txtAccYear.Text = mod.AccYear.ToString();
            txtAccMonth.Text = mod.AccMonth.ToString();
            txtEmployeeCount.Text = mod.EmployeeCount.ToString();
            txtAccName.ReadOnly = true;
            txtAccYear.ReadOnly = true;
            txtAccMonth.ReadOnly = true;
            _userid = Util.UserId;
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtAccName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Subject Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAccYear.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Acc Year") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccYear.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtAccYear.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Acc Year") + clsTranslate.TranslateString(" must be numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccYear.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAccMonth.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Acc Month") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccMonth.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtAccMonth.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Acc Month") + clsTranslate.TranslateString(" must be numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAccMonth.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtEmployeeCount.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Employee Count") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmployeeCount.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtEmployeeCount.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Employee Count") + clsTranslate.TranslateString(" must be numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmployeeCount.Focus();
                    return;
                }
                modAccPeriodList mod = new modAccPeriodList();
                mod.AccYear = Convert.ToInt32(txtAccYear.Text);
                mod.AccMonth = Convert.ToInt32(txtAccMonth.Text);
                mod.AccName = txtAccName.Text.Trim();
                mod.StartDate = Convert.ToDateTime(mod.AccYear.ToString().Trim() + "-" + mod.AccMonth.ToString().Trim() + "-" + "1");
                mod.EndDate = mod.StartDate.AddMonths(1).AddDays(-1);                
                mod.EmployeeCount = Convert.ToInt32(txtEmployeeCount.Text);
                mod.CostFlag = 0;
                mod.LockFlag = 0;
                mod.UpdateUser = _userid;
                bool ret = false;
                if (_action == "ADD")
                    ret = _dal.Insert(mod, out Util.emsg);
                else
                    ret = _dal.Update(txtAccName.Text.Trim(), mod, out Util.emsg);
                if (ret)
                {
                    Util.retValue1 = txtAccName.Text.Trim();                    
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

        private void txtAccYear_TextChanged(object sender, EventArgs e)
        {
            txtAccName.Text = txtAccYear.Text.Trim() + "年" + txtAccMonth.Text.Trim().PadLeft(2,'0') + "月财务区间";
        }

        private void EditAccPeriodList_Shown(object sender, EventArgs e)
        {
            txtEmployeeCount.Focus();
        }
    }
}
