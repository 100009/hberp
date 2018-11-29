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
    public partial class QRY_SALES_DESIGN_DETAIL : Form
    {
        dalSalesDesignForm _dal = new dalSalesDesignForm();
        public QRY_SALES_DESIGN_DETAIL()
        {
            InitializeComponent();
        }

        private void QRY_SALES_DESIGN_DETAIL_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Sales Statement"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuSalesStatement_Click));
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillDesignType(cboFormType, true);
            FillControl.FillCustomerList(lstCustomer);
            FillControl.FillEmployeeList(cboSalesMan, true, false);
            lstCustomer.BackColor = frmOptions.BACKCOLOR;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
        }

        private void QRY_SALES_DESIGN_DETAIL_Shown(object sender, EventArgs e)
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

                BindingCollection<modSalesDesignForm> list = _dal.GetIList(statuslist, cboFormType.SelectedValue.ToString(), custlist, txtInvNo.Text.Trim(), txtCustOrderNo.Text.Trim(), cboSalesMan.SelectedValue.ToString(), txtProductName.Text.Trim(), paystatuslist, invoicestatuslist, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                decimal sum = 0;
                if (list != null && list.Count > 0)
                {
                    foreach (modSalesDesignForm mod in list)
                    {
                        sum += mod.Mny * Convert.ToDecimal(mod.AdFlag);
                    }
                    ShowColor();
                    StatusLabel1.Text = "共 " + list.Count.ToString() + " 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusLabel1.Text = "共 0 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
                }
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
                if (DBGrid.Rows[i].Cells["FormType"].Value.ToString().IndexOf("退货") > 0)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void mnuSalesStatement_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;

                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                string custid = string.Empty;
                string custname = string.Empty;
                string sheetname = string.Empty;
                int copies = 1;
                int pagesize = 1;
                int pagerows = 1;
                int pagecols = 1;
                int titleindex = 1;
                int datarows = 0;
                if (DBGrid.SelectedRows.Count == 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Visible)
                            datarows++;
                    }
                }
                else
                {
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (DBGrid.SelectedRows[i].Visible)
                            datarows++;
                    }
                }
                string company = clsLxms.GetParameterValue("COMPANY_NAME");
                switch (company)
                {
                    case "深圳市蓝图净化用品有限公司":
                        sheetname = "蓝图对帐单";
                        pagesize = 17;
                        pagerows = 30;
                        pagecols = 10;
                        titleindex = 11;
                        copies = datarows / pagesize;
                        if (datarows % pagesize > 0)
                            copies++;
                        break;
                    default:
                        switch (custname)
                        {
                            case "威廉姆":
                                sheetname = "威廉姆对帐单";
                                pagesize = 19;
                                pagerows = 36;
                                pagecols = 12;
                                titleindex = 6;
                                copies = datarows / pagesize;
                                if (datarows % pagesize > 0)
                                    copies++;
                                break;
                            case "深科达":
                            case "深圳市深科达气动设备有限":
                                sheetname = "深科达对帐单";
                                pagesize = 17;
                                pagerows = 31;
                                pagecols = 13;
                                titleindex = 8;
                                copies = datarows / pagesize;
                                if (datarows % pagesize > 0)
                                    copies++;
                                break;
                            default:
                                sheetname = "普通对帐单";
                                pagesize = 22;
                                pagerows = 36;
                                pagecols = 10;
                                titleindex = 10;
                                copies = datarows / pagesize;
                                if (datarows % pagesize > 0)
                                    copies++;
                                break;
                        }
                        break;
                }

                int currrow = 0;
                if (DBGrid.SelectedRows.Count == 0)
                {
                    for (int iCopy = 0; iCopy < copies; iCopy++)
                    {
                        int rowindex = 0;
                        decimal pagesum = 0;
                        while (rowindex < pagesize && currrow < DBGrid.RowCount)
                        {
                            if (currrow < DBGrid.RowCount && DBGrid.Rows[currrow].Visible)
                            {
                                if (string.IsNullOrEmpty(custid))
                                {
                                    custid = DBGrid.Rows[currrow].Cells["CustId"].Value.ToString();
                                    custname = DBGrid.Rows[currrow].Cells["CustName"].Value.ToString();
                                    AddHeader(sheetname, pagerows, iCopy, custid, ref list);
                                }
                                else if (custid.CompareTo(DBGrid.Rows[currrow].Cells["CustId"].Value.ToString()) != 0)
                                {
                                    MessageBox.Show("您所选择的单据必须属于同一个客户!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                modSalesDesignForm modd = (modSalesDesignForm)DBGrid.Rows[currrow].DataBoundItem;
                                string col = (iCopy * pagerows + titleindex + rowindex).ToString().Trim();
                                AddItem(sheetname, modd, col, ref list);
                                rowindex++;
                                pagesum += modd.Mny;
                            }
                            currrow++;
                        }
                        AddFoot(sheetname, pagerows, iCopy, copies, pagesum, ref list);
                    }
                }
                else
                {
                    for (int iCopy = 0; iCopy < copies; iCopy++)
                    {
                        int rowindex = 0;
                        decimal pagesum = 0;
                        while (rowindex < pagesize && currrow < DBGrid.SelectedRows.Count)
                        {
                            if (DBGrid.SelectedRows[currrow].Visible)
                            {
                                if (string.IsNullOrEmpty(custid))
                                {
                                    custid = DBGrid.SelectedRows[currrow].Cells["CustId"].Value.ToString();
                                    custname = DBGrid.SelectedRows[currrow].Cells["CustName"].Value.ToString();
                                    AddHeader(sheetname, pagerows, iCopy, custid, ref list);
                                }
                                else if (custid.CompareTo(DBGrid.SelectedRows[currrow].Cells["CustId"].Value.ToString()) != 0)
                                {
                                    MessageBox.Show("您所选择的单据必须属于同一个客户!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                modSalesDesignForm modd = (modSalesDesignForm)DBGrid.SelectedRows[currrow].DataBoundItem;
                                string col = (iCopy * pagerows + titleindex + rowindex).ToString().Trim();
                                AddItem(sheetname, modd, col, ref list);
                                rowindex++;
                                pagesum += modd.Mny;
                            }
                            currrow++;
                        }
                        AddFoot(sheetname, pagerows, iCopy, copies, pagesum, ref list);
                    }
                }
                clsExport.ExportByTemplate(list, sheetname, 1, pagerows, pagecols, copies);
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

        private void AddHeader(string sheetname, int pagerows, int iCopy, string custid, ref IList<modExcelRangeData> list)
        {
            dalCustomerList dalcust = new dalCustomerList();
            modCustomerList modcust = dalcust.GetItem(custid, out Util.emsg);
            switch (sheetname)
            {
                case "蓝图对帐单":
                    list.Add(new modExcelRangeData(modcust.FullName, "B" + ((iCopy * pagerows) + 6).ToString(), "B" + ((iCopy * pagerows) + 6).ToString()));
                    list.Add(new modExcelRangeData(modcust.Tel, "I" + ((iCopy * pagerows) + 6).ToString(), "J" + ((iCopy * pagerows) + 6).ToString()));
                    list.Add(new modExcelRangeData(modcust.Fax, "I" + ((iCopy * pagerows) + 7).ToString(), "J" + ((iCopy * pagerows) + 7).ToString()));
                    list.Add(new modExcelRangeData(DateTime.Today.ToString("yyyy-MM-dd"), "I" + ((iCopy * pagerows) + 8).ToString(), "J" + ((iCopy * pagerows) + 8).ToString()));
                    break;
                case "威廉姆对帐单":
                    list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME") + "\r\n" + dtpTo.Value.ToString("yyyy") + "年" + dtpTo.Value.ToString("MM") + "月份对帐单", "A" + ((iCopy * pagerows) + 1).ToString(), "L" + ((iCopy * pagerows) + 1).ToString()));
                    list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "C" + ((iCopy * pagerows) + 2).ToString(), "C" + ((iCopy * pagerows) + 2).ToString()));
                    list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_TEL"), "C" + ((iCopy * pagerows) + 3).ToString(), "C" + ((iCopy * pagerows) + 3).ToString()));
                    list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_FAX"), "C" + ((iCopy * pagerows) + 4).ToString(), "C" + ((iCopy * pagerows) + 4).ToString()));
                    list.Add(new modExcelRangeData("联系人：" + clsLxms.GetParameterValue("COMPANY_LINKMAN"), "H" + ((iCopy * pagerows) + 2).ToString(), "K" + ((iCopy * pagerows) + 2).ToString()));
                    break;
                case "深科达对帐单":
                    list.Add(new modExcelRangeData(dtpTo.Value.ToString("yyyy") + "年" + dtpTo.Value.ToString("MM") + "月", "D" + ((iCopy * pagerows) + 6).ToString(), "D" + ((iCopy * pagerows) + 6).ToString()));
                    list.Add(new modExcelRangeData("From:" + clsLxms.GetParameterValue("COMPANY_NAME"), "H" + ((iCopy * pagerows) + 1).ToString(), "J" + ((iCopy * pagerows) + 2).ToString()));
                    list.Add(new modExcelRangeData("联系人：" + clsLxms.GetParameterValue("COMPANY_LINKMAN"), "H" + ((iCopy * pagerows) + 2).ToString(), "J" + ((iCopy * pagerows) + 2).ToString()));
                    list.Add(new modExcelRangeData("电话:" + clsLxms.GetParameterValue("COMPANY_TEL"), "H" + ((iCopy * pagerows) + 3).ToString(), "J" + ((iCopy * pagerows) + 3).ToString()));
                    list.Add(new modExcelRangeData("传真:" + clsLxms.GetParameterValue("COMPANY_FAX"), "H" + ((iCopy * pagerows) + 4).ToString(), "J" + ((iCopy * pagerows) + 4).ToString()));
                    break;
                default:
                    list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "A" + ((iCopy * pagerows) + 2).ToString(), "J" + ((iCopy * pagerows) + 2).ToString()));
                    list.Add(new modExcelRangeData("TEL：" + clsLxms.GetParameterValue("COMPANY_TEL") + "  Fax：" + clsLxms.GetParameterValue("COMPANY_FAX"), "A" + ((iCopy * pagerows) + 3).ToString(), "J" + ((iCopy * pagerows) + 3).ToString()));
                    list.Add(new modExcelRangeData("公司地址:" + clsLxms.GetParameterValue("COMPANY_ADDR"), "A" + ((iCopy * pagerows) + 4).ToString(), "J" + ((iCopy * pagerows) + 4).ToString()));

                    list.Add(new modExcelRangeData(modcust.FullName, "B" + ((iCopy * pagerows) + 6).ToString(), "D" + ((iCopy * pagerows) + 6).ToString()));
                    list.Add(new modExcelRangeData(dtpFrom.Value.ToString("yyyy-MM-dd"), "F" + ((iCopy * pagerows) + 6).ToString(), "H" + ((iCopy * pagerows) + 6).ToString()));
                    list.Add(new modExcelRangeData(dtpTo.Value.ToString("yyyy-MM-dd"), "J" + ((iCopy * pagerows) + 6).ToString(), "J" + ((iCopy * pagerows) + 6).ToString()));

                    list.Add(new modExcelRangeData(modcust.Tel, "B" + ((iCopy * pagerows) + 7).ToString(), "D" + ((iCopy * pagerows) + 7).ToString()));
                    list.Add(new modExcelRangeData(modcust.Fax, "F" + ((iCopy * pagerows) + 7).ToString(), "H" + ((iCopy * pagerows) + 7).ToString()));
                    list.Add(new modExcelRangeData(modcust.Linkman, "J" + ((iCopy * pagerows) + 7).ToString(), "J" + ((iCopy * pagerows) + 7).ToString()));
                    break;
            }
        }

        private void AddFoot(string sheetname, int pagerows, int iCopy, int copies, decimal pagesum, ref IList<modExcelRangeData> list)
        {
            switch (sheetname)
            {
                case "蓝图对帐单":
                    list.Add(new modExcelRangeData(pagesum.ToString(), "I" + ((iCopy * pagerows) + 28).ToString(), "I" + ((iCopy * pagerows) + 28).ToString()));
                    break;
                case "威廉姆对帐单":
                    list.Add(new modExcelRangeData("本期合计金额： " + clsMoney.ConvertToMoney(Convert.ToDouble(pagesum)), "B" + ((iCopy * pagerows) + 25).ToString(), "J" + ((iCopy * pagerows) + 25).ToString()));
                    list.Add(new modExcelRangeData(pagesum.ToString(), "K" + ((iCopy * pagerows) + 25).ToString(), "L" + ((iCopy * pagerows) + 25).ToString()));
                    list.Add(new modExcelRangeData("第 " + (iCopy + 1).ToString() + " 页, 共 " + copies.ToString() + " 页", "A" + ((iCopy + 1) * pagerows).ToString(), "L" + ((iCopy + 1) * pagerows).ToString()));
                    break;
                case "深科达对帐单":
                    list.Add(new modExcelRangeData(pagesum.ToString(), "K" + ((iCopy * pagerows) + 25).ToString(), "L" + ((iCopy * pagerows) + 25).ToString()));
                    break;
                default:
                    list.Add(new modExcelRangeData(clsMoney.ConvertToMoney(Convert.ToDouble(pagesum)), "B" + ((iCopy * pagerows) + 32).ToString(), "E" + ((iCopy * pagerows) + 32).ToString()));
                    list.Add(new modExcelRangeData(pagesum.ToString(), "I" + ((iCopy * pagerows) + 32).ToString(), "I" + ((iCopy * pagerows) + 32).ToString()));
                    list.Add(new modExcelRangeData("第 " + (iCopy + 1).ToString() + " 页, 共 " + copies.ToString() + " 页", "A" + (iCopy * pagerows + 1).ToString(), "J" + (iCopy * pagerows + 1).ToString()));
                    break;
            }
        }

        private void AddItem(string sheetname, modSalesDesignForm modd, string col, ref IList<modExcelRangeData> list)
        {
            switch (sheetname)
            {
                case "蓝图对帐单":
                    list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                    //list.Add(new modExcelRangeData(modd.No, "C" + col, "C" + col));
                    list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy-MM-dd"), "D" + col, "D" + col));
                    list.Add(new modExcelRangeData(modd.CustOrderNo, "E" + col, "E" + col));
                    list.Add(new modExcelRangeData(modd.UnitNo, "F" + col, "F" + col));
                    list.Add(new modExcelRangeData((modd.Qty * modd.AdFlag).ToString(), "G" + col, "G" + col));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny / modd.Qty), "H" + col, "H" + col));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny * modd.AdFlag), "I" + col, "I" + col));
                    list.Add(new modExcelRangeData(modd.Remark, "J" + col, "J" + col));
                    break;
                case "威廉姆对帐单":
                    list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy-MM-dd"), "B" + col, "B" + col));
                    list.Add(new modExcelRangeData(modd.No, "C" + col, "C" + col));
                    list.Add(new modExcelRangeData(modd.CustOrderNo, "E" + col, "E" + col));
                    list.Add(new modExcelRangeData(modd.ProductName, "F" + col, "F" + col));
                    list.Add(new modExcelRangeData((modd.Qty * modd.AdFlag).ToString(), "I" + col, "I" + col));
                    list.Add(new modExcelRangeData(modd.UnitNo, "J" + col, "J" + col));
                    if(modd.Qty>0)
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny / modd.Qty), "K" + col, "K" + col));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny * modd.AdFlag), "L" + col, "L" + col));
                    break;
                case "深科达对帐单":
                    list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy-MM-dd"), "A" + col, "A" + col));
                    list.Add(new modExcelRangeData(modd.No, "B" + col, "B" + col));
                    list.Add(new modExcelRangeData(modd.ProductName, "C" + col, "C" + col));
                    list.Add(new modExcelRangeData(modd.ProductName, "D" + col, "D" + col));
                    //list.Add(new modExcelRangeData(modd.Specify, "F" + col, "F" + col));
                    list.Add(new modExcelRangeData(modd.CustOrderNo, "G" + col, "G" + col));
                    list.Add(new modExcelRangeData(modd.UnitNo, "I" + col, "I" + col));
                    list.Add(new modExcelRangeData((modd.Qty * modd.AdFlag).ToString(), "J" + col, "J" + col));
                    if (modd.Qty > 0)
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny / modd.Qty), "K" + col, "K" + col));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny * modd.AdFlag), "L" + col, "L" + col));
                    list.Add(new modExcelRangeData(modd.Remark, "M" + col, "M" + col));
                    break;
                default:
                    list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy-MM-dd"), "A" + col, "A" + col));
                    list.Add(new modExcelRangeData(modd.CustOrderNo, "B" + col, "B" + col));
                    list.Add(new modExcelRangeData(modd.No, "C" + col, "C" + col));
                    list.Add(new modExcelRangeData(modd.ProductName, "D" + col, "E" + col));
                    //list.Add(new modExcelRangeData(modd.Specify, "F" + col, "F" + col));
                    list.Add(new modExcelRangeData((modd.Qty * modd.AdFlag).ToString(), "G" + col, "G" + col));
                    if (modd.Qty > 0)
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny / modd.Qty), "H" + col, "H" + col));
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny * modd.AdFlag), "I" + col, "I" + col));
                    list.Add(new modExcelRangeData(modd.Remark, "J" + col, "J" + col));
                    break;
            }
        }
    }
}
