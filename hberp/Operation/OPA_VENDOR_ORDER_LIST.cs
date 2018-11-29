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
    public partial class OPA_VENDOR_ORDER_LIST : Form
    {
        dalVendorOrderList _dal = new dalVendorOrderList();
        public OPA_VENDOR_ORDER_LIST()
        {
            InitializeComponent();
        }

        private void OPA_VENDOR_ORDER_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Purchase List"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuPurchaseList_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Copy Item"), LXMS.Properties.Resources.Copy, new System.EventHandler(this.mnuCopyItem_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Generate ") + clsTranslate.TranslateString("Purchase List"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuNewPurchaseList_Click));
            clsTranslate.InitLanguage(this);
            FillControl.FillVendorList(cboVendor, "1", string.Empty, true);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            //LoadData();
        }

        private void OPA_VENDOR_ORDER_LIST_Shown(object sender, EventArgs e)
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
                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modVendorOrderList> list = _dal.GetIList(chkIncludeFinished.Checked, cboVendor.SelectedValue.ToString(), string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
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
                modVendorOrderList mod = (modVendorOrderList)DBGrid.Rows[i].DataBoundItem;
                if (mod.Qty > 0)
                {
                    decimal ratio = mod.ReceivedQty / mod.Qty;
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
                EditVendorOrderList frm = new EditVendorOrderList();
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
                modVendorOrderList mod = (modVendorOrderList)DBGrid.CurrentRow.DataBoundItem;
                EditVendorOrderList frm = new EditVendorOrderList();
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
                modVendorOrderList mod = (modVendorOrderList)DBGrid.CurrentRow.DataBoundItem;
                EditVendorOrderList frm = new EditVendorOrderList();
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
                modVendorOrderList mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
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
            string vendorname = string.Empty;
            string vendororderno = string.Empty;
            dalProductList dalpdt = new dalProductList();
            if (DBGrid.SelectedRows.Count == 0)
            {
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Visible)
                    {
                        modVendorOrderList modd = (modVendorOrderList)DBGrid.Rows[i].DataBoundItem;
                        if (string.IsNullOrEmpty(vendorname))
                        {
                            dalVendorList dalvendor = new dalVendorList();

                            vendorname = modd.VendorName;
                            vendororderno = modd.VendorOrderNo;
                            list.Add(new modExcelRangeData(modd.VendorName, "B6", "E6"));
                            list.Add(new modExcelRangeData(modd.VendorOrderNo, "I6", "I6"));
                            list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        }
                        else
                        {
                            if (vendorname != modd.VendorName)
                            {
                                MessageBox.Show("您所选的订单必须属于同一个供应商！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (vendororderno != modd.VendorOrderNo)
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
                        modVendorOrderList modd = (modVendorOrderList)DBGrid.SelectedRows[i].DataBoundItem;
                        if (string.IsNullOrEmpty(vendorname))
                        {
                            dalVendorList dalcust = new dalVendorList();
                            
                            vendorname = modd.VendorName;
                            vendororderno = modd.VendorOrderNo;
                            list.Add(new modExcelRangeData(modd.VendorName, "B6", "E6"));
                            list.Add(new modExcelRangeData(modd.VendorOrderNo, "I6", "I6"));
                            list.Add(new modExcelRangeData(modd.FormDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        }
                        else
                        {
                            if (vendorname != modd.VendorName)
                            {
                                MessageBox.Show("您所选的订单必须属于同一个供应商！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (vendororderno != modd.VendorOrderNo)
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
                frm.ImportPO(selectionlist);
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

        private void mnuPurchaseList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                modVendorOrderList mod = (modVendorOrderList)DBGrid.CurrentRow.DataBoundItem;
                dalPurchaseList dal = new dalPurchaseList();
                BindingCollection<modVPurchaseDetail> list = dal.GetVDetail(string.Empty, string.Empty, string.Empty, mod.VendorName, mod.VendorOrderNo, string.Empty, string.Empty, string.Empty, string.Empty, mod.ProductId, string.Empty, string.Empty, string.Empty, out Util.emsg);
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
                this.Cursor = Cursors.WaitCursor;

                frmSingleSelect frm = new frmSingleSelect();
                dalVendorList dalvendor = new dalVendorList();
                BindingCollection<modVendorList> listvendor = dalvendor.GetIList("1", string.Empty, out Util.emsg);
                if (listvendor != null)
                {
                    frm.InitViewList("请选择供应商", listvendor, "VendorName", "VendorName", ComboBoxStyle.DropDown);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        modVendorList modvendor = dalvendor.GetItem(Util.retValue1, out Util.emsg);
                        frmSelectGrid frmsel = new frmSelectGrid();

                        dalPurchaseList dalorder = new dalPurchaseList();
                        BindingCollection<modCustomerOrderList> listorder = dalorder.GetImportOrderData(DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                        if (listorder != null)
                        {
                            frmsel.InitViewList("请选择要导入的客户订单号:", listorder);
                            if (frmsel.ShowDialog() == DialogResult.OK)
                            {
                                BindingCollection<modVendorOrderList> list = new BindingCollection<modVendorOrderList>();
                                dalCustomerOrderList dalco = new dalCustomerOrderList();
                                BindingCollection<modCustomerOrderList> listco = dalco.GetIList(frmSelectGrid.selectionlist, out Util.emsg);
                                foreach (modCustomerOrderList modco in listco)
                                {
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
                                VendorOrderImport frmImport = new VendorOrderImport();
                                frmImport.InitViewList(list);
                                if (frmImport.ShowDialog() == DialogResult.OK)
                                {
                                    LoadData();
                                }
                            }
                        }
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
    }
}
