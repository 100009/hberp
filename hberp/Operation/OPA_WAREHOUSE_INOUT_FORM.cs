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
    public partial class OPA_WAREHOUSE_INOUT_FORM : Form
    {
        dalWarehouseInoutForm _dal = new dalWarehouseInoutForm();
        public OPA_WAREHOUSE_INOUT_FORM()
        {
            InitializeComponent();
        }

        private void OPA_WAREHOUSE_INOUT_FORM_Load(object sender, EventArgs e)
        {
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

        private void OPA_WAREHOUSE_INOUT_FORM_Shown(object sender, EventArgs e)
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
                if (Convert.ToInt32(DBGrid.Rows[i].Cells["InoutFlag"].Value) == -1)
                    DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.toolCancelFrozen_Click(null, null);
                BindingCollection<modWarehouseInoutForm> list = _dal.GetIList(rbStatus0.Checked ? "0" : "1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, rbStatus0.Checked ? string.Empty : dtpFrom.Text, rbStatus0.Checked ? string.Empty : dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    ShowColor();
                    DBGrid.Columns["CostPrice"].Visible = false;
                    for (int i = 0; i < list.Count; i++)
                    {
                        DBGrid.Rows[i].Cells["CostPrice"].Value = "*";
                        DBGrid.Columns["size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGrid.Columns["qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGrid.Columns["CostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditWarehouseInout frm = new EditWarehouseInout();
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

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                modWarehouseInoutForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
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
                    modWarehouseInoutForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
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
                        modWarehouseInoutForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.SelectedRows[i].Cells[0].Value), out Util.emsg);
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
                    modWarehouseInoutForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.CurrentRow.Cells[0].Value), out Util.emsg);
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
                        modWarehouseInoutForm mod = _dal.GetItem(Convert.ToInt32(DBGrid.SelectedRows[i].Cells[0].Value), out Util.emsg);
                        if (mod.AccSeq > 0)
                        {
                            MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_dal.Reset(mod.Id, Util.UserId, out Util.emsg))
                        {
                            //
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

    }
}