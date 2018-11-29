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
    public partial class MTN_PRODUCT_CLEAR : Form
    {
        dalProductList _dal = new dalProductList();
        public MTN_PRODUCT_CLEAR()
        {
            InitializeComponent();
        }

        private void MTN_PRODUCT_CLEAR_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Warehouse Product Inout"), LXMS.Properties.Resources.largeLogs, new System.EventHandler(this.mnuWarehouseProductInout_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Account Product Inout"), LXMS.Properties.Resources.largeLogs, new System.EventHandler(this.mnuAccProductInout_Click));

            DBGrid2.ContextMenuStrip.Items.Add("-");
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Warehouse Product Inout"), LXMS.Properties.Resources.largeLogs, new System.EventHandler(this.mnuWarehouseProductInout2_Click));
            DBGrid2.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Account Product Inout"), LXMS.Properties.Resources.largeLogs, new System.EventHandler(this.mnuAccProductInout2_Click));
            clsTranslate.InitLanguage(this);
            if (Util.modperiod.LockFlag == 1)
            {
                toolClear.Enabled = false;
                toolRestore2.Enabled = false;
            }
            LoadData();
            LoadData2();
        }

        private void MTN_PRODUCT_CLEAR_Shown(object sender, EventArgs e)
        {
            if (DBGrid.RowCount >= 1 && DBGrid.ColumnCount >= 2)
                DBGrid.CurrentCell = DBGrid.Rows[0].Cells[1];

            if (DBGrid2.RowCount >= 1 && DBGrid2.ColumnCount >= 2)
                DBGrid2.CurrentCell = DBGrid2.Rows[0].Cells[1];
        }

        #region clear product
        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.Columns.Clear();
                BindingCollection<modProductList> list = _dal.GetUselessProduct(Util.modperiod.AccName, out Util.emsg);
                DBGrid.DataSource = list;
                DBGrid.ReadOnly = false;
                DBGrid.Enabled = true;
                if (list != null && list.Count>0)
                {
                    for (int i = 0; i < DBGrid.Columns.Count; i++)
                    {
                        DBGrid.Columns[i].ReadOnly = true;
                    }
                    AddComboBoxColumns();
                    DBGrid.CurrentCell = DBGrid.Rows[0].Cells[1];
                    if (clsLxms.ShowProductSpecify() == 0)
                    {
                        DBGrid.Columns["Specify"].Visible = false;
                    }
                    if (clsLxms.ShowProductSize() == 0)
                    {
                        DBGrid.Columns["SizeFlag"].Visible = false;
                    }
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Checked");
            checkboxColumn.DataPropertyName = "Checked";
            checkboxColumn.Name = "Checked";
            checkboxColumn.Width = 40;
            checkboxColumn.ReadOnly = false;
            DBGrid.Columns.Insert(0, checkboxColumn);
        }

        private void toolClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void toolClear_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            DBGrid.EndEdit();
            string selectionlist = string.Empty;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (DBGrid.Rows[i].Cells[0].Value != null && Convert.ToBoolean(DBGrid.Rows[i].Cells[0].Value) == true)
                {
                    if (string.IsNullOrEmpty(selectionlist))
                        selectionlist = DBGrid.Rows[i].Cells[1].Value.ToString();
                    else
                        selectionlist += "," + DBGrid.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (string.IsNullOrEmpty(selectionlist))
            {
                MessageBox.Show("请勾选您要的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("您真的要清理这些产品资料？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            bool ret = _dal.DeleteUselessProduct(selectionlist, out Util.emsg);
            if (ret)
                LoadData();
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        private void toolCheckAll_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                DBGrid.Rows[i].Cells[0].Value = 1;
            }
        }

        private void toolCheckNone_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                DBGrid.Rows[i].Cells[0].Value = 0;
            }
        }

        private void mnuWarehouseProductInout_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                dalWarehouseProductInout dal = new dalWarehouseProductInout();
                BindingCollection<modWarehouseProductInout> list = dal.GetIList(string.Empty, DBGrid.CurrentRow.Cells["ProductId"].Value.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
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
                    else
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

        private void mnuAccProductInout_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                dalAccProductInout dal = new dalAccProductInout();
                BindingCollection<modAccProductInout> list = dal.GetIList(DBGrid.CurrentRow.Cells["productid"].Value.ToString(), false, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(list[0].ProductName, list);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
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
        #endregion

        #region restore product
        private void toolRefresh2_Click(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void LoadData2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid2.Columns.Clear();
                BindingCollection<modProductList> list = _dal.GetClearProduct(out Util.emsg);
                DBGrid2.DataSource = list;
                DBGrid2.ReadOnly = false;
                DBGrid2.Enabled = true;
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < DBGrid2.Columns.Count; i++)
                    {
                        DBGrid2.Columns[i].ReadOnly = true;
                    }
                    AddComboBoxColumns2();
                    DBGrid2.CurrentCell = DBGrid2.Rows[0].Cells[1];
                    if (clsLxms.ShowProductSpecify() == 0)
                    {
                        DBGrid2.Columns["Specify"].Visible = false;
                    }
                    if (clsLxms.ShowProductSize() == 0)
                    {
                        DBGrid2.Columns["SizeFlag"].Visible = false;
                    }
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

        private void AddComboBoxColumns2()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Checked");
            checkboxColumn.DataPropertyName = "Checked";
            checkboxColumn.Name = "Checked";
            checkboxColumn.Width = 40;
            checkboxColumn.ReadOnly = false;
            DBGrid2.Columns.Insert(0, checkboxColumn);
        }
        
        private void toolRestore2_Click(object sender, EventArgs e)
        {
            if (DBGrid2.RowCount == 0) return;

            DBGrid2.EndEdit();
            string selectionlist = string.Empty;
            for (int i = 0; i < DBGrid2.RowCount; i++)
            {
                if (DBGrid2.Rows[i].Cells[0].Value != null && Convert.ToBoolean(DBGrid2.Rows[i].Cells[0].Value) == true)
                {
                    if (string.IsNullOrEmpty(selectionlist))
                        selectionlist = DBGrid2.Rows[i].Cells[1].Value.ToString();
                    else
                        selectionlist += "," + DBGrid2.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (string.IsNullOrEmpty(selectionlist))
            {
                MessageBox.Show("请勾选您要的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("您真的要恢复这些产品资料？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            bool ret = _dal.RestoreUselessProduct(selectionlist, out Util.emsg);
            if (ret)
                LoadData2();
            else
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolCheckAll2_Click(object sender, EventArgs e)
        {
            if (DBGrid2.RowCount == 0) return;

            for (int i = 0; i < DBGrid2.RowCount; i++)
            {
                DBGrid2.Rows[i].Cells[0].Value = 1;
            }
        }

        private void toolCheckNone2_Click(object sender, EventArgs e)
        {
            if (DBGrid2.RowCount == 0) return;

            for (int i = 0; i < DBGrid2.RowCount; i++)
            {
                DBGrid2.Rows[i].Cells[0].Value = 0;
            }
        }

        private void mnuWarehouseProductInout2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null) return;
                dalWarehouseProductInout dal = new dalWarehouseProductInout();
                BindingCollection<modWarehouseProductInout> list = dal.GetIList(string.Empty, DBGrid2.CurrentRow.Cells["ProductId"].Value.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
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
                    else
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

        private void mnuAccProductInout2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null) return;
                dalAccProductInout dal = new dalAccProductInout();
                BindingCollection<modAccProductInout> list = dal.GetIList(DBGrid2.CurrentRow.Cells["productid"].Value.ToString(), false, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(list[0].ProductName, list);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
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
        #endregion
    }
}
