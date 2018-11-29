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
    public class dalAccOtherPayable
    {
        /// <summary>
        /// get customer payable summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accpayablelist</returns>
        public BindingCollection<modOtherPayableSummary> GetOtherPayableSummary(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modOtherPayableSummary> modellist = new BindingCollection<modOtherPayableSummary>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,object_name,currency,exchange_rate,sum(start_mny) start_mny,sum(adding_mny) adding_mny,sum(paid_mny) paid_mny from acc_other_payable where acc_name='{0}' group by acc_name,object_name,currency,exchange_rate", accname);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modOtherPayableSummary model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modOtherPayableSummary();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
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
                model = new modOtherPayableSummary();
                model.AccName = accname;
                model.ObjectName = "合计";
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
        public BindingCollection<modOtherPayableBook> GetOtherPayableBook(string objectName, DateTime startDate, DateTime endDate, out string emsg)
        {
            try
            {
                BindingCollection<modOtherPayableBook> modellist = new BindingCollection<modOtherPayableBook>();
                //Execute a query to read the categories
                string sql = string.Format("select a.acc_name,a.object_name,a.acc_date,a.seq,a.exchange_rate,a.start_mny,a.adding_mny,a.paid_mny,a.remark,b.no "
                        + "from acc_other_payable a left join acc_other_payable_form b on a.form_id=b.id where a.object_name='{0}' and a.acc_date between '{1}' and '{2}' order by a.acc_name,a.seq", objectName, startDate, endDate);

                string accName = string.Empty;
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modOtherPayableBook model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (string.IsNullOrEmpty(accName))
                        {
                            accName = dalUtility.ConvertToString(rdr["acc_name"]);

                            model = new modOtherPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                            model = new modOtherPayableBook();
                            model.AccName = accName;
                            model.ObjectName = "";
                            model.AccSeq = "本月合计";
                            model.StartMny = totalstart == 0 ? "" : totalstart.ToString("#0.00");
                            model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                            model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
                            model.EndMny = (totalstart + totaladding - totalpaid).ToString("#0.00");
                            modellist.Add(model);

                            accName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model = new modOtherPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                            model = new modOtherPayableBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
                            if (string.IsNullOrEmpty(rdr["no"].ToString()))
                                model.Digest = dalUtility.ConvertToString(rdr["remark"]);
                            else
                                model.Digest = ("单号：" + rdr["no"].ToString() + "  " + dalUtility.ConvertToString(rdr["remark"])).Trim();
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
                    model = new modOtherPayableBook();
                    model.AccName = accName;
                    model.ObjectName = "";
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
        /// get all accotherpayable
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accotherpayable</returns>
        public BindingCollection<modAccOtherPayable> GetIList(string accname, string objectname, out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherPayable> modellist = new BindingCollection<modAccOtherPayable>();
                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_other_payable where acc_name='{0}' and object_name='{1}' order by id", accname, objectname);
                decimal totalstart = 0;
                decimal totaladding = 0;
                decimal totalpaid = 0;
                modAccOtherPayable model;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        model = new modAccOtherPayable();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate = dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
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
                    }
                }
                model = new modAccOtherPayable();
                model.AccName = accname;
                model.ObjectName = "合计";
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
        /// get all accotherpayable
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accotherpayable</returns>
        public BindingCollection<modAccOtherPayable> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherPayable> modellist = new BindingCollection<modAccOtherPayable>();
                //Execute a query to read the categories
                string sql = "select id,acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_other_payable order by acc_name,seq,object_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccOtherPayable model = new modAccOtherPayable();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
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
        /// get all accotherpayable
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accotherpayable</returns>
        public BindingCollection<modAccOtherPayable> GetIList(string accname, int? seq, out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherPayable> modellist = new BindingCollection<modAccOtherPayable>();
                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_other_payable where acc_name='{0}' and seq={1} order by acc_name,seq,object_name",accname,seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccOtherPayable model = new modAccOtherPayable();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
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
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accotherpayable</returns>
        public modAccOtherPayable GetItem(string accname,int? seq,string objectname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time from acc_other_payable where acc_name='{0}' and seq={1} and object_name='{2}' order by acc_name,seq,object_name",accname,seq,objectname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccOtherPayable model = new modAccOtherPayable();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccDate=dalUtility.ConvertToDateTime(rdr["acc_date"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
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
        public BindingCollection<modStartOtherPayable> GetStartOtherPayable(string accname, string currency, decimal exchange_rate, out string emsg)
        {
            try
            {
                BindingCollection<modStartOtherPayable> modellist = new BindingCollection<modStartOtherPayable>();
                //Execute a query to read the categories
                //string sql = string.Format("a.object_name,isnull(b.start_mny,1) start_mny,isnull(b.exchange_rate,0) exchange_rate from other_payable_object a left join acc_other_payable b on a.object_name=b.object_name where b.acc_name='{0}' and b.currency='{1}'", accname, currency);
                string sql = string.Format("select a.object_name,isnull(b.start_mny,0) start_mny from other_payable_object a left join "
                    + "(select object_name,currency,start_mny,exchange_rate from acc_other_payable where acc_name='{0}' and seq=0 and currency='{1}') b on a.object_name=b.object_name where a.currency='{2}'", accname, currency, currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartOtherPayable model = new modStartOtherPayable();
                        model.AccName = accname;
                        model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
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
        public bool SaveStartOtherPayable(string accname, DateTime startdate, string currency, decimal exchangerate, BindingCollection<modStartOtherPayable> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete acc_other_payable where acc_name='{0}' and currency='{1}' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    decimal summny = 0;
                    foreach (modStartOtherPayable modd in list)
                    {
                        if (modd.StartMny != 0)
                        {
                            sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, 0, startdate, modd.ObjectName, modd.Currency, exchangerate, modd.StartMny, 0, 0, '0', "期初数据", "期初设定", updateuser);
                            SqlHelper.ExecuteNonQuery(sql);
                            summny += modd.StartMny;
                        }
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and subject_id='5155' and seq=0", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "5155", "其它应付款", "", "", 0, summny, exchangerate, -1, 1, currency);
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
            string sql = string.Format("Select isnull(max(detail_seq),400) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=401 and detail_seq<=499", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// insert a accotherpayable
        /// <summary>
        /// <param name=mod>model object of accotherpayable</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccOtherPayable mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())",mod.AccName,mod.Seq,mod.AccDate,mod.ObjectName,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser);
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
        /// update a accotherpayable
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=objectname>objectname</param>
        /// <param name=mod>model object of accotherpayable</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string accname,int? seq,string objectname,modAccOtherPayable mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_other_payable set id={0},acc_date='{1}',currency='{2}',exchange_rate={3},start_mny={4},adding_mny={5},paid_mny={6},form_id='{7}',form_type='{8}',remark='{9}',update_user='{10}',update_time=getdate() where acc_name='{11}' and seq={12} and object_name='{13}'",mod.Id,mod.AccDate,mod.Currency,mod.ExchangeRate,mod.StartMny,mod.AddingMny,mod.PaidMny,mod.FormId,mod.FormType,mod.Remark,mod.UpdateUser,accname,seq,objectname);
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
        /// delete a accotherpayable
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string accname,int? seq,string objectname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1} and object_name='{2}' ",accname,seq,objectname);
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
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname,int? seq,string objectname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_other_payable where acc_name='{0}' and seq={1} and object_name='{2}' ",accname,seq,objectname);
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
