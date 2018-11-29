using BindingCollection;
using LXMS.DAL;
using LXMS.Model;
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
    public partial class EditCollectCamp : Form
    {
        dalSalesShipment _dal = new dalSalesShipment();
        INIClass ini = new INIClass(Util.INI_FILE);
        int _iPaperWidth = 240;
        int _iPaperHeight = 0;
        public EditCollectCamp()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
            this.BackColor = frmOptions.BORDERCOLOR;
            DBGridHistory.ContextMenuStrip.Items.Clear();
            DBGridHistory.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Print"), LXMS.Properties.Resources.preview, new System.EventHandler(this.btnPreview_Click));
        }

        private void EditCollectCamp_Load(object sender, EventArgs e)
        {
            LoadHistory();
            LoadDBGrid();
            
            txtQty.TextAlign = HorizontalAlignment.Right;
            txtAmount.TextAlign = HorizontalAlignment.Right;
        }

        private void EditCollectCamp_Shown(object sender, EventArgs e)
        {
            lblCurrentUser.Text = "营业员：" + Util.UserName;
        }

        private void LoadDBGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.Rows.Clear();
                DBGrid.Columns.Clear();
                DBGrid.ReadOnly = false;
                DBGrid.AllowUserToAddRows = false;
                DBGrid.AllowUserToDeleteRows = false;
                DBGrid.ReadOnly = false;
                DBGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
                DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;

                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductId");
                col.DataPropertyName = "ProductId";
                col.Name = "ProductId";
                col.ReadOnly = false;
                col.Visible = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ProductName");
                col.DataPropertyName = "ProductName";
                col.Name = "ProductName";
                col.Width = 200;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Barcode");
                col.DataPropertyName = "Barcode";
                col.Name = "Barcode";
                col.Visible = false;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("UnitNo");
                col.DataPropertyName = "UnitNo";
                col.Name = "UnitNo";
                col.Width = 50;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Qty");
                col.DataPropertyName = "Qty";
                col.Name = "Qty";
                col.Width = 80;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Price");
                col.DataPropertyName = "Price";
                col.Name = "Price";
                col.Width = 100;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Amount");
                col.DataPropertyName = "Amount";
                col.Name = "Amount";
                col.Width = 120;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.DefaultCellStyle.ForeColor = Color.LightGray;                
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Remark");
                col.DataPropertyName = "Remark";
                col.Name = "Remark";
                col.Width = 150;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Specify");
                col.DataPropertyName = "Specify";
                col.Name = "Specify";
                col.Visible = false;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) || e.KeyChar == Convert.ToChar(Keys.Space))
            {
                dalProductSalePrice dalprice = new dalProductSalePrice();
                dalProductList dal = new dalProductList();
                BindingCollection<modProductList> list = dal.GetIListByBarcode(txtProduct.Text.Trim(), out Util.emsg);
                if(list!=null && list.Count>0)
                {
                    if(list.Count==1)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);

                        row.Height = 40;
                        row.Cells[0].Value = list[0].ProductId;
                        row.Cells[1].Value = list[0].ProductName;
                        row.Cells[2].Value = list[0].Barcode;
                        row.Cells[3].Value = list[0].UnitNo;
                        row.Cells[4].Value = 1;
                        row.Cells[5].Value = dalprice.GetDefaultPrice(list[0].ProductId, out Util.emsg);
                        row.Cells[6].Value = decimal.Parse(row.Cells[5].Value.ToString());
                        row.Cells[8].Value = list[0].Specify;                        
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    else
                    {
                        frmViewList frm = new frmViewList();
                        frm.InitViewList("请选择商品：", list);
                        frm.Selection = true;
                        if(frm.ShowDialog()==DialogResult.OK)
                        {
                            var modPdt = list.Where(c => c.ProductId == Util.retValue1).First();
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);

                            row.Height = 40;
                            row.Cells[0].Value = modPdt.ProductId;
                            row.Cells[1].Value = modPdt.ProductName;
                            row.Cells[2].Value = modPdt.Barcode;
                            row.Cells[3].Value = modPdt.UnitNo;
                            row.Cells[4].Value = 1;
                            row.Cells[5].Value = dalprice.GetDefaultPrice(modPdt.ProductId, out Util.emsg);
                            row.Cells[6].Value = decimal.Parse(row.Cells[5].Value.ToString());
                            row.Cells[8].Value = modPdt.Specify;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                    }
                    SumData();                    
                }
                else
                {
                    modProductList mod = dal.GetItem(txtProduct.Text.Trim(), out Util.emsg);
                    if (mod != null)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(DBGrid);

                        row.Height = 40;
                        row.Cells[0].Value = mod.ProductId;
                        row.Cells[1].Value = mod.ProductName;
                        row.Cells[2].Value = mod.Barcode;
                        row.Cells[3].Value = mod.UnitNo;
                        row.Cells[4].Value = 1;
                        row.Cells[5].Value = dalprice.GetDefaultPrice(mod.ProductId, out Util.emsg);
                        row.Cells[6].Value = decimal.Parse(row.Cells[5].Value.ToString());
                        row.Cells[8].Value = mod.Specify;
                        DBGrid.Rows.Add(row);
                        row.Dispose();

                        SumData();
                    }
                    else
                    {
                        MessageBox.Show("没有找到商品！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if(DBGrid.RowCount>0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[DBGrid.RowCount - 1].Cells[4];
                }
                txtProduct.Text = string.Empty;
                txtProduct.Focus();
            }
        }

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==4 || e.ColumnIndex==5)
            {
                DBGrid.Rows[e.RowIndex].Cells[6].Value = decimal.Parse(DBGrid.Rows[e.RowIndex].Cells[4].Value.ToString()) * decimal.Parse(DBGrid.Rows[e.RowIndex].Cells[5].Value.ToString());
                SumData();
            }

        }

        private void SumData()
        {
            if (DBGrid.Rows.Count == 0) return;

            decimal sumQty = 0;
            decimal sumAmount = 0;
            for(int i=0;i<DBGrid.RowCount;i++)
            {
                if (DBGrid.Rows[i].Cells[4].Value != null)
                {
                    sumQty += decimal.Parse(DBGrid.Rows[i].Cells[4].Value.ToString());                    
                }
                if (DBGrid.Rows[i].Cells[6].Value != null)
                {
                    sumAmount += decimal.Parse(DBGrid.Rows[i].Cells[6].Value.ToString());
                }
            }
            txtQty.Text = sumQty.ToString();
            txtAmount.Text = sumAmount.ToString();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.EndEdit();                
                if (DBGrid.RowCount == 0)
                {
                    MessageBox.Show("没有明细数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[0].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[0].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Product id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }                 
                    if (DBGrid.Rows[i].Cells[4].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[4].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (Convert.ToDecimal(DBGrid.Rows[i].Cells[4].Value.ToString()) <= 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Qty") + clsTranslate.TranslateString(" must > 0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (DBGrid.Rows[i].Cells[5].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (!Util.IsNumeric(DBGrid.Rows[i].Cells[5].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Price") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    
                }

                modSalesShipment mod = new modSalesShipment();
                mod.ShipId = _dal.GetNewId(DateTime.Today);
                mod.ShipDate = DateTime.Today;
                mod.ShipType = "收营单";
                //mod.No = txtNo.Text.Trim();
                mod.AdFlag = 1;
                dalCustomerList dalcust = new dalCustomerList();
                //mod.CustOrderNo = txtCustOrderNo.Text.Trim();
                mod.CustName = "散客";
                mod.CustId = clsLxms.GetDefaultCustId();
                
                mod.PayMethod = "现金";
                mod.SalesMan = Util.UserId;                
                mod.OtherMny = 0;
                mod.KillMny = 0;
                mod.Currency = Util.Currency;
                dalAccCurrencyList dalcur = new dalAccCurrencyList();
                modAccCurrencyList modcur = dalcur.GetItem(mod.Currency, out Util.emsg);
                mod.ExchangeRate = modcur.ExchangeRate;
                mod.DetailSum = decimal.Parse(txtAmount.Text);
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;
                mod.ReceiveStatus = 1;
                mod.AccountNo = "现金";

                mod.MakeDate = DateTime.Today;
                mod.ReceiveDate = DateTime.Today.ToString("yyyy-MM-dd");
                mod.InvoiceStatus = 0;
                mod.InvoiceMny = 0;
                string detaillist = string.Empty;
                dalCustomerOrderList dalorder = new dalCustomerOrderList();
                BindingCollection<modSalesShipmentDetail> list = new BindingCollection<modSalesShipmentDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modSalesShipmentDetail modd = new modSalesShipmentDetail();
                    modd.Seq = i + 1;
                    modd.ProductId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.ProductName = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.Specify = DBGrid.Rows[i].Cells[8].Value.ToString();
                    modd.UnitNo = DBGrid.Rows[i].Cells[3].Value.ToString();
                    modd.Size = 1;
                    modd.Qty = Convert.ToDecimal(DBGrid.Rows[i].Cells[4].Value.ToString());
                    modd.Price = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.SalesManMny = 0;
                    modd.WarehouseId = clsLxms.GetDefaultWarehouseId();
                    
                    list.Add(modd);
                }
                bool ret = _dal.Save("ADD", mod, list, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    LoadHistory();
                    DBGrid.Rows.Clear();
                    txtQty.Text = "0";
                    txtAmount.Text = "0";
                    btnPreview_Click(null, null);
                    txtProduct.Focus();
                }
                else
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnLock_Click(object sender, EventArgs e)
        {
            frmChangeUser frm = new frmChangeUser();
            if(frm.ShowDialog()!=DialogResult.OK)
            {                
                this.Dispose();
            }
            else
            {
                this.lblCurrentUser.Text = "营业员：" + Util.UserName;
            }
        }

        private void LoadHistory()
        {            
            BindingCollection<modCollectCamp> list = _dal.GetCollectCamp(Util.UserId, DateTime.Today.ToString("yyyy-MM-dd"), out Util.emsg);
            DBGridHistory.DataSource = list;
            if (list != null && list.Count > 0)
                DBGridHistory.CurrentCell = DBGridHistory.Rows[0].Cells[1];
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (DBGridHistory.CurrentRow == null) return;

            PrintService print = new PrintService();
            IList<modPrintItem> list = GenerateLabelData();
            print.StartPreview(list, 1, _iPaperWidth, _iPaperHeight);
        }

        private IList<modPrintItem> GenerateLabelData()
        {
            IList<modPrintItem> list = new List<modPrintItem>();

            int t_margin = 5;
            int l_margin = 5;
            int RowHeight = 20;
            string fontname = "宋体";
            float fontsize = 8;
            float fontsize_title = 10;
            int linewidth = 230;
            int printlogo = 0;
            int[] colLeft = { 10, 120, 160, 190 };

            FontStyle title_fontstyle = FontStyle.Bold;
            FontStyle label_fontstyle = FontStyle.Regular;
                        
            int iTop = t_margin;
            modPrintItem mod = new modPrintItem();
            int iLogoWidth = 0;
            if (printlogo == 1)
            {
                mod = new modPrintItem();
                mod.printType = "IMAGE";
                mod.PrintImage = Properties.Resources.ICON_16;
                mod.iLeft = l_margin;
                mod.iTop = iTop;
                iLogoWidth = 28;
                mod.iWidth = iLogoWidth;
                mod.iHeight = 30;
                list.Add(mod);
                iLogoWidth += 2;
            }

            mod = new modPrintItem();
            mod.printType = "TXT";
            mod.PrintText = clsLxms.GetParameterValue("COMPANY_NAME");
            mod.iLeft = 40;
            mod.iTop = iTop;
            mod.FontName = fontname;
            mod.FontSize = fontsize_title;
            mod.LabelFontStyle = title_fontstyle;
            list.Add(mod);

            mod = new modPrintItem();
            mod.printType = "LINE";
            mod.PrintText = "";
            mod.iLeft = l_margin + iLogoWidth;
            iTop += RowHeight + 3;
            mod.iTop = iTop;
            mod.iWidth = linewidth - iLogoWidth;
            mod.FontName = fontname;
            mod.FontSize = fontsize;
            list.Add(mod);
            /////////////////////////////////////////////////////////////////////
            string labeltitle = string.Empty;
            string content = string.Empty;
            SizeF size = Util.GetSizeF(this.Controls[0], labeltitle, fontname, fontsize);

            iTop += RowHeight + 3;
            list.Add(new modPrintItem { printType = "TXT", PrintText = "商品名称", iLeft = colLeft[0], iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });
            list.Add(new modPrintItem { printType = "TXT", PrintText = "单价", iLeft = colLeft[1], iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });
            list.Add(new modPrintItem { printType = "TXT", PrintText = "数量", iLeft = colLeft[2], iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });
            list.Add(new modPrintItem { printType = "TXT", PrintText = "金额", iLeft = colLeft[3]+5, iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });

            /////////////////////////////////////////////////////////////////////
            decimal totalAmount = 0;
            BindingCollection<modSalesShipmentDetail> listDetail = _dal.GetDetail(DBGridHistory.CurrentRow.Cells["ShipId"].Value.ToString(), out Util.emsg);
            if (listDetail != null && listDetail.Count > 0)
            {
                foreach (modSalesShipmentDetail modd in listDetail)
                {
                    iTop += RowHeight + 3;
                    list.Add(new modPrintItem { printType = "TXT", PrintText = modd.ProductName.Length <= 17 ? modd.ProductName : modd.ProductName.Substring(1,14)+ "...", iLeft = colLeft[0], iTop = iTop, FontName = fontname, FontSize = fontsize, LabelFontStyle = label_fontstyle });
                    list.Add(new modPrintItem { printType = "TXT", PrintText = modd.Price.ToString(), iLeft = colLeft[1], iTop = iTop, FontName = fontname, FontSize = fontsize, LabelFontStyle = label_fontstyle });
                    list.Add(new modPrintItem { printType = "TXT", PrintText = modd.Qty.ToString(), iLeft = colLeft[2], iTop = iTop, FontName = fontname, FontSize = fontsize, LabelFontStyle = label_fontstyle });
                    list.Add(new modPrintItem { printType = "TXT", PrintText = (modd.Price*modd.Qty).ToString(), iLeft = colLeft[3], iTop = iTop, FontName = fontname, FontSize = fontsize, LabelFontStyle = label_fontstyle });
                    totalAmount += modd.Price * modd.Qty;
                }
            }

            iTop += RowHeight + 3;
            mod = new modPrintItem();
            mod.printType = "LINE";
            mod.PrintText = "";
            mod.iLeft = l_margin ;            
            mod.iTop = iTop;
            mod.iWidth = linewidth - iLogoWidth;
            mod.FontName = fontname;
            mod.FontSize = fontsize;
            list.Add(mod);

            iTop += RowHeight + 3;
            list.Add(new modPrintItem { printType = "TXT", PrintText = "合  计：", iLeft = colLeft[0], iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });
            list.Add(new modPrintItem { printType = "TXT", PrintText = totalAmount.ToString(), iLeft = colLeft[1], iTop = iTop, FontName = fontname, FontSize = fontsize + 1, LabelFontStyle = FontStyle.Bold });

            _iPaperHeight = 30 + iTop; 
            return list;
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                if (MessageBox.Show("您真的要删除本行数据吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                DBGrid.Rows.Remove(DBGrid.CurrentRow);
                SumData();
            }
        }

    }
}
