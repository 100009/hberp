using System;
using System.Drawing;
using System.Windows.Forms;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;
using LXMS.Operation.Edit;

namespace LXMS
{
    public partial class EditProductionForm : Form
    {
        string _action;
        dalProductionForm _dal = new dalProductionForm();
        INIClass ini = new INIClass(Util.INI_FILE);
        bool _showprice=false;
        public EditProductionForm()
        {
            InitializeComponent();
            DBGridWare.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            DBGridMaterial.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
        }

        private void EditProductionForm_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtFormId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_PRODUCTION_NO").CompareTo("T") == 0)
                lblStar.Visible = true;
            else
                lblStar.Visible = false;
        }

        private void LoadDBGridWare(bool showprice)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _showprice = showprice;
                DBGridWare.Rows.Clear();
                DBGridWare.Columns.Clear();
                DBGridWare.ReadOnly = false;
                DBGridWare.AllowUserToAddRows = false;
                DBGridWare.AllowUserToDeleteRows = false;
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductId");
                col.DataPropertyName = "ProductId";
                col.Name = "ProductId";
                col.Width = 160;
                col.ReadOnly = false;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductName");
                col.DataPropertyName = "ProductName";
                col.Name = "ProductName";
                col.Width = 160;
                col.ReadOnly = true;
                DBGridWare.Columns.Add(col);
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
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Size");
                col.DataPropertyName = "Size";
                col.Name = "Size";
                col.Width = 50;
                col.ReadOnly = showprice ? true : false;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 70;
                col.ReadOnly = showprice ? true : false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProcessPrice");
                col.DataPropertyName = "ProcessPrice";
                col.Name = "ProcessPrice";
                col.Width = 70;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("CostPrice");
                col.DataPropertyName = "CostPrice";
                col.Name = "CostPrice";
                if (showprice)
                {
                    col.Visible = true;
                    col.Width = 70;
                    col.ReadOnly = false;
                }
                else
                {
                    col.Visible = false;
                    col.ReadOnly = true;
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProcessAmount");
                col.DataPropertyName = "ProcessAmount";
                col.Name = "ProcessAmount";
                if (showprice)
                {
                    col.Visible = true;
                    col.Width = 70;
                    col.ReadOnly = true;
                }
                else
                {
                    col.Visible = false;
                    col.ReadOnly = true;
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("CostAmount");
                col.DataPropertyName = "CostAmount";
                col.Name = "CostAmount";
                if (showprice)
                {
                    col.Visible = true;
                    col.Width = 70;
                    col.ReadOnly = true;
                }
                else
                {
                    col.Visible = false;
                    col.ReadOnly = true;
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("WarehouseId");
                col.DataPropertyName = "WarehouseId";
                col.Name = "WarehouseId";
                col.Width = 90;
                col.Visible = true;
                col.ReadOnly = showprice ? true : false;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Remark");
                col.DataPropertyName = "Remark";
                col.Name = "Remark";
                col.Width = 80;
                col.ReadOnly = showprice ? true : false;
                DBGridWare.Columns.Add(col);
                col.Dispose();

                string[] showcell = { "WarehouseId" };
                DBGridWare.SetParam(showcell);
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
        private void DBGridWare_ButtonSelectClick()
        {
            MTN_WAREHOUSE_LIST frmct = new MTN_WAREHOUSE_LIST();
            frmct.SelectVisible = true;
            if (frmct.ShowDialog() == DialogResult.OK)
            {
                DBGridWare.CurrentRow.Cells["WarehouseId"].Value = Util.retValue1;
            }
        }

        private void LoadDBGridMaterial(bool showprice)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGridMaterial.Rows.Clear();
                DBGridMaterial.Columns.Clear();
                DBGridMaterial.ReadOnly = false;
                DBGridMaterial.AllowUserToAddRows = false;
                DBGridMaterial.AllowUserToDeleteRows = false;
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductId");
                col.DataPropertyName = "ProductId";
                col.Name = "ProductId";
                col.Width = 160;
                col.ReadOnly = false;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductName");
                col.DataPropertyName = "ProductName";
                col.Name = "ProductName";
                col.Width = 160;
                col.ReadOnly = true;
                DBGridMaterial.Columns.Add(col);
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
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Size");
                col.DataPropertyName = "Size";
                col.Name = "Size";
                col.Width = 50;
                col.ReadOnly = showprice ? true : false;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 70;
                col.ReadOnly = showprice ? true : false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("CostPrice");
                col.DataPropertyName = "CostPrice";
                col.Name = "CostPrice";
                if (showprice)
                {
                    col.Visible = true;
                    col.Width = 70;
                    col.ReadOnly = false;
                }
                else
                {
                    col.Visible = false;
                    col.ReadOnly = true;
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Amount");
                col.DataPropertyName = "Amount";
                col.Name = "Amount";
                if (showprice)
                {
                    col.Visible = true;
                    col.Width = 70;
                    col.ReadOnly = true;
                }
                else
                {
                    col.Visible = false;
                    col.ReadOnly = true;
                }
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("WarehouseId");
                col.DataPropertyName = "WarehouseId";
                col.Name = "WarehouseId";
                col.Width = 60;
                col.Visible = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = showprice ? true : false;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Remark");
                col.DataPropertyName = "Remark";
                col.Name = "Remark";
                col.Width = 80;
                col.ReadOnly = showprice ? true : false;
                DBGridMaterial.Columns.Add(col);
                col.Dispose();

                string[] showcell = { "WarehouseId" };
                DBGridMaterial.SetParam(showcell);
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

        private void DBGridMaterial_ButtonSelectClick()
        {
            MTN_WAREHOUSE_LIST frmct = new MTN_WAREHOUSE_LIST();
            frmct.SelectVisible = true;
            if (frmct.ShowDialog() == DialogResult.OK)
            {
                DBGridMaterial.CurrentRow.Cells["WarehouseId"].Value = Util.retValue1;
            }
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            txtFormId.Text = _dal.GetNewFormId(dtpFormDate.Value);
            dtpFormDate.Value = DateTime.Today;
            txtKillMny.Text = "0";
            txtOtherMny.Text = "0";

            FillControl.FillCurrency(cboCurrency, false, false);
            FillControl.FillProductionType(cboFormType, false);
            cboFormType.SelectedIndex = -1;
            LoadDBGridWare(false);
            LoadDBGridMaterial(false);

            toolUpdatePrice.Visible = false;
            DBGridWare.ContextMenuStrip.Items.Add("-");
            mnuNewWare = DBGridWare.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddWare_Click));
            mnuDeleteWare = DBGridWare.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDeleteWare_Click));
            mnuNewMaterial = DBGridMaterial.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddMaterial_Click));
            mnuDeleteMaterial = DBGridMaterial.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDeleteMaterial_Click));

            status4.Image = null;
        }

        public void EditItem(string formid, bool showprice)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                FillControl.FillCurrency(cboCurrency, false, false);
                FillControl.FillProductionType(cboFormType, false);

                modProductionForm mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;
                    dtpRequireDate.Value = mod.RequireDate;
                    cboFormType.SelectedValue = mod.FormType;
                    cboFormType_SelectedIndexChanged(null, null);
                    cboCurrency.SelectedValue = mod.Currency;
                    txtNo.Text = mod.No;
                    txtKillMny.Text = mod.KillMny.ToString();
                    txtOtherMny.Text = mod.OtherMny.ToString();
                    txtOtherReason.Text = mod.OtherReason;
                    cboDept.Text = mod.DeptId;
                    txtShipMan.Text = mod.ShipMan;
                    txtRemark.Text = mod.Remark;                    
                    if (mod.Status == 1)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);
                        DBGridWare.ReadOnly = true;
                        DBGridMaterial.ReadOnly = true;

                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        toolSave.Visible = true;
                        Util.ChangeStatus(this, false);
                        txtFormId.ReadOnly = true;
                        DBGridWare.ReadOnly = false;
                        DBGridMaterial.ReadOnly = false;

                        toolSave.Enabled = true;
                    }

                    DBGridWare.Rows.Clear();
                    DBGridMaterial.Rows.Clear();

                    LoadDBGridWare(showprice);
                    LoadDBGridMaterial(showprice);
                    if (showprice)
                    {
                        toolUpdatePrice.Visible = true;
                        DBGridWare.ContextMenuStrip.Items.Add("-");
                        DBGridMaterial.ContextMenuStrip.Items.Add("-");
                        DBGridWare.ContextMenuStrip.Items.Add("获取成本价", LXMS.Properties.Resources.mnuPropertyChange, new System.EventHandler(this.mnuGetWarePrice_Click));
                        DBGridMaterial.ContextMenuStrip.Items.Add("获取成本价", LXMS.Properties.Resources.mnuPropertyChange, new System.EventHandler(this.mnuGetMaterialPrice_Click));
                    }
                    else
                    {
                        toolUpdatePrice.Visible = false;
                        if (mod.Status == 0)
                        {
                            DBGridWare.ContextMenuStrip.Items.Add("-");
                            DBGridMaterial.ContextMenuStrip.Items.Add("-");
                            mnuNewWare = DBGridWare.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddWare_Click));
                            mnuDeleteWare = DBGridWare.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDeleteWare_Click));
                            mnuNewMaterial = DBGridMaterial.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAddMaterial_Click));
                            mnuDeleteMaterial = DBGridMaterial.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDeleteMaterial_Click));
                        }
                    }
                    if (mod.AccSeq > 0)
                        toolUpdatePrice.Enabled = false;
                    else
                        toolUpdatePrice.Enabled = true;

                    BindingCollection<modProductionFormWare> listware = _dal.GetProductionFormWare(formid, out Util.emsg);
                    if (listware == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        foreach (modProductionFormWare modd in listware)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGridWare);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Specify;
                            row.Cells[3].Value = modd.Size;
                            row.Cells[4].Value = modd.Qty.ToString();
                            row.Cells[5].Value = modd.ProcessPrice.ToString();
                            row.Cells[6].Value = modd.CostPrice.ToString();
                            row.Cells[7].Value = modd.ProcessAmount.ToString();
                            row.Cells[8].Value = modd.CostAmount.ToString();
                            row.Cells[9].Value = modd.WarehouseId;
                            row.Cells[10].Value = modd.Remark;
                            row.Height = 40;
                            DBGridWare.Rows.Add(row);
                            row.Dispose();
                        }
                        GetWareDetailSum();
                        GetProcessMny();
                    }

                    BindingCollection<modProductionFormMaterial> listmaterial = _dal.GetProductionFormMaterial(formid, out Util.emsg);
                    if (listmaterial == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        foreach (modProductionFormMaterial modd in listmaterial)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGridMaterial);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Specify;
                            row.Cells[3].Value = modd.Size;
                            row.Cells[4].Value = modd.Qty.ToString();
                            row.Cells[5].Value = modd.CostPrice.ToString();
                            row.Cells[6].Value = modd.CostAmount.ToString();
                            row.Cells[7].Value = modd.WarehouseId;
                            row.Cells[8].Value = modd.Remark;
                            row.Height = 40;
                            DBGridMaterial.Rows.Add(row);
                            row.Dispose();
                        }
                        GetMaterialDetailSum();
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

        private void DBGridWare_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6)
            {
                GetWareDetailSum();
                GetProcessMny();
            }
        }

        private void DBGridMaterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6)
                GetMaterialDetailSum();
        }

        private decimal GetProcessMny()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                if (DBGridWare.RowCount > 0)
                {
                    for (int i = 0; i < DBGridWare.RowCount; i++)
                    {
                        decimal processamount = Convert.ToDecimal(DBGridWare.Rows[i].Cells[4].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells[5].Value);                        
                        DBGridWare.Rows[i].Cells[7].Value = processamount;                        
                        summny += processamount;                        
                    }
                }
                Status3.Text = "加工费用合计： " + string.Format("{0:C2}", summny);
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

        private decimal GetWareDetailSum()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                if (DBGridWare.RowCount > 0)
                {
                    for (int i = 0; i < DBGridWare.RowCount; i++)
                    {
                        decimal costamount = Convert.ToDecimal(DBGridWare.Rows[i].Cells[3].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells[4].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells[6].Value);
                        DBGridWare.Rows[i].Cells[8].Value = costamount;
                        summny += costamount;
                    }
                }
                if (_showprice)
                    Status2.Text = "入库产品合计： " + string.Format("{0:C2}", summny);
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

        private decimal GetMaterialDetailSum()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                if (DBGridMaterial.RowCount > 0)
                {
                    for (int i = 0; i < DBGridMaterial.RowCount; i++)
                    {
                        decimal amount = Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[3].Value) * Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[4].Value) * Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[5].Value);
                        DBGridMaterial.Rows[i].Cells[6].Value = amount;
                        summny += amount;                        
                    }
                }
                if (_showprice)
                    Status1.Text = "出库材料成本合计： " + string.Format("{0:C2}", summny);
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
                DBGridMaterial.EndEdit();
                DBGridWare.EndEdit();
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
                    return;
                }
                if (cboFormType.SelectedValue == null || string.IsNullOrEmpty(cboFormType.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Form Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboFormType.Focus();
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
                if (string.IsNullOrEmpty(cboDept.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Dept Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboFormType.Focus();
                    return;
                }
                if (clsLxms.GetParameterValue("NEED_PRODUCTION_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return;
                }                
                if (DBGridWare.RowCount == 0)
                {
                    MessageBox.Show("没有入库成品数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (DBGridMaterial.RowCount == 0)
                {
                    MessageBox.Show("没有出库材料数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < DBGridWare.RowCount; i++)
                {
                    if (DBGridWare.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[0].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGridWare.Rows[i].Cells[1].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[1].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGridWare.Rows[i].Cells[3].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[3].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridWare.Rows[i].Cells[3].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGridWare.Rows[i].Cells[3].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGridWare.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridWare.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGridWare.Rows[i].Cells[4].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGridWare.Rows[i].Cells[5].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Process Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridWare.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Process Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGridWare.Rows[i].Cells[6].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridWare.Rows[i].Cells[6].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //else if (Convert.ToDecimal(DBGridWare.Rows[i].Cells[6].Value.ToString()) <= 0)
                    //{
                    //    MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    if (DBGridWare.Rows[i].Cells[9].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells[9].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Warehouse Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                for (int i = 0; i < DBGridMaterial.RowCount; i++)
                {
                    if (DBGridMaterial.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[0].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGridMaterial.Rows[i].Cells[1].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[1].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGridMaterial.Rows[i].Cells[3].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[3].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridMaterial.Rows[i].Cells[3].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[3].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Size") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGridMaterial.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridMaterial.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[4].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGridMaterial.Rows[i].Cells[5].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridMaterial.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //else if (Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[5].Value.ToString()) <= 0)
                    //{
                    //    MessageBox.Show(clsTranslate.TranslateString("Cost Price") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}

                    if (DBGridMaterial.Rows[i].Cells[7].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells[7].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Warehouse Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                modProductionForm mod = new modProductionForm();                
                mod.FormId = txtFormId.Text.Trim();
                mod.FormDate = dtpFormDate.Value;
                mod.RequireDate = dtpRequireDate.Value;
                mod.FormType = cboFormType.SelectedValue.ToString();
                mod.DeptId = cboDept.Text.Trim();
                mod.No = txtNo.Text.Trim();
                mod.ShipMan = txtShipMan.Text.Trim();
                mod.OtherReason = txtOtherReason.Text.Trim();
                mod.OtherMny = Convert.ToDecimal(txtOtherMny.Text);
                mod.KillMny = Convert.ToDecimal(txtKillMny.Text);
                mod.Currency = cboCurrency.SelectedValue.ToString();
                mod.ExchangeRate = ((modAccCurrencyList)cboCurrency.SelectedItem).ExchangeRate;
                mod.MaterialMny = GetMaterialDetailSum();
                mod.ProcessMny = GetProcessMny();
                if (mod.ProcessMny != 0 && mod.FormType == "内部单")
                {
                    MessageBox.Show("内部单不允许有加工费用！" + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;

                BindingCollection<modProductionFormWare> listware = new BindingCollection<modProductionFormWare>();
                for (int i = 0; i < DBGridWare.RowCount; i++)
                {
                    modProductionFormWare modd = new modProductionFormWare();
                    modd.FormId = txtFormId.Text.Trim();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGridWare.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGridWare.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGridWare.Rows[i].Cells[2].Value == null ? string.Empty : DBGridWare.Rows[i].Cells[2].Value.ToString();
                    if (clsLxms.GetProductSizeFlag(modd.ProductId) == 0)
                        modd.Size = 1;
                    else
                        modd.Size = Convert.ToDecimal(DBGridWare.Rows[i].Cells[3].Value.ToString());
                    modd.Qty = Convert.ToDecimal(DBGridWare.Rows[i].Cells[4].Value.ToString());
                    modd.ProcessPrice = Convert.ToDecimal(DBGridWare.Rows[i].Cells[5].Value.ToString());
                    modd.CostPrice = Convert.ToDecimal(DBGridWare.Rows[i].Cells[6].Value.ToString());
                    modd.WarehouseId = DBGridWare.Rows[i].Cells[9].Value.ToString();
                    modd.Remark = DBGridWare.Rows[i].Cells[10].Value == null ? string.Empty : DBGridWare.Rows[i].Cells[10].Value.ToString();
                    listware.Add(modd);
                }
                BindingCollection<modProductionFormMaterial> listmaterial = new BindingCollection<modProductionFormMaterial>();
                for (int i = 0; i < DBGridMaterial.RowCount; i++)
                {
                    modProductionFormMaterial modd = new modProductionFormMaterial();
                    modd.FormId = txtFormId.Text.Trim();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGridMaterial.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGridMaterial.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGridMaterial.Rows[i].Cells[2].Value == null ? string.Empty : DBGridMaterial.Rows[i].Cells[2].Value.ToString();
                    if (clsLxms.GetProductSizeFlag(modd.ProductId) == 0)
                        modd.Size = 1;
                    else
                        modd.Size = Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[3].Value.ToString());
                    modd.Qty = Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[4].Value.ToString());
                    modd.CostPrice = Convert.ToDecimal(DBGridMaterial.Rows[i].Cells[5].Value.ToString());
                    modd.WarehouseId = DBGridMaterial.Rows[i].Cells[7].Value.ToString();
                    modd.Remark = DBGridMaterial.Rows[i].Cells[8].Value == null ? string.Empty : DBGridMaterial.Rows[i].Cells[8].Value.ToString();
                    listmaterial.Add(modd);
                }

                bool ret = _dal.Save(_action, mod, listware, listmaterial, out Util.emsg);
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

        private void mnuAddWare_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SearchProduct frm = new SearchProduct();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalProductList dal = new dalProductList();
                    modProductList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGridWare);
                        row.Cells[0].Value = mod.ProductId;
                        row.Cells[1].Value = mod.ProductName;
                        row.Cells[2].Value = mod.Specify;
                        if (mod.SizeFlag == 0)
                        {
                            row.Cells[3].Value = 1;
                            row.Cells[3].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[3].Value = 0;
                            row.Cells[3].ReadOnly = false;
                        }
                        row.Cells[4].Value = 0;
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = clsLxms.GetPrice(mod.ProductId);
                        row.Cells[7].Value = 0;
                        row.Cells[8].Value = 0;
                        row.Cells[9].Value = clsLxms.GetDefaultWarehouseId();
                        row.Cells[10].Value = "";
                        row.Height = 40;
                        DBGridWare.Rows.Add(row);
                        row.Dispose();
                    }
                    GetMaterialDetailSum();
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

        private void mnuAddMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SearchProduct frm = new SearchProduct();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dalProductList dal = new dalProductList();
                    modProductList mod = dal.GetItem(Util.retValue1, out Util.emsg);
                    if (mod != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGridMaterial);
                        row.Cells[0].Value = mod.ProductId;
                        row.Cells[1].Value = mod.ProductName;
                        row.Cells[2].Value = mod.Specify;
                        if (mod.SizeFlag == 0)
                        {
                            row.Cells[3].Value = 1;
                            row.Cells[3].ReadOnly = true;
                        }
                        else
                        {
                            row.Cells[3].Value = 0;
                            row.Cells[3].ReadOnly = false;
                        }
                        row.Cells[4].Value = 0;
                        row.Cells[5].Value = clsLxms.GetPrice(mod.ProductId);
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = clsLxms.GetDefaultWarehouseId();
                        row.Cells[8].Value = "";
                        row.Height = 40;
                        DBGridMaterial.Rows.Add(row);
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

        private void mnuDeleteWare_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (DBGridWare.CurrentRow == null) return;

                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                DBGridWare.Rows.RemoveAt(DBGridWare.CurrentRow.Index);
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

        private void mnuDeleteMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (DBGridMaterial.CurrentRow == null) return;

                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                DBGridMaterial.Rows.RemoveAt(DBGridMaterial.CurrentRow.Index);
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

        private void cboFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDept.DataSource = null;
            if (cboFormType.SelectedIndex == 0)
            {
                FillControl.FillAdminDeptList(cboDept, false, false);
                lblDeptId.Text = clsTranslate.TranslateString("Dept Id");
                txtKillMny.Enabled = false;
                txtOtherMny.Enabled = false;
                txtOtherReason.Enabled = false;
            }
            else if (cboFormType.SelectedIndex == 1)
            {
                FillControl.FillOtherPayableObject(cboDept, false);
                lblDeptId.Text = clsTranslate.TranslateString("Object Name");
                txtKillMny.Enabled = true;
                txtOtherMny.Enabled = true;
                txtOtherReason.Enabled = true;
            }
        }

        private void toolUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGridMaterial.EndEdit();
                DBGridWare.EndEdit();
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
                    return;
                }                
                if (DBGridWare.RowCount == 0)
                {
                    MessageBox.Show("没有入库数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (DBGridWare.RowCount == 0)
                {
                    MessageBox.Show("没有出库数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < DBGridWare.RowCount; i++)
                {
                    if (DBGridWare.Rows[i].Cells["CostPrice"].Value == null || string.IsNullOrEmpty(DBGridWare.Rows[i].Cells["CostPrice"].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("CostPrice") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridWare.Rows[i].Cells["CostPrice"].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("CostPrice") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Ware"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                for (int i = 0; i < DBGridMaterial.RowCount; i++)
                {
                    if (DBGridMaterial.Rows[i].Cells["CostPrice"].Value == null || string.IsNullOrEmpty(DBGridMaterial.Rows[i].Cells["CostPrice"].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("CostPrice") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGridMaterial.Rows[i].Cells["CostPrice"].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("CostPrice") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                decimal warecost = GetWareDetailSum();
                decimal materialcost = GetMaterialDetailSum();
                if (Math.Abs(warecost - materialcost) > Convert.ToDecimal("0.001"))
                {
                    MessageBox.Show("出库和入库成本不平衡,不能保存\r\n成品：[" + warecost.ToString() + "]\r\n原材料：[" + materialcost.ToString() + "]", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                modProductionForm mod = _dal.GetItem(txtFormId.Text.Trim(), out Util.emsg);
                if (mod.AccSeq > 0)
                {
                    MessageBox.Show("该单据已做凭证，不可修改单价！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BindingCollection<modProductionFormWare> listware = new BindingCollection<modProductionFormWare>();
                for (int i = 0; i < DBGridWare.RowCount; i++)
                {
                    modProductionFormWare modd = new modProductionFormWare();
                    modd.FormId = txtFormId.Text.Trim();
                    modd.Seq = i + 1;
                    modd.CostPrice = Convert.ToDecimal(DBGridWare.Rows[i].Cells["CostPrice"].Value.ToString());                    
                    listware.Add(modd);
                }
                BindingCollection<modProductionFormMaterial> listmaterial = new BindingCollection<modProductionFormMaterial>();
                for (int i = 0; i < DBGridMaterial.RowCount; i++)
                {
                    modProductionFormMaterial modd = new modProductionFormMaterial();
                    modd.FormId = txtFormId.Text.Trim();
                    modd.Seq = i + 1;
                    modd.CostPrice = Convert.ToDecimal(DBGridMaterial.Rows[i].Cells["CostPrice"].Value.ToString());                    
                    listmaterial.Add(modd);
                }

                decimal processmny = GetProcessMny();
                if (processmny != 0 && mod.FormType == "内部单")
                {
                    MessageBox.Show("内部单不允许有加工费用！" + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Material"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                bool ret = _dal.UpdatePrice(listware, listmaterial, GetMaterialDetailSum(), GetProcessMny(), Util.UserId, out Util.emsg);
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

        private void mnuGetMaterialPrice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGridMaterial.CurrentRow == null) return;
                if (toolUpdatePrice.Visible && toolUpdatePrice.Enabled)
                {
                    DBGridMaterial.CurrentRow.Cells["CostPrice"].Value = clsLxms.GetPrice(DBGridMaterial.CurrentRow.Cells["ProductId"].Value.ToString());
                    GetMaterialDetailSum();
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

        private void mnuGetWarePrice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGridWare.CurrentRow == null) return;

                if (toolUpdatePrice.Visible && toolUpdatePrice.Enabled)
                {                    
                    decimal sizeqty = 0;
                    decimal warecost = 0;
                    for (int i = 0; i < DBGridWare.RowCount; i++)
                    {
                        if (i != DBGridWare.CurrentRow.Index)
                        {
                            if (Convert.ToDecimal(DBGridWare.Rows[i].Cells["CostPrice"].Value) <= 0)
                            {
                                for (int k = 0; k < DBGridMaterial.RowCount; k++)
                                {
                                    if (DBGridMaterial.Rows[k].Cells["ProductId"].Value.ToString().CompareTo(DBGridWare.CurrentRow.Cells["ProductId"].Value.ToString()) == 0)
                                    {
                                        if (Convert.ToDecimal(DBGridMaterial.Rows[k].Cells["CostPrice"].Value) > 0)
                                        {
                                            DBGridWare.CurrentRow.Cells["CostPrice"].Value = Convert.ToDecimal(DBGridMaterial.Rows[k].Cells["CostPrice"].Value);
                                            return;
                                        }
                                        break;
                                    }
                                }                                
                                DBGridWare.CurrentRow.Cells["CostPrice"].Value = clsLxms.GetPrice(DBGridWare.CurrentRow.Cells["ProductId"].Value.ToString());
                                return;
                            }
                            else
                            {
                                warecost += Convert.ToDecimal(DBGridWare.Rows[i].Cells["Size"].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells["Qty"].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells["CostPrice"].Value);
                            }
                        }
                        else
                            sizeqty = Convert.ToDecimal(DBGridWare.Rows[i].Cells["Size"].Value) * Convert.ToDecimal(DBGridWare.Rows[i].Cells["Qty"].Value);
                    }
                    decimal materialcost = GetMaterialDetailSum();
                    for (int i = 0; i < DBGridMaterial.RowCount; i++)
                    {
                        if (Convert.ToDecimal(DBGridMaterial.Rows[i].Cells["CostPrice"].Value) <= 0)
                        {
                            MessageBox.Show("请先设定出库材料的价格！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    if (materialcost - warecost <= 0)
                    {
                        MessageBox.Show("单价有误，出库成本比入库的成本小！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    decimal price = 0;
                    if (sizeqty > 0)
                        price = Math.Round((materialcost - warecost) / sizeqty, 8);
                    DBGridWare.CurrentRow.Cells["CostPrice"].Value = price;
                    GetWareDetailSum();                        
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

        private void DBGridWare_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mnuGetWarePrice_Click(null, null);
        }

        private void DBGridMaterial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mnuGetMaterialPrice_Click(null, null);
        }

        private void btnShipMan_Click(object sender, EventArgs e)
        {
            clsLxms.SetEmployeeName(txtShipMan);
        }

        private void txtShipMan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                clsLxms.SetEmployeeName(txtShipMan);
            }
        }

        private void dtpFormDate_ValueChanged(object sender, EventArgs e)
        {
            if (_action == "NEW")
            {
                txtFormId.Text = _dal.GetNewFormId(dtpFormDate.Value);
            }
        }
    }
}