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
    public partial class WAREHOUSE_INOUT_COST_PRICE : Form
    {
        dalWarehouseInoutForm _dal = new dalWarehouseInoutForm();
        public WAREHOUSE_INOUT_COST_PRICE()
        {
            InitializeComponent();
        }

        private void WAREHOUSE_INOUT_COST_PRICE_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            if (Util.modperiod.LockFlag == 1)
            {
                toolPrice.Visible = false;
            }
        }

        private void WAREHOUSE_INOUT_COST_PRICE_Shown(object sender, EventArgs e)
        {
            LoadData();
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
                BindingCollection<modWarehouseInoutForm> list = _dal.GetIList("1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    for (int i = 0; i < list.Count; i++)
                    {                        
                        if (list[i].PriceStatus >= 1)
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                        DBGrid.Columns["size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGrid.Columns["qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGrid.Columns["costprice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(1);
            DBGrid.Columns.Insert(1, checkboxColumn);

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("PriceStatus");
            checkboxColumn.DataPropertyName = "PriceStatus";
            checkboxColumn.Name = "PriceStatus";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(12);
            DBGrid.Columns.Insert(12, checkboxColumn);
            checkboxColumn.Dispose();
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modWarehouseInoutForm mod = (modWarehouseInoutForm)DBGrid.CurrentRow.DataBoundItem;
                EditWarehouseInout frm = new EditWarehouseInout();
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

        private void toolPrice_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;        
            modWarehouseInoutForm mod = (modWarehouseInoutForm)DBGrid.CurrentRow.DataBoundItem;
            if (mod.AccSeq > 0)
            {
                MessageBox.Show("该单据已做凭证，不可修改单价！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal price = mod.CostPrice;
            if (price == 0)
            {
                price = clsLxms.GetPrice(mod.ProductId);
            }
            frmInputBox frm = new frmInputBox("请输入产品的成本价格:", price.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                bool ret = _dal.UpdatePrice(mod.Id, Convert.ToDecimal(Util.retValue1), out Util.emsg);
                if (ret)
                {
                    DBGrid.CurrentRow.Cells["CostPrice"].Value = Convert.ToDecimal(Util.retValue1);
                    DBGrid.CurrentRow.Cells["PriceStatus"].Value = 1;
                    DBGrid.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }            
        }
    }
}
