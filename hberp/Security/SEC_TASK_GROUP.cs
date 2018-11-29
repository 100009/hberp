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
    public partial class SEC_TASK_GROUP : Form
    {
        bool rowChanged = false;
        dalTaskGroup _dal = new dalTaskGroup();
        public SEC_TASK_GROUP()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }
        private void SEC_USER_GROUP_Load(object sender, EventArgs e)
        {
            cboExportType.SelectedIndex = 0;
            LoadData();
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                toolLock.Checked = true;
                toolLock.Text = clsTranslate.TranslateString("Unlock");

                //DataSet ds = _dal.GetTaskGroup(false, out Util.emsg);
                BindingCollection<modTaskGroup> list = _dal.GetIList(false, out Util.emsg);
                if (list != null)
                {
                    DBGrid.DataSource = list;
                    AddComboBoxColumns();
                    DBGrid.AllowUserToAddRows = false;
                    DBGrid.AllowUserToDeleteRows = false;
                    DBGrid.MultiSelect = false;
                    DBGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    for (int iCol = 0; iCol < DBGrid.ColumnCount; iCol++)
                        DBGrid.Columns[iCol].ReadOnly = true;
                }
                else
                {
                    DBGrid.DataSource = null;
                }  
                StatusLabel1.Text = DBGrid.Rows.Count.ToString() + " rows";
                StatusLabel4.Text = "Refresh";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = Cursors.Default;
        }

        private void AddComboBoxColumns()
        {            
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
        }   

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void DBGrid_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[2].Value = "1";
            e.Row.Cells[3].Value = DBGrid.RowCount;
            e.Row.Cells[4].Value = Util.UserId;
            //e.Row.Cells[4].Value = DateTime.Now.ToString();
        }
        
        private void DBGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!rowChanged) { return; }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataGridViewRow row = DBGrid.Rows[e.RowIndex];

                string valResult = ValidateRowEntry(e);
                if (valResult.Length > 0)
                {
                    row.ErrorText = valResult;
                    e.Cancel = true;
                    if (row.IsNewRow)
                    {
                        row.ErrorText = "";
                        DBGrid.CancelEdit();
                    }
                    return;
                }

                string groupid = row.Cells[0].Value.ToString().ToUpper();
                string groupdesc = row.Cells[1].Value.ToString();
                int status = row.Cells[2].Value == null ? 0 : Convert.ToInt32(row.Cells[2].Value.ToString());
                int? seq=Convert.ToInt32(row.Cells[3].Value.ToString());
                modTaskGroup mod = new modTaskGroup(groupid, groupdesc, status, seq ,Util.UserId, DateTime.Now);
                if (Convert.ToDateTime(row.Cells[5].Value).Year == 1)
                {
                    bool ret = _dal.Insert(mod, out Util.emsg);
                    if (!ret)
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        row.Cells[0].Value = row.Cells[0].Value.ToString().ToUpper();
                        row.Cells[5].Value = DateTime.Now.ToString();
                        row.Tag = "1";
                        StatusLabel4.Text = "Add succeed!";
                    }
                }
                else
                {
                    bool ret = _dal.Update(groupid, mod, out Util.emsg);
                    if (!ret)
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        row.Cells[0].Value = row.Cells[0].Value.ToString().ToUpper();
                        row.Cells[5].Value = DateTime.Now.ToString();
                        StatusLabel4.Text = "Update succeed!";
                    }
                }
                rowChanged = false;
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

        private void DBGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            rowChanged = true;
            StatusLabel4.Text = "";            
        }

        private void DBGrid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DBGrid.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        /// <summary>
        /// 验证一行输入的数据是否有效
        /// </summary>
        /// <param name="rowEntry"></param>
        /// <returns></returns>
        private string ValidateRowEntry(DataGridViewCellCancelEventArgs rowEntry)
        {
            try
            {
                string result = string.Empty;

                DataGridViewRow row = DBGrid.Rows[rowEntry.RowIndex];

                string v_ID = row.Cells[0].Value.ToString();
                string v_Desc = row.Cells[0].Value.ToString();

                if (v_ID.Length == 0)
                {
                    result += "Please enter group id" + Environment.NewLine;
                }
                if (v_Desc.Length == 0)
                {
                    result += "Please enter group desc" + Environment.NewLine;
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private void DBGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string errorText = string.Empty;
            errorText += "Error，Location，Row:" + (e.RowIndex + 1).ToString() + "，Col" + (e.ColumnIndex + 1).ToString();
            DBGrid.Rows[e.RowIndex].ErrorText = errorText;

            e.Cancel = true;
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private bool DeleteCurrentRow()
        {
            string roleid = DBGrid.CurrentRow.Cells[0].Value.ToString();

            bool ret = _dal.Delete(roleid, out Util.emsg);
            if (!ret)
            {
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                StatusLabel4.Text = "Delete succeed!";
                return true;
            }
        }

        private void DBGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (e.Row.IsNewRow) { return; }
                
                DialogResult result = MessageBox.Show("Do you really want to delete it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DeleteCurrentRow();
                }
                else
                {
                    e.Cancel = true;
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

        private void toolLock_Click(object sender, EventArgs e)
        {
            toolLock.Checked = !toolLock.Checked;
            if (toolLock.Checked == true)
            {
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = false;
                DBGrid.Columns[0].ReadOnly = true;
                DBGrid.Columns[1].ReadOnly = true;
                DBGrid.Columns[2].ReadOnly = true;
                DBGrid.Columns[3].ReadOnly = true;
                //DBGrid.AllowUserToAddRows = false;
                StatusLabel1.Text = clsTranslate.TranslateString("Locked");
                toolLock.Text = clsTranslate.TranslateString("Unlock");
            }
            else
            {
                DBGrid.AllowUserToAddRows = true;
                DBGrid.AllowUserToDeleteRows = true;
                DBGrid.Columns[0].ReadOnly = false;
                DBGrid.Columns[1].ReadOnly = false;
                DBGrid.Columns[2].ReadOnly = false;
                DBGrid.Columns[3].ReadOnly = false;
                //DBGrid.AllowUserToAddRows = true;
                StatusLabel1.Text = "Editable";
                toolLock.Text = clsTranslate.TranslateString("Locked");
            }            
            StatusLabel4.Text = "";
        }

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow.IsNewRow)
                {
                    DBGrid.CurrentRow.ErrorText = "";
                    DBGrid.CancelEdit();
                    return;
                }

                DialogResult result = MessageBox.Show("Do you really want to delete it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    DeleteCurrentRow();
                    DBGrid.Rows.RemoveAt(DBGrid.CurrentRow.Index);
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

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(2, DBGrid);
        }

        private void widthToHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(1, DBGrid);
        }

        private void widthToContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.AutoSetColWidth(3, DBGrid);
        }

        private void exportToOOoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private void toolExport_Click(object sender, EventArgs e)
        {
            string filename = clsExport.GetExportFilePath(cboExportType.SelectedIndex);
            this.Cursor = Cursors.WaitCursor;
            if (string.IsNullOrEmpty(filename) == false)
            {
                clsExport exp = new clsExport(this.Text, filename, cboExportType.SelectedIndex, DBGrid);
                bool ret = exp.ExportGrid(out Util.emsg);
                if (!ret)
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }

        private void toolInactive_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool ret = _dal.Inactive(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                if (ret)
                {
                    MessageBox.Show("Inactive Success!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DBGrid.CurrentRow.Cells[2].Value != null)
                        DBGrid.CurrentRow.Cells[2].Value = 1 - Convert.ToInt32(DBGrid.CurrentRow.Cells[2].Value.ToString());
                    else
                        DBGrid.CurrentRow.Cells[2].Value = 1;
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
