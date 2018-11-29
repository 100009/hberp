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
    public partial class ACC_CREDENCE_LIST : Form
    {
        dalAccCredenceList _dal = new dalAccCredenceList();
        public ACC_CREDENCE_LIST()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
        }

        private void ACC_CREDENCE_LIST_Load(object sender, EventArgs e)
        {            
            //if (Util.modperiod.LockFlag == 1 || Util.IsTrialBalance)
            //{
            //    toolNew.Visible = false;
            //    toolEdit.Visible = false;
            //    toolDel.Visible = false; 
            //    toolAudit.Visible = false;
            //    toolReset.Visible = false;
            //    toolTrial.Visible = false;
            //    toolBalance.Visible = false;
            //}
            FillControl.FillPeriodList(cboAccName.ComboBox);
        }

        private void ACC_CREDENCE_LIST_Shown(object sender, EventArgs e)
        {
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
        private void cboAccName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboAccName.SelectedIndex == -1) return;
                DBGrid.toolCancelFrozen_Click(null, null);

                modAccPeriodList modPeriod = (modAccPeriodList)cboAccName.SelectedItem;
                BindingCollection<modAccCredenceList> list = _dal.GetIList(modPeriod.AccName, string.Empty, Util.IsTrialBalance, out Util.emsg);
                DBGrid.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    AddComboBoxColumns();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].Status == 1)
                            DBGrid.Rows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                    if (modPeriod.LockFlag == 1 || Util.IsTrialBalance)
                    {
                        toolNew.Visible = false;
                        toolEdit.Visible = false;
                        toolDel.Visible = false;
                        toolAudit.Visible = false;
                        toolReset.Visible = false;
                        toolTrial.Visible = false;
                        toolBalance.Visible = false;
                    }
                    else
                    {
                        toolNew.Visible = true;
                        toolEdit.Visible = true;
                        toolDel.Visible = true;
                        toolAudit.Visible = true;
                        toolReset.Visible = true;
                        toolTrial.Visible = true;
                        toolBalance.Visible = true;
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(2);
            DBGrid.Columns.Insert(2, checkboxColumn);
        }

        private void toolNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmSingleSelect frmsingle = new frmSingleSelect();
                frmsingle.InitData("请选择记帐凭证类别", "一般凭证,销售凭证,设计加工凭证,采购凭证,仓库进出,费用登记,收款凭证,付款凭证,其它应收凭证,其它应付凭证,生产凭证,支票承兑,固定资产增加,固定资产处理,固定资产评估,资产折旧,零库清理,价格调整,利润分配", ComboBoxStyle.DropDownList);
                if (frmsingle.ShowDialog() == DialogResult.OK)
                {
                    frmSelectGrid frmsel = new frmSelectGrid();
                    EditAccCredenceList frm = new EditAccCredenceList();
                    switch (Util.retValue1)
                    {
                        case "销售凭证":                            
                            dalSalesShipment dalss = new dalSalesShipment();
                            BindingCollection<modSalesShipment> listss = dalss.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listss != null && listss.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listss);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "设计加工凭证":
                            dalSalesDesignForm dalsd = new dalSalesDesignForm();
                            BindingCollection<modSalesDesignForm> listsd = dalsd.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listsd != null && listsd.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listsd);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "采购凭证":
                            dalPurchaseList dalpc = new dalPurchaseList();
                            BindingCollection<modPurchaseList> listpc = dalpc.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listpc != null && listpc.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listpc);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;                        
                        case "费用登记":
                            dalAccExpenseForm dalexp = new dalAccExpenseForm();
                            BindingCollection<modAccExpenseForm> listexp = dalexp.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listexp != null && listexp.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listexp);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "收款凭证":
                            dalAccReceivableForm dalrec = new dalAccReceivableForm();
                            BindingCollection<modAccReceivableForm> listrec = dalrec.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listrec != null && listrec.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listrec);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "付款凭证":
                            dalAccPayableForm dalpay = new dalAccPayableForm();
                            BindingCollection<modAccPayableForm> listpay = dalpay.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listpay != null && listpay.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listpay);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "其它应收凭证":
                            dalAccOtherReceivableForm dalorec = new dalAccOtherReceivableForm();
                            BindingCollection<modAccOtherReceivableForm> listorec = dalorec.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listorec != null && listorec.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listorec);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "其它应付凭证":
                            dalAccOtherPayableForm dalopay = new dalAccOtherPayableForm();
                            BindingCollection<modAccOtherPayableForm> listopay = dalopay.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listopay != null && listopay.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listopay);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "仓库进出":
                            dalWarehouseInoutForm dalio = new dalWarehouseInoutForm();
                            BindingCollection<modWarehouseInoutForm> listio = dalio.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listio != null && listio.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listio);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "生产凭证":
                            dalProductionForm dalpdt = new dalProductionForm();
                            BindingCollection<modProductionForm> listpdt = dalpdt.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listpdt != null && listpdt.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listpdt);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "支票承兑":
                            dalAccCheckForm dalcheck = new dalAccCheckForm();
                            BindingCollection<modAccCheckForm> listcheck = dalcheck.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listcheck != null && listcheck.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listcheck);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "固定资产增加":
                            dalAssetAdd dalassetadd = new dalAssetAdd();
                            BindingCollection<modAssetAdd> listassetadd = dalassetadd.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listassetadd != null && listassetadd.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listassetadd);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "固定资产处理":
                            dalAssetSale dalassetsale = new dalAssetSale();
                            BindingCollection<modAssetSale> listassetsale = dalassetsale.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listassetsale != null && listassetsale.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listassetsale);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "固定资产评估":
                            dalAssetEvaluate dalassetevaluate = new dalAssetEvaluate();
                            BindingCollection<modAssetEvaluate> listassetevaluate = dalassetevaluate.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listassetevaluate != null && listassetevaluate.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listassetevaluate);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "价格调整":
                            dalPriceAdjustForm dalpaf = new dalPriceAdjustForm();
                            BindingCollection<modPriceAdjustForm> listpaf = dalpaf.GetWaitCredenceList(Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                            if (listpaf != null && listpaf.Count > 0)
                            {
                                frmsel.InitViewList(Util.retValue1, listpaf);
                                if (frmsel.ShowDialog() == DialogResult.OK)
                                {
                                    if (frm.AddItem(Util.retValue1, frmSelectGrid.selectionlist))
                                    {
                                        if (frm.ShowDialog() == DialogResult.OK)
                                        {
                                            LoadData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Util.emsg))
                                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("没有找到相应的数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case "零库清理":
                            if (frm.AddItem(Util.retValue1, string.Empty))
                            {
                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    LoadData();
                                }
                            }
                            break;
						case "利润分配":
							frm.AddItem(Util.retValue1, string.Empty);
							if (frm.ShowDialog() == DialogResult.OK)
							{
								LoadData();
							}
							break;
                        default:                            
                            frm.AddItem(Util.retValue1, string.Empty);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                LoadData();
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

        private void toolEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modAccCredenceList mod = (modAccCredenceList)DBGrid.CurrentRow.DataBoundItem;
                EditAccCredenceList frm = new EditAccCredenceList();
                frm.EditItem(mod.AccName, mod.AccSeq);
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
                modAccCredenceList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), Convert.ToInt32(DBGrid.CurrentRow.Cells[1].Value), false, out Util.emsg);
                if (mod.Status == 1)
                {
                    MessageBox.Show("该凭证已审核，您不能删除！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool ret = _dal.Save("DEL", mod, null, string.Empty, null, out Util.emsg);
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

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow == null) return;
            modAccCredenceList mod = (modAccCredenceList)DBGrid.CurrentRow.DataBoundItem;
            if (mod.Status == 0)
            {
                toolDel.Enabled = true;
                toolAudit.Enabled = true;
                toolReset.Enabled = false;
            }
            else
            {
                toolDel.Enabled = false;
                toolAudit.Enabled = false;
                toolReset.Enabled = true;
            }
        }

        private void toolAudit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;
                if (DBGrid.SelectedRows.Count == 0 && DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to audit it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (DBGrid.SelectedRows.Count == 0)
                {
                    modAccCredenceList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), Convert.ToInt32(DBGrid.CurrentRow.Cells[1].Value), false, out Util.emsg);
                    if (mod.Status == 0 && _dal.Audit(mod.AccName, mod.AccSeq, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Audit Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBGrid.CurrentRow.Cells["status"].Value = 1;
                        DBGrid.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
                        DBGrid.CurrentRow.Selected = false;
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modAccCredenceList mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), Convert.ToInt32(DBGrid.SelectedRows[i].Cells[1].Value), false, out Util.emsg);
                        if (mod.Status == 0 && _dal.Audit(mod.AccName, mod.AccSeq, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 1;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.DarkGray;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }                
                toolDel.Enabled = false;
                toolAudit.Enabled = false;
                toolReset.Enabled = true;
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

        private void toolReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.RowCount == 0) return;
                if (DBGrid.SelectedRows.Count == 0 && DBGrid.CurrentRow == null) return;
                if (MessageBox.Show(clsTranslate.TranslateString("Do you really want to reset it?"), clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                if (DBGrid.SelectedRows.Count == 0)
                {
                    modAccCredenceList mod = _dal.GetItem(DBGrid.CurrentRow.Cells[0].Value.ToString(), Convert.ToInt32(DBGrid.CurrentRow.Cells[1].Value), false, out Util.emsg);
                    if (mod.Status == 1 && _dal.Reset(mod.AccName, mod.AccSeq, Util.UserId, out Util.emsg))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Reset Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DBGrid.CurrentRow.Cells["status"].Value = 0;
                        DBGrid.CurrentRow.DefaultCellStyle.ForeColor = Color.Black;
                        DBGrid.CurrentRow.Selected = false;
                    }
                    else
                    {
                        MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    for (int i = DBGrid.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        modAccCredenceList mod = _dal.GetItem(DBGrid.SelectedRows[i].Cells[0].Value.ToString(), Convert.ToInt32(DBGrid.SelectedRows[i].Cells[1].Value), false, out Util.emsg);
                        if (mod.Status == 1 && _dal.Reset(mod.AccName, mod.AccSeq, Util.UserId, out Util.emsg))
                        {
                            DBGrid.SelectedRows[i].Cells["status"].Value = 0;
                            DBGrid.SelectedRows[i].DefaultCellStyle.ForeColor = Color.Black;
                            DBGrid.SelectedRows[i].Selected = false;
                        }
                        else
                        {
                            MessageBox.Show(clsTranslate.TranslateString(Util.emsg), mod.AccSeq.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }                
                toolDel.Enabled = true;
                toolAudit.Enabled = true;
                toolReset.Enabled = false;
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
            modAccCredenceList mod = (modAccCredenceList)DBGrid.CurrentRow.DataBoundItem;
            list.Add(new modExcelRangeData("单据共 " + mod.AttachCount.ToString() + " 张", "A3", "A3"));
            list.Add(new modExcelRangeData(mod.CredenceDate.ToString("yyyy年MM月dd日"), "B3", "C3"));
            list.Add(new modExcelRangeData("总 字第 " + mod.AccSeq.ToString().Trim() + " 号", "E3", "E3"));
            decimal borrowsum = 0;
            decimal lendsum = 0;
            BindingCollection<modAccCredenceDetail> listdetail = _dal.GetCredenceDetail(mod.AccName, mod.AccSeq, Util.IsTrialBalance, out Util.emsg);
            for (int i = 0; i < listdetail.Count; i++)
            {
                modAccCredenceDetail modd = listdetail[i];
                string col = (6 + i).ToString().Trim();
                list.Add(new modExcelRangeData(modd.Digest, "A" + col, "A" + col));
                list.Add(new modExcelRangeData(modd.SubjectName, "B" + col, "B" + col));
                list.Add(new modExcelRangeData(modd.DetailId, "C" + col, "C" + col));
                if (modd.BorrowMoney != 0)
                {
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.BorrowMoney), "D" + col, "D" + col));
                    borrowsum += modd.BorrowMoney;
                }
                if (modd.LendMoney != 0)
                {
                    list.Add(new modExcelRangeData(string.Format("{0:C2}", modd.LendMoney), "E" + col, "E" + col));
                    lendsum += modd.LendMoney;
                }
            }
            list.Add(new modExcelRangeData(string.Format("{0:C2}", borrowsum), "D14", "D14"));
            list.Add(new modExcelRangeData(string.Format("{0:C2}", lendsum), "E14", "E14"));
            list.Add(new modExcelRangeData(mod.UpdateUser, "D16", "D16"));
            clsExport.ExportByTemplate(list, "记帐凭证", 1, 16, 6, 1);
        }

        private void toolBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Util.emsg = string.Empty;
                dalAccReport dal = new dalAccReport();                
                BindingCollection<modWaitingAuditList> list = dal.GetWaitingAuditList(Util.modperiod.AccName, Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                if (list != null && list.Count > 0)
                {
                    MessageBox.Show("您本月还有未完成单据,不能结算!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmViewList frmvl = new frmViewList();
                    frmvl.InitViewList(clsTranslate.TranslateString("Waiting Audit List"), list);
                    frmvl.ShowDialog();
                    return;
                }
                BindingCollection<modAccountBalance> list2 = dal.GetAccountBalance(Util.modperiod.AccName, Util.IsTrialBalance, out Util.emsg);
                if (list2 != null && list2.Count > 0)
                {
                    foreach (modAccountBalance mod in list2)
                    {
                        if (Math.Abs(mod.Differ) >= Convert.ToDecimal("0.5"))
                        {
                            MessageBox.Show("财务数据不平衡，请先联系程序员检查原因并修正错误!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmViewList frmvl = new frmViewList();
                            frmvl.InitViewList(clsTranslate.TranslateString("Account Balance"), list2);
                            frmvl.ShowDialog();
                            return;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EditAccCredenceList frm = new EditAccCredenceList();
                frm.AddItem("月末结算", string.Empty);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Util.IsTrialBalance = false;
                    LoadData();
                    EditAccPeriodList frmapl = new EditAccPeriodList();
                    frmapl.InitForm(Util.modperiod.EndDate.AddDays(1));
                    frmapl.AddItem(Util.UserId);
                    if (frmapl.ShowDialog() == DialogResult.OK)
                    {
                        Util.modperiod.LockFlag = 1;
                        dalAccAnalyzeProfit dalprofit = new dalAccAnalyzeProfit();
                        dalprofit.Generate(Util.modperiod.AccName, Util.IsTrialBalance, out Util.emsg);

                        dalAccAnalyzeSales dalsales = new dalAccAnalyzeSales();
                        dalsales.Generate(Util.modperiod.AccName, out Util.emsg);

                        dalAccAnalyzePurchase dalpur = new dalAccAnalyzePurchase();
                        dalpur.Generate(Util.modperiod.AccName, out Util.emsg);

                        dalAccAnalyzeWaste dalwaste = new dalAccAnalyzeWaste();
                        dalwaste.Generate(Util.modperiod.AccName, out Util.emsg);

                        dalAccAnalyzeProduct dalpdt = new dalAccAnalyzeProduct();
                        dalpdt.Generate(Util.modperiod.AccName, out Util.emsg);
                        Application.Exit();
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

        private void toolTrial_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Util.emsg = string.Empty;
                dalAccReport dal = new dalAccReport();
                BindingCollection<modWaitingAuditList> list = dal.GetWaitingAuditList(Util.modperiod.AccName, Util.modperiod.StartDate.ToString(), Util.modperiod.EndDate.ToString(), out Util.emsg);
                if (list != null && list.Count > 0)
                {                    
                    frmViewList frmvl = new frmViewList();
                    frmvl.InitViewList(clsTranslate.TranslateString("Waiting Audit List"), list);
                    frmvl.ShowDialog();
                    if(MessageBox.Show("您本月还有未完成单据,您是否要试算？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.No)
                    return;
                }
                BindingCollection<modAccountBalance> list2 = dal.GetAccountBalance(Util.modperiod.AccName, false, out Util.emsg);
                if (list2 != null && list2.Count > 0)
                {
                    foreach (modAccountBalance mod in list2)
                    {
                        if (mod.SubjectId!="1030" && Math.Abs(mod.Differ) > Convert.ToDecimal("1"))
                        {
                            MessageBox.Show("财务数据不平衡，请先联系程序员检查原因并修正错误!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmViewList frmvl = new frmViewList();
                            frmvl.InitViewList(clsTranslate.TranslateString("Account Balance"), list2);
                            frmvl.ShowDialog();
                            return;
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                EditAccCredenceList frm = new EditAccCredenceList();
                frm.AddItem("试算平衡", string.Empty);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Util.IsTrialBalance = true;
                    LoadData();
                    MessageBox.Show("试算操作成功，您现在查到的财务报表是您试算之后的数据！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmMain frmmain = (frmMain)this.ParentForm;
                    ((Label)frmmain.Controls.Find("lblTrialBalance", true).First()).Visible = true;
                    toolNew.Visible = false;
                    toolEdit.Visible = false;
                    toolDel.Visible = false;
                    toolAudit.Visible = false;
                    toolReset.Visible = false;
                    toolTrial.Visible = false;
                    toolBalance.Visible = false;
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
