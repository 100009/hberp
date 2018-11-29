using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.Model;
using LXMS.DAL;

namespace LXMS
{
    public partial class QRY_SALES_DESIGN_SUMMARY : Form
    {
        dalSalesDesignForm _dal = new dalSalesDesignForm();
        public QRY_SALES_DESIGN_SUMMARY()
        {
            InitializeComponent();
        }

        private void QRY_SALES_DESIGN_SUMMARY_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillDesignType(cboFormType, true);
            FillControl.FillCustomerList(lstCustomer);
            FillControl.FillEmployeeList(cboSalesMan, true, false);
            lstCustomer.BackColor = frmOptions.BACKCOLOR;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;

        }

        private void QRY_SALES_DESIGN_SUMMARY_Shown(object sender, EventArgs e)
        {
            ShowColor();
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string statuslist = "9";
                if (chkNotAudit.Checked)
                    statuslist += ",0";
                if (chkAudited.Checked)
                    statuslist += ",1";

                string paystatuslist = string.Empty;
                if (chkPay0.Checked)
                    paystatuslist += ",0";
                if (chkPay1.Checked)
                    paystatuslist += ",1";

                string invoicestatuslist = string.Empty;
                if (chkInvoice0.Checked)
                    invoicestatuslist += ",0";
                if (chkInvoice1.Checked)
                    invoicestatuslist += ",1";
                if (chkInvoice2.Checked)
                    invoicestatuslist += ",2";

                string custlist = string.Empty;
                if (lstCustomer.SelectedItems.Count > 0 && lstCustomer.SelectedItems.Count < lstCustomer.Items.Count)
                {
                    for (int i = 0; i < lstCustomer.SelectedItems.Count; i++)
                    {
                        modCustomerList mod = (modCustomerList)lstCustomer.SelectedItems[i];
                        if (i == 0)
                            custlist = mod.CustId;
                        else
                            custlist += "," + mod.CustId;
                    }
                }

                BindingCollection<modSalesDesignSummary> list = _dal.GetSalesDesignSummary(statuslist, cboFormType.SelectedValue.ToString(), custlist, string.Empty, cboSalesMan.SelectedValue.ToString(), paystatuslist, invoicestatuslist, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (list.Count > 0)
                {
                    decimal sum = 0;
                    foreach (modSalesDesignSummary mod in list)
                    {
                        sum += Convert.ToDecimal(mod.AdFlag) * mod.SumMny;
                    }
                    StatusLabel1.Text = "共 " + list.Count.ToString() + " 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
                    DBGrid.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["SumMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                }
                ShowColor();
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

        private void ShowColor()
        {
            if (DBGrid.RowCount == 0) return;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if(DBGrid.Rows[i].Cells["FormType"].Value.ToString().IndexOf("退货")>0)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            string statuslist = "9";
            if (chkNotAudit.Checked)
                statuslist += ",0";
            if (chkAudited.Checked)
                statuslist += ",1";

            string paystatuslist = string.Empty;
            if (chkPay0.Checked)
                paystatuslist += ",0";
            if (chkPay1.Checked)
                paystatuslist += ",1";

            string invoicestatuslist = string.Empty;
            if (chkInvoice0.Checked)
                invoicestatuslist += ",0";
            if (chkInvoice1.Checked)
                invoicestatuslist += ",1";
            if (chkInvoice2.Checked)
                invoicestatuslist += ",2";

            string custlist = string.Empty;
            if (lstCustomer.SelectedItems.Count > 0 && lstCustomer.SelectedItems.Count < lstCustomer.Items.Count)
            {
                for (int i = 0; i < lstCustomer.SelectedItems.Count; i++)
                {
                    modCustomerList mod = (modCustomerList)lstCustomer.SelectedItems[i];
                    if (i == 0)
                        custlist = mod.CustId;
                    else
                        custlist += "," + mod.CustId;
                }
            }

            BindingCollection<modSalesDesignForm> list = new BindingCollection<modSalesDesignForm>();
            if (DBGrid.CurrentRow.Index == DBGrid.RowCount - 1)
            {
                list = _dal.GetIList(statuslist, cboFormType.SelectedValue.ToString(), custlist, string.Empty, string.Empty, string.Empty, string.Empty, paystatuslist, invoicestatuslist, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            else
            {
                modSalesDesignSummary moditem = (modSalesDesignSummary)DBGrid.CurrentRow.DataBoundItem;
                list = _dal.GetIList(string.Empty, moditem.FormType, moditem.CustId, string.Empty, string.Empty, string.Empty, string.Empty, paystatuslist, invoicestatuslist, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            if (list != null && list.Count > 0)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Sales Shipment Detail"), list);
                frm.ShowDialog();
            }
        }
    }
}
