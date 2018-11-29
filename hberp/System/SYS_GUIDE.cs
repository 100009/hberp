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
    public partial class SYS_GUIDE : Form
    {
        public SYS_GUIDE()
        {
            InitializeComponent();
        }

        private void OPA_SYSTEM_GUIDE_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            if (Util.modperiod.Seq >= 2 && Util.UserId != "SYSADMIN")
                ACC_START_DATA_FORM.Enabled = false;

            if (Util.UserId != "SYSADMIN")
                SYS_INIT.Enabled = false;
        }

        private void ACC_START_DATA_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_START_DATA_FORM") == true)
            {
                return;
            }
            ACC_START_DATA_FORM newFrm = new ACC_START_DATA_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void SYS_INIT_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("SYS_INIT") == true)
            {
                return;
            }
            SYS_INIT newFrm = new SYS_INIT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
