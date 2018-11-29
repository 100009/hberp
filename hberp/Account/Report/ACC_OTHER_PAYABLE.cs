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
    public partial class ACC_OTHER_PAYABLE : Form
    {
        dalAccOtherPayable _dal = new dalAccOtherPayable();
        public ACC_OTHER_PAYABLE()
        {
            InitializeComponent();
        }

        private void ACC_OTHER_PAYABLE_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Detail"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuDetail_Click));
            DBGrid.Tag = this.Name;            
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);            
        }

        private void ACC_OTHER_PAYABLE_Shown(object sender, EventArgs e)
        {
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
                BindingCollection<modOtherPayableSummary> list = _dal.GetOtherPayableSummary(cboAccName.ComboBox.SelectedValue.ToString(), out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid.Columns[0].Visible = false;
                    DBGrid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
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

        private void DBGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mnuDetail_Click(null, null);
        }

        private void mnuDetail_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                BindingCollection<modAccOtherPayable> list = _dal.GetIList(cboAccName.ComboBox.SelectedValue.ToString(), DBGrid.CurrentRow.Cells["objectname"].Value.ToString(), out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(list[0].ObjectName, list);
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

    }
}