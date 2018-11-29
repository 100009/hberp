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
    public partial class GuiCRM : Form
    {
        public GuiCRM()
        {
            InitializeComponent();
        }

        private void GuiCRM_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("CRM");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
        }


        private void CUSTOMER_SCORE_RULE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("CUSTOMER_SCORE_RULE") == true)
            {
                return;
            }
            CUSTOMER_SCORE_RULE newFrm = new CUSTOMER_SCORE_RULE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
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

        private void MTN_CUSTOMER_LIST_Click(object sender, EventArgs e)
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

        private void CRM_DATA_CALENDAR_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("CRM_DATA_CALENDAR") == true)
            {
                return;
            }
            CRM_DATA_CALENDAR newFrm = new CRM_DATA_CALENDAR();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void CRM_CUSTOMER_LOG_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("CRM_CUSTOMER_LOG") == true)
            {
                return;
            }
            CRM_CUSTOMER_LOG newFrm = new CRM_CUSTOMER_LOG();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACTION_SCORES_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACTION_SCORES_SUMMARY") == true)
            {
                return;
            }
            ACTION_SCORES_SUMMARY newFrm = new ACTION_SCORES_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void CRM_CHANGE_SALES_MAN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("CRM_CHANGE_SALES_MAN") == true)
            {
                return;
            }
            CRM_CHANGE_SALES_MAN newFrm = new CRM_CHANGE_SALES_MAN();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OPA_QUOTATION_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OPA_QUOTATION_FORM") == true)
            {
                return;
            }
            OPA_QUOTATION_FORM newFrm = new OPA_QUOTATION_FORM();
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

        private void MTN_CUSTOMER_LEVEL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_CUSTOMER_LEVEL") == true)
            {
                return;
            }
            MTN_CUSTOMER_LEVEL newFrm = new MTN_CUSTOMER_LEVEL();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
    }
}
