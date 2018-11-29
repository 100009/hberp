using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BindingCollection;
using LXMS.DAL;
using LXMS.Model;

namespace LXMS.Operation.Edit
{
    public partial class SearchProduct : Form
    {
        dalProductList _dal = new dalProductList();
        public SearchProduct()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (String.IsNullOrWhiteSpace(txtProduct.Text.Trim())){
                    return;
                }
                
                BindingCollection<modProductList> list = _dal.GetIList(String.Empty, txtProduct.Text.Trim(), out Util.emsg);
                DBGrid.DataSource = list;
                DBGrid.Enabled = true;
                if (list != null)
                {
                    if (clsLxms.ShowProductSpecify() == 0)
                    {
                        DBGrid.Columns["Specify"].Visible = false;
                    }
                    if (clsLxms.ShowProductSize() == 0)
                    {
                        DBGrid.Columns["SizeFlag"].Visible = false;
                    }
                }
                else
                {
                    DBGrid.DataSource = null;
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

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells["ProductId"].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells["ProductName"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
                frm.SelectVisible = true;
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
    
    }
}
