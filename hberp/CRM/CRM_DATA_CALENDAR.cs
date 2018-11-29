using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{    
    public partial class CRM_DATA_CALENDAR : Form
    {
        public CRM_DATA_CALENDAR()
        {
            InitializeComponent();
            lstActionCode.BackColor = frmOptions.BACKCOLOR;
            clsTranslate.InitLanguage(this);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    string txtName = "txtContent" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                    TextBox txt = (TextBox)(this.Controls.Find(txtName, true))[0];
                    txt.DoubleClick += new System.EventHandler(this.txtContent_DoubleClick);
                    txt.Validated += new System.EventHandler(this.txtContent_Validated);
                }
            }
        }

        private void CRM_DATA_CALENDAR_Load(object sender, EventArgs e)
        {
            FillControl.FillCustomerScoreRule(lstActionCode, string.Empty);
            txtYear.Text = DateTime.Today.Year.ToString();
            cboMonth.Text = DateTime.Today.Month.ToString();
            txtSalesMan.Text = Util.UserName;
            dalSysUserPrivilege bllpri = new dalSysUserPrivilege();
            modSysUserPrivilege modpri = bllpri.GetItem(Util.UserId, "CUST_ACCESS_OPTION", out Util.emsg);
            if (modpri != null && modpri.PrivilegeValue=="3")
            {
                btnSalesMan.Enabled = true;
            }
            else
            {
                btnSalesMan.Enabled = false;
            }

            rbSchedule.Checked = true;
            lstActionCode.Enabled = false;
            InitDate();
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitDate();
            LoadData();
        }

        private void InitDate()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime firstdate = DateTime.Parse(txtYear.Text + "-" + cboMonth.Text + "-1");
                DateTime lastdate = firstdate.AddMonths(1).AddDays(-1);
                DayOfWeek firstweekday = firstdate.DayOfWeek;
                DayOfWeek lastweekday = lastdate.DayOfWeek;

                DateTime curdate = firstdate;
                switch (firstweekday)
                {
                    case DayOfWeek.Monday:
                        curdate = firstdate.AddDays(-1);
                        break;
                    case DayOfWeek.Tuesday:
                        curdate = firstdate.AddDays(-2);
                        break;
                    case DayOfWeek.Wednesday:
                        curdate = firstdate.AddDays(-3);
                        break;
                    case DayOfWeek.Thursday:
                        curdate = firstdate.AddDays(-4);
                        break;
                    case DayOfWeek.Friday:
                        curdate = firstdate.AddDays(-5);
                        break;
                    case DayOfWeek.Saturday:
                        curdate = firstdate.AddDays(-6);
                        break;
                    default:
                        break;
                }
                int oldmonth = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        string lblName = "lblDate" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                        string txtName = "txtContent" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                        string plName = "Panel" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                        Control[] ctl = this.Controls.Find(lblName, true);
                        Control[] ctl2 = this.Controls.Find(txtName, true);
                        Control[] ctl3 = this.Controls.Find(plName, true);
                        if (ctl != null && ctl.Length > 0)
                        {
                            Label lbl = (Label)ctl[0];
                            TextBox txt = (TextBox)ctl2[0];
                            Panel pl = (Panel)ctl3[0];

                            txt.Text = string.Empty;
                            txt.Tag = curdate.ToString("yyyy-MM-dd");
                            if (rbSchedule.Checked)
                                txt.ReadOnly = false;
                            else
                                txt.ReadOnly = true;

                            txt.ContextMenuStrip = null;
                            pl.Dock = DockStyle.Fill;
                            lbl.TextAlign = ContentAlignment.MiddleCenter;                            
                            if (curdate.Month != firstdate.Month)
                            {
                                lbl.BackColor = Color.WhiteSmoke;
                            }
                            else
                            {
                                lbl.BackColor = Color.White;                                
                            }
                            txt.BackColor = frmOptions.BACKCOLOR;
                            txt.ForeColor = Color.Black;
                            if (oldmonth == curdate.Month)
                            {
                                lbl.Text = curdate.Day.ToString();
                            }
                            else
                            {
                                oldmonth = curdate.Month;
                                lbl.Text = curdate.Month + "月" + curdate.Day.ToString() +"日";                                
                            }
                            if (curdate == DateTime.Today)
                            {
                                lbl.BackColor = Color.OrangeRed;
                                lbl.ForeColor = Color.White;
                            }
                            else
                            {
                                if (curdate >= firstdate && curdate <= lastdate)
                                {
                                    if (j == 0 || j == 6)
                                    {
                                        lbl.BackColor = frmOptions.BASECOLOR;
                                        lbl.ForeColor = Color.OrangeRed;
                                    }
                                    else
                                    {
                                        lbl.BackColor = frmOptions.BACKCOLOR;
                                        lbl.ForeColor = Color.Black;
                                    }
                                }
                                else
                                {
                                    lbl.ForeColor = Color.DarkGray;
                                }
                            }
                        }
                        curdate = curdate.AddDays(1);
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

        private void btnSalesMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtSalesMan);
            InitDate();
            LoadData();
        }

        private void rbSchedule_CheckedChanged(object sender, EventArgs e)
        {
            lstActionCode.Enabled = rbSchedule.Checked ? false : true;
            if(rbCustomerLog.Checked)
                FillControl.FillCustomerScoreRule(lstActionCode, "1");
            else if (rbActionScores.Checked)
                FillControl.FillCustomerScoreRule(lstActionCode, string.Empty);
            lstActionCode.SelectedItems.Clear();
            InitDate();
            LoadData();
        }

        private void txtYear_Validated(object sender, EventArgs e)
        {
            InitDate();
            LoadData();
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDate();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string fromtime = txtContent0101.Tag.ToString();
                string totime = txtContent0607.Tag.ToString();
                if (rbSchedule.Checked)
                {
                    dalCrmActionSchedule dal = new dalCrmActionSchedule();
                    BindingCollection<modCrmActionSchedule> list = dal.GetIList(string.Empty, txtSalesMan.Text, fromtime, totime, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        foreach (modCrmActionSchedule mod in list)
                        {
                            bool find = false;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    string txtName = "txtContent" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                                    TextBox txt = (TextBox)(this.Controls.Find(txtName, true))[0];
                                    if (txt.GetType().ToString() == "System.Windows.Forms.TextBox" && txt.Tag != null && txt.Tag.ToString() == mod.ActionDate.ToString("yyyy-MM-dd"))
                                    {
                                        txt.Text = mod.ActionContent;
                                        if (mod.Status == 0)
                                        {
                                            txt.ReadOnly = false;
                                            txt.ForeColor = Color.Black;
                                        }
                                        else if (mod.Status == 1)
                                        {
                                            txt.ReadOnly = true;
                                            txt.ForeColor = Color.DarkGoldenrod;
                                        }
                                        else
                                        {
                                            txt.ReadOnly = true;
                                            txt.ForeColor = Color.DarkGray;
                                        }
                                        txt.ContextMenuStrip = contextMenuStrip1;
                                        find = true;
                                        break;
                                    }
                                }
                                if (find)
                                    break;
                            }
                        }
                    }
                }
                else if (rbCustomerLog.Checked)
                {
                    string actioncodelist = string.Empty;
                    if (lstActionCode.SelectedItems.Count > 0 && lstActionCode.SelectedItems.Count < lstActionCode.Items.Count)
                    {
                        for (int i = 0; i < lstActionCode.SelectedItems.Count; i++)
                        {
                            modCustomerScoreRule mod = (modCustomerScoreRule)lstActionCode.SelectedItems[i];
                            if (i == 0)
                                actioncodelist = mod.ActionCode;
                            else
                                actioncodelist += "," + mod.ActionCode;
                        }
                    }

                    dalCustomerLog dal = new dalCustomerLog();
                    BindingCollection<modCustomerDailyLog> list = dal.GetCustomerDailyLog(actioncodelist, txtSalesMan.Text, fromtime, totime, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        foreach (modCustomerDailyLog mod in list)
                        {
                            bool find = false;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    string txtName = "txtContent" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                                    TextBox txt = (TextBox)(this.Controls.Find(txtName, true))[0];
                                    if (txt.GetType().ToString() == "System.Windows.Forms.TextBox" && txt.Tag != null && txt.Tag.ToString() == mod.ActionDate)
                                    {
                                        txt.Text = mod.ActionContent;                                        
                                        find = true;
                                        break;
                                    }
                                }
                                if (find)
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    string actioncodelist = string.Empty;
                    if (lstActionCode.SelectedItems.Count > 0 && lstActionCode.SelectedItems.Count < lstActionCode.Items.Count)
                    {
                        for (int i = 0; i < lstActionCode.SelectedItems.Count; i++)
                        {
                            modCustomerScoreRule mod = (modCustomerScoreRule)lstActionCode.SelectedItems[i];
                            if (i == 0)
                                actioncodelist = mod.ActionCode;
                            else
                                actioncodelist += "," + mod.ActionCode;
                        }
                    }
                    dalCustomerLog dal = new dalCustomerLog();
                    BindingCollection<modCustomerDailyScores> list = dal.GetCustomerDailyScores(actioncodelist, txtSalesMan.Text, fromtime, totime, out Util.emsg);
                    if (list != null && list.Count > 0)
                    {
                        foreach (modCustomerDailyScores mod in list)
                        {
                            bool find = false;
                            for (int i = 0; i < 6; i++)
                            {
                                for (int j = 0; j < 7; j++)
                                {
                                    string txtName = "txtContent" + (i + 1).ToString().PadLeft(2, '0') + (j + 1).ToString().PadLeft(2, '0');
                                    TextBox txt = (TextBox)(this.Controls.Find(txtName, true))[0];
                                    if (txt.GetType().ToString() == "System.Windows.Forms.TextBox" && txt.Tag != null && txt.Tag.ToString() == mod.ActionDate)
                                    {
                                        txt.Text = "积分：" + mod.Scores.ToString();
                                        find = true;
                                        break;
                                    }
                                }
                                if (find)
                                    break;
                            }
                        }
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

        private void txtContent_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                
                TextBox txt=(TextBox)sender;
                if (rbSchedule.Checked)
                {
                    frmViewText frm = new frmViewText(clsTranslate.TranslateString("Action Schedule"), txt.Text.Trim());
                    frm.ShowDialog();
                }                
                else
                {
                    string traceflaglist = string.Empty;
                    if(rbCustomerLog.Checked)
                        traceflaglist="1";
                    dalCustomerLog dal = new dalCustomerLog();
                    BindingCollection<modCustomerLog> list = dal.GetIList(string.Empty, string.Empty, string.Empty, txtSalesMan.Text, traceflaglist, ((TextBox)sender).Tag.ToString(), DateTime.Parse(txt.Tag.ToString()).AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"), out Util.emsg);
                    if (list != null)
                    {
                        frmViewList frm = new frmViewList();
                        frm.InitViewList(clsTranslate.TranslateString("Customer Log"), list);
                        frm.ShowDialog();
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

        private void txtContent_Validated(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox txt = (TextBox)sender;
                if (!txt.ReadOnly && rbSchedule.Checked)
                {                    
                    dalCrmActionSchedule dal = new dalCrmActionSchedule();
                    if (!dal.Exists(txtSalesMan.Text, Convert.ToDateTime(txt.Tag.ToString()), txt.Text.Trim(), out Util.emsg))
                    {
                        bool ret = dal.Delete(txtSalesMan.Text.Trim(), Convert.ToDateTime(txt.Tag.ToString()), out Util.emsg);
                        if (ret)
                        {
                            if (!string.IsNullOrEmpty(txt.Text.Trim()))
                            {
                                modCrmActionSchedule mod = new modCrmActionSchedule();
                                mod.ActionMan = txtSalesMan.Text.Trim();
                                mod.ActionDate = Convert.ToDateTime(txt.Tag.ToString());
                                mod.ActionContent = txt.Text.Trim();
                                mod.Status = 0;
                                mod.UpdateUser = Util.UserId;
                                ret = dal.Insert(mod, out Util.emsg);
                                if (!ret)
                                {
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    float fontsize = txt.Font.Size;
                                    txt.Font = new Font(txt.Font.FontFamily, 16, txt.Font.Style);
                                    txt.Refresh();
                                    Thread.Sleep(200);
                                    txt.Font = new Font(txt.Font.FontFamily, fontsize, txt.Font.Style);
                                    txt.Refresh();
                                    txt.ReadOnly = false;
                                    txt.ContextMenuStrip = contextMenuStrip1;
                                }
                            }
                            else
                            {
                                txt.ContextMenuStrip = null;                         
                            }
                        }
                        else
                        {
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        private void EditScheduleStatus(ref modCrmActionSchedule mod, ref TextBox txt)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                EditActionScheduleStatus frm = new EditActionScheduleStatus();
                frm.InitData(mod);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    txt.ReadOnly = mod.Status == 0 ? false : true;
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

        private void menuStatus_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TextBox txt = (TextBox)contextMenuStrip1.SourceControl;
                dalCrmActionSchedule dal = new dalCrmActionSchedule();
                modCrmActionSchedule mod = dal.GetItem(txtSalesMan.Text, DateTime.Parse(txt.Tag.ToString()), out Util.emsg);
                EditScheduleStatus(ref mod, ref txt);
                mod = dal.GetItem(txtSalesMan.Text, DateTime.Parse(txt.Tag.ToString()), out Util.emsg);
                if (mod.Status == 0)
                {
                    txt.ReadOnly = false;
                    txt.ForeColor = Color.Black;
                }
                else if (mod.Status == 1)
                {
                    txt.ReadOnly = true;
                    txt.ForeColor = Color.DarkGoldenrod;
                }
                else
                {
                    txt.ReadOnly = true;
                    txt.ForeColor = Color.DarkGray;
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
