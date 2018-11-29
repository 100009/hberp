using LXMS.Model;
using LXMS.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_EXPENSE_COLUMN : Form
    {
        public ACC_EXPENSE_COLUMN()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void ACC_EXPENSE_COLUMN_Load(object sender, EventArgs e)
        {
            FillControl.FillPeriodList(cboAccName.ComboBox);
            FillControl.FillAccExpenseType(cboExpenseType.ComboBox, false);
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
        }
        
        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (cboAccName.ComboBox.SelectedValue == null || cboExpenseType.ComboBox.SelectedValue == null) return;

            DBGrid.Columns.Clear();

            DataGridViewTextBoxColumn col0 = new DataGridViewTextBoxColumn();
            col0.HeaderText = "月";
            col0.DataPropertyName = "Month";
            col0.Name = "Month";
            col0.Width = 50;
            DBGrid.Columns.Add(col0);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "日";
            col1.DataPropertyName = "Day";
            col1.Name = "Day";
            col1.Width = 50;
            DBGrid.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "凭证号";
            col2.DataPropertyName = "AccSeq";
            col2.Name = "AccSeq";
            col2.Width = 50;
            DBGrid.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "摘要";
            col3.DataPropertyName = "Digest";
            col3.Name = "Digest";
            col3.Width = 220;
            DBGrid.Columns.Add(col3);
            
            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "小计";
            col4.DataPropertyName = "RowSum";
            col4.Name = "RowSum";
            col4.Width = 120;
            col4.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            //col4.DefaultCellStyle.ForeColor = Color.Red;
            col4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DBGrid.Columns.Add(col4);

            dalAccExpenseForm dal=new dalAccExpenseForm();
            BindingCollection<modAccExpenseColumn> list = dal.GetExpenseColumn(cboAccName.ComboBox.SelectedValue.ToString(), cboExpenseType.ComboBox.SelectedValue.ToString(), out Util.emsg);
            if(list!=null && list.Count>0)
            {
                var p = list.Select(c => c.ExpenseName).Distinct();
                foreach(string expenseName in p)
                {
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = expenseName;
                    col.DataPropertyName = expenseName;
                    col.Name = expenseName;
                    col.Width = 100;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns.Add(col);
                    col.Dispose();
                }

                var q = list.Select(c => new { Month = c.FormDate.Month, Day = c.FormDate.Day, AccSeq = c.AccSeq, Digest = c.Digest }).OrderBy(a => a.Month).OrderBy(a => a.Day).OrderBy(a => a.AccSeq).Distinct().ToList();
                q.ForEach(item =>
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Height = 36;
                        row.Cells[0].Value = item.Month;
                        row.Cells[1].Value = item.Day;
                        row.Cells[2].Value = item.AccSeq;
                        row.Cells[3].Value = item.Digest;
						decimal rowSum = 0;
						for (int i = 5; i < DBGrid.ColumnCount; i++)
						{
							var t = list.Where(a => a.FormDate.Day == item.Day && a.AccSeq == item.AccSeq && a.Digest == item.Digest && a.ExpenseName == DBGrid.Columns[i].Name).Select(c => c.ExpenseMny).FirstOrDefault();
							if (t != 0)
							{
								row.Cells[i].Value = t;
								rowSum += t;
							}
						}
                        row.Cells[4].Value = rowSum;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    });

                DataGridViewRow rowTotal = new DataGridViewRow();
                rowTotal.CreateCells(DBGrid);
                rowTotal.Height = 36;
                rowTotal.Cells[2].Value = "合  计";
                for (int i = 4; i < DBGrid.ColumnCount; i++)
                {
                    decimal colSum = 0;
                    for(int j =0;j<DBGrid.RowCount;j++)
                    {
                        colSum += DBGrid.Rows[j].Cells[i].Value == null ? 0 : decimal.Parse(DBGrid.Rows[j].Cells[i].Value.ToString());
                    }
                    rowTotal.Cells[i].Value = colSum;
                }
                rowTotal.DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                DBGrid.Rows.Add(rowTotal);                
            }
            else
            {
                if(!string.IsNullOrEmpty(Util.emsg))
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                IList<modExcelRangeData> list = new List<modExcelRangeData>();
                //list.Add(new modExcelRangeData(string.Format("{0}", clsLxms.GetParameterValue("COMPANY_NAME")), "C3", "D3"));
                //modAccPeriodList modperiod = (modAccPeriodList)cboAccName.SelectedItem;
                //list.Add(new modExcelRangeData(string.Format("{0}", modperiod.EndDate.ToString("yyyy年MM月dd日")), "F3", "F3"));
                list.Add(new modExcelRangeData(string.Format("{0}", cboExpenseType.SelectedText), "A2", "D2"));
                for (int i = 4; i < DBGrid.ColumnCount; i++)
                {
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Columns[i].HeaderText), ((char)(65 + i)) + "3", ((char)(65 + i)) + "3"));
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[DBGrid.RowCount-1].Cells[i].Value.ToString()), ((char)(65 + i)) + "35", ((char)(65 + i)) + "35"));
                }
                for (int i = 0; i < DBGrid.RowCount-1; i++)
                {                    
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[0].Value.ToString()), "A" + (i + 4).ToString(), "A"+(i + 4).ToString()));
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[1].Value.ToString()), "B" + (i + 4).ToString(), "B" + (i + 4).ToString()));
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[2].Value.ToString()), "C" + (i + 4).ToString(), "C" + (i + 4).ToString()));
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[3].Value.ToString()), "D" + (i + 4).ToString(), "D" + (i + 4).ToString()));
                    list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[4].Value.ToString()), "E" + (i + 4).ToString(), "E" + (i + 4).ToString()));
                    for (int j = 5; j < DBGrid.ColumnCount; j++)
                    {
                        list.Add(new modExcelRangeData(string.Format("{0}", DBGrid.Rows[i].Cells[j].Value==null?"":DBGrid.Rows[i].Cells[j].Value.ToString()), ((char)(65 + j)) + (i + 5).ToString(), ((char)(65 + j)) + (i + 5).ToString()));
                    }
                }
                clsExport.ExportByTemplate(list, "费用多栏账", 1, 35, 36, 1);
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
