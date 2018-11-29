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
    public partial class ACC_PRODUCT_INOUT : Form
    {
        dalAccProductInout _dal = new dalAccProductInout();
        public ACC_PRODUCT_INOUT()
        {
            InitializeComponent();
        }

        private void ACC_PRODUCT_INOUT_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Detail"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuDetail_Click));
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);
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

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void rbProductWip_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.Columns.Clear();
                if (rbProductWip.Checked)
                {
                    BindingCollection<modAccProductSummary> list = _dal.GetAccProductSummary(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        decimal totalendmny = 0;
                        foreach (modAccProductSummary mod in list)
                        {
                            totalendmny += Convert.ToDecimal(mod.EndMny);
                        }
                        Status1.Text = "结存金额:  " + string.Format("{0:C2}", totalendmny);
                    }
                }
                else
                {
                    BindingCollection<modAccProductSizeSummary> list = _dal.GetAccProductSizeSummary(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                    DBGrid.DataSource = list;
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        DBGrid.Columns["Size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        decimal totalendmny = 0;
                        foreach (modAccProductSizeSummary mod in list)
                        {
                            totalendmny += Convert.ToDecimal(mod.EndMny);
                        }
                        Status1.Text = "结存金额:  " + string.Format("{0:C2}", totalendmny);
                    }
                }
                DBGrid.Columns["StartQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["StartMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["InputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["InputMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["OutputQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["OutputMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["EndQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns["EndMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns[DBGrid.ColumnCount-1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        private void mnuDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                BindingCollection<modAccProductInout> list = _dal.GetIList(cboAccName.ComboBox.SelectedValue.ToString(), DBGrid.CurrentRow.Cells["productid"].Value.ToString(), Util.IsTrialBalance, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(list[0].ProductName, list);
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            mnuDetail_Click(null, null);
        }

        private void toolFind_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;

            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                DBGrid.Rows[i].Visible = true;
            }

            int startindex = 0;
            if (DBGrid.CurrentRow.Index < DBGrid.RowCount - 1)
                startindex = DBGrid.CurrentRow.Index + 1;

            string[] finds = txtFind.Text.Trim().ToUpper().Split('*');
            string flag = clsLxms.GetParameterValue("HIDE_NOT_MATCH_PRODUCT");
            if (flag == "F")   //不隐藏不匹配的
            {
                bool find = false;
                for (int i = startindex; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                        {
                            found = false;
                        }
                    }
                    if (found)
                    {
                        DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                        find = true;
                        return;
                    }
                }
                if (!find)
                {
                    for (int i = 0; i < DBGrid.Rows.Count; i++)
                    {
                        bool found = true;
                        for (int j = 0; j < finds.Length; j++)
                        {
                            if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                            find = true;
                            return;
                        }
                    }
                }
            }
            else   //隐藏不匹配的
            {
                DBGrid.CurrentCell = null;
                for (int i = 0; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (DBGrid.Rows[i].Cells["ProductName"].Value.ToString().IndexOf(finds[j]) < 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                        DBGrid.Rows[i].Visible = true;
                    else
                        DBGrid.Rows[i].Visible = false;
                }
            }
        }
    }
}
