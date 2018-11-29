using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.Model;
using LXMS.DAL;

namespace LXMS
{
    public partial class ACTION_SCORES_SUMMARY : Form
    {
        dalCustomerLog _dal = new dalCustomerLog();
        public ACTION_SCORES_SUMMARY()
        {
            InitializeComponent();
        }

        private void ACTION_SCORES_SUMMARY_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
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
                BindingCollection<modActionScoresSummary> list = _dal.GetActionScoresSummary(string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    ArrayList arr = new ArrayList();
                    ArrayList arrman = new ArrayList();
                    foreach (modActionScoresSummary mod in list)
                    {
                        if (arr == null || arr.Count == 0)
                        {
                            arr.Add(mod.ActionType);
                            arrman.Add(mod.ActionMan);
                        }
                        else
                        {
                            bool exists = false;
                            for (int i = 0; i < arr.Count; i++)
                            {
                                if (arr[i].ToString() == mod.ActionType)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                                arr.Add(mod.ActionType);

                            exists = false;
                            for (int i = 0; i < arrman.Count; i++)
                            {
                                if (arrman[i].ToString() == mod.ActionMan)
                                {
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                                arrman.Add(mod.ActionMan);
                        }
                    }
                    DBGrid.Rows.Clear();
                    DBGrid.Columns.Clear();
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.ReadOnly = true;

                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString("ActionMan");
                    col.DataPropertyName = "ActionMan";
                    col.Name = "ActionMan";
                    col.Width = 120;
                    col.ReadOnly = false;
                    DBGrid.Columns.Add(col);
                    col.Dispose();

                    for (int i = 0; i < arr.Count; i++)
                    {
                        col = new DataGridViewTextBoxColumn();
                        col.HeaderText = arr[i].ToString();
                        col.DataPropertyName = arr[i].ToString();
                        col.Name = arr[i].ToString();
                        col.Width = 70;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        col.ReadOnly = true;
                        DBGrid.Columns.Add(col);
                        col.Dispose();
                    }
                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = clsTranslate.TranslateString("Sum");
                    col.DataPropertyName = "Sum";
                    col.Name = "Sum";
                    col.Width = 70;
                    col.ReadOnly = true;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    DBGrid.Columns.Add(col);
                    col.Dispose();

                    for (int i = 0; i < arrman.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = arrman[i].ToString();                        
                        row.DefaultCellStyle.BackColor = Color.Empty;
                        DBGrid.Rows.Add(row);
                    }
                    DataGridViewRow rowsum = new DataGridViewRow();
                    rowsum.CreateCells(DBGrid);
                    rowsum.Cells[0].Value = clsTranslate.TranslateString("Sum");
                    rowsum.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    DBGrid.Rows.Add(rowsum);

                    foreach (modActionScoresSummary mod in list)
                    {
                        for (int i = 0; i < DBGrid.RowCount; i++)
                        {
                            if (mod.ActionMan == DBGrid.Rows[i].Cells[0].Value.ToString())
                            {
                                for (int j = 0; j < DBGrid.ColumnCount; j++)
                                {
                                    if (mod.ActionType == DBGrid.Columns[j].Name)
                                    {
                                        DBGrid.Rows[i].Cells[j].Value = mod.Scores;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    decimal sum;
                    for (int i = 0; i < DBGrid.RowCount -1; i++)
                    {
                        sum = Convert.ToDecimal("0");
                        for (int j = 1; j < DBGrid.ColumnCount - 1; j++)
                        {
                            if (DBGrid.Rows[i].Cells[j].Value != null)
                                sum += Convert.ToDecimal(DBGrid.Rows[i].Cells[j].Value);                            
                        }
                        DBGrid.Rows[i].Cells[DBGrid.ColumnCount - 1].Value = sum;
                    }
                    
                    for (int j = 1; j < DBGrid.ColumnCount; j++)
                    {
                        sum = Convert.ToDecimal("0");
                        for (int i = 0; i < DBGrid.RowCount - 1; i++)
                        {
                            if (DBGrid.Rows[i].Cells[j].Value != null)
                                sum += Convert.ToDecimal(DBGrid.Rows[i].Cells[j].Value);
                        }
                        DBGrid.Rows[DBGrid.RowCount -1].Cells[j].Value = sum;
                    }                    
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentCell == null) return;
                if (DBGrid.CurrentCell.ColumnIndex == 0) return;
                if (DBGrid.CurrentCell.Value == null) return;

                string actiontype=string.Empty;
                if (DBGrid.CurrentCell.ColumnIndex < DBGrid.ColumnCount - 1)
                    actiontype = DBGrid.Columns[DBGrid.CurrentCell.ColumnIndex].Name;

                string actionman = string.Empty;
                if (DBGrid.CurrentCell.RowIndex < DBGrid.RowCount - 1)
                    actionman = DBGrid.Rows[DBGrid.CurrentCell.RowIndex].Cells[0].Value.ToString();
                BindingCollection<modCustomerLog> list = _dal.GetIList(string.Empty, string.Empty, actiontype, actionman, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                if(list!=null)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Customer Log"), list);
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
