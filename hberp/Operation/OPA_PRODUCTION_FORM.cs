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
    public partial class OPA_PRODUCTION_FORM : Form
    {
        dalProductionForm _dal = new dalProductionForm();
        public OPA_PRODUCTION_FORM()
        {
            InitializeComponent();
        }

        private void OPA_PRODUCTION_FORM_Load(object sender, EventArgs e)
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
                BindingCollection<modProductionForm> list = _dal.GetIList(rbStatus0.Checked ? "0" : "1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, rbStatus0.Checked ? string.Empty : dtpFrom.Text, rbStatus0.Checked ? string.Empty : dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    DBGrid.Columns["MaterialMny"].Visible = false;
                    DBGrid.Columns["WareMny"].Visible = false;
                    DBGrid.Columns["ProcessMny"].Visible = false;
                    DBGrid.Columns["KillMny"].Visible = false;
                    DBGrid.Columns["OtherMny"].Visible = false;
                    DBGrid.Columns["PriceStatus"].Visible = false;
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

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditProductionForm frm = new EditProductionForm();
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
                modProductionForm mod = (modProductionForm)DBGrid.CurrentRow.DataBoundItem;
                EditProductionForm frm = new EditProductionForm();
                frm.EditItem(mod.FormId, false);
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
                modProductionForm mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                if (mod.Status == 1)
                {
                    MessageBox.Show("该单据已审核，您不能删除！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool ret = _dal.Save("DEL", mod, null, null, out Util.emsg);
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
                    modProductionForm mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (_dal.Audit(mod.FormId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.FormId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modProductionForm mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (_dal.Audit(mod.FormId, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 1;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.FormId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    modProductionForm mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                    if (mod.AccSeq > 0)
                    {
                        MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_dal.Reset(mod.FormId, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.FormId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modProductionForm mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), out Util.emsg);
                        if (mod.AccSeq > 0)
                        {
                            MessageBox.Show("该单据已做凭证，不可重置！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (_dal.Reset(mod.FormId, Util.UserId, out Util.emsg))
                        {
                            //
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.FormId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            IList<modExcelRangeData> list = new List<modExcelRangeData>();
            modProductionForm mod = (modProductionForm)DBGrid.CurrentRow.DataBoundItem;
            list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "B1", "J1"));
            list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_ADDR"), "B2", "J2"));
            list.Add(new modExcelRangeData("电话：" + clsLxms.GetParameterValue("COMPANY_TEL") + "      传真：" + clsLxms.GetParameterValue("COMPANY_FAX"), "B3", "J3"));
            
            list.Add(new modExcelRangeData(mod.No, "J4", "J4"));
            list.Add(new modExcelRangeData(mod.DeptId, "D5", "H5"));
            list.Add(new modExcelRangeData(mod.FormDate.ToString("yyyy年MM月dd日"), "J5", "J5"));
            list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.MaterialMny), "E15", "I15"));
            list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.OtherMny), "E16", "I16"));
            list.Add(new modExcelRangeData(string.Format("{0:C2}", mod.KillMny), "E17", "I17"));
            list.Add(new modExcelRangeData(mod.OtherReason, "J16", "J16"));
            list.Add(new modExcelRangeData(mod.Remark, "D19", "J19"));
            list.Add(new modExcelRangeData(mod.AuditMan, "G21", "H21"));
            list.Add(new modExcelRangeData(mod.ShipMan, "J21", "J21"));
            BindingCollection<modProductionFormWare> listdetail = _dal.GetProductionFormWare(mod.FormId, out Util.emsg);
            for (int i = 0; i < listdetail.Count; i++)
            {
                modProductionFormWare modd = listdetail[i];
                string col = (7 + i).ToString().Trim();
                list.Add(new modExcelRangeData((i+1).ToString(), "B" + col, "B" + col));
                list.Add(new modExcelRangeData(modd.ProductName, "C" + col, "D" + col));
                list.Add(new modExcelRangeData(modd.Specify, "E" + col, "F" + col));
                list.Add(new modExcelRangeData(modd.Qty.ToString(), "G" + col, "G" + col));
                list.Add(new modExcelRangeData(string.Format("{0:N2}", modd.ProcessPrice), "H" + col, "H" + col));
                list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Qty * modd.ProcessPrice), "I" + col, "I" + col));
                list.Add(new modExcelRangeData(modd.Remark, "J" + col, "J" + col));
            }
            clsExport.ExportByTemplate(list, "外发加工单", 1, 21, 10, 1);
        }
    }
}