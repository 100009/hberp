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
    public partial class OPA_SALES_SHIPMENT : Form
    {
        dalSalesShipment _dal = new dalSalesShipment();
        public OPA_SALES_SHIPMENT()
        {
            InitializeComponent();
        }
        
        private void OPA_SALES_SHIPMENT_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Receive Status"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuReceiveStatus_Click));
            clsTranslate.InitLanguage(this);
            FillControl.FillCustomerList(cboCustomer, true);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            if (Util.modperiod.LockFlag == 1)
            {
                toolNew.Visible = false;
                toolEdit.Visible = false;
                toolDel.Visible = false;
                toolAudit.Visible = false;
                toolReset.Visible = false;
            }
            else
            {
                toolAudit.Enabled = true;
                toolReset.Enabled = false;
            }
            rbStatus0_CheckedChanged(null, null);
        }

        private void OPA_SALES_SHIPMENT_Shown(object sender, EventArgs e)
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
        
        private void ShowColor()
        {
            if (DBGrid.RowCount == 0) return;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                switch (DBGrid.Rows[i].Cells["ShipType"].Value.ToString())
                {
                    case "退货单":
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        break;
                    case "样品单":
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                        break;
                    default:
                        break;
                }                    
            }
            DBGrid.Columns["ReceiveStatus"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["ReceiveDate"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["AccountNo"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["InvoiceStatus"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["InvoiceNo"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["InvoiceMny"].DefaultCellStyle.ForeColor = Color.Red;
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string custlist = string.Empty;
                if (cboCustomer.Visible)
                    custlist = cboCustomer.SelectedValue.ToString();
                else
                    custlist = string.Empty;

                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modSalesShipment> list = _dal.GetIList(rbStatus0.Checked ? "0" : "1", string.Empty, custlist, string.Empty, string.Empty, string.Empty, string.Empty, rbStatus0.Checked ? string.Empty : dtpFrom.Text, rbStatus0.Checked ? string.Empty : dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    DBGrid.ReadOnly = false;
                    for (int i = 0; i < DBGrid.ColumnCount; i++)
                    {
                        DBGrid.Columns[i].ReadOnly = true;
                    }
                    DBGrid.Columns["ReceiveStatus"].ReadOnly = false;
                    DBGrid.Columns["ReceiveDate"].ReadOnly = false;
                    DBGrid.Columns["AccountNo"].ReadOnly = false;
                    DBGrid.Columns["InvoiceStatus"].ReadOnly = false;
                    DBGrid.Columns["InvoiceNo"].ReadOnly = false;
                    DBGrid.Columns["InvoiceMny"].ReadOnly = false;
                    ShowColor();
                    string[] showcell = { "AccountNo" };
                    DBGrid.SetParam(showcell);
                }
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(1);
            DBGrid.Columns.Insert(1, checkboxColumn);
            checkboxColumn.Dispose();

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("ReceiveStatus");
            checkboxColumn.DataPropertyName = "ReceiveStatus";
            checkboxColumn.Name = "ReceiveStatus";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(14);
            DBGrid.Columns.Insert(14, checkboxColumn);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditSalesShipment frm = new EditSalesShipment();
                frm.AddItem(Util.retValue1);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    rbStatus0.Checked = true;
                    LoadData();
                }
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

        private void toolEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modSalesShipment mod = (modSalesShipment)DBGrid.CurrentRow.DataBoundItem;                
                EditSalesShipment frm = new EditSalesShipment();
                frm.EditItem(mod.ShipId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
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

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                modSalesShipment mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                if (mod.Status == 1)
                {
                    MessageBox.Show("该单据已审核，您不能删除！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool ret = _dal.Save("DEL", mod, null, out Util.emsg);
                if (ret)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
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
        
        private void toolAudit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;
                if (DBGrid.SelectedRows.Count == 0 && DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to audit it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (DBGrid.SelectedRows.Count == 0)
                {
                    modSalesShipment mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (_dal.Audit(mod.ShipId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.ShipId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modSalesShipment mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (_dal.Audit(mod.ShipId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 1;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.ShipId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    LoadData();
                }
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

        private void toolReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;
                if (DBGrid.SelectedRows.Count == 0 && DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to reset it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (DBGrid.SelectedRows.Count == 0)
                {
                    modSalesShipment mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (mod.AccSeq > 0)
                    {
                        MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_dal.Reset(mod.ShipId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.ShipId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modSalesShipment mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (mod.AccSeq > 0)
                        {
                            MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_dal.Reset(mod.ShipId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 0;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Black;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.ShipId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }                        
                    }
                    LoadData();
                }
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            toolEdit_Click(null, null);
        }

        private void rbStatus0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus0.Checked)
            {
                groupBox1.Visible = false;
                toolAudit.Enabled = true;
                toolReset.Enabled = false;
                toolDel.Enabled = true;
                lblCustomer.Visible = false;
                cboCustomer.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
                toolAudit.Enabled = false;
                toolReset.Enabled = true;
                toolDel.Enabled = false;
                lblCustomer.Visible = true;
                cboCustomer.Visible = true;
            }
            LoadData();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            frmSingleSelect frm = new frmSingleSelect();
            frm.InitData("请选择导出单据类别：", "送货单,购销合同", "送货单", ComboBoxStyle.DropDownList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                modSalesShipment mod = (modSalesShipment)DBGrid.CurrentRow.DataBoundItem;
                BindingCollection<modSalesShipmentDetail> listdetail = _dal.GetDetail(mod.ShipId, out Util.emsg);
                dalCustomerList dalcust = new dalCustomerList();
                modCustomerList modcust = dalcust.GetItem(mod.CustId, out Util.emsg);
                switch (Util.retValue1)
                {
                    case "送货单":
                    case "收营单":
                        string company = clsLxms.GetParameterValue("COMPANY_NAME");
                        switch(company)
                        {
                            case "深圳市蓝图净化用品有限公司":
                                list.Add(new modExcelRangeData("№ " + mod.ShipNo, "H6", "I6"));
                                list.Add(new modExcelRangeData(modcust.FullName, "C7", "E7"));
                                list.Add(new modExcelRangeData(modcust.Tel, "G7", "I7"));
                                list.Add(new modExcelRangeData(modcust.Linkman, "C8", "E8"));
                                list.Add(new modExcelRangeData(modcust.Fax, "G8", "I8"));
                                list.Add(new modExcelRangeData(mod.ShipAddr, "C9", "I9"));

                                list.Add(new modExcelRangeData(mod.CustOrderNo, "A13", "C13"));
                                list.Add(new modExcelRangeData(modcust.PayMethod, "D13", "E13"));
                                dalAdminEmployeeList dalemp = new dalAdminEmployeeList();
                                modAdminEmployeeList modemp = dalemp.GetItem(mod.SalesMan, out Util.emsg);
                                if(modemp!=null)
                                    list.Add(new modExcelRangeData(modemp.EmployeeName, "F13", "F13"));
                                else
                                    list.Add(new modExcelRangeData(mod.SalesMan, "F13", "F13"));
                                list.Add(new modExcelRangeData(mod.ShipDate.ToString("yyyy年MM月dd日"), "G13", "G13"));
                                
                                for (int i = 0; i < listdetail.Count; i++)
                                {
                                    modSalesShipmentDetail modd = listdetail[i];
                                    string col = (17 + i).ToString().Trim();
                                    list.Add(new modExcelRangeData((i + 1).ToString(), "A" + col, "A" + col));
                                    list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "C" + col));
                                    list.Add(new modExcelRangeData(modd.Specify, "D" + col, "D" + col));
                                    list.Add(new modExcelRangeData(modd.Qty.ToString(), "E" + col, "E" + col));
                                    list.Add(new modExcelRangeData(modd.UnitNo, "F" + col, "F" + col));
                                    list.Add(new modExcelRangeData(modd.Price.ToString("#,##0.00"), "G" + col, "G" + col));
                                    list.Add(new modExcelRangeData((modd.Qty* modd.Price).ToString("#,##0.00"), "H" + col, "H" + col));
                                    list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));
                                }
                                clsExport.ExportByTemplate(list, "蓝图送货单", 1, 24, 10, 1);
                                break;
                            default:
                                list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "A1", "L1"));
                                list.Add(new modExcelRangeData("TEL：" + clsLxms.GetParameterValue("COMPANY_TEL") + "  Fax：" + clsLxms.GetParameterValue("COMPANY_FAX"), "A2", "L2"));
                                list.Add(new modExcelRangeData("公司地址:" + clsLxms.GetParameterValue("COMPANY_ADDR"), "A3", "L3"));
                                if (mod.InvoiceStatus >= 1)
                                    list.Add(new modExcelRangeData("S", "L4", "L4"));
                                list.Add(new modExcelRangeData(mod.ShipType, "E4", "I5"));
                                list.Add(new modExcelRangeData(mod.CustOrderNo, "C4", "D4"));
                                list.Add(new modExcelRangeData(modcust.FullName, "C5", "D5"));
                                list.Add(new modExcelRangeData(mod.ShipAddr, "C6", "D6"));
                                list.Add(new modExcelRangeData(modcust.Linkman + " " + modcust.Tel, "K4", "K4"));
                                list.Add(new modExcelRangeData(mod.ShipNo, "K5", "K5"));
                                list.Add(new modExcelRangeData(mod.ShipDate.ToString("yyyy年MM月dd日"), "K6", "K6"));
                                list.Add(new modExcelRangeData("合计金额:" + clsMoney.ConvertToMoney(Convert.ToDouble(mod.DetailSum + mod.OtherMny - mod.KillMny)), "A16", "I17"));
                                list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.DetailSum + mod.OtherMny - mod.KillMny), "J16", "K16"));
                                list.Add(new modExcelRangeData("付款方式:" + mod.PayMethod, "J17", "K17"));
                                list.Add(new modExcelRangeData(Util.UserName, "K20", "K20"));

                                for (int i = 0; i < listdetail.Count; i++)
                                {
                                    modSalesShipmentDetail modd = listdetail[i];
                                    string col = (8 + i).ToString().Trim();
                                    list.Add(new modExcelRangeData((i + 1).ToString(), "A" + col, "A" + col));
                                    list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "D" + col));
                                    list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));
                                    list.Add(new modExcelRangeData(modd.Qty.ToString(), "F" + col, "G" + col));
                                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "H" + col, "I" + col));
                                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "J" + col, "J" + col));
                                    list.Add(new modExcelRangeData(modd.Remark, "K" + col, "K" + col));
                                }
                                clsExport.ExportByTemplate(list, "送货单", 1, 20, 11, 1);
                                break;
                        }
                        break;
                    case "购销合同":
                        list.Add(new modExcelRangeData(modcust.FullName, "B6", "E6"));
                        list.Add(new modExcelRangeData(mod.ShipNo, "I6", "I6"));
                        list.Add(new modExcelRangeData(mod.ShipDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        list.Add(new modExcelRangeData("金额大写:" + clsMoney.ConvertToMoney(Convert.ToDouble(mod.DetailSum + mod.OtherMny - mod.KillMny)), "A23", "I23"));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.DetailSum + mod.OtherMny - mod.KillMny), "H22", "H22"));

                        dalProductList dalpdt = new dalProductList();
                        for (int i = 0; i < listdetail.Count; i++)
                        {
                            modSalesShipmentDetail modd = listdetail[i];
                            string col = (10 + i).ToString().Trim();
                            //list.Add(new modExcelRangeData((i+1).ToString(), "A" + col, "A" + col));
                            list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                            modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                            if(modpdt!=null)
                                list.Add(new modExcelRangeData(modpdt.Brand, "C" + col, "C" + col));
                            list.Add(new modExcelRangeData(modd.Qty.ToString(), "D" + col, "D" + col)); 
                            list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));                                           
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "H" + col, "H" + col));
                            list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));
                        }
                        clsExport.ExportByTemplate(list, "购销合同", 1, 38, 9, 1);
                        break;
                }
            }
        }

        private void toolImport_Click(object sender, EventArgs e)
        {
            try
            {
                frmSelectGrid frmsel = new frmSelectGrid();
                frmSingleSelect frms = new frmSingleSelect();
                frms.InitData("请选择导入方式：", "客户订单,采购单", "客户订单", ComboBoxStyle.DropDownList);
                if (frms.ShowDialog() == DialogResult.OK)
                {
                    switch (Util.retValue1)
                    {
                        case "客户订单":
                            dalCustomerOrderList dalorder = new dalCustomerOrderList();
                            BindingCollection<modCustomerOrderList> listorder = dalorder.GetIList(false, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                            if (listorder != null)
                            {
                                frmsel.InitViewList("请选择要导入的客户订单号:", listorder);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    EditSalesShipment frm = new EditSalesShipment();
                                    frm.ImportItem(Util.retValue1, frmSelectGrid.selectionlist);
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        LoadData();
                                    }
                                }
                            }
                            break;
                        case "采购单":
                            dalPurchaseList dal = new dalPurchaseList();
                            BindingCollection<modPurchaseList> list = dal.GetIList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                            if (list != null)
                            {
                                frmsel.InitViewList("请选择要导入的采购单号:", list);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    EditSalesShipment frm = new EditSalesShipment();
                                    frm.ImportItem(Util.retValue1, frmSelectGrid.selectionlist);
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        LoadData();
                                    }
                                }
                            }
                            break;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuReceiveStatus_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;

                string shipidlist = string.Empty;
                if (DBGrid.SelectedRows.Count == 0)
                    shipidlist = DBGrid.CurrentRow.Cells["ShipId"].Value.ToString();
                else
                {
                    string custid = DBGrid.SelectedRows[0].Cells["CustId"].Value.ToString();
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (custid.CompareTo(DBGrid.SelectedRows[i].Cells["CustId"].Value.ToString()) != 0)
                        {
                            MessageBox.Show("您所选择的单据必须属于同一个客户!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (i == 0)
                            shipidlist = DBGrid.SelectedRows[i].Cells["ShipId"].Value.ToString();
                        else
                            shipidlist += "," + DBGrid.SelectedRows[i].Cells["ShipId"].Value.ToString();
                    }
                }
                EditPayStatus frm = new EditPayStatus();
                frm.InitData("SALE", shipidlist);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
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

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modSalesShipment mod = (modSalesShipment)DBGrid.CurrentRow.DataBoundItem;
                switch (DBGrid.Columns[e.ColumnIndex].Name)
                {
                    case "ReceiveStatus":
                    case "ReceiveDate":
                    case "AccountNo":
                        _dal.UpdateReceiveStatus(mod.ShipId, mod.ReceiveStatus, mod.ReceiveDate, mod.AccountNo, Util.UserId, out Util.emsg);
                        if (DBGrid.Columns[e.ColumnIndex].Name == "ReceiveStatus" && mod.ReceiveStatus == 1)
                            DBGrid.CurrentRow.Cells["ReceiveDate"].Value=DateTime.Today.ToString("yyyy-MM-dd");
                        break;
                    case "InvoiceStatus":
                    case "InvoiceNo":
                    case "InvoiceMny":
                        _dal.UpdateInvoiceStatus(mod.ShipId, mod.InvoiceStatus, mod.InvoiceMny, mod.InvoiceNo, Util.UserId, out Util.emsg);
                        break;
                }
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

        private void DBGrid_ButtonSelectClick()
        {
            ACC_BANK_ACCOUNT frm = new ACC_BANK_ACCOUNT();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DBGrid.CurrentCell.Value = Util.retValue1;
            }
        }
    }
}