using BindingCollection;
using LXMS.Model;
using LXMS.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LXMS
{
    public partial class ACC_BOOK : Form
    {
        bool prepared = true;
        public ACC_BOOK()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void ACC_BOOK_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Parse(DateTime.Today.Year.ToString().Trim() + "-01-01");
            dtpTo.Value = DateTime.Today.AddDays(1);
            FillControl.FillAccBookType(cboBookType, false);
        }

        private void cboBookType_SelectedIndexChanged(object sender, EventArgs e)
        {
            prepared = false;
            DBGrid.DataSource = null;
            switch (cboBookType.SelectedIndex)
            {
                case 0:
                    dalCustomerList dalCust = new dalCustomerList();
                    BindingCollection<modCustomerSimpleList> listCust = dalCust.GetSimpleList(out Util.emsg);
                    DBGrid.DataSource = listCust;
                    if (listCust != null)
                    {
                        for (int i = 2; i < DBGrid.ColumnCount; i++)
                        {
                            DBGrid.Columns[i].Visible = false;
                        }
                        DBGrid.Columns[0].Width = 90;
                        DBGrid.Columns[1].Width = DBGrid.Width - 140;
                    }
                    break;
                case 1:
                    dalVendorList dalVendor = new dalVendorList();
                    BindingCollection<modVendorList> listVendor = dalVendor.GetIList("1", string.Empty, out Util.emsg);
                    DBGrid.DataSource = listVendor;
                    if (listVendor != null)
                    {
                        for (int i = 1; i < DBGrid.ColumnCount; i++)
                        {
                            DBGrid.Columns[i].Visible = false;
                        }
                        DBGrid.Columns[0].Width = DBGrid.Width - 50;
                    }
                    break;
                case 2:
                    dalOtherReceivableObject dalORec = new dalOtherReceivableObject();
                    BindingCollection<modOtherReceivableObject> listORec = dalORec.GetIList(true, out Util.emsg);
                    DBGrid.DataSource = listORec;
                    if (listORec != null)
                    {
                        for (int i = 1; i < DBGrid.ColumnCount; i++)
                        {
                            DBGrid.Columns[i].Visible = false;
                        }
                        DBGrid.Columns[0].Width = DBGrid.Width - 50;
                    }
                    break;
                case 3:
                    dalOtherPayableObject dalOPay = new dalOtherPayableObject();
                    BindingCollection<modOtherPayableObject> listOPay = dalOPay.GetIList(true, out Util.emsg);
                    DBGrid.DataSource = listOPay;
                    if (listOPay != null)
                    {
                        for (int i = 1; i < DBGrid.ColumnCount; i++)
                        {
                            DBGrid.Columns[i].Visible = false;
                        }
                        DBGrid.Columns[0].Width = DBGrid.Width - 50;
                    }
                    break;
                case 4:
                    dalAccBankAccount dalCash = new dalAccBankAccount();
                    BindingCollection<modAccBankAccount> listCash = dalCash.GetIList(out Util.emsg);
                    DBGrid.DataSource = listCash;
                    if (listCash != null)
                    {
                        for (int i = 2; i < DBGrid.ColumnCount; i++)
                        {
                            DBGrid.Columns[i].Visible = false;
                        }
                        DBGrid.Columns[0].Width = 90;
                        DBGrid.Columns[1].Width = DBGrid.Width - 140;
                    }
                    break;
            }
            prepared = true;
        }

        private void LoadData()
        {
            if (prepared == false) return;
            if (DBGrid.CurrentRow == null) return;

            switch (cboBookType.SelectedIndex)
            {
                case 0:  //应收
                    modCustomerSimpleList modCust = (modCustomerSimpleList)DBGrid.CurrentRow.DataBoundItem;
                    dalAccReceivableList dalRec = new dalAccReceivableList();
                    BindingCollection<modReceivableBook> listRec = dalRec.GetReceivableBook(modCust.CustId, dtpFrom.Value, dtpTo.Value, out Util.emsg);
                    DBGrid2.DataSource = listRec;                    
                    break;
                case 1:  //应付
                    modVendorList modVendor = (modVendorList)DBGrid.CurrentRow.DataBoundItem;
                    dalAccPayableList dalPay = new dalAccPayableList();
                    BindingCollection<modPayableBook> listPay = dalPay.GetPayableBook(modVendor.VendorName, dtpFrom.Value, dtpTo.Value, out Util.emsg);
                    DBGrid2.DataSource = listPay;                    
                    break;
                case 2:  //其它应收
                    modOtherReceivableObject modObjRec = (modOtherReceivableObject)DBGrid.CurrentRow.DataBoundItem;
                    dalAccOtherReceivable dalORec = new dalAccOtherReceivable();
                    BindingCollection<modOtherReceivableBook> listORec = dalORec.GetOtherReceivableBook(modObjRec.ObjectName, dtpFrom.Value, dtpTo.Value, out Util.emsg);
                    DBGrid2.DataSource = listORec;
                    break;
                case 3:  //其它应付
                    modOtherPayableObject modObjPay = (modOtherPayableObject)DBGrid.CurrentRow.DataBoundItem;
                    dalAccOtherPayable dalOPay = new dalAccOtherPayable();
                    BindingCollection<modOtherPayableBook> listOPay = dalOPay.GetOtherPayableBook(modObjPay.ObjectName, dtpFrom.Value, dtpTo.Value, out Util.emsg);
                    DBGrid2.DataSource = listOPay;
                    break;
                case 4:  //现金银行
                    modAccBankAccount modCash = (modAccBankAccount)DBGrid.CurrentRow.DataBoundItem;
                    dalAccReport dalCash = new dalAccReport();
                    BindingCollection<modAccCredenceBook> listCash = dalCash.GetCashAndBankBook(modCash.AccountNo, dtpFrom.Value, dtpTo.Value, out Util.emsg);
                    DBGrid2.DataSource = listCash;
                    break;
            }
            for(int i=0;i<DBGrid2.RowCount;i++)
            {
                if(DBGrid2.Rows[i].Cells["AccSeq"].Value.ToString()=="本月合计")
                {
                    DBGrid2.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            if(DBGrid2.RowCount>0)
            {
                DBGrid2.Columns[0].Visible = false;

                DBGrid2.Columns["StartMny"].Width = 120;
                DBGrid2.Columns["AddingMny"].Width = 120;
                DBGrid2.Columns["PaidMny"].Width = 120;
                DBGrid2.Columns["EndMny"].Width = 120;

                DBGrid2.Columns["StartMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["AddingMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["PaidMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid2.Columns["EndMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }                
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DBGrid2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid2.CurrentRow == null || DBGrid2.CurrentRow.Cells["AccSeq"].Value == null) return;
                if (!Util.IsInt(DBGrid2.CurrentRow.Cells["AccSeq"].Value.ToString())) return;
                //modAccCredenceList mod = (modAccCredenceList)DBGrid.CurrentRow.DataBoundItem;
                EditAccCredenceList frm = new EditAccCredenceList();
                frm.EditItem(DBGrid2.CurrentRow.Cells["AccName"].Value.ToString(), int.Parse(DBGrid2.CurrentRow.Cells["AccSeq"].Value.ToString()), true);
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
    }
}
