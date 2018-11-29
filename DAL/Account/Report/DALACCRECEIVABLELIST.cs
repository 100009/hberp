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
    public class dalAccReceivableList
    {
        /// <summary>
        /// get customer receivable summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modCustReceivableSummary> GetCustReceivableSummary(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modCustReceivableSummary> modellist = new BindingCollection<modCustReceivableSummary>();
                //Execute a query to read the categories
                string sql = string.Format("select a.acc_name,a.cust_id,b.cust_name,a.currency,a.exchange_rate,sum(a.start_mny) start_mny,sum(a.adding_mny) adding_mny,sum(a.paid_mny) paid_mny from acc_receivable_list a inner join customer_list b on a.cust_id=b.cust_id where a.acc_name='{0}' group by a.acc_name,a.cust_id,b.cust_name,a.currency,a.exchange_rate", accname);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modCustReceivableSummary model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modCustReceivableSummary();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
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
                model = new modCustReceivableSummary();
                model.AccName = accname;
                model.CustId = "合计";
                model.CustName = "合计";
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
        /// get customer receivable summary
        /// <summary>
        /// <param name=custId>custId</param>
        /// <param name=startDate>startDate</param>
        /// <param name=endDate>endDate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modReceivableBook> GetReceivableBook(string custId, DateTime startDate, DateTime endDate, out string emsg)
        {
            try
            {
                BindingCollection<modReceivableBook> modellist = new BindingCollection<modReceivableBook>();
                //Execute a query to read the categories
                string sql = string.Format("select a.acc_name,a.cust_id,b.cust_name,a.acc_date,a.seq,a.exchange_rate,a.start_mny,a.adding_mny,a.paid_mny,a.remark,c.no "
                        + "from acc_receivable_list a inner join customer_list b on a.cust_id=b.cust_id left join sales_shipment c on a.form_id=c.ship_id "
                        + "where a.cust_id='{0}' and a.acc_date between '{1}' and '{2}' order by a.acc_name,a.seq", custId, startDate, endDate);
                
                string accName = string.Empty;
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modReceivableBook model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (string.IsNullOrEmpty(accName))
                        {
                            accName = dalUtility.ConvertToString(rdr["acc_name"]);

                            model = new modReceivableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                            model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("送货单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                            model = new modReceivableBook();
                            model.AccName = accName;
                            model.CustId = "";
                            model.CustName = "";
                            model.AccSeq = "本月合计";
                            model.StartMny = totalstart == 0 ? "" : totalstart.ToString("#0.00");
                            model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                            model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
                            model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                            modellist.Add(model);

                            accName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model = new modReceivableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                            model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("送货单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                            model = new modReceivableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                            model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("送货单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                    model = new modReceivableBook();
                    model.AccName = accName;
                    model.CustId = "";
                    model.CustName = "";
                    model.AccSeq = "本月合计";
                    model.StartMny = totalstart == 0 ? "" : totalstart.ToString("#0.00");
                    model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                    model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
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
        /// get all accreceivablelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modAccReceivableList> GetIList(string accname, string custid, out string emsg)
        {
            try
            {
                BindingCollection<modAccReceivableList> modellist = new BindingCollection<modAccReceivableList>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.seq,a.acc_date,a.cust_id,b.cust_name,a.currency,a.exchange_rate,a.start_mny,a.adding_mny,a.paid_mny,a.form_id,a.form_type,a.remark,a.update_user,a.update_time "
                        + "from acc_receivable_list a inner join customer_list b on a.cust_id=b.cust_id where a.acc_name='{0}' and a.cust_id='{1}' order by a.id", accname, custid);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modAccReceivableList model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modAccReceivableList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
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
                        totalstart += model.StartMny * model.ExchangeRate;
                        totaladding += model.AddingMny * model.ExchangeRate;
                        totalpaid += model.PaidMny * model.ExchangeRate;
                    }
                }
                model = new modAccReceivableList();
                model.AccName = accname;
                model.CustId = "合计";
                model.CustName = "合计";
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
        /// get table record
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accreceivablelist</returns>
        public modAccReceivableList GetItem(int? id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_receivable_list where ID={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccReceivableList model = new modAccReceivableList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
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
        /// get all accreceivablelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accreceivablelist</returns>
        public BindingCollection<modStartReceivable> GetStartReceivable(string accname, string currency, decimal exchange_rate, out string emsg)
        {
            try
            {
                BindingCollection<modStartReceivable> modellist = new BindingCollection<modStartReceivable>();
                //Execute a query to read the categories
                string sql = string.Format("select a.cust_id,a.cust_name,isnull(b.start_mny,0) start_mny from customer_list a left join "
                    + "(select cust_id,currency,start_mny,exchange_rate from acc_receivable_list where acc_name='{0}' and seq=0 and currency='{1}') b on a.cust_id=b.cust_id where a.currency='{2}'", accname, currency, currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartReceivable model = new modStartReceivable();
                        model.AccName = accname;
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
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
        public bool SaveStartReceivable(string accname, DateTime startdate, string currency, decimal exchangerate, BindingCollection<modStartReceivable> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete acc_receivable_list where acc_name='{0}' and currency='{1}' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);                    
                    decimal summny = 0;
                    foreach (modStartReceivable modd in list)
                    {
                        if (modd.StartMny != 0)
                        {
                            sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, 0, startdate, modd.CustId, modd.Currency, exchangerate, modd.StartMny, 0, 0, '0', "期初数据", "期初设定", updateuser);
                            SqlHelper.ExecuteNonQuery(sql);
                            summny += modd.StartMny;
                        }
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and subject_id='1055' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "1055", "应收帐款", "", "", summny, 0, exchangerate, 1, 1, currency);
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
            string sql = string.Format("Select isnull(max(detail_seq),100) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=101 and detail_seq<=199", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// insert a accreceivablelist
        /// <summary>
        /// <param name=mod>model object of accreceivablelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccReceivableList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())",mod.AccName,mod.Seq,mod.AccDate,mod.CustId,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser);
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
        /// update a accreceivablelist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accreceivablelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modAccReceivableList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_receivable_list set acc_name='{0}',seq={1},acc_date='{2}',cust_id='{3}',currency='{4}',exchange_rate={5},start_mny={6},adding_mny={7},paid_mny={8},form_id='{9}',form_type='{10}',remark='{11}',update_user='{12}',update_time=getdate() where id={13}",mod.AccName,mod.Seq,mod.AccDate,mod.CustId,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser,id);
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
        /// delete a accreceivablelist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_receivable_list where id={0} ",id);
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
                string sql = string.Format("select count(1) from acc_receivable_list where id={0} ",id);
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
