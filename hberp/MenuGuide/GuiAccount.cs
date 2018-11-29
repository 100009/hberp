using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class GuiAccount : Form
    {
        public GuiAccount()
        {
            InitializeComponent();
        }

        private void GuiAccount_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            this.BackColor = Color.White;
            toolStripLabel1.Text = clsTranslate.TranslateString("Account");
            toolStripLabel2.Text = clsTranslate.TranslateString("Other");
            this.splitContainer1.SplitterDistance = splitContainer1.Width - 200;
            clsLxms.SetFormStatus(this);
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

        private void ACC_CREDENCE_LIST_AUDIT_Click(object sender, EventArgs e)
        {

        }

        private void ACC_SUBJECT_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_SUBJECT_LIST") == true)
            {
                return;
            }
            ACC_SUBJECT_LIST newFrm = new ACC_SUBJECT_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CURRENCY_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("MTN_CURRENCY_LIST") == true)
            {
                return;
            }
            ACC_CURRENCY_LIST newFrm = new ACC_CURRENCY_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_BANK_ACCOUNT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_BANK_ACCOUNT") == true)
            {
                return;
            }
            ACC_BANK_ACCOUNT newFrm = new ACC_BANK_ACCOUNT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CREDENCE_WORD_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CREDENCE_WORD") == true)
            {
                return;
            }
            ACC_CREDENCE_WORD newFrm = new ACC_CREDENCE_WORD();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_COMMON_DIGEST_TYPE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_COMMON_DIGEST_TYPE") == true)
            {
                return;
            }
            ACC_COMMON_DIGEST_TYPE newFrm = new ACC_COMMON_DIGEST_TYPE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_COMMON_DIGEST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_COMMON_DIGEST") == true)
            {
                return;
            }
            ACC_COMMON_DIGEST newFrm = new ACC_COMMON_DIGEST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CHECK_TYPE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CHECK_TYPE") == true)
            {
                return;
            }
            ACC_CHECK_TYPE newFrm = new ACC_CHECK_TYPE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_BALANCE_STYLE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_BALANCE_STYLE") == true)
            {
                return;
            }
            ACC_BALANCE_STYLE newFrm = new ACC_BALANCE_STYLE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OTHER_RECEIVABLE_OBJECT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OTHER_RECEIVABLE_OBJECT") == true)
            {
                return;
            }
            OTHER_RECEIVABLE_OBJECT newFrm = new OTHER_RECEIVABLE_OBJECT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void OTHER_PAYABLE_OBJECT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("OTHER_PAYABLE_OBJECT") == true)
            {
                return;
            }
            OTHER_PAYABLE_OBJECT newFrm = new OTHER_PAYABLE_OBJECT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_ASSET_DEBT_REPORT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_ASSET_DEBT_REPORT") == true)
            {
                return;
            }
            ACC_ASSET_DEBT_REPORT newFrm = new ACC_ASSET_DEBT_REPORT();
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
        
        private void ACC_RECEIVABLE_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_RECEIVABLE_LIST") == true)
            {
                return;
            }
            ACC_RECEIVABLE_LIST newFrm = new ACC_RECEIVABLE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PAYABLE_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_PAYABLE_LIST") == true)
            {
                return;
            }
            ACC_PAYABLE_LIST newFrm = new ACC_PAYABLE_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_RECEIVABLE_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_RECEIVABLE_FORM") == true)
            {
                return;
            }
            ACC_RECEIVABLE_FORM newFrm = new ACC_RECEIVABLE_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PAYABLE_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_PAYABLE_FORM") == true)
            {
                return;
            }
            ACC_PAYABLE_FORM newFrm = new ACC_PAYABLE_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CHECK_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CHECK_LIST") == true)
            {
                return;
            }
            ACC_CHECK_LIST newFrm = new ACC_CHECK_LIST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_OTHER_RECEIVABLE_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_OTHER_RECEIVABLE_FORM") == true)
            {
                return;
            }
            ACC_OTHER_RECEIVABLE_FORM newFrm = new ACC_OTHER_RECEIVABLE_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_OTHER_PAYABLE_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_OTHER_PAYABLE_FORM") == true)
            {
                return;
            }
            ACC_OTHER_PAYABLE_FORM newFrm = new ACC_OTHER_PAYABLE_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
        
        private void ACC_OTHER_RECEIVABLE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_OTHER_RECEIVABLE") == true)
            {
                return;
            }
            ACC_OTHER_RECEIVABLE newFrm = new ACC_OTHER_RECEIVABLE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_OTHER_PAYABLE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_OTHER_PAYABLE") == true)
            {
                return;
            }
            ACC_OTHER_PAYABLE newFrm = new ACC_OTHER_PAYABLE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PRODUCT_INOUT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_PRODUCT_INOUT") == true)
            {
                return;
            }
            ACC_PRODUCT_INOUT newFrm = new ACC_PRODUCT_INOUT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CHECK_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CHECK_FORM") == true)
            {
                return;
            }
            ACC_CHECK_FORM newFrm = new ACC_CHECK_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void PRODUCTION_FORM_COST_PRICE_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("PRODUCTION_FORM_COST_PRICE") == true)
            {
                return;
            }
            PRODUCTION_FORM_COST_PRICE newFrm = new PRODUCTION_FORM_COST_PRICE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_WAITING_AUDIT_LIST_Click(object sender, EventArgs e)
        {
            dalAccReport _dal=new dalAccReport();
            BindingCollection<modWaitingAuditList> list = _dal.GetWaitingAuditList(Util.modperiod.AccName, Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
            if (list != null)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Waiting Audit List"), list);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ACC_WAITING_AUDIT_LIST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dalAccReport dal = new dalAccReport();
            BindingCollection<modWaitingAuditList> list = dal.GetWaitingAuditList(Util.modperiod.AccName, Util.modperiod.StartDate.ToString("MM-dd-yyyy"), Util.modperiod.EndDate.ToString("MM-dd-yyyy"), out Util.emsg);
            if (list != null && list.Count > 0)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList("未处理单据清单", list);
                frm.ShowDialog();
            }
            else
            {
                if (!string.IsNullOrEmpty(Util.emsg))
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("没有找到未处理的数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ACC_SUBJECT_DETAIL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_SUBJECT_DETAIL") == true)
            {
                return;
            }
            ACC_SUBJECT_DETAIL newFrm = new ACC_SUBJECT_DETAIL();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PROFIT_REPORT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_PROFIT_REPORT") == true)
            {
                return;
            }
            ACC_PROFIT_REPORT newFrm = new ACC_PROFIT_REPORT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_EXPENSE_REPORT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_EXPENSE_REPORT") == true)
            {
                return;
            }
            ACC_EXPENSE_REPORT newFrm = new ACC_EXPENSE_REPORT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_EXPENSE_FORM_Click(object sender, EventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_EXPENSE_FORM") == true)
            {
                return;
            }
            ACC_EXPENSE_FORM newFrm = new ACC_EXPENSE_FORM();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_EXPENSE_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_EXPENSE_SUMMARY") == true)
            {
                return;
            }
            ACC_EXPENSE_SUMMARY newFrm = new ACC_EXPENSE_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_ANALYZE_REPORT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_ANALYZE_REPORT") == true)
            {
                return;
            }
            ACC_ANALYZE_REPORT newFrm = new ACC_ANALYZE_REPORT();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_CREDENCE_SUMMARY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_CREDENCE_SUMMARY") == true)
            {
                return;
            }
            ACC_CREDENCE_SUMMARY newFrm = new ACC_CREDENCE_SUMMARY();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_ACCOUNT_BALANCE_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_ACCOUNT_BALANCE") == true)
            {
                return;
            }
            ACC_ACCOUNT_BALANCE newFrm = new ACC_ACCOUNT_BALANCE();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_PRICE_ADJUST_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_PRICE_ADJUST") == true)
            {
                return;
            }
            ACC_PRICE_ADJUST newFrm = new ACC_PRICE_ADJUST();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }

        private void ACC_BOOK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			frmMain frmmain = (frmMain)this.ParentForm;
			if (frmmain.CheckChildFrmExist("ACC_BOOK") == true)
			{
				return;
			}
			ACC_BOOK newFrm = new ACC_BOOK();
			if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
			{
				newFrm.Dispose();
				newFrm = null;
			}
		}
		private void ACC_PRODUCT_BOOK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			frmMain frmmain = (frmMain)this.ParentForm;
			if (frmmain.CheckChildFrmExist("ACC_PRODUCT_BOOK") == true)
			{
				return;
			}
			ACC_PRODUCT_BOOK newFrm = new ACC_PRODUCT_BOOK();
			if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
			{
				newFrm.Dispose();
				newFrm = null;
			}
		}
		private void ACC_EXPENSE_COLUMN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMain frmmain = (frmMain)this.ParentForm;
            if (frmmain.CheckChildFrmExist("ACC_EXPENSE_COLUMN") == true)
            {
                return;
            }
            ACC_EXPENSE_COLUMN newFrm = new ACC_EXPENSE_COLUMN();
            if (newFrm != null && !frmmain.ShowMDIChild(newFrm, newFrm))
            {
                newFrm.Dispose();
                newFrm = null;
            }
        }
	}
}
