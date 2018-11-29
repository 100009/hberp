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
    public partial class OPA_SALES_DESIGN_FORM : Form
    {
        dalSalesDesignForm _dal = new dalSalesDesignForm();
        public OPA_SALES_DESIGN_FORM()
        {
            InitializeComponent();
        }

        private void OPA_SALES_DESIGN_FORM_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Receive Status"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuReceiveStatus_Click));
            clsTranslate.InitLanguage(this);
            FillControl.FillCustomerList(cboCustomer, true);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
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
            rbStatus0_CheckedChanged(null, null);
        }

        private void OPA_SALES_DESIGN_FORM_Shown(object sender, EventArgs e)
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
                if(DBGrid.Rows[i].Cells["FormType"].Value.ToString().IndexOf("退货")>0)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
            DBGrid.Columns["ReceiveStatus"].DefaultCellStyle.ForeColor = Color.Red;
            DBGrid.Columns["ReceiveDate"].DefaultCellStyle.ForeColor = Color.Red;
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
                string custlist = string.Empty;
                if (cboCustomer.Visible)
                    custlist = cboCustomer.SelectedValue.ToString();
                else
                    custlist = string.Empty;
                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modSalesDesignForm> list = _dal.GetIList(rbStatus0.Checked ? "0" : "1", string.Empty, custlist, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, rbStatus0.Checked ? string.Empty : dtpFrom.Text, rbStatus0.Checked ? string.Empty : dtpTo.Text, out Util.emsg);
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
                    DBGrid.Columns["ReceiveStatus"].ReadOnly = false;
                    DBGrid.Columns["ReceiveDate"].ReadOnly = false;
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
            DBGrid.Columns.RemoveAt(1);
            DBGrid.Columns.Insert(1, checkboxColumn);
            checkboxColumn.Dispose();

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("ReceiveStatus");
            checkboxColumn.DataPropertyName = "ReceiveStatus";
            checkboxColumn.Name = "ReceiveStatus";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(18);
            DBGrid.Columns.Insert(18, checkboxColumn);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditDesignForm frm = new EditDesignForm();
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
                modSalesDesignForm mod = (modSalesDesignForm)DBGrid.CurrentRow.DataBoundItem;
                EditDesignForm frm = new EditDesignForm();
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

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                modSalesDesignForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
                if (mod.Status == 1)
                {
                    MessageBox.Show("该单据已审核，您不能删除！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool ret = _dal.Delete(mod.Id, out Util.emsg);
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
                    modSalesDesignForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
                    if (_dal.Audit(mod.Id, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.Id.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modSalesDesignForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.SelectedRows[i].Cells[0].Value), out Util.emsg);
                        if (_dal.Audit(mod.Id, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 1;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.Id.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    modSalesDesignForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
                    if (mod.AccSeq > 0)
                    {
                        MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_dal.Reset(mod.Id, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.Id.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modSalesDesignForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.SelectedRows[i].Cells[0].Value), out Util.emsg);
                        if (mod.AccSeq > 0)
                        {
                            MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_dal.Reset(mod.Id, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 0;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Black;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.Id.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                lblCustomer.Visible = false;
                cboCustomer.Visible = false;
            }
            else
            {
                groupBox1.Visible = true;
                toolAudit.Enabled = false;
                toolReset.Enabled = true;
                toolDel.Enabled = false;
                lblCustomer.Visible = true;
                cboCustomer.Visible = true;
            }
            LoadData();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            
        }

        private void mnuReceiveStatus_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;

                string shipidlist = string.Empty;
                if (DBGrid.SelectedRows.Count == 0)
                    shipidlist = DBGrid.CurrentRow.Cells["Id"].Value.ToString();
                else
                {
                    string custid = DBGrid.SelectedRows[0].Cells["CustId"].Value.ToString();
                    for (int i = 0; i < DBGrid.SelectedRows.Count; i++)
                    {
                        if (custid.CompareTo(DBGrid.SelectedRows[i].Cells["CustId"].Value.ToString()) != 0)
                        {
                            MessageBox.Show("您所选择的单据必须属于同一个客户!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (i == 0)
                            shipidlist = DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                        else
                            shipidlist += "," + DBGrid.SelectedRows[i].Cells["Id"].Value.ToString();
                    }
                }
                EditPayStatus frm = new EditPayStatus();
                frm.InitData("SALE", shipidlist);
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
                modSalesDesignForm mod = (modSalesDesignForm)DBGrid.CurrentRow.DataBoundItem;
                switch (DBGrid.Columns[e.ColumnIndex].Name)
                {
                    case "ReceiveStatus":
                    case "ReceiveDate":
                    case "AccountNo":
                        _dal.UpdateReceiveStatus(mod.Id, mod.ReceiveStatus, mod.ReceiveDate, mod.AccountNo, Util.UserId, out Util.emsg);
                        if (DBGrid.Columns[e.ColumnIndex].Name == "ReceiveStatus" && mod.ReceiveStatus == 1)
                            DBGrid.CurrentRow.Cells["ReceiveDate"].Value = DateTime.Today.ToString("yyyy-MM-dd");
                        break;
                    case "InvoiceStatus":
                    case "InvoiceNo":
                    case "InvoiceMny":
                        _dal.UpdateInvoiceStatus(mod.Id, mod.InvoiceStatus, mod.InvoiceMny, mod.InvoiceNo, Util.UserId, out Util.emsg);
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

        private void DBGrid_ButtonSelectClick()
        {
            ACC_BANK_ACCOUNT frm = new ACC_BANK_ACCOUNT();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DBGrid.CurrentCell.Value = Util.retValue1;
            }
        }

        private void toolImport_Click(object sender, EventArgs e)
        {
            try
            {
                frmViewList frmsel = new frmViewList();                
                dalCustomerOrderList dalorder = new dalCustomerOrderList();
                BindingCollection<modCustomerOrderList> listorder = dalorder.GetIList(false, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.Today.AddDays(-30).ToString("MM-dd-yyyy"), string.Empty, out Util.emsg);
                if (listorder != null)
                {
                    frmsel.Selection = true;
                    frmsel.InitViewList("请选择要导入的客户订单号:", listorder);
                    if (frmsel.ShowDialog() == DialogResult.OK)
                    {
                        EditDesignForm frm = new EditDesignForm();
                        frm.Import(Convert.ToInt32(Util.retValue1));
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            LoadData();
                        }
                    }
                }                            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
