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
    public class dalAccPayableList
    {
        /// <summary>
        /// get customer receivable summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modVendorPayableSummary> GetVendorPayableSummary(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modVendorPayableSummary> modellist = new BindingCollection<modVendorPayableSummary>();
                //Execute a query to read the categories
                string sql = string.Format("select a.acc_name,a.vendor_name,a.currency,a.exchange_rate,sum(a.start_mny) start_mny,sum(a.adding_mny) adding_mny,sum(a.paid_mny) paid_mny from acc_payable_list a where a.acc_name='{0}' group by a.acc_name,a.vendor_name,a.currency,a.exchange_rate", accname);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modVendorPayableSummary model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modVendorPayableSummary();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        model.AddingMny = dalUtility.ConvertToDecimal(rdr["adding_mny"]);
                        model.PaidMny = dalUtility.ConvertToDecimal(rdr["paid_mny"]);
                        model.EndMny = model.StartMny + model.AddingMny - model.PaidMny;
                        modellist.Add(model);
                        totalstart += model.StartMny * model.ExchangeRate;
                        totaladding += model.AddingMny * model.ExchangeRate;
                        totalpaid += model.PaidMny * model.ExchangeRate;
                    }
                }
                model = new modVendorPayableSummary();
                model.AccName = accname;
                model.VendorName = "合计";
                model.Currency = "人民币";
                model.ExchangeRate = 1;
                model.StartMny = totalstart;
                model.AddingMny = totaladding;
                model.PaidMny = totalpaid;
                model.EndMny = totalstart + totaladding - totalpaid;
                modellist.Add(model);
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
        /// get customer payable summary
        /// <summary>
        /// <param name=custId>custId</param>
        /// <param name=startDate>startDate</param>
        /// <param name=endDate>endDate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modPayableBook> GetPayableBook(string vendorName, DateTime startDate, DateTime endDate, out string emsg)
        {
            try
            {
                BindingCollection<modPayableBook> modellist = new BindingCollection<modPayableBook>();
                //Execute a query to read the categories
                string sql = string.Format("select a.acc_name,a.seq,a.acc_date,a.exchange_rate,a.vendor_name,a.start_mny,a.adding_mny,a.paid_mny,a.remark,b.no "
                        + "from acc_payable_list a left join purchase_list b on a.form_id=b.purchase_id where a.vendor_name='{0}' and a.acc_date between '{1}' and '{2}' order by a.acc_name,a.seq", vendorName, startDate, endDate);
                
                string accName = string.Empty;
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modPayableBook model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (string.IsNullOrEmpty(accName))
                        {
                            accName = dalUtility.ConvertToString(rdr["acc_name"]);

                            model = new modPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("采购单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
                            model.AccDate = dalUtility.ConvertToDate(rdr["acc_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["start_mny"]);
                            model.AddingMny = dalUtility.ConvertToDecimal(rdr["adding_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["adding_mny"]);
                            model.PaidMny = dalUtility.ConvertToDecimal(rdr["paid_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["paid_mny"]);
                            model.EndMny = model.StartMny;
                            modellist.Add(model);
                            totalstart += dalUtility.ConvertToDecimal(rdr["start_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totaladding += dalUtility.ConvertToDecimal(rdr["adding_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totalpaid += dalUtility.ConvertToDecimal(rdr["paid_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        }
                        else if (accName != dalUtility.ConvertToString(rdr["acc_name"]))
                        {
                            model = new modPayableBook();
                            model.AccName = accName;
                            model.VendorName = "";
                            model.AccSeq = "本月合计";
                            model.StartMny = totalstart == 0 ? "" : totalstart.ToString("#0.00");
                            model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                            model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
                            model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                            modellist.Add(model);

                            accName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model = new modPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("采购单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
                            model.AccDate = dalUtility.ConvertToDate(rdr["acc_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["start_mny"]);
                            model.AddingMny = dalUtility.ConvertToDecimal(rdr["adding_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["adding_mny"]);
                            model.PaidMny = dalUtility.ConvertToDecimal(rdr["paid_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["paid_mny"]);

                            totalstart = dalUtility.ConvertToDecimal(rdr["start_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totaladding = dalUtility.ConvertToDecimal(rdr["adding_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totalpaid = dalUtility.ConvertToDecimal(rdr["paid_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);

                            model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                            modellist.Add(model);
                        }
                        else
                        {
                            model = new modPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("采购单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
                            model.AccDate = dalUtility.ConvertToDate(rdr["acc_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["start_mny"]);
                            model.AddingMny = dalUtility.ConvertToDecimal(rdr["adding_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["adding_mny"]);
                            model.PaidMny = dalUtility.ConvertToDecimal(rdr["paid_mny"]) == 0 ? "" : dalUtility.ConvertToString(rdr["paid_mny"]);

                            totalstart += dalUtility.ConvertToDecimal(rdr["start_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totaladding += dalUtility.ConvertToDecimal(rdr["adding_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            totalpaid += dalUtility.ConvertToDecimal(rdr["paid_mny"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);

                            model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                            modellist.Add(model);
                        }
                    }
                    model = new modPayableBook();
                    model.AccName = accName;
                    model.VendorName = "";
                    model.AccSeq = "本月合计";
                    model.StartMny = totalstart == 0 ? "" : totalstart.ToString("#0.00");
                    model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                    model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString();
                    model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                    modellist.Add(model);
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
        /// get all accpayablelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modAccPayableList> GetIList(string accname, string vendorname, out string emsg)
        {
            try
            {
                BindingCollection<modAccPayableList> modellist = new BindingCollection<modAccPayableList>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.seq,a.acc_date,a.vendor_name,a.currency,a.exchange_rate,a.start_mny,a.adding_mny,a.paid_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time from acc_payable_list a where a.acc_name='{0}' and a.vendor_name='{1}' order by a.id", accname, vendorname);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modAccPayableList model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modAccPayableList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.StartMny = dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        model.AddingMny = dalUtility.ConvertToDecimal(rdr["adding_mny"]);
                        model.PaidMny = dalUtility.ConvertToDecimal(rdr["paid_mny"]);
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                        totalstart += model.StartMny * model.ExchangeRate;
                        totaladding += model.AddingMny * model.ExchangeRate;
                        totalpaid += model.PaidMny * model.ExchangeRate;
                    }
                }
                model = new modAccPayableList();
                model.AccName = accname;
                model.VendorName = "合计";
                model.Currency = "人民币";
                model.ExchangeRate = 1;
                model.StartMny = totalstart;
                model.AddingMny = totaladding;
                model.PaidMny = totalpaid;
                model.EndMny = totalstart + totaladding - totalpaid;
                modellist.Add(model);
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
        /// get all accpayablelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accpayablelist</returns>
        public BindingCollection<modAccPayableList> GetIList(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAccPayableList> modellist = new BindingCollection<modAccPayableList>();
                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_payable_list where acc_name='{0}' order by id",accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccPayableList model = new modAccPayableList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.VendorName=dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.StartMny=dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        model.AddingMny=dalUtility.ConvertToDecimal(rdr["adding_mny"]);
                        model.PaidMny=dalUtility.ConvertToDecimal(rdr["paid_mny"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        ///<returns>get a record detail of accpayablelist</returns>
        public modAccPayableList GetItem(int? id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_payable_list where ID={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccPayableList model = new modAccPayableList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.VendorName=dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.StartMny=dalUtility.ConvertToDecimal(rdr["start_mny"]);
                        model.AddingMny=dalUtility.ConvertToDecimal(rdr["adding_mny"]);
                        model.PaidMny=dalUtility.ConvertToDecimal(rdr["paid_mny"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get all init payable
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modStartPayable> GetStartPayable(string accname, string currency, decimal exchange_rate, out string emsg)
        {
            try
            {
                BindingCollection<modStartPayable> modellist = new BindingCollection<modStartPayable>();
                //Execute a query to read the categories
                //string sql = string.Format("a.vendor_name,isnull(b.start_mny,1) start_mny,isnull(b.exchange_rate,0) exchange_rate from vendor_list a left join acc_receivable_list b on a.vendor_name=b.vendor_name where b.acc_name='{0}' and b.currency='{1}'", accname, currency);
                string sql = string.Format("select a.vendor_name,isnull(b.start_mny,0) start_mny from vendor_list a left join "
                    + "(select vendor_name,currency,start_mny,exchange_rate from acc_payable_list where acc_name='{0}' and seq=0 and currency='{1}') b on a.vendor_name=b.vendor_name where a.currency='{2}'", accname, currency, currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartPayable model = new modStartPayable();
                        model.AccName = accname;
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Currency = currency;
                        model.ExchangeRate = exchange_rate;
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
        /// save init receivable data
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=startdate>startdate</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=list>list of modStartReceivable</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartPayable(string accname, DateTime startdate, string currency, decimal exchangerate, BindingCollection<modStartPayable> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete acc_payable_list where acc_name='{0}' and currency='{1}' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    decimal summny = 0;
                    foreach (modStartPayable modd in list)
                    {
                        if (modd.StartMny != 0)
                        {
                            sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, 0, startdate, modd.VendorName, modd.Currency, exchangerate, modd.StartMny, 0, 0, '0', "期初数据", "期初设定", updateuser);
                            SqlHelper.ExecuteNonQuery(sql);
                            summny += modd.StartMny;
                        }
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and subject_id='5145' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "5145", "应付帐款", "", "", 0, summny, exchangerate, -1, 1, currency);
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
            string sql = string.Format("Select isnull(max(detail_seq),200) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=201 and detail_seq<=299", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// insert a accpayablelist
        /// <summary>
        /// <param name=mod>model object of accpayablelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccPayableList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())",mod.AccName,mod.Seq,mod.AccDate,mod.VendorName,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser);
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
        /// update a accpayablelist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accpayablelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modAccPayableList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_payable_list set acc_name='{0}',seq={1},acc_date='{2}',vendor_name='{3}',currency='{4}',exchange_rate={5},start_mny={6},adding_mny={7},paid_mny={8},form_id='{9}',form_type='{10}',remark='{11}',update_user='{12}',update_time=getdate() where id={13}",mod.AccName,mod.Seq,mod.AccDate,mod.VendorName,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser,id);
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
        /// delete a accpayablelist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_payable_list where id={0} ",id);
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
        /// change accpayablelist's status(valid/invalid)
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_payable_list set status=1-status where id={0} ",id);
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
                string sql = string.Format("select count(1) from acc_payable_list where id={0} ",id);
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
