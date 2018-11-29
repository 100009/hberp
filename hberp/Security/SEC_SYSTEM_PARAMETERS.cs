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
    public partial class SEC_SYSTEM_PARAMETERS : Form
    {
        dalSysParameters _dal = new dalSysParameters();
        bool rowChanged = false;
        public SEC_SYSTEM_PARAMETERS()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void SEC_SYSTEM_PARAMETERS_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                toolLock.Checked = true;
                toolLock.Text = clsTranslate.TranslateString("Unlock");

                BindingCollection<modSysParameters> list = _dal.GetIList(out Util.emsg);
                //DataSet ds = _dal.GetSysParameters(false, out Util.emsg);
                DBGrid.DataSource = list;
                if (list != null && list.Count>0)
                {                    
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

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void DBGrid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!rowChanged) { return; }

            DataGridViewRow row = DBGrid.Rows[e.RowIndex];

            try
            {
                this.Cursor = Cursors.WaitCursor;
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

                string parmid = row.Cells[0].Value.ToString().ToUpper();
                string parmname = row.Cells[1].Value.ToString();
                string parmvalue = row.Cells[2].Value.ToString();
                string remark = row.Cells[3].Value.ToString();
                modSysParameters mod = new modSysParameters(parmid, parmname, parmvalue, remark, Util.UserId, DateTime.Now.ToString());
                bool ret = _dal.Update(parmid, mod, out Util.emsg);
                if (!ret)
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    row.Cells[0].Value = row.Cells[0].Value.ToString().ToUpper();
                    row.Cells[4].Value = Util.UserId;
                    row.Cells[5].Value = DateTime.Now.ToString();
                    StatusLabel4.Text = "Update succeed!";
                }
                
                rowChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                if (v_ID.Length == 0)
                {
                    result += "Please enter para id" + Environment.NewLine;
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

        private void toolLock_Click(object sender, EventArgs e)
        {
            toolLock.Checked = !toolLock.Checked;
            DBGrid.ReadOnly = toolLock.Checked;
            if (toolLock.Checked == true)
            {
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = false;
                DBGrid.Columns[2].ReadOnly = true;
                DBGrid.Columns[2].DefaultCellStyle.ForeColor = Color.Black;
                StatusLabel1.Text = clsTranslate.TranslateString("Locked");
                toolLock.Text = clsTranslate.TranslateString("Unlock");
            }
            else
            {
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = true;
                DBGrid.Columns[2].ReadOnly = false;
                DBGrid.Columns[2].DefaultCellStyle.ForeColor = Color.Red;
                StatusLabel1.Text = "Editable";
                toolLock.Text = clsTranslate.TranslateString("Locked");
            }
            StatusLabel4.Text = "";
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
    }
}