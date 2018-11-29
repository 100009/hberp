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
    public partial class ACC_START_DATA_FORM : Form
    {
        string _accname = string.Empty;
        public ACC_START_DATA_FORM()
        {
            InitializeComponent();
        }

        private void ACC_START_DATA_FORM_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            dalAccPeriodList dalp = new dalAccPeriodList();
            modAccPeriodList modp = dalp.GetItem(1, out Util.emsg);
            if (modp != null)
                _accname = modp.AccName;
            else
                _accname = Util.modperiod.AccName;

            FillControl.FillCurrency(cboCurrency1, false, true);
            FillControl.FillCurrency(cboCurrency2, false, true);
            FillControl.FillCurrency(cboCurrency3, false, true);
            FillControl.FillCurrency(cboCurrency4, false, true);
            FillControl.FillCurrency(cboCurrency5, false, true);
            FillControl.FillWarehouseList(cboWarehouse6, false);
            DBGrid6.ContextMenuStrip.Items.Add("-");
            DBGrid6.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("复制此行"), Properties.Resources.Copy, new System.EventHandler(this.mnuCopy6_Click));
            DBGrid6.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("删除"), Properties.Resources.Delete, new System.EventHandler(this.mnuDelete6_Click));
            FillControl.FillCheckSubject(cboSubject7, false);
            FillControl.FillCurrency(cboCurrency7, false, true);
            FillControl.FillCurrency(cboCurrency8, false, true);
                        
            LoadData6();
            LoadData9();
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Cash and Bank
        private void btnConfigure1_Click(object sender, EventArgs e)
        {
            ACC_BANK_ACCOUNT frm = new ACC_BANK_ACCOUNT();
            frm.ShowDialog();
            LoadData1();
        }

        private void btnRefresh1_Click(object sender, EventArgs e)
        {
            LoadData1();
        }

        private void LoadData1()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccCredenceList dal=new dalAccCredenceList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency1.SelectedItem;
                BindingCollection<modStartCashandBank> list = dal.GetStartCashandBank(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid1.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid1.ReadOnly = false;
                    for (int i = 0; i < DBGrid1.ColumnCount; i++)
                    {
                        DBGrid1.Columns[i].ReadOnly = true;
                    }
                    DBGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid1.Columns["BorrowMoney"].ReadOnly = false;
                    DBGrid1.Columns["LendMoney"].ReadOnly = false;
                    DBGrid1.Columns["BorrowMoney"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid1.Columns["LendMoney"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency1.SelectedValue == null) return;
            if (cboCurrency1.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData1();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency1, false, true);
            }
        }

        private void btnPost1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid1.EndEdit();
                dalAccCredenceList dal = new dalAccCredenceList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency1.SelectedItem;
                BindingCollection<modStartCashandBank> list = (BindingCollection<modStartCashandBank>)DBGrid1.DataSource;
                bool ret = dal.SaveStartCashandBank(_accname, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    ret = dal.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData1();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Receivable List
        private void btnConfigure2_Click(object sender, EventArgs e)
        {
            MTN_CUSTOMER_LIST frm = new MTN_CUSTOMER_LIST();
            frm.ShowDialog();
            LoadData2();
        }

        private void btnRefresh2_Click(object sender, EventArgs e)
        {
            LoadData2();
        }

        private void LoadData2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid2.EndEdit();
                dalAccReceivableList dal = new dalAccReceivableList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency2.SelectedItem;
                BindingCollection<modStartReceivable> list = dal.GetStartReceivable(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid2.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid2.ReadOnly = false;
                    for (int i = 0; i < DBGrid2.ColumnCount; i++)
                    {
                        DBGrid2.Columns[i].ReadOnly = true;
                    }
                    DBGrid2.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid2.Columns["StartMny"].ReadOnly = false;
                    DBGrid2.Columns["StartMny"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency2.SelectedValue == null) return;
            if (cboCurrency2.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData2();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency2, false, true);
            }
        }

        private void btnPost2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccReceivableList dal = new dalAccReceivableList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency2.SelectedItem;
                BindingCollection<modStartReceivable> list = (BindingCollection<modStartReceivable>)DBGrid2.DataSource;
                bool ret = dal.SaveStartReceivable(_accname, Util.modperiod.StartDate, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData2();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Payable List
        private void btnConfigure3_Click(object sender, EventArgs e)
        {
            MTN_VENDOR_LIST frm = new MTN_VENDOR_LIST();
            frm.ShowDialog();
            LoadData3();
        }

        private void btnRefresh3_Click(object sender, EventArgs e)
        {
            LoadData3();
        }

        private void LoadData3()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccPayableList dal = new dalAccPayableList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency3.SelectedItem;
                BindingCollection<modStartPayable> list = dal.GetStartPayable(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid3.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid3.ReadOnly = false;
                    for (int i = 0; i < DBGrid3.ColumnCount; i++)
                    {
                        DBGrid3.Columns[i].ReadOnly = true;
                    }
                    DBGrid3.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid3.Columns["StartMny"].ReadOnly = false;
                    DBGrid3.Columns["StartMny"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency3.SelectedValue == null) return;
            if (cboCurrency3.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData3();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency3, false, true);
            }
        }

        private void btnPost3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid3.EndEdit();
                dalAccPayableList dal = new dalAccPayableList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency3.SelectedItem;
                BindingCollection<modStartPayable> list = (BindingCollection<modStartPayable>)DBGrid3.DataSource;
                bool ret = dal.SaveStartPayable(_accname, Util.modperiod.StartDate, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData3();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Other Receivable List
        private void btnConfigure4_Click(object sender, EventArgs e)
        {
            OTHER_RECEIVABLE_OBJECT frm = new OTHER_RECEIVABLE_OBJECT();
            frm.ShowDialog();
            LoadData4();
        }

        private void btnRefresh4_Click(object sender, EventArgs e)
        {
            LoadData4();
        }

        private void LoadData4()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccOtherReceivable dal = new dalAccOtherReceivable();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency4.SelectedItem;
                BindingCollection<modStartOtherReceivable> list = dal.GetStartOtherReceivable(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid4.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid4.ReadOnly = false;
                    for (int i = 0; i < DBGrid4.ColumnCount; i++)
                    {
                        DBGrid4.Columns[i].ReadOnly = true;
                    }
                    DBGrid4.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid4.Columns["StartMny"].ReadOnly = false;
                    DBGrid4.Columns["StartMny"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency4.SelectedValue == null) return;
            if (cboCurrency4.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData4();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency4, false, true);
            }
        }

        private void btnPost4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid4.EndEdit();
                dalAccOtherReceivable dal = new dalAccOtherReceivable();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency4.SelectedItem;
                BindingCollection<modStartOtherReceivable> list = (BindingCollection<modStartOtherReceivable>)DBGrid4.DataSource;
                bool ret = dal.SaveStartOtherReceivable(_accname, Util.modperiod.StartDate, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData4();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Other Payable List
        private void btnConfigure5_Click(object sender, EventArgs e)
        {
            OTHER_PAYABLE_OBJECT frm = new OTHER_PAYABLE_OBJECT();
            frm.ShowDialog();
            LoadData5();
        }

        private void btnRefresh5_Click(object sender, EventArgs e)
        {
            LoadData5();
        }

        private void LoadData5()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccOtherPayable dal = new dalAccOtherPayable();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency5.SelectedItem;
                BindingCollection<modStartOtherPayable> list = dal.GetStartOtherPayable(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid5.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid5.ReadOnly = false;
                    for (int i = 0; i < DBGrid5.ColumnCount; i++)
                    {
                        DBGrid5.Columns[i].ReadOnly = true;
                    }
                    DBGrid5.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid5.Columns["StartMny"].ReadOnly = false;
                    DBGrid5.Columns["StartMny"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency5.SelectedValue == null) return;
            if (cboCurrency5.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData5();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency5, false, true);
            }
        }

        private void btnPost5_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid5.EndEdit();
                dalAccOtherPayable dal = new dalAccOtherPayable();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency5.SelectedItem;
                BindingCollection<modStartOtherPayable> list = (BindingCollection<modStartOtherPayable>)DBGrid5.DataSource;
                bool ret = dal.SaveStartOtherPayable(_accname, Util.modperiod.StartDate, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData5();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Product Inout
        private void btnConfigure6_Click(object sender, EventArgs e)
        {
            MTN_PRODUCT_LIST frm = new MTN_PRODUCT_LIST();
            frm.ShowDialog();
            LoadData6();
        }

        private void btnRefresh6_Click(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void cboWarehouse6_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData6();
        }

        private void mnuCopy6_Click(object sender, EventArgs e)
        {
            if (DBGrid6.CurrentRow == null) return;

            BindingCollection<modStartProductWip> list = (BindingCollection<modStartProductWip>)DBGrid6.DataSource;
            modStartProductWip mod = (modStartProductWip)DBGrid6.CurrentRow.DataBoundItem;
            modStartProductWip modnew = new modStartProductWip();
            modnew.ProductId = mod.ProductId;
            modnew.ProductName = mod.ProductName;
            modnew.WarehouseId = mod.WarehouseId;
            modnew.AccName = mod.AccName;
            modnew.Size = 0;
            modnew.StartMny = 0;
            modnew.StartQty = 0;
            if (DBGrid6.CurrentRow.Index < DBGrid6.RowCount - 1)
            {
                int curidx=DBGrid6.CurrentRow.Index;
                list.Insert(curidx + 1, modnew);
                DBGrid6.DataSource = list;
                DBGrid6.CurrentCell =DBGrid6.Rows[curidx + 1].Cells["Size"];
            }
            else
            {
                list.Add(modnew);
                DBGrid6.DataSource = list;
                DBGrid6.CurrentCell = DBGrid6.Rows[DBGrid6.RowCount - 1].Cells["Size"];
            }
            
        }

        private void mnuDelete6_Click(object sender, EventArgs e)
        {
            if (DBGrid6.CurrentRow == null) return;

            DBGrid6.Rows.RemoveAt(DBGrid6.CurrentRow.Index);
        }

        private void LoadData6()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboWarehouse6.SelectedValue == null) return;
                dalAccProductInout dal = new dalAccProductInout();
                BindingCollection<modStartProductWip> list = dal.GetStartProductWip(_accname, cboWarehouse6.SelectedValue.ToString(), out Util.emsg);
                DBGrid6.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid6.ReadOnly = false;
                    for (int i = 0; i < DBGrid6.ColumnCount; i++)
                    {
                        DBGrid6.Columns[i].ReadOnly = true;
                    }
                    DBGrid6.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid6.Columns["Size"].ReadOnly = false;
                    DBGrid6.Columns["StartQty"].ReadOnly = false;
                    DBGrid6.Columns["StartMny"].ReadOnly = false;
                    DBGrid6.Columns["Size"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid6.Columns["StartQty"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid6.Columns["StartMny"].DefaultCellStyle.ForeColor = Color.Red;
                    if (clsLxms.ShowProductSize() == 0)
                    {
                        DBGrid6.Columns["Size"].Visible = false;
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

        private void DBGrid6T_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["AccName"].Value = _accname;
            if(cboWarehouse6.SelectedValue!=null)
                e.Row.Cells["WarehouseId"].Value = cboWarehouse6.SelectedValue.ToString();
            e.Row.Cells["UnitNo"].Value = "pcs";
        }

        private void btnPost6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid6.EndEdit();
                dalAccProductInout dal = new dalAccProductInout();
                BindingCollection<modStartProductWip> list = (BindingCollection<modStartProductWip>)DBGrid6.DataSource;
                foreach (modStartProductWip mod in list)
                {
                    if (mod.StartQty <= 0 && mod.StartMny > 0)
                    {
                        MessageBox.Show("产品 [" + mod.ProductName + "] 的数量不能等于0", clsTranslate.TranslateString("Information"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                    if (mod.Size == 0 && mod.StartQty == 0)
                    {
                        MessageBox.Show("产品 [" + mod.ProductName + "] 的尺寸不能等于0", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                bool ret = dal.SaveStartProductWip(_accname, Util.modperiod.StartDate, cboWarehouse6.SelectedValue.ToString(), list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData6();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Account Check List
        private void btnRefresh7_Click(object sender, EventArgs e)
        {
            LoadData7();
        }

        private void LoadData7()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cboCurrency7.SelectedValue == null) return;
                if (cboSubject7.SelectedValue == null) return;
                dalAccCheckList dal = new dalAccCheckList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency7.SelectedItem;
                BindingCollection<modAccCheckList> list = dal.GetStartCheckList(_accname, cboSubject7.SelectedValue.ToString(), modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid7.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid7.ReadOnly = false;
                    for (int i = 0; i < DBGrid7.ColumnCount; i++)
                    {
                        DBGrid7.Columns[i].ReadOnly = false;
                    }
                    DBGrid7.Columns["Id"].ReadOnly = true;
                    DBGrid7.Columns["AccName"].ReadOnly = true;
                    DBGrid7.Columns["AccSeq"].ReadOnly = true;
                    DBGrid7.Columns["SubjectId"].ReadOnly = true;
                    DBGrid7.Columns["SubjectName"].ReadOnly = true;
                    DBGrid7.Columns["BankName"].ReadOnly = true;
                    DBGrid7.Columns["CheckType"].ReadOnly = true;
                    DBGrid7.Columns["FormId"].ReadOnly = true;
                    DBGrid7.Columns["FormType"].ReadOnly = true;
                    DBGrid7.Columns["Status"].ReadOnly = true;
                    DBGrid7.Columns["Currency"].ReadOnly = true;
                    DBGrid7.Columns["ExchangeRate"].ReadOnly = true;
                    DBGrid7.Columns["UpdateUser"].ReadOnly = true;
                    DBGrid7.Columns["UpdateTime"].ReadOnly = true;
                    
                    DBGrid7.Columns["Id"].Visible = false;
                    DBGrid7.Columns["AccName"].Visible = false;
                    DBGrid7.Columns["AccSeq"].Visible = false;
                    DBGrid7.Columns["Status"].Visible = false;
                    DBGrid7.Columns["GetDate"].Visible = false;
                    DBGrid7.Columns["Currency"].Visible = false;
                    DBGrid7.Columns["UpdateUser"].Visible = false;
                    DBGrid7.Columns["UpdateTime"].Visible = false;
                    DBGrid7.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid7.AllowUserToAddRows = true;
                    DBGrid7.AllowUserToDeleteRows = true;
                    string[] showcell = {"CheckType","BankName"};
                    DBGrid7.SetParam(showcell);
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

        private void DBGrid7_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Id"].Value = 0;
            e.Row.Cells["AccName"].Value = _accname;
            e.Row.Cells["AccSeq"].Value = 0;
            e.Row.Cells["SubjectId"].Value = cboSubject7.SelectedValue.ToString();
            e.Row.Cells["SubjectName"].Value = cboSubject7.Text.ToString();
            e.Row.Cells["FormType"].Value = "期初数据";
            e.Row.Cells["FormId"].Value = "0";
            e.Row.Cells["Currency"].Value = Util.Currency;
            e.Row.Cells["ExchangeRate"].Value = "1";
            e.Row.Cells["CreateDate"].Value = DateTime.Today;
            e.Row.Cells["PromiseDate"].Value = DateTime.Today.AddDays(7);
            e.Row.Cells["Status"].Value = 0;
            e.Row.Cells["UpdateUser"].Value = Util.UserId;
            e.Row.Cells["UpdateTime"].Value = DateTime.Now;
        }

        private void DBGrid7_ButtonSelectClick()
        {
            switch (DBGrid7.Columns[DBGrid7.CurrentCell.ColumnIndex].Name.ToLower())
            {
                case "checktype":
                    ACC_CHECK_TYPE frmchecktype = new ACC_CHECK_TYPE();
                    frmchecktype.SelectVisible = true;
                    if (frmchecktype.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid7.CurrentRow.Cells["CheckType"].Value = Util.retValue1;                        
                    }
                    break;
                case "bankname":
                    ACC_BANK_LIST frmbank = new ACC_BANK_LIST();
                    frmbank.SelectVisible = true;
                    if (frmbank.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid7.CurrentRow.Cells["BankName"].Value = Util.retValue1;                        
                    }
                    break;
            }            
        }

        private void cboCurrency7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency7.SelectedValue == null) return;
            if (cboCurrency7.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData7();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency7, false, true);
            }
        }

        private void btnPost7_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid7.EndEdit();
                if (DBGrid7.RowCount == 0) return;                
                dalAccCheckList dal = new dalAccCheckList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency7.SelectedItem;

                for (int i = 0; i < DBGrid7.RowCount; i++)
                {
                    if (DBGrid7.Rows[i].Cells[0].Value != null && !string.IsNullOrEmpty(DBGrid7.Rows[i].Cells[0].Value.ToString()))
                    {
                        if (DBGrid7.Rows[i].Cells["CheckNo"].Value==null && string.IsNullOrEmpty(DBGrid7.Rows[i].Cells["CheckNo"].Value.ToString()))
                        {
                            MessageBox.Show(clsTranslate.TranslateString("Check no") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (DBGrid7.Rows[i].Cells["BankName"].Value == null && string.IsNullOrEmpty(DBGrid7.Rows[i].Cells["BankName"].Value.ToString()))
                        {
                            MessageBox.Show(clsTranslate.TranslateString("Bank Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (DBGrid7.Rows[i].Cells["CheckType"].Value == null && string.IsNullOrEmpty(DBGrid7.Rows[i].Cells["CheckType"].Value.ToString()))
                        {
                            MessageBox.Show(clsTranslate.TranslateString("Check type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (DBGrid7.Rows[i].Cells["PromiseDate"].Value == null && string.IsNullOrEmpty(DBGrid7.Rows[i].Cells["PromiseDate"].Value.ToString()))
                        {
                            MessageBox.Show(clsTranslate.TranslateString("Promise Date") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (DBGrid7.Rows[i].Cells["Mny"].Value == null && string.IsNullOrEmpty(DBGrid7.Rows[i].Cells["Mny"].Value.ToString()))
                        {
                            MessageBox.Show(clsTranslate.TranslateString("Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                        {
                            if(!Util.IsNumeric(DBGrid7.Rows[i].Cells["Mny"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else if (Convert.ToDecimal(DBGrid7.Rows[i].Cells["Mny"].Value.ToString()) == 0)
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Mny") + clsTranslate.TranslateString(" must >0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
                BindingCollection<modAccCheckList> list = (BindingCollection<modAccCheckList>)DBGrid7.DataSource;
                bool ret = dal.SaveStartCheckList(_accname, cboSubject7.SelectedValue.ToString(), modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData7();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        #endregion

        #region Account Subject List
        private void btnRefresh8_Click(object sender, EventArgs e)
        {
            LoadData8();
        }

        private void LoadData8()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAccCredenceList dal = new dalAccCredenceList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency8.SelectedItem;
                BindingCollection<modStartSubjectData> list = dal.GetStartSubjectDetail(_accname, modcur.Currency, modcur.ExchangeRate, out Util.emsg);
                DBGrid8.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid8.ReadOnly = false;
                    for (int i = 0; i < DBGrid8.ColumnCount; i++)
                    {
                        DBGrid8.Columns[i].ReadOnly = true;
                    }
                    DBGrid8.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid8.Columns["BorrowMoney"].ReadOnly = false;
                    DBGrid8.Columns["LendMoney"].ReadOnly = false;
                    DBGrid8.Columns["BorrowMoney"].DefaultCellStyle.ForeColor = Color.Red;
                    DBGrid8.Columns["LendMoney"].DefaultCellStyle.ForeColor = Color.Red;
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

        private void cboCurrency8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency8.SelectedValue == null) return;
            if (cboCurrency8.SelectedValue.ToString().CompareTo("New...") != 0)
            {
                LoadData8();
            }
            else
            {
                ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
                frm.ShowDialog();
                FillControl.FillCurrency(cboCurrency8, false, true);
            }
        }

        private void btnPost8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid8.EndEdit();
                dalAccCredenceList dal = new dalAccCredenceList();
                modAccCurrencyList modcur = (modAccCurrencyList)cboCurrency8.SelectedItem;
                BindingCollection<modStartSubjectData> list = (BindingCollection<modStartSubjectData>)DBGrid8.DataSource;
                bool ret = dal.SaveStartSubjectData(_accname, modcur.Currency, modcur.ExchangeRate, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    ret = dal.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData8();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion

        #region Asset List
        private void btnRefresh9_Click(object sender, EventArgs e)
        {
            LoadData9();
        }

        private void LoadData9()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dalAssetList dal = new dalAssetList();
                BindingCollection<modAssetList> list = dal.GetStartAssetList(_accname, out Util.emsg);
                DBGrid9.DataSource = list;
                if (list == null && !string.IsNullOrEmpty(Util.emsg))
                {
                    MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    DBGrid9.ReadOnly = false;
                    for (int i = 0; i < DBGrid9.ColumnCount; i++)
                    {
                        DBGrid9.Columns[i].ReadOnly = false;
                    }
                    DBGrid9.Columns["AssetId"].ReadOnly = true;
                    //DBGrid9.Columns["AssetName"].ReadOnly = true;
                    //DBGrid9.Columns["AssetProperty"].ReadOnly = true;
                    DBGrid9.Columns["ControlDepart"].ReadOnly = true;
                    DBGrid9.Columns["UsingDepart"].ReadOnly = true;
                    //DBGrid9.Columns["SignDate"].ReadOnly = true;
                    //DBGrid9.Columns["PurchaseDate"].ReadOnly = true;
                    DBGrid9.Columns["DepreMethod"].ReadOnly = true;
                    //DBGrid9.Columns["DepreUnit"].ReadOnly = true;
                    //DBGrid9.Columns["RawQty"].ReadOnly = true;
                    DBGrid9.Columns["RawMny"].ReadOnly = true;
                    //DBGrid9.Columns["LastMny"].ReadOnly = true;
                    //DBGrid9.Columns["Remark"].ReadOnly = true;
                    DBGrid9.Columns["UpdateUser"].ReadOnly = true;
                    DBGrid9.Columns["UpdateTime"].ReadOnly = true;

                    DBGrid9.Columns["AssetId"].Visible = false;
                    DBGrid9.Columns["AccName"].Visible = false;
                    DBGrid9.Columns["AccSeq"].Visible = false;
                    DBGrid9.Columns["Status"].Visible = false;
                    DBGrid9.Columns["RawMny"].Visible = false;
                    DBGrid9.Columns["DepreMny"].Visible = false;
                    DBGrid9.Columns["UpdateUser"].Visible = false;
                    DBGrid9.Columns["UpdateTime"].Visible = false;
                    DBGrid9.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                    DBGrid9.AllowUserToAddRows = true;
                    DBGrid9.AllowUserToDeleteRows = true;
                    string[] showcell = { "ControlDepart", "UsingDepart", "DepreMethod" };
                    DBGrid9.SetParam(showcell);
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

        private void DBGrid9_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["AssetId"].Value = "0";
            e.Row.Cells["AccName"].Value = _accname;
            e.Row.Cells["AccSeq"].Value = 0;
            e.Row.Cells["SignDate"].Value = DateTime.Today;
            e.Row.Cells["PurchaseDate"].Value = DateTime.Today.AddDays(-7);
            e.Row.Cells["Status"].Value = 1;
            e.Row.Cells["DepreMethod"].Value = "平均年限法";
            e.Row.Cells["DepreUnit"].Value = "月";
            e.Row.Cells["Remark"].Value = "期初数据";
            e.Row.Cells["UpdateUser"].Value = Util.UserId;
            e.Row.Cells["UpdateTime"].Value = DateTime.Now;
        }

        private void DBGrid9_ButtonSelectClick()
        {
            switch (DBGrid9.Columns[DBGrid9.CurrentCell.ColumnIndex].Name.ToLower())
            {
                case "controldepart":
                    MTN_DEPT_LIST frmdept1 = new MTN_DEPT_LIST();
                    frmdept1.SelectVisible = true;
                    if (frmdept1.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid9.CurrentRow.Cells["ControlDepart"].Value = Util.retValue1;
                    }
                    break;
                case "usingdepart":
                    MTN_DEPT_LIST frmdept2 = new MTN_DEPT_LIST();
                    frmdept2.SelectVisible = true;
                    if (frmdept2.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid9.CurrentRow.Cells["UsingDepart"].Value = Util.retValue1;
                    }
                    break;
                case "depremethod":
                    frmSingleSelect frm = new frmSingleSelect();
                    frm.InitData("请选择折旧方式:", "平均年限法,双倍余额递减法,工作量法", ComboBoxStyle.DropDownList);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid9.CurrentRow.Cells["DepreMethod"].Value = Util.retValue1;
                    }
                    break;
            }
        }

        private void btnPost9_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid9.EndEdit();
                if (DBGrid9.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid9.RowCount; i++)
                    {
                        if (DBGrid9.Rows[i].Cells[0].Value != null && !string.IsNullOrEmpty(DBGrid9.Rows[i].Cells[0].Value.ToString()))
                        {
                            if (DBGrid9.Rows[i].Cells["AssetName"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["AssetName"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Asset Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["ControlDepart"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["ControlDepart"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Control Depart") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["DepreMethod"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["DepreMethod"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Depre Method") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["DepreUnit"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["DepreUnit"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Depre Unit") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["SignDate"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["SignDate"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Sign Date") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["PurchaseDate"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["PurchaseDate"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Purchase Date") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid9.Rows[i].Cells["NetMny"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["NetMny"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                if (!Util.IsNumeric(DBGrid9.Rows[i].Cells["NetMny"].Value.ToString()))
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else if (Convert.ToDecimal(DBGrid9.Rows[i].Cells["NetMny"].Value.ToString()) == 0)
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Net Mny") + clsTranslate.TranslateString(" must >0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            if (DBGrid9.Rows[i].Cells["LastMny"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["LastMny"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                if (!Util.IsNumeric(DBGrid9.Rows[i].Cells["LastMny"].Value.ToString()))
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else if (Convert.ToDecimal(DBGrid9.Rows[i].Cells["LastMny"].Value.ToString()) == 0)
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Last Mny") + clsTranslate.TranslateString(" must >0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            if (DBGrid9.Rows[i].Cells["RawQty"].Value == null && string.IsNullOrEmpty(DBGrid9.Rows[i].Cells["RawQty"].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                if (!Util.IsNumeric(DBGrid9.Rows[i].Cells["RawQty"].Value.ToString()))
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                else if (Convert.ToDecimal(DBGrid9.Rows[i].Cells["RawQty"].Value.ToString()) == 0)
                                {
                                    MessageBox.Show(clsTranslate.TranslateString("Raw Qty") + clsTranslate.TranslateString(" must >0!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }
                    }
                }
                BindingCollection<modAssetList> list = (BindingCollection<modAssetList>)DBGrid9.DataSource;
                dalAssetList dal = new dalAssetList();
                bool ret = dal.SaveStartAssetList(_accname, list, Util.UserId, out Util.emsg);
                if (ret)
                {
                    dalAccCredenceList dalcre = new dalAccCredenceList();
                    ret = dalcre.UpdateStart9165(_accname, out Util.emsg);
                    if (ret)
                    {
                        MessageBox.Show(clsTranslate.TranslateString("Post Success!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData7();
                    }
                    else
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #endregion
    }
}
