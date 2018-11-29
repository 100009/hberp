using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LXMS.DAL;
using LXMS.Model;
using BindingCollection;

namespace LXMS
{
    public partial class ACC_EXPENSE_SUMMARY : Form
    {
        dalAccExpenseForm _dal = new dalAccExpenseForm();
        public ACC_EXPENSE_SUMMARY()
        {
            InitializeComponent();
        }

        private void ACC_EXPENSE_SUMMARY_Load(object sender, EventArgs e)
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

        public void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string statuslist = "9";
                if (chkNotAudit.Checked)
                    statuslist += ",0";
                if (chkAudited.Checked)
                    statuslist += ",1";

                DBGrid.Rows.Clear();
                BindingCollection<modAccExpenseSummary> list = _dal.GetExpenseSummary(statuslist, string.Empty, string.Empty, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null)
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
                SetChartData();
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
            if (DBGrid.CurrentRow == null) return;
            string statuslist = "9";
            if (chkNotAudit.Checked)
                statuslist += ",0";
            if (chkAudited.Checked)
                statuslist += ",1";

            modAccExpenseSummary mod = (modAccExpenseSummary)DBGrid.CurrentRow.DataBoundItem;
            BindingCollection<modAccExpenseForm> list = _dal.GetIList(statuslist, string.Empty, mod.ExpenseId, mod.ExpenseMan, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            if (list != null && list.Count > 0)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Account Expense Form"), list);
                frm.ShowDialog();
            }
        }

        private void SetChartData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                chart1.Text = clsTranslate.TranslateString("Expense Man");
                chart1.Series.Clear();

                chart2.Text = clsTranslate.TranslateString("Expense Name");
                chart2.Series.Clear();
                if (DBGrid.RowCount == 0) return;

                ArrayList arrMan = new ArrayList();                
                ArrayList arrName = new ArrayList();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {                    
                    modAccExpenseSummary mod = (modAccExpenseSummary)DBGrid.Rows[i].DataBoundItem;
                    if (i == 0)
                    {
                        arrMan.Add(mod.ExpenseMan);
                        arrName.Add(mod.ExpenseName);
                    }
                    else
                    {
                        bool exists = false;
                        for (int j = 0; j < arrMan.Count; j++)
                        {
                            if (mod.ExpenseMan == arrMan[j].ToString())
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            arrMan.Add(mod.ExpenseMan);
                        }
                        ////////////////////////////////////////////////////////
                        exists = false;
                        for (int j = 0; j < arrName.Count; j++)
                        {
                            if (mod.ExpenseName == arrName[j].ToString())
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            arrName.Add(mod.ExpenseName);
                        }
                    }
                }

                Series ser1 = new Series();
                for(int k=0;k<arrMan.Count;k++)
                {
                    decimal summan=0;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        modAccExpenseSummary mod = (modAccExpenseSummary)DBGrid.Rows[i].DataBoundItem;
                        if (mod.ExpenseMan == arrMan[k].ToString())
                            summan += mod.ExpenseMny;
                    }
                    ser1.Points.AddXY(arrMan[k], summan);
                }
                ser1.ChartType = SeriesChartType.Pie;
                ser1.BorderColor = Color.Transparent;
                ser1.LabelAngle = 60;
                chart1.Series.Add(ser1);
                chart1.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart1.Legends[0].BackColor = Color.Transparent;
                //////////////////////////////////////////////////////////////////////////
                Series ser2 = new Series();
                ser2 = new Series();
                for (int k = 0; k < arrName.Count; k++)
                {
                    decimal sumname = 0;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        modAccExpenseSummary mod = (modAccExpenseSummary)DBGrid.Rows[i].DataBoundItem;
                        if (mod.ExpenseName == arrName[k].ToString())
                            sumname += mod.ExpenseMny;
                    }
                    ser2.Points.AddXY(arrName[k], sumname);
                }
                ser2.ChartType = SeriesChartType.Pie;
                ser2.BorderColor = Color.Transparent;
                ser2.LabelAngle = 60;
                chart2.Series.Add(ser2);
                chart2.ChartAreas[0].AxisX.IsMarginVisible = true;
                chart2.Legends[0].BackColor = Color.Transparent;                                
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
    }
}
