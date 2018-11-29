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
    public partial class GuiSales : Form
    {
        public GuiSales()
        {
            InitializeComponent();
        }

        private void GuiSales_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Sales");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
        }

        private void MTN_CUSTOMER_TYPE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_CUSTOMER_TYPE") == true)
            {
                return;
            }
            MTN_CUSTOMER_TYPE newFrm = new MTN_CUSTOMER_TYPE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void MTN_CUSTOMER_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_CUSTOMER_LIST") == true)
            {
                return;
            }
            MTN_CUSTOMER_LIST newFrm = new MTN_CUSTOMER_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_SALES_SHIPMENT_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_SALES_SHIPMENT") == true)
            {
                return;
            }
            OPA_SALES_SHIPMENT newFrm = new OPA_SALES_SHIPMENT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
        
        private void QRY_SALES_SHIPMENT_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_SALES_SHIPMENT_SUMMARY") == true)
            {
                return;
            }
            QRY_SALES_SHIPMENT_SUMMARY newFrm = new QRY_SALES_SHIPMENT_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_SALES_SHIPMENT_DETAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_SALES_SHIPMENT_DETAIL") == true)
            {
                return;
            }
            QRY_SALES_SHIPMENT_DETAIL newFrm = new QRY_SALES_SHIPMENT_DETAIL();
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

        private void OPA_SALES_MAN_MNY_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_SALES_MAN_MNY") == true)
            {
                return;
            }
            OPA_SALES_MAN_MNY newFrm = new OPA_SALES_MAN_MNY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_QUOTATION_DETAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_QUOTATION_DETAIL") == true)
            {
                return;
            }
            QRY_QUOTATION_DETAIL newFrm = new QRY_QUOTATION_DETAIL();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_SALES_DESIGN_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_SALES_DESIGN_FORM") == true)
            {
                return;
            }
            OPA_SALES_DESIGN_FORM newFrm = new OPA_SALES_DESIGN_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_SALES_DESIGN_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_SALES_DESIGN_SUMMARY") == true)
            {
                return;
            }
            QRY_SALES_DESIGN_SUMMARY newFrm = new QRY_SALES_DESIGN_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_SALES_DESIGN_DETAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_SALES_DESIGN_DETAIL") == true)
            {
                return;
            }
            QRY_SALES_DESIGN_DETAIL newFrm = new QRY_SALES_DESIGN_DETAIL();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_CUSTOMER_ORDER_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_CUSTOMER_ORDER_LIST") == true)
            {
                return;
            }
            OPA_CUSTOMER_ORDER_LIST newFrm = new OPA_CUSTOMER_ORDER_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void EditCollectCamp_Click(object sender, EventArgs e)
        {
            frmChangeUser frm = new frmChangeUser();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
            }
            else
            {
                frmMain frmmain = (frmMain)this.ParentForm;
                if (frmmain.CheckChildFrmExist("EditCollectCamp") == true)
                {
                    return;
                }
                EditCollectCamp newFrm = new EditCollectCamp();
                if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
                {
                    newFrm.Dispose();
                    newFrm = null;
                }
                //this.lblCurrentUser.Text = "营业员：" + Util.UserName;
            }            
        }
    }
}
