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
    public partial class VendorOrderImport : Form
    {
        bool _initflag = false;
        public VendorOrderImport()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void VendorOrderImport_Load(object sender, EventArgs e)
        {

        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
            
                if (DBGrid.DataSource == null) return;
                //for(int i=0;i<DBGrid.RowCount;i++)
                //{
                //    modVendorOrderList mod = (modVendorOrderList)DBGrid.Rows[i].DataBoundItem;
                //    if (mod.Price <= 0)
                //    {
                //        MessageBox.Show("请设置产品[" + mod.ProductName + "]的价格,必须>0", clsTranslate.TranslateString("information"), MessageBoxButtons.OK,MessageBoxIcon.Information);
                //        DBGrid.CurrentCell = DBGrid.Rows[i].Cells["Price"];
                //        return;
                //    }
                //}
                DBGrid.EndEdit();
                BindingCollection<modVendorOrderList> list = (BindingCollection<modVendorOrderList>)DBGrid.DataSource;
                var p = (from c in list where c.Price <= 0 select c.ProductName).FirstOrDefault();
                if (!string.IsNullOrEmpty(p))
                {
                    MessageBox.Show("请设置产品[" + p + "]的价格,必须>0", clsTranslate.TranslateString("information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dalVendorOrderList dal = new dalVendorOrderList();
                bool ret = dal.Save(list, out Util.emsg);
                if(ret)
                {
                    MessageBox.Show("保存成功", clsTranslate.TranslateString("information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Faulure"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void InitViewList<T>(IList<T> list)
        {
            this.Cursor = Cursors.WaitCursor;
            DBGrid.DataSource = list;
            DBGrid.ReadOnly = false;
            DBGrid.Columns["Price"].ReadOnly = false;
            DBGrid.Columns["ReceivedQty"].Visible = false;
            DBGrid.Columns["Differ"].Visible = false;
            _initflag = true;
            this.Cursor = Cursors.Default;

        }

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_initflag && DBGrid.Columns[e.ColumnIndex].Name == "Price")
            {
                if (DBGrid.CurrentRow.Cells["Price"].Value != null && Util.IsNumeric(DBGrid.CurrentRow.Cells["Price"].Value.ToString()))
                {
                    DBGrid.CurrentRow.Cells["SumMny"].Value = decimal.Parse(DBGrid.CurrentRow.Cells["Qty"].Value.ToString()) * decimal.Parse(DBGrid.CurrentRow.Cells["Price"].Value.ToString());
                }
            }
        }
    }
}
