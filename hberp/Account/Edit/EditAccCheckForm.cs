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
    public partial class EditAccCheckForm : Form
    {
        string _action;
        dalAccCheckForm _dal = new dalAccCheckForm();
        INIClass ini = new INIClass(Util.INI_FILE);
        public EditAccCheckForm()
        {
            InitializeComponent();
            DBGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;            
        }

        private void EditAccCheckForm_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            txtFormId.ReadOnly = true;
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
                col.HeaderText = clsTranslate.TranslateString("SubjectId");
                col.DataPropertyName = "SubjectId";
                col.Name = "SubjectId";
                col.Width = 60;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();
                                
                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("SubjectName");
                col.DataPropertyName = "SubjectName";
                col.Name = "SubjectId";
                col.Width = 110;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("DetailId");
                col.DataPropertyName = "DetailId";
                col.Name = "DetailId";
                col.Width = 110;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("DetailName");
                col.DataPropertyName = "DetailName";
                col.Name = "DetailName";
                col.Width = 50;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Currency");
                col.DataPropertyName = "Currency";
                col.Name = "Currency";
                col.Width = 40;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Mny");
                col.DataPropertyName = "Mny";
                col.Name = "Mny";
                col.Width = 60;
                col.ReadOnly = false;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                col.HeaderText = clsTranslate.TranslateString("CheckNo");
                col.DataPropertyName = "CheckNo";
                col.Name = "CheckNo";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("CheckType");
                col.DataPropertyName = "CheckType";
                col.Name = "CheckType";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("BankName");
                col.DataPropertyName = "BankName";
                col.Name = "BankName";
                col.Width = 60;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = true;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("PromiseDate");
                col.DataPropertyName = "PromiseDate";
                col.Name = "PromiseDate";
                col.Width = 90;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                col = new DataGridViewTextBoxColumn();
                col.HeaderText = clsTranslate.TranslateString("Remark");
                col.DataPropertyName = "Remark";
                col.Name = "Remark";
                col.Width = 80;
                col.ReadOnly = false;
                DBGrid.Columns.Add(col);
                col.Dispose();

                string[] showcell = { "SubjectId", "CheckType", "BankName" };
                DBGrid.SetParam(showcell);

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

        private void DBGrid_ButtonSelectClick()
        {
            switch (DBGrid.Columns[DBGrid.CurrentCell.ColumnIndex].Name)
            {
                case "SubjectId":
                    EditItem(DBGrid.CurrentRow);
                    break;
                case "CheckType":
                    ACC_CHECK_TYPE frmct = new ACC_CHECK_TYPE();
                    frmct.SelectVisible = true;
                    if (frmct.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid.CurrentRow.Cells["CheckType"].Value = Util.retValue1;
                    }
                    break;
                case "BankName":
                    ACC_BANK_LIST frmbank = new ACC_BANK_LIST();
                    frmbank.SelectVisible = true;
                    if (frmbank.ShowDialog() == DialogResult.OK)
                    {
                        DBGrid.CurrentRow.Cells["BankName"].Value = Util.retValue1;
                    }
                    break;
            }
        }

        public void AddItem(string acctype)
        {
            _action = "NEW";
            DBGrid.ContextMenuStrip.Items.Add("-");
            mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
            mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
            //dtpFormDate.Value = DateTime.Today;
            dtpFormDate.Value = Util.modperiod.EndDate <= DateTime.Today ? Util.modperiod.EndDate : DateTime.Today;
            txtFormId.Text = _dal.GetNewId(dtpFormDate.Value).ToString();            
            txtMny.Text = "0";

            btnCheckId.Enabled = true;
            mnuNew.Enabled = true;
            mnuDelete.Enabled = true;
            LoadDBGrid();
            txtCheckId.ReadOnly = true;
            txtCheckNo.ReadOnly = true;
            txtCheckType.ReadOnly = true;
            txtCurrency.ReadOnly = true;
            txtMny.ReadOnly = true;
            txtExchangeRate.ReadOnly = true;
            txtBankName.ReadOnly = true;
            txtSubjectName.ReadOnly = true;
            status4.Image = null;
        }

        public void EditItem(string formid)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _action = "EDIT";
                DBGrid.ContextMenuStrip.Items.Add("-");
                mnuNew = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.OK, new System.EventHandler(this.mnuAdd_Click));
                mnuDelete = DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Delete"), LXMS.Properties.Resources.Delete, new System.EventHandler(this.mnuDelete_Click));
                modAccCheckForm mod = _dal.GetItem(formid, out Util.emsg);
                if (mod != null)
                {
                    txtFormId.Text = formid;
                    dtpFormDate.Value = mod.FormDate;
                    txtCheckId.Text = mod.CheckId.ToString();
                    txtSubjectName.Text = mod.SubjectName;
                    txtCheckNo.Text = mod.CheckNo;
                    txtCurrency.Text = mod.Currency;
                    txtMny.Text = mod.Mny.ToString();
                    txtExchangeRate.Text = mod.ExchangeRate.ToString();
                    txtBankName.Text = mod.BankName;
                    txtCheckType.Text = mod.CheckType;
                    txtRemark.Text = mod.Remark;
                    if (mod.Status == 1)
                    {
                        status4.Image = Properties.Resources.audited;
                        Util.ChangeStatus(this, true);
                        DBGrid.ReadOnly = true;
                        btnCheckId.Enabled = false;
                        mnuNew.Enabled = false;
                        mnuDelete.Enabled = false;
                        toolSave.Enabled = false;
                    }
                    else
                    {
                        status4.Image = null;
                        toolSave.Visible = true;
                        Util.ChangeStatus(this, false);
                        txtFormId.ReadOnly = true;
                        DBGrid.ReadOnly = false;
                        btnCheckId.Enabled = true;
                        mnuNew.Enabled = true;
                        mnuDelete.Enabled = true;
                        toolSave.Enabled = true;
                    }
                    txtCheckId.ReadOnly = true;
                    txtCheckNo.ReadOnly = true;
                    txtCheckType.ReadOnly = true;
                    txtCurrency.ReadOnly = true;
                    txtMny.ReadOnly = true;
                    txtExchangeRate.ReadOnly = true;
                    txtBankName.ReadOnly = true;
                    txtSubjectName.ReadOnly = true;
                    DBGrid.Rows.Clear();
                    LoadDBGrid();
                    BindingCollection<modAccCheckFormDetail> list = _dal.GetDetail(formid, out Util.emsg);
                    if (list == null && !string.IsNullOrEmpty(Util.emsg))
                    {
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        foreach (modAccCheckFormDetail modd in list)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DBGrid);
                            row.Cells[0].Value = modd.SubjectId;
                            row.Cells[1].Value = modd.SubjectName;
                            row.Cells[2].Value = modd.DetailId;
                            row.Cells[3].Value = modd.DetailName;
                            row.Cells[4].Value = modd.Currency;
                            row.Cells[5].Value = modd.Mny.ToString();
                            row.Cells[6].Value = modd.ExchangeRate.ToString();
                            row.Cells[7].Value = modd.CheckNo;
                            row.Cells[8].Value = modd.CheckType;
                            row.Cells[9].Value = modd.BankName;
                            row.Cells[10].Value = modd.PromiseDate.ToString("MM-dd-yyyy");
                            row.Cells[11].Value = modd.Remark;
                            row.Height = 40;
                            DBGrid.Rows.Add(row);
                            row.Dispose();
                        }
                        GetDetailSum();
                    }
                    //DBGrid.Enabled = true;
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

        private decimal GetDetailSum()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                decimal summny = 0;
                if (DBGrid.RowCount > 0)
                {
                    for (int i = 0; i < DBGrid.RowCount; i++)
                    {
                        summny += Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value) * Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value);

                    }
                }
                Status2.Text = "明细合计： " + string.Format("{0:C2}", summny);
                return summny;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DBGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGrid.EndEdit();
                if (dtpFormDate.Value < Util.modperiod.StartDate)
                {
                    MessageBox.Show("该日期的数据已锁定,不能更新数据!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFormDate.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCheckId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Check Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCheckId.Focus();
                    return;
                }
                if (DBGrid.RowCount == 0)
                {
                    MessageBox.Show("没有明细数据", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    switch (DBGrid.Rows[i].Cells[0].Value.ToString().Trim())
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
                            dalOtherReceivableObject dalpay = new dalOtherReceivableObject();
                            if (!dalpay.Exists(DBGrid.Rows[i].Cells["DetailId"].Value.ToString().Trim(), out Util.emsg))
                            {
                                MessageBox.Show("其它应付款明细不正确！", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            break;
                        case "1075":   //应收票据
                        case "5125":   //应付票据
                            if(DBGrid.Rows[i].Cells[7].Value==null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[7].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Check No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid.Rows[i].Cells[8].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[8].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Check Type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid.Rows[i].Cells[9].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[9].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Bank Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (DBGrid.Rows[i].Cells[10].Value == null || string.IsNullOrEmpty(DBGrid.Rows[i].Cells[10].Value.ToString()))
                            {
                                MessageBox.Show(clsTranslate.TranslateString("Promise Date") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            break;
                    }                    
                }
                if (Math.Abs(Convert.ToDecimal(txtMny.Text) - GetDetailSum()) > Convert.ToDecimal("0.001"))
                {
                    MessageBox.Show("金额不平衡!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                modAccCheckForm mod = new modAccCheckForm();
                mod.FormId = txtFormId.Text.Trim();
                mod.FormDate = dtpFormDate.Value;
                mod.CheckId =Convert.ToInt32(txtCheckId.Text);
                mod.Remark = txtRemark.Text.Trim();
                mod.UpdateUser = Util.UserId;
                mod.Status = 0;

                string detaillist = string.Empty;
                BindingCollection<modAccCheckFormDetail> list = new BindingCollection<modAccCheckFormDetail>();
                for (int i = 0; i < DBGrid.RowCount; i++)
                {
                    modAccCheckFormDetail modd = new modAccCheckFormDetail();
                    modd.Seq = i + 1;
                    modd.SubjectId = DBGrid.Rows[i].Cells[0].Value.ToString();
                    modd.SubjectName = DBGrid.Rows[i].Cells[1].Value.ToString();
                    modd.DetailId = DBGrid.Rows[i].Cells[2].Value == null ? string.Empty : DBGrid.Rows[i].Cells[2].Value.ToString();
                    modd.DetailName = DBGrid.Rows[i].Cells[3].Value.ToString();
                    modd.Currency = DBGrid.Rows[i].Cells[4].Value == null ? string.Empty : DBGrid.Rows[i].Cells[4].Value.ToString();
                    modd.Mny = Convert.ToDecimal(DBGrid.Rows[i].Cells[5].Value.ToString());
                    modd.ExchangeRate = Convert.ToDecimal(DBGrid.Rows[i].Cells[6].Value.ToString());
                    modd.CheckNo = DBGrid.Rows[i].Cells[7].Value == null ? string.Empty : DBGrid.Rows[i].Cells[7].Value.ToString();
                    modd.CheckType = DBGrid.Rows[i].Cells[8].Value == null ? string.Empty : DBGrid.Rows[i].Cells[8].Value.ToString();
                    modd.BankName = DBGrid.Rows[i].Cells[9].Value == null ? string.Empty : DBGrid.Rows[i].Cells[9].Value.ToString();
                    modd.PromiseDate = Convert.ToDateTime(DBGrid.Rows[i].Cells[10].Value);
                    modd.Remark = DBGrid.Rows[i].Cells[11].Value == null ? string.Empty : DBGrid.Rows[i].Cells[11].Value.ToString();
                    list.Add(modd);
                }
                bool ret = _dal.Save(_action, mod, list, out Util.emsg);
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

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ACC_SUBJECT_LIST frm = new ACC_SUBJECT_LIST();
                frm.ShowHideVisible(true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (ACC_SUBJECT_LIST._mod.SubjectId.IndexOf("9135") >= 0)
                    {
                        DataGridViewRow rowdef = new DataGridViewRow();
                        rowdef.CreateCells(DBGrid);
                        rowdef.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                        rowdef.Cells[1].Value = "本年利润";
                        rowdef.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                        rowdef.Cells[3].Value = "";
                        rowdef.Cells[4].Value = Util.Currency;
                        rowdef.Cells[5].Value = "0";
                        rowdef.Cells[6].Value = 1;
                        rowdef.Cells[7].Value = "";
                        rowdef.Cells[8].Value = "";
                        rowdef.Cells[9].Value = "";
                        rowdef.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                        rowdef.Height = 40;
                        DBGrid.Rows.Add(rowdef);
                        rowdef.Dispose();
                    }
                    else
                    {
                        DataGridViewRow row;
                        switch (ACC_SUBJECT_LIST._mod.SubjectId)
                        {
                            case "1030":   //现金银行
                                ACC_BANK_ACCOUNT frm2 = new ACC_BANK_ACCOUNT();
                                frm2.ShowHideSelection(true);
                                if (frm2.ShowDialog() == DialogResult.OK)
                                {
                                    row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName.Replace(".", "").Trim();
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value ="";
                                    row.Cells[4].Value = Util.retValue2;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = clsLxms.GetExchangeRate(Util.retValue1);
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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
                                    row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = Util.retValue2;
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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
                                    row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = Util.retValue2;
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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
                                    row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = "";
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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
                                    row = new DataGridViewRow();
                                    row.CreateCells(DBGrid);
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = "";
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[5].Value = "0";
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                    row.Height = 40;
                                    DBGrid.Rows.Add(row);
                                    row.Dispose();
                                }
                                break;
                            case "1075":    //应收票据
                            case "5125":    //应付票据
                                row = new DataGridViewRow();
                                row.CreateCells(DBGrid);
                                row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                row.Cells[2].Value = Util.retValue1;
                                row.Cells[3].Value = "";
                                row.Cells[4].Value = Util.Currency;
                                row.Cells[5].Value = "0";
                                row.Cells[6].Value = 1;
                                row.Cells[7].Value = "";
                                row.Cells[8].Value = "";
                                row.Cells[9].Value = "";
                                row.Cells[10].Value = DateTime.Today.AddDays(7).ToString("MM-dd-yyyy");
                                row.Height = 40;
                                DBGrid.Rows.Add(row);
                                row.Dispose();
                                break;
                            default:
                                row = new DataGridViewRow();
                                row.CreateCells(DBGrid);
                                row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                row.Cells[2].Value = "";
                                row.Cells[3].Value = "";
                                row.Cells[4].Value = Util.Currency;
                                row.Cells[5].Value = "0";
                                row.Cells[6].Value = 1;
                                row.Cells[7].Value = "";
                                row.Cells[8].Value = "";
                                row.Cells[9].Value = "";
                                row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                row.Height = 40;
                                DBGrid.Rows.Add(row);
                                row.Dispose();
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

        private void EditItem(DataGridViewRow row)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (row == null) return;
                ACC_SUBJECT_LIST frm = new ACC_SUBJECT_LIST();
                frm.ShowHideVisible(true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (ACC_SUBJECT_LIST._mod.SubjectId.IndexOf("9135") >= 0)
                    {
                        row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                        row.Cells[1].Value = "本年利润";
                        row.Cells[2].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                        row.Cells[3].Value = "";
                        row.Cells[4].Value = Util.retValue2;
                        row.Cells[6].Value = clsLxms.GetExchangeRate(Util.retValue1);
                        row.Cells[7].Value = "";
                        row.Cells[8].Value = "";
                        row.Cells[9].Value = "";
                        row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = "";
                                    row.Cells[4].Value = Util.retValue2;
                                    row.Cells[6].Value = clsLxms.GetExchangeRate(Util.retValue1);
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                }
                                break;
                            case "1055":    //应收帐款
                                MTN_CUSTOMER_LIST frmccust = new MTN_CUSTOMER_LIST();
                                frmccust.ShowHideSelection(true);
                                if (frmccust.ShowDialog() == DialogResult.OK)
                                {
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = Util.retValue2;
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                }
                                break;
                            case "5145":    //应付帐款
                                MTN_VENDOR_LIST frmvendor = new MTN_VENDOR_LIST();
                                frmvendor.SelectVisible = true;
                                if (frmvendor.ShowDialog() == DialogResult.OK)
                                {
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = Util.retValue2;
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                }
                                break;
                            case "1060":    //其它应收款
                                OTHER_RECEIVABLE_OBJECT frmor = new OTHER_RECEIVABLE_OBJECT();
                                frmor.SelectVisible = true;
                                if (frmor.ShowDialog() == DialogResult.OK)
                                {
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = "";
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                }
                                break;
                            case "5155":    //其它应付款
                                OTHER_PAYABLE_OBJECT frmop = new OTHER_PAYABLE_OBJECT();
                                frmop.SelectVisible = true;
                                if (frmop.ShowDialog() == DialogResult.OK)
                                {
                                    row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                    row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                    row.Cells[2].Value = Util.retValue1;
                                    row.Cells[3].Value = Util.retValue2;
                                    row.Cells[4].Value = Util.Currency;
                                    row.Cells[6].Value = 1;
                                    row.Cells[7].Value = "";
                                    row.Cells[8].Value = "";
                                    row.Cells[9].Value = "";
                                    row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
                                }
                                break;
                            case "1075":    //应收票据
                            case "5125":    //应付票据
                                row = new DataGridViewRow();
                                row.CreateCells(DBGrid);
                                row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                row.Cells[2].Value = "";
                                row.Cells[3].Value = "";
                                row.Cells[4].Value = Util.Currency;
                                row.Cells[6].Value = 1;
                                row.Cells[7].Value = "";
                                row.Cells[8].Value = "";
                                row.Cells[9].Value = "";
                                row.Cells[10].Value = DateTime.Today.AddDays(7).ToString("MM-dd-yyyy");
                                break;
                            default:
                                row.Cells[0].Value = ACC_SUBJECT_LIST._mod.SubjectId;
                                row.Cells[1].Value = ACC_SUBJECT_LIST._mod.SubjectName;
                                row.Cells[2].Value = "";
                                row.Cells[3].Value = "";
                                row.Cells[4].Value = Util.Currency;
                                row.Cells[6].Value = 1;
                                row.Cells[7].Value = "";
                                row.Cells[8].Value = "";
                                row.Cells[9].Value = "";
                                row.Cells[10].Value = DateTime.Today.ToString("MM-dd-yyyy");
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

        private void btnCheckId_Click(object sender, EventArgs e)
        {
            dalAccCheckList dal = new dalAccCheckList();
            BindingCollection<modAccCheckList> list = dal.GetWaitingCredenceList(string.Empty, out Util.emsg);
            if (list != null)
            {
                frmViewList frm = new frmViewList();
                frm.InitViewList("请选择要承兑的支票:", list);
                frm.Selection = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    modAccCheckList mod = dal.GetItem(Convert.ToInt32(Util.retValue1), out Util.emsg);
                    txtCheckId.Text = mod.Id.ToString();
                    txtSubjectName.Text = mod.SubjectName;
                    txtCheckNo.Text = mod.CheckNo;
                    txtCheckType.Text = mod.CheckType;
                    txtBankName.Text = mod.BankName;
                    txtCurrency.Text = mod.Currency;
                    txtMny.Text = mod.Mny.ToString();
                    txtExchangeRate.Text = mod.ExchangeRate.ToString();
                }
            }
        }

        private void dtpFormDate_ValueChanged(object sender, EventArgs e)
        {
            if (_action == "NEW")
            {
                txtFormId.Text = _dal.GetNewId(dtpFormDate.Value);
            }
        }
    }
}
