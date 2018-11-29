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
    public partial class ACC_CHECK_LIST : Form
    {
        dalAccCheckList _dal = new dalAccCheckList();
        public ACC_CHECK_LIST()
        {
            InitializeComponent();
        }

        private void ACC_CHECK_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Detail"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAccCredenceList_Click));
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillCheckType(cboCheckType, true, false);            
            FillControl.FillCheckStatus(cboStatus, true);
            FillControl.FillCheckSubject(cboSubject, true);
            FillControl.FillBankList(cboBank, true, false);
            cboStatus.SelectedIndex = 1;
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
                BindingCollection<modAccCheckList> list = _dal.GetIList(cboStatus.SelectedValue.ToString(), string.Empty, cboSubject.SelectedValue.ToString(), cboCheckType.SelectedValue.ToString(), cboBank.SelectedValue.ToString(), txtCheckNo.Text.Trim(), out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DBGrid.Columns["mny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["exchangerate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    decimal totalmny = 0;
                    foreach (modAccCheckList mod in list)
                    {
                        totalmny += mod.Mny;
                    }
                    Status1.Text = "合计金额: " + string.Format("{0:N}", totalmny);
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

        private void mnuAccCredenceList_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modAccCheckList mod = (modAccCheckList)DBGrid.CurrentRow.DataBoundItem;
				if (mod.Status == 0)
				{
					EditAccCredenceList frm = new EditAccCredenceList();
					frm.EditItem(mod.AccName, mod.AccSeq);
					if (frm.ShowDialog() == DialogResult.OK)
					{
						LoadData();
					}
				}
				else
				{
					EditAccCheckForm frm = new EditAccCheckForm();
					dalAccCheckForm dalcf = new dalAccCheckForm();
					modAccCheckForm modcf = dalcf.GetItembyCheckid(mod.Id, out Util.emsg);
					if (modcf != null)
					{
						frm.EditItem(modcf.FormId);
						frm.ShowDialog();
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
            mnuAccCredenceList_Click(null, null);
        }
    }
}
