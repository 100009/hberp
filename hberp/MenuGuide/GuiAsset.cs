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
    public partial class GuiAsset : Form
    {
        public GuiAsset()
        {
            InitializeComponent();
        }

        private void GuiAsset_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Asset");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
        }

        private void ASSET_LIST_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ASSET_LIST") == true)
            {
                return;
            }
            ASSET_LIST newFrm = new ASSET_LIST();
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

        private void ASSET_ADD_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ASSET_ADD") == true)
            {
                return;
            }
            ASSET_ADD newFrm = new ASSET_ADD();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ASSET_SALE_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ASSET_SALE") == true)
            {
                return;
            }
            ASSET_SALE newFrm = new ASSET_SALE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ASSET_EVALUATE_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ASSET_EVALUATE") == true)
            {
                return;
            }
            ASSET_EVALUATE newFrm = new ASSET_EVALUATE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void QRY_ASSET_LOG_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("QRY_ASSET_LOG") == true)
            {
                return;
            }
            QRY_ASSET_LOG newFrm = new QRY_ASSET_LOG();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
    }
}
