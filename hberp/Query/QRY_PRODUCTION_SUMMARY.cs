using System;
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
    public partial class QRY_PRODUCTION_SUMMARY : Form
    {
        dalProductionForm _dal = new dalProductionForm();
        public QRY_PRODUCTION_SUMMARY()
        {
            InitializeComponent();
        }

        private void QRY_PRODUCTION_SUMMARY_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string statuslist = "9";
                if (chkNotAudit.Checked)
                    statuslist += ",0";
                if (chkAudited.Checked)
                    statuslist += ",1";

                string custlist = string.Empty;

                BindingCollection<modProductionSummary> list = _dal.GetProductionSummary(statuslist, string.Empty, string.Empty, string.Empty, txtInvNo.Text.Trim(), txtDeptId.Text.Trim(), dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                decimal sum = 0;
                if (list == null || list.Count == 0)
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusLabel1.Text = "共 0 行数据， 合计金额为: " + string.Format("{0:C2}", sum);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            BindingCollection<modProductionForm> list = new BindingCollection<modProductionForm>();
            if (DBGrid.CurrentRow.Index == DBGrid.RowCount - 1)
            {
                list = _dal.GetIList(statuslist, string.Empty, string.Empty, string.Empty, txtInvNo.Text.Trim(), txtDeptId.Text.Trim(), dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            else
            {
                modProductionSummary moditem = (modProductionSummary)DBGrid.CurrentRow.DataBoundItem;
                list = _dal.GetIList(statuslist, string.Empty, string.Empty, string.Empty, txtInvNo.Text.Trim(), moditem.DeptId, dtpFrom.Text, dtpTo.Text, out Util.emsg);
            }
            if (list != null && list.Count > 0)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList(clsTranslate.TranslateString("Production Form"), list);
                frm.ShowDialog();
            }
        }
    }
}
