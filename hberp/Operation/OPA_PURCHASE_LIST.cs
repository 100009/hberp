using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class OPA_PURCHASE_LIST : Form
    {
        dalPurchaseList _dal = new dalPurchaseList();
        public OPA_PURCHASE_LIST()
        {
            InitializeComponent();
        }

        private void OPA_PURCHASE_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Pay Status"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuPayStatus_Click));
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            LoadData();
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
        }

        private void OPA_PURCHASE_LIST_Shown(object sender, EventArgs e)
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
                if(Convert.ToInt32(DBGrid.Rows[i].Cells["AdFlag"].Value)==-1)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
            DBGrid.Columns["PayStatus"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["PayDate"].DefaultCellStyle.ForeColor = Color.Red;
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
                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modPurchaseList> list = _dal.GetIList(rbStatus0.Checked ? "0" : "1", string.Empty, string.Empty, string.Empty, string.Empty, rbStatus0.Checked ? string.Empty : dtpFrom.Text, rbStatus0.Checked ? string.Empty : dtpTo.Text, out Util.emsg);
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
                    DBGrid.Columns["PayStatus"].ReadOnly = false;
                    DBGrid.Columns["PayDate"].ReadOnly = false;
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
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
            checkboxColumn.Dispose();

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("PayStatus");
            checkboxColumn.DataPropertyName = "PayStatus";
            checkboxColumn.Name = "PayStatus";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(13);
            DBGrid.Columns.Insert(13, checkboxColumn);
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

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditPurchaseList frm = new EditPurchaseList();
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
                modPurchaseList mod = (modPurchaseList)DBGrid.CurrentRow.DataBoundItem;
                EditPurchaseList frm = new EditPurchaseList();
                frm.EditItem(mod.PurchaseId);
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
                modPurchaseList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
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
                    modPurchaseList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (_dal.Audit(mod.PurchaseId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.PurchaseId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modPurchaseList mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (_dal.Audit(mod.PurchaseId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 1;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.PurchaseId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    modPurchaseList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (mod.AccSeq > 0)
                    {
                        MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_dal.Reset(mod.PurchaseId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.PurchaseId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modPurchaseList mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (mod.AccSeq > 0)
                        {
                            MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_dal.Reset(mod.PurchaseId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 0;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Black;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.PurchaseId, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            }
            else
            {
                groupBox1.Visible = true;
                toolAudit.Enabled = false;
                toolReset.Enabled = true;
                toolDel.Enabled = false;
            }
            LoadData();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            
            frmSingleSelect frm = new frmSingleSelect();
            frm.InitData("请选择导出单据类别：", "购销合同,采购订单", "购销合同", ComboBoxStyle.DropDownList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                modPurchaseList mod = (modPurchaseList)DBGrid.CurrentRow.DataBoundItem;
                BindingCollection<modPurchaseDetail> listdetail = _dal.GetDetail(mod.PurchaseId, out Util.emsg);
                
                switch (Util.retValue1)
                {
                    case "采购订单":    
                        dalVendorList dalv = new dalVendorList();
                        modVendorList modv = dalv.GetItem(mod.VendorName, out Util.emsg);
                        list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "A1", "I1"));
                        list.Add(new modExcelRangeData("电话：" + clsLxms.GetParameterValue("COMPANY_TEL") + "      传真：" + clsLxms.GetParameterValue("COMPANY_FAX"), "A2", "I2"));
                        list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_ADDR"), "A3", "I3"));                                                
                        list.Add(new modExcelRangeData(modv.VendorName, "C6", "E6"));
                        list.Add(new modExcelRangeData(modv.Tel, "C7", "E7"));
                        list.Add(new modExcelRangeData(modv.Fax, "C8", "E8"));
                        list.Add(new modExcelRangeData(modv.Linkman, "C9", "E9"));
                        list.Add(new modExcelRangeData(modv.Addr, "C10", "E10"));

                        list.Add(new modExcelRangeData(mod.PurchaseNo, "G6", "I6"));
                        list.Add(new modExcelRangeData(mod.PurchaseDate.ToString("yyyy年MM月dd日"), "G7", "I7"));
                        list.Add(new modExcelRangeData(mod.UpdateUser, "G8", "I8"));
                        list.Add(new modExcelRangeData(mod.txtPayMethod, "G9", "I9"));
                        list.Add(new modExcelRangeData(mod.UpdateUser, "F38", "F38"));
                        for (int i = 0; i < listdetail.Count; i++)
                        {
                            modPurchaseDetail modd = listdetail[i];
                            string col = (13 + i).ToString().Trim();
                            list.Add(new modExcelRangeData((i + 1).ToString(), "A" + col, "A" + col));
                            list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "C" + col));
                            list.Add(new modExcelRangeData(modd.Brand, "D" + col, "D" + col));
                            list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));
                            list.Add(new modExcelRangeData(modd.Qty.ToString(), "F" + col, "F" + col));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "H" + col, "H" + col));
                            list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));
                        }
                        clsExport.ExportByTemplate(list, "采购单", 1, 38, 9, 1);
                        break;
                    case "购销合同":
                        list.Add(new modExcelRangeData(mod.VendorName, "B6", "E6"));
                        list.Add(new modExcelRangeData(mod.PurchaseNo, "I6", "I6"));
                        list.Add(new modExcelRangeData(mod.PurchaseDate.ToString("yyyy年MM月dd日"), "I7", "I7"));
                        list.Add(new modExcelRangeData("金额大写:" + clsMoney.ConvertToMoney(Convert.ToDouble(mod.DetailSum + mod.OtherMny - mod.KillMny)), "A23", "I23"));
                        list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.DetailSum + mod.OtherMny - mod.KillMny), "H22", "H22"));
                        
                        for (int i = 0; i < listdetail.Count; i++)
                        {
                            modPurchaseDetail modd = listdetail[i];
                            string col = (10 + i).ToString().Trim();
                            //list.Add(new modExcelRangeData((i+1).ToString(), "A" + col, "A" + col));
                            list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                            list.Add(new modExcelRangeData(modd.Brand, "C" + col, "C" + col));
                            list.Add(new modExcelRangeData(modd.Qty.ToString(), "D" + col, "D" + col)); 
                            list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));                                           
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                            list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.Price), "H" + col, "H" + col));
                            list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));
                        }
                        clsExport.ExportByTemplate(list, "购销合同", 1, 41, 9, 1);
                        break;
                }
            }            
        }

        private void toolImport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmSelectGrid frmsel = new frmSelectGrid();
                frmSingleSelect frms = new frmSingleSelect();
                frms.InitData("请选择导入方式：", "采购订单,客户订单,EXCEL", "采购订单", ComboBoxStyle.DropDownList);
                if (frms.ShowDialog() == DialogResult.OK)
                {
                    switch (Util.retValue1)
                    {
                        case "客户订单":
                            dalPurchaseList dalorder = new dalPurchaseList();
                            BindingCollection<modCustomerOrderList> listorder = dalorder.GetImportOrderData(DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                            if (listorder != null)
                            {
                                frmsel.InitViewList("请选择要导入的客户订单号:", listorder);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    EditPurchaseList frm = new EditPurchaseList();
                                    frm.ImportOrder(frmSelectGrid.selectionlist);
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        LoadData();
                                    }
                                }
                            }
                            break;
                        case "采购订单":
                            dalVendorOrderList dalpo = new dalVendorOrderList();
                            BindingCollection<modVendorOrderList> listpo = dalpo.GetIList(false, string.Empty, string.Empty, string.Empty, DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                            if (listpo != null)
                            {
                                frmsel.InitViewList("请选择要导入的采购订单号:", listpo);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    EditPurchaseList frm = new EditPurchaseList();
                                    frm.ImportPO(frmSelectGrid.selectionlist);
                                    if (frm.ShowDialog() == DialogResult.OK)
                                    {
                                        LoadData();
                                    }
                                }
                            }
                            break;
                        case "EXCEL":
                            OpenFileDialog ofd = new OpenFileDialog();
                            string inifolder = clsLxms.GetParameterValue("PURCHASE_IMPORT_PATH");
                            if (Directory.Exists(inifolder))
                                ofd.InitialDirectory = clsLxms.GetParameterValue("PURCHASE_IMPORT_PATH");
                            else
                                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            ofd.Multiselect = true;       //允许同时选择多个文件
                            ofd.Filter = "Excel文件|*.xls;*.xlsx";
                            ofd.RestoreDirectory = true;
                            ofd.FilterIndex = 1;
                            if (ofd.ShowDialog() == DialogResult.OK)
                            {
                                string[] files = ofd.FileNames;
                                EditPurchaseList frm = new EditPurchaseList();
                                frm.Import(files);
                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    LoadData();
                                }
                            }
                            break;
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

        private void mnuPayStatus_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;

                string shipidlist = string.Empty;
                if (DBGrid.SelectedRows.Count == 0)
                    shipidlist = DBGrid.CurrentRow.Cells["PurchaseId"].Value.ToString();
                else
                {
                    string custid = DBGrid.SelectedRows[0].Cells["VendorName"].Value.ToString();
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (custid.CompareTo(DBGrid.SelectedRows[i].Cells["VendorName"].Value.ToString()) != 0)
                        {
                            MessageBox.Show("您所选择的单据必须属于同一个供应商!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (i == 0)
                            shipidlist = DBGrid.SelectedRows[i].Cells["PurchaseId"].Value.ToString();
                        else
                            shipidlist += "," + DBGrid.SelectedRows[i].Cells["PurchaseId"].Value.ToString();
                    }
                }
                EditPayStatus frm = new EditPayStatus();
                frm.InitData("PURCHASE", shipidlist);
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
                modPurchaseList mod = (modPurchaseList)DBGrid.CurrentRow.DataBoundItem;
                switch (DBGrid.Columns[e.ColumnIndex].Name)
                {
                    case "PayStatus":
                    case "PayDate":
                    case "AccountNo":
                        _dal.UpdatePayStatus(mod.PurchaseId, mod.PayStatus, mod.PayDate, mod.AccountNo, Util.UserId, out Util.emsg);
                        if (DBGrid.Columns[e.ColumnIndex].Name == "PayStatus" && mod.PayStatus == 1)
                            DBGrid.CurrentRow.Cells["PayDate"].Value = DateTime.Today.ToString("yyyy-MM-dd");
                        break;
                    case "InvoiceStatus":
                    case "InvoiceNo":
                    case "InvoiceMny":
                        _dal.UpdateInvoiceStatus(mod.PurchaseId, mod.InvoiceStatus, mod.InvoiceMny, mod.InvoiceNo, Util.UserId, out Util.emsg);
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
    }
}