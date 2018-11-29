using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LXMS.DBUtility;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class SYS_INIT : Form
    {
        public SYS_INIT()
        {
            InitializeComponent();
        }

        private void SYS_INIT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (MessageBox.Show("初始化前,您是否已备份好您的数据?", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                if (MessageBox.Show("初始化会清除系统中所有的单据及报表数据，您确定要继续数据吗?", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                string sql = string.Empty;
                sql = "truncate table acc_credence_detail";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_credence_list";
                SqlHelper.ExecuteNonQuery(sql);                
                sql = "truncate table acc_product_inout";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_check_form_detail";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_check_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "delete acc_check_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_other_payable";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_other_payable_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_other_receivable";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_other_receivable_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_payable_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_payable_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_receivable_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_receivable_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table acc_expense_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table warehouse_product_inout";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table production_form_material";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table production_form_ware";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table production_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table purchase_detail";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table purchase_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table sales_shipment_detail";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table sales_shipment";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table quotation_detail";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table quotation_form";
                SqlHelper.ExecuteNonQuery(sql);                
                sql = "truncate table warehouse_inout_form";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table asset_depre_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table asset_work_qty";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table asset_sale";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table asset_evaluate";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "delete asset_list";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "truncate table asset_add";
                SqlHelper.ExecuteNonQuery(sql);

                if (rbClearCustomerList.Checked)
                {
                    sql = "delete customer_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearCustomerType.Checked)
                {
                    sql = "delete customer_type";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearVendorList.Checked)
                {
                    sql = "delete vendor_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearVendorType.Checked)
                {
                    sql = "delete vendor_type";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearProductList.Checked)
                {
                    sql = "delete product_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearProductType.Checked)
                {
                    sql = "delete product_type_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearUserList.Checked)
                {
                    sql = "delete sys_user_privilege where user_id!='SYSADMIN'";
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = "delete sys_task_grant where role_id not in ('R001','SYSADMIN')";
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = "delete sys_user_list where user_id!='SYSADMIN'";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearRoleList.Checked)
                {
                    sql = "delete sys_role_list where role_id!='R001'";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearEmployeeList.Checked)
                {
                    sql = "delete admin_employee_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearDeptList.Checked)
                {
                    sql = "delete admin_dept_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearDegreeList.Checked)
                {
                    sql = "delete admin_degree_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearDutyList.Checked)
                {
                    sql = "delete admin_duty_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                
                if (rbClearUnitList.Checked)
                {
                    sql = "delete unit_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearWarehouseList.Checked)
                {
                    sql = "delete warehouse_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearBankList.Checked)
                {
                    sql = "delete acc_bank_list";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearCurrencyList.Checked)
                {
                    sql = "delete acc_currency_list where owner_flag!=1";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearBankAccount.Checked)
                {
                    sql = "delete acc_bank_account where account_no!='现金'";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearCredenceWord.Checked)
                {
                    sql = "delete acc_credence_word";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearBalanceStyle.Checked)
                {
                    sql = "delete acc_balance_style";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearCheckType.Checked)
                {
                    sql = "delete acc_check_type";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearCommonDigest.Checked)
                {
                    sql = "delete acc_common_digest";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearDigestType.Checked)
                {
                    sql = "delete acc_common_digest_type";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearOtherReceivableObject.Checked)
                {
                    sql = "delete other_receivable_object";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                if (rbClearOtherPayableObject.Checked)
                {
                    sql = "delete other_payable_object";
                    SqlHelper.ExecuteNonQuery(sql);
                }
                sql = "delete acc_period_list";
                SqlHelper.ExecuteNonQuery(sql);
                MessageBox.Show("系统初始化完成，请您建一个新的财务区间，然后重启程序！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditAccPeriodList frm = new EditAccPeriodList();
                frm.AddItem(Util.UserId);
                frm.ShowDialog();
                frmLogin frmlogin = new frmLogin();
                frmlogin.ShowDialog();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            MTN_CUSTOMER_LIST frm = new MTN_CUSTOMER_LIST();
            frm.ShowDialog();
        }

        private void btnCustomerType_Click(object sender, EventArgs e)
        {
            MTN_CUSTOMER_TYPE frm = new MTN_CUSTOMER_TYPE();
            frm.ShowDialog();
        }

        private void btnVendorList_Click(object sender, EventArgs e)
        {
            MTN_VENDOR_LIST frm = new MTN_VENDOR_LIST();
            frm.ShowDialog();
        }

        private void btnVendorType_Click(object sender, EventArgs e)
        {
            MTN_VENDOR_TYPE frm = new MTN_VENDOR_TYPE();
            frm.ShowDialog();
        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
            frm.ShowDialog();
        }

        private void btnProductType_Click(object sender, EventArgs e)
        {
            MTN_PRODUCT_TYPE frm = new MTN_PRODUCT_TYPE();
            frm.ShowDialog();
        }

        private void btnUserList_Click(object sender, EventArgs e)
        {
            SEC_USER_LIST frm = new SEC_USER_LIST();
            frm.ShowDialog();
        }

        private void btnRoleList_Click(object sender, EventArgs e)
        {
            SEC_ROLE_LIST frm = new SEC_ROLE_LIST();
            frm.ShowDialog();
        }

        private void btnDeptList_Click(object sender, EventArgs e)
        {
            MTN_DEPT_LIST frm = new MTN_DEPT_LIST();
            frm.ShowDialog();
        }

        private void btnDutyList_Click(object sender, EventArgs e)
        {
            MTN_DUTY_LIST frm = new MTN_DUTY_LIST();
            frm.ShowDialog();
        }

        private void btnDegreeList_Click(object sender, EventArgs e)
        {
            MTN_DEGREE_LIST frm = new MTN_DEGREE_LIST();
            frm.ShowDialog();
        }

        private void btnEmployeeList_Click(object sender, EventArgs e)
        {
            MTN_EMPLOYEE_LIST frm = new MTN_EMPLOYEE_LIST();
            frm.ShowDialog();
        }

        private void btnUnitList_Click(object sender, EventArgs e)
        {
            MTN_UNIT_LIST frm = new MTN_UNIT_LIST();
            frm.ShowDialog();
        }

        private void btnWarehouseList_Click(object sender, EventArgs e)
        {
            MTN_WAREHOUSE_LIST frm = new MTN_WAREHOUSE_LIST();
            frm.ShowDialog();
        }

        private void btnBankList_Click(object sender, EventArgs e)
        {
            ACC_BANK_LIST frm = new ACC_BANK_LIST();
            frm.ShowDialog();
        }

        private void btnBankAccount_Click(object sender, EventArgs e)
        {
            ACC_BANK_ACCOUNT frm = new ACC_BANK_ACCOUNT();
            frm.ShowDialog();
        }

        private void btnCurrencyList_Click(object sender, EventArgs e)
        {
            ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
            frm.ShowDialog();
        }

        private void btnCredenceWord_Click(object sender, EventArgs e)
        {
            ACC_CREDENCE_WORD frm = new ACC_CREDENCE_WORD();
            frm.ShowDialog();
        }

        private void btnBalanceStyle_Click(object sender, EventArgs e)
        {
            ACC_BALANCE_STYLE frm = new ACC_BALANCE_STYLE();
            frm.ShowDialog();
        }

        private void btnCheckType_Click(object sender, EventArgs e)
        {
            ACC_CHECK_TYPE frm = new ACC_CHECK_TYPE();
            frm.ShowDialog();
        }

        private void btnCommonDigest_Click(object sender, EventArgs e)
        {
            ACC_COMMON_DIGEST frm = new ACC_COMMON_DIGEST();
            frm.ShowDialog();
        }

        private void btnDigestType_Click(object sender, EventArgs e)
        {
            ACC_COMMON_DIGEST_TYPE frm = new ACC_COMMON_DIGEST_TYPE();
            frm.ShowDialog();
        }

        private void btnOtherReceivableObject_Click(object sender, EventArgs e)
        {
            OTHER_RECEIVABLE_OBJECT frm = new OTHER_RECEIVABLE_OBJECT();
            frm.ShowDialog();
        }

        private void btnOtherPayableObject_Click(object sender, EventArgs e)
        {
            OTHER_PAYABLE_OBJECT frm = new OTHER_PAYABLE_OBJECT();
            frm.ShowDialog();
        }
    }
}
