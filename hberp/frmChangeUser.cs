using LXMS.DAL;
using LXMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class frmChangeUser : Form
    {
        INIClass ini = new INIClass(Util.INI_FILE);
        public frmChangeUser()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
            this.BackColor = frmOptions.BORDERCOLOR;
        }

        private void frmChangeUser_Load(object sender, EventArgs e)
        {
            txtUserId.Text = Util.UserId;            
        }

        private void frmChangeUser_Shown(object sender, EventArgs e)
        {
            txtPwd.Focus();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void Login()
        {            
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

                dalLogLoginHost bllhost = new dalLogLoginHost();
                modLogLoginHost modhost = new modLogLoginHost();
                modhost.HostName = Environment.MachineName;
                
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

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Login();
            }
        }

    }
}
