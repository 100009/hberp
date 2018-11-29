using System;
using BindingCollection;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;
using System.Transactions;
using System.Collections.Generic;

namespace LXMS.DAL
{
    public class dalWarehouseInoutDailyReport
    {
        /// <summary>
        /// get all modWarehouseInoutDailyReport
        /// <summary>
        /// <param name=getwhere>getwhere</param>
        ///<returns>details of all modWarehouseInoutDailyReport</returns>
        public BindingCollection<modWarehouseInoutDailyReport> GetReport(DateTime reportDate, bool changedOnly, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutDailyReport> modellist = new BindingCollection<modWarehouseInoutDailyReport>();
				//Execute a query to read the categories
				SqlParameter[] parm = {
					new SqlParameter("report_date", SqlDbType.DateTime)
				};
				parm[0].Value = reportDate;

				string sqlTmp = "";
				if (changedOnly)
					sqlTmp = " and abs(purchase_qty)+abs(sale_qty)+abs(overflow_qty)+abs(loss_qty)+abs(production_output)+abs(production_input)+abs(transfer_output)+abs(transfer_input)>0";

				string sql = "select a.id,a.report_date,a.product_id,b.product_name,a.start_qty,a.end_qty,a.purchase_qty,a.sale_qty,a.overflow_qty,a.loss_qty,a.production_output,a.production_input,a.transfer_output,a.transfer_input "
					+ "from warehouse_inout_daily_report a inner join product_list b on a.product_id=b.product_id where a.report_date=@report_date" + sqlTmp + " order by id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parm))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutDailyReport model = new modWarehouseInoutDailyReport();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.ReportDate = dalUtility.ConvertToDateTime(rdr["report_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
						model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
						model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
						model.EndQty = dalUtility.ConvertToDecimal(rdr["end_qty"]);
						model.PurchaseQty = dalUtility.ConvertToDecimal(rdr["purchase_qty"]);
                        model.SaleQty = dalUtility.ConvertToDecimal(rdr["sale_qty"]);
                        model.OverflowQty = dalUtility.ConvertToDecimal(rdr["overflow_qty"]);
                        model.LossQty = dalUtility.ConvertToDecimal(rdr["loss_qty"]);
                        model.ProductionOutput = dalUtility.ConvertToDecimal(rdr["production_output"]);
                        model.ProductionInput = dalUtility.ConvertToDecimal(rdr["production_input"]);
                        model.TransferOutput = dalUtility.ConvertToDecimal(rdr["transfer_output"]);
                        model.TransferInput = dalUtility.ConvertToDecimal(rdr["transfer_input"]);
                        modellist.Add(model);
                    }
                }
                emsg = string.Empty;
                return modellist;
            }
            catch(Exception ex)
            {
                emsg = ex.Message;
                return null;
            }
        }

		public List<modShipmentDailyReport> GetShipmentDailyReport(DateTime reportDate, out string emsg)
		{
			try
			{
				List<modShipmentDailyReport> modellist = new List<modShipmentDailyReport>();
				SqlParameter[] parm = {
					new SqlParameter("inout_date", SqlDbType.DateTime)
				};
				parm[0].Value = reportDate;

				string sql = "select a.product_id,b.cust_name,sum(a.size * (a.output_qty - a.input_qty)) sale_qty "
							+ "from warehouse_product_inout a "
							+ "inner join sales_shipment b on a.form_id=b.ship_id "
							+ "where a.form_type in ('送货单','退货单') and a.inout_date = @inout_date "
							+ "group by a.product_id,b.cust_name";

				using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parm))
				{
					while (rdr.Read())
					{
						modShipmentDailyReport model = new modShipmentDailyReport();
						model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
						model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
						model.SaleQty = dalUtility.ConvertToDecimal(rdr["sale_qty"]);
						modellist.Add(model);
					}
				}
				emsg = string.Empty;
				return modellist;
			}
			catch (Exception ex)
			{
				emsg = ex.Message;
				return null;
			}
		}


		public bool GenDailyReport(DateTime reportDate, out string emsg)
		{
			try
			{
				var dalPeriod = new dalAccPeriodList();
				modAccPeriodList modPeriod = dalPeriod.GetDuringItem(reportDate, out emsg);
				if (modPeriod == null)
				{
					return false;
				}

				string sql = "select count(1) from warehouse_inout_daily_report where report_date=@report_date";
				SqlParameter[] parm = {
					new SqlParameter("report_date", SqlDbType.DateTime)
				};
				parm[0].Value = reportDate;
				int i = int.Parse(SqlHelper.ExecuteScalar(CommandType.Text, sql, parm).ToString());
				if (i > 0)
				{
					emsg = "今天的数据已生成,不可重生成！";
					return false;
				}

				sql = "select a.product_id,a.size*(a.start_qty+a.input_qty-a.output_qty) as start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.inout_date between @first_date and @yesterday_date "
						+ "union all select a.product_id,0 start_qty,a.size * (a.start_qty + a.input_qty - a.output_qty) as end_qty,0 purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.inout_date between @first_date and @today_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,a.size*(a.input_qty-a.output_qty) purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('采购收货','采购退货') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,a.size*(a.output_qty-a.input_qty) sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('送货单','退货单') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,a.size*a.input_qty as overflow_qty,a.size*a.output_qty as loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('溢余入库','借入物入库','借出物入库','损耗出库','借入物出库','借出物出库') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 overflow_qty,0 loss_qty,a.size*a.input_qty production_output,a.size*a.output_qty production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('外发单','内部单') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 overflow_qty,0 loss_qty,0 production_output,0 production_input,a.size*a.output_qty transfer_output,a.size*a.input_qty transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('转仓入库','转仓出库') and a.inout_date = @inout_date";

				sql = "insert into warehouse_inout_daily_report(report_date,product_id,start_qty,end_qty,purchase_qty,sale_qty,overflow_qty,loss_qty,production_output,production_input,transfer_output,transfer_input) "
					+ "select @reportDate,product_id,sum(start_qty) start_qty,sum(end_qty) end_qty,sum(purchase_qty) purchase_qty,sum(sale_qty) sale_qty,sum(overflow_qty) overflow_qty,sum(loss_qty) loss_qty,"
					+ "sum(production_output) production_output,sum(production_input) production_input,sum(transfer_output) transfer_output,sum(transfer_input) transfer_input "
					+ "from ( " + sql + ") t group by product_id";

				SqlParameter[] parm2 = { new SqlParameter("reportDate", SqlDbType.DateTime),
									new SqlParameter("first_date", SqlDbType.DateTime),
									new SqlParameter("yesterday_date", SqlDbType.DateTime),
									new SqlParameter("today_date", SqlDbType.DateTime),
									new SqlParameter("inout_date", SqlDbType.DateTime)};
				parm2[0].Value = reportDate;
				parm2[1].Value = modPeriod.StartDate;
				parm2[2].Value = reportDate.AddDays(-1);
				parm2[3].Value = reportDate;
				parm2[4].Value = reportDate;

				i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parm2);  
				if (i > 0)
					emsg = string.Empty;
				else
					emsg = "未产生任何数据！";
				return true;
			}
			catch (Exception ex)
			{
				emsg = ex.Message;
				return false;
			}			
		}

		public bool RegenDailyReport(DateTime reportDate, out string emsg)
		{
			using (TransactionScope transaction = new TransactionScope())//使用事务            
			{
				try
				{
					var dalPeriod = new dalAccPeriodList();
					modAccPeriodList modPeriod = dalPeriod.GetDuringItem(reportDate, out emsg);
					if (modPeriod == null)
					{
						return false;
					}

					string sql = "delete from warehouse_inout_daily_report where report_date=@report_date";
					SqlParameter[] parm = {
						new SqlParameter("report_date", SqlDbType.DateTime)
					};
					parm[0].Value = reportDate;
					int i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parm);

					sql = "select a.product_id,a.size*(a.start_qty+a.input_qty-a.output_qty) as start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.inout_date between @first_date and @yesterday_date "
						+ "union all select a.product_id,0 start_qty,a.size * (a.start_qty + a.input_qty - a.output_qty) as end_qty,0 purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.inout_date between @first_date and @today_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,a.size*(a.input_qty-a.output_qty) purchase_qty,0 sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('采购收货','采购退货') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,a.size*(a.output_qty-a.input_qty) sale_qty,0 as overflow_qty,0 loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('送货单','退货单') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,a.size*a.input_qty as overflow_qty,a.size*a.output_qty as loss_qty,0 production_output,0 production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('溢余入库','借入物入库','借出物入库','损耗出库','借入物出库','借出物出库') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 overflow_qty,0 loss_qty,a.size*a.input_qty production_output,a.size*a.output_qty production_input,0 transfer_output,0 transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('外发单','内部单') and a.inout_date = @inout_date "
						+ "union all select a.product_id,0 start_qty,0 end_qty,0 purchase_qty,0 sale_qty,0 overflow_qty,0 loss_qty,0 production_output,0 production_input,a.size*a.output_qty transfer_output,a.size*a.input_qty transfer_input "
						+ "from warehouse_product_inout a where a.form_type in ('转仓入库','转仓出库') and a.inout_date = @inout_date";

					sql = "insert into warehouse_inout_daily_report(report_date,product_id,start_qty,end_qty,purchase_qty,sale_qty,overflow_qty,loss_qty,production_output,production_input,transfer_output,transfer_input) "
						+ "select @reportDate,product_id,sum(start_qty) start_qty,sum(end_qty) end_qty,sum(purchase_qty) purchase_qty,sum(sale_qty) sale_qty,sum(overflow_qty) overflow_qty,sum(loss_qty) loss_qty,"
						+ "sum(production_output) production_output,sum(production_input) production_input,sum(transfer_output) transfer_output,sum(transfer_input) transfer_input "
						+ "from ( " + sql + ") t group by product_id";

					SqlParameter[] parm2 = { new SqlParameter("reportDate", SqlDbType.DateTime),
									new SqlParameter("first_date", SqlDbType.DateTime),
									new SqlParameter("yesterday_date", SqlDbType.DateTime),
									new SqlParameter("today_date", SqlDbType.DateTime),
									new SqlParameter("inout_date", SqlDbType.DateTime)};
					parm2[0].Value = reportDate;
					parm2[1].Value = modPeriod.StartDate;
					parm2[2].Value = reportDate.AddDays(-1);
					parm2[3].Value = reportDate;
					parm2[4].Value = reportDate;

					i = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, parm2);
					transaction.Complete();//就这句就可以了。  
					if (i > 0)
						emsg = string.Empty;
					else
						emsg = "未产生任何数据！";

					return true;
				}
				catch (Exception ex)
				{
					emsg = ex.Message;
					return false;
				}
			}
		}
	}
}
