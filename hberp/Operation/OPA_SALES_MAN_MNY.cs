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
    public partial class OPA_SALES_MAN_MNY : Form
    {
        dalSalesShipment _dal = new dalSalesShipment();
        public OPA_SALES_MAN_MNY()
        {
            InitializeComponent();
        }

        private void OPA_SALES_MAN_MNY_Load(object sender, EventArgs e)
        {
            //DBGrid.ContextMenuStrip.Items.Add("-");
            //DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Sales Statement"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuSalesStatement_Click));
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillEmployeeList(cboSalesMan, false, false);
            FillControl.FillCustomerList(lstCustomer);            
            lstCustomer.BackColor = frmOptions.BACKCOLOR;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
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
                string emsg = string.Empty;
                string statuslist = "9";
                if (chkNotAudit.Checked)
                    statuslist += ",0";
                if (chkAudited.Checked)
                    statuslist += ",1";

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

                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modSalesManMny> list = _dal.GetSalesManMny(statuslist, string.Empty, custlist, string.Empty, cboSalesMan.SelectedValue.ToString(), txtProductName.Text.Trim(), dtpFrom.Text, dtpTo.Text, out emsg);
                DBGrid.DataSource = list;                
                if (list != null && list.Count > 0)
                {
                    AddComboBoxColumns();
                    GetDetailSum();
                    ShowColor();
                    DBGrid.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["SumMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["SalesManMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["PaidSalesMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                    
                    DBGrid.ReadOnly = false;
                    DBGrid.Columns["SalesManMny"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid.Columns["PaidSalesMny"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid.Columns["SalesManMny"].ReadOnly = false;
                    DBGrid.Columns["PaidSalesMny"].ReadOnly = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusLabel1.Text = "共 0 行数据， 合计金额为: " + string.Format("{0:C2}", 0);
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

        private void GetDetailSum()
        {
            decimal sum = 0;
            decimal salesmny = 0;
            decimal paidsalesmny = 0;
            if (DBGrid.RowCount == 0) return;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                modSalesManMny mod = (modSalesManMny)DBGrid.Rows[i].DataBoundItem;
                sum += mod.SumMny * mod.AdFlag;
                salesmny += mod.SalesManMny * mod.AdFlag;
                paidsalesmny += mod.PaidSalesMny * mod.AdFlag;
            }

            StatusLabel1.Text = "共 " + DBGrid.RowCount.ToString() + " 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
            StatusLabel2.Text = "业务提成: " + string.Format("{0:C2}", salesmny);
            StatusLabel3.Text = "已付提成: " + string.Format("{0:C2}", paidsalesmny);
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
        }

        private void ShowColor()
        {
            if (DBGrid.RowCount == 0) return;            
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (Convert.ToInt32(DBGrid.Rows[i].Cells["AdFlag"].Value) == -1)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;                
            }
        }

        private void QRY_SALES_SHIPMENT_DETAIL_Shown(object sender, EventArgs e)
        {
            ShowColor();
        }
                
        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                if (e.RowIndex == -1) return;
                if (e.ColumnIndex == -1) return;
                if (DBGrid.Columns[e.ColumnIndex].Name == "SalesManMny" || DBGrid.Columns[e.ColumnIndex].Name == "PaidSalesMny")
                {
                    modSalesManMny mod = (modSalesManMny)DBGrid.Rows[e.RowIndex].DataBoundItem;
                    bool ret = false;
                    switch (mod.ShipType)
                    {
                        case "送货单":
                        case "收营单":
                        case "退货单":
                            ret = _dal.UpdateSalesManMny(mod.ShipId, mod.Seq, mod.SalesManMny, mod.PaidSalesMny, Util.UserId, out Util.emsg);
                            break;
                        case "设计服务":
                        case "来料加工":
                        case "设计服务退货":
                        case "来料加工退货":
                            dalSalesDesignForm dal = new dalSalesDesignForm();
                            ret = dal.UpdateSalesManMny(Convert.ToInt32(mod.ShipId), mod.SalesManMny, mod.PaidSalesMny, Util.UserId, out Util.emsg);
                            break;
                        default:
                            MessageBox.Show("无法识别该种单据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                    }
                    
                    if (ret)
                        GetDetailSum();
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
    }
}
