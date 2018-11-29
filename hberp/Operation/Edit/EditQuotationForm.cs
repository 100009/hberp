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
    public partial class EditQuotationForm : Form
    {
        string _action;
        INIClass ini = new INIClass(Util.INI_FILE);
        dalQuotationForm _dal = new dalQuotationForm();
        public EditQuotationForm()
        {
            InitializeComponent();
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("History Detail"), LXMS.Properties.Resources.Browser, new System.EventHandler(this.mnuHistoryDetail_Click));
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
        }

        private void EditQuotationForm_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtFormId.ReadOnly = true;
            if (clsLxms.GetParameterValue("NEED_QUOTATION_NO").CompareTo("T") == 0)
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
                col.HeaderText = clsTranslate.TranslateString("Brand");
                col.DataPropertyName = "Brand";
                col.Name = "Brand";
                col.Width = 60;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
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

        public void AddItem()
        {
            _action = "NEW";
            dtpFormDate.Value = DateTime.Today;
            txtFormId.Text = _dal.GetNewId(DateTime.Today);
            txtFormId.ReadOnly = true;            
            txtContactPerson.Text = ini.IniReadValue("QUOTATION", "CONTACT_PERSON");
            if(string.IsNullOrEmpty(txtContactPerson.Text.Trim()))
                txtContactPerson.Text = Util.UserName;
            txtCustName.ReadOnly = true;
            LoadDBGrid();
        }

        public void EditItem(string formid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";                
                modQuotationForm mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;
                    txtNo.Text = mod.No;
                    txtCurrency.Text = mod.Currency;
                    txtCustName.Tag = mod.CustId;
                    txtCustName.Text = mod.CustName;                    
                    txtRemark.Text = mod.Remark;
                    txtContactPerson.Text = mod.ContactPerson;
                    txtFormId.ReadOnly = true;
                    txtCustName.ReadOnly = true;
                    DBGrid.Rows.Clear();
                    LoadDBGrid();
                    BindingCollection<modQuotationDetail> list = _dal.GetDetail(formid, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        dalProductList dalpdt = new dalProductList();
                        foreach (modQuotationDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.ProductId;
                            row.Cells[1].Value = modd.ProductName;
                            row.Cells[2].Value = modd.Specify;
                            row.Cells[3].Value = modd.UnitNo;
                            row.Cells[4].Value = modd.Brand;
                            row.Cells[5].Value = modd.Qty.ToString();
                            row.Cells[6].Value = modd.Price.ToString();
                            row.Cells[7].Value = modd.Remark;
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
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
                if (clsLxms.GetParameterValue("NEED_QUOTATION_NO").CompareTo("T") == 0 && string.IsNullOrEmpty(txtNo.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
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
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                modQuotationForm mod = new modQuotationForm();
                mod.FormId = txtFormId.Text.Trim();
                mod.FormDate = dtpFormDate.Value;
                mod.No = txtNo.Text.Trim();
                mod.CustId = txtCustName.Tag.ToString();
                mod.CustName = txtCustName.Text.Trim();                
                mod.Remark = txtRemark.Text.Trim();
                mod.ContactPerson = txtContactPerson.Text.Trim();
                mod.Currency = txtCurrency.Text.Trim();                
                mod.UpdateUser = Util.UserId;               
                string detaillist = string.Empty;
                BindingCollection<modQuotationDetail> list = new BindingCollection<modQuotationDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modQuotationDetail modd = new modQuotationDetail();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGrid.Rows[i].Cells[2].Value == null ? string.Empty : DBGrid.Rows[i].Cells[2].Value.ToString();
                    modd.UnitNo = DBGrid.Rows[i].Cells[3].Value == null ? string.Empty : DBGrid.Rows[i].Cells[3].Value.ToString();
                    modd.Brand = DBGrid.Rows[i].Cells[4].Value == null ? string.Empty : DBGrid.Rows[i].Cells[4].Value.ToString();
                    modd.Qty = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.Price = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());                    
                    modd.Remark = DBGrid.Rows[i].Cells[7].Value == null ? string.Empty : DBGrid.Rows[i].Cells[7].Value.ToString();
                    list.Add(modd);
                }
                bool ret = _dal.Save(_action, mod, list, out Util.emsg);
                if (ret)
                {
                    ini.IniWriteValue("QUOTATION", "CONTACT_PERSON", txtContactPerson.Text.Trim());
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
                        row.Cells[4].Value = mod.Brand;
                        row.Cells[5].Value = 1;
                        row.Cells[6].Value = 0; 
                        row.Cells[7].Value = "";
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
                        txtCurrency.Text = mod.Currency;                        
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

        private void dtpFormDate_ValueChanged(object sender, EventArgs e)
        {
            if (_action == "NEW")
            {
                txtFormId.Text = _dal.GetNewId(dtpFormDate.Value);                
            }
        }

        private void mnuHistoryDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;

                if (DBGrid.CurrentRow.Cells[0].Value == null || string.IsNullOrEmpty(DBGrid.CurrentRow.Cells[0].Value.ToString())) return;

                BindingCollection<modVQuotationDetail> list = _dal.GetVDetail(string.Empty, string.Empty, DBGrid.CurrentRow.Cells[0].Value.ToString(), string.Empty, DateTime.Today.AddDays(-365).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
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
