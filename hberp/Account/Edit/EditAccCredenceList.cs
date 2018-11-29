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
    public partial class EditAccCredenceList : Form
    {
        dalAccCredenceList _dal = new dalAccCredenceList();
        string _action;
        public EditAccCredenceList()
        {
            InitializeComponent();
            DBGrid.ReadOnly = false;
            DBGrid.ContextMenuStrip.Items.Add("-");
            mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
            mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
        }

        private void EditAccCredenceList_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;                     
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

                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Digest");
                col.DataPropertyName = "Digest";
                col.Name = "Digest";
                col.Width = 120;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                string[] showcell = { "Digest" };
                DBGrid.SetParam(showcell);

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("SubjectId");
                col.DataPropertyName = "SubjectId";
                col.Name = "SubjectId";
                col.Width = 80;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("SubjectName");
                col.DataPropertyName = "SubjectName";
                col.Name = "SubjectName";
                col.Width = 120;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("DetailId");
                col.DataPropertyName = "DetailId";
                col.Name = "DetailId";
                col.Width = 80;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("DetailName");
                col.DataPropertyName = "DetailName";
                col.Name = "DetailName";
                col.Width = 80;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("BorrowMoney");
                col.DataPropertyName = "BorrowMoney";
                col.Name = "BorrowMoney";
                col.Width = 120;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("LendMoney");
                col.DataPropertyName = "LendMoney";
                col.Name = "LendMoney";
                col.Width = 120;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("ExchangeRate");
                col.DataPropertyName = "ExchangeRate";
                col.Name = "ExchangeRate";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Currency");
                col.DataPropertyName = "Currency";
                col.Name = "Currency";
                col.Width = 60;
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

        public bool AddItem(string acctype, string selectionlist)
        {
            _action = "NEW";            
            txtSeq.Text = _dal.GetNewSeq(Util.modperiod.AccName, out Util.emsg).ToString();            
            dtpDate.Value = DateTime.Today;
            if (dtpDate.Value > Util.modperiod.EndDate)
                dtpDate.Value = Util.modperiod.EndDate;

            txtAttachCount.Text = "0";
            lblCredenceType.Text = acctype;
            FillControl.FillCredenceWord(cboCredenceWord, false, true);
            cboCredenceWord.SelectedIndex = -1;
            txtSeq.ReadOnly = false;
            LoadDBGrid();
            DataGridViewRow row;
            decimal summny = 0;
            string remark = string.Empty;
            string subjectid = string.Empty;
            string subjectname = string.Empty;
            int maxRemarkLen = 1000 - (string.IsNullOrEmpty(Util.retValue1)? 0 : Util.retValue1.Length);
            switch (acctype)
            {
                case "销售凭证":
                    #region salesshipment
                    dalSalesShipment dalss=new dalSalesShipment();
                    BindingCollection<modSalesShipment> listss = dalss.GetIList(selectionlist, out Util.emsg);
                    DBGridDetail.DataSource = listss;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listss.Count.ToString();                    
                    if (listss != null && listss.Count > 0)
                    {
                        foreach (modSalesShipment mod in listss)
                        {
                            summny += Convert.ToDecimal(mod.AdFlag) * (mod.DetailSum + mod.OtherMny - mod.KillMny) * mod.ExchangeRate;
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.CustName;
                            else
                            {
                                if (remark.IndexOf(mod.CustName) < 0 && remark.Length + mod.CustName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.CustName;
                            }                            
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "售货款";
                    row.Cells[1].Value = "1055";
                    row.Cells[2].Value = "应收帐款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = summny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();                    
                    
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "销售收入";
                    row.Cells[1].Value = "913505";
                    row.Cells[2].Value = "本年利润";
                    row.Cells[3].Value = "主营业务收入";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = summny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                    #endregion
                    break;
                case "设计加工凭证":
                    #region sales design form
                    dalSalesDesignForm dalsd = new dalSalesDesignForm();
                    BindingCollection<modSalesDesignForm> listsd = dalsd.GetIList(selectionlist, out Util.emsg);
                    DBGridDetail.DataSource = listsd;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listsd.Count.ToString();
                    if (listsd != null && listsd.Count > 0)
                    {
                        foreach (modSalesDesignForm mod in listsd)
                        {
                            summny += Convert.ToDecimal(mod.AdFlag) * mod.Mny * mod.ExchangeRate;
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.CustName;
                            else
                            {
                                if (remark.IndexOf(mod.CustName) < 0 && remark.Length + mod.CustName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.CustName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "设计加工货款";
                    row.Cells[1].Value = "1055";
                    row.Cells[2].Value = "应收帐款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = summny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "设计加工收入";
                    row.Cells[1].Value = "913505";
                    row.Cells[2].Value = "本年利润";
                    row.Cells[3].Value = "主营业务收入";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = summny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                    #endregion
                    break;
                case "采购凭证":
                    #region purchaselist
                    dalPurchaseList dalpc = new dalPurchaseList();
                    BindingCollection<modPurchaseList> listpc = dalpc.GetIList(selectionlist, out Util.emsg);
                    DBGridDetail.DataSource = listpc;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listpc.Count.ToString();
                    if (listpc != null && listpc.Count > 0)
                    {
                        foreach (modPurchaseList mod in listpc)
                        {
                            summny += Convert.ToDecimal(mod.AdFlag) * (mod.DetailSum + mod.OtherMny - mod.KillMny) * mod.ExchangeRate;
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.VendorName;
                            else
                            {
                                if (remark.IndexOf(mod.VendorName) < 0 && remark.Length + mod.VendorName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.VendorName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "购入商品";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "购入商品合计";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = summny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "购货款";
                    row.Cells[1].Value = "5145";
                    row.Cells[2].Value = "应付帐款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = summny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();                                        
                    #endregion
                    break;                
                case "费用登记":
                    #region acc expense form
                    dalAccExpenseForm dalexp = new dalAccExpenseForm();
                    BindingCollection<modAccExpenseForm> listexp = dalexp.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listexp;
                    cboCredenceWord.SelectedValue = "付";
                    txtAttachCount.Text = listexp.Count.ToString();
                    if (listexp != null && listexp.Count > 0)
                    {
                        foreach (modAccExpenseForm mod in listexp)
                        {
                            ExpenseFormItemAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.ExpenseName;
                            else
                            {
                                if (remark.IndexOf(mod.ExpenseName) < 0 && remark.Length + mod.ExpenseName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.ExpenseName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "收款凭证":
                    #region acc receivable form
                    dalAccReceivableForm dalrec = new dalAccReceivableForm();
                    BindingCollection<modAccReceivableForm> listrec = dalrec.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listrec;
                    cboCredenceWord.SelectedValue = "收";
                    txtAttachCount.Text = listrec.Count.ToString();
                    if (listrec != null && listrec.Count > 0)
                    {
                        foreach (modAccReceivableForm mod in listrec)
                        {
                            ReceivableFormItemAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.CustName;
                            else
                            {
                                if (remark.IndexOf(mod.CustName) < 0 && remark.Length + mod.CustName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.CustName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "付款凭证":
                    #region acc payable form
                    dalAccPayableForm dalpay = new dalAccPayableForm();
                    BindingCollection<modAccPayableForm> listpay = dalpay.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listpay;
                    cboCredenceWord.SelectedValue = "付";
                    txtAttachCount.Text = listpay.Count.ToString();
                    if (listpay != null && listpay.Count > 0)
                    {
                        foreach (modAccPayableForm mod in listpay)
                        {
                            PayableFormItemAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.VendorName;
                            else
                            {
                                if (remark.IndexOf(mod.VendorName) < 0 && remark.Length + mod.VendorName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.VendorName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;                    
                    #endregion
                    break;
                case "其它应收凭证":
                    #region acc other receivable form
                    dalAccOtherReceivableForm dalorec = new dalAccOtherReceivableForm();
                    BindingCollection<modAccOtherReceivableForm> listorec = dalorec.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listorec;
                    cboCredenceWord.SelectedValue = "收";
                    txtAttachCount.Text = listorec.Count.ToString();
                    if (listorec != null && listorec.Count > 0)
                    {
                        foreach (modAccOtherReceivableForm mod in listorec)
                        {
                            OtherReceivableFormItemAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.ObjectName;
                            else
                            {
                                if (remark.IndexOf(mod.ObjectName) < 0 && remark.Length + mod.ObjectName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.ObjectName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "其它应付凭证":
                    #region acc other payable form
                    dalAccOtherPayableForm dalopay = new dalAccOtherPayableForm();
                    BindingCollection<modAccOtherPayableForm> listopay = dalopay.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listopay;
                    cboCredenceWord.SelectedValue = "付";
                    txtAttachCount.Text = listopay.Count.ToString();
                    if (listopay != null && listopay.Count > 0)
                    {
                        foreach (modAccOtherPayableForm mod in listopay)
                        {
                            OtherPayableFormItemAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.ObjectName;
                            else
                            {
                                if (remark.IndexOf(mod.ObjectName) < 0 && remark.Length + mod.ObjectName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.ObjectName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "仓库进出":
                    #region warehouse inout form
                    dalWarehouseInoutForm dalio = new dalWarehouseInoutForm();
                    BindingCollection<modWarehouseInoutForm> listio = dalio.GetIList(selectionlist, out Util.emsg);
                    DBGridDetail.DataSource = listio;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listio.Count.ToString();
                    if (listio != null && listio.Count > 0)
                    {
                        foreach (modWarehouseInoutForm mod in listio)
                        {
                            summny = mod.Size * mod.Qty * mod.CostPrice;
                            if (summny > 0)
                            {
                                InoutItemAdd(mod.InoutType, mod.InoutFlag, summny);
                            }
                            else
                            {
                                MessageBox.Show("商品[ " + mod.ProductName + " ]的成本不能为0", mod.Id.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                    }
                    txtRemark.Text = Util.retValue1;
                    #endregion
                    break;
                case "生产凭证":
                    #region acc production form
                    dalProductionForm dalpdt = new dalProductionForm();
                    BindingCollection<modProductionForm> listpdt = dalpdt.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listpdt;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listpdt.Count.ToString();
                    if (listpdt != null && listpdt.Count > 0)
                    {
                        foreach (modProductionForm mod in listpdt)
                        {
                            if (!ProductionItemAdd(mod))
                            {
                                return false;
                            }
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.FormId;
                            else
                            {
                                if (remark.IndexOf(mod.FormId) < 0 && remark.Length + mod.FormId.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.FormId;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "支票承兑":
                    #region acc check form
                    dalAccCheckForm dalcheck = new dalAccCheckForm();
                    BindingCollection<modAccCheckForm> listcheck = dalcheck.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listcheck;
                    cboCredenceWord.SelectedValue = "兑";
                    txtAttachCount.Text = listcheck.Count.ToString();
                    if (listcheck != null && listcheck.Count > 0)
                    {
                        foreach (modAccCheckForm mod in listcheck)
                        {
                            AccCheckFormItemAdd(mod);
                        }
                    }
                    #endregion
                    break;
                case "固定资产增加":
                    #region acc asset add
                    dalAssetAdd dalassetadd = new dalAssetAdd();
                    BindingCollection<modAssetAdd> listassetadd = dalassetadd.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listassetadd;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listassetadd.Count.ToString();
                    if (listassetadd != null && listassetadd.Count > 0)
                    {
                        foreach (modAssetAdd mod in listassetadd)
                        {
                            AssetAdd(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.AssetName;
                            else
                            {
                                if (remark.IndexOf(mod.AssetName) < 0 && remark.Length + mod.AssetName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.AssetName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "固定资产处理":
                    #region acc asset sale
                    dalAssetSale dalassetsale = new dalAssetSale();
                    BindingCollection<modAssetSale> listassetsale = dalassetsale.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listassetsale;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listassetsale.Count.ToString();
                    if (listassetsale != null && listassetsale.Count > 0)
                    {
                        foreach (modAssetSale mod in listassetsale)
                        {
                            AssetSale(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.AssetName;
                            else
                            {
                                if (remark.IndexOf(mod.AssetName) < 0 && remark.Length + mod.AssetName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.AssetName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "固定资产评估":
                    #region acc asset evaluate
                    dalAssetEvaluate dalassetevaluate = new dalAssetEvaluate();
                    BindingCollection<modAssetEvaluate> listassetevaluate = dalassetevaluate.GetIList(string.Empty, selectionlist, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                    DBGridDetail.DataSource = listassetevaluate;
                    cboCredenceWord.SelectedValue = "转";
                    txtAttachCount.Text = listassetevaluate.Count.ToString();
                    if (listassetevaluate != null && listassetevaluate.Count > 0)
                    {
                        foreach (modAssetEvaluate mod in listassetevaluate)
                        {
                            AssetEvaluate(mod);
                            if (string.IsNullOrEmpty(remark))
                                remark = mod.AssetName;
                            else
                            {
                                if (remark.IndexOf(mod.AssetName) < 0 && remark.Length + mod.AssetName.Length + 1 <= maxRemarkLen)
                                    remark += "," + mod.AssetName;
                            }
                        }
                        remark += "等" + Util.retValue1;
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "资产折旧":
                    #region acc asset depre
                    dalAssetList daldepre = new dalAssetList();
                    BindingCollection<modAssetDepreList> listdepre = daldepre.GetWaitDepreList(Util.modperiod.AccName, out Util.emsg);
                    DBGridDetail.DataSource = listdepre;
                    if (listdepre != null && listdepre.Count > 0)
                    {
                        cboCredenceWord.SelectedValue = "折";
                        txtAttachCount.Text = listdepre.Count.ToString();
                        if (listdepre != null && listdepre.Count > 0)
                        {
                            foreach (modAssetDepreList mod in listdepre)
                            {
                                AssetDepre(mod);
                                if (string.IsNullOrEmpty(remark))
                                    remark = mod.AssetName;
                                else
                                {
                                    if (remark.IndexOf(mod.AssetName) < 0 && remark.Length + mod.AssetName.Length + 1 <= maxRemarkLen)
                                        remark += "," + mod.AssetName;
                                }
                            }
                            remark += "等" + Util.retValue1;
                        }
                        txtRemark.Text = remark;
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(Util.emsg))
                            MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    #endregion
                    break;
                case "月末结算":
                case "试算平衡":
                    #region salesshipment
                    cboCredenceWord.SelectedValue = "结";                        
                    dtpDate.Value = Util.modperiod.EndDate;
                    txtRemark.Text = acctype;
                    dalSalesShipment dalbalance = new dalSalesShipment();
                    BindingCollection<modSalesShipmentCost> listbalance = dalbalance.GetSalesShipmentCost(Util.modperiod.AccName, out Util.emsg);
                    DBGridDetail.DataSource = listbalance;
                    if (listbalance != null && listbalance.Count > 0)
                    {
                        txtAttachCount.Text = listbalance.Count.ToString();
                        int idx = 0;
                        foreach (modSalesShipmentCost mod in listbalance)
                        {
                            summny += mod.Size * mod.Qty * mod.CostPrice;                            
                            if (mod.Profit < 0)
                                DBGridDetail.Rows[idx].Cells["Profit"].Style.ForeColor = Color.Red;
                            idx++;
                        }
                        DBGridDetail.ReadOnly = false;
                        for (int i = 0; i < DBGridDetail.ColumnCount; i++)
                            DBGridDetail.Columns[i].ReadOnly = true;
                        DBGridDetail.Columns["CostPrice"].ReadOnly = false;
                        DBGridDetail.Columns["CostPrice"].DefaultCellStyle.ForeColor = Color.Red;
                        DBGridDetail.Columns["CostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGridDetail.Columns["CostMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGridDetail.Columns["SalesMny"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DBGridDetail.Columns["Profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    else
                    {
                        txtAttachCount.Text = "0";
                        //MessageBox.Show(clsTranslate.TranslateString("No data found"), clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "库存商品结算";
                    row.Cells[1].Value = "913510";
                    row.Cells[2].Value = "本年利润";
                    row.Cells[3].Value = "主营业务成本";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = summny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "月末结算";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "分明细记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = summny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose(); 
                    #endregion
                    break;
                case "价格调整":
                    #region price adjust form
                    dalPriceAdjustForm dalpaf = new dalPriceAdjustForm();
                    cboCredenceWord.SelectedValue = "结";                    
                    BindingCollection<modPriceAdjustDetail> listpaf = dalpaf.GetDetail(selectionlist, out Util.emsg);
                    DBGridDetail.DataSource = listpaf;
                    if (listpaf != null && listpaf.Count > 0)
                    {
                        txtAttachCount.Text = listpaf.Count.ToString();
                        for (int i = 0; i < listpaf.Count; i++)
                        {
                            PriceAdjustFormAdd(listpaf[i]);
                            if (string.IsNullOrEmpty(remark))
                                remark = listpaf[i].ProductName;
                            else
                            {
                                remark += "," + listpaf[i].ProductName;
                            }
                        }
                    }
                    txtRemark.Text = remark;
                    #endregion
                    break;
                case "零库清理":
                    #region salesshipment
                    cboCredenceWord.SelectedValue = "结";                        
                    dtpDate.Value = Util.modperiod.EndDate;
                    txtRemark.Text = acctype;
                    dalAccProductInout dalzero = new dalAccProductInout();
                    BindingCollection<modAccProductInout> listzero = dalzero.GetNewZeroProduct(Util.modperiod.AccName, out Util.emsg);
                    DBGridDetail.DataSource = listzero;
                    if (listzero != null && listzero.Count > 0)
                    {
                        txtAttachCount.Text = listzero.Count.ToString();
                        foreach (modAccProductInout mod in listzero)
                        {
                            summny += mod.StartMny;
                        }                     
                    }
                    else
                    {
                        txtAttachCount.Text = "0";
                        return false;
                        //MessageBox.Show(clsTranslate.TranslateString("No data found"), clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }

                    if (summny > 0)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "零库存商品清理";
                        row.Cells[1].Value = "91353082";
                        row.Cells[2].Value = "管理费用";
                        row.Cells[3].Value = "商品损耗";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();

                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "零库清理";
                        row.Cells[1].Value = "1235";
                        row.Cells[2].Value = "库存商品";
                        row.Cells[3].Value = "分明细记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    else
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "库存商品清理";
                        row.Cells[1].Value = "91353080";
                        row.Cells[2].Value = "管理费用";
                        row.Cells[3].Value = "商品盈溢";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny * (-1);
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();

                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "月末月末结算";
                        row.Cells[1].Value = "1235";
                        row.Cells[2].Value = "库存商品";
                        row.Cells[3].Value = "分明细记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = summny * (-1);
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    #endregion
                    break;
				case "利润分配":
					#region salesshipment
					cboCredenceWord.SelectedValue = "转";
					dtpDate.Value = Util.modperiod.EndDate;
					txtRemark.Text = acctype;					
					txtAttachCount.Text = "0";
										
					row = new DataGridViewRow();
					row.CreateCells(DBGrid);
					row.Cells[0].Value = "本年利润结转";
					row.Cells[1].Value = "9135";
					row.Cells[2].Value = "本年利润";
					row.Cells[3].Value = "利润结转";
					row.Cells[4].Value = "";
					row.Cells[5].Value = 0;
					row.Cells[6].Value = 0;
					row.Cells[7].Value = 1;
					row.Cells[8].Value = Util.Currency;
					row.Height = 40;
					DBGrid.Rows.Add(row);
					row.Dispose();

					row = new DataGridViewRow();
					row.CreateCells(DBGrid);
					row.Cells[0].Value = "未分配利润转入";
					row.Cells[1].Value = "9165";
					row.Cells[2].Value = "未分配利润";
					row.Cells[3].Value = "无";
					row.Cells[4].Value = "";
					row.Cells[5].Value = 0;
					row.Cells[6].Value = 0;
					row.Cells[7].Value = 1;
					row.Cells[8].Value = Util.Currency;
					row.Height = 40;
					DBGrid.Rows.Add(row);
					row.Dispose();
					
					#endregion
					break;
                default:   
                    //DBGrid.ReadOnly = false;
                    //DBGrid.ContextMenuStrip.Items.Add("-");
                    //mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
                    //mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
                    break;
            }
            GetSumMoney();
            status4.Image = null;
            return true;
        }

        private void InoutItemAdd(string inouttype, int inoutflag, decimal summny)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "商品出入库";
                row.Cells[1].Value = "1235";
                row.Cells[2].Value = "库存商品";
                row.Cells[3].Value = "商品出入库合计";
                row.Cells[4].Value = "";
                if (inoutflag == 1)   //入库
                {
                    row.Cells[5].Value = summny;
                    row.Cells[6].Value = 0;
                }
                else   //出库
                {
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = summny;
                }
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "商品出入库";                               
                row.Cells[4].Value = "";
                switch (inouttype)
                {
                    case "生产领料出库":
                        row.Cells[1].Value = "1225";
                        row.Cells[2].Value = "在建品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                        break;
                    case "生产商品入库":
                        row.Cells[1].Value = "1225";
                        row.Cells[2].Value = "在建品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                        break;
                    case "损耗出库":                        
                        row.Cells[1].Value = "91353082";
                        row.Cells[2].Value = "管理费用";
                        row.Cells[3].Value = "商品损耗";
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                        break;
                    case "溢余入库":
                        row.Cells[1].Value = "91353080";
                        row.Cells[2].Value = "管理费用";
                        row.Cells[3].Value = "商品盈溢";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                        break;
                    case "借出物出库":
                        row.Cells[1].Value = "1245";
                        row.Cells[2].Value = "借出商品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                        break;
                    case "借出物入库":                        
                        row.Cells[1].Value = "1245";
                        row.Cells[2].Value = "借出商品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                        break;
                    case "借入物出库":
                        row.Cells[1].Value = "5165";
                        row.Cells[2].Value = "借入商品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                        break;
                    case "借入物入库":
                        row.Cells[1].Value = "5165";
                        row.Cells[2].Value = "借入商品";
                        row.Cells[3].Value = "分单记";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                        break;
                }        
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose(); 
            }
            else
            {
                //库存商品/////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1235") == 0)  //库存商品
                    {
                        if (inoutflag == 1)   //入库
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + summny;
                        }
                        else   //出库
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + summny;
                        }
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "商品出入库";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "商品出入库合计";
                    row.Cells[4].Value = "";
                    if (inoutflag == 1)   //入库
                    {
                        row.Cells[5].Value = summny;
                        row.Cells[6].Value = 0;
                    }
                    else   //出库
                    {
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = summny;
                    }
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                //出入库科目//////////////////////////////////////////////////////////////////////
                exists = false;
                string subjectid = string.Empty;
                switch (inouttype)
                {
                    case "生产领料出库":
                        subjectid = "1225";                        
                        break;
                    case "生产商品入库":
                        subjectid = "1225";
                        break;
                    case "损耗出库":
                        subjectid = "91353082";
                        break;
                    case "溢余入库":
                        subjectid = "91353080";                        
                        break;
                    case "借出物出库":
                        subjectid = "1245";                        
                        break;
                    case "借出物入库":
                        subjectid = "1245";                        
                        break;
                    case "借入物出库":
                        subjectid = "5165";                        
                        break;
                    case "借入物入库":
                        subjectid = "5165";
                        break;
                }
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(subjectid) == 0)  //库存商品
                    {
                        switch (inouttype)
                        {
                            case "生产领料出库":
                            case "损耗出库":
                            case "借出物出库":
                            case "借入物出库":
                                DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + summny;
                                break;
                            case "生产商品入库":
                            case "溢余入库":
                            case "借出物入库":
                            case "借入物入库":
                                DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + summny;
                                break;                            
                        }
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "商品出入库";
                    row.Cells[4].Value = "";
                    switch (inouttype)
                    {
                        case "生产领料出库":
                            row.Cells[1].Value = "1225";
                            row.Cells[2].Value = "在建品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = summny;
                            row.Cells[6].Value = 0;
                            break;
                        case "生产商品入库":
                            row.Cells[1].Value = "1225";
                            row.Cells[2].Value = "在建品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = 0;
                            row.Cells[6].Value = summny;
                            break;
                        case "损耗出库":
                            row.Cells[1].Value = "91353082";
                            row.Cells[2].Value = "本年利润";
                            row.Cells[3].Value = "商品损耗";
                            row.Cells[5].Value = summny;
                            row.Cells[6].Value = 0;
                            break;
                        case "溢余入库":
                            row.Cells[1].Value = "91353080";
                            row.Cells[2].Value = "本年利润";
                            row.Cells[3].Value = "商品盈溢";
                            row.Cells[5].Value = 0;
                            row.Cells[6].Value = summny;
                            break;
                        case "借出物出库":
                            row.Cells[1].Value = "1245";
                            row.Cells[2].Value = "借出商品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = summny;
                            row.Cells[6].Value = 0;
                            break;
                        case "借出物入库":
                            row.Cells[1].Value = "1245";
                            row.Cells[2].Value = "借出商品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = 0;
                            row.Cells[6].Value = summny;
                            break;
                        case "借入物出库":
                            row.Cells[1].Value = "5165";
                            row.Cells[2].Value = "借入商品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = summny;
                            row.Cells[6].Value = 0;
                            break;
                        case "借入物入库":
                            row.Cells[1].Value = "5165";
                            row.Cells[2].Value = "借入商品";
                            row.Cells[3].Value = "分单记";
                            row.Cells[5].Value = 0;
                            row.Cells[6].Value = summny;
                            break;
                    }
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private bool ProductionItemAdd(modProductionForm mod)
        {
            DataGridViewRow row;
            if (mod.MaterialMny == 0)
            {
                MessageBox.Show("单据[" + mod.FormId + "]，成本价格为空，请先设置该单据的成本！", mod.FormId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            decimal mny = mod.MaterialMny;
            bool exists = false;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "生产入库";
                row.Cells[1].Value = "1235";
                row.Cells[2].Value = "库存商品";
                row.Cells[3].Value = "生产入库合计";
                row.Cells[4].Value = "";
                row.Cells[5].Value = mny;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                //库存商品/////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1235") == 0 && Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) != 0)  //库存商品
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "生产入库";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "生产入库合计";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }

            dalProductionForm dal = new dalProductionForm();
            decimal waremny = dal.GetWareMny(mod.FormId);
            exists = false;
            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1235") == 0 && Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) != 0)  //库存商品
                {
                    DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + waremny;
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "生产出库";
                row.Cells[1].Value = "1235";
                row.Cells[2].Value = "库存商品";
                row.Cells[3].Value = "生产出库合计";
                row.Cells[4].Value = "";
                row.Cells[5].Value = 0;
                row.Cells[6].Value = waremny;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }

            decimal processmny = (mod.ProcessMny + mod.OtherMny - mod.KillMny) * mod.ExchangeRate;
            if (processmny != 0)
            {
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("5155") == 0)  //其他应付款
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + processmny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "生产加工费";
                    row.Cells[1].Value = "5155";
                    row.Cells[2].Value = "其他应付款";
                    row.Cells[3].Value = mod.DeptId;
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = processmny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }

                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("913518") == 0)  //库存商品
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + processmny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "外发加工费";
                    row.Cells[1].Value = "913518";
                    row.Cells[2].Value = "加工制造成本";
                    row.Cells[3].Value = "";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = processmny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
            return true;
        }

        private void ExpenseFormItemAdd(modAccExpenseForm mod)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "费用登记";
                row.Cells[1].Value = mod.ExpenseId;
                row.Cells[2].Value = mod.ExpenseType;
                row.Cells[3].Value = mod.ExpenseName;
                row.Cells[4].Value = "";
                row.Cells[5].Value = mod.ExpenseMny * mod.ExchangeRate;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "付费用";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = mod.DetailId;
                row.Cells[4].Value = mod.DetailName;
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.ExpenseMny * mod.ExchangeRate;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.ExpenseId) == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.ExpenseMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "费用登记";
                    row.Cells[1].Value = mod.ExpenseId;
                    row.Cells[2].Value = mod.ExpenseType;
                    row.Cells[3].Value = mod.ExpenseName;
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.ExpenseMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                ////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.ExpenseMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付费用";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.ExpenseMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void ReceivableFormItemAdd(modAccReceivableForm mod)
        {
            DataGridViewRow row;            
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "收货款";
                row.Cells[1].Value = "1055";
                row.Cells[2].Value = "应收帐款";
                row.Cells[3].Value = "分单记";
                row.Cells[4].Value = "";
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.ReceivableMny * mod.ExchangeRate;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "收入客户货款";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = mod.DetailId;
                row.Cells[4].Value = mod.DetailName;
                row.Cells[5].Value = mod.GetMny * mod.ExchangeRate;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1055") == 0)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.ReceivableMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "收货款";
                    row.Cells[1].Value = "1055";
                    row.Cells[2].Value = "应收帐款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.ReceivableMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                ////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.GetMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "收入客户货款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = mod.GetMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void PayableFormItemAdd(modAccPayableForm mod)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "付货款";
                row.Cells[1].Value = "5145";
                row.Cells[2].Value = "应付帐款";
                row.Cells[3].Value = "分单记";
                row.Cells[4].Value = "";
                row.Cells[5].Value = mod.PayableMny * mod.ExchangeRate;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "付供应商货款";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = mod.DetailId;
                row.Cells[4].Value = mod.DetailName;
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.PaidMny * mod.ExchangeRate;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("5145") == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.PayableMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付货款";
                    row.Cells[1].Value = "5145";
                    row.Cells[2].Value = "应付帐款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.PayableMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                ////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.PaidMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付供应商货款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.PaidMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void OtherReceivableFormItemAdd(modAccOtherReceivableForm mod)
        {
            DataGridViewRow row;
            if (mod.AdFlag == -1)    //收入其它应收款
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "收款";
                    row.Cells[1].Value = "1060";
                    row.Cells[2].Value = "其它应收款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.ReceivableMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "收入其它应收款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = mod.GetMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1060") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.ReceivableMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "收款";
                        row.Cells[1].Value = "1060";
                        row.Cells[2].Value = "其它应收款";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.ReceivableMny * mod.ExchangeRate;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.GetMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "收入其它应收款";
                        row.Cells[1].Value = mod.SubjectId;
                        row.Cells[2].Value = mod.SubjectName;
                        row.Cells[3].Value = mod.DetailId;
                        row.Cells[4].Value = mod.DetailName;
                        row.Cells[5].Value = mod.GetMny * mod.ExchangeRate;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
            else    //ADFlag=1   借出其它应收款
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "借款";
                    row.Cells[1].Value = "1060";
                    row.Cells[2].Value = "其它应收款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.ReceivableMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "借出其它应收款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.GetMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1060") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.ReceivableMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "借款";
                        row.Cells[1].Value = "1060";
                        row.Cells[2].Value = "其它应收款";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = mod.ReceivableMny * mod.ExchangeRate;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.GetMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "借出其它应收款";
                        row.Cells[1].Value = mod.SubjectId;
                        row.Cells[2].Value = mod.SubjectName;
                        row.Cells[3].Value = mod.DetailId;
                        row.Cells[4].Value = mod.DetailName;
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.GetMny * mod.ExchangeRate;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
        }

        private void OtherPayableFormItemAdd(modAccOtherPayableForm mod)
        {
            DataGridViewRow row;
            if (mod.AdFlag == 1)    //借入其它应付款
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "借入款";
                    row.Cells[1].Value = "5155";
                    row.Cells[2].Value = "其它应付款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.PayableMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "借入其它应付款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = mod.PaidMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("5155") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.PayableMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "借入款";
                        row.Cells[1].Value = "5155";
                        row.Cells[2].Value = "其它应付款";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.PayableMny * mod.ExchangeRate;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.PaidMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "借入其它应付款";
                        row.Cells[1].Value = mod.SubjectId;
                        row.Cells[2].Value = mod.SubjectName;
                        row.Cells[3].Value = mod.DetailId;
                        row.Cells[4].Value = mod.DetailName;
                        row.Cells[5].Value = mod.PaidMny * mod.ExchangeRate;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
            else    //ADFlag=-1   付其它应收款
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付款";
                    row.Cells[1].Value = "5155";
                    row.Cells[2].Value = "其它应付款";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.PayableMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付其它应付款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = mod.DetailId;
                    row.Cells[4].Value = mod.DetailName;
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.PaidMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("5155") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.PayableMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "付款";
                        row.Cells[1].Value = "5155";
                        row.Cells[2].Value = "其它应付款";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = mod.PayableMny * mod.ExchangeRate;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == mod.DetailId)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.PaidMny * mod.ExchangeRate;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "付其它应付款";
                        row.Cells[1].Value = mod.SubjectId;
                        row.Cells[2].Value = mod.SubjectName;
                        row.Cells[3].Value = mod.DetailId;
                        row.Cells[4].Value = mod.DetailName;
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.PaidMny * mod.ExchangeRate;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
        }

        private void AccCheckFormItemAdd(modAccCheckForm mod)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "支票到期兑帐";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = "分单记";
                row.Cells[4].Value = "";
                if (mod.SubjectId == "1075")   //应收票据
                {
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.Mny * mod.ExchangeRate;
                }
                else
                {
                    row.Cells[5].Value = mod.Mny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                }
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                dalAccCheckForm dal = new dalAccCheckForm();
                BindingCollection<modAccCheckFormDetail> list = dal.GetDetail(mod.FormId, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccCheckFormDetail modd in list)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "支票承兑";
                        row.Cells[1].Value = modd.SubjectId;
                        row.Cells[2].Value = modd.SubjectName;
                        row.Cells[3].Value = modd.DetailId;
                        row.Cells[4].Value = modd.DetailName;
                        if (mod.SubjectId == "1075")   //应收票据
                        {
                            row.Cells[5].Value = modd.Mny * modd.ExchangeRate;
                            row.Cells[6].Value = 0;
                        }
                        else
                        {
                            row.Cells[5].Value = 0;
                            row.Cells[6].Value = modd.Mny * modd.ExchangeRate;                            
                        }
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0)
                    {
                        if (mod.SubjectId == "1075")   //应收票据
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.Mny * mod.ExchangeRate;
                        }
                        else
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.Mny * mod.ExchangeRate;
                        }
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "支票到期兑帐";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    if (mod.SubjectId == "1075")   //应收票据
                    {
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.Mny * mod.ExchangeRate;
                    }
                    else
                    {
                        row.Cells[5].Value = mod.Mny * mod.ExchangeRate;
                        row.Cells[6].Value = 0;
                    }
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                ////////////////////////////////////////////////////////////////////////
                dalAccCheckForm dal = new dalAccCheckForm();
                BindingCollection<modAccCheckFormDetail> list = dal.GetDetail(mod.FormId, out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccCheckFormDetail modd in list)
                    {
                        exists = false;
                        for (int i = 0; i < DBGrid.RowCount; i++)
                        {
                            if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(modd.SubjectId) == 0 && DBGrid.Rows[i].Cells[3].Value.ToString() == modd.DetailId)
                            {
                                if (mod.SubjectId == "1075")   //应收票据
                                {
                                    DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + modd.Mny * modd.ExchangeRate;
                                }
                                else
                                {
                                    DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + modd.Mny * modd.ExchangeRate;
                                }
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = "支票承兑";
                            row.Cells[1].Value = modd.SubjectId;
                            row.Cells[2].Value = modd.SubjectName;
                            row.Cells[3].Value = modd.DetailId;
                            row.Cells[4].Value = modd.DetailName;
                            if (mod.SubjectId == "1075")  //应收票据
                            {
                                row.Cells[5].Value = modd.Mny * modd.ExchangeRate;
                                row.Cells[6].Value = 0;
                            }
                            else
                            {
                                row.Cells[5].Value = 0;
                                row.Cells[6].Value = modd.Mny * modd.ExchangeRate;
                            }
                            row.Cells[7].Value = 1;
                            row.Cells[8].Value = Util.Currency;
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                    }
                }
            }
        }

        private void AssetAdd(modAssetAdd mod)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "增加资产";
                row.Cells[1].Value = "2120";
                row.Cells[2].Value = "固定资产净值";
                row.Cells[3].Value = "分单记";
                row.Cells[4].Value = "";
                row.Cells[5].Value = mod.SumMny * mod.ExchangeRate;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "增加固定资产";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = mod.DetailId;
                row.Cells[4].Value = mod.DetailName;
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.SumMny * mod.ExchangeRate;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("2120") == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.SumMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "增加资产";
                    row.Cells[1].Value = "2120";
                    row.Cells[2].Value = "固定资产净值";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.SumMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }

                ///////////////////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.SumMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "付资产费用";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.SumMny * mod.ExchangeRate;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }  
            }
        }

        private void AssetSale(modAssetSale mod)
        {
            bool exists = false;
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "资产处理";
                row.Cells[1].Value = "2120";
                row.Cells[2].Value = "固定资产净值";
                row.Cells[3].Value = "分单记";
                row.Cells[4].Value = "";
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.NetMny;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "处理固定资产";
                row.Cells[1].Value = mod.SubjectId;
                row.Cells[2].Value = mod.SubjectName;
                row.Cells[3].Value = mod.DetailId;
                row.Cells[4].Value = mod.DetailName;
                row.Cells[5].Value = mod.SaleMny * mod.ExchangeRate;
                row.Cells[6].Value = 0;
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("2120") == 0)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.NetMny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "资产处理";
                    row.Cells[1].Value = "2120";
                    row.Cells[2].Value = "固定资产净值";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.NetMny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }

                ///////////////////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo(mod.SubjectId) == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.SaleMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "固定资产处理货款";
                    row.Cells[1].Value = mod.SubjectId;
                    row.Cells[2].Value = mod.SubjectName;
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.SaleMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }                                
            }
            if (Math.Abs(mod.NetMny - mod.SaleMny * mod.ExchangeRate) >= Convert.ToDecimal("0.01"))
            {
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("91353070") == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.NetMny - mod.SaleMny * mod.ExchangeRate;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "固定资产处理折价";
                    row.Cells[1].Value = "91353070";
                    row.Cells[2].Value = "折旧费";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.NetMny - mod.SaleMny * mod.ExchangeRate;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void AssetEvaluate(modAssetEvaluate mod)
        {
            bool exists = false;
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                if (mod.NetMny - mod.EvaluateMny > 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "评估贬值";
                    row.Cells[1].Value = "2120";
                    row.Cells[2].Value = "固定资产净值";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.NetMny - mod.EvaluateMny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "固定资产评估损失";
                    row.Cells[1].Value = "913550";
                    row.Cells[2].Value = "营业外支出";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.NetMny - mod.EvaluateMny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "评估增值";
                    row.Cells[1].Value = "2120";
                    row.Cells[2].Value = "固定资产净值";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.EvaluateMny - mod.NetMny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "固定资产评估收益";
                    row.Cells[1].Value = "913545";
                    row.Cells[2].Value = "营业外收入";
                    row.Cells[3].Value = "分单记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.EvaluateMny - mod.NetMny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                if (mod.NetMny - mod.EvaluateMny > 0)
                {
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("2120") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.NetMny - mod.EvaluateMny;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "评估贬值";
                        row.Cells[1].Value = "2120";
                        row.Cells[2].Value = "固定资产净值";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.NetMny - mod.EvaluateMny;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }

                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("913550") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.NetMny - mod.EvaluateMny;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "固定资产评估损失";
                        row.Cells[1].Value = "913550";
                        row.Cells[2].Value = "营业外支出";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = mod.NetMny - mod.EvaluateMny;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
                else   //增值
                {
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("2120") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.EvaluateMny - mod.NetMny;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "评估增值";
                        row.Cells[1].Value = "2120";
                        row.Cells[2].Value = "固定资产净值";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = mod.EvaluateMny - mod.NetMny;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }

                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("913550") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.EvaluateMny - mod.NetMny;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "固定资产评估收益";
                        row.Cells[1].Value = "913545";
                        row.Cells[2].Value = "营业外收入";
                        row.Cells[3].Value = "分单记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.EvaluateMny - mod.NetMny;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }            
        }

        private void AssetDepre(modAssetDepreList mod)
        {
            DataGridViewRow row;
            if (DBGrid.RowCount == 0)
            {
                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "资产折旧";
                row.Cells[1].Value = "2120";
                row.Cells[2].Value = "固定资产净值";
                row.Cells[3].Value = "累计折旧";
                row.Cells[4].Value = "";
                row.Cells[5].Value = 0;
                row.Cells[6].Value = mod.DepreMny;                
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();

                row = new DataGridViewRow();
                row.CreateCells(DBGrid);
                row.Cells[0].Value = "固定资产折旧";
                row.Cells[1].Value = "91353070";
                row.Cells[2].Value = "管理费用";
                row.Cells[3].Value = "折旧费";
                row.Cells[4].Value = "";
                row.Cells[5].Value = mod.DepreMny;
                row.Cells[6].Value = 0;                
                row.Cells[7].Value = 1;
                row.Cells[8].Value = Util.Currency;
                row.Height = 40;
                DBGrid.Rows.Add(row);
                row.Dispose();
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////////////////////
                bool exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("2120") == 0)
                    {
                        DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.DepreMny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "资产折旧";
                    row.Cells[1].Value = "2120";
                    row.Cells[2].Value = "固定资产净值";
                    row.Cells[3].Value = "累计折旧";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.DepreMny;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }

                ///////////////////////////////////////////////////////////////////////////////////////
                exists = false;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("91353070") == 0)
                    {
                        DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.DepreMny;
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "固定资产折旧";
                    row.Cells[1].Value = "91353070";
                    row.Cells[2].Value = "管理费用";
                    row.Cells[3].Value = "折旧费";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.DepreMny;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
            }
        }

        private void PriceAdjustFormAdd(modPriceAdjustDetail mod)
        {
            DataGridViewRow row;
            if (mod.Differ > 0)  //溢余
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "价格调整";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "分明细记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = mod.Differ;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "调整产品成本价格";
                    row.Cells[1].Value = "91353080";
                    row.Cells[2].Value = "商品盈溢";
                    row.Cells[3].Value = "";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = mod.Differ;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1235") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) + mod.Differ;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.Cells[0].Value = "价格调整";
                        row.Cells[1].Value = "1235";
                        row.Cells[2].Value = "库存商品";
                        row.Cells[3].Value = "分明细记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = mod.Differ;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("91353080") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) + mod.Differ;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "调整产品成本价格";
                        row.Cells[1].Value = "91353080";
                        row.Cells[2].Value = "商品盈溢";
                        row.Cells[3].Value = "";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = mod.Differ;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
            else   //损耗
            {
                if (DBGrid.RowCount == 0)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "价格调整";
                    row.Cells[1].Value = "1235";
                    row.Cells[2].Value = "库存商品";
                    row.Cells[3].Value = "分明细记";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = 0;
                    row.Cells[6].Value = -mod.Differ;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();

                    row = new DataGridViewRow();
                    row.CreateCells(DBGrid);
                    row.Cells[0].Value = "调整产品成本价格";
                    row.Cells[1].Value = "91353082";
                    row.Cells[2].Value = "商品损耗";
                    row.Cells[3].Value = "";
                    row.Cells[4].Value = "";
                    row.Cells[5].Value = -mod.Differ;
                    row.Cells[6].Value = 0;
                    row.Cells[7].Value = 1;
                    row.Cells[8].Value = Util.Currency;
                    row.Height = 40;
                    DBGrid.Rows.Add(row);
                    row.Dispose();
                }
                else
                {
                    ///////////////////////////////////////////////////////////////////////////////////////
                    bool exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("1235") == 0)
                        {
                            DBGrid.Rows[i].Cells[6].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) - mod.Differ;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.Cells[0].Value = "价格调整";
                        row.Cells[1].Value = "1235";
                        row.Cells[2].Value = "库存商品";
                        row.Cells[3].Value = "分明细记";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = 0;
                        row.Cells[6].Value = -mod.Differ;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                    ////////////////////////////////////////////////////////////////////////
                    exists = false;
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        if (DBGrid.Rows[i].Cells[1].Value.ToString().CompareTo("91353082") == 0)
                        {
                            DBGrid.Rows[i].Cells[5].Value = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) - mod.Differ;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(DBGrid);
                        row.Cells[0].Value = "调整产品成本价格";
                        row.Cells[1].Value = "91353082";
                        row.Cells[2].Value = "商品损耗";
                        row.Cells[3].Value = "";
                        row.Cells[4].Value = "";
                        row.Cells[5].Value = -mod.Differ;
                        row.Cells[6].Value = 0;
                        row.Cells[7].Value = 1;
                        row.Cells[8].Value = Util.Currency;
                        row.Height = 40;
                        DBGrid.Rows.Add(row);
                        row.Dispose();
                    }
                }
            }
        }

        public void EditItem(string accname, int seq, bool isRead = false)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                if (isRead)
                    toolSave.Enabled = false;

                string emsg = string.Empty;
                FillControl.FillCredenceWord(cboCredenceWord, false, true);
                modAccCredenceList mod = _dal.GetItem(accname, seq, Util.IsTrialBalance, out Util.emsg);
                if (mod != null)
                {
					DBGrid.Rows.Clear();
					LoadDBGrid();
										                                        
                    BindingCollection<modAccCredenceDetail> list = _dal.GetCredenceDetail(accname, seq, Util.IsTrialBalance, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        foreach (modAccCredenceDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.Digest;
                            row.Cells[1].Value = modd.SubjectId;
                            row.Cells[2].Value = modd.SubjectName;
                            row.Cells[3].Value = modd.DetailId;
                            row.Cells[4].Value = modd.DetailName;
                            row.Cells[5].Value = modd.BorrowMoney.ToString();
                            row.Cells[6].Value = modd.LendMoney.ToString();                            
                            row.Cells[7].Value = modd.ExchangeRate.ToString();
                            row.Cells[8].Value = modd.Currency;
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                        GetSumMoney();
                    }

					lblCredenceType.Text = mod.CredenceType;
					txtSeq.Text = seq.ToString();
					dtpDate.Value = mod.CredenceDate;
					cboCredenceWord.SelectedValue = mod.CredenceWord;
					txtAttachCount.Text = mod.AttachCount.ToString();
					txtRemark.Text = mod.Remark;
					txtSeq.ReadOnly = true;
					if (mod.Status == 1 || Util.IsTrialBalance)
					{
						if (mod.Status == 1)
							status4.Image = Properties.Resources.audited;
						else
							status4.Image = null;
						toolSave.Enabled = false;
						cboCredenceWord.Enabled = false;
						txtAttachCount.ReadOnly = true;
						txtRemark.ReadOnly = true;
						dtpDate.Enabled = false;
						DBGrid.ReadOnly = true;
					}
					else
					{
						status4.Image = null;
						toolSave.Enabled = true;
						cboCredenceWord.Enabled = true;
						txtAttachCount.ReadOnly = false;
						txtRemark.ReadOnly = false;
						dtpDate.Enabled = true;
						DBGrid.ReadOnly = false;
					}
					//DBGrid.ReadOnly = true;
					switch (mod.CredenceType)
                    {
                        case "销售凭证":
                            dalSalesShipment dalss = new dalSalesShipment();
                            BindingCollection<modSalesShipment> listss = dalss.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listss;
                            break;
                        case "设计加工凭证":
                            dalSalesDesignForm dalsd = new dalSalesDesignForm();
                            BindingCollection<modSalesDesignForm> listsd = dalsd.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listsd;
                            break;
                        case "采购凭证":
                            dalPurchaseList dalpl = new dalPurchaseList();
                            BindingCollection<modPurchaseList> listpl = dalpl.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listpl;
                            break;                        
                        case "仓库进出":
                            dalWarehouseInoutForm dalio = new dalWarehouseInoutForm();
                            BindingCollection<modWarehouseInoutForm> listio = dalio.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listio;
                            break;
                        case "费用登记":
                            dalAccExpenseForm dalexp = new dalAccExpenseForm();
                            BindingCollection<modAccExpenseForm> listexp = dalexp.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listexp;
                            break;
                        case "收款凭证":
                            dalAccReceivableForm dalrec = new dalAccReceivableForm();
                            BindingCollection<modAccReceivableForm> listrec = dalrec.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listrec;
                            break;
                        case "付款凭证":
                            dalAccPayableForm dalpay = new dalAccPayableForm();
                            BindingCollection<modAccPayableForm> listpay = dalpay.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listpay;
                            break;
                        case "其它应收凭证":
                            dalAccOtherReceivableForm dalorec = new dalAccOtherReceivableForm();
                            BindingCollection<modAccOtherReceivableForm> listorec = dalorec.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listorec;
                            break;
                        case "其它应付凭证":
                            dalAccOtherPayableForm dalopay = new dalAccOtherPayableForm();
                            BindingCollection<modAccOtherPayableForm> listopay = dalopay.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listopay;
                            break;
                        case "生产凭证":
                            dalProductionForm dalpdt = new dalProductionForm();
                            BindingCollection<modProductionForm> listpdt = dalpdt.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listpdt;
                            break;
                        case "支票承兑":
                            dalAccCheckForm dalcheck = new dalAccCheckForm();
                            BindingCollection<modAccCheckForm> listcheck = dalcheck.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listcheck;
                            break;
                        case "固定资产增加":
                            dalAssetAdd dalassetadd = new dalAssetAdd();
                            BindingCollection<modAssetAdd> listassetadd = dalassetadd.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listassetadd;
                            break;
                        case "固定资产处理":
                            dalAssetSale dalassetsale = new dalAssetSale();
                            BindingCollection<modAssetSale> listassetsale = dalassetsale.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listassetsale;
                            break;
                        case "固定资产评估":
                            dalAssetEvaluate dalassetevaluate = new dalAssetEvaluate();
                            BindingCollection<modAssetEvaluate> listassetevaluate = dalassetevaluate.GetIList(mod.AccName, mod.AccSeq, out Util.emsg);
                            DBGridDetail.DataSource = listassetevaluate;
                            break;
                        case "资产折旧":
                            BindingCollection<modAssetDepreList> listdepre = _dal.GetDepreList(accname, out emsg);
                            DBGridDetail.DataSource = listdepre;
                            break;
                        case "价格调整":
                            dalPriceAdjustForm dalpaf = new dalPriceAdjustForm();
                            DBGridDetail.DataSource = dalpaf.GetCredenceList(mod.AccName, mod.AccSeq, out emsg);
                            break;
                        case "零库清理":
                            dalAccProductInout dalzero = new dalAccProductInout();
                            BindingCollection<modAccProductInout> listzero = dalzero.GetZeroProduct(Util.modperiod.AccName, out Util.emsg);
                            DBGridDetail.DataSource = listzero;
                            break;
                        case "月末结算":
                            dalAccProductInout dalinout1 = new dalAccProductInout();
                            BindingCollection<modAccProductInout> listinout1 = dalinout1.GetIList(Util.modperiod.AccName, seq, false, out Util.emsg);
                            DBGridDetail.DataSource = listinout1;
                            break;
                        case "试算平衡":
                            dalAccProductInout dalinout2 = new dalAccProductInout();
                            BindingCollection<modAccProductInout> listinout2 = dalinout2.GetIList(Util.modperiod.AccName, seq, true, out Util.emsg);
                            DBGridDetail.DataSource = listinout2;
                            break;
                        default:
                            DBGrid.ReadOnly = false;                            
                            if (mod.Status == 0)
                            {
                                DBGrid.ContextMenuStrip.Items.Add("-");
                                mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
                                mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
                            }
                            break;

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

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.EndEdit();
                if (dtpDate.Value < Util.modperiod.StartDate || dtpDate.Value > Util.modperiod.EndDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpDate.Focus();
                    return;
                }
                if (cboCredenceWord.SelectedValue==null || string.IsNullOrEmpty(cboCredenceWord.SelectedValue.ToString()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Credence word") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCredenceWord.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAttachCount.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Attach count") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAttachCount.Focus();
                    return;
                }
                else if (!Util.IsInt(txtAttachCount.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Attach count") + clsTranslate.TranslateString(" must be a integer!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAttachCount.Focus();
                    return;
                }
                else if (Convert.ToInt32(txtAttachCount.Text)<=0)
                {
                    int mincount = Convert.ToInt32(clsLxms.GetParameterValue("CREDENCE_ATTACHMENT_MIN_COUNT"));
                    if (mincount > 0)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Attach count") + clsTranslate.TranslateString(" must >0 !"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAttachCount.Focus();
                        return;
                    }
                }
                if (DBGrid.RowCount == 0)
                {
                    MessageBox.Show("没有明细数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (lblCredenceType.Text.Trim() == "月末结算")
                {
                    DBGridDetail.EndEdit();
                    AnalyzeBalance();
                }
                decimal sumborrow = 0;
                decimal sumlend = 0;
                for (int i = 0; i < DBGrid.RowCount; i++)
                {                    
                    if (DBGrid.Rows[i].Cells[1].Value==null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[1].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Subject id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (DBGrid.Rows[i].Cells[2].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[2].Value.ToString()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Subject name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    dalAccSubjectList dalsj = new dalAccSubjectList();
                    modAccSubjectList modsj = dalsj.GetItem(DBGrid.Rows[i].Cells[1].Value.ToString(), out Util.emsg);
                    if (modsj.CheckCurrency == 1)
                    {
                        dalAccBankAccount dalaccount = new dalAccBankAccount();
                        if (!dalaccount.Exists(DBGrid.Rows[i].Cells[3].Value.ToString(), out Util.emsg))
                        {
                            MessageBox.Show("【" + DBGrid.Rows[i].Cells[2].Value.ToString() + "】 现金银行明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (DBGrid.Rows[i].Cells[5].Value.ToString().Trim() == "0" && DBGrid.Rows[i].Cells[6].Value.ToString().Trim() == "0")
                    {
                        if(MessageBox.Show("借方和贷方金额为0,您真的要继续吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)==DialogResult.No) return;
                    }
                    if (DBGrid.Rows[i].Cells[5].Value == null)
                        DBGrid.Rows[i].Cells[5].Value = "0";
                    if (DBGrid.Rows[i].Cells[6].Value == null)
                        DBGrid.Rows[i].Cells[6].Value = "0";

                    sumborrow += Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);
                    sumlend += Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);
                    if (lblCredenceType.Text.CompareTo("一般凭证")==0)
                    {
                        switch (DBGrid.Rows[i].Cells[1].Value.ToString().Trim())
                        {
                            case "1055":    //应收帐款
                                dalCustomerList dalcust = new dalCustomerList();
                                if (!dalcust.Exists(DBGrid.Rows[i].Cells["DetailId"].Value.ToString().Trim(), out Util.emsg))
                                {
                                    MessageBox.Show("应收帐款明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                break;
                            case "5145":    //应付帐款
                                dalVendorList dalvendor = new dalVendorList();
                                if (!dalvendor.Exists(DBGrid.Rows[i].Cells["DetailId"].Value.ToString().Trim(), out Util.emsg))
                                {
                                    MessageBox.Show("应付帐款明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                break;
                            case "1060":    //其它应收款
                                dalOtherReceivableObject dalrec = new dalOtherReceivableObject();
                                if (!dalrec.Exists(DBGrid.Rows[i].Cells["DetailId"].Value.ToString().Trim(), out Util.emsg))
                                {
                                    MessageBox.Show("其它应收款明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                break;
                            case "5155":    //其它应付款
                                dalOtherPayableObject dalpay = new dalOtherPayableObject();
                                if (!dalpay.Exists(DBGrid.Rows[i].Cells["DetailId"].Value.ToString().Trim(), out Util.emsg))
                                {
                                    MessageBox.Show("其它应付款明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                break;                            
                        }                        
                    }
                }                
                if (Math.Abs(sumborrow - sumlend) > Convert.ToDecimal("0.001"))
                {
                    MessageBox.Show("借贷金额不平衡\r\n借方：[" + sumborrow.ToString() + "]\r\n贷方：[" + sumlend.ToString() + "]", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                modAccCredenceList mod = new modAccCredenceList();
                mod.AccName = Util.modperiod.AccName;
                mod.AccSeq = Convert.ToInt32(txtSeq.Text);
                mod.CredenceType = lblCredenceType.Text.Trim();
                mod.CredenceDate = dtpDate.Value;
                mod.CredenceWord = cboCredenceWord.SelectedValue.ToString();
                mod.AttachCount = Convert.ToInt32(txtAttachCount.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;

                string detaillist = string.Empty;
                BindingCollection<modAccCredenceDetail> list = new BindingCollection<modAccCredenceDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modAccCredenceDetail modd = new modAccCredenceDetail();
                    modd.DetailSeq = i + 1;
                    modd.Digest = DBGrid.Rows[i].Cells[0].Value==null?string.Empty : DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.SubjectId = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.SubjectName = DBGrid.Rows[i].Cells[2].Value.ToString().Replace(".", "").Trim();
                    modd.DetailId = DBGrid.Rows[i].Cells[3].Value == null ? string.Empty : DBGrid.Rows[i].Cells[3].Value.ToString().Replace(".", "").Trim();
                    modd.DetailName = DBGrid.Rows[i].Cells[4].Value == null ? string.Empty : DBGrid.Rows[i].Cells[4].Value.ToString().Replace(".", "").Trim();
                    modd.BorrowMoney = DBGrid.Rows[i].Cells[5].Value==null? 0 : Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.LendMoney = DBGrid.Rows[i].Cells[6].Value == null ? 0 : Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());
                    modd.ExchangeRate = Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value.ToString());
                    modd.Currency = DBGrid.Rows[i].Cells[8].Value.ToString();
                    list.Add(modd);
                }
                if (DBGridDetail.RowCount > 0)
                {
                    for (int i = 0; i < DBGridDetail.RowCount; i++)
                    {
                        if (i == 0)
                            detaillist = DBGridDetail.Rows[i].Cells[0].Value.ToString();
                        else
                            detaillist +=  "," + DBGridDetail.Rows[i].Cells[0].Value.ToString();
                    }
                }
                bool ret=false;
                if (mod.CredenceType == "月末结算")
                {
                    int rowidx = 0;                    
                    BindingCollection<modSalesShipmentCost> listbalance = (BindingCollection<modSalesShipmentCost>)DBGridDetail.DataSource;
                    foreach (modSalesShipmentCost modbalance in listbalance)
                    {
                        if (modbalance.CostPrice <= 0)
                        {
                            if (MessageBox.Show("产品 [" + modbalance.ProductName + "] 的成本单价为0, 您真的要结算吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                DBGridDetail.CurrentCell = DBGridDetail.Rows[rowidx].Cells["CostPrice"];                                
                                return;
                            }
                        }
                        if (modbalance.Profit < 0)
                        {
                            if (MessageBox.Show("产品 [" + modbalance.ProductName + "] 的毛利润为[" + modbalance.Profit + "], 您真的要结算吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                DBGridDetail.CurrentCell = DBGridDetail.Rows[rowidx].Cells["Profit"];
                                return;
                            }
                        }
                        rowidx++;
                    }
                    
                    if (MessageBox.Show("您真的要进行月末结算吗?", clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    ret = _dal.Save(_action, mod, list, detaillist, listbalance, out Util.emsg);
                }
                else if (mod.CredenceType == "试算平衡")
                {
                    int rowidx = 0;
                    BindingCollection<modSalesShipmentCost> listbalance = (BindingCollection<modSalesShipmentCost>)DBGridDetail.DataSource;
                    //foreach (modSalesShipmentCost modbalance in listbalance)
                    //{
                    //    if (modbalance.CostPrice <= 0)
                    //    {
                    //        if (MessageBox.Show("产品 [" + modbalance.ProductName + "] 的成本单价为0, 您真的要结算吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    //        {
                    //            DBGridDetail.CurrentCell = DBGridDetail.Rows[rowidx].Cells["CostPrice"];
                    //            return;
                    //        }
                    //    }
                    //    if (modbalance.Profit < 0)
                    //    {
                    //        if (MessageBox.Show("产品 [" + modbalance.ProductName + "] 的毛利润为[" + modbalance.Profit + "], 您真的要结算吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    //        {
                    //            DBGridDetail.CurrentCell = DBGridDetail.Rows[rowidx].Cells["Profit"];
                    //            return;
                    //        }
                    //    }
                    //    rowidx++;
                    //}

                    //if (MessageBox.Show("您真的要进行月末结算吗?", clsTranslate.TranslateString("Information"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    ret = _dal.SaveTrial(mod, list, detaillist, listbalance, out Util.emsg);
                }
                else if (mod.CredenceType == "资产折旧")
                {
                    BindingCollection<modAssetDepreList> listdepre = (BindingCollection<modAssetDepreList>)DBGridDetail.DataSource;
                    ret = _dal.SaveDepreList(listdepre, Util.modperiod.AccName, Util.UserId, out Util.emsg);
                    if (ret)
                    {
                        ret = _dal.Save(_action, mod, list, detaillist, null, out Util.emsg);
                    }
                }
                else
                    ret = _dal.Save(_action, mod, list, detaillist, null, out Util.emsg);
                if (ret)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Dispose();
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

        private void GetSumMoney()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal sumborrow = 0;
                decimal sumlend = 0;
                if (DBGrid.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        sumborrow += Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);
                        sumlend += Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[7].Value);
                    }
                }
                lblBorrow.Text = "借方合计： " + string.Format("{0:C2}", sumborrow);
                lblLend.Text = "贷方合计： " + string.Format("{0:C2}", sumlend);
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

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                GetSumMoney();
        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ACC_SUBJECT_LIST frm = new ACC_SUBJECT_LIST();
                frm.ShowHideVisible(true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (ACC_SUBJECT_LIST._mod.SubjectId.IndexOf("9135")>=0)
                    {
                        DataGridViewRow rowdef = new DataGridViewRow();
                        rowdef.CreateCells(DBGrid);
                        rowdef.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                        dalAccSubjectList dalsub = new dalAccSubjectList();
                        modAccSubjectList modsub = dalsub.GetItem(ACC_SUBJECT_LIST._mod.PSubjectId, out Util.emsg);
                        rowdef.Cells[2].Value = modsub.SubjectName;
                        rowdef.Cells[3].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                        rowdef.Cells[5].Value = "0";
                        rowdef.Cells[6].Value = "0";
                        rowdef.Cells[7].Value = 1;
                        rowdef.Cells[8].Value = Util.Currency;
                        rowdef.Height = 40;
                        DBGrid.Rows.Add(rowdef);
                        rowdef.Dispose();
                    }
                    else
                    {
                        switch (ACC_SUBJECT_LIST._mod.SubjectId)
                        {
                            case "1030":   //现金银行
                                ACC_BANK_ACCOUNT frm2 = new ACC_BANK_ACCOUNT();
                                frm2.ShowHideSelection(true);
                                if (frm2.ShowDialog() == DialogResult.OK)
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName.Replace(".","").Trim();
                                    row.Cells[3].Value = Util.retValue1;
                                    row.Cells[4].Value = string.Empty;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = "0";
                                    row.Cells[7].Value = clsLxms.GetExchangeRate(Util.retValue1);
                                    row.Cells[8].Value = Util.retValue2;
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            case "1055":    //应收帐款
                                MTN_CUSTOMER_LIST frmccust = new MTN_CUSTOMER_LIST();
                                frmccust.ShowHideSelection(true);
                                if (frmccust.ShowDialog() == DialogResult.OK)
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[3].Value = Util.retValue1;
                                    row.Cells[4].Value = Util.retValue2;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = "0";
                                    row.Cells[7].Value = 1;
                                    row.Cells[8].Value = Util.Currency;
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            case "5145":    //应付帐款
                                MTN_VENDOR_LIST frmvendor = new MTN_VENDOR_LIST();
                                frmvendor.SelectVisible = true;
                                if (frmvendor.ShowDialog() == DialogResult.OK)
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[3].Value = Util.retValue1;
                                    row.Cells[4].Value = Util.retValue2;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = "0";
                                    row.Cells[7].Value = 1;
                                    row.Cells[8].Value = Util.Currency;
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            case "1060":    //其它应收款
                                OTHER_RECEIVABLE_OBJECT frmor = new OTHER_RECEIVABLE_OBJECT();
                                frmor.SelectVisible = true;
                                if (frmor.ShowDialog() == DialogResult.OK)
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[3].Value = Util.retValue1;                                    
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = "0";
                                    row.Cells[7].Value = 1;
                                    row.Cells[8].Value = Util.Currency;
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            case "5155":    //其它应付款
                                OTHER_PAYABLE_OBJECT frmop = new OTHER_PAYABLE_OBJECT();
                                frmop.SelectVisible = true;
                                if (frmop.ShowDialog() == DialogResult.OK)
                                {
                                    DataGridViewRow row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[3].Value = Util.retValue1;                                    
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = "0";
                                    row.Cells[7].Value = 1;
                                    row.Cells[8].Value = Util.Currency;
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            default:
                                DataGridViewRow rowdef = new DataGridViewRow();
                                rowdef.CreateCells(DBGrid);
                                rowdef.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                rowdef.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                rowdef.Cells[3].Value = "";                                
                                rowdef.Cells[5].Value = "0";
                                rowdef.Cells[6].Value = "0";
                                rowdef.Cells[7].Value = 1;
                                rowdef.Cells[8].Value = Util.Currency;
                                rowdef.Height = 40;
                                DBGrid.Rows.Add(rowdef);
                                rowdef.Dispose();
                                break;
                        }
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
        
        private void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (DBGrid.CurrentRow == null) return;

                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to delete it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                DBGrid.Rows.RemoveAt(DBGrid.CurrentRow.Index);
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

        private void cboCredenceWord_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCredenceWord.SelectedValue == null) return;
            if (cboCredenceWord.SelectedValue.ToString().CompareTo("New...") != 0) return;

            ACC_CREDENCE_WORD frm = new ACC_CREDENCE_WORD();
            frm.ShowDialog();
            FillControl.FillCredenceWord(cboCredenceWord, false, true);
        }

        private void DBGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void DBGrid_ButtonSelectClick()
        {            
            ACC_COMMON_DIGEST frm = new ACC_COMMON_DIGEST();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DBGrid.CurrentCell.Value = Util.retValue1;
            }
        }

        private void DBGridDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (lblCredenceType.Text == "月末结算")
            {
                if (DBGridDetail.Columns[e.ColumnIndex].Name == "CostPrice" && e.RowIndex>=0)
                {
                    DBGridDetail.Rows[e.RowIndex].Cells["CostMny"].Value = Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["Size"].Value) * Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["Qty"].Value) * Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["CostPrice"].Value);
                    DBGridDetail.Rows[e.RowIndex].Cells["Profit"].Value = Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["SalesMny"].Value) - Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["CostMny"].Value);
                    if (Convert.ToDecimal(DBGridDetail.Rows[e.RowIndex].Cells["Profit"].Value) < 0)
                        DBGridDetail.Rows[e.RowIndex].Cells["Profit"].Style.ForeColor = Color.Red;
                    else
                        DBGridDetail.Rows[e.RowIndex].Cells["Profit"].Style.ForeColor = Color.Black;
                    AnalyzeBalance();
                }
            }
        }

        private void AnalyzeBalance()
        {
            decimal summny = 0;
            for (int i = 0; i < DBGridDetail.RowCount; i++)
            {
                summny += Convert.ToDecimal(DBGridDetail.Rows[i].Cells["Size"].Value.ToString()) * Convert.ToDecimal(DBGridDetail.Rows[i].Cells["Qty"].Value.ToString()) * Convert.ToDecimal(DBGridDetail.Rows[i].Cells["CostPrice"].Value.ToString());
            }

            for (int i = 0; i < DBGrid.RowCount; i++)
            {
                if (DBGrid.Rows[i].Cells[1].Value.ToString() == "913510")
                    DBGrid.Rows[i].Cells[5].Value = summny;
                else
                    DBGrid.Rows[i].Cells[6].Value = summny;
            }
        }

        private void EditAccCredenceList_Shown(object sender, EventArgs e)
        {
            if (lblCredenceType.Text == "月末结算")
            {
                if (DBGridDetail.RowCount > 0)
                {
                    for (int i = 0; i < DBGridDetail.RowCount; i++)
                    {
                        if (Convert.ToDecimal(DBGridDetail.Rows[i].Cells["Profit"].Value) < 0)
                            DBGridDetail.Rows[i].Cells["Profit"].Style.ForeColor = Color.Red;
                    }
                    DBGridDetail.Refresh();
                }
            }
        }
    }
}
