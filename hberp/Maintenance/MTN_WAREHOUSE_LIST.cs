﻿using System;
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
    public partial class MTN_WAREHOUSE_LIST : BaseFormEdit
    {
        dalWarehouseList _dal = new dalWarehouseList();
        public MTN_WAREHOUSE_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_WAREHOUSE_LIST_Load(object sender, EventArgs e)
        {
            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillStatus(cboStatus, false);
            FillControl.FillYesNo(cboDefault, false);
            DBGrid.Tag = this.Text;
            LoadData();
        }

        private void LoadData()
        {
            BindingCollection<modWarehouseList> list = _dal.GetIList(false, out Util.emsg);
            DBGrid.DataSource = list;
            DBGrid.Enabled = true;
            if (list != null)
            {
                AddComboBoxColumns();                
                Status1 = DBGrid.Rows.Count.ToString();
                Status2 = clsTranslate.TranslateString("Refresh");
            }
            else
            {
                DBGrid.DataSource = null;
                MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            checkboxColumn.Dispose();

            checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = clsTranslate.TranslateString("Default");
            checkboxColumn.DataPropertyName = "DefaultFlag";
            checkboxColumn.Name = "DefaultFlag";
            checkboxColumn.Width = 50;
            DBGrid.Columns.RemoveAt(3);
            DBGrid.Columns.Insert(3, checkboxColumn);
        }

        protected override void Refresh()
        {
            LoadData();
        }

        protected override void Select()
        {
            if (DBGrid.CurrentRow == null) return;

            Util.retValue1 = DBGrid.CurrentRow.Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        protected override void Find()
        {
            for (int i = 0; i < DBGrid.Rows.Count; i++)
            {
                modWarehouseList mod = (modWarehouseList)DBGrid.Rows[i].DataBoundItem;
                if (mod.WarehouseId.CompareTo(FindText) == 0)
                {
                    DBGrid.CurrentCell = DBGrid.Rows[i].Cells[0];
                    DBGrid_SelectionChanged(null, null);
                    return;
                }
            }
        }

        protected override void New()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            Util.EmptyFormBox(this);
            cboStatus.SelectedIndex = 1;
            cboDefault.SelectedIndex = 0;
            txtWarehouseId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            txtWarehouseId.ReadOnly = true;
            txtWarehouseDesc.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtWarehouseId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtWarehouseId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Warehouse Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtWarehouseId.Focus();
                    return false;
                }                
                modWarehouseList mod = new modWarehouseList();
                mod.WarehouseId = txtWarehouseId.Text.Trim();
                mod.WarehouseDesc = txtWarehouseDesc.Text.Trim();
                mod.Status = cboStatus.SelectedIndex;
                mod.DefaultFlag = cboDefault.SelectedIndex;
                mod.UpdateUser = Util.UserId;
                bool ret = false;
                if (_status == 1)
                    ret = _dal.Insert(mod, out Util.emsg);
                else if (_status == 2)
                    ret = _dal.Update(txtWarehouseId.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    LoadData();
                    FindText = mod.WarehouseId;
                    Find();
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
            DBGrid_SelectionChanged(null, null);
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtWarehouseId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtWarehouseId.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modWarehouseList mod = (modWarehouseList)DBGrid.CurrentRow.DataBoundItem;
                txtWarehouseId.Text = mod.WarehouseId;
                txtWarehouseDesc.Text = mod.WarehouseDesc;                
                cboStatus.SelectedIndex = Convert.ToInt32(mod.Status);
                cboDefault.SelectedIndex = Convert.ToInt32(mod.DefaultFlag);
                FindText = mod.WarehouseId;
            }
        }

        private void txtWarehouseList_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }
    }
}