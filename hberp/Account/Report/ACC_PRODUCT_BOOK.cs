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
	public partial class ACC_PRODUCT_BOOK : Form
	{
		dalProductList _dalPdt = new dalProductList();
		dalAccProductInout _dalAccPdt = new dalAccProductInout();
		bool _loadingLeft = false;
		public ACC_PRODUCT_BOOK()
		{
			InitializeComponent();
		}

		private void ACC_PRODUCT_BOOK_Load(object sender, EventArgs e)
		{
			clsTranslate.InitLanguage(this);
			FillControl.FillPeriodList(cboStartAccName);
			FillControl.FillProductType(cboProductType, false, false);						
		}

		private void LoadLeft()
		{
			if (cboProductType.SelectedValue == null) return;
			try
			{
				_loadingLeft = true;
				BindingCollection<modProductList> listPdt = _dalPdt.GetIList(cboProductType.SelectedValue.ToString(), txtProduct.Text.Trim(), out Util.emsg);
				DBGrid.DataSource = listPdt;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			finally
			{
				_loadingLeft = false;
				LoadData();
				this.Cursor = Cursors.Default;
			}
		}

		private void cboProductType_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadLeft();
		}
		private void LoadData()
		{
			if (_loadingLeft) return;
			if (DBGrid.CurrentCell == null) return;
			if (cboStartAccName.SelectedValue == null) return;
			if (cboEndAccName.SelectedValue == null) return;
			if (cboProductType.SelectedValue == null) return;
			try
			{
				this.Cursor = Cursors.WaitCursor;
				BindingCollection<modAccProductBook> list = _dalAccPdt.GetAccProductBook(cboStartAccName.SelectedValue.ToString(), cboEndAccName.SelectedValue.ToString(), DBGrid.CurrentRow.Cells[0].Value.ToString(), Util.IsTrialBalance, out Util.emsg);
				if (list == null && !string.IsNullOrEmpty(Util.emsg))
				{
					MessageBox.Show(Util.emsg, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					DBGrid2.DataSource = null;
					return;
				}
				DBGrid2.DataSource = list;
				for(int i=2;i<DBGrid2.ColumnCount;i++)
					DBGrid2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
			LoadData();
		}

		private void cboStartAccName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboStartAccName.SelectedValue != null)
				FillControl.FillEndPeriodList(cboEndAccName, cboStartAccName.SelectedValue.ToString());
			else
				cboEndAccName.DataSource = null;

			cboEndAccName.DroppedDown = true;
		}
		private void cboEndAccName_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadData();
		}
		private void txtProduct_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Enter))
				LoadLeft();
		}

	}
}
