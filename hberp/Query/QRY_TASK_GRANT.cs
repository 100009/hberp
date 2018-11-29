using System;
using System.Collections;
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
    public partial class QRY_TASK_GRANT : Form
    {
        public QRY_TASK_GRANT()
        {
            InitializeComponent();
        }

        private void QRY_TASK_GRANT_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
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
            DBGrid.Columns.Clear();
            ArrayList arrHeader = new ArrayList();
            ArrayList arrTitle = new ArrayList();
            arrHeader.Add(clsTranslate.TranslateString("GroupId"));
            arrHeader.Add(clsTranslate.TranslateString("TaskCode"));
            arrHeader.Add(clsTranslate.TranslateString("TaskName"));
            arrTitle.Add(clsTranslate.TranslateString("GroupId"));
            arrTitle.Add(clsTranslate.TranslateString("TaskCode"));
            arrTitle.Add(clsTranslate.TranslateString("TaskName"));
            dalUserList blluser = new dalUserList();
            BindingCollection<modUserList> listuser = blluser.GetIList(true, out Util.emsg);
            if(listuser!=null && listuser.Count>0)
            {
                foreach (modUserList mod in listuser)
                {
                    arrHeader.Add(mod.UserId);
                    arrTitle.Add(mod.UserName);
                }
            }
            for (int i = 0; i < arrHeader.Count; i++)
            {
                //if (i <= 1)
                //{
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = arrTitle[i].ToString();
                    col.DataPropertyName = arrHeader[i].ToString();
                    col.Name = arrHeader[i].ToString();
                    if (i == 1)
                        col.Visible = false;
                    else if (i == 0 || i == 2)
                        col.Width = 120;
                    else
                        col.Width = 30;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DBGrid.Columns.Add(col);
                    col.Dispose();
                //}
                //else
                //{
                //    DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
                //    col.HeaderText = arrTitle[i].ToString();
                //    col.DataPropertyName = arrHeader[i].ToString();
                //    col.Width = 70;
                //    DBGrid.Columns.Add(col);
                //    col.Dispose();
                //}
            }

            DataGridViewRow row;
            dalTaskList bll = new dalTaskList();
            BindingCollection<modTaskList> list = bll.GetIList(string.Empty, false, false, out Util.emsg);
            if (list != null && list.Count > 0)
            {
                foreach (modTaskList mod in list)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = clsTranslate.TranslateString(mod.GroupId);
                    row.Cells[1].Value = mod.TaskCode;
                    row.Cells[2].Value = clsTranslate.TranslateString(mod.TaskName);
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
            for (int iCol = 3; iCol < DBGrid.ColumnCount; iCol++)
            {
                dalTaskGrant blltg = new dalTaskGrant();
                BindingCollection<modTaskGrant> listtg = blltg.GetUserGrantData(false, false, DBGrid.Columns[iCol].Name, string.Empty, string.Empty, out Util.emsg);
                if (listtg != null && listtg.Count > 0)
                {
                    foreach (modTaskGrant mod in listtg)
                    {
                        for (int iRow = 0; iRow < DBGrid.RowCount; iRow++)
                        {
                            if (mod.TaskCode.CompareTo(DBGrid.Rows[iRow].Cells[1].Value.ToString()) == 0)
                            {
                                DBGrid.Rows[iRow].Cells[iCol].Value = "√";
                                break;
                            }
                        }
                    }
                }
            }
            DBGrid.Columns[2].Frozen = true;
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
            DBGrid.Columns[0].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid.Columns[1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid.Columns[2].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
            DBGrid.MergeColumnNames.Add(arrHeader[0].ToString());
            DBGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }
    }
}
