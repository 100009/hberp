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
    public partial class QRY_QUOTATION_DETAIL : Form
    {
        dalQuotationForm _dal = new dalQuotationForm();
        public QRY_QUOTATION_DETAIL()
        {
            InitializeComponent();
        }

        private void QRY_QUOTATION_DETAIL_Load(object sender, EventArgs e)
        {
            DBGrid.Tag = this.Name;
            clsTranslate.InitLanguage(this);
            FillControl.FillCustomerList(lstCustomer);
            lstCustomer.BackColor = frmOptions.BACKCOLOR;
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
                string custlist = string.Empty;
                if (lstCustomer.SelectedItems.Count > 0 && lstCustomer.SelectedItems.Count < lstCustomer.Items.Count)
                {
                    for (int i = 0; i < lstCustomer.SelectedItems.Count; i++)
                    {
                        modCustomerList mod = (modCustomerList)lstCustomer.SelectedItems[i];
                        if (i == 0)
                            custlist = mod.CustId;
                        else
                            custlist += "," + mod.CustId;
                    }
                }

                BindingCollection<modVQuotationDetail> list = _dal.GetVDetail(custlist, string.Empty, string.Empty, txtProductName.Text.Trim(), dtpFrom.Text, dtpTo.Text, out Util.emsg);
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
    }
}