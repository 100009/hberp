using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS
{
    public partial class EditPriceAdjust : Form
    {
        string _action;
        dalPriceAdjustForm _dal = new dalPriceAdjustForm();
        public EditPriceAdjust()
        {
            InitializeComponent();
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Inout Detail"), LXMS.Properties.Resources.Bookmark, new System.EventHandler(this.mnuDetail_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuNew_Click));
            mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
        }

        private void EditPriceAdjust_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            LoadDBGrid2();
        }

        private void LoadDBGrid2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccProductInout dal = new dalAccProductInout();
                DBGrid2.Columns.Clear();
                BindingCollection<modAccProductSummary> list = dal.GetAccProductSummary(Util.modperiod.AccName, false, out Util.emsg);
                DBGrid2.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    decimal totalendmny = 0;
                    foreach (modAccProductSummary mod in list)
                    {
                        totalendmny += Convert.ToDecimal(mod.EndMny);
                    }
                    Status1.Text = "结存金额:  " + string.Format("{0:C2}", totalendmny);
                }

                DBGrid2.Columns["AccName"].Visible = false;
                DBGrid2.Columns["ProductId"].Visible = false;
                DBGrid2.Columns["StartQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["StartMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["InputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["InputMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["OutputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["OutputMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["EndQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["EndMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns[DBGrid.ColumnCount - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                col.Visible = false;
                col.ReadOnly = true;
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
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 60;
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Current Price");
                col.DataPropertyName = "Current Price";
                col.Name = "Current Price";
                col.Width = 90;                
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("True Price");
                col.DataPropertyName = "True Price";
                col.Name = "True Price";
                col.Width = 80;
                col.ReadOnly = false;
                col.DefaultCellStyle.ForeColor = Color.Red;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Current Mny");
                col.DataPropertyName = "Current Mny";
                col.Name = "Current Mny";
                col.Width = 80;
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("True Mny");
                col.DataPropertyName = "True Mny";
                col.Name = "True Mny";
                col.Width = 90;
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Differ");
                col.DataPropertyName = "Differ";
                col.Name = "Differ";
                col.Width = 90;
                col.ReadOnly = true;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        public void AddItem(string acctype)
        {
            _action = "NEW";
            //dtpFormDate.Value = DateTime.Today;
            dtpFormDate.Value = Util.modperiod.EndDate <= DateTime.Today ? Util.modperiod.EndDate : DateTime.Today;
            txtFormId.Text = _dal.GetNewId(dtpFormDate.Value).ToString();

            mnuNew.Enabled = true;
            mnuDelete.Enabled = true;
            LoadDBGrid();
            DBGrid2_DoubleClick(null, null);
            status4.Image = null;
        }

        public void EditItem(string formid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                modPriceAdjustForm mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;                    
                    txtRemark.Text = mod.Remark;                    
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
                    DBGrid.Rows.Clear();
                    LoadDBGrid();
                    DBGrid2_DoubleClick(null, null);
                    BindingCollection<modPriceAdjustDetail> list = _dal.GetDetail(formid, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        foreach (modPriceAdjustDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Qty.ToString();
                            row.Cells[3].Value = modd.CurrentPrice;
                            row.Cells[4].Value = modd.TruePrice;
                            row.Cells[5].Value = modd.CurrentMny;
                            row.Cells[6].Value = modd.TrueMny;
                            row.Cells[7].Value = modd.Differ;
                            row.Cells[8].Value = modd.Remark;                            
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
                        summny += Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);

                    }
                }
                Status2.Text = "差额合计： " + string.Format("{0:C2}", summny);
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
            if (e.ColumnIndex == 4)
            {
                DBGrid.CurrentRow.Cells[6].Value = Convert.ToDecimal(DBGrid.CurrentRow.Cells[2].Value) * Convert.ToDecimal(DBGrid.CurrentRow.Cells[4].Value);
                DBGrid.CurrentRow.Cells[7].Value = Convert.ToDecimal(DBGrid.CurrentRow.Cells[6].Value) - Convert.ToDecimal(DBGrid.CurrentRow.Cells[5].Value);
                GetDetailSum();
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
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
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
                        MessageBox.Show(clsTranslate.TranslateString("Current Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Current Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                modPriceAdjustForm mod = new modPriceAdjustForm();
                mod.FormId = txtFormId.Text.Trim();
                mod.FormDate = dtpFormDate.Value;                
                mod.Remark = txtRemark.Text.Trim();                
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;                
                string detaillist = string.Empty;
                BindingCollection<modPriceAdjustDetail> list = new BindingCollection<modPriceAdjustDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modPriceAdjustDetail modd = new modPriceAdjustDetail();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGrid.Rows[i].Cells[1].Value.ToString();                    
                    modd.Qty = Convert.ToDecimal(DBGrid.Rows[i].Cells[2].Value.ToString());
                    modd.CurrentPrice = Convert.ToDecimal(DBGrid.Rows[i].Cells[3].Value.ToString());
                    modd.TruePrice = Convert.ToDecimal(DBGrid.Rows[i].Cells[4].Value.ToString());
                    modd.CurrentMny = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.TrueMny = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());
                    modd.Differ = Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value.ToString());
                    modd.Remark = DBGrid.Rows[i].Cells[8].Value == null ? string.Empty : DBGrid.Rows[i].Cells[8].Value.ToString();
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

        private void mnuDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                dalAccProductInout dal = new dalAccProductInout();
                BindingCollection<modAccProductInout> list = dal.GetIList(Util.modperiod.AccName, DBGrid.CurrentRow.Cells["productid"].Value.ToString(), Util.IsTrialBalance, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(list[0].ProductName, list);
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

        private void mnuNew_Click(object sender, EventArgs e)
        {
            if (mnuNew.Text == clsTranslate.TranslateString("New"))
            {
                mnuNew.Text = clsTranslate.TranslateString("Hide");
                DBGrid2.Visible = true;
            }
            else
            {
                mnuNew.Text = clsTranslate.TranslateString("New");
                DBGrid2.Visible = false;
            }
        }

        private void DBGrid2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null) return;

                modAccProductSummary mod = (modAccProductSummary)DBGrid2.CurrentRow.DataBoundItem;
                if (DBGrid.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().Trim() == mod.ProductId)
                        {
                            MessageBox.Show("该产品已经添加，您不能重复添加！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DBGrid2.CurrentRow.Cells[10].Value == null || string.IsNullOrEmpty(DBGrid2.CurrentRow.Cells[10].Value.ToString()) || decimal.Parse(DBGrid2.CurrentRow.Cells[10].Value.ToString())==0)
                        {
                            MessageBox.Show("该产品数量为0，您不能调整它的价格！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = mod.ProductId;
                row.Cells[1].Value = mod.ProductName;
                row.Cells[2].Value = mod.EndQty;
                row.Cells[3].Value = mod.EndPrice;
                row.Cells[4].Value = mod.EndPrice;
                row.Cells[5].Value = mod.EndMny;
                row.Cells[6].Value = mod.EndMny;
                row.Cells[7].Value = 0;
                row.Cells[8].Value = "";
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
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
    }
}
