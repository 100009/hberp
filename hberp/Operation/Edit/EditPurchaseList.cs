using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class EditPurchaseList : Form
    {
        string _action;
        dalPurchaseList _dal = new dalPurchaseList();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditPurchaseList()
        {
            InitializeComponent();
            FillControl.FillBankAccount(cboAccountNo, true, false);
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("History Detail"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuHistoryDetail_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
            mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
        }

        private void EditPurchaseList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtPurchaseId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_PURCHASE_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
        }

        private void LoadDBGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.Rows.Clear();
                DBGrid.Columns.Clear();
                DBGrid.ReadOnly = false;
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = false;
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductId");
                col.DataPropertyName = "ProductId";
                col.Name = "ProductId";
                col.Width = 160;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductName");
                col.DataPropertyName = "ProductName";
                col.Name = "ProductName";
                col.Width = 160;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Specify");
                col.DataPropertyName = "Specify";
                col.Name = "Specify";
                if (clsLxms.ShowProductSpecify() == 1)
                    col.Width = 100;
                else
                {
                    col.Visible = false;
                }
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("UnitNo");
                col.DataPropertyName = "UnitNo";
                col.Name = "UnitNo";
                col.Width = 40;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                int showsize = clsLxms.ShowProductSize();
                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.HeaderText = clsTranslate.TranslateString("SizeFlag");
                checkboxColumn.DataPropertyName = "SizeFlag";
                checkboxColumn.Name = "SizeFlag";
                checkboxColumn.Width = 50;
                if (showsize == 1)
                    checkboxColumn.Width = 50;
                else
                    checkboxColumn.Visible = false;
                checkboxColumn.ReadOnly = true;
                DBGrid.Columns.Add(checkboxColumn);
                checkboxColumn.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Size");
                col.DataPropertyName = "Size";
                col.Name = "Size";
                if (showsize == 1)
                    col.Width = 50;
                else
                    col.Visible = false;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 60;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Price");
                col.DataPropertyName = "Price";
                col.Name = "Price";
                col.Width = 60;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Amount");
                col.DataPropertyName = "Amount";
                col.Name = "Amount";
                col.Width = 70;
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("WarehouseId");
                col.DataPropertyName = "WarehouseId";
                col.Name = "WarehouseId";
                col.Width = 90;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Remark");
                col.DataPropertyName = "Remark";
                col.Name = "Remark";
                col.Width = 80;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("CustOrderNo");
                col.DataPropertyName = "CustOrderNo";
                col.Name = "CustOrderNo";
                col.Width = 80;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductType");
                col.DataPropertyName = "ProductType";
                col.Name = "ProductType";
                col.Width = 100;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();
                if (_action == "IMPORT")
                {
                    string[] showcell = { "WarehouseId", "ProductType" };
                    DBGrid.SetParam(showcell);
                    DBGrid.Columns["SizeFlag"].ReadOnly = false;
                }
                else
                {
                    string[] showcell = { "WarehouseId" };
                    DBGrid.SetParam(showcell);
                    DBGrid.Columns["SizeFlag"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid_ButtonSelectClick()
        {
            switch (DBGrid.Columns[DBGrid.CurrentCell.ColumnIndex].Name)
            {
                case "WarehouseId":
                    MTN_WAREHOUSE_LIST frmct = new MTN_WAREHOUSE_LIST();
                    frmct.SelectVisible = true;
                    if (frmct.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid.CurrentRow.Cells["WarehouseId"].Value = Util.retValue1;
                    }
                    break;
                case "ProductType":
                    MTN_PRODUCT_TYPE frmpdt = new MTN_PRODUCT_TYPE();
                    frmpdt.SelectVisible = true;
                    if (frmpdt.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid.CurrentRow.Cells["ProductType"].Value = Util.retValue1;
                    }
                    break;
            }
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            dtpPurchaseDate.Value = DateTime.Today;
            txtPurchaseId.Text = _dal.GetNewId(dtpPurchaseDate.Value).ToString();            
            txtKillMny.Text = "0";
            txtOtherMny.Text = "0";
            txtInvoiceMny.Text = "0";
            
            FillControl.FillPurchaseType(cboPurchaseType, false);
            FillControl.FillPayStatus(cboPayStatus, false);
            FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
            cboPayStatus.SelectedIndex = -1;
            cboInvoiceStatus.SelectedIndex = -1;
            mnuNew.Enabled = true;
            mnuDelete.Enabled = true;
            btnUpdate.Enabled = false;
            txtPurchaseId.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            LoadDBGrid();
            status4.Image = null;
        }

        public void EditItem(string purchaseid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                FillControl.FillPurchaseType(cboPurchaseType, false);
                FillControl.FillPayStatus(cboPayStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                modPurchaseList mod = _dal.GetItem(purchaseid, out Util.emsg);
                if (mod != null)
                {
                    txtPurchaseId.Text = purchaseid;
                    dtpPurchaseDate.Value = mod.PurchaseDate;
                    cboPurchaseType.SelectedValue = mod.PurchaseType;
                    txtPurchaseNo.Text = mod.PurchaseNo;
                    txtCurrency.Text = mod.Currency;
                    txtVendorName.Text = mod.VendorName;                    
                    txtPaymentMethod.Text = mod.txtPayMethod;
                    txtOtherReason.Text = mod.OtherReason;
                    txtOtherMny.Text = mod.OtherMny.ToString();
                    txtKillMny.Text = mod.KillMny.ToString();
                    txtRemark.Text = mod.Remark;
                    cboPayStatus.SelectedIndex = Convert.ToInt32(mod.PayStatus);
                    cboAccountNo.SelectedValue = mod.AccountNo;
                    txtPayDate.Text = mod.PayDate;
                    cboInvoiceStatus.SelectedIndex = mod.InvoiceStatus;
                    txtInvoiceMny.Text = mod.InvoiceMny.ToString();
                    txtInvoiceNo.Text = mod.InvoiceNo;
                    if (mod.Status == 1)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);
                        DBGrid.ReadOnly = true;
                        mnuNew.Enabled = false;
                        mnuDelete.Enabled = false;
                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        toolSave.Visible = true;
                        Util.ChangeStatus(this, false);
                        DBGrid.ReadOnly = false;
                        mnuNew.Enabled = true;
                        mnuDelete.Enabled = true;
                        toolSave.Enabled = true;
                    }
                    cboPayStatus.Enabled = true;
                    cboInvoiceStatus.Enabled = true;
                    cboAccountNo.Enabled = true;
                    txtCurrency.ReadOnly = true;
                    txtPurchaseId.ReadOnly = true;
                    txtInvoiceMny.ReadOnly = false;
                    txtInvoiceNo.ReadOnly = false;
                    btnUpdate.Enabled = true;
                    btnPayDate.Enabled = true;
                    DBGrid.Rows.Clear();
                    LoadDBGrid();
                    BindingCollection<modPurchaseDetail> list = _dal.GetDetail(purchaseid, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dalProductList dalpdt = new dalProductList();
                        foreach (modPurchaseDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Specify;
                            row.Cells[3].Value = modd.UnitNo;
                            row.Cells[4].Value = modd.SizeFlag;
                            row.Cells[5].Value = modd.Size;
                            modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                            if (modpdt.SizeFlag == 0)
                                row.Cells[5].ReadOnly = true;
                            else
                                row.Cells[5].ReadOnly = false;
                            row.Cells[6].Value = modd.Qty.ToString();
                            row.Cells[7].Value = modd.Price.ToString();
                            row.Cells[8].Value = modd.Amount.ToString();
                            row.Cells[9].Value = modd.WarehouseId;
                            row.Cells[10].Value = modd.Remark;
                            row.Cells[11].Value = modd.CustOrderNo;
                            row.Cells[12].Value = modd.ProductType;
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                        GetDetailSum();
                    }
                    //DBGrid.Enabled = true;
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

        public void Import(string[] files)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "IMPORT";
                LoadDBGrid();
                for (int fi = 0; fi < files.Length; fi++)
                {
                    Excel.Application m_objExcel = new Excel.Application();
                    m_objExcel.DisplayAlerts = false;
                    Excel.Workbooks m_objBooks = m_objExcel.Workbooks;
                    m_objBooks.Open(files[fi].ToString(), Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    Excel.Workbook m_objBook = (Excel.Workbook)m_objBooks.get_Item(1);
                    Excel.Sheets sm_objSheets = (Excel.Sheets)m_objBook.Worksheets;

                    Excel.Worksheet m_objSheet = (Excel.Worksheet)sm_objSheets.get_Item("Contract合同");
                    Excel.Range range = m_objSheet.get_Range("I6", "I7");
                    Array values = range.Cells.Value2 as Array;

                    txtInvoiceNo.Text = values.GetValue(1, 1).ToString();
                    dtpPurchaseDate.Value = DateTime.Today;
                    txtPurchaseId.Text = _dal.GetNewId(dtpPurchaseDate.Value).ToString();
                    txtKillMny.Text = "0";
                    txtOtherMny.Text = "0";
                    txtInvoiceMny.Text = "0";

                    FillControl.FillPurchaseType(cboPurchaseType, false);
                    FillControl.FillPayStatus(cboPayStatus, false);
                    FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                    cboPayStatus.SelectedIndex = -1;
                    cboInvoiceStatus.SelectedIndex = -1;
                    mnuNew.Enabled = true;
                    mnuDelete.Enabled = true;
                    btnUpdate.Enabled = false;
                    range = m_objSheet.get_Range("B10", "I21");
                    values = range.Cells.Value2 as Array;
                    dalProductList dalpdt = new dalProductList();
                    for (int i = 1; i <= values.Length / 8; i++)
                    {
                        if (values.GetValue(i, 1) != null && !string.IsNullOrEmpty(values.GetValue(i, 1).ToString()))
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = values.GetValue(i, 1).ToString();
                            modProductList modpdt = dalpdt.GetItembyName(values.GetValue(i, 1).ToString(), out Util.emsg);
                            if (modpdt != null)
                            {
                                row.Cells[0].Value = modpdt.ProductId;
                                row.Cells[1].Value = modpdt.ProductName;
                                row.Cells[2].Value = modpdt.Specify;
                                row.Cells[3].Value = modpdt.UnitNo;
                                row.Cells[4].Value = modpdt.SizeFlag;
                                if (modpdt.SizeFlag == 1)
                                {
                                    row.Cells[5].Value = "0";
                                    row.Cells[5].ReadOnly = false;
                                }
                                else
                                {
                                    row.Cells[5].Value = "1";
                                    row.Cells[5].ReadOnly = true;
                                }
                                row.Cells[6].Value = values.GetValue(i, 3).ToString();
                                row.Cells[7].Value = values.GetValue(i, 6).ToString().Replace("¥","");
                                row.Cells[8].Value = values.GetValue(i, 7).ToString().Replace("¥", "");
                                row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                                row.Cells[10].Value = values.GetValue(i, 8) == null ? string.Empty : values.GetValue(i, 8).ToString();
                                row.Cells[11].Value = modpdt.ProductType;
                                row.Cells[0].ReadOnly = true;
                                row.Cells[1].ReadOnly = true;
                                row.Cells[2].ReadOnly = true;
                                row.Cells[3].ReadOnly = true;
                                row.Cells[4].ReadOnly = true;
                                row.Cells[12].ReadOnly = true;
                            }
                            else
                            {
                                row.Cells[1].Value = values.GetValue(i, 1).ToString();
                                row.Cells[2].Value = values.GetValue(i, 2) == null ? string.Empty : values.GetValue(i, 2).ToString();
                                row.Cells[3].Value = values.GetValue(i, 4).ToString();
                                row.Cells[4].Value = -1;
                                if(DBGrid.Columns[5].Visible)
                                    row.Cells[5].Value = "0";
                                else
                                    row.Cells[5].Value = "1";
                                row.Cells[6].Value = values.GetValue(i, 3).ToString();
                                row.Cells[7].Value = values.GetValue(i, 6).ToString().Replace("¥", "");
                                row.Cells[8].Value = values.GetValue(i, 7).ToString().Replace("¥", "");
                                row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                                row.Cells[10].Value = values.GetValue(i, 8) == null ? string.Empty : values.GetValue(i, 8).ToString();
                            }
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                        else
                            break;
                    }
                    m_objBook.Close();
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

        public void ImportOrder(string liststr)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "NEW";
                LoadDBGrid();
                dtpPurchaseDate.Value = DateTime.Today;
                txtPurchaseId.Text = _dal.GetNewId(dtpPurchaseDate.Value).ToString();
                txtKillMny.Text = "0";
                txtOtherMny.Text = "0";
                txtInvoiceMny.Text = "0";

                FillControl.FillPurchaseType(cboPurchaseType, false);
                FillControl.FillPayStatus(cboPayStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                cboPayStatus.SelectedIndex = -1;
                cboInvoiceStatus.SelectedIndex = -1;
                mnuNew.Enabled = true;
                mnuDelete.Enabled = true;
                btnUpdate.Enabled = false;

                dalProductList dalpdt = new dalProductList();
                dalCustomerOrderList dalorder = new dalCustomerOrderList();
                string[] id = liststr.Split(',');

                for (int i = 0; i < id.Length; i++)
                {
                    modCustomerOrderList modd = dalorder.GetItem(Convert.ToInt32(id[i]), out Util.emsg);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);                    
                    row.Cells[0].Value = modpdt.ProductId;
                    row.Cells[1].Value = modpdt.ProductName;
                    row.Cells[2].Value = modpdt.Specify;
                    row.Cells[3].Value = modpdt.UnitNo;
                    row.Cells[4].Value = modpdt.SizeFlag;
                    if (modpdt.SizeFlag == 1)
                    {
                        row.Cells[5].Value = "0";
                        row.Cells[5].ReadOnly = false;
                    }
                    else
                    {
                        row.Cells[5].Value = "1";
                        row.Cells[5].ReadOnly = true;
                    }
                    row.Cells[6].Value = modd.Qty;
                    row.Cells[7].Value = "0";
                    row.Cells[8].Value = "0";
                    row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                    row.Cells[10].Value = modd.CustName;
                    row.Cells[11].Value = modd.CustOrderNo;
                    row.Cells[12].Value = modpdt.ProductType;
                    row.Cells[0].ReadOnly = true;
                    row.Cells[1].ReadOnly = true;
                    row.Cells[2].ReadOnly = true;
                    row.Cells[3].ReadOnly = true;
                    row.Cells[4].ReadOnly = true;
                    row.Cells[8].ReadOnly = true;
                    row.Cells[11].ReadOnly = true;
                    row.Cells[12].ReadOnly = true;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                GetDetailSum();
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
        public void ImportPO(string ordernolist)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "NEW";
                LoadDBGrid();
                dtpPurchaseDate.Value = DateTime.Today;
                txtPurchaseId.Text = _dal.GetNewId(dtpPurchaseDate.Value).ToString();
                txtKillMny.Text = "0";
                txtOtherMny.Text = "0";
                txtInvoiceMny.Text = "0";

                FillControl.FillPurchaseType(cboPurchaseType, false);
                FillControl.FillPayStatus(cboPayStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                cboPayStatus.SelectedIndex = -1;
                cboInvoiceStatus.SelectedIndex = -1;
                mnuNew.Enabled = true;
                mnuDelete.Enabled = true;
                btnUpdate.Enabled = false;

                dalProductList dalpdt = new dalProductList();
                dalVendorOrderList dalpo = new dalVendorOrderList();                
                BindingCollection<modVendorOrderList> listpo = dalpo.GetIList(ordernolist, out Util.emsg);
                foreach (modVendorOrderList modd in listpo)
                {
                    if (string.IsNullOrEmpty(txtVendorName.Text.Trim()))
                    {
                        txtVendorName.Text = modd.VendorName;
                        txtPurchaseNo.Text = _dal.GetNewNo(dtpPurchaseDate.Value, modd.VendorName);
                        txtCurrency.Text = modd.Currency;
                    }
                    else
                    {
                        if (txtVendorName.Text != modd.VendorName)
                        {
                            MessageBox.Show("请选择同一个供应商的订单!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                    if (modpdt != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);

                        row.Cells[0].Value = modpdt.ProductId;
                        row.Cells[1].Value = modpdt.ProductName;
                        row.Cells[2].Value = modpdt.Specify;
                        row.Cells[3].Value = modpdt.UnitNo;
                        row.Cells[4].Value = modpdt.SizeFlag;
                        if (modpdt.SizeFlag == 1)
                        {
                            row.Cells[5].Value = modd.Size;
                            row.Cells[5].ReadOnly = false;
                        }
                        else
                        {
                            row.Cells[5].Value = "1";
                            row.Cells[5].ReadOnly = true;
                        }
                        row.Cells[6].Value = modd.Qty;
                        row.Cells[7].Value = modd.Price;
                        row.Cells[8].Value = modd.SumMny;
                        row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                        row.Cells[10].Value = modd.Id;
                        row.Cells[11].Value = modd.VendorOrderNo;
                        row.Cells[12].Value = modpdt.ProductType;
                        row.Cells[0].ReadOnly = true;
                        row.Cells[1].ReadOnly = true;
                        row.Cells[2].ReadOnly = true;
                        row.Cells[3].ReadOnly = true;
                        row.Cells[4].ReadOnly = true;
                        row.Cells[8].ReadOnly = true;
                        row.Cells[11].ReadOnly = true;
                        row.Cells[12].ReadOnly = true;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("产品列表中没有["+ modd.ProductId + "]!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                GetDetailSum();
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
        private decimal GetDetailSum()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                if (DBGrid.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        decimal rowamount = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);
                        DBGrid.Rows[i].Cells[8].Value = rowamount;
                        summny += rowamount;
                    }
                }
                Status2.Text = "商品合计： " + string.Format("{0:C2}", summny);
                return summny;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
            {
                GetDetailSum();
            }
            else if (e.ColumnIndex == 4)
            {
                if (DBGrid.CurrentCell.Value != null)
                {
                    if (Convert.ToInt32(DBGrid.CurrentCell.Value) == 0)
                    {
                        DBGrid.CurrentRow.Cells[5].Value = "1";
                        DBGrid.CurrentRow.Cells[5].ReadOnly = true;
                    }
                    else
                    {
                        DBGrid.CurrentRow.Cells[5].Value = "0";
                        DBGrid.CurrentRow.Cells[5].ReadOnly = false;
                    }
                }
                else
                {
                    DBGrid.CurrentRow.Cells[5].Value = "1";
                    DBGrid.CurrentRow.Cells[5].ReadOnly = true;
                }
            }
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.EndEdit();
                if (dtpPurchaseDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpPurchaseDate.Focus();
                    return;
                }
                if (cboPurchaseType.SelectedValue == null || string.IsNullOrEmpty(cboPurchaseType.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Purchase Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboPurchaseType.Focus();
                    return;
                }
                if (cboPayStatus.SelectedValue == null || string.IsNullOrEmpty(cboPayStatus.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Pay Status") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboPayStatus.Focus();
                    return;
                }
                if (cboInvoiceStatus.SelectedValue == null || string.IsNullOrEmpty(cboInvoiceStatus.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Status") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboInvoiceStatus.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtOtherMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Other Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtOtherMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtOtherMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Other Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtOtherMny.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtOtherMny.Text) != 0)
                {
                    if (string.IsNullOrEmpty(txtOtherReason.Text.Trim()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Other Reason") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtOtherReason.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtKillMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Kill Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtKillMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtKillMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Kill Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtKillMny.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtInvoiceMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtInvoiceMny.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                if (cboInvoiceStatus.SelectedIndex == 2)
                {
                    if (Convert.ToDecimal(txtInvoiceMny.Text) == 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must >0 !"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceMny.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceNo.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtVendorName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Vendor Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtVendorName.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_PURCHASE_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtPurchaseNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPurchaseNo.Focus();
                    return;
                }
                if (DBGrid.RowCount == 0)
                {
                    MessageBox.Show("没有明细数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[0].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGrid.Rows[i].Cells[1].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[1].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //if (DBGrid.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    //{
                    //    MessageBox.Show(clsTranslate.TranslateString("Size Flag") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}
                    //else if (DBGrid.Rows[i].Cells[4].Value.ToString() != "0" && DBGrid.Rows[i].Cells[4].Value.ToString() != "1")
                    //{
                    //    MessageBox.Show(clsTranslate.TranslateString("Size Flag") + clsTranslate.TranslateString(" must be 0 or 1!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    if (DBGrid.Rows[i].Cells[5].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGrid.Rows[i].Cells[6].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[7].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[7].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[7].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[9].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[9].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Warehouse Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[12].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[12].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                modPurchaseList mod = new modPurchaseList();
                if (_action == "NEW" || _action == "ADD")
                    mod.PurchaseId = _dal.GetNewId(dtpPurchaseDate.Value).ToString(); 
                else
                    mod.PurchaseId = txtPurchaseId.Text.Trim();
                mod.PurchaseDate = dtpPurchaseDate.Value;
                mod.PurchaseType = cboPurchaseType.SelectedValue.ToString();
                mod.PurchaseNo = txtPurchaseNo.Text.Trim();
                mod.AdFlag = cboPurchaseType.SelectedValue.ToString().CompareTo("采购退货") == 0 ? -1 : 1;
                mod.VendorName = txtVendorName.Text.Trim();
                mod.txtPayMethod = txtPaymentMethod.Text.Trim();
                mod.OtherReason = txtOtherReason.Text.Trim();
                mod.OtherMny = Convert.ToDecimal(txtOtherMny.Text);
                mod.KillMny = Convert.ToDecimal(txtKillMny.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.Currency = txtCurrency.Text.Trim();
                dalAccCurrencyList dalcur = new dalAccCurrencyList();
                modAccCurrencyList modcur = dalcur.GetItem(txtCurrency.Text.Trim(), out Util.emsg);
                mod.ExchangeRate = modcur.ExchangeRate;
                mod.DetailSum = GetDetailSum();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                mod.PayStatus = cboPayStatus.SelectedIndex;
                mod.AccountNo = cboAccountNo.SelectedValue == null ? string.Empty : cboAccountNo.SelectedValue.ToString();
                mod.PayDate = txtPayDate.Text.Trim();
                mod.InvoiceStatus = cboInvoiceStatus.SelectedIndex;
                mod.InvoiceMny = Convert.ToDecimal(txtInvoiceMny.Text);
                mod.InvoiceNo = txtInvoiceNo.Text.Trim();
                string detaillist = string.Empty;
                BindingCollection<modPurchaseDetail> list = new BindingCollection<modPurchaseDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modPurchaseDetail modd = new modPurchaseDetail();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGrid.Rows[i].Cells[2].Value == null ? string.Empty : DBGrid.Rows[i].Cells[2].Value.ToString();
                    modd.UnitNo = DBGrid.Rows[i].Cells[3].Value == null ? string.Empty : DBGrid.Rows[i].Cells[3].Value.ToString();
                    modd.SizeFlag = DBGrid.Rows[i].Cells[4].Value == null ? 0 : (Convert.ToBoolean(DBGrid.Rows[i].Cells[4].Value) ? 0 : 1);
                    modd.Size = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.Qty = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());
                    modd.Price = Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value.ToString());
                    modd.WarehouseId = DBGrid.Rows[i].Cells[9].Value.ToString();
                    modd.Remark = DBGrid.Rows[i].Cells[10].Value == null ? string.Empty : DBGrid.Rows[i].Cells[10].Value.ToString();
                    modd.CustOrderNo = DBGrid.Rows[i].Cells[11].Value == null ? string.Empty : DBGrid.Rows[i].Cells[11].Value.ToString();
                    modd.ProductType = DBGrid.Rows[i].Cells[12].Value.ToString();
                    list.Add(modd);
                }
                bool ret = _dal.Save(_action, mod, list, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalProductList dal = new dalProductList();
                    modProductList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = mod.ProductId;
                        row.Cells[1].Value = mod.ProductName;
                        row.Cells[2].Value = mod.Specify;
                        row.Cells[3].Value = mod.UnitNo;
                        row.Cells[4].Value = mod.SizeFlag;
                        if (mod.SizeFlag == 0)
                        {
                            row.Cells[5].Value = 1;
                            row.Cells[5].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[5].Value = 0;
                            row.Cells[5].ReadOnly = false;
                        }
                        row.Cells[6].Value = 1;
                        row.Cells[7].Value = 0;
                        row.Cells[8].Value = 0;
                        row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                        row.Cells[10].Value = "";
                        row.Cells[11].Value = "";
                        row.Cells[12].Value = mod.ProductType;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
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

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (DBGrid.CurrentRow == null) return;

                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                DBGrid.Rows.RemoveAt(DBGrid.CurrentRow.Index);
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

        private void txtVendorName_DoubleClick(object sender, EventArgs e)
        {
            btnVendor_Click(null, null);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboAccountNo.SelectedValue == null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Account No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboAccountNo.Focus();
                    return;
                }
                if (cboPayStatus.SelectedIndex == 1)
                {
                    if (cboAccountNo.SelectedIndex <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Account No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboAccountNo.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtInvoiceMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                else if (!Util.IsNumeric(txtInvoiceMny.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtInvoiceMny.Focus();
                    return;
                }
                if (cboInvoiceStatus.SelectedIndex == 2)
                {
                    if (Convert.ToDecimal(txtInvoiceMny.Text) == 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice Mny") + clsTranslate.TranslateString(" must >0 !"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceMny.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Invoice No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtInvoiceNo.Focus();
                        return;
                    }
                }
                bool ret = _dal.UpdatePayStatus(true, true, txtPurchaseId.Text.Trim(), cboPayStatus.SelectedIndex, cboAccountNo.SelectedValue.ToString(), txtPayDate.Text.Trim(), cboInvoiceStatus.SelectedIndex, Convert.ToDecimal(txtInvoiceMny.Text), txtInvoiceNo.Text.Trim(), Util.UserId, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dtpPurchaseDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtVendorName.Tag == null) return;
            if (_action == "NEW")
            {
                txtPurchaseId.Text = _dal.GetNewId(dtpPurchaseDate.Value);
                txtPurchaseNo.Text = _dal.GetNewNo(dtpPurchaseDate.Value, txtVendorName.Text.Trim().ToString());
            }
        }

        private void txtVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnVendor_Click(null, null);
            }
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_VENDOR_LIST frm = new MTN_VENDOR_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalVendorList dal = new DAL.dalVendorList();
                    modVendorList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        txtVendorName.Text = mod.VendorName;
                        txtCurrency.Text = mod.Currency;
                        txtPurchaseNo.Text = _dal.GetNewNo(dtpPurchaseDate.Value, mod.VendorName);
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

        private void btnPayDate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayDate.Text.Trim()))
                txtPayDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void mnuHistoryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                if (DBGrid.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(DBGrid.CurrentRow.Cells[0].Value.ToString())) return;

                BindingCollection<modVPurchaseDetail> list = _dal.GetVDetail("1", "采购收货", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DBGrid.CurrentRow.Cells[0].Value.ToString(), string.Empty, string.Empty, DateTime.Today.AddDays(-365).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                if (list != null)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("History Detail"), list);
                    frm.ShowDialog();
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