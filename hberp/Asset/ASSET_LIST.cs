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
    public partial class ASSET_LIST : Form
    {
        dalAssetList _dal = new dalAssetList();
        public ASSET_LIST()
        {
            InitializeComponent();
        }

        private void ASSET_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            mnuWorkQty = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Work Qty"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuWorkQty_Click));
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Asset Evaluate"), LXMS.Properties.Resources.Plugin, new System.EventHandler(this.mnuAssetEvaluate_Click));
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
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
                BindingCollection<modAssetList> list = _dal.GetIList("1", string.Empty, string.Empty, string.Empty, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    DBGrid.Columns["RawQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["RawMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["LastMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["NetMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DBGrid.Columns["DepreMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Status1.Text = clsTranslate.TranslateString("Record Count") + " : " + list.Count.ToString();
                    decimal rawmny = 0;
                    decimal netmny = 0;
                    decimal depremny = 0;
                    foreach (modAssetList mod in list)
                    {
                        rawmny += mod.RawMny;
                        netmny += mod.NetMny;
                        depremny += mod.DepreMny;
                    }
                    Status2.Text = clsTranslate.TranslateString("Raw mny") + " : " + string.Format("{0:C2}",rawmny);
                    Status3.Text = clsTranslate.TranslateString("Net mny") + " : " + string.Format("{0:C2}", netmny);
                    Status4.Text = clsTranslate.TranslateString("Depre mny") + " : " + string.Format("{0:C2}", depremny);
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
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(3);
            DBGrid.Columns.Insert(3, checkboxColumn);
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modAssetList mod = (modAssetList)DBGrid.CurrentRow.DataBoundItem;
                EditAssetList frm = new EditAssetList();
                frm.EditItem(mod.AssetId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
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
            toolEdit_Click(null, null);
        }

        private void mnuAssetEvaluate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modAssetList mod = (modAssetList)DBGrid.CurrentRow.DataBoundItem;
                dalAssetEvaluate dal=new dalAssetEvaluate();
                BindingCollection<modAssetEvaluate> list = dal.GetIList(string.Empty, string.Empty, mod.AssetId, string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Asset Evaluate"), list);
                    frm.ShowDialog();
                }
                else
                {
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

        private void mnuWorkQty_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modAssetList mod = (modAssetList)DBGrid.CurrentRow.DataBoundItem;
                EditWorkQty frm = new EditWorkQty();
                frm.InitData(mod);
                frm.ShowDialog();
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

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;

            modAssetList mod = (modAssetList)DBGrid.CurrentRow.DataBoundItem;
            if (mod.DepreMethod == "工作量法")
            {
                mnuWorkQty.Enabled = true;
            }
            else
            {
                mnuWorkQty.Enabled = false;
            }
        }
    }
}
