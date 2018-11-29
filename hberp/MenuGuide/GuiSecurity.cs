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
    public partial class GuiSecurity : Form
    {
        public GuiSecurity()
        {
            InitializeComponent();
        }

        private void GuiSecurity_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Security");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
            if(Util.UserId.CompareTo("READ")!=0)
                SEC_CHANGE_PASSWORD.Enabled = true;
        }

        private void SEC_ROLE_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_ROLE_LIST") == true)
            {
                return;
            }
            SEC_ROLE_LIST newFrm = new SEC_ROLE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SEC_USER_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_USER_LIST") == true)
            {
                return;
            }
            SEC_USER_LIST newFrm = new SEC_USER_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SEC_TASK_GROUP_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_TASK_GROUP") == true)
            {
                return;
            }
            SEC_TASK_GROUP newFrm = new SEC_TASK_GROUP();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SEC_TASK_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_TASK_LIST") == true)
            {
                return;
            }
            SEC_TASK_LIST newFrm = new SEC_TASK_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SEC_TASK_GRANT_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_TASK_GRANT") == true)
            {
                return;
            }
            SEC_TASK_GRANT newFrm = new SEC_TASK_GRANT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
        
        private void SEC_STORE_GRANT_Click(object sender, EventArgs e)
        {
            
        }

        private void SEC_PROCESS_GRANT_Click(object sender, EventArgs e)
        {
            
        }

        private void SEC_CHANGE_PASSWORD_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_CHANGE_PASSWORD") == true)
            {
                return;
            }
            SEC_CHANGE_PASSWORD newFrm = new SEC_CHANGE_PASSWORD();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SEC_SYSTEM_PARAMETERS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SEC_SYSTEM_PARAMETERS") == true)
            {
                return;
            }
            SEC_SYSTEM_PARAMETERS newFrm = new SEC_SYSTEM_PARAMETERS();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_TASK_GRANT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_TASK_GRANT") == true)
            {
                return;
            }
            QRY_TASK_GRANT newFrm = new QRY_TASK_GRANT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
    }
}
