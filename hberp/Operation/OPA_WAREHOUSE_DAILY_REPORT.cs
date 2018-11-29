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
	public partial class OPA_WAREHOUSE_DAILY_REPORT : Form
	{
		dalWarehouseInoutDailyReport _dal = new dalWarehouseInoutDailyReport();
		public OPA_WAREHOUSE_DAILY_REPORT()
		{
			InitializeComponent();
		}

		private void OPA_WAREHOUSE_DAILY_REPORT_Load(object sender, EventArgs e)
		{
			if (DateTime.Today > Util.modperiod.EndDate)
				dtpReportDate.Value = Util.modperiod.EndDate;
			else
				dtpReportDate.Value = DateTime.Today;
		}
		private void Refresh()
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				BindingCollection<modWarehouseInoutDailyReport> list = _dal.GetReport(dtpReportDate.Value, chkChangedOnly.Checked, out Util.emsg);
				if (list != null)
					DBGrid.DataSource = list;
				else
					MessageBox.Show(Util.emsg);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
		private void dtpFrom_ValueChanged(object sender, EventArgs e)
		{
			Refresh();
		}
		private void chkChangedOnly_CheckedChanged(object sender, EventArgs e)
		{
			Refresh();
		}
		private void btnGen_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (_dal.GenDailyReport(dtpReportDate.Value, out Util.emsg))
				{
					Refresh();
				}
				else
				{
					MessageBox.Show(Util.emsg);
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}			
		}

		private void btnRegen_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				if (_dal.RegenDailyReport(dtpReportDate.Value, out Util.emsg))
				{
					Refresh();
				}
				else
				{
					MessageBox.Show(Util.emsg);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cursor = Cursors.WaitCursor;
				IList<modExcelRangeData> list = new List<modExcelRangeData>();
				var listReport = _dal.GetReport(dtpReportDate.Value, true, out Util.emsg);
				var listShipment = _dal.GetShipmentDailyReport(dtpReportDate.Value, out Util.emsg);

				list.Add(new modExcelRangeData(dtpReportDate.Value.ToString("yyyy年MM月dd日"), "C2", "C2"));

				var listCustomer = listShipment.Select(a => a.CustName).Distinct();

				short iCust = 78;
				Dictionary<string, string> dicCust = new Dictionary<string, string>();
				foreach (string custName in listCustomer)
				{
					iCust++;
					if (iCust <= 90)
					{
						string cell = string.Format("{0}4", ((char)iCust).ToString());
						list.Add(new modExcelRangeData(custName, cell, cell));
						dicCust.Add(cell, custName);
					}
					else
					{
						string cell = string.Format("A{0}4", ((char)iCust).ToString());
						list.Add(new modExcelRangeData(custName, cell, cell));
						dicCust.Add(cell, custName);
					}
				}

				int i = 4;
				foreach (modWarehouseInoutDailyReport item in listReport)
				{
					i++;
					string row = i.ToString();
					list.Add(new modExcelRangeData((i - 4).ToString(), "B" + row, "B" + row));
					list.Add(new modExcelRangeData(item.ProductId, "C" + row, "C" + row));
					list.Add(new modExcelRangeData(item.ProductName, "D" + row, "D" + row));
					list.Add(new modExcelRangeData(item.StartQty.ToString(), "E" + row, "E" + row));
					list.Add(new modExcelRangeData(item.PurchaseQty.ToString(), "F" + row, "F" + row));
					list.Add(new modExcelRangeData(item.SaleQty.ToString(), "G" + row, "G" + row));
					if (item.SaleQty != 0)
					{
						var listShipCust = listShipment.Where(a => a.ProductId == item.ProductId).ToList();
						foreach (var itemCust in listShipCust)
						{
							foreach (var cell in dicCust)
							{
								if (cell.Value == itemCust.CustName)
								{
									var curCell = cell.Key.Substring(0, cell.Key.Length - 1) + row;
									list.Add(new modExcelRangeData(item.SaleQty.ToString(), curCell, curCell));
									break;
								}
							}
						}
					}
					list.Add(new modExcelRangeData(item.OverflowQty.ToString(), "H" + row, "H" + row));
					list.Add(new modExcelRangeData(item.LossQty.ToString(), "I" + row, "I" + row));
					list.Add(new modExcelRangeData(item.ProductionOutput.ToString(), "J" + row, "J" + row));
					list.Add(new modExcelRangeData(item.ProductionInput.ToString(), "K" + row, "K" + row));
					list.Add(new modExcelRangeData(item.TransferOutput.ToString(), "L" + row, "L" + row));
					list.Add(new modExcelRangeData(item.TransferInput.ToString(), "M" + row, "M" + row));
					list.Add(new modExcelRangeData(item.EndQty.ToString(), "N" + row, "N" + row));
				}
				clsExport.ExportByTemplate(list, "仓库进销存日报表", 1, 10000, 200, 1);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
		}
	}
}
