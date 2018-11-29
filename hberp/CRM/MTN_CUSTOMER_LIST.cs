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
    public partial class MTN_CUSTOMER_LIST : BaseFormEdit
    {
        dalCustomerList _dal = new dalCustomerList();
        public MTN_CUSTOMER_LIST()
        {
            InitializeComponent();
            clsTranslate.InitLanguage(this);
            SelectVisible = false;
        }

        private void MTN_CUSTOMER_LIST_Load(object sender, EventArgs e)
        {
            DBGrid.ContextMenuStrip.Items.Add("-");
            DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Customer Log"), LXMS.Properties.Resources.largeLogs, new System.EventHandler(this.mnuCustomerLog_Click));

            DBGridTrace.ContextMenuStrip.Items.Add("-");
            DBGridTrace.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("New"), LXMS.Properties.Resources.new_med, new System.EventHandler(this.mnuAddTrace_Click));
            DBGridTrace.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Edit"), LXMS.Properties.Resources.Edit, new System.EventHandler(this.mnuEditTrace_Click));
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillCustomerLevel(cboCustLevel, false);
            FillControl.FillCustomerType(cboCustType, false, true);
            FillControl.FillEmployeeList(cboSalesMan, false, true);
            FillControl.FillShipmentTemplete(cboShipmentTemplete, false);
            FillControl.FillCurrency(cboCurrency, false, true);
            //SelectVisible = false;
            DBGrid.Tag = this.Text;
            DBGridTrace.Tag = this.Text+"Trace";
            LoadTree();
        }

        private void LoadTree()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                tvLeft.AllowDrop = true;
                tvLeft.Nodes.Clear();
                tvLeft.ImageList = Util.GetImageList();
                
                tvLeft.Nodes.Add("ALL", "全部的客户", 0, 1);
                tvLeft.Nodes.Add("SHIPMENT30", "30天内有送货的客户", 0, 1);
                tvLeft.Nodes.Add("SHIPMENT60", "60天内有送货的客户", 0, 1);
                tvLeft.Nodes.Add("SAMPLE15", "15天内有取样的客户", 0, 1);
                tvLeft.Nodes.Add("QUOTATION15", "15天内有报价的客户", 0, 1);
                tvLeft.Nodes.Add("NEWCUST07", "7天内新增的客户", 0, 1);
                tvLeft.Nodes.Add("NEWCUST15", "15天内新增的客户", 0, 1);
                tvLeft.Nodes.Add("LOST30", "30天未联系的客户", 0, 1);
                tvLeft.Nodes.Add("LOST90", "90天未联系的客户", 0, 1);
                tvLeft.SelectedNode = tvLeft.Nodes[0];
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

        private void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (tvLeft.SelectedNode == null) return;
                BindingCollection<modCustomerList> list = _dal.GetIList(tvLeft.SelectedNode.Name, string.Empty, true, Util.UserId, out Util.emsg);
                DBGrid.DataSource = list;
                DBGrid.Enabled = true;
                if (list != null && list.Count > 0)
                {
                    AddComboBoxColumns();                        
                    Status1 = DBGrid.Rows.Count.ToString();
                    Status2 = clsTranslate.TranslateString("Refresh");
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
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

        private void AddComboBoxColumns()
        {
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Status");
            checkboxColumn.DataPropertyName = "Status";
            checkboxColumn.Name = "Status";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(3);
            DBGrid.Columns.Insert(3, checkboxColumn);
        }

        protected override void Find()
        {
            if (DBGrid.CurrentRow == null) return;

            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                DBGrid.Rows[i].Visible = true;
            }
            int startindex = 0;
            if (DBGrid.CurrentRow.Index < DBGrid.RowCount - 1)
                startindex = DBGrid.CurrentRow.Index + 1;

            string[] finds = FindText.ToUpper().Split('*');
            string flag = clsLxms.GetParameterValue("HIDE_NOT_MATCH_PRODUCT");
            if (flag == "F")   //不隐藏不匹配的
            {
                bool find = false;
                for (int i = startindex; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;
                    modCustomerList mod = (modCustomerList)DBGrid.Rows[i].DataBoundItem;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (mod.FullName.IndexOf(finds[j]) < 0)
                        {
                            found = false;
                        }
                    }
                    if (found)
                    {
                        DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                        DBGrid_SelectionChanged(null, null);
                        find = true;
                        return;
                    }
                }
                if (!find)
                {
                    for (int i = 0; i < DBGrid.Rows.Count; i++)
                    {
                        bool found = true;
                        modCustomerList mod = (modCustomerList)DBGrid.Rows[i].DataBoundItem;
                        for (int j = 0; j < finds.Length; j++)
                        {
                            if (mod.FullName.IndexOf(finds[j]) < 0)
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                            DBGrid_SelectionChanged(null, null);
                            find = true;
                            return;
                        }
                    }
                }
            }
            else   //隐藏不匹配的
            {
                DBGrid.CurrentCell = null;
                for (int i = 0; i < DBGrid.Rows.Count; i++)
                {
                    bool found = true;
                    modCustomerList mod = (modCustomerList)DBGrid.Rows[i].DataBoundItem;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (mod.FullName.IndexOf(finds[j]) < 0)
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                        DBGrid.Rows[i].Visible = true;
                    else
                        DBGrid.Rows[i].Visible = false;
                }
            }
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells["custid"].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells["custname"].Value.ToString();
            Util.retValue3 = DBGrid.CurrentRow.Cells["currency"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            DBGridTrace.Enabled = false;
            Util.EmptyFormBox(this);
            cboStatus.SelectedIndex = 1;
            cboNeedInvoice.SelectedIndex = -1;
            cboCustType.SelectedIndex = -1;
            cboSalesMan.SelectedValue = Util.UserId;
            txtCustId.Text = _dal.GetCustId();
            cboCustLevel.SelectedIndex = 0;
            cboCurrency.SelectedIndex = 0;
            DBGridTrace.DataSource = null;
            cboShipmentTemplete.SelectedValue = "SalesShipment";            
            txtFullName.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            DBGridTrace.Enabled = false;
            txtCustId.ReadOnly = true;
            cboCurrency.Enabled = false;
            txtCustName.Focus();
        }

        protected override bool Delete()
        {
            if (DBGrid.CurrentRow == null) return false;
            modCustomerList mod = (modCustomerList)DBGrid.CurrentRow.DataBoundItem;
            bool ret = _dal.Delete(mod, out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtCustId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("cust id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustId.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtCustName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("cust name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return false;
                }                
                if (cboCustType.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust type") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCustType.Focus();
                    return false;
                }
                if (cboCustLevel.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust level") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCustLevel.Focus();
                    return false;
                }
                if (cboCurrency.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Currency") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboCurrency.Focus();
                    return false;
                }
                if (cboNeedInvoice.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Need invoice") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboNeedInvoice.Focus();
                    return false;
                }    
                if (string.IsNullOrEmpty(txtCustName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Cust name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCustName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Full name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtFullName.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtNo.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Ship no") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return false;
                }
                if(Util.IsChina(txtNo.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("No") + clsTranslate.TranslateString(" can not be chinese!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNo.Focus();
                    return false;
                }
                if (cboSalesMan.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Sales man") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboSalesMan.Focus();
                    return false;
                }
                if(!string.IsNullOrEmpty(txtEMail.Text.Trim()))
                {
                    if(!Util.IsEmailAddr(txtEMail.Text.Trim()))
                    {
                        MessageBox.Show(clsTranslate.TranslateString("EMail") + clsTranslate.TranslateString(" is not a valid one!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtEMail.Focus();
                        return false;
                    }
                }
                int? status = cboStatus.SelectedIndex;
                modCustomerList mod = new modCustomerList();
                mod.CustId = txtCustId.Text.Trim();
                mod.CustName = txtCustName.Text.Trim();
                mod.FullName = txtFullName.Text.Trim();
                mod.Status = status;
                mod.NeedInvoice = cboNeedInvoice.SelectedIndex;
                mod.CustType = cboCustType.SelectedValue.ToString();
                mod.CustLevel = cboCustLevel.SelectedValue.ToString();
                mod.No = txtNo.Text.Trim().Replace("*","");
                mod.Currency = cboCurrency.SelectedValue.ToString();
                mod.Linkman = txtLinkman.Text.Trim();
                mod.Tel = txtTel.Text.Trim();
                mod.Fax = txtFax.Text.Trim();
                mod.Addr = txtAddr.Text.Trim();
                mod.ShipAddr = txtShipmentAddr.Text.Trim();
                mod.EMail = txtEMail.Text.Trim();
                mod.QQ = txtQQ.Text.Trim();
                mod.Remark = txtRemark.Text.Trim();
                mod.Incorporator = txtIncorporator.Text.Trim();
                mod.ProductType = txtProductType.Text.Trim();
                mod.CompanySize = txtCompanySize.Text.Trim();
                mod.PayMethod = txtPaymentMethod.Text.Trim();
                mod.CheckAccountDate = txtCheckAccountDate.Text.Trim();
                mod.AccountBank = txtAccountBank.Text.Trim();
                mod.AccountNo = txtAccountNo.Text.Trim();
                mod.SalesMan = cboSalesMan.SelectedValue.ToString();
                mod.ShipmentTemplete = cboShipmentTemplete.SelectedValue.ToString();
                mod.UpdateUser = Util.UserId;

                bool ret = false;
                if (_status == 1)
                {
                    if(_dal.Exists(mod, out Util.emsg))
                    {
                        if (MessageBox.Show(Util.emsg + "\r\n\r\n您要继续添加此客户吗？", clsTranslate.TranslateString("Confirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            ret = _dal.Insert(mod, out Util.emsg);
                    }
                    else
                        ret = _dal.Insert(mod, out Util.emsg);
                }
                else if (_status == 2)
                    ret = _dal.Update(txtCustId.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    DBGridTrace.Enabled = true;
                    tvLeft.SelectedNode = tvLeft.Nodes[0];
                    LoadData();
                    FindText = mod.FullName;
                    //Find();
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void Cancel()
        {
            Util.ChangeStatus(this, true);
            DBGrid.Enabled = true;
            DBGridTrace.Enabled = true;
            DBGrid_SelectionChanged(null, null);
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtCustId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtCustId.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modCustomerList mod = (modCustomerList)DBGrid.CurrentRow.DataBoundItem;
                txtCustId.Text = mod.CustId;
                txtCustName.Text = mod.CustName;
                txtFullName.Text = mod.FullName;
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboCustType.SelectedValue = mod.CustType;
                cboCustLevel.SelectedValue = mod.CustLevel;
                txtNo.Text = mod.No;
                cboCurrency.SelectedValue = mod.Currency;
                txtLinkman.Text = mod.Linkman;
                txtTel.Text = mod.Tel;
                txtFax.Text = mod.Fax;
                txtAddr.Text = mod.Addr;
                txtEMail.Text = mod.EMail;
                txtQQ.Text = mod.QQ;
                txtRemark.Text = mod.Remark;
                txtShipmentAddr.Text = mod.ShipAddr;
                txtIncorporator.Text = mod.Incorporator;
                txtProductType.Text = mod.ProductType;
                txtCompanySize.Text = mod.CompanySize;
                txtPaymentMethod.Text = mod.PayMethod;
                txtCheckAccountDate.Text = mod.CheckAccountDate;
                txtAccountBank.Text = mod.AccountBank;
                txtAccountNo.Text = mod.AccountNo;
                cboSalesMan.SelectedValue = mod.SalesMan;
                cboNeedInvoice.SelectedIndex = mod.NeedInvoice;
                cboShipmentTemplete.SelectedValue = mod.ShipmentTemplete;
                //FindText = mod.CustId;
                LoadTraceInfo(mod.CustId);
            }
            else
            {
                Util.EmptyFormBox(this);
                DBGridTrace.DataSource = null;
            }
        }

        private void LoadTraceInfo(string custid)
        {
            dalCustomerLog dallog = new dalCustomerLog();
            BindingCollection<modCustomerLog> listlog = dallog.GetIList(custid, string.Empty, string.Empty, string.Empty, "1", string.Empty, string.Empty, out Util.emsg);
            DBGridTrace.DataSource = listlog;
        }

        private void txtCustId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void txtCustName_Validated(object sender, EventArgs e)
        {
            if (txtCustName.ReadOnly) return;
            if(!string.IsNullOrEmpty(txtCustName.Text.Trim()))
            {
                txtNo.Text = Util.GetChineseSpell(txtCustName.Text.Trim());
            }
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if(SelectVisible)
                Select();
        }

        private void cboCustType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustType.SelectedValue == null) return;
            if (cboCustType.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_CUSTOMER_TYPE frm = new MTN_CUSTOMER_TYPE();
            frm.ShowDialog();
            FillControl.FillCustomerType(cboCustType, false, true);
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurrency.SelectedValue == null) return;
            if (cboCurrency.SelectedValue.ToString().CompareTo("New...") != 0) return;
            
            ACC_CURRENCY_LIST frm = new ACC_CURRENCY_LIST();
            frm.ShowDialog();
            FillControl.FillCurrency(cboCurrency, false, true);            
        }

        private void cboSalesMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSalesMan.SelectedValue == null) return;
            if (cboSalesMan.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_EMPLOYEE_LIST frm = new MTN_EMPLOYEE_LIST();
            frm.ShowDialog();
            FillControl.FillEmployeeList(cboSalesMan, false, true);
        }

        private void mnuCustomerLog_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modCustomerList mod = (modCustomerList)DBGrid.CurrentRow.DataBoundItem;
                dalCustomerLog dallog = new dalCustomerLog();
                BindingCollection<modCustomerLog> listlog = dallog.GetIList(mod.CustId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, out Util.emsg);
                if (listlog != null && listlog.Count > 0)
                {
                    frmViewList frm = new frmViewList();
                    frm.InitViewList(clsTranslate.TranslateString("Customer Log"), listlog);
                    frm.ShowDialog();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Util.emsg))
                        MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(clsTranslate.TranslateString("No data found!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void mnuAddTrace_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGrid.CurrentRow == null) return;
                modCustomerList mod = (modCustomerList)DBGrid.CurrentRow.DataBoundItem;
                EditCustomerLog frm = new EditCustomerLog();
                frm.AddItem(mod.CustId, mod.CustName);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTraceInfo(mod.CustId);
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

        private void mnuEditTrace_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (DBGridTrace.CurrentRow == null) return;
                modCustomerLog mod = (modCustomerLog)DBGridTrace.CurrentRow.DataBoundItem;
                EditCustomerLog frm = new EditCustomerLog();
                frm.EditItem(mod);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadTraceInfo(mod.CustId);
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

        private void DBGridTrace_DoubleClick(object sender, EventArgs e)
        {
            mnuEditTrace_Click(null, null);
        }
    }
}
