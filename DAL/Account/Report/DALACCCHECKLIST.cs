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
    public class dalAccCheckList
    {
        /// <summary>
        /// get all accchecklist
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=idlist>idlist</param>
        /// <param name=subjectlist>subjectlist</param>
        /// <param name=checktypelist>checktypelist</param>
        /// <param name=banklist>banklist</param>
        /// <param name=checkno>checkno</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccCheckList> GetIList(string statuslist, string idlist, string subjectlist, string checktypelist, string banklist, string checkno, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckList> modellist = new BindingCollection<modAccCheckList>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string subjectwhere = string.Empty;
                if (!string.IsNullOrEmpty(subjectlist) && subjectlist.CompareTo("ALL") != 0)
                    subjectwhere = "and a.subject_id in ('" + subjectlist.Replace(",", "','") + "') ";

                string checktypewhere = string.Empty;
                if (!string.IsNullOrEmpty(checktypelist) && checktypelist.CompareTo("ALL") != 0)
                    checktypewhere = "and a.check_type in ('" + checktypelist.Replace(",", "','") + "') ";

                string bankwhere = string.Empty;
                if (!string.IsNullOrEmpty(banklist) && banklist.CompareTo("ALL") != 0)
                    bankwhere = "and a.bank_name in ('" + banklist.Replace(",", "','") + "') ";

                string checknowhere = string.Empty;
                if (!string.IsNullOrEmpty(checkno) && checkno.CompareTo("ALL") != 0)
                    checknowhere = "and a.check_no like '%" + checkno + "%' ";

                string sql = "select a.id,a.acc_name,a.acc_seq,a.check_no,a.subject_id,b.subject_name,a.bank_name,a.check_type,a.account_no,a.form_type,a.form_id,a.currency,mny,a.exchange_rate,a.create_date,a.promise_date,a.get_date,a.status,a.remark,a.update_user,a.update_time "
                        + "from acc_check_list a inner join acc_subject_list b on a.subject_id=b.subject_id where 1=1 " + statuswhere + idwhere + subjectwhere + checktypewhere + bankwhere + checknowhere + " order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckList model = new modAccCheckList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.AccountNo=dalUtility.ConvertToString(rdr["account_no"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CreateDate=dalUtility.ConvertToDateTime(rdr["create_date"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.GetDate=dalUtility.ConvertToDateTime(rdr["get_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
        /// get all accchecklist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccCheckList> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckList> modellist = new BindingCollection<modAccCheckList>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.check_no,a.subject_id,b.subject_name,a.bank_name,a.check_type,a.account_no,a.form_type,a.form_id,a.remark,a.currency,mny,a.exchange_rate,a.create_date,a.promise_date,a.get_date,a.status,a.update_user,a.update_time "
                        + "from acc_check_list a inner join acc_subject_list b on a.subject_id=b.subject_id where acc_name='{0}' and acc_seq={1} order by a.id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckList model = new modAccCheckList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.AccountNo=dalUtility.ConvertToString(rdr["account_no"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CreateDate=dalUtility.ConvertToDateTime(rdr["create_date"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.GetDate=dalUtility.ConvertToDateTime(rdr["get_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
        /// get wait credence list
        /// <summary>
        /// <param name=subjectlist>subjectlist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>accchecklist</returns>
        public BindingCollection<modAccCheckList> GetWaitingCredenceList(string subjectlist, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckList> modellist = new BindingCollection<modAccCheckList>();
                //Execute a query to read the categories
                string subjectwhere = string.Empty;
                if (!string.IsNullOrEmpty(subjectlist) && subjectlist.CompareTo("ALL") != 0)
                    subjectwhere = "and a.subject_id in ('" + subjectlist.Replace(",", "','") + "') ";

                string sql = "select a.id,a.acc_name,a.acc_seq,a.check_no,a.subject_id,b.subject_name,a.bank_name,a.check_type,a.account_no,a.form_type,a.form_id,a.remark,a.currency,mny,a.exchange_rate,a.create_date,a.promise_date,a.get_date,a.status,a.update_user,a.update_time "
                        + "from acc_check_list a inner join acc_subject_list b on a.subject_id=b.subject_id where not exists (select 1 from acc_check_form where check_id=a.id) " + subjectlist + "order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckList model = new modAccCheckList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CreateDate = dalUtility.ConvertToDateTime(rdr["create_date"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.GetDate = dalUtility.ConvertToDateTime(rdr["get_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
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
        ///<returns>get a record detail of accchecklist</returns>
        public modAccCheckList GetItem(int? id,out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.check_no,a.subject_id,b.subject_name,a.bank_name,a.check_type,a.account_no,a.form_type,a.form_id,a.remark,a.currency,mny,a.exchange_rate,a.create_date,a.promise_date,a.get_date,a.status,a.update_user,a.update_time "
                        + "from acc_check_list a inner join acc_subject_list b on a.subject_id=b.subject_id where ID={0} order by a.id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCheckList model = new modAccCheckList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.AccountNo=dalUtility.ConvertToString(rdr["account_no"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CreateDate=dalUtility.ConvertToDateTime(rdr["create_date"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.GetDate=dalUtility.ConvertToDateTime(rdr["get_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
        /// get all accchecklist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccCheckList> GetStartCheckList(string accname, string subjectid, string currency, decimal exchangerate, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckList> modellist = new BindingCollection<modAccCheckList>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.acc_name,a.acc_seq,a.check_no,a.subject_id,b.subject_name,a.bank_name,a.check_type,a.account_no,a.form_type,a.form_id,a.remark,a.currency,mny,a.exchange_rate,a.create_date,a.promise_date,a.get_date,a.status,a.update_user,a.update_time "
                        + "from acc_check_list a inner join acc_subject_list b on a.subject_id=b.subject_id where a.acc_name='{0}' and a.acc_seq = 0 and a.currency='{1}' and a.subject_id='{2}' order by a.id", accname, currency, subjectid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckList model = new modAccCheckList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CreateDate = dalUtility.ConvertToDateTime(rdr["create_date"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.GetDate = dalUtility.ConvertToDateTime(rdr["get_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
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
        /// save init check list
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=subjectname>subjectname</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=list>list of modAccCheckList</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartCheckList(string accname, string subjectid, string currency, decimal exchangerate, BindingCollection<modAccCheckList> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq=0 and subject_id='{1}'", accname, subjectid);
                    SqlHelper.ExecuteNonQuery(sql);
                    decimal summny = 0;
                    foreach (modAccCheckList modd in list)
                    {
						sql = string.Format("insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')", accname, 0, modd.CheckNo, subjectid, modd.BankName, modd.CheckType, modd.AccountNo, "期初数据", "0", currency, modd.Mny, exchangerate, modd.CreateDate, modd.PromiseDate, 0, modd.UpdateUser, modd.Remark);
                        SqlHelper.ExecuteNonQuery(sql);
                        summny += modd.Mny;
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and subject_id='{2}' and seq=0", accname, currency, subjectid);
                    SqlHelper.ExecuteNonQuery(sql);
                    if(subjectid.CompareTo("1075")==0)   //应收票据
                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", subjectid, "应收票据", "", "", summny, 0, exchangerate, 1, 1, currency);
                    else
                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", subjectid, "应付票据", "", "", 0, summny, exchangerate, -1, 1, currency);
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
            string sql = string.Format("Select isnull(max(detail_seq),600) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=601 and detail_seq<=699", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// insert a accchecklist
        /// <summary>
        /// <param name=mod>model object of accchecklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCheckList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')",mod.AccName,mod.AccSeq,mod.CheckNo,mod.SubjectId,mod.BankName,mod.CheckType,mod.AccountNo,mod.FormType,mod.FormId,mod.Currency,mod.Mny,mod.ExchangeRate,mod.CreateDate,mod.PromiseDate,0,mod.UpdateUser,mod.Remark);
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
        /// update a accchecklist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accchecklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modAccCheckList mod,out string emsg)
        {
            try
            {
				string sql = string.Format("update acc_check_list set acc_name='{0}',acc_seq={1},check_no='{2}',subject_id='{3}',bank_name='{4}',check_type='{5}',account_no='{6}',form_type='{7}',form_id='{8}',currency='{9}',mny={10},exchange_rate={11},create_date='{12}',promise_date='{13}',get_date='{14}',status={15},update_user='{16}',update_time=getdate(),remark='{17}' where id={18}", mod.AccName, mod.AccSeq, mod.CheckNo, mod.SubjectId, mod.BankName, mod.CheckType, mod.AccountNo, mod.FormType, mod.FormId, mod.Currency, mod.Mny, mod.ExchangeRate, mod.CreateDate, mod.PromiseDate, mod.GetDate, mod.Status, mod.Remark, mod.UpdateUser, id);
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
        /// delete a accchecklist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_check_list where id={0} ",id);
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
                string sql = string.Format("select count(1) from acc_check_list where id={0} ",id);
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
