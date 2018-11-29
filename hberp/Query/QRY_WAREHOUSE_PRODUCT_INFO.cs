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
    public partial class QRY_WAREHOUSE_PRODUCT_INFO : Form
    {
        dalWarehouseProductInout _dal = new dalWarehouseProductInout();
        public QRY_WAREHOUSE_PRODUCT_INFO()
        {
            InitializeComponent();
        }

        private void QRY_WAREHOUSE_PRODUCT_INFO_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
            FillControl.FillPeriodList(cboAccName.ComboBox);            
            if (clsLxms.ShowProductSize() == 1)
            {
                rbProductSizeWip.Visible = true;
                rbWarehouseProductSizeWip.Visible = true;
                rbProductSizeWip.Checked = true;
            }
            else
            {
                rbProductSizeWip.Visible = false;
                rbWarehouseProductSizeWip.Visible = false;
                rbProductWip.Checked = true;
            }
        }

        private void QRY_WAREHOUSE_PRODUCT_INFO_Shown(object sender, EventArgs e)
        {
            SetColor();
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
                if (cboAccName.ComboBox.SelectedValue == null) return;

                DBGrid.Columns.Clear();
                if (rbProductWip.Checked)
                {
                    BindingCollection<modProductWip> list = _dal.GetProductWip(cboAccName.ComboBox.SelectedValue.ToString(), false, out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        SetColor();
                    }
                }
                else if (rbProductSizeWip.Checked)
                {
                    BindingCollection<modProductSizeWip> list = _dal.GetProductSizeWip(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DBGrid.Columns["Size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (rbWarehouseProductWip.Checked)
                {
                    BindingCollection<modWarehouseProductWip> list = _dal.GetWarehouseProductWip(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else if (rbWarehouseProductSizeWip.Checked)
                {
                    BindingCollection<modWarehouseProductSizeWip> list = _dal.GetWarehouseProductSizeWip(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DBGrid.Columns["Size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (DBGrid.ColumnCount > 0)
                {
                    DBGrid.Columns["StartQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["InputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["OutputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["EndQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void SetColor()
        {
            if (rbProductWip.Checked && DBGrid.RowCount>0)
            {
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    decimal minqty = Convert.ToDecimal(DBGrid.Rows[i].Cells["MinQty"].Value);
                    decimal maxqty = Convert.ToDecimal(DBGrid.Rows[i].Cells["MaxQty"].Value);
                    if (maxqty > 0)
                    {
                        decimal endqty = Convert.ToDecimal(DBGrid.Rows[i].Cells["EndQty"].Value);
                        if (endqty < minqty)
                        {
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else if (endqty > maxqty)
                        {
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void rbProductWip_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                string productid =DBGrid.CurrentRow.Cells["ProductId"].Value.ToString();
                string warehouseid = string.Empty;
                string size = string.Empty;
                string fromdate = string.Empty;
                string todate = string.Empty;
                modAccPeriodList mod = (modAccPeriodList)cboAccName.SelectedItem;
                fromdate = mod.StartDate.ToString("MM-dd-yyyy");
                if (mod.LockFlag == 0)
                    todate = mod.EndDate >= DateTime.Today ? mod.EndDate.ToString("MM-dd-yyyy") : DateTime.Today.ToString("MM-dd-yyyy");
                else
                    todate = mod.EndDate.ToString("MM-dd-yyyy");
                if (rbProductSizeWip.Checked)
                {
                    size = DBGrid.CurrentRow.Cells["size"].Value.ToString();
                }
                else if (rbWarehouseProductWip.Checked)
                {
                    warehouseid = DBGrid.CurrentRow.Cells["warehouseid"].Value.ToString();
                }
                else if (rbWarehouseProductSizeWip.Checked)
                {
                    warehouseid = DBGrid.CurrentRow.Cells["warehouseid"].Value.ToString();
                    size = DBGrid.CurrentRow.Cells["size"].Value.ToString();
                }
                BindingCollection<modWarehouseProductInout> list = _dal.GetIList(warehouseid, productid, string.Empty, size, string.Empty, fromdate, todate, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(this.Text, list);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
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

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void toolFind_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;

            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                DBGrid.Rows[i].Visible = true;
            }

            int startindex = 0;
            if (DBGrid.CurrentRow.Index < DBGrid.RowCount - 1)
                startindex = DBGrid.CurrentRow.Index + 1;

            string[] finds = txtFind.Text.Trim().ToUpper().Split('*');
            string flag = clsLxms.GetParameterValue("HIDE_NOT_MATCH_PRODUCT");
            if (flag == "F")   //不隐藏不匹配的
            {
                bool find = false;
                for (int i = startindex; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;                    
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                        {
                            found = false;
                        }
                    }
                    if (found)
                    {
                        DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                        find = true;
                        return;
                    }
                }
                if (!find)
                {
                    for (int i = 0; i < DBGrid.Rows.Count; i++)
                    {
                        bool found = true;
                        for (int j = 0; j < finds.Length; j++)
                        {
                            if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                            find = true;
                            return;
                        }
                    }
                }
            }
            else   //隐藏不匹配的
            {
                DBGrid.CurrentCell = null;
                for (int i = 0; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                        DBGrid.Rows[i].Visible = true;
                    else
                        DBGrid.Rows[i].Visible = false;
                }
            }
        }
    }
}
