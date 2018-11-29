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
    public partial class GuiPurchase : Form
    {
        public GuiPurchase()
        {
            InitializeComponent();
        }

        private void GuiPurchase_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Purchase");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
        }

        private void MTN_VENDOR_TYPE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_VENDOR_TYPE") == true)
            {
                return;
            }
            MTN_VENDOR_TYPE newFrm = new MTN_VENDOR_TYPE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void MTN_VENDOR_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_VENDOR_LIST") == true)
            {
                return;
            }
            MTN_VENDOR_LIST newFrm = new MTN_VENDOR_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_PURCHASE_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_PURCHASE_LIST") == true)
            {
                return;
            }
            OPA_PURCHASE_LIST newFrm = new OPA_PURCHASE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_PURCHASE_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_PURCHASE_SUMMARY") == true)
            {
                return;
            }
            QRY_PURCHASE_SUMMARY newFrm = new QRY_PURCHASE_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_PURCHASE_DETAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_PURCHASE_DETAIL") == true)
            {
                return;
            }
            QRY_PURCHASE_DETAIL newFrm = new QRY_PURCHASE_DETAIL();
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

        private void OPA_PURCHASE_ORDER_Click(object sender, EventArgs e)
        {

        }

        private void OPA_VENDOR_ORDER_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_VENDOR_ORDER_LIST") == true)
            {
                return;
            }
            OPA_VENDOR_ORDER_LIST newFrm = new OPA_VENDOR_ORDER_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
    }
}
