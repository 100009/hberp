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
    public partial class ACC_CREDENCE_SUMMARY : Form
    {
        dalAccReport _dal = new dalAccReport();
        public ACC_CREDENCE_SUMMARY()
        {
            InitializeComponent();
        }

        private void ACC_CREDENCE_SUMMARY_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillPeriodList(cboAccName.ComboBox);
            //LoadData();
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
                BindingCollection<modAccCredenceSummary> list = _dal.GetCredenceSummary(cboAccName.ComboBox.SelectedValue.ToString(), Util.IsTrialBalance, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid.Columns["BorrowMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["LendMoney"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["BorrowMoney"].Width = 140;
                    DBGrid.Columns["LendMoney"].Width = 140;
                    DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid.Rows[DBGrid.RowCount - 1].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;
                    modAccCredenceSummary mod = (modAccCredenceSummary)DBGrid.Rows[DBGrid.RowCount - 1].DataBoundItem;
                    Status1.Text = clsTranslate.TranslateString("Borrow Money") + ": " + mod.BorrowMoney;
                    Status2.Text = clsTranslate.TranslateString("Lend Money") + ": " + mod.LendMoney;
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
                if (DBGrid.CurrentRow == null) return;

                modAccCredenceSummary mod = (modAccCredenceSummary)DBGrid.CurrentRow.DataBoundItem;
                BindingCollection<modAccCredenceDetail> list = new BindingCollection<modAccCredenceDetail>();
                _dal.GetCredenceDetail(false, cboAccName.ComboBox.SelectedValue.ToString(), mod.SubjectId, Util.IsTrialBalance, ref list, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(mod.SubjectName, list);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
