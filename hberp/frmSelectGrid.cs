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
    public partial class frmSelectGrid : Form
    {
        public static string selectionlist = string.Empty;
        public frmSelectGrid()
        {
            InitializeComponent();
        }

        private void frmSelectGrid_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            selectionlist = string.Empty;
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        public void InitViewList<T>(string strTitle, IList<T> list, bool single=false)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Text = strTitle;
            DBGrid.DataSource = list;
            DBGrid.ReadOnly = false;
            if(list!=null && list.Count>0)
                AddComboBoxColumns();
            DBGrid.Tag = "View List - " + strTitle;
            if (single)
            { 
                toolCheckAll.Visible = false;
                toolCheckNone.Visible = false;
            }
            this.Cursor = Cursors.Default;
            
        }

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Checked");
            checkboxColumn.DataPropertyName = "Checked";
            checkboxColumn.Name = "Checked";
            checkboxColumn.Width = 40;
            checkboxColumn.ReadOnly = false;
            DBGrid.Columns.Insert(0, checkboxColumn);
        }

        private void toolSelect_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            DBGrid.EndEdit();
            selectionlist = string.Empty;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (DBGrid.Rows[i].Cells[0].Value != null && Convert.ToBoolean(DBGrid.Rows[i].Cells[0].Value) == true)
                {
                    if (string.IsNullOrEmpty(selectionlist))
                        selectionlist = DBGrid.Rows[i].Cells[1].Value.ToString();
                    else
                        selectionlist += "," + DBGrid.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (string.IsNullOrEmpty(selectionlist))
            {
                MessageBox.Show("请勾选您要的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(!toolCheckAll.Visible && selectionlist.IndexOf(",")>0)
            {
                MessageBox.Show("您只能选择一项!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void toolCheckAll_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                DBGrid.Rows[i].Cells[0].Value = 1;
            }
        }

        private void toolCheckNone_Click(object sender, EventArgs e)
        {
            if (DBGrid.RowCount == 0) return;

            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                DBGrid.Rows[i].Cells[0].Value = 0;
            }
        }

        private void frmSelectGrid_Shown(object sender, EventArgs e)
        {
            if (DBGrid.RowCount>=1 && DBGrid.ColumnCount>=2)
                DBGrid.CurrentCell = DBGrid.Rows[0].Cells[1];
        }
    }
}
