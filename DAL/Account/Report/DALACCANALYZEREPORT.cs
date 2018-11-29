using System;
using System.Collections;
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
    public class dalAccAnalyzeProfit
    {
        /// <summary>
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProfitMonth> GetMonthlyReport(int year, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeProfitMonth> modellist = new BindingCollection<modAnalyzeProfitMonth>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,subject_id,subject_name,sum_mny,ad_flag from acc_analyze_profit where acc_year={0} order by acc_name,subject_id,acc_month", year);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProfitMonth model = new modAnalyzeProfitMonth();
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName=dalUtility.ConvertToString(rdr["subject_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProfitYears> GetYearsReport(out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeProfitYears> modellist = new BindingCollection<modAnalyzeProfitYears>();
                //Execute a query to read the categories
                string sql = "select acc_year,subject_id,subject_name,sum(sum_mny) sum_mny,ad_flag from acc_analyze_profit group by acc_year,subject_id,subject_name,ad_flag order by acc_year,subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProfitYears model = new modAnalyzeProfitYears();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
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

        public bool Generate(string accname, bool isTrialBalance, out string emsg)
        {            
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                if (modp == null)
                {
                    emsg = "Can not find the acc name [" + accname + "]";
                    return false;
                }

                string sql = string.Format("delete acc_analyze_profit where acc_name='{0}'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                dalAccReport dal = new dalAccReport();
                BindingCollection<modAccProfitReport> list = dal.GetAccProfitReport(accname, isTrialBalance, out emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccProfitReport mod in list)
                    {                        
                        sql = string.Format("insert into acc_analyze_profit(acc_name,acc_year,acc_month,subject_id,subject_name,sum_mny,ad_flag)values('{0}',{1},{2},'{3}','{4}',{5},{6})", mod.AccName, modp.AccYear, modp.AccMonth, mod.SubjectId, mod.SubjectName, mod.ThisMonth, mod.AdFlag);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                } 
                emsg = string.Empty;
                return true;
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
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname,string subjectid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_analyze_profit where acc_name='{0}' and subject_id='{1}' ",accname,subjectid);
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

    public class dalAccAnalyzeSales
    {

        public bool Generate(string accname, out string emsg)
        {
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                if (modp == null)
                {
                    emsg = "Can not find the acc name [" + accname + "]";
                    return false;
                }

                dalAccProductInout dalpdt = new dalAccProductInout();
                string sql = string.Format("delete acc_analyze_sales where acc_name='{0}'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                sql = "select a.cust_id,d.cust_name,a.ship_type,a.ad_flag,b.product_id,c.product_name,sum(b.qty) qty,sum(b.qty*b.price) mny from sales_shipment a inner join sales_shipment_detail b on a.ship_id=b.ship_id inner join product_list c on b.product_id=c.product_id inner join customer_list d on a.cust_id=d.cust_id "
                        + "where ship_date>='" + modp.StartDate + "' and ship_date<='" + modp.EndDate + "' group by a.cust_id,d.cust_name,a.ship_type,a.ad_flag,b.product_id,c.product_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        decimal qty = Convert.ToDecimal(rdr["qty"]);
                        decimal mny = Convert.ToDecimal(rdr["mny"]);
                        decimal price = 0;
                        if (qty != 0)
                            price = decimal.Round(Convert.ToDecimal(mny / qty), 2);
                        decimal costprice = dalpdt.GetPrice(accname, rdr["product_id"].ToString(), out emsg);
                        decimal costmny = costprice * qty;
                        sql = string.Format("insert into acc_analyze_sales(acc_name,acc_year,acc_month,ship_type,cust_id,cust_name,ad_flag,product_id,product_name,qty,price,cost_price,mny,cost_mny)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13})",
                            accname, modp.AccYear, modp.AccMonth, rdr["ship_type"].ToString(), rdr["cust_id"].ToString(), rdr["cust_name"].ToString(), Convert.ToInt32(rdr["ad_flag"]), rdr["product_id"].ToString(), rdr["product_name"].ToString(), qty, price, costprice, mny, costmny);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                }
                sql = "select cust_id,cust_name,form_type,ad_flag,product_name,sum(qty) qty,sum(mny) mny from sales_design_form "
                        + "where form_date>='" + modp.StartDate + "' and form_date<='" + modp.EndDate + "' group by cust_id,cust_name,form_type,ad_flag,product_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        decimal qty = Convert.ToDecimal(rdr["qty"]);
                        decimal mny = Convert.ToDecimal(rdr["mny"]);
                        decimal price = 0;
                        if (qty != 0)
                            price = decimal.Round(Convert.ToDecimal(mny / qty), 2);
                        decimal costprice = 0;
                        decimal costmny = 0;
                        sql = string.Format("insert into acc_analyze_sales(acc_name,acc_year,acc_month,ship_type,cust_id,cust_name,ad_flag,product_id,product_name,qty,price,cost_price,mny,cost_mny)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},{12},{13})",
                            accname, modp.AccYear, modp.AccMonth, rdr["form_type"].ToString(), rdr["cust_id"].ToString(), rdr["cust_name"].ToString(), Convert.ToInt32(rdr["ad_flag"]), rdr["product_name"].ToString(), rdr["product_name"].ToString(), qty, price, costprice, mny, costmny);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                }
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeSalesMonth> GetCustomerReport(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeSalesMonth> modellist = new BindingCollection<modAnalyzeSalesMonth>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,cust_id,cust_name,sum(mny*ad_flag) sum_mny,sum(cost_mny*ad_flag) cost_mny from acc_analyze_sales where acc_name='{0}' "
                    + "group by acc_name,acc_year,acc_month,cust_id,cust_name order by acc_name,cust_name,acc_month", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeSalesMonth model = new modAnalyzeSalesMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CostMny = decimal.Round(decimal.Parse(rdr["cost_mny"].ToString()), 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny != 0)
                            model.ProfitRatio = decimal.Round(Convert.ToDecimal(model.Profit / model.CostMny), 2);
                        else
                            model.ProfitRatio = 0;
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
        /// get all accanalyzesales
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzesales</returns>
        public BindingCollection<modAccAnalyzeSales> GetAnalyzeSalesDetail(string accname, string custid, out string emsg)
        {
            try
            {
                BindingCollection<modAccAnalyzeSales> modellist = new BindingCollection<modAccAnalyzeSales>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,ship_type,cust_id,cust_name,ad_flag,product_id,product_name,qty,cost_price,price,cost_mny,mny "
                        + "from acc_analyze_sales where acc_name='{0}' and cust_id='{1}' order by acc_name,ship_type,cust_id,product_id", accname, custid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccAnalyzeSales model = new modAccAnalyzeSales();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice = dalUtility.ConvertToDecimal(rdr["cost_price"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.CostMny = dalUtility.ConvertToDecimal(rdr["cost_mny"]);
                        model.SalesMny = dalUtility.ConvertToDecimal(rdr["mny"]);
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeSalesMonth> GetMonthlyReport(int year, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeSalesMonth> modellist = new BindingCollection<modAnalyzeSalesMonth>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,cust_id,cust_name,sum(mny*ad_flag) sum_mny,sum(cost_mny*ad_flag) cost_mny from acc_analyze_sales where acc_year={0} "
                    + "group by acc_name,acc_year,acc_month,cust_id,cust_name order by acc_name,cust_name,acc_month", year);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeSalesMonth model = new modAnalyzeSalesMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CostMny = decimal.Round(decimal.Parse(rdr["cost_mny"].ToString()), 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny != 0)
                            model.ProfitRatio = decimal.Round(Convert.ToDecimal(model.Profit / model.CostMny), 2);
                        else
                            model.ProfitRatio = 0;
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeSalesYear> GetYearsReport(out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeSalesYear> modellist = new BindingCollection<modAnalyzeSalesYear>();
                //Execute a query to read the categories
                string sql = "select acc_year,cust_id,cust_name,sum(sum_mny*ad_flag) sum_mny,sum(cost_mny*ad_flag) cost_mny from acc_analyze_sales where 1=1 "
                    + "group by acc_year,cust_id,cust_name order by acc_year,cust_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeSalesYear model = new modAnalyzeSalesYear();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CostMny = decimal.Round(decimal.Parse(rdr["cost_mny"].ToString()), 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny != 0)
                            model.ProfitRatio = decimal.Round(Convert.ToDecimal(model.Profit / model.CostMny), 2);
                        else
                            model.ProfitRatio = 0;
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

        public Hashtable GetSalesCustomer(int accyear, out string emsg)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string sql = string.Format("select distinct cust_id,cust_name from acc_analyze_sales where acc_year={0} order by cust_name", accyear);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (!ht.ContainsKey(rdr[0].ToString()))
                            ht.Add(rdr[0].ToString(), rdr[1].ToString());
                    }
                }
                emsg = string.Empty;
                return ht;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        public Hashtable GetSalesCustomer(out string emsg)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string sql = "select distinct cust_id,cust_name from acc_analyze_sales order by cust_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        ht.Add(rdr[0].ToString(), rdr[1].ToString());
                    }
                }
                emsg = string.Empty;
                return ht;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
    }

    public class dalAccAnalyzePurchase
    {
        public bool Generate(string accname, out string emsg)
        {
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                if (modp == null)
                {
                    emsg = "Can not find the acc name [" + accname + "]";
                    return false;
                }

                string sql = string.Format("delete acc_analyze_purchase where acc_name='{0}'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                sql = "insert into acc_analyze_purchase(acc_name,purchase_type,acc_year,acc_month,vendor_name,sum_mny,ad_flag) "
                        + "select '" + accname + "',purchase_type,'" + modp.AccYear + "','" + modp.AccMonth + "',vendor_name,sum(detail_sum+other_mny-kill_mny),ad_flag "
                        + "from purchase_list where purchase_type in ('采购收货','采购退货') and purchase_date>='" + modp.StartDate + "' and purchase_date<='" + modp.EndDate + "' group by purchase_type,vendor_name,ad_flag";
                SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzePurchaseMonth> GetMonthlyReport(int year, string purchasetypelist, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzePurchaseMonth> modellist = new BindingCollection<modAnalyzePurchaseMonth>();
                //Execute a query to read the categories
                string purchasetypewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchasetypelist) && purchasetypelist.CompareTo("ALL") != 0)
                    purchasetypewhere = "and purchase_type in ('" + purchasetypelist.Replace(",", "','") + "') ";
                string sql = string.Format("select acc_name,acc_year,acc_month,vendor_name,sum(sum_mny*ad_flag) sum_mny from acc_analyze_purchase where acc_year={0} "
                    + purchasetypewhere + "group by acc_name,acc_year,acc_month,vendor_name order by acc_name,vendor_name,acc_month", year);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzePurchaseMonth model = new modAnalyzePurchaseMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzePurchaseYear> GetYearsReport(string purchasetypelist, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzePurchaseYear> modellist = new BindingCollection<modAnalyzePurchaseYear>();
                //Execute a query to read the categories
                string purchasetypewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchasetypelist) && purchasetypelist.CompareTo("ALL") != 0)
                    purchasetypewhere = "and purchase_type in ('" + purchasetypelist.Replace(",", "','") + "') ";
                string sql = "select acc_year,vendor_name,sum(sum_mny*ad_flag) sum_mny from acc_analyze_purchase where 1=1 "
                    + purchasetypewhere + "group by acc_year,vendor_name order by acc_year,vendor_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzePurchaseYear model = new modAnalyzePurchaseYear();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
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

        public ArrayList GetPurchaseVendor(int accyear, out string emsg)
        {
            try
            {
                ArrayList arr = new ArrayList();
                string sql = string.Format("select distinct vendor_name from acc_analyze_purchase where acc_year={0} order by vendor_name", accyear);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        arr.Add(rdr[0].ToString());
                    }
                }
                emsg = string.Empty;
                return arr;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        public ArrayList GetPurchaseVendor(out string emsg)
        {
            try
            {
                ArrayList arr = new ArrayList();
                string sql = "select distinct vendor_name from acc_analyze_purchase order by vendor_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        arr.Add(rdr[0].ToString());
                    }
                }
                emsg = string.Empty;
                return arr;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
    }

    public class dalAccAnalyzeWaste
    {
        /// <summary>
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeWasteMonth> GetMonthlyReport(int year, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeWasteMonth> modellist = new BindingCollection<modAnalyzeWasteMonth>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,subject_id,subject_name,sum_mny,ad_flag from acc_analyze_waste where acc_year={0} order by acc_name,subject_id,acc_month", year);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeWasteMonth model = new modAnalyzeWasteMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeWasteYears> GetYearsReport(out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeWasteYears> modellist = new BindingCollection<modAnalyzeWasteYears>();
                //Execute a query to read the categories
                string sql = "select acc_year,subject_id,subject_name,sum(sum_mny) sum_mny,ad_flag from acc_analyze_waste group by acc_year,subject_id,subject_name,ad_flag order by acc_year,subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeWasteYears model = new modAnalyzeWasteYears();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.SumMny = decimal.Round(decimal.Parse(rdr["sum_mny"].ToString()), 2);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
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

        public bool Generate(string accname, out string emsg)
        {
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                if (modp == null)
                {
                    emsg = "Can not find the acc name [" + accname + "]";
                    return false;
                }

                string sql = string.Format("delete acc_analyze_waste where acc_name='{0}'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                sql = "insert into acc_analyze_waste(acc_name,acc_year,acc_month,subject_id,subject_name,sum_mny,ad_flag) "
                    + "select '" + accname + "','" + modp.AccYear + "','" + modp.AccMonth + "',subject_id,'商品盈溢' detail_id,sum(lend_money), 1 from acc_credence_detail where acc_name='" + accname + "' and seq>0 and subject_id='91353080' group by subject_id,detail_id";
                SqlHelper.ExecuteNonQuery(sql);
                sql = "insert into acc_analyze_waste(acc_name,acc_year,acc_month,subject_id,subject_name,sum_mny,ad_flag) "
                    + "select '" + accname + "','" + modp.AccYear + "','" + modp.AccMonth + "',subject_id,'商品损耗' detail_id,sum(borrow_money), -1 from acc_credence_detail where acc_name='" + accname + "' and seq>0 and subject_id='91353082' group by subject_id,detail_id";
                SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return true;
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
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname, string subjectid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_analyze_waste where acc_name='{0}' and subject_id='{1}' ", accname, subjectid);
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

    public class dalAccAnalyzeProduct
    {
        /// <summary>
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        ///  <param name=productidlist>productidlist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProductDetailMonth> GetMonthlyDetailReport(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeProductDetailMonth> modellist = new BindingCollection<modAnalyzeProductDetailMonth>();
                //Execute a query to read the categories                
                string sql = string.Format("select acc_name,acc_year,acc_month,product_id,product_name,sum(start_qty) start_qty,sum(start_mny) start_mny,sum(purchase_qty) purchase_qty,sum(purchase_mny) purchase_mny,sum(used_qty) used_qty,sum(used_mny) used_mny,sum(production_qty) production_qty,sum(production_mny) production_mny,sum(surplus_qty) surplus_qty,sum(surplus_mny) surplus_mny,sum(waste_qty) waste_qty,sum(waste_mny) waste_mny,sum(sales_qty) sales_qty,sum(sales_mny) sales_mny "
                        + "from acc_analyze_product where acc_name='{0}' group by acc_name,acc_year,acc_month,product_id,product_name order by acc_name,product_id,acc_month", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProductDetailMonth model = new modAnalyzeProductDetailMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.StartQty = decimal.Round(decimal.Parse(rdr["start_qty"].ToString()), 2);
                        model.StartMny = decimal.Round(decimal.Parse(rdr["start_mny"].ToString()), 2);
                        if (model.StartQty != 0)
                        {
                            model.StartPrice = decimal.Round(model.StartMny / model.StartQty, 2);
                        }
                        else
                            model.StartPrice = 0;
                        model.PurchaseQty = decimal.Round(decimal.Parse(rdr["purchase_qty"].ToString()), 2);
                        model.PurchaseMny = decimal.Round(decimal.Parse(rdr["purchase_mny"].ToString()), 2);
                        if (model.PurchaseQty != 0)
                        {
                            model.PurchasePrice = decimal.Round(model.PurchaseMny / model.PurchaseQty, 2);
                        }
                        else
                            model.PurchasePrice = 0;
                        model.UsedQty = decimal.Round(decimal.Parse(rdr["used_qty"].ToString()), 2);
                        model.UsedMny = decimal.Round(decimal.Parse(rdr["used_mny"].ToString()), 2);
                        model.ProductionQty = decimal.Round(decimal.Parse(rdr["production_qty"].ToString()), 2);
                        model.ProductionMny = decimal.Round(decimal.Parse(rdr["production_mny"].ToString()), 2);
                        model.SurplusQty = decimal.Round(decimal.Parse(rdr["surplus_qty"].ToString()), 2);
                        model.SurplusMny = decimal.Round(decimal.Parse(rdr["surplus_mny"].ToString()), 2);
                        model.WasteQty = decimal.Round(decimal.Parse(rdr["waste_qty"].ToString()), 2);
                        model.WasteMny = decimal.Round(decimal.Parse(rdr["waste_mny"].ToString()), 2);
                        if (model.StartQty + model.PurchaseQty + model.ProductionQty != 0)
                        {
                            model.WipPrice = decimal.Round((model.StartMny + model.PurchaseMny + model.ProductionMny) / (model.StartQty + model.PurchaseQty + model.ProductionQty), 2);
                        }
                        else
                        {
                            model.WipPrice = 0;
                            
                        }
                        if ((model.StartMny + model.PurchaseMny + model.ProductionMny) > 0)
                        {
                            model.WasteRatio = decimal.Round((model.WasteMny) / (model.StartMny + model.PurchaseMny + model.ProductionMny), 2);
                        }
                        else
                        {
                            model.WasteRatio = 0;
                        }
                        model.SalesQty = decimal.Round(decimal.Parse(rdr["sales_qty"].ToString()), 2);
                        model.CostMny = decimal.Round(model.SalesQty * model.WipPrice, 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sales_mny"].ToString()), 2);
                        if (model.SalesQty != 0)
                            model.SalesPrice = decimal.Round(model.SalesMny / model.SalesQty, 2);
                        else
                            model.SalesPrice = 0;
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny > 0)
                            model.ProfitRatio = decimal.Round(model.Profit / model.CostMny, 1);
                        else
                            model.ProfitRatio = 0;
                        model.WipQty = model.StartQty + model.PurchaseQty - model.UsedQty + model.ProductionQty + model.SurplusQty - model.WasteQty - model.SalesQty;
                        model.WipMny = model.StartMny + model.PurchaseMny - model.UsedMny + model.ProductionMny + model.SurplusMny - model.WasteMny - model.CostMny;
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=productidlist>productidlist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProductDetailYears> GetYearsDetailReport(out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeProductDetailYears> modellist = new BindingCollection<modAnalyzeProductDetailYears>();
                //Execute a query to read the categories
                string sql = "select acc_year,product_id,product_name,sum(start_qty) start_qty,sum(start_mny) start_mny,sum(purchase_qty) purchase_qty,sum(purchase_mny) purchase_mny,sum(used_qty) used_qty,sum(used_mny) used_mny,sum(production_qty) production_qty,sum(production_mny) production_mny,sum(surplus_qty) surplus_qty,sum(surplus_mny) surplus_mny,sum(waste_qty) waste_qty,sum(waste_mny) waste_mny,sum(sales_qty) sales_qty,sum(sales_mny) sales_mny "
                    + "from acc_analyze_product where 1=1 group by acc_year,product_id,product_name order by acc_year,product_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProductDetailYears model = new modAnalyzeProductDetailYears();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.StartQty = decimal.Round(decimal.Parse(rdr["start_qty"].ToString()), 2);
                        model.StartMny = decimal.Round(decimal.Parse(rdr["start_mny"].ToString()), 2);
                        if (model.StartQty != 0)
                        {
                            model.StartPrice = decimal.Round(model.StartMny / model.StartQty, 2);
                        }
                        else
                            model.StartPrice = 0;
                        model.PurchaseQty = decimal.Round(decimal.Parse(rdr["purchase_qty"].ToString()), 2);
                        model.PurchaseMny = decimal.Round(decimal.Parse(rdr["purchase_mny"].ToString()), 2);
                        if (model.PurchaseQty != 0)
                        {
                            model.PurchasePrice = decimal.Round(model.PurchaseMny / model.PurchaseQty, 2);
                        }
                        else
                            model.PurchasePrice = 0;
                        model.UsedQty = decimal.Round(decimal.Parse(rdr["used_qty"].ToString()), 2);
                        model.UsedMny = decimal.Round(decimal.Parse(rdr["used_mny"].ToString()), 2);
                        model.ProductionQty = decimal.Round(decimal.Parse(rdr["production_qty"].ToString()), 2);
                        model.ProductionMny = decimal.Round(decimal.Parse(rdr["production_mny"].ToString()), 2);
                        model.SurplusQty = decimal.Round(decimal.Parse(rdr["surplus_qty"].ToString()), 2);
                        model.SurplusMny = decimal.Round(decimal.Parse(rdr["surplus_mny"].ToString()), 2);
                        model.WasteQty = decimal.Round(decimal.Parse(rdr["waste_qty"].ToString()), 2);
                        model.WasteMny = decimal.Round(decimal.Parse(rdr["waste_mny"].ToString()), 2);
                        if (model.StartQty + model.PurchaseQty + model.ProductionQty != 0)
                        {
                            model.WipPrice = decimal.Round((model.StartMny + model.PurchaseMny + model.ProductionMny) / (model.StartQty + model.PurchaseQty + model.ProductionQty), 2);
                            model.WasteRatio = decimal.Round((model.WasteMny) / (model.StartMny + model.PurchaseMny), 2);
                        }
                        else
                        {
                            model.WipPrice = 0;
                            model.WasteRatio = 0;
                        }
                        model.SalesQty = decimal.Round(decimal.Parse(rdr["sales_qty"].ToString()), 2);
                        model.CostMny = decimal.Round(model.SalesQty * model.WipPrice, 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sales_mny"].ToString()), 2);
                        if (model.SalesQty != 0)
                            model.SalesPrice = decimal.Round(model.SalesMny / model.SalesQty, 2);
                        else
                            model.SalesPrice = 0;
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny > 0)
                            model.ProfitRatio = decimal.Round(model.Profit / model.CostMny, 1);
                        else
                            model.ProfitRatio = 0;
                        model.WipQty = model.StartQty + model.PurchaseQty - model.UsedQty + model.ProductionQty + model.SurplusQty - model.WasteQty - model.SalesQty;
                        model.WipMny = model.StartMny + model.PurchaseMny - model.UsedMny + model.ProductionMny + model.SurplusMny - model.WasteMny - model.CostMny;
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProductMonth> GetMonthlyReport(int year, out string emsg)
        {
            try
            {
                dalAccProductInout dalpdt = new dalAccProductInout();
                BindingCollection<modAnalyzeProductMonth> modellist = new BindingCollection<modAnalyzeProductMonth>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,acc_year,acc_month,sum(start_qty) start_qty,sum(start_mny) start_mny,sum(purchase_qty) purchase_qty,sum(purchase_mny) purchase_mny,sum(used_qty) used_qty,sum(used_mny) used_mny,sum(production_qty) production_qty,sum(production_mny) production_mny,sum(surplus_qty) surplus_qty,sum(surplus_mny) surplus_mny,sum(waste_qty) waste_qty,sum(waste_mny) waste_mny,sum(sales_qty) sales_qty,sum(sales_mny) sales_mny, sum(sales_qty*wip_price) cost_mny, avg(wip_price) wip_price "
                    + "from acc_analyze_product where acc_year={0} group by acc_name,acc_year,acc_month order by acc_name,acc_year,acc_month", year);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProductMonth model = new modAnalyzeProductMonth();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.StartQty = decimal.Round(decimal.Parse(rdr["start_qty"].ToString()), 2);
                        model.StartMny = decimal.Round(decimal.Parse(rdr["start_mny"].ToString()), 2);
                        if (model.StartQty != 0)
                        {
                            model.StartPrice = decimal.Round(model.StartMny / model.StartQty, 2);
                        }
                        else
                            model.StartPrice = 0;
                        model.PurchaseQty = decimal.Round(decimal.Parse(rdr["purchase_qty"].ToString()), 2);
                        model.PurchaseMny = decimal.Round(decimal.Parse(rdr["purchase_mny"].ToString()), 2);
                        if (model.PurchaseQty != 0)
                        {
                            model.PurchasePrice = decimal.Round(model.PurchaseMny / model.PurchaseQty, 2);
                        }
                        else
                            model.PurchasePrice = 0;
                        model.UsedQty = decimal.Round(decimal.Parse(rdr["used_qty"].ToString()), 2);
                        model.UsedMny = decimal.Round(decimal.Parse(rdr["used_mny"].ToString()), 2);
                        model.ProductionQty = decimal.Round(decimal.Parse(rdr["production_qty"].ToString()), 2);
                        model.ProductionMny = decimal.Round(decimal.Parse(rdr["production_mny"].ToString()), 2);
                        model.SurplusQty = decimal.Round(decimal.Parse(rdr["surplus_qty"].ToString()), 2);
                        model.SurplusMny = decimal.Round(decimal.Parse(rdr["surplus_mny"].ToString()), 2);
                        model.WasteQty = decimal.Round(decimal.Parse(rdr["waste_qty"].ToString()), 2);
                        model.WasteMny = decimal.Round(decimal.Parse(rdr["waste_mny"].ToString()), 2);
                        model.WipPrice = decimal.Round(decimal.Parse(rdr["wip_price"].ToString()), 2);
                        if (model.StartQty + model.PurchaseQty + model.ProductionQty != 0)
                        {
                            //model.WipPrice = decimal.Round((model.StartMny + model.PurchaseMny + model.ProductionMny + model.SurplusMny) / (model.StartQty + model.PurchaseQty + model.ProductionQty + model.SurplusQty), 2);
                            model.WasteRatio = decimal.Round((model.WasteMny) / (model.StartMny + model.PurchaseMny + model.ProductionQty), 2);
                        }
                        else
                        {
                            //model.WipPrice = 0;
                            model.WasteRatio = 0;
                        }
                        model.SalesQty = decimal.Round(decimal.Parse(rdr["sales_qty"].ToString()), 2);
                        model.CostMny = decimal.Round(decimal.Parse(rdr["cost_mny"].ToString()), 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sales_mny"].ToString()), 2);
                        if (model.SalesQty != 0)
                            model.SalesPrice = decimal.Round(model.SalesMny / model.SalesQty, 2);
                        else
                            model.SalesPrice = 0;
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny > 0)
                            model.ProfitRatio = decimal.Round(model.Profit / model.CostMny, 1);
                        else
                            model.ProfitRatio = 0;
                        model.WipQty = model.StartQty + model.PurchaseQty - model.UsedQty + model.ProductionQty + model.SurplusQty - model.WasteQty - model.SalesQty;
                        model.WipMny = model.StartMny + model.PurchaseMny - model.UsedMny + model.ProductionMny + model.SurplusMny - model.WasteMny - model.CostMny;
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
        /// get all accanalyzeprofit
        /// <summary>
        /// <param name=year>year</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accanalyzeprofit</returns>
        public BindingCollection<modAnalyzeProductYears> GetYearsReport(out string emsg)
        {
            try
            {
                BindingCollection<modAnalyzeProductYears> modellist = new BindingCollection<modAnalyzeProductYears>();
                //Execute a query to read the categories
                string sql = "select acc_year,sum(purchase_qty) purchase_qty,sum(purchase_mny) purchase_mny,sum(used_qty) used_qty,sum(used_mny) used_mny,sum(production_qty) production_qty,sum(production_mny) production_mny,sum(surplus_qty) surplus_qty,sum(surplus_mny) surplus_mny,sum(waste_qty) waste_qty,sum(waste_mny) waste_mny,sum(sales_qty) sales_qty,sum(sales_mny) sales_mny, sum(sales_qty*wip_price) cost_mny,avg(wip_price) wip_price "
                    + "from acc_analyze_product group by acc_year order by acc_year";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAnalyzeProductYears model = new modAnalyzeProductYears();
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        sql = string.Format("select sum(start_qty) start_qty,sum(start_mny) start_mny from acc_analyze_product where acc_year={0} and acc_month =(select min(acc_month) from acc_period_list where acc_year={1})", model.AccYear, model.AccYear);
                        using (SqlDataReader rdr2 = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdr2.Read())
                            {
                                model.StartQty = decimal.Round(decimal.Parse(rdr2["start_qty"].ToString()), 2);
                                model.StartMny = decimal.Round(decimal.Parse(rdr2["start_mny"].ToString()), 2);
                            }
                        }
                        if (model.StartQty != 0)
                        {
                            model.StartPrice = decimal.Round(model.StartMny / model.StartQty, 2);
                        }
                        else
                            model.StartPrice = 0;
                        model.PurchaseQty = decimal.Round(decimal.Parse(rdr["purchase_qty"].ToString()), 2);
                        model.PurchaseMny = decimal.Round(decimal.Parse(rdr["purchase_mny"].ToString()), 2);
                        if (model.PurchaseQty != 0)
                        {
                            model.PurchasePrice = decimal.Round(model.PurchaseMny / model.PurchaseQty, 2);
                        }
                        else
                            model.PurchasePrice = 0;
                        model.UsedQty = decimal.Round(decimal.Parse(rdr["used_qty"].ToString()), 2);
                        model.UsedMny = decimal.Round(decimal.Parse(rdr["used_mny"].ToString()), 2);
                        model.ProductionQty = decimal.Round(decimal.Parse(rdr["production_qty"].ToString()), 2);
                        model.ProductionMny = decimal.Round(decimal.Parse(rdr["production_mny"].ToString()), 2);
                        model.SurplusQty = decimal.Round(decimal.Parse(rdr["surplus_qty"].ToString()), 2);
                        model.SurplusMny = decimal.Round(decimal.Parse(rdr["surplus_mny"].ToString()), 2);
                        model.WasteQty = decimal.Round(decimal.Parse(rdr["waste_qty"].ToString()), 2);
                        model.WasteMny = decimal.Round(decimal.Parse(rdr["waste_mny"].ToString()), 2);
                        if (model.StartQty + model.PurchaseQty + model.ProductionQty != 0)
                        {
                            model.WipPrice = decimal.Round((model.StartMny + model.PurchaseMny + model.ProductionMny) / (model.StartQty + model.PurchaseQty + model.ProductionQty), 2);
                            model.WasteRatio = decimal.Round((model.WasteMny) / (model.StartMny + model.PurchaseMny + model.ProductionQty), 2);
                        }
                        else
                        {
                            model.WipPrice = 0;
                            model.WasteRatio = 0;
                        }
                        model.SalesQty = decimal.Round(decimal.Parse(rdr["sales_qty"].ToString()), 2);
                        model.CostMny = decimal.Round(decimal.Parse(rdr["cost_mny"].ToString()), 2);
                        model.SalesMny = decimal.Round(decimal.Parse(rdr["sales_mny"].ToString()), 2);
                        if (model.SalesQty != 0)
                            model.SalesPrice = decimal.Round(model.SalesMny / model.SalesQty, 2);
                        else
                            model.SalesPrice = 0;
                        model.Profit = model.SalesMny - model.CostMny;
                        if (model.CostMny > 0)
                            model.ProfitRatio = decimal.Round(model.Profit / model.CostMny, 1);
                        else
                            model.ProfitRatio = 0;
                        model.WipQty = model.StartQty + model.PurchaseQty - model.UsedQty + model.ProductionQty + model.SurplusQty - model.WasteQty - model.SalesQty;
                        model.WipMny = model.StartMny + model.PurchaseMny - model.UsedMny + model.ProductionMny + model.SurplusMny - model.WasteMny - model.CostMny;
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

        public bool Generate(string accname, out string emsg)
        {
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                if (modp == null)
                {
                    emsg = "Can not find the acc name [" + accname + "]";
                    return false;
                }

                dalAccProductInout dalpdt = new dalAccProductInout();
                string sql = string.Format("delete acc_analyze_product where acc_name='{0}'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                sql = string.Format("select a.acc_name,a.product_id,b.product_name,sum(a.size*a.start_qty) start_qty,sum(a.start_mny) start_mny from acc_product_inout a inner join product_list b on a.product_id=b.product_id and a.acc_name ='{0}' group by a.acc_name,a.product_id,b.product_name", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        bool insertflag = false;
                        
                        decimal startqty = Convert.ToDecimal(rdr["start_qty"]);
                        decimal startmny = Convert.ToDecimal(rdr["start_mny"]);
                        if (startqty != 0 || startmny != 0)
                            insertflag = true;

                        decimal purchaseqty = 0;
                        decimal purchasemny = 0;
                        sql = "select isnull(sum(a.size*a.qty),0) qty,isnull(sum(a.qty*a.price),0) mny from purchase_detail a inner join purchase_list b on a.purchase_id=b.purchase_id where a.product_id='" + rdr["product_id"].ToString() + "' and b.purchase_date>='" + modp.StartDate + "' and b.purchase_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                purchaseqty = Convert.ToDecimal(rdrp[0]);
                                purchasemny = Convert.ToDecimal(rdrp[1]);
                                if (purchaseqty != 0 || purchasemny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal salesqty = 0;
                        decimal salesmny = 0;
                        sql = "select isnull(sum(a.size*a.qty*ad_flag),0) qty,isnull(sum(a.qty*a.price*ad_flag),0) mny from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id where b.ship_type in ('送货单','收营单','退货单') and a.product_id='" + rdr["product_id"].ToString() + "' and b.ship_date>='" + modp.StartDate + "' and b.ship_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                salesqty = Convert.ToDecimal(rdrp[0]);
                                salesmny = Convert.ToDecimal(rdrp[1]);
                                if (salesqty != 0 || salesmny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal surplusqty = 0;
                        decimal surplusmny = 0;
                        sql = "select isnull(sum(a.size*a.qty),0) qty,isnull(sum(a.qty*a.price),0) mny from warehouse_inout_form a where a.inout_type='溢余入库' and a.product_id='" + rdr["product_id"].ToString() + "' and a.inout_date>='" + modp.StartDate + "' and a.inout_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                surplusqty = Convert.ToDecimal(rdrp[0]);
                                surplusmny = Convert.ToDecimal(rdrp[1]);
                                if (surplusqty != 0 || surplusmny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal wasteqty = 0;
                        decimal wastemny = 0;
                        sql = "select isnull(sum(a.size*a.qty),0) qty,isnull(sum(a.qty*a.price),0) mny from warehouse_inout_form a where a.inout_type='损耗出库' and a.product_id='" + rdr["product_id"].ToString() + "' and a.inout_date>='" + modp.StartDate + "' and a.inout_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                wasteqty = Convert.ToDecimal(rdrp[0]);
                                wastemny = Convert.ToDecimal(rdrp[1]);
                                if (wasteqty != 0 || wastemny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal usedqty = 0;
                        decimal usedmny = 0;
                        sql = "select isnull(sum(a.size*a.qty),0) qty,isnull(sum(a.qty*a.cost_price),0) mny from production_form_material a inner join production_form b on a.form_id=b.form_id where a.product_id='" + rdr["product_id"].ToString() + "' and b.form_date>='" + modp.StartDate + "' and b.form_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                usedqty = Convert.ToDecimal(rdrp[0]);
                                usedmny = Convert.ToDecimal(rdrp[1]);
                                if (usedqty != 0 || usedmny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal productionqty = 0;
                        decimal productionmny = 0;
                        sql = "select isnull(sum(a.size*a.qty),0) qty,isnull(sum(a.qty*a.cost_price),0) mny from production_form_ware a inner join production_form b on a.form_id=b.form_id where a.product_id='" + rdr["product_id"].ToString() + "' and b.form_date>='" + modp.StartDate + "' and b.form_date<='" + modp.EndDate + "'";
                        using (SqlDataReader rdrp = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdrp.Read())
                            {
                                productionqty = Convert.ToDecimal(rdrp[0]);
                                productionmny = Convert.ToDecimal(rdrp[1]);
                                if (productionqty != 0 || productionmny != 0)
                                    insertflag = true;
                            }
                        }

                        decimal wipprice = 0;
                        if (startqty + purchaseqty + productionqty + surplusqty != 0)
                        {
                            wipprice = decimal.Round((startmny + purchasemny + productionmny + surplusmny) / (startqty + purchaseqty + productionqty + surplusqty), 8);
                            insertflag = true;
                        }
                        if (insertflag)
                        {
                            sql = string.Format("insert into acc_analyze_product(acc_name,acc_year,acc_month,product_id,product_name,start_qty,start_mny,purchase_qty,purchase_mny,used_qty,used_mny,production_qty,production_mny,surplus_qty,surplus_mny,waste_qty,waste_mny,sales_qty,sales_mny,wip_price)values('{0}',{1},{2},'{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19})",
                                accname, modp.AccYear, modp.AccMonth, rdr["product_id"].ToString(), rdr["product_name"].ToString(), Convert.ToDecimal(rdr["start_qty"]), Convert.ToDecimal(rdr["start_mny"]), purchaseqty, purchasemny, usedqty, usedmny, productionqty, productionmny, surplusqty, surplusmny, wasteqty, wastemny, salesqty, salesmny, wipprice);
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }
                }
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        public Hashtable GetAnalyzeProduct(int accyear, out string emsg)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string sql = string.Format("select distinct product_id,product_name from acc_analyze_product where acc_year={0} order by product_name", accyear);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if(!ht.ContainsKey(rdr[0].ToString()))
                            ht.Add(rdr[0].ToString(), rdr[1].ToString());
                    }
                }
                emsg = string.Empty;
                return ht;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        public Hashtable GetAnalyzeProduct(out string emsg)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string sql = "select distinct product_id,product_name from acc_analyze_product order by product_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        ht.Add(rdr[0].ToString(), rdr[1].ToString());
                    }
                }
                emsg = string.Empty;
                return ht;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname, string subjectid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_analyze_waste where acc_name='{0}' and subject_id='{1}' ", accname, subjectid);
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
