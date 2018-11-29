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
    public partial class CRM_CHANGE_SALES_MAN : Form
    {
        dalCustomerList _dal = new dalCustomerList();
        public CRM_CHANGE_SALES_MAN()
        {
            InitializeComponent();
        }

        private void CRM_CHANGE_SALES_MAN_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            FillControl.FillEmployeeList(cboSalesMan.ComboBox, false, false);
            FillControl.FillEmployeeList(cboNewMan.ComboBox, false, true);
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cboNewMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNewMan.ComboBox.SelectedValue == null) return;
            if (cboNewMan.ComboBox.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_EMPLOYEE_LIST frm = new MTN_EMPLOYEE_LIST();
            frm.ShowDialog();
            FillControl.FillEmployeeList(cboNewMan.ComboBox, false, true);
            FillControl.FillEmployeeList(cboSalesMan.ComboBox, false, false);
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

        private void cboSalesMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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
                DBGrid.DataSource = null;
                DBGrid.Columns.Clear();
                BindingCollection<modCustomerList> list = _dal.GetIList(cboSalesMan.ComboBox.SelectedValue.ToString(), out Util.emsg);
                DBGrid.DataSource = list;
                DBGrid.ReadOnly = false;
                if (list != null && list.Count > 0)
                {
                    AddComboBoxColumns();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
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

        private void toolPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (DBGrid.RowCount == 0) return;

                DBGrid.EndEdit();

                if (MessageBox.Show("您真的要更改业务员吗?", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                string custidlist = string.Empty;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[0].Value != null && Convert.ToBoolean(DBGrid.Rows[i].Cells[0].Value) == true)
                    {
                        if (string.IsNullOrEmpty(custidlist))
                            custidlist = DBGrid.Rows[i].Cells[1].Value.ToString();
                        else
                            custidlist += "," + DBGrid.Rows[i].Cells[1].Value.ToString();
                    }
                }
                if (string.IsNullOrEmpty(custidlist))
                {
                    MessageBox.Show("请勾选您要的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool ret = _dal.UpdateSalesMan(custidlist, cboNewMan.ComboBox.SelectedValue.ToString(), out Util.emsg);
                if (ret)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
