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
    public partial class OPA_CUSTOMER_ORDER_LIST : Form
    {
        dalCustomerOrderList _dal = new dalCustomerOrderList();
        public OPA_CUSTOMER_ORDER_LIST()
        {
            InitializeComponent();
        }

        private void OPA_CUSTOMER_ORDER_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Sales Shipment"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuSalesShipment_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Sales Design Form"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuDesignForm_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Purchase List"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuPurchaseList_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Copy Item"), LXMS.Properties.Resources.Copy, new System.EventHandler(this.mnuCopyItem_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Generate ") + clsTranslate.TranslateString("Sales Shipment"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuNewSalesShipment_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Generate ") + clsTranslate.TranslateString("Sales Design Form"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuNewDesignForm_Click));
            dalTaskGrant dalgrant = new dalTaskGrant();
            if (dalgrant.CheckAccess(Util.UserId, "OPA_011"))
                DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Generate ") + clsTranslate.TranslateString("Vendor Order List"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuNewVendorOrderList_Click));
            if (dalgrant.CheckAccess(Util.UserId, "OPA_015"))
                DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Generate ") + clsTranslate.TranslateString("Purchase List"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuNewPurchaseList_Click));
            clsTranslate.InitLanguage(this);
            FillControl.FillCustomerList(cboCustomer, true);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            //LoadData();
        }

        private void OPA_CUSTOMER_ORDER_LIST_Shown(object sender, EventArgs e)
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
                BindingCollection<modCustomerOrderList> list = _dal.GetIList(chkIncludeFinished.Checked, custlist, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid.ReadOnly = false;
                    for (int i = 0; i < DBGrid.ColumnCount; i++)
                    {
                        DBGrid.Columns[i].ReadOnly = true;
                    }
                    ShowColor();
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

        private void ShowColor()
        {
            if (DBGrid.RowCount == 0) return;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.Rows[i].DataBoundItem;
                if (mod.Qty > 0)
                {
                    decimal ratio = mod.ShipQty / mod.Qty;
                    decimal ratio_f = Convert.ToDecimal(clsLxms.GetParameterValue("ORDER_FINISHED_RATIO"));
                    decimal ratio_o = Convert.ToDecimal(clsLxms.GetParameterValue("ORDER_OVERFLOW_RATIO"));
                    if (ratio < ratio_f)
                    {
                        if(mod.RequireDate > DateTime.Today)
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                        else
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    else if (ratio <= ratio_o)
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                    else
                        DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }            
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditCustomerOrderList frm = new EditCustomerOrderList();
                frm.AddItem(Util.retValue1);
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

        private void toolEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                EditCustomerOrderList frm = new EditCustomerOrderList();
                frm.EditItem(mod.Id);
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

        private void mnuCopyItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                EditCustomerOrderList frm = new EditCustomerOrderList();
                frm.CopyItem(mod);
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
                modCustomerOrderList mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
                bool ret = _dal.Delete(mod.Id, out Util.emsg);
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            toolEdit_Click(null, null);
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            
            IList<modExcelRangeData> list = new List<modExcelRangeData>();

            decimal summny=0;
            string custid = string.Empty;
            string custorderno = string.Empty;
            dalProductList dalpdt = new dalProductList();
            if (DBGrid.SelectedRows.Count == 0)
            {
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Visible)
                    {
                        modCustomerOrderList modd = (modCustomerOrderList)DBGrid.Rows[i].DataBoundItem;
                        if (string.IsNullOrEmpty(custid))
                        {
                            dalCustomerList dalcust = new dalCustomerList();
                            modCustomerList modcust = dalcust.GetItem(modd.CustId, out Util.emsg);

                            custid = modd.CustId;
                            custorderno = modd.CustOrderNo;
                            list.Add(new modExcelRangeData(modcust.FullName, "B6", "E6"));
                            list.Add(new modExcelRangeData(modd.CustOrderNo, "I6", "I6"));
                            list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        }
                        else
                        {
                            if (custid != modd.CustId)
                            {
                                MessageBox.Show("您所选的订单必须属于同一个客户！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (custorderno != modd.CustOrderNo)
                            {
                                if (MessageBox.Show("您所选的订单不是同一个订单编号，是否要继续？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    return;
                            }
                        }
                        string col = (10 + i).ToString().Trim();
                        list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                        modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                        if (modpdt != null)
                            list.Add(new modExcelRangeData(modpdt.Brand, "C" + col, "C" + col));
                        list.Add(new modExcelRangeData(modd.Qty.ToString(), "D" + col, "D" + col));
                        list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "H" + col, "H" + col));
                        list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));

                        summny = modd.Qty * modd.Price;
                    }
                }
                list.Add(new modExcelRangeData("金额大写:" + clsMoney.ConvertToMoney(Convert.ToDouble(summny)), "A23", "I23"));
                list.Add(new modExcelRangeData(string.Format("{0:C2}", summny), "H22", "H22"));
            }
            else
            {
                for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                {
                    if (DBGrid.SelectedRows[i].Visible)
                    {
                        modCustomerOrderList modd = (modCustomerOrderList)DBGrid.SelectedRows[i].DataBoundItem;
                        if (string.IsNullOrEmpty(custid))
                        {
                            dalCustomerList dalcust = new dalCustomerList();
                            modCustomerList modcust = dalcust.GetItem(modd.CustId, out Util.emsg);

                            custid = modd.CustId;
                            custorderno = modd.CustOrderNo;
                            list.Add(new modExcelRangeData(modcust.FullName, "B6", "E6"));
                            list.Add(new modExcelRangeData(modd.CustOrderNo, "I6", "I6"));
                            list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        }
                        else
                        {
                            if (custid != modd.CustId)
                            {
                                MessageBox.Show("您所选的订单必须属于同一个客户！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (custorderno != modd.CustOrderNo)
                            {
                                if (MessageBox.Show("您所选的订单不是同一个订单编号，是否要继续？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    return;
                            }
                        }
                        string col = (10 + i).ToString().Trim();
                        list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                        modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                        if (modpdt != null)
                            list.Add(new modExcelRangeData(modpdt.Brand, "C" + col, "C" + col));
                        list.Add(new modExcelRangeData(modd.Qty.ToString(), "D" + col, "D" + col));
                        list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "H" + col, "H" + col));
                        list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));

                        summny = modd.Qty * modd.Price;
                    }
                }
                list.Add(new modExcelRangeData("金额大写:" + clsMoney.ConvertToMoney(Convert.ToDouble(summny)), "A23", "I23"));
                list.Add(new modExcelRangeData(string.Format("{0:C2}", summny), "H22", "H22"));
            }            
            clsExport.ExportByTemplate(list, "购销合同", 1, 38, 9, 1);
                
        }

        private void mnuNewSalesShipment_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                string selectionlist = string.Empty;
                if (DBGrid.SelectedRows.Count == 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Visible)
                        {
                            if (string.IsNullOrEmpty(selectionlist))
                                selectionlist = DBGrid.Rows[i].Cells["Id"].Value.ToString();
                            else
                                selectionlist += "," + DBGrid.Rows[i].Cells["Id"].Value.ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (DBGrid.SelectedRows[i].Visible)
                        {
                            if (string.IsNullOrEmpty(selectionlist))
                                selectionlist = DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                            else
                                selectionlist += "," + DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                        }
                    }
                }
                EditSalesShipment frm = new EditSalesShipment();
                frm.ImportItem("客户订单", selectionlist);
                if (frm.ShowDialog() == DialogResult.OK)
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

        private void mnuNewDesignForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                EditDesignForm frm = new EditDesignForm();
                frm.Import(Convert.ToInt32(DBGrid.CurrentRow.Cells["Id"].Value));
                if (frm.ShowDialog() == DialogResult.OK)
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
        private void mnuNewVendorOrderList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                string selectionlist = string.Empty;
                
                dalVendorList dalvendor = new dalVendorList();
                BindingCollection<modVendorList> listvendor = dalvendor.GetIList("1", string.Empty, out Util.emsg);
                if(listvendor!=null)
                {
                    frmSingleSelect frm = new frmSingleSelect();
                    frm.InitViewList("请选择供应商", listvendor, "VendorName", "VendorName", ComboBoxStyle.DropDown);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        modVendorList modvendor = dalvendor.GetItem(Util.retValue1, out Util.emsg);
                        BindingCollection<modVendorOrderList> list = new BindingCollection<modVendorOrderList>();                        
                        if (DBGrid.SelectedRows.Count == 0)
                        {
                            modCustomerOrderList modco = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                            modVendorOrderList mod = new modVendorOrderList();
                            mod.Id = 0;
                            mod.FormDate = DateTime.Today;
                            mod.RequireDate = DateTime.Today;
                            //mod.VendorOrderNo = modco.CustOrderNo;
                            mod.Remark = modco.CustName + ": " + modco.CustOrderNo;
                            mod.VendorName = Util.retValue1;
                            mod.PayMethod = string.Empty;
                            mod.PurchaseMan = Util.UserId;
                            mod.ProductId = modco.ProductId;
                            mod.ProductName = modco.ProductName;
                            if (clsLxms.GetProductSizeFlag(mod.ProductId) == 0)
                                mod.Size = 1;
                            else
                                mod.Size = modco.Size;
                            mod.Currency = modvendor.Currency;
                            mod.UnitNo = modco.UnitNo;
                            mod.Qty = modco.Qty;
                            mod.Price = 0;
                            mod.UpdateUser = Util.UserId;
                            list.Add(mod);                               
                            
                        }
                        else
                        {
                            for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                            {
                                if (DBGrid.SelectedRows[i].Visible)
                                {
                                    modCustomerOrderList modco = (modCustomerOrderList)DBGrid.SelectedRows[i].DataBoundItem;
                                    modVendorOrderList mod = new modVendorOrderList();
                                    mod.Id = 0;
                                    mod.FormDate = DateTime.Today;
                                    mod.RequireDate = DateTime.Today;
                                    //mod.VendorOrderNo = modco.CustOrderNo;
                                    mod.Remark = modco.CustName + ": " + modco.CustOrderNo;
                                    mod.VendorName = Util.retValue1;
                                    mod.PayMethod = string.Empty;
                                    mod.PurchaseMan = Util.UserId;
                                    mod.ProductId = modco.ProductId;
                                    mod.ProductName = modco.ProductName;
                                    if (clsLxms.GetProductSizeFlag(mod.ProductId) == 0)
                                        mod.Size = 1;
                                    else
                                        mod.Size = modco.Size;
                                    mod.Currency = modvendor.Currency;
                                    mod.UnitNo = modco.UnitNo;
                                    mod.Qty = modco.Qty;
                                    mod.Price = 0;
                                    mod.UpdateUser = Util.UserId;
                                    list.Add(mod);
                                }
                            }
                        }
                        VendorOrderImport frmImport = new VendorOrderImport();
                        frmImport.InitViewList(list);
                        frmImport.ShowDialog();
                    }
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
        private void mnuNewPurchaseList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                string selectionlist = string.Empty;
                if (DBGrid.SelectedRows.Count == 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Visible)
                        {
                            if (string.IsNullOrEmpty(selectionlist))
                                selectionlist = DBGrid.Rows[i].Cells["Id"].Value.ToString();
                            else
                                selectionlist += "," + DBGrid.Rows[i].Cells["Id"].Value.ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (DBGrid.SelectedRows[i].Visible)
                        {
                            if (string.IsNullOrEmpty(selectionlist))
                                selectionlist = DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                            else
                                selectionlist += "," + DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                        }
                    }
                }
                EditPurchaseList frm = new EditPurchaseList();
                frm.ImportOrder(selectionlist);
                if (frm.ShowDialog() == DialogResult.OK)
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

        private void mnuSalesShipment_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                dalSalesShipment dal = new dalSalesShipment();
                BindingCollection<modVSalesShipmentDetail> list = dal.GetVDetail(string.Empty, string.Empty, mod.CustId, string.Empty, string.Empty, mod.CustOrderNo, string.Empty, string.Empty, string.Empty, string.Empty, mod.ProductId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Sales Shipment"), list);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void mnuDesignForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                dalSalesDesignForm dal = new dalSalesDesignForm();
                BindingCollection<modSalesDesignForm> list = dal.GetIList(string.Empty, string.Empty, mod.CustId, string.Empty, mod.CustOrderNo, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Sales Design Form"), list);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void mnuPurchaseList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                modCustomerOrderList mod = (modCustomerOrderList)DBGrid.CurrentRow.DataBoundItem;
                dalPurchaseList dal = new dalPurchaseList();
                BindingCollection<modVPurchaseDetail> list = dal.GetVDetail(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, mod.ProductId, string.Empty, mod.CustOrderNo, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Purchase List"), list);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void toolImport_Click(object sender, EventArgs e)
        {
            try
            {
                frmViewList frmsel = new frmViewList();
                dalQuotationForm dalquot = new dalQuotationForm();
                BindingCollection<modVQuotationDetail> listquot = dalquot.GetVDetail(string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                if (listquot != null)
                {
                    frmsel.InitViewList("请选择要导入的客户报价单号:", listquot);
                    frmsel.Selection = true;
                    if (frmsel.ShowDialog() == DialogResult.OK)
                    {
                        modVQuotationDetail mod = dalquot.GetDetailItem(Util.retValue1, Convert.ToInt32(Util.retValue2), out Util.emsg);
                        if (mod != null)
                        {
                            EditCustomerOrderList frm = new EditCustomerOrderList();
                            frm.ImportItem(mod);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
                            }
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
