using System;
using System.Collections;
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
    public partial class MTN_PRODUCT_LIST : BaseFormEdit
    {
        dalProductList _dal = new dalProductList();
		string _productType = string.Empty;
		public MTN_PRODUCT_LIST()
        {
            InitializeComponent();
            SelectVisible = false;
        }

        private void MTN_PRODUCT_LIST_Load(object sender, EventArgs e)
        {
            //DBGrid.ContextMenuStrip.Items.Add("-");
            //DBGrid.ContextMenuStrip.Items.Add(clsTranslate.TranslateString("Product Sale Price"), LXMS.Properties.Resources.Property, new System.EventHandler(this.mnuProductSalePrice_Click));

            clsTranslate.InitLanguage(this);
            Util.ChangeStatus(this, true);
            FillControl.FillUnitList(cboUnitNo, false, true);
            if (clsLxms.ShowProductSpecify() == 1)
            {
                lblSpecify.Visible = true;
                txtSpecify.Visible = true;
            }
            else
            {
                lblSpecify.Visible = false;
                txtSpecify.Visible = false;
            }
            if (clsLxms.ShowProductSize() == 1)
            {
                lblSize.Visible = true;
                lblSizeStar.Visible = true;
                cboSizeFlag.Visible = true;
            }
            else
            {
                lblSize.Visible = false;
                lblSizeStar.Visible = false;
                cboSizeFlag.Visible = false;
            }
            DBGrid.Tag = this.Text;
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
				dalProductTypeList dal = new dalProductTypeList();
				BindingCollection<modProductTypeList> list = dal.GetIList(true, out Util.emsg);
				if (list != null)
				{
					foreach (modProductTypeList mod in list)
					{
						tvLeft.Nodes.Add(mod.ProductType, mod.ProductType, 0, 1);
					}
					if (tvLeft.Nodes.Count > 0)
						tvLeft.SelectedNode = tvLeft.Nodes[0];
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

		private void tvLeft_AfterSelect(object sender, TreeViewEventArgs e)
		{
			LoadData();
		}

		private void LoadData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
				if (tvLeft.SelectedNode.Level == 0)
				{
					_productType = tvLeft.SelectedNode.Name;
					BindingCollection<modProductList> list = _dal.GetIList(_productType, string.Empty, out Util.emsg);
					DBGrid.DataSource = list;
					DBGrid.Enabled = true;
					if (list != null)
					{
						AddComboBoxColumns();
						Status1 = DBGrid.Rows.Count.ToString();
						Status2 = clsTranslate.TranslateString("Refresh");
						if (clsLxms.ShowProductSpecify() == 0)
						{
							DBGrid.Columns["Specify"].Visible = false;
						}
						if (clsLxms.ShowProductSize() == 0)
						{
							DBGrid.Columns["SizeFlag"].Visible = false;
						}
					}
					else
					{
						DBGrid.DataSource = null;
						MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
					DBGrid.DataSource = null;
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
            checkboxColumn.HeaderText = clsTranslate.TranslateString("SizeFlag");
            checkboxColumn.DataPropertyName = "SizeFlag";
            checkboxColumn.Name = "SizeFlag";
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

            Util.retValue1 = DBGrid.CurrentRow.Cells["ProductId"].Value.ToString();
            Util.retValue2 = DBGrid.CurrentRow.Cells["ProductName"].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
        public void LoadPriceData(string productid)
        {
            dalProductSalePrice dal = new dalProductSalePrice();
            BindingCollection<modProductSalePrice> list = dal.GetProductSalePrice(productid, out Util.emsg);
            DBGridPrice.DataSource = list;
            if (list != null)
            {
                DBGridPrice.Columns["ProductId"].Visible = false;
                DBGridPrice.Columns["UpdateUser"].Visible = false;
                DBGridPrice.Columns["UpdateTime"].Visible = false;

                DBGridPrice.ReadOnly = false;
                DBGridPrice.Columns["CustLevel"].Width = 80;
                DBGridPrice.Columns["Price"].Width = 100;
                DBGridPrice.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                DBGridPrice.Columns["CustLevel"].ReadOnly = true;
                DBGridPrice.Columns["Price"].ReadOnly = false;
                DBGridPrice.AlternatingRowsDefaultCellStyle.BackColor = Color.Empty;
                DBGridPrice.Columns["CustLevel"].DefaultCellStyle.BackColor = frmOptions.ALTERNATING_BACKCOLOR;

                if (DBGridPrice.RowCount > 0)
                    DBGridPrice.CurrentCell = DBGridPrice.Rows[0].Cells["Price"];
            }
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
                    modProductList mod = (modProductList)DBGrid.Rows[i].DataBoundItem;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (mod.ProductName.IndexOf(finds[j]) < 0 || (!string.IsNullOrEmpty(mod.Barcode) && mod.Barcode.IndexOf(finds[j]) < 0))
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
                        modProductList mod = (modProductList)DBGrid.Rows[i].DataBoundItem;
                        for (int j = 0; j < finds.Length; j++)
                        {
                            if (mod.ProductName.IndexOf(finds[j]) < 0 || (!string.IsNullOrEmpty(mod.Barcode) && mod.Barcode.IndexOf(finds[j]) < 0))
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
                    modProductList mod = (modProductList)DBGrid.Rows[i].DataBoundItem;
                    for (int j = 0; j < finds.Length; j++)
                    {
                        if (mod.ProductName.IndexOf(finds[j]) < 0 || (!string.IsNullOrEmpty(mod.Barcode) && mod.Barcode.IndexOf(finds[j]) < 0))
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

        protected override void New()
        {
			if (_productType == null) return;

            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            toolSavePrice.Enabled = false;
            Util.EmptyFormBox(this);
            if (cboSizeFlag.Visible)
                cboSizeFlag.SelectedIndex = -1;
            else
                cboSizeFlag.SelectedIndex = 1;
            txtMinQty.Text = "0";
            txtMaxQty.Text = "0";
            txtProductId.Focus();
        }

        protected override void Edit()
        {
            Util.ChangeStatus(this, false);
            DBGrid.Enabled = false;
            toolSavePrice.Enabled = false;
            txtProductId.ReadOnly = true;
            txtProductName.Focus();
        }

        protected override bool Delete()
        {
            bool ret = _dal.Delete(txtProductId.Text.Trim(), out Util.emsg);
            if (ret)
                LoadData();
            return ret;
        }

        protected override bool Save()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtProductId.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Id") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductId.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Product Name") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtProductName.Focus();
                    return false;
                }
                if (cboUnitNo.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Unit No") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboUnitNo.Focus();
                    return false;
                }

                if (cboSizeFlag.Visible && cboSizeFlag.SelectedIndex == -1)
                {
                    MessageBox.Show(clsTranslate.TranslateString("Size Flag") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboSizeFlag.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMinQty.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Min Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMinQty.Focus();
                    return false;
                }
                else if (!Util.IsNumeric(txtMinQty.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Min Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMinQty.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtMaxQty.Text.Trim()))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Max Qty") + clsTranslate.TranslateString(" can not be null!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaxQty.Focus();
                    return false;
                }
                else if (!Util.IsNumeric(txtMaxQty.Text))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Max Qty") + clsTranslate.TranslateString(" must be a numeric!"), clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMaxQty.Focus();
                    return false;
                }
                modProductList mod = new modProductList();
                mod.ProductId = txtProductId.Text.Trim();
                mod.ProductName = txtProductName.Text.Trim();
                mod.ProductType = _productType;
                mod.Barcode = txtBarcode.Text.Trim();
                mod.Specify = txtSpecify.Text.Trim();
                mod.Brand = txtBrand.Text.Trim();
                mod.UnitNo = cboUnitNo.SelectedValue.ToString();          
                mod.Remark = txtRemark.Text.Trim();
                mod.MinQty = Convert.ToDecimal(txtMinQty.Text);
                mod.MaxQty = Convert.ToDecimal(txtMaxQty.Text);
                if (cboSizeFlag.Visible)
                    mod.SizeFlag = cboSizeFlag.SelectedIndex == 0 ? 1 : 0;
                else
                    mod.SizeFlag = 0;
                mod.UpdateUser = Util.UserId;
                bool ret = false;
                if (_status == 1)
                {
                    if(_dal.Exists(mod.ProductId, out Util.emsg))
                        ret = _dal.Update(txtProductId.Text, mod, out Util.emsg);
                    else
                        ret = _dal.Insert(mod, out Util.emsg);
                }
                else if (_status == 2)
                    ret = _dal.Update(txtProductId.Text, mod, out Util.emsg);
                if (ret)
                {
                    Util.ChangeStatus(this, true);
                    DBGrid.Enabled = true;
                    toolSavePrice.Enabled = true;
                    LoadData();
                    FindText = mod.ProductName;
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
            toolSavePrice.Enabled = true;
            DBGrid_SelectionChanged(null, null);
        }

        protected override bool Inactive()
        {
            bool ret = _dal.Inactive(txtProductId.Text.Trim(), out Util.emsg);
            if (ret)
            {
                LoadData();
                FindText = txtProductName.Text.Trim();
                Find();
            }
            return ret;
        }

        private void DBGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (DBGrid.CurrentRow != null)
            {
                modProductList mod = (modProductList)DBGrid.CurrentRow.DataBoundItem;
                txtProductId.Text = mod.ProductId;
                txtProductName.Text = mod.ProductName;                
                txtBarcode.Text = mod.Barcode;
                txtSpecify.Text = mod.Specify;
                txtBrand.Text = mod.Brand;
                cboUnitNo.SelectedValue = mod.UnitNo;                
                txtRemark.Text = mod.Remark;
                txtMinQty.Text = mod.MinQty.ToString();
                txtMaxQty.Text = mod.MaxQty.ToString();
                cboSizeFlag.SelectedIndex = mod.SizeFlag == 0 ? 1 : 0;
                //FindText = mod.ProductName;

                LoadPriceData(mod.ProductId);
            }
        }

        private void txtProductId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void DBGrid_DoubleClick(object sender, EventArgs e)
        {
            if (SelectVisible)
                Select();
        }

        private void cboUnitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUnitNo.SelectedValue == null) return;
            if (cboUnitNo.SelectedValue.ToString().CompareTo("New...") != 0) return;

            MTN_UNIT_LIST frm = new MTN_UNIT_LIST();
            frm.ShowDialog();
            FillControl.FillUnitList(cboUnitNo, false, true);
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmSingleSelect frm = new frmSingleSelect();
            ArrayList arr = _dal.GetBrandList(out Util.emsg);
            frm.InitData("请选择品牌：", arr, ComboBoxStyle.DropDown);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtBrand.Text = Util.retValue1;
            }
        }

        private void txtBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }
        private void toolSavePrice_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DBGridPrice.EndEdit();

                dalProductSalePrice dal = new dalProductSalePrice();
                BindingCollection<modProductSalePrice> list = (BindingCollection<modProductSalePrice>)DBGridPrice.DataSource;
                bool ret = dal.Save(list, out Util.emsg);
                if (ret)
                {
                    MessageBox.Show("数据保存成功!", clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		private void tvLeft_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
			{
				Point p = tvLeft.PointToClient(new Point(e.X, e.Y));
				TreeViewHitTestInfo index = tvLeft.HitTest(p);
				if (index.Node != null)
				{
					DataGridViewRow dr = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
					bool ret = _dal.UpdateProductType(dr.Cells[0].Value.ToString(), index.Node.Name.ToString(), Util.UserId, out Util.emsg);
					if (!ret)
					{
						MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Failure"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else
					{
						tvLeft.SelectedNode = index.Node;
						TreeViewEventArgs ea = new TreeViewEventArgs(index.Node);
						tvLeft_AfterSelect(null, ea);
						tvLeft.Focus();
					}
				}
			}
		}

		private void DBGrid_MouseDown(object sender, MouseEventArgs e)
		{
			if (txtProductId.ReadOnly == true && e.Button == MouseButtons.Left)
			{
				DataGridView.HitTestInfo info = DBGrid.HitTest(e.X, e.Y);
				if (info.RowIndex >= 0)
				{
					DataGridViewRow dr = (DataGridViewRow)DBGrid.Rows[info.RowIndex];
					if (dr != null)
						DBGrid.DoDragDrop(dr, DragDropEffects.Copy);
				}
			}
		}

		private void tvLeft_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
		}
	}
}