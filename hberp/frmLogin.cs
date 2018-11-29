using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using LXMS.DBUtility;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class frmLogin : Form
    {
        INIClass ini = new INIClass(Util.INI_FILE);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboPeriodList);
            frmMain.bLogined = false;
            timer1_Tick(null, null);
            timer1.Enabled = true;
            //string strHostName = Dns.GetHostName();
            string strHostName = Environment.MachineName;
            if (strHostName.ToUpper() == "KAIFA0007" || strHostName.ToUpper() == "LUCJ" || strHostName.ToUpper() == "QUWEIQIN")
            {
                //if (strHostName.ToUpper() == "30-0001-14544")
                //    pictureBox1.Visible = false;
                txtUserId.Text = "SYSADMIN";
                txtPwd.Text = "PPT123";
                txtPwd.Focus();
            }
            else
            {
                txtUserId.Text = ini.IniReadValue("login", "userid");
                if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
                    txtUserId.Focus();
                else
                    txtPwd.Focus();
            }
            RWReg reg = new RWReg("LOCAL_MACHINE");
            bool ret = reg.SetRegValue(@"SOFTWARE\ORACLE", "NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK", "STRING");

            dalUserList bll = new dalUserList();
            if (!bll.UpdateDatabaseObject(Application.StartupPath, out Util.emsg))
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {            
            Login();
        }
        
        private void Login()
        {
            if (txtSystemTime.Text.Trim() != txtServerTime.Text.Trim())
            {
                DateTime dt1 = DateTime.Parse(txtSystemTime.Text);
                DateTime dt2 = DateTime.Parse(txtServerTime.Text);
                TimeSpan ts = new TimeSpan(dt1.Ticks - dt2.Ticks);
                if (Math.Abs(ts.Days) > 1 || Math.Abs(ts.Minutes) > 2 || Math.Abs(ts.Hours) > 1 || Math.Abs(ts.Days) > 1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("local time must equal to server time,please adjust your system time first!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSystemTime.Focus();
                    return;
                }
            }
            if (cboPeriodList.Items.Count == 0)
            {
                btnNew_Click(null, null);
                return;
            }
            if (txtUserId.Text.Trim().Length == 0)
            {
                MessageBox.Show("User Id can not be null!");
                txtUserId.Focus();
                return;
            }
            if (txtPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password can not be null!");
                txtPwd.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            string userid = txtUserId.Text.Trim().ToString();
            string strPwd = Util.Encrypt(txtPwd.Text.Trim().ToString(), Util.PWD_MASK);
            dalUserList dal = new dalUserList();
            bool ret = dal.Login(userid, strPwd, out Util.emsg);
            if (ret)
            {
                modUserList mod = dal.GetItem(userid);
                Util.UserId = mod.UserId;
                Util.UserName = mod.UserName;
                Util.RoleId = mod.RoleId;
                dalRoleList role = new dalRoleList();
                modRoleList modrole = role.GetItem(mod.RoleId, out Util.emsg);
                Util.RoleDesc = modrole.RoleDesc;
                Util.UserStatus = mod.Status;
                ini.IniWriteValue("login", "userid", userid);

                dalAccCurrencyList dalcur = new dalAccCurrencyList();
                modAccCurrencyList modcur = dalcur.GetOwnerItem(out Util.emsg);
                Util.Currency = modcur.Currency;
                dalLogLoginHost bllhost = new dalLogLoginHost();
                modLogLoginHost modhost = new modLogLoginHost();
                modhost.HostName = Environment.MachineName;
                Util.modperiod = (modAccPeriodList)cboPeriodList.SelectedItem;
                clsLxms.CheckSoftwareRegister();
                if (Util.SOFT_REGISTER)
                {
                    modhost.RegisterCode = Util.REGISTER_CODE;
                }
                else
                {
                    modhost.RegisterCode = string.Empty;
                    if (cboPeriodList.Items.Count >= 7)
                    {
                        frmSoftRegister frm = new frmSoftRegister();
                        if(frm.ShowDialog() != DialogResult.OK)
                            return;
                    }
                }
                modhost.HostCode = Util.HOST_CODE;                
                modhost.UpdateUser = Util.UserId;
                if (bllhost.Exists(modhost.HostName, out Util.emsg))
                    bllhost.Update(modhost.HostName, modhost, out Util.emsg);
                else
                    bllhost.Insert(modhost, out Util.emsg);

                this.DialogResult = DialogResult.OK;
                this.Cursor = Cursors.Default;                
                return;
            }
            else
            {
                //this.DialogResult = DialogResult.Cancel;
                this.Cursor = Cursors.Default;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;            
            string userid = "READ";
            string strPwd = Util.Encrypt("READ", Util.PWD_MASK);
            dalUserList bll = new dalUserList();
            bool ret = bll.Login(userid, strPwd, out Util.emsg);
            if (ret)
            {
                modUserList mod = bll.GetItem(userid);
                Util.UserId = mod.UserId;
                Util.UserName = mod.UserName;
                Util.RoleId = mod.RoleId;
                Util.UserStatus = mod.Status;
                this.DialogResult = DialogResult.OK;
                this.Cursor = Cursors.Default;
                return;
            }
            else
            {
                //this.DialogResult = DialogResult.Cancel;
                this.Cursor = Cursors.Default;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                Login();
            }
            else
                e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dalUserList dal = new dalUserList();
            txtSystemTime.Text = DateTime.Now.ToString("MM-dd-yyyy HH:mm");
            txtServerTime.Text = dal.GetServerTime(out Util.emsg).ToString("MM-dd-yyyy HH:mm");
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserId.Text.Trim()))
                txtUserId.Focus();
            else
                txtPwd.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EditAccPeriodList frm = new EditAccPeriodList();
            frm.AddItem(txtUserId.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                FillControl.FillPeriodList(cboPeriodList);
            }
        }
    }
}