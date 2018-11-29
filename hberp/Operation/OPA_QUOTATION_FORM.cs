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
    public partial class OPA_QUOTATION_FORM : Form
    {
        dalQuotationForm _dal = new dalQuotationForm();
        public OPA_QUOTATION_FORM()
        {
            InitializeComponent();
        }

        private void OPA_QUOTATION_FORM_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            DBGrid.Tag = this.Name;
            dtpFrom.Value = Util.modperiod.StartDate;
            dtpTo.Value = Util.modperiod.EndDate;
            if (dtpTo.Value < DateTime.Today)
                dtpTo.Value = DateTime.Today.AddDays(1);
            if (Util.modperiod.LockFlag == 1)
            {
                toolNew.Visible = false;
                toolEdit.Visible = false;
                toolDel.Visible = false;
            }
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
                string custlist = string.Empty;
                BindingCollection<modQuotationForm> list = _dal.GetIList(string.Empty, custlist, string.Empty, dtpFrom.Text, dtpTo.Text, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
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

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                EditQuotationForm frm = new EditQuotationForm();
                frm.AddItem();
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

        private void toolEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modQuotationForm mod = (modQuotationForm)DBGrid.CurrentRow.DataBoundItem;
                EditQuotationForm frm = new EditQuotationForm();
                frm.EditItem(mod.FormId);
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

        private void toolDel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                modQuotationForm mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), out Util.emsg);
                bool ret = _dal.Save("DEL", mod, null, out Util.emsg);
                if (ret)
                {
                    LoadData();
                }
                else
                {
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
            toolEdit_Click(null, null);
        }

        private void toolExport_Click(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            IList<modExcelRangeData> list = new List<modExcelRangeData>();
            modQuotationForm mod = (modQuotationForm)DBGrid.CurrentRow.DataBoundItem;
            BindingCollection<modQuotationDetail> listdetail = _dal.GetDetail(mod.FormId, out Util.emsg);
            dalCustomerList dalcust = new dalCustomerList();
            modCustomerList modcust = dalcust.GetItem(mod.CustId, out Util.emsg);
            //list.Add(new modExcelRangeData(clsLxms.GetParameterValue("COMPANY_NAME"), "B1", "J1"));
            list.Add(new modExcelRangeData("电话/TEL: " + clsLxms.GetParameterValue("COMPANY_TEL") + "                 传真/FAX: " + clsLxms.GetParameterValue("COMPANY_FAX") + "                 联系人/Contact Person: " + mod.ContactPerson, "A4", "I4"));
            //list.Add(new modExcelRangeData("公司地址:" + clsLxms.GetParameterValue("COMPANY_ADDR"), "B3", "J3"));
            list.Add(new modExcelRangeData(modcust.FullName, "B6", "B6"));
            list.Add(new modExcelRangeData("TEL: " + modcust.Linkman + "  " + modcust.Tel, "B7", "B7"));
            list.Add(new modExcelRangeData("单号Invoice No.: " + mod.No, "H6", "H6"));
            list.Add(new modExcelRangeData(mod.FormDate.ToString("yyyy/MM/dd"), "I8", "I8"));
            
            for (int i = 0; i < listdetail.Count; i++)
            {
                modQuotationDetail modd = listdetail[i];
                string col = (10 + i).ToString().Trim();
                list.Add(new modExcelRangeData((i + 1).ToString(), "A" + col, "A" + col));
                list.Add(new modExcelRangeData(modd.ProductName, "B" + col, "B" + col));
                list.Add(new modExcelRangeData(modd.Brand, "C" + col, "C" + col));
                list.Add(new modExcelRangeData(modd.Qty.ToString(), "D" + col, "D" + col));
                list.Add(new modExcelRangeData(modd.UnitNo, "E" + col, "E" + col));
                list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Price), "G" + col, "G" + col));
                list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.Mny), "H" + col, "H" + col));
                list.Add(new modExcelRangeData(modd.Remark, "I" + col, "I" + col));
            }
            clsExport.ExportByTemplate(list, "报价单", 1, 20, 10, 1);
        }
    }
}