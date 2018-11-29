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
    public partial class PRODUCTION_FORM_COST_PRICE : Form
    {
        dalProductionForm _dal = new dalProductionForm();
        public PRODUCTION_FORM_COST_PRICE()
        {
            InitializeComponent();
        }

        private void PRODUCTION_FORM_COST_PRICE_Load(object sender, EventArgs e)
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

        private void PRODUCTION_FORM_COST_PRICE_Shown(object sender, EventArgs e)
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
                BindingCollection<modProductionForm> list = _dal.GetIList("1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
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
            checkboxColumn.Dispose();

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("PriceStatus");
            checkboxColumn.DataPropertyName = "PriceStatus";
            checkboxColumn.Name = "PriceStatus";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(13);
            DBGrid.Columns.Insert(13, checkboxColumn);
            checkboxColumn.Dispose();
        }

        private void toolPrice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modProductionForm mod = (modProductionForm)DBGrid.CurrentRow.DataBoundItem;
                if (mod.AccSeq > 0)
                {
                    MessageBox.Show("该单据已做凭证，不可修改单价！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                }
                EditProductionForm frm = new EditProductionForm();
                frm.EditItem(mod.FormId, true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DBGrid.CurrentRow.Cells["PriceStatus"].Value = 1;
                    DBGrid.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
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
            toolPrice_Click(null, null);
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
                    modProductionForm mod = (modProductionForm)DBGrid.CurrentRow.DataBoundItem;
                    if (mod.PriceStatus == 1 && _dal.ResetPrice(mod.FormId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBGrid.CurrentRow.Cells["PriceStatus"].Value = 0;
                        DBGrid.CurrentRow.DefaultCellStyle.ForeColor = Color.Black;
                        DBGrid.CurrentRow.Selected = false;
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modProductionForm mod = (modProductionForm)DBGrid.SelectedRows[i].DataBoundItem;
                        if (mod.PriceStatus == 1 && _dal.ResetPrice(mod.FormId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["PriceStatus"].Value = 0;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Black;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
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

    }
}