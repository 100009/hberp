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
    public partial class EditSalesShipment : Form
    {
        string _action;
        dalSalesShipment _dal = new dalSalesShipment();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditSalesShipment()
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

        private void EditSalesShipment_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtShipId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_SHIP_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;

            if (clsLxms.GetParameterValue("NEED_CUST_ORDER_NO").CompareTo("T") == 0)
                lblStarOrder.Visible = true;
            else
                lblStarOrder.Visible = false;
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
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductName");
                col.DataPropertyName = "ProductName";
                col.Name = "ProductName";
                col.Width = 160;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Specify");
                col.DataPropertyName = "Specify";
                col.Name = "Specify";
                if (clsLxms.ShowProductSpecify() == 1)
                    col.Width = 110;
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

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Size");
                col.DataPropertyName = "Size";
                col.Name = "Size";
                if (clsLxms.ShowProductSize() == 1)
                    col.Width = 50;
                else
                {
                    col.Visible = false;
                }
                col.ReadOnly = false;
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
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Amount");
                col.DataPropertyName = "Amount";
                col.Name = "Amount";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("SalesManMny");
                col.DataPropertyName = "SalesManMny";
                col.Name = "SalesManMny";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
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

                string[] showcell = { "WarehouseId" };
                DBGrid.SetParam(showcell);
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
            MTN_WAREHOUSE_LIST frmct = new MTN_WAREHOUSE_LIST();
            frmct.SelectVisible = true;
            if (frmct.ShowDialog() == DialogResult.OK)
            {
                DBGrid.CurrentRow.Cells["WarehouseId"].Value = Util.retValue1;
            }
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            dtpShipDate.Value = DateTime.Today;
            txtShipId.Text = _dal.GetNewId(dtpShipDate.Value);
            dtpMakeDate.Value = DateTime.Today;
            txtMakeMan.Text = Util.UserName;
            txtKillMny.Text = "0";
            txtOtherMny.Text = "0";
            txtInvoiceMny.Text = "0";
            txtShipMan.Text = ini.IniReadValue("SALES_SHIPMENT", "SHIP_MAN");
            txtMotorMan.Text = ini.IniReadValue("SALES_SHIPMENT", "MOTOR_MAN");

            FillControl.FillShipType(cboShipType, false);
            FillControl.FillReceiveStatus(cboReceiveStatus, false);
            FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
            cboReceiveStatus.SelectedIndex = -1;
            cboInvoiceStatus.SelectedIndex = -1;
            txtMakeMan.ReadOnly = true;
            mnuNew.Enabled = true;
            mnuDelete.Enabled = true;
            btnUpdate.Enabled = false;
            txtSalesMan.ReadOnly = true;
            txtShipId.ReadOnly = true;
            txtCustName.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            LoadDBGrid();
            status4.Image = null;
        }

        public void EditItem(string shipid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                FillControl.FillShipType(cboShipType, false);
                FillControl.FillReceiveStatus(cboReceiveStatus, false);
                FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
                modSalesShipment mod = _dal.GetItem(shipid, out Util.emsg);
                if (mod != null)
                {                    
                    txtShipId.Text = shipid;
                    dtpShipDate.Value = mod.ShipDate;
                    cboShipType.SelectedValue = mod.ShipType;
                    txtCustOrderNo.Text = mod.CustOrderNo;
                    txtShipNo.Text = mod.ShipNo;
                    txtCurrency.Text = mod.Currency;
                    txtCustName.Tag = mod.CustId;
                    txtCustName.Text = mod.CustName;
                    txtTel.Text = mod.Tel;
                    txtShipAddr.Text = mod.ShipAddr;
                    txtPayMethod.Text = mod.PayMethod;
                    txtSalesMan.Text = mod.SalesMan;
                    txtShipMan.Text = mod.ShipMan;
                    txtMotorMan.Text = mod.MotorMan;
                    txtMakeMan.Text = mod.MakeMan;
                    dtpMakeDate.Value = mod.MakeDate;
                    txtOtherReason.Text = mod.OtherReason;
                    txtOtherMny.Text = mod.OtherMny.ToString();
                    txtKillMny.Text = mod.KillMny.ToString();
                    txtRemark.Text = mod.Remark;
                    cboReceiveStatus.SelectedIndex = Convert.ToInt32(mod.ReceiveStatus);
                    cboAccountNo.SelectedValue = mod.AccountNo;
                    txtReceiveDate.Text = mod.ReceiveDate;
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
                    txtSalesMan.ReadOnly = true;
                    txtShipId.ReadOnly = true;
                    txtCustName.ReadOnly = true;
                    txtCurrency.ReadOnly = true;

                    cboReceiveStatus.Enabled = true;
                    cboInvoiceStatus.Enabled = true;
                    cboAccountNo.Enabled = true;
                    txtInvoiceMny.ReadOnly = false;
                    txtInvoiceNo.ReadOnly = false;
                    txtMakeMan.ReadOnly = true;
                    btnUpdate.Enabled = true;
                    btnReceiveDate.Enabled = true;
                    DBGrid.Rows.Clear();
                    LoadDBGrid();
                    BindingCollection<modSalesShipmentDetail> list = _dal.GetDetail(shipid, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dalProductList dalpdt = new dalProductList();
                        foreach (modSalesShipmentDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Specify;                            
                            row.Cells[3].Value = modd.UnitNo;
                            row.Cells[4].Value = modd.Size;
                            modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                            if (modpdt.SizeFlag == 0)
                                row.Cells[4].ReadOnly = true;
                            else
                                row.Cells[4].ReadOnly = false;
                            row.Cells[5].Value = modd.Qty.ToString();
                            row.Cells[6].Value = modd.Price.ToString();
                            row.Cells[7].Value = modd.Amount.ToString();
                            row.Cells[8].Value = modd.SalesManMny.ToString();
                            row.Cells[9].Value = modd.WarehouseId;
                            row.Cells[10].Value = modd.Remark;
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

        public void ImportItem(string importtype, string liststr)
        {
            _action = "NEW";
            txtShipId.Text = _dal.GetNewId(dtpShipDate.Value).ToString();
            dtpShipDate.Value = DateTime.Today;
            dtpMakeDate.Value = DateTime.Today;
            txtMakeMan.Text = Util.UserName;
            txtKillMny.Text = "0";
            txtOtherMny.Text = "0";
            txtInvoiceMny.Text = "0";
            txtShipMan.Text = ini.IniReadValue("SALES_SHIPMENT", "SHIP_MAN");
            txtMotorMan.Text = ini.IniReadValue("SALES_SHIPMENT", "MOTOR_MAN");

            FillControl.FillShipType(cboShipType, false);
            FillControl.FillReceiveStatus(cboReceiveStatus, false);
            FillControl.FillInvoiceStatus(cboInvoiceStatus, false);
            cboReceiveStatus.SelectedIndex = -1;
            cboInvoiceStatus.SelectedIndex = -1;
            txtMakeMan.ReadOnly = true;
            mnuNew.Enabled = true;
            mnuDelete.Enabled = true;
            btnUpdate.Enabled = false;
            LoadDBGrid();
            switch (importtype)
            {
                case "客户订单":
                    dalCustomerOrderList dalorder = new dalCustomerOrderList();
                    string[] id = liststr.Split(',');

                    string custid = string.Empty;
                    string custorderno = string.Empty;
                    for (int i = 0; i < id.Length; i++)
                    {
                        modCustomerOrderList modd = dalorder.GetItem(Convert.ToInt32(id[i]), out Util.emsg);
                        if (i == 0)
                        {
                            custid = modd.CustId;
                            custorderno = modd.CustOrderNo;
                            dalCustomerList dal = new DAL.dalCustomerList();
                            modCustomerList modcust = dal.GetItem(custid, out Util.emsg);
                            if (modcust != null)
                            {
                                txtCustName.Tag = modcust.CustId;
                                txtCustName.Text = modcust.CustName;
                                txtTel.Text = modcust.Tel;
                                txtSalesMan.Text = modcust.SalesMan;
                                txtPayMethod.Text = modcust.PayMethod;
                                txtShipNo.Text = _dal.GetNewNo(dtpShipDate.Value, modcust.CustId);
                                txtCurrency.Text = modcust.Currency;
                                if (!string.IsNullOrEmpty(modcust.ShipAddr))
                                    txtShipAddr.Text = modcust.ShipAddr;
                                else
                                    txtShipAddr.Text = modcust.Addr;
                                if (modcust.NeedInvoice == 1)
                                {
                                    cboReceiveStatus.SelectedIndex = 0;
                                    cboInvoiceStatus.SelectedIndex = 1;
                                }
                                else
                                {
                                    cboReceiveStatus.SelectedIndex = 1;
                                    cboInvoiceStatus.SelectedIndex = 0;
                                }
                            }
                            txtCustOrderNo.Text = modd.CustOrderNo;
                            txtCurrency.Text = modd.Currency;
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
                                if(MessageBox.Show("您所选的订单不是同一个订单编号，是否要继续？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)==DialogResult.No)
                                    return;
                            }
                        }
                        dalProductList dalpdt = new dalProductList();
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = modd.ProductId;
                        row.Cells[1].Value = modd.ProductName;
                        row.Cells[3].Value = modd.UnitNo;
                        row.Cells[4].Value = modd.Size;
                        modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                        row.Cells[2].Value = modpdt.Specify;
                        if (modpdt.SizeFlag == 0)
                            row.Cells[4].ReadOnly = true;
                        else
                            row.Cells[4].ReadOnly = false;
                        row.Cells[5].Value = modd.Qty.ToString();
                        row.Cells[6].Value = modd.Price.ToString();
                        row.Cells[7].Value = modd.SumMny.ToString();
                        row.Cells[8].Value = 0;   //SalesMan Money
                        row.Cells[9].Value =clsLxms.GetDefaultWarehouseId();
                        row.Cells[10].Value = modd.Remark;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                        
                    }
                    GetDetailSum();
                    break;
                case "采购单":
                    dalPurchaseList dalpur=new dalPurchaseList();
                    string[] purchaseid = liststr.Split(',');
                    for(int i=0;i<purchaseid.Length;i++)
                    {
                        BindingCollection<modPurchaseDetail> listpur = dalpur.GetDetail(purchaseid[i], out Util.emsg);
                        if (listpur != null && listpur.Count > 0)
                        {
                            dalProductList dalpdt = new dalProductList();
                            foreach (modPurchaseDetail modd in listpur)
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                row.CreateCells(DBGrid);
                                row.Cells[0].Value = modd.ProductId;
                                row.Cells[1].Value = modd.ProductName;                        
                                row.Cells[3].Value = modd.UnitNo;
                                row.Cells[4].Value = modd.Size;
                                modProductList modpdt = dalpdt.GetItem(modd.ProductId, out Util.emsg);
                                row.Cells[2].Value = modpdt.Specify;
                                if (modpdt.SizeFlag == 0)
                                    row.Cells[4].ReadOnly = true;
                                else
                                    row.Cells[4].ReadOnly = false;
                                row.Cells[5].Value = modd.Qty.ToString();
                                row.Cells[6].Value = 0;   //modd.Price.ToString();
                                row.Cells[7].Value = 0;   //Amount
                                row.Cells[8].Value = 0;   //SalesMan Money
                                row.Cells[9].Value = modd.WarehouseId;
                                row.Cells[10].Value = modd.Remark;
                                row.Height = 40;
                                DBGrid.Rows.Add(row);
                                row.Dispose();
                            }                            
                        }
                    }
                    GetDetailSum();
                    break;
            }            
            status4.Image = null;
        }

        private decimal GetDetailSum()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                decimal sumsalesmany = 0;
                if (DBGrid.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        decimal rowamount = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value);
                        DBGrid.Rows[i].Cells[7].Value = rowamount;
                        summny += rowamount;
                        sumsalesmany += Convert.ToDecimal(DBGrid.Rows[i].Cells[8].Value);
                    }
                }
                Status1.Text = "商品合计： " + string.Format("{0:C2}", summny);
                Status2.Text = "业务提成合计： " + string.Format("{0:C2}", sumsalesmany);
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
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                GetDetailSum();
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
                if (dtpShipDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpShipDate.Focus();
                    return;
                }
                if (cboShipType.SelectedValue == null || string.IsNullOrEmpty(cboShipType.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Ship Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboShipType.Focus();
                    return;
                }
                if (cboReceiveStatus.SelectedValue == null || string.IsNullOrEmpty(cboReceiveStatus.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Receive Status") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboReceiveStatus.Focus();
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
                    if (Convert.ToDecimal(txtInvoiceMny.Text)==0)
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
                if (txtCustName.Tag == null)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCustName.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_SHIP_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtShipNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtShipNo.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_CUST_ORDER_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtCustOrderNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust Order No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustOrderNo.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtSalesMan.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sales Man") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSalesMan.Focus();
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
                    if (DBGrid.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[4].Value.ToString())<=0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGrid.Rows[i].Cells[5].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[6].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        if (cboShipType.SelectedValue.ToString() == "样品单")
                        {
                            if (Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString()) != 0)
                            {
                                MessageBox.Show(cboShipType.SelectedValue.ToString() + "\r\n" + clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must = 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        //else
                        //{
                        //    if (Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString()) == 0)
                        //    {
                        //        MessageBox.Show(cboShipType.SelectedValue.ToString() + "\r\n" + clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //        return;
                        //    }
                        //}
                    }
                    if (DBGrid.Rows[i].Cells[8].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[8].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Sales Man Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[8].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Sales Man Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[9].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[9].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Warehouse Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                
                modSalesShipment mod = new modSalesShipment();                
                if (_action == "NEW" || _action == "ADD")
                    mod.ShipId = _dal.GetNewId(dtpShipDate.Value).ToString();
                else
                    mod.ShipId = txtShipId.Text.Trim();
                mod.ShipDate = dtpShipDate.Value;
                mod.ShipType = cboShipType.SelectedValue.ToString();
                mod.ShipNo = txtShipNo.Text.Trim();
                mod.AdFlag = cboShipType.SelectedValue.ToString().CompareTo("退货单") == 0 ? -1 : 1;
                mod.CustOrderNo = txtCustOrderNo.Text.Trim();
                mod.CustId = txtCustName.Tag.ToString();
                mod.CustName = txtCustName.Text.Trim();
                mod.Tel = txtTel.Text.Trim();
                mod.ShipAddr = txtShipAddr.Text.Trim();
                mod.PayMethod = txtPayMethod.Text.Trim();
                mod.SalesMan = txtSalesMan.Text.Trim();
                mod.ShipMan = txtShipMan.Text.Trim();
                mod.MotorMan = txtMotorMan.Text.Trim();
                mod.MakeMan = txtMakeMan.Text.Trim();
                mod.MakeDate = dtpMakeDate.Value;
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
                mod.ReceiveStatus = cboReceiveStatus.SelectedIndex;
                mod.AccountNo = cboAccountNo.SelectedValue==null? string.Empty : cboAccountNo.SelectedValue.ToString();
                mod.ReceiveDate = txtReceiveDate.Text.Trim();
                mod.InvoiceStatus = cboInvoiceStatus.SelectedIndex;
                mod.InvoiceMny = Convert.ToDecimal(txtInvoiceMny.Text);
                mod.InvoiceNo = txtInvoiceNo.Text.Trim();
                string detaillist = string.Empty;
                dalCustomerOrderList dalorder = new dalCustomerOrderList();
                BindingCollection<modSalesShipmentDetail> list = new BindingCollection<modSalesShipmentDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modSalesShipmentDetail modd = new modSalesShipmentDetail();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGrid.Rows[i].Cells[2].Value == null ? string.Empty : DBGrid.Rows[i].Cells[2].Value.ToString();
                    modd.UnitNo = DBGrid.Rows[i].Cells[3].Value == null ? string.Empty : DBGrid.Rows[i].Cells[3].Value.ToString();
                    modd.Size = Convert.ToDecimal(DBGrid.Rows[i].Cells[4].Value.ToString());           
                    modd.Qty = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.Price = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());
                    modd.SalesManMny = Convert.ToDecimal(DBGrid.Rows[i].Cells[8].Value.ToString());
                    modd.WarehouseId = DBGrid.Rows[i].Cells[9].Value.ToString();
                    modd.Remark = DBGrid.Rows[i].Cells[10].Value == null ? string.Empty : DBGrid.Rows[i].Cells[10].Value.ToString();
                    if (lblStarOrder.Visible)
                    {
                        if (!dalorder.Exists(mod.CustId, mod.CustOrderNo, modd.ProductId, out Util.emsg))
                        {
                            if (MessageBox.Show("无法找到对应的客户订单号，您要继续保存吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                        }
                    }
                    list.Add(modd);
                }
                bool ret = _dal.Save(_action, mod, list, out Util.emsg);
                if (ret)
                {
                    ini.IniWriteValue("SALES_SHIPMENT", "SHIP_MAN", txtShipMan.Text.Trim());
                    ini.IniWriteValue("SALES_SHIPMENT", "MOTOR_MAN", txtMotorMan.Text.Trim());
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
                    dalProductList dal=new dalProductList();
                    modProductList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = mod.ProductId;
                        row.Cells[1].Value = mod.ProductName;
                        row.Cells[2].Value = mod.Specify;
                        row.Cells[3].Value = mod.UnitNo;
                        if (mod.SizeFlag == 0)
                        {
                            row.Cells[4].Value = 1;
                            row.Cells[4].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[4].Value = 0;
                            row.Cells[4].ReadOnly = false;
                        }
                        row.Cells[5].Value = 1;
                        if (txtCustName.Tag != null)
                        {
                            dalProductSalePrice dalprice = new dalProductSalePrice();
                            row.Cells[6].Value = dalprice.GetPrice(mod.ProductId, txtCustName.Tag.ToString(), out Util.emsg);
                        }
                        else
                            row.Cells[6].Value = 0;
                        row.Cells[7].Value = 0;
                        row.Cells[8].Value = 0;
                        row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                        row.Cells[10].Value = "";
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

        private void txtCustName_DoubleClick(object sender, EventArgs e)
        {
            btnCust_Click(null, null);
        }

        private void txtShipMan_DoubleClick(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtShipMan);
        }

        private void txtSalesMan_DoubleClick(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtSalesMan);
        }

        private void txtMotorMan_DoubleClick(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtMotorMan);
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
                if (cboReceiveStatus.SelectedIndex == 1)
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
                bool ret = _dal.UpdateReceiveStatus(true, true, txtShipId.Text.Trim(), cboReceiveStatus.SelectedIndex, cboAccountNo.SelectedValue.ToString(), txtReceiveDate.Text.Trim(), cboInvoiceStatus.SelectedIndex, Convert.ToDecimal(txtInvoiceMny.Text), txtInvoiceNo.Text.Trim(), Util.UserId, out Util.emsg);
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

        private void dtpShipDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustName.Tag == null) return;
            if (_action == "NEW")
            {
                txtShipId.Text = _dal.GetNewId(dtpShipDate.Value);
                txtShipNo.Text = _dal.GetNewNo(dtpShipDate.Value, txtCustName.Tag.ToString());
            }
        }

        private void btnCust_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_CUSTOMER_LIST frm = new MTN_CUSTOMER_LIST();
                frm.SelectVisible = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalCustomerList dal = new DAL.dalCustomerList();
                    modCustomerList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        txtCustName.Tag = mod.CustId;
                        txtCustName.Text = mod.CustName;
                        txtTel.Text = mod.Tel;
                        txtSalesMan.Text = mod.SalesMan;
                        txtPayMethod.Text = mod.PayMethod;
                        txtShipNo.Text = _dal.GetNewNo(dtpShipDate.Value, mod.CustId);
                        txtCurrency.Text = mod.Currency;
                        if (!string.IsNullOrEmpty(mod.ShipAddr))
                            txtShipAddr.Text = mod.ShipAddr;
                        else
                            txtShipAddr.Text = mod.Addr;
                        if (mod.NeedInvoice == 1)
                        {
                            cboReceiveStatus.SelectedIndex = 0;
                            cboInvoiceStatus.SelectedIndex = 1;
                        }
                        else
                        {
                            cboReceiveStatus.SelectedIndex = 1;
                            cboInvoiceStatus.SelectedIndex = 0;
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

        private void btnSalesMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtSalesMan);
        }

        private void btnShipMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtShipMan);
        }

        private void btnMotorMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtMotorMan);
        }

        private void txtCustName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                btnCust_Click(null, null);
            }
        }

        private void txtSalesMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtSalesMan);
            }
        }

        private void txtShipMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtShipMan);
            }
        }

        private void txtMotorMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtMotorMan);
            }
        }

        private void txtMakeMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtMakeMan);
            }
        }

        private void btnReceiveDate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtReceiveDate.Text.Trim()))
                txtReceiveDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        private void mnuHistoryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                                
                if (DBGrid.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(DBGrid.CurrentRow.Cells[0].Value.ToString())) return;

                BindingCollection<modVSalesShipmentDetail> list = _dal.GetVDetail("1", "送货单,收营单", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DBGrid.CurrentRow.Cells[0].Value.ToString(), string.Empty, DateTime.Today.AddDays(-365).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
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
