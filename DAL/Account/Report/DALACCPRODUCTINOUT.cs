using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalAccProductInout
    {
        /// <summary>
        /// get account product summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accproductinout</returns>
        public BindingCollection<modAccProductSummary> GetAccProductSummary(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductSummary> modellist = new BindingCollection<modAccProductSummary>();
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.acc_name,a.product_id,b.product_name,sum(a.size * a.start_qty) start_qty,sum(a.start_mny) start_mny,sum(a.size * a.input_qty) input_qty,sum(a.input_mny) input_mny,sum(a.size * a.output_qty) output_qty,sum(a.output_mny) output_mny "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' group by a.acc_name,a.product_id,b.product_name order by a.product_id", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductSummary model = new modAccProductSummary();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.StartQty = string.Format("{0:N2}", rdr["start_qty"]);
                        model.StartMny = string.Format("{0:N2}", rdr["start_mny"]);
                        if (Convert.ToDecimal(rdr["start_qty"]) != 0)
                            model.StartPrice = string.Format("{0:N2}", Convert.ToDecimal(rdr["start_mny"]) / Convert.ToDecimal(rdr["start_qty"]));
                        model.InputQty = string.Format("{0:N2}", rdr["input_qty"]);
                        model.InputMny = string.Format("{0:N2}", rdr["input_mny"]);
                        model.OutputQty = string.Format("{0:N2}", rdr["output_qty"]);
                        model.OutputMny = string.Format("{0:N2}", rdr["output_mny"]);
                        decimal endqty=Convert.ToDecimal(rdr["start_qty"]) + Convert.ToDecimal(rdr["input_qty"]) - Convert.ToDecimal(rdr["output_qty"]);
                        decimal endmny=Convert.ToDecimal(rdr["start_mny"]) + Convert.ToDecimal(rdr["input_mny"]) - Convert.ToDecimal(rdr["output_mny"]);
                        model.EndQty = string.Format("{0:N2}", endqty);
                        model.EndMny = string.Format("{0:N2}", endmny);
                        if (endqty != 0)
                            model.EndPrice = string.Format("{0:N2}", endmny / endqty);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get account product summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accproductinout</returns>
        public BindingCollection<modAccProductSizeSummary> GetAccProductSizeSummary(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductSizeSummary> modellist = new BindingCollection<modAccProductSizeSummary>();
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.acc_name,a.product_id,b.product_name,a.size,sum(a.start_qty) start_qty,sum(a.start_mny) start_mny,sum(a.input_qty) input_qty,sum(a.input_mny) input_mny,sum(a.output_qty) output_qty,sum(a.output_mny) output_mny "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' group by a.acc_name,a.product_id,b.product_name,a.size order by a.product_id,a.size desc", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductSizeSummary model = new modAccProductSizeSummary();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = string.Format("{0:N2}", rdr["start_qty"]);
                        model.StartMny = string.Format("{0:N2}", rdr["start_mny"]);
                        model.InputQty = string.Format("{0:N2}", rdr["input_qty"]);
                        model.InputMny = string.Format("{0:N2}", rdr["input_mny"]);
                        model.OutputQty = string.Format("{0:N2}", rdr["output_qty"]);
                        model.OutputMny = string.Format("{0:N2}", rdr["output_mny"]);
                        model.EndQty = string.Format("{0:N2}", Convert.ToDecimal(rdr["start_qty"]) + Convert.ToDecimal(rdr["input_qty"]) - Convert.ToDecimal(rdr["output_qty"]));
                        model.EndMny = string.Format("{0:N2}", Convert.ToDecimal(rdr["start_mny"]) + Convert.ToDecimal(rdr["input_mny"]) - Convert.ToDecimal(rdr["output_mny"]));
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

		public Tuple<decimal, decimal> GetStartData(string accName, string productId, int accSeq, string inoutTablename)
		{
			string sql= @"select isnull(sum(a.size * a.start_qty + a.size * a.input_qty - a.size * a.output_qty),0) qty, isnull(sum(a.start_mny+a.input_mny-a.output_mny),0) mny 
					from " + inoutTablename + " a where a.acc_name=@acc_name and product_id=@product_id and acc_seq < @acc_seq";
			SqlParameter[] parms = {
				new SqlParameter("acc_name", accName),
				new SqlParameter("product_id", productId),
				new SqlParameter("acc_seq", accSeq)
			};
			using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
			{
				if (rdr.Read())
					return Tuple.Create(Math.Round(decimal.Parse(rdr["qty"].ToString()), 2), Math.Round(decimal.Parse(rdr["mny"].ToString()),2));
				else
					return Tuple.Create(decimal.Parse("0"), decimal.Parse("0"));
			}
		}
		
		///// <summary>
		///// get account product book
		///// <summary>
		///// <param name=accname>accname</param>
		///// <param name=out emsg>return error message</param>
		/////<returns>details of all accproductinout</returns>
		//public BindingCollection<modAccProductBook> GetAccProductBook(string accname, string productId, bool isTrialBalance, out string emsg)
		//{
		//	try
		//	{
		//		BindingCollection<modAccProductBook> modellist = new BindingCollection<modAccProductBook>();
		//		//Execute a query to read the categories
		//		string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
		//		string sql = "select a.acc_seq,a.remark,a.form_type,a.size * a.start_qty start_qty, a.start_mny,a.size * a.input_qty input_qty,a.input_mny,a.size * a.output_qty output_qty,a.output_mny "
		//				+ "from " + inoutTablename + " a where a.acc_name=@acc_name and a.product_id=@product_id order by a.acc_seq";

		//		SqlParameter[] parms = {
		//			new SqlParameter("acc_name", accname),
		//			new SqlParameter("product_id", productId)
		//		};

		//		using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
		//		{
		//			while (rdr.Read())
		//			{
		//				modAccProductBook model = new modAccProductBook();
		//				model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
						
		//				if (model.AccSeq > 0)
		//				{
		//					model.Remark = "(" + rdr["form_type"].ToString() + ") " + rdr["remark"].ToString();

		//					var startData = GetStartData(accname, productId, int.Parse(rdr["acc_seq"].ToString()), inoutTablename);

		//					model.InputQty = string.Format("{0:N2}", rdr["input_qty"]);
		//					model.InputMny = string.Format("{0:N2}", rdr["input_mny"]);

		//					model.OutputQty = string.Format("{0:N2}", rdr["output_qty"]);
		//					model.OutputMny = string.Format("{0:N2}", rdr["output_mny"]);
		//					decimal endqty = decimal.Parse(startData.Item1.ToString()) + decimal.Parse(rdr["input_qty"].ToString()) - decimal.Parse(rdr["output_qty"].ToString());
		//					decimal endmny = decimal.Parse(startData.Item2.ToString()) + decimal.Parse(rdr["input_mny"].ToString()) - decimal.Parse(rdr["output_mny"].ToString());
		//					model.EndQty = string.Format("{0:N2}", endqty);
		//					model.EndMny = string.Format("{0:N2}", endmny);
		//				}
		//				else
		//				{
		//					model.Remark = rdr["form_type"].ToString();
		//					model.StartQty = string.Format("{0:N2}", rdr["start_qty"]);
		//					model.StartMny = string.Format("{0:N2}", rdr["start_mny"]);

		//					decimal endqty = decimal.Parse(rdr["start_qty"].ToString());
		//					decimal endmny = decimal.Parse(rdr["start_mny"].ToString());
		//					model.EndQty = string.Format("{0:N2}", endqty);
		//					model.EndMny = string.Format("{0:N2}", endmny);
		//				}
						
		//				modellist.Add(model);
		//			}
		//		}
		//		emsg = null;
		//		return modellist;
		//	}
		//	catch (Exception ex)
		//	{
		//		emsg = dalUtility.ErrorMessage(ex.Message);
		//		return null;
		//	}
		//}

		/// <summary>
		/// get account product book
		/// <summary>
		/// <param name=accname>accname</param>
		/// <param name=out emsg>return error message</param>
		///<returns>details of all accproductinout</returns>
		public BindingCollection<modAccProductBook> GetAccProductBook(string startAccName, string endAccName, string productId, bool isTrialBalance, out string emsg)
		{
			try
			{
				BindingCollection<modAccProductBook> modellist = new BindingCollection<modAccProductBook>();

				dalAccPeriodList dalPeriod = new dalAccPeriodList();
				modAccPeriodList modStartPeriod = dalPeriod.GetItem(startAccName, out emsg);
				modAccPeriodList modEndPeriod = dalPeriod.GetItem(endAccName, out emsg);
				//Execute a query to read the categories
				string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
				string sql = @"select a.acc_name,a.acc_seq,a.acc_date,a.remark,a.form_type,sum(a.size * a.start_qty) start_qty, sum(a.start_mny) start_mny,sum(a.size * a.input_qty) input_qty,sum(a.input_mny) input_mny,sum(a.size * a.output_qty) output_qty,sum(a.output_mny) output_mny 
						from " + inoutTablename + " a where a.acc_date>=@start_date and a.acc_date<=@end_date and a.product_id=@product_id group by a.acc_name,a.acc_seq,a.acc_date,a.remark,a.form_type order by a.acc_date";

				SqlParameter[] parms = {
					new SqlParameter("start_date", modStartPeriod.StartDate),
					new SqlParameter("end_date", modEndPeriod.EndDate),
					new SqlParameter("product_id", productId)
				};

				decimal startqty = 0;
				decimal startmny = 0;
				decimal endqty = 0;
				decimal endmny = 0;
				int idx = 0;
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
				{
					while (rdr.Read())
					{
						modAccProductBook model = new modAccProductBook();
						model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);

						if (idx > 0)
						{
							model.AccName = rdr["acc_name"].ToString();
							model.Remark = "(" + rdr["form_type"].ToString() + ") " + rdr["remark"].ToString();

							model.StartQty = string.Format("{0:N2}", startqty);
							model.StartMny = string.Format("{0:N2}", startmny);

							model.InputQty = string.Format("{0:N2}", rdr["input_qty"]);
							model.InputMny = string.Format("{0:N2}", rdr["input_mny"]);

							model.OutputQty = string.Format("{0:N2}", rdr["output_qty"]);
							model.OutputMny = string.Format("{0:N2}", rdr["output_mny"]);
							endqty += decimal.Parse(rdr["start_qty"].ToString()) + decimal.Parse(rdr["input_qty"].ToString()) - decimal.Parse(rdr["output_qty"].ToString());
							endmny += decimal.Parse(rdr["start_mny"].ToString()) + decimal.Parse(rdr["input_mny"].ToString()) - decimal.Parse(rdr["output_mny"].ToString());
							
						}
						else
						{
							model.AccName = startAccName;
							model.Remark = rdr["form_type"].ToString();
							model.StartQty = string.Format("{0:N2}", rdr["start_qty"]);
							model.StartMny = string.Format("{0:N2}", rdr["start_mny"]);
							endqty += decimal.Parse(rdr["start_qty"].ToString());
							endmny += decimal.Parse(rdr["start_mny"].ToString());
							startqty = endqty;
							startmny = endmny;
						}
						model.EndQty = string.Format("{0:N2}", endqty);
						model.EndMny = string.Format("{0:N2}", endmny);

						modellist.Add(model);
						idx++;
					}
				}
				//if (modellist.Count > 0)
				//{
				//	modAccProductBook model = new modAccProductBook();
				//	model.Remark = "结存";
				//	model.EndQty = string.Format("{0:N2}", endqty);
				//	model.EndMny = string.Format("{0:N2}", endmny);
				//	modellist.Add(model);
				//}
				emsg = null;
				return modellist;
			}
			catch (Exception ex)
			{
				emsg = dalUtility.ErrorMessage(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// get all accproductinout
		/// <summary>
		/// <param name=accname>accname</param>
		/// <param name=productid>productid</param>
		/// <param name=out emsg>return error message</param>
		///<returns>details of all accproductinout</returns>
		public BindingCollection<modAccProductInout> GetIList(string accname, string productid, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductInout> modellist = new BindingCollection<modAccProductInout>();
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty,a.start_mny,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' and a.product_id='{1}' order by a.id", accname, productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all accproductinout
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accproductinout</returns>
        public BindingCollection<modAccProductInout> GetIList(string accname, int accseq, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductInout> modellist = new BindingCollection<modAccProductInout>();
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty,a.start_mny,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' and a.acc_seq={1} order by a.id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all accproductinout
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accproductinout</returns>
        public BindingCollection<modAccProductInout> GetIList(string productid, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductInout> modellist = new BindingCollection<modAccProductInout>();
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty,a.start_mny,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.product_id='{0}' order by a.id", productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get table record
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accproductinout</returns>
        public modAccProductInout GetItem(int? id, bool isTrialBalance, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty,a.start_mny,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from " + inoutTablename + " a inner join product_list b on a.product_id=b.product_id where a.id={0} order by a.id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        emsg = null;
                        return model;
                    }
                    else
                    {
                        emsg="Error on read data";
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get start product and size wip
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all product wip</returns>
        public BindingCollection<modStartProductWip> GetStartProductWip(string accname, string warehouseid, out string emsg)
        {
            try
            {
                BindingCollection<modStartProductWip> modellist = new BindingCollection<modStartProductWip>();
                //Execute a query to read the categories
                string sql = string.Format("select a.product_id,a.product_name,isnull(b.size,1) size,isnull(b.start_qty,0) start_qty,isnull(b.start_mny,0) start_mny from product_list a left join "
                        + "(select product_id,size,start_qty,start_mny from acc_product_inout where acc_name='{0}' and acc_seq=0 and remark='{1}') b on a.product_id=b.product_id where a.product_type<>'临时'", accname, warehouseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartProductWip model = new modStartProductWip();
                        model.AccName = accname;
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.WarehouseId = warehouseid;
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get start product and size wip
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all product wip</returns>
        public BindingCollection<modStartTempProductWip> GetStartTempProductWip(string accname, string warehouseid, out string emsg)
        {
            try
            {
                BindingCollection<modStartTempProductWip> modellist = new BindingCollection<modStartTempProductWip>();
                //Execute a query to read the categories
                string sql = string.Format("select a.product_id,a.product_name,a.brand,a.unit_no,isnull(b.start_qty,0) start_qty,isnull(b.start_mny,0) start_mny from product_list a left join "
                        + "(select product_id,start_qty,start_mny from acc_product_inout where acc_name='{0}' and acc_seq=0 and remark='{1}') b on a.product_id=b.product_id where a.product_type='临时'", accname, warehouseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartTempProductWip model = new modStartTempProductWip();
                        model.AccName = accname;
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.WarehouseId = warehouseid;
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get product list of zero qty but not zero mny
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modAccProductInout> GetNewZeroProduct(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductInout> modellist = new BindingCollection<modAccProductInout>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty2,a.start_mny2,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from acc_product_inout a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' and start_qty=0 and start_mny<>0", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty2"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny2"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get product list of zero qty but not zero mny
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modAccProductInout> GetZeroProduct(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAccProductInout> modellist = new BindingCollection<modAccProductInout>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.acc_date,a.product_id,b.product_name,a.size,a.start_qty2,a.start_mny2,a.input_qty,a.input_mny,a.output_qty,a.output_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from acc_product_inout a inner join product_list b on a.product_id=b.product_id where a.acc_name='{0}' and start_qty2=0 and start_mny2<>0", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProductInout model = new modAccProductInout();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_qty2"].ToString()));
                        model.StartMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["start_mny2"].ToString()));
                        model.InputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_qty"].ToString()));
                        model.InputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["input_mny"].ToString()));
                        model.OutputQty = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_qty"].ToString()));
                        model.OutputMny = dalUtility.ConvertToDecimal(string.Format("{0:N2}", rdr["output_mny"].ToString()));
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// save init product wip
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=startdate>startdate</param>
        /// <param name=list>list of modStartProductWip</param>
        /// <param name=listtmp>list of modStartTempProductWip</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartProductWip(string accname, DateTime startdate, string warehouseid, BindingCollection<modStartProductWip> list, string updateuser, out string emsg)
        {            
            string sql=string.Format("select isnull(sum(start_mny),0) from acc_product_inout where acc_name='{0}' and acc_seq=0 and remark<>'{1}'", accname, warehouseid);
            decimal summny = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq=0 and remark='{1}'", accname, warehouseid);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("delete warehouse_product_inout where warehouse_id='{0}' and form_type='期初数据'", warehouseid);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (list != null && list.Count > 0)
                    {
                        foreach (modStartProductWip modd in list)
                        {
                            if (modd.StartQty != 0 || modd.StartMny != 0)
                            {
                                sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", warehouseid, modd.ProductId, modd.Size, "0", "期初数据", "", startdate, modd.StartQty, 0, 0, "期初数据", updateuser);
                                SqlHelper.ExecuteNonQuery(sql);
                                sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, 0, startdate, modd.ProductId, modd.Size, modd.StartQty, modd.StartMny, 0, 0, 0, 0, '0', "期初数据", warehouseid, updateuser);
                                SqlHelper.ExecuteNonQuery(sql);
                                summny += modd.StartMny;
                            }
                        }
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and subject_id='1235' and seq=0", accname);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "1235", "库存商品", "", "", summny, 0, 1, 1, 1, "人民币");
                    SqlHelper.ExecuteNonQuery(sql);
                    transaction.Complete();//就这句就可以了。  
                    emsg = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    emsg = dalUtility.ErrorMessage(ex.Message);
                    return false;
                }
            }
        }

        private int GetNewDetailSeq(string accname)
        {
            string sql = string.Format("Select isnull(max(detail_seq),500) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=501 and detail_seq<=599", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// get product price
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=productid>productid</param>
        /// <param name=mod>model object of accproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>price</returns>
        public decimal GetPrice(string accname, string productid, out string emsg)
        {
            try
            {
                string sql = string.Format("select isnull(sum(start_mny+input_mny)/sum((start_qty+input_qty)*size),0) from acc_product_inout where acc_name='{0}' and product_id='{1}'", accname, productid);
                decimal price=Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
                emsg = string.Empty;
                return price;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// insert a accproductinout
        /// <summary>
        /// <param name=mod>model object of accproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccProductInout mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())",mod.AccName,mod.AccSeq,mod.AccDate,mod.ProductId,mod.Size,mod.StartQty,mod.StartMny,mod.InputQty,mod.InputMny,mod.OutputQty,mod.OutputMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = null;
                    return true;
                }
                else
                {
                    emsg = "Unknown error when ExecuteNonQuery!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update a accproductinout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modAccProductInout mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_product_inout set acc_name='{0}',acc_seq={1},acc_date='{2}',product_id='{3}',size={4},start_qty={5},start_mny={6},input_qty={7},input_mny={8},output_qty={9},output_mny={10},form_id='{11}',form_type='{12}',remark='{13}',update_user='{14}',update_time=getdate() where id={15}",mod.AccName,mod.AccSeq,mod.AccDate,mod.ProductId,mod.Size,mod.StartQty,mod.StartMny,mod.InputQty,mod.InputMny,mod.OutputQty,mod.OutputMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser,id);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = null;
                    return true;
                }
                else
                {
                    emsg = "Unknown error when ExecuteNonQuery!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// delete a accproductinout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_product_inout where id={0} ",id);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = null;
                    return true;
                }
                else
                {
                    emsg = "Unknown error when ExecuteNonQuery!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(int? id, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_product_inout where id={0} ",id);
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    emsg = null;
                    return true;
                }
                else
                {
                    emsg = "Unknown error when ExecuteNonQuery!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

    }
}
