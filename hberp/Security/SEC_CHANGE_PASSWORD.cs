using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class SEC_CHANGE_PASSWORD : Form
    {
        dalUserList _dal = new dalUserList();
        public SEC_CHANGE_PASSWORD()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);            
        }

        private void btnOK_Click(object sender, EventArgs e)
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
            if (txtNewPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("Password can not be null!");
                txtNewPwd.Focus();
                return;
            }
            if (txtNewPwd.Text.Trim() != txtConfirm.Text.Trim())
            {
                MessageBox.Show("Password is incorrect!");
                txtConfirm.Focus();
                return;
            }
                        
            string userid = txtUserId.Text.Trim().ToString();
            string oldpwd = Util.Encrypt(txtPwd.Text.Trim().ToUpper().ToString(), Util.PWD_MASK);
            string newpwd = Util.Encrypt(txtNewPwd.Text.Trim().ToUpper().ToString(), Util.PWD_MASK);

            bool ret = _dal.ChangePassword(userid, oldpwd, newpwd, out Util.emsg);
            if (ret)
            {
                MessageBox.Show("Success!","OK",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SEC_CHANGE_PASSWORD_Load(object sender, EventArgs e)
        {            
            txtUserId.Text = Util.UserId;
            txtPwd.Focus();            
        }

        private void txtConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnOK_Click(null, null);
            }
        }
    }
}