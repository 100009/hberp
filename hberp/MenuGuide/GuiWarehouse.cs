using System;
using System.Drawing;
using System.Windows.Forms;

namespace LXMS
{
	public partial class GuiWarehouse : Form
    {
        public GuiWarehouse()
        {
            InitializeComponent();
        }

        private void GuiWarehouse_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Warehouse");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
        }

        private void MTN_WAREHOUSE_INOUT_TYPE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_WAREHOUSE_INOUT_TYPE") == true)
            {
                return;
            }
            MTN_WAREHOUSE_INOUT_TYPE newFrm = new MTN_WAREHOUSE_INOUT_TYPE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void MTN_WAREHOUSE_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_WAREHOUSE_LIST") == true)
            {
                return;
            }
            MTN_WAREHOUSE_LIST newFrm = new MTN_WAREHOUSE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_WAREHOUSE_INOUT_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_WAREHOUSE_INOUT_FORM") == true)
            {
                return;
            }
            OPA_WAREHOUSE_INOUT_FORM newFrm = new OPA_WAREHOUSE_INOUT_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_WAREHOUSE_PRODUCT_INFO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_WAREHOUSE_PRODUCT_INFO") == true)
            {
                return;
            }
            QRY_WAREHOUSE_PRODUCT_INFO newFrm = new QRY_WAREHOUSE_PRODUCT_INFO();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CREDENCE_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CREDENCE_LIST") == true)
            {
                return;
            }
            ACC_CREDENCE_LIST newFrm = new ACC_CREDENCE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void WAREHOUSE_INOUT_COST_PRICE_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("WAREHOUSE_INOUT_COST_PRICE") == true)
            {
                return;
            }
            WAREHOUSE_INOUT_COST_PRICE newFrm = new WAREHOUSE_INOUT_COST_PRICE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_WAREHOUSE_PRODUCT_TRANSFER_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_WAREHOUSE_PRODUCT_TRANSFER") == true)
            {
                return;
            }
            OPA_WAREHOUSE_PRODUCT_TRANSFER newFrm = new OPA_WAREHOUSE_PRODUCT_TRANSFER();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

		private void OPA_WAREHOUSE_DAILY_REPORT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			frmMain frmmain = (frmMain)this.ParentForm;
			if (frmmain.CheckChildFrmExist("OPA_WAREHOUSE_DAILY_REPORT") == true)
			{
				return;
			}
			OPA_WAREHOUSE_DAILY_REPORT newFrm = new OPA_WAREHOUSE_DAILY_REPORT();
			if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
			{
				newFrm.Dispose();
				newFrm = null;
			}
		}
	}
}
