using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class UserPrivilegeEdit : Form
    {
        dalSysUserPrivilege _dal = new dalSysUserPrivilege();
        public UserPrivilegeEdit()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void UserPrivilegeEdit_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindingCollection<modSysUserPrivilege> list = new BindingCollection<modSysUserPrivilege>();
                modSysUserPrivilege mod = new modSysUserPrivilege();
                mod.UserId = txtUserId.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.PrivilegeName = "CUST_ACCESS_OPTION";
                if (rbOwnerOnly.Checked)
                    mod.PrivilegeValue = "1";
                else if (rbAll.Checked)
                    mod.PrivilegeValue = "3";
                else
                    mod.PrivilegeValue = "0";
                list.Add(mod);

                //mod = new modSysUserPrivilege();
                //mod.UserId = txtUserId.Text.Trim();
                //mod.UpdateUser = Util.UserId;
                //mod.PrivilegeName = "UPLOAD_ITEMNO";
                //mod.PrivilegeValue = chkUpload.Checked ? "1" : "0";
                //list.Add(mod);
                
                bool ret = _dal.Save(txtUserId.Text, list, out Util.emsg);
                if (ret)
                {
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
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public string UserId
        {
            get { return txtUserId.Text.Trim(); }
            set 
            { 
                txtUserId.Text = value;
                BindingCollection<modSysUserPrivilege> list = _dal.GetIList(value, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modSysUserPrivilege mod in list)
                    {
                        switch (mod.PrivilegeName)
                        {
                            case "CUST_ACCESS_OPTION":
                                switch (mod.PrivilegeValue)
                                {
                                    case "0":
                                        rbNone.Checked = true;
                                        break;
                                    case "1":
                                        rbOwnerOnly.Checked = true;
                                        break;
                                    case "3":
                                        rbAll.Checked = true;
                                        break;
                                }
                                break;
                            //case "UPLOAD_ITEMNO":
                            //    chkUpload.Checked = mod.PrivilegeValue == "1" ? true : false;
                            //    break;                            
                        }
                    } 
                }
            }
        }

        public string UserName
        {
            get { return txtUserName.Text.Trim(); }
            set { txtUserName.Text = value; }
        }
    }
}
