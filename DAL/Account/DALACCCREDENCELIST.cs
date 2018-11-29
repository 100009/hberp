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
    public class dalAccCredenceList
    {
        /// <summary>
        /// get all acccredencelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=statuslist>statuslist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencelist</returns>
        public BindingCollection<modAccCredenceList> GetIList(string accname, string statuslist, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceList> modellist = new BindingCollection<modAccCredenceList>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string credenceTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";
                string sql = string.Format("select acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time,audit_man,audit_time from " + credenceTablename + " where acc_name='{0}' and seq>0 " + statuswhere + "order by acc_name,seq desc",accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceList model = new modAccCredenceList();
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.CredenceType=dalUtility.ConvertToString(rdr["credence_type"]);
                        model.CredenceWord=dalUtility.ConvertToString(rdr["credence_word"]);
                        model.CredenceDate=dalUtility.ConvertToDateTime(rdr["credence_date"]);                       
                        model.AttachCount=dalUtility.ConvertToInt(rdr["attach_count"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
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
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccredencelist</returns>
        public modAccCredenceList GetItem(string accname,int? seq, bool isTrialBalance, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string credenceTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";
                string sql = string.Format("select acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time,audit_man,audit_time from " + credenceTablename + " where acc_name='{0}' and seq={1} order by acc_name,seq",accname,seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCredenceList model = new modAccCredenceList();
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.CredenceType=dalUtility.ConvertToString(rdr["credence_type"]);
                        model.CredenceWord=dalUtility.ConvertToString(rdr["credence_word"]);
                        model.CredenceDate=dalUtility.ConvertToDateTime(rdr["credence_date"]);                        
                        model.AttachCount=dalUtility.ConvertToInt(rdr["attach_count"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
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
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modAccCredenceDetail> GetCredenceDetail(string accname, int? seq, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceDetail> modellist = new BindingCollection<modAccCredenceDetail>();
                //Execute a query to read the categories
                string credenceTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string sql = string.Format("select acc_name,seq,detail_seq,subject_id,subject_name,zcfz_flag,ad_flag,detail_id,detail_name,digest,lend_money,borrow_money,exchange_rate,currency from " 
                    + credenceTablename + " where acc_name='{0}' and seq={1} and ad_flag<>0 order by detail_seq", accname, seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceDetail model = new modAccCredenceDetail();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);                        
                        model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                        model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
                        model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);                        
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
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
        /// get start subject detail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modStartCashandBank> GetStartCashandBank(string accname, string currency, decimal exchangerate, out string emsg)
        {
            try
            {
                BindingCollection<modStartCashandBank> modellist = new BindingCollection<modStartCashandBank>();
                //Execute a query to read the categories
                string sql = string.Format("select a.account_no,isnull(b.borrow_money,0) borrow_money,isnull(b.lend_money,0) lend_money "
                        + "from acc_bank_account a left join (select detail_id,currency,borrow_money,lend_money from acc_credence_detail where acc_name='{0}' and seq=0 and subject_id='1030' and currency='{1}') b on a.account_no=b.detail_id and a.currency=b.currency "
                        + "where a.currency='{2}' order by a.account_no", accname, currency, currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartCashandBank model = new modStartCashandBank();
                        model.AccName = accname;
                        model.DetailId = dalUtility.ConvertToString(rdr["account_no"]);
                        model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
                        model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
                        model.ExchangeRate = exchangerate;
                        model.Currency = currency;
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
        /// get start subject detail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modStartSubjectData> GetStartSubjectDetail(string accname, string currency, decimal exchangerate, out string emsg)
        {
            try
            {
                BindingCollection<modStartSubjectData> modellist = new BindingCollection<modStartSubjectData>();
                //Execute a query to read the categories
                string sql = string.Format("select a.subject_id,a.subject_name,a.ad_flag,isnull(b.borrow_money,0) borrow_money,isnull(b.lend_money,0) lend_money "
                        + "from acc_subject_list a left join (select subject_id,borrow_money,lend_money from acc_credence_detail where acc_name='{0}' and seq=0 and currency='{1}') b on a.subject_id=b.subject_id where a.has_children=0 "
                        + "and a.subject_id not in ('1030','1055','1060','2115','2120','2125','1075','5145','5155','5125','1235','9165') order by a.subject_id", accname, currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modStartSubjectData model = new modStartSubjectData();
                        model.AccName = accname;
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.ZcfzFlag = model.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1;
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
                        model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);                        
                        model.ExchangeRate = exchangerate;
                        model.Currency = currency;
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
        /// save init subject data
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=list>list of modStartSubjectData</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartCashandBank(string accname, string currency, decimal exchangerate, BindingCollection<modStartCashandBank> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    int detailseq = GetNewCashBankDetailSeq(accname);
                    string sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and seq=0 and subject_id = '1030'", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (modStartCashandBank mod in list)
                    {
                        detailseq++;
                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "1030", "现金银行", mod.DetailId, "", mod.BorrowMoney, mod.LendMoney, exchangerate, 1, 1, currency);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
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

        private int GetNewCashBankDetailSeq(string accname)
        {
            string sql = string.Format("Select isnull(max(detail_seq),100) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=100 and detail_seq<=199", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        public bool UpdateStart9165(string accname, out string emsg)
        {
            try
            {
                decimal startmny1 = 0;
                decimal endmny1 = 0;
                decimal startmny2 = 0;
                decimal endmny2 = 0;
                dalAccReport dal = new dalAccReport();
                dal.GetSubjectMny(accname, "1", false, ref startmny1, ref endmny1, out emsg);
                string sql = string.Format("delete acc_credence_detail where acc_name='{0}' and seq=0 and subject_id = '9165'", accname);
                SqlHelper.ExecuteNonQuery(sql);
                dal.GetSubjectMny(accname, "5", false, ref startmny2, ref endmny2, out emsg);
                sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, 9999, "期初数据", "9165", "未分配利润", "", "", 0, startmny1 - startmny2, 1, -1, 1, "人民币");
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
        /// save init subject data
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=list>list of modStartSubjectData</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartSubjectData(string accname, string currency, decimal exchangerate, BindingCollection<modStartSubjectData> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    int detailseq = GetNewDetailSeq(accname);
                    string sql = string.Format("delete acc_credence_detail where acc_name='{0}' and currency='{1}' and seq=0 and subject_id not in ('1030','1055','1060','1075','2115','2120','5145','5155','5125','1235','9165')", accname, currency);
                    SqlHelper.ExecuteNonQuery(sql);                 
                    foreach (modStartSubjectData mod in list)
                    {
                        if (mod.BorrowMoney != 0 || mod.LendMoney != 0)
                        {
                            detailseq++;
                            sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", mod.SubjectId, mod.SubjectName, "", "", mod.BorrowMoney, mod.LendMoney, exchangerate, mod.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, mod.AdFlag, currency);
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }                    
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
            string sql = string.Format("Select isnull(max(detail_seq),1000) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=1000", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// get credence seq
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>seq</returns>
        public int GetNewSeq(string accname, out string emsg)
        {
            try
            {
                string sql = string.Format("select isnull(max(seq),0) from acc_credence_list where acc_name='{0}'", accname);
                int seq = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                emsg = string.Empty;
                return seq + 1;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// insert a acccredencelist
        /// <summary>
        /// <param name=mod>model object of acccredencelist</param>
        /// <param name=list>collect of acccredencedetail</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modAccCredenceList mod, BindingCollection<modAccCredenceDetail> list, string detaillist, BindingCollection<modSalesShipmentCost> listbalance, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    int? seq = mod.AccSeq;
                    dalAccSubjectList dals = new dalAccSubjectList();
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            if (Exists(mod.AccName, mod.AccSeq, out emsg))
                                seq = GetNewSeq(mod.AccName, out emsg);

                            if (mod.CredenceType.CompareTo("月末结算") == 0)
                            {
                                sql = string.Format("update acc_period_list set cost_flag=1,lock_flag=1 where acc_name='{0}'", mod.AccName);
                                SqlHelper.ExecuteNonQuery(sql);
                                mod.Status = 1;
                            }
                            sql = string.Format("insert into acc_credence_list(acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time)values('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',getdate())", mod.AccName, seq, mod.Status, mod.CredenceType, mod.CredenceWord, mod.CredenceDate, mod.AttachCount, mod.Remark, mod.UpdateUser);
                            SqlHelper.ExecuteNonQuery(sql);
                            foreach (modAccCredenceDetail modd in list)
                            {
                                if (modd.AdFlag != 0)
                                {
                                    modAccSubjectList mods = dals.GetItem(modd.SubjectId, out emsg);
                                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq, modd.Digest, modd.SubjectId, modd.SubjectName, modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, mods.AdFlag, modd.Currency);
                                    SqlHelper.ExecuteNonQuery(sql);
                                    if (modd.SubjectId == "2120")
                                    {
                                        switch (mod.CredenceType)
                                        {
                                            case "固定资产增加":
                                                sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2115", "固定资产原值", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                                SqlHelper.ExecuteNonQuery(sql);
                                                break;
                                            case "固定资产处理":
                                                sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2115", "固定资产原值", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                                SqlHelper.ExecuteNonQuery(sql);
                                                break;
                                            case "资产折旧":
                                                sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2125", "资产折旧", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                                SqlHelper.ExecuteNonQuery(sql);
                                                break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("delete acc_credence_detail where acc_name='{0}' and seq={1} ", mod.AccName, mod.AccSeq);
                            SqlHelper.ExecuteNonQuery(sql);

                            sql = string.Format("update acc_credence_list set status={0},credence_type='{1}',credence_word='{2}',credence_date='{3}',attach_count={4},remark='{5}',update_user='{6}',update_time=getdate() where acc_name='{7}' and seq={8}", mod.Status, mod.CredenceType, mod.CredenceWord, mod.CredenceDate, mod.AttachCount, mod.Remark, mod.UpdateUser, mod.AccName, mod.AccSeq);
                            SqlHelper.ExecuteNonQuery(sql);
                            foreach (modAccCredenceDetail modd in list)
                            {
                                modAccSubjectList mods = dals.GetItem(modd.SubjectId, out emsg);
                                sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq, modd.Digest, modd.SubjectId, modd.SubjectName, modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, mods.AdFlag, modd.Currency);
                                SqlHelper.ExecuteNonQuery(sql);
                                if (modd.SubjectId == "2120")
                                {
                                    switch (mod.CredenceType)
                                    {
                                        //case "固定资产增加":
                                        //    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2115", "固定资产原值", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                        //    SqlHelper.ExecuteNonQuery(sql);
                                        //    break;
                                        case "固定资产处理":
                                            sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2115", "固定资产原值", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                            SqlHelper.ExecuteNonQuery(sql);
                                            break;
                                        case "资产折旧":
                                            sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2125", "资产折旧", modd.DetailId, modd.DetailName, modd.LendMoney, modd.BorrowMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                            SqlHelper.ExecuteNonQuery(sql);
                                            break;
                                    }
                                }
                                else if (modd.SubjectId == "2115" && mod.CredenceType == "固定资产增加")
                                {
                                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq + 100, modd.Digest, "2120", "固定资产净值", modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, 0, modd.Currency);
                                    SqlHelper.ExecuteNonQuery(sql);
                                }
                            }                            
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete acc_credence_detail where acc_name='{0}' and seq={1} ", mod.AccName, mod.AccSeq);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete acc_credence_list where acc_name='{0}' and seq={1} ", mod.AccName, mod.AccSeq);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                        default:
                            emsg = "Can not recognize the command " + oprtype;
                            return false;
                    }
                    SaveDetail(oprtype, mod, detaillist, listbalance);
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
        /// <summary>
        /// insert a acccredencelist
        /// <summary>
        /// <param name=mod>model object of acccredencelist</param>
        /// <param name=list>collect of acccredencedetail</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveTrial(modAccCredenceList mod, BindingCollection<modAccCredenceDetail> list, string detaillist, BindingCollection<modSalesShipmentCost> listbalance, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    int? seq = mod.AccSeq;
                    dalAccSubjectList dals = new dalAccSubjectList();
                    if (Exists(mod.AccName, mod.AccSeq, out emsg))
                        seq = GetNewSeq(mod.AccName, out emsg);

                    
                    sql = string.Format("delete acc_trial_credence_detail where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("delete acc_trial_credence_list where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("delete acc_trial_product_inout where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);

                    sql = string.Format("insert into acc_trial_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)"
                            + "select acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time from acc_product_inout where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_trial_credence_list select * from acc_credence_list where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_trial_credence_detail select * from acc_credence_detail where acc_name='{0}'", mod.AccName);
                    SqlHelper.ExecuteNonQuery(sql);
                    
                    mod.Status = 1;

                    sql = string.Format("insert into acc_trial_credence_list(acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time)values('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',getdate())", mod.AccName, seq, mod.Status, mod.CredenceType, mod.CredenceWord, mod.CredenceDate, mod.AttachCount, mod.Remark, mod.UpdateUser);
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (modAccCredenceDetail modd in list)
                    {
                        if (modd.AdFlag != 0)
                        {
                            modAccSubjectList mods = dals.GetItem(modd.SubjectId, out emsg);
                            sql = string.Format("insert into acc_trial_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", mod.AccName, mod.AccSeq, modd.DetailSeq, modd.Digest, modd.SubjectId, modd.SubjectName, modd.DetailId, modd.DetailName, modd.BorrowMoney, modd.LendMoney, modd.ExchangeRate, modd.SubjectId.Substring(0, 1).CompareTo("5") >= 0 ? -1 : 1, mods.AdFlag, modd.Currency);
                            SqlHelper.ExecuteNonQuery(sql);                            
                        }
                    }
                    foreach (modSalesShipmentCost modd in listbalance)
                    {
                        sql = string.Format("insert into acc_trial_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modd.ProductId, modd.Size, 0, 0, 0, 0, modd.Qty, modd.Size * modd.Qty * modd.CostPrice, mod.AccSeq, mod.CredenceType, modd.Remark, mod.UpdateUser);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
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
        private void SaveDetail(string oprtype, modAccCredenceList mod, string detaillist, BindingCollection<modSalesShipmentCost> listbalance)
        {
            string sql = string.Empty;
            string emsg = string.Empty;
            switch (mod.CredenceType)
            {
                case "销售凭证":
                    sql = string.Format("update sales_shipment set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update sales_shipment set acc_name='{0}',acc_seq={1} where ship_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "设计加工凭证":
                    sql = string.Format("update sales_design_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update sales_design_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "采购凭证":
                    sql = string.Format("update purchase_list set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update purchase_list set acc_name='{0}',acc_seq={1} where purchase_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;                
                case "费用登记":
                    sql = string.Format("update acc_expense_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_expense_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "收款凭证":
                    sql = string.Format("update acc_receivable_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_receivable_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "付款凭证":
                    sql = string.Format("update acc_payable_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_payable_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "仓库进出":  //生产领料，生产入库，盘点盈溢，盘点损耗，借料入库，借料还出，借料出库，借料还入
                    sql = string.Format("update warehouse_inout_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update warehouse_inout_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;                
                case "其它应收凭证":
                    sql = string.Format("update acc_other_receivable_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_other_receivable_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "其它应付凭证":
                    sql = string.Format("update acc_other_payable_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_other_payable_form set acc_name='{0}',acc_seq={1} where id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "生产凭证":
                    sql = string.Format("update production_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update production_form set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "支票承兑":
                    sql = string.Format("update acc_check_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update acc_check_form set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "固定资产增加":
                    sql = string.Format("update asset_add set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update asset_add set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "固定资产处理":
                    sql = string.Format("update asset_sale set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update asset_sale set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "固定资产评估":
                    sql = string.Format("update asset_evaluate set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update asset_evaluate set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "资产折旧":
                    if (oprtype == "DEL" || oprtype == "DELETE")
                    {
                        sql = string.Format("delete asset_depre_list where acc_name='{0}'", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "月末结算":                    
                    foreach (modSalesShipmentCost modd in listbalance)
                    {
                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modd.ProductId, modd.Size, 0, 0, 0, 0, modd.Qty, modd.Size * modd.Qty * modd.CostPrice, mod.AccSeq, mod.CredenceType, modd.Remark, mod.UpdateUser);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                case "价格调整":
                    sql = string.Format("update price_adjust_form set acc_name='',acc_seq=0 where acc_name='{0}' and acc_seq={1}", mod.AccName, mod.AccSeq);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (oprtype != "DEL" && oprtype != "DELETE")
                    {
                        sql = string.Format("update price_adjust_form set acc_name='{0}',acc_seq={1} where form_id in ('" + detaillist.Replace(",", "','") + "')", mod.AccName, mod.AccSeq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// get assetdeprelist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetdeprelist</returns>
        public BindingCollection<modAssetDepreList> GetDepreList(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAssetDepreList> modellist = new BindingCollection<modAssetDepreList>();
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,asset_id,asset_name,depre_method,depre_unit,net_mny,depre_mny,depre_qty,net_qty,remark,update_user,update_time from asset_depre_list where acc_name='{0}' order by acc_name", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetDepreList model = new modAssetDepreList();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
                        model.NetMny = dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.DepreMny = dalUtility.ConvertToDecimal(rdr["depre_mny"]);
                        model.DepreQty = dalUtility.ConvertToDecimal(rdr["depre_qty"]);
                        model.NetQty = dalUtility.ConvertToDecimal(rdr["net_qty"]);
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

        public bool SaveDepreList(BindingCollection<modAssetDepreList> list, string accname, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    dalAccPeriodList dalp = new dalAccPeriodList();
                    modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                    string sql = string.Format("select count(1) from acc_credence_list where acc_name='{0}' and status=0 and credence_type='固定资产处理'", accname);
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                    {
                        emsg = "固定资产处理凭证未审核，折旧暂时不能进行！";
                        return false;
                    }
                    sql = string.Format("select count(1) from acc_credence_list where acc_name='{0}' and status=0 and credence_type='固定资产评估'", accname);
                    if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                    {
                        emsg = "固定资产评估凭证未审核，折旧暂时不能进行！";
                        return false;
                    }
                    sql = string.Format("delete asset_depre_list where acc_name='{0}'", accname);
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach (modAssetDepreList mod in list)
                    {
                        sql = string.Format("select count(1) from asset_sale where asset_id='{0}' and (status=0 or acc_seq=0) and form_date >= '" + modp.StartDate + "' and form_date <= '" + modp.EndDate + "'", mod.AssetId);
                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                        {
                            emsg = "资产 [" + mod.AssetId + "]" + mod.AssetName + " 正在被处理，折旧暂时不能进行！";
                            return false;
                        }
                        sql = string.Format("select count(1) from asset_evaluate where asset_id='{0}' and (status=0 or acc_seq=0) and form_date >= '" + modp.StartDate + "' and form_date <= '" + modp.EndDate + "'", mod.AssetId);
                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                        {
                            emsg = "资产 [" + mod.AssetId + "]" + mod.AssetName + " 正在被评估，折旧暂时不能进行！";
                            return false;
                        }
                        sql = string.Format("select count(1) from asset_list where asset_id='{0}' and status<>1", mod.AssetId);
                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                        {
                            emsg = "[" + mod.AssetId + "]" + mod.AssetName + " 不是有效资产，折旧暂时不能进行！";                            
                            return false;
                        }
                        sql = string.Format("insert into asset_depre_list(acc_name,asset_id,asset_name,depre_method,depre_unit,net_mny,depre_mny,depre_qty,net_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}',getdate())", accname, mod.AssetId, mod.AssetName, mod.DepreMethod, mod.DepreUnit, mod.NetMny, mod.DepreMny, mod.DepreQty, mod.NetQty, "", updateuser);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
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

        /// <summary>
        /// audit acc credence
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>seq</returns>
        public bool Audit(string accname, int? seq, string updateuser, out string emsg)
        {
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if(!Exists(accname, seq, 0, out emsg))
                {
                    emsg = "Data does not exists!";
                    return false;
                }
                modAccCredenceList mod = GetItem(accname, seq, false, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "It is already audited,you can not audit it again!";
                    return false;
                }
                string sql = string.Empty;                
                sql = string.Format("select * from acc_credence_detail where acc_name='{0}' and seq={1} order by detail_seq", accname, seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        decimal borrowmny = Convert.ToDecimal(rdr["borrow_money"]);
                        decimal lendmny = Convert.ToDecimal(rdr["lend_money"]);
                        decimal exchangerate = Convert.ToDecimal(rdr["exchange_rate"]);
                        if (mod.CredenceType.CompareTo("一般凭证") == 0)
                        {
                            switch (rdr["subject_id"].ToString().Trim())
                            {
                                case "1055":    //应收帐款
                                    if (borrowmny != 0)
                                    {                                        
                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, borrowmny, 0, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();                                        
                                    }
                                    if (lendmny != 0)
                                    {
                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, 0, lendmny, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        sql = string.Format("select count(1) from acc_credence_detail where acc_name='{0}' and seq={1} and subject_id='{2}'", accname, seq, "91353535");
                                        int iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                                        if (iCount == 1)
                                        {
                                            sql = string.Format("select count(1) from acc_credence_detail where acc_name='{0}' and seq={1} and subject_id<>'1055' and subject_id<>'91353535'", accname, seq);
                                            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                                            if (iCount == 0)
                                            {
                                                string actioncode = "BADDEBTS";
                                                dalCustomerList dalcust = new dalCustomerList();
                                                string salesman = dalcust.GetSalesMan(rdr["detail_id"].ToString());
                                                dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                                                modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);

                                                sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}',getdate())", rdr["detail_id"].ToString(), rdr["detail_name"].ToString(), actioncode, "货款折扣", salesman,accname + "-" + seq.ToString().Trim(), string.Empty, rdr["digest"].ToString(), string.Empty, string.Empty, string.Empty, string.Empty, modcsr.Scores * lendmny, -1, mod.UpdateUser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    break;
                                case "5145":    //应付帐款
                                    if (borrowmny != 0)
                                    {
                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, 0, borrowmny, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (lendmny != 0)
                                    {
                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, lendmny, 0, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                    break;
                                case "1060":    //其它应收款
                                    if (borrowmny != 0)
                                    {
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, borrowmny, 0, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (lendmny != 0)
                                    {
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, 0, lendmny, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }                                                    
                                    break;
                                case "5155":    //其它应付款
                                    if (borrowmny != 0)
                                    {
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, 0, borrowmny, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (lendmny != 0)
                                    {
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, rdr["detail_id"].ToString(), rdr["currency"].ToString(), exchangerate, 0, lendmny, 0, seq.ToString(), mod.CredenceType, rdr["digest"].ToString(), updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                    break;                                
                            }
                        }                        
                    }
                }
                sql = string.Format("update acc_credence_list set status=1,audit_man='{0}',audit_time=getdate() where acc_name='{1}' and seq={2}", updateuser, accname, seq);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                switch (mod.CredenceType)
                {
                    case "销售凭证":
                        #region 审核销售明细
                        dalSalesShipment dalsale = new dalSalesShipment();
                        BindingCollection<modSalesShipment> listsale = dalsale.GetIList(accname, seq, out emsg);
                        if (listsale != null && listsale.Count > 0)
                        {
                            foreach (modSalesShipment modsale in listsale)
                            {
                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modsale.CustId, modsale.Currency, modsale.ExchangeRate, 0, (modsale.DetailSum + modsale.OtherMny - modsale.KillMny) * modsale.AdFlag, 0, modsale.ShipId, modsale.ShipType, modsale.Remark, updateuser);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        break;
                    case "设计加工凭证":
                        #region 审核设计加工明细
                        dalSalesDesignForm daldesign = new dalSalesDesignForm();
                        BindingCollection<modSalesDesignForm> listdesign = daldesign.GetIList(accname, seq, out emsg);
                        if (listdesign != null && listdesign.Count > 0)
                        {
                            foreach (modSalesDesignForm moddesign in listdesign)
                            {
                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, moddesign.CustId, moddesign.Currency, moddesign.ExchangeRate, 0, moddesign.Mny * moddesign.AdFlag, 0, moddesign.Id.ToString(), moddesign.FormType, moddesign.Remark, updateuser);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        break;
                    case "采购凭证":
                        #region 审核采购收货明细
                        dalPurchaseList dalpurc = new dalPurchaseList();
                        BindingCollection<modPurchaseList> listpurc = dalpurc.GetIList(accname, seq, out emsg);
                        if (listpurc != null && listpurc.Count > 0)
                        {
                            foreach (modPurchaseList modpurc in listpurc)
                            {
                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpurc.VendorName, modpurc.Currency, modpurc.ExchangeRate, 0, (modpurc.DetailSum + modpurc.OtherMny - modpurc.KillMny) * modpurc.AdFlag, 0, modpurc.PurchaseId, modpurc.PurchaseType, modpurc.Remark, updateuser);                
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();

                                BindingCollection<modPurchaseDetail> listpur = dalpurc.GetDetail(modpurc.PurchaseId, out emsg);
                                if (listpur != null && listpur.Count > 0)
                                {
                                    foreach (modPurchaseDetail modpurdetail in listpur)
                                    {
                                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modpurdetail.ProductId, modpurdetail.Size, 0, 0, modpurc.AdFlag * modpurdetail.Qty, modpurc.AdFlag * modpurdetail.Qty * modpurdetail.Price * modpurdetail.ExchangeRate, 0, 0, modpurdetail.PurchaseId, modpurc.PurchaseType, modpurdetail.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        #endregion
                        break;                    
                    case "费用登记":
                        #region 审核收款单据明细
                        dalAccExpenseForm dalexp = new dalAccExpenseForm();
                        BindingCollection<modAccExpenseForm> listexp = dalexp.GetIList(accname, seq, out emsg);
                        if (listexp != null && listexp.Count > 0)
                        {
                            foreach (modAccExpenseForm modexp in listexp)
                            {                                
                                switch (modexp.SubjectId)
                                {
                                    case "5125":   //应付票据  增加
                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
													values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
													", accname, seq, modexp.CheckNo, modexp.SubjectId, modexp.BankName, modexp.CheckType, modexp.DetailId, mod.CredenceType, modexp.Id.ToString(), modexp.Currency, modexp.ExpenseMny, modexp.ExchangeRate, mod.CredenceDate, modexp.PromiseDate, 0, updateuser, modexp.ExpenseName);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
									case "1060":   //其它应收款  
										sql = string.Format(@"insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)
													values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())
													", mod.AccName, mod.AccSeq, mod.CredenceDate, modexp.DetailId, modexp.Currency, modexp.ExchangeRate, 0, 0, modexp.ExpenseMny, modexp.Id.ToString(), "费用单", modexp.Remark, updateuser);
										SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
										cmd.ExecuteNonQuery();
										break;
									case "5155":   //其它应付款
										sql = string.Format(@"insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)
													values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())
													", accname, seq, mod.CredenceDate, modexp.DetailId, modexp.Currency, modexp.ExchangeRate, 0, modexp.ExpenseMny, 0, modexp.Id.ToString(), "费用单", modexp.Remark, updateuser);
										SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
										cmd.ExecuteNonQuery();
										break;
									case "1075":   //应收票据
                                    case "1055":   //应收帐款
                                    case "5145":   //应付帐款                                                                      
                                    case "1235":   //库存商品
                                        trans.Rollback();
                                        emsg = "副方科目不允许！";
                                        return false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "收款凭证":
                        #region 审核收款单据明细
                        dalAccReceivableForm dalrecv = new dalAccReceivableForm();
                        BindingCollection<modAccReceivableForm> listrecv = dalrecv.GetIList(accname, seq, out emsg);
                        if (listrecv != null && listrecv.Count > 0)
                        {
                            foreach (modAccReceivableForm modrecv in listrecv)
                            {
                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modrecv.CustId, modrecv.Currency, modrecv.ExchangeRate, 0, 0, modrecv.ReceivableMny, modrecv.Id.ToString(), "收款单据", modrecv.Remark, updateuser);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();

                                switch(modrecv.SubjectId)
                                {
                                    case "1075":   //应收票据  增加
                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
											", accname, seq, modrecv.CheckNo, modrecv.SubjectId, modrecv.BankName, modrecv.CheckType, modrecv.DetailId, mod.CredenceType, modrecv.Id.ToString(), modrecv.Currency, modrecv.GetMny, modrecv.ExchangeRate, mod.CredenceDate, modrecv.PromiseDate, 0, updateuser, modrecv.CustName);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "5125":   //应付票据  减少
                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'(16}')", accname, seq, modrecv.CheckNo, modrecv.SubjectId, modrecv.BankName, modrecv.CheckType, modrecv.DetailId, mod.CredenceType, modrecv.Id.ToString(), modrecv.Currency, (-1) * modrecv.GetMny, modrecv.ExchangeRate, mod.CredenceDate, modrecv.PromiseDate, 0, updateuser, modrecv.CustName);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "1055":   //应收帐款
                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modrecv.DetailId, modrecv.Currency, modrecv.ExchangeRate, 0, modrecv.GetMny, 0, modrecv.Id.ToString(), "收款单", modrecv.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "1060":   //其它应收款
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modrecv.DetailId, modrecv.Currency, modrecv.ExchangeRate, 0, modrecv.GetMny, 0, modrecv.Id.ToString(), "收款单", modrecv.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;                                    
                                    case "5145":   //应付帐款
                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modrecv.DetailId, modrecv.Currency, modrecv.ExchangeRate, 0, 0, modrecv.GetMny, modrecv.Id.ToString(), "收款单", modrecv.Remark, updateuser);                
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "5155":   //其它应付款
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modrecv.DetailId, modrecv.Currency, modrecv.ExchangeRate, 0, 0, modrecv.GetMny, modrecv.Id.ToString(), "收款单", modrecv.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;                                    
                                    case "1235":   //库存商品
                                        trans.Rollback();
                                        emsg = "副方科目不允许！";
                                        return false;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "付款凭证":
                        #region 审核付款单据明细
                        dalAccPayableForm dalpay = new dalAccPayableForm();
                        BindingCollection<modAccPayableForm> listpay = dalpay.GetIList(accname, seq, out emsg);
                        if (listpay != null && listpay.Count > 0)
                        {
                            foreach (modAccPayableForm modpay in listpay)
                            {
                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpay.VendorName, modpay.Currency, modpay.ExchangeRate, 0, 0, modpay.PayableMny, modpay.Id.ToString(), "付款单", modpay.Remark, updateuser);                
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                                                                
                                switch (modpay.SubjectId)
                                {
                                    case "1075":   //应收票据  减少
                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
												values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
												", accname, seq, modpay.CheckNo, modpay.SubjectId, modpay.BankName, modpay.CheckType, modpay.DetailId, mod.CredenceType, modpay.Id.ToString(), modpay.Currency, (-1) * modpay.PaidMny, modpay.ExchangeRate, mod.CredenceDate, modpay.PromiseDate, 0, updateuser, modpay.VendorName);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "5125":   //应付票据  增加
                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
												values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
												", accname, seq, modpay.CheckNo, modpay.SubjectId, modpay.BankName, modpay.CheckType, modpay.DetailId, mod.CredenceType, modpay.Id.ToString(), modpay.Currency, modpay.PaidMny, modpay.ExchangeRate, mod.CredenceDate, modpay.PromiseDate, 0, updateuser, modpay.VendorName);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "1055":   //应收帐款
                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpay.DetailId, modpay.Currency, modpay.ExchangeRate, 0, 0, modpay.PaidMny, modpay.Id.ToString(), "付款单", modpay.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "1060":   //其它应收款  
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modpay.DetailId, modpay.Currency, modpay.ExchangeRate, 0, 0, modpay.PaidMny, modpay.Id.ToString(), "付款单", modpay.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "5145":   //应付帐款
                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpay.DetailId, modpay.Currency, modpay.ExchangeRate, 0, modpay.PaidMny, 0, modpay.Id.ToString(), "付款单", modpay.Remark, updateuser);                
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case "5155":   //其它应付款
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpay.DetailId, modpay.Currency, modpay.ExchangeRate, 0, modpay.PaidMny, 0, modpay.Id.ToString(), "付款单", modpay.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;                                    
                                    case "1235":   //库存商品
                                        trans.Rollback();
                                        emsg = "副方科目不允许！";
                                        return false;
                                }
                            }                            
                        }
                        #endregion
                        break;                    
                    case "其它应收凭证":
                        #region 审核其它应收款明细
                        dalAccOtherReceivableForm dalaorf = new dalAccOtherReceivableForm();
                        BindingCollection<modAccOtherReceivableForm> listaorf = dalaorf.GetIList(accname, seq, out emsg);
                        if (listaorf != null && listaorf.Count > 0)
                        {
                            foreach (modAccOtherReceivableForm modaorf in listaorf)
                            {
                                switch (modaorf.FormType)
                                {
                                    case "其它应收增加":
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.ObjectName, modaorf.Currency, modaorf.ExchangeRate, 0, modaorf.ReceivableMny, 0, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        switch (modaorf.SubjectId)
                                        {
                                            case "1075":   //应收票据  减少
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaorf.CheckNo, modaorf.SubjectId, modaorf.BankName, modaorf.CheckType, modaorf.DetailId, mod.CredenceType, modaorf.Id.ToString(), modaorf.Currency, (-1) * modaorf.GetMny, modaorf.ExchangeRate, mod.CredenceDate, modaorf.PromiseDate, 0, updateuser, modaorf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5125":   //应付票据  增加
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaorf.CheckNo, modaorf.SubjectId, modaorf.BankName, modaorf.CheckType, modaorf.DetailId, mod.CredenceType, modaorf.Id.ToString(), modaorf.Currency, modaorf.GetMny, modaorf.ExchangeRate, mod.CredenceDate, modaorf.PromiseDate, 0, updateuser, modaorf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1055":   //应收帐款
                                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, 0, modaorf.GetMny, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1060":   //其它应收款
                                                sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, 0, modaorf.GetMny, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5145":   //应付帐款
                                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, modaorf.GetMny, 0, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5155":   //其它应付款
                                                sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, modaorf.GetMny, 0, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;                                            
                                            case "1235":   //库存商品
                                                trans.Rollback();
                                                emsg = "副方科目不允许！";
                                                return false;
                                        }
                                        break;
                                    case "其它应收减少":
                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.ObjectName, modaorf.Currency, modaorf.ExchangeRate, 0, 0, modaorf.ReceivableMny, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        switch (modaorf.SubjectId)
                                        {
                                            case "1075":   //应收票据  增加
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaorf.CheckNo, modaorf.SubjectId, modaorf.BankName, modaorf.CheckType, modaorf.DetailId, mod.CredenceType, modaorf.Id.ToString(), modaorf.Currency, modaorf.GetMny, modaorf.ExchangeRate, mod.CredenceDate, modaorf.PromiseDate, 0, updateuser, modaorf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5125":   //应付票据  减少
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaorf.CheckNo, modaorf.SubjectId, modaorf.BankName, modaorf.CheckType, modaorf.DetailId, mod.CredenceType, modaorf.Id.ToString(), modaorf.Currency, (-1) * modaorf.GetMny, modaorf.ExchangeRate, mod.CredenceDate, modaorf.PromiseDate, 0, updateuser, modaorf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1055":   //应收帐款
                                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, modaorf.GetMny, 0, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1060":   //其它应收款
                                                sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, modaorf.GetMny, 0, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5145":   //应付帐款
                                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, 0, modaorf.GetMny, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5155":   //其它应付款
                                                sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaorf.DetailId, modaorf.Currency, modaorf.ExchangeRate, 0, 0, modaorf.GetMny, modaorf.Id.ToString(), modaorf.FormType, modaorf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;                                            
                                            case "1235":   //库存商品
                                                trans.Rollback();
                                                emsg = "副方科目不允许！";
                                                return false;
                                        }
                                        break;
                                }                                
                            }
                        }
                        #endregion
                        break;
                    case "其它应付凭证":
                        #region 审核其它应付款明细
                        dalAccOtherPayableForm dalaopf = new dalAccOtherPayableForm();
                        BindingCollection<modAccOtherPayableForm> listaopf = dalaopf.GetIList(accname, seq, out emsg);
                        if (listaopf != null && listaopf.Count > 0)
                        {
                            foreach (modAccOtherPayableForm modaopf in listaopf)
                            {
                                switch (modaopf.FormType)
                                {
                                    case "其它应付增加":
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.ObjectName, modaopf.Currency, modaopf.ExchangeRate, 0, modaopf.PayableMny, 0, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        switch (modaopf.SubjectId)
                                        {
                                            case "1075":   //应收票据  增加
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaopf.CheckNo, modaopf.SubjectId, modaopf.BankName, modaopf.CheckType, modaopf.DetailId, mod.CredenceType, modaopf.Id.ToString(), modaopf.Currency, modaopf.PaidMny, modaopf.ExchangeRate, mod.CredenceDate, modaopf.PromiseDate, 0, updateuser, modaopf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5125":   //应付票据  减少
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaopf.CheckNo, modaopf.SubjectId, modaopf.BankName, modaopf.CheckType, modaopf.DetailId, mod.CredenceType, modaopf.Id.ToString(), modaopf.Currency, (-1) * modaopf.PaidMny, modaopf.ExchangeRate, mod.CredenceDate, modaopf.PromiseDate, 0, updateuser, modaopf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1055":   //应收帐款
                                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, modaopf.PaidMny, 0, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1060":   //其它应收款
                                                sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, modaopf.PaidMny, 0, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5145":   //应付帐款
                                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, 0, modaopf.PaidMny, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5155":   //其它应付款
                                                sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, 0, modaopf.PaidMny, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;                                            
                                            case "1235":   //库存商品
                                                trans.Rollback();
                                                emsg = "副方科目不允许！";
                                                return false;
                                        }                                        
                                        break;
                                    case "其它应付减少":
                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.ObjectName, modaopf.Currency, modaopf.ExchangeRate, 0, 0, modaopf.PayableMny, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        switch (modaopf.SubjectId)
                                        {
                                            case "1075":   //应收票据  减少
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaopf.CheckNo, modaopf.SubjectId, modaopf.BankName, modaopf.CheckType, modaopf.DetailId, mod.CredenceType, modaopf.Id.ToString(), modaopf.Currency, (-1) * modaopf.PaidMny, modaopf.ExchangeRate, mod.CredenceDate, modaopf.PromiseDate, 0, updateuser, modaopf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5125":   //应付票据  增加
                                                sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
														values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
														", accname, seq, modaopf.CheckNo, modaopf.SubjectId, modaopf.BankName, modaopf.CheckType, modaopf.DetailId, mod.CredenceType, modaopf.Id.ToString(), modaopf.Currency, modaopf.PaidMny, modaopf.ExchangeRate, mod.CredenceDate, modaopf.PromiseDate, 0, updateuser, modaopf.ObjectName);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1055":   //应收帐款
                                                sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, 0, modaopf.PaidMny, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "1060":   //其它应收款
                                                sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, 0, modaopf.PaidMny, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5145":   //应付帐款
                                                sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, modaopf.PaidMny, 0, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;
                                            case "5155":   //其它应付款
                                                sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modaopf.DetailId, modaopf.Currency, modaopf.ExchangeRate, 0, modaopf.PaidMny, 0, modaopf.Id.ToString(), modaopf.FormType, modaopf.Remark, updateuser);
                                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                cmd.ExecuteNonQuery();
                                                break;                                            
                                            case "1235":   //库存商品
                                                trans.Rollback();
                                                emsg = "副方科目不允许！";
                                                return false;
                                        }
                                        break;
                                }                                
                            }
                        }
                        #endregion
                        break;                    
                    case "仓库进出":  //盘点盈溢，盘点损耗，借料入库，借料还出，借料出库，借料还入
                        #region 审核仓库进出明细
                        dalWarehouseInoutForm dalwif = new dalWarehouseInoutForm();
                        BindingCollection<modWarehouseInoutForm> listwif = dalwif.GetIList(accname, seq, out emsg);
                        if (listwif != null && listwif.Count > 0)
                        {
                            foreach (modWarehouseInoutForm modwif in listwif)
                            {
                                switch (modwif.InoutFlag)
                                {
                                    case 1:   //入库
                                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modwif.ProductId, modwif.Size, 0, 0, modwif.Qty, modwif.Size * modwif.Qty * modwif.CostPrice, 0, 0, modwif.Id, modwif.InoutType, modwif.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                    case -1:  //出库
                                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modwif.ProductId, modwif.Size, 0, 0, 0, 0, modwif.Qty, modwif.Size * modwif.Qty * modwif.CostPrice, modwif.Id, modwif.InoutType, modwif.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                        break;
                                }
                            }
                        }
                        #endregion
                        break;
                    case "生产凭证":
                        #region 审核生产配料明细
                        dalProductionForm dalpdt = new dalProductionForm();
                        BindingCollection<modProductionForm> listpdt = dalpdt.GetIList(accname, seq, out emsg);
                        if (listpdt != null && listpdt.Count > 0)
                        {
                            foreach (modProductionForm modpdt in listpdt)
                            {
                                if (modpdt.ProcessMny != 0)   //其他应付款 增加
                                {
                                    sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modpdt.DeptId, modpdt.Currency, modpdt.ExchangeRate, 0, modpdt.ProcessMny + modpdt.OtherMny - modpdt.KillMny, 0, modpdt.FormId, modpdt.FormType, modpdt.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                }
                                BindingCollection<modProductionFormWare> listware = dalpdt.GetProductionFormWare(modpdt.FormId, out emsg);
                                if (listware != null && listware.Count > 0)
                                {
                                    foreach (modProductionFormWare modware in listware)
                                    {
                                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modware.ProductId, modware.Size, 0, 0, modware.Qty, decimal.Round(modware.Size * modware.Qty * modware.CostPrice,2), 0, 0, modpdt.FormId, modpdt.FormType, modware.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                BindingCollection<modProductionFormMaterial> listmaterial = dalpdt.GetProductionFormMaterial(modpdt.FormId, out emsg);
                                if (listmaterial != null && listmaterial.Count > 0)
                                {
                                    foreach (modProductionFormMaterial modmaterial in listmaterial)
                                    {
                                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modmaterial.ProductId, modmaterial.Size, 0, 0, 0, 0, modmaterial.Qty, decimal.Round(modmaterial.Size * modmaterial.Qty * modmaterial.CostPrice, 2), modpdt.FormId, modpdt.FormType, modmaterial.Remark, updateuser);
                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case "支票承兑":
                        #region 审核支票承兑明细
                        dalAccCheckForm dalacf = new dalAccCheckForm();
                        BindingCollection<modAccCheckForm> listacf = dalacf.GetIList(accname, seq, out emsg);
                        if (listacf != null && listacf.Count > 0)
                        {
                            foreach (modAccCheckForm modacf in listacf)
                            {
                                BindingCollection<modAccCheckFormDetail> listdetailacf = dalacf.GetDetail(modacf.FormId, out emsg);
                                if (listdetailacf != null && listdetailacf.Count > 0)
                                {
                                    foreach (modAccCheckFormDetail modacfdetail in listdetailacf)
                                    {
                                        switch (modacf.SubjectId)
                                        {
                                            case "1075":    //应收票据
                                                switch(modacfdetail.SubjectId)
                                                {
                                                    case "1075":   //应收票据  增加
                                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
																values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
																", accname, seq, modacfdetail.CheckNo, modacfdetail.SubjectId, modacfdetail.BankName, modacfdetail.CheckType, modacfdetail.DetailId, mod.CredenceType, modacfdetail.FormId, modacfdetail.Currency, modacfdetail.Mny, modacfdetail.ExchangeRate, mod.CredenceDate, modacfdetail.PromiseDate, 0, updateuser, modacfdetail.Remark);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;                                                    
                                                    case "1055":   //应收帐款  增加
                                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, modacfdetail.Mny, 0, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "1060":   //其它应收款  增加
                                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, modacfdetail.Mny, 0, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5125":   //应付票据  减少
                                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
																values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
																", accname, seq, modacfdetail.CheckNo, modacfdetail.SubjectId, modacfdetail.BankName, modacfdetail.CheckType, modacfdetail.DetailId, mod.CredenceType, modacfdetail.FormId, modacfdetail.Currency, (-1) * modacfdetail.Mny, modacfdetail.ExchangeRate, mod.CredenceDate, modacfdetail.PromiseDate, 0, updateuser, modacfdetail.Remark);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5145":   //应付帐款   减少
                                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, 0, modacfdetail.Mny, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5155":   //其它应付款   减少
                                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, 0, modacfdetail.Mny, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "1235":   //库存商品
                                                        trans.Rollback();
                                                        emsg = "副方科目不允许！";
                                                        return false;
                                                }
                                                break;
                                            case "5125":    //应付票据
                                                switch (modacfdetail.SubjectId)
                                                {
                                                    case "1075":   //应收票据  减少
                                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
																values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
																", accname, seq, modacfdetail.CheckNo, modacfdetail.SubjectId, modacfdetail.BankName, modacfdetail.CheckType, modacfdetail.DetailId, mod.CredenceType, modacfdetail.FormId, modacfdetail.Currency, (-1) * modacfdetail.Mny, modacfdetail.ExchangeRate, mod.CredenceDate, modacfdetail.PromiseDate, 0, updateuser, modacfdetail.Remark);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break; 
                                                    case "1055":   //应收帐款   减少
                                                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, 0, modacfdetail.Mny, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "1060":   //其它应收款  减少
                                                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, 0, modacfdetail.Mny, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5125":   //应付票据  增加
                                                        sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
																values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
																", accname, seq, modacfdetail.CheckNo, modacfdetail.SubjectId, modacfdetail.BankName, modacfdetail.CheckType, modacfdetail.DetailId, mod.CredenceType, modacfdetail.FormId, modacfdetail.Currency, modacfdetail.Mny, modacfdetail.ExchangeRate, mod.CredenceDate, modacfdetail.PromiseDate, 0, updateuser, modacfdetail.Remark);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5145":   //应付帐款   增加
                                                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, modacfdetail.Mny, 0, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "5155":   //其它应付款  增加
                                                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modacfdetail.DetailId, modacfdetail.Currency, modacfdetail.ExchangeRate, 0, modacfdetail.Mny, 0, modacf.FormId, modacf.SubjectId, modacfdetail.Remark, updateuser);
                                                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                                        cmd.ExecuteNonQuery();
                                                        break;
                                                    case "1235":   //库存商品
                                                        trans.Rollback();
                                                        emsg = "副方科目不允许！";
                                                        return false;
                                                }
                                                break;
                                        }
                                    }
                                }
                                sql = string.Format("update acc_check_list set status=1,get_date='{0}' where id={1}", mod.CredenceDate, modacf.CheckId);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        break;
                    case "固定资产增加":
                        #region 审核固定资产增加明细
                        dalAssetAdd dalasset = new dalAssetAdd();
                        BindingCollection<modAssetAdd> listasset = dalasset.GetIList(accname, seq, out emsg);
                        int assid = 0;
                        string assetid = string.Empty;
                        foreach (modAssetAdd modasset in listasset)
                        {
                            for (int i = 0; i < modasset.Qty; i++)
                            {
                                assid++;
                                if (assid == 1)
                                {
                                    dalAssetList dall = new dalAssetList();
                                    assetid = dall.GetNewId(modasset.FormDate);
                                }
                                else
                                    assetid = assetid.Substring(0, 9) + (Convert.ToInt32(assetid.Substring(9, 4)) + 1).ToString().Trim().PadLeft(4, '0');

                                sql = string.Format("insert into asset_list(asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name,acc_seq)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},'{12}','{13}','{14}',getdate(),'{15}',{16})", assetid, modasset.AssetName, string.Empty, 1, modasset.FormDate, modasset.FormDate, string.Empty, string.Empty, string.Empty, 0, modasset.Price, 0, string.Empty, modasset.Remark, updateuser, accname, seq);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                            switch (modasset.SubjectId)
                            {
                                case "1075":   //应收票据  减少
                                    sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
											", accname, seq, modasset.CheckNo, modasset.SubjectId, modasset.BankName, modasset.CheckType, modasset.DetailId, mod.CredenceType, modasset.FormId, modasset.Currency, (-1) * modasset.Qty * modasset.Price, modasset.ExchangeRate, mod.CredenceDate, modasset.PromiseDate, 0, updateuser, modasset.AssetName);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1055":   //应收帐款   减少
                                    sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modasset.DetailId, modasset.Currency, modasset.ExchangeRate, 0, 0, modasset.Qty * modasset.Price, modasset.FormId, modasset.SubjectId, modasset.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1060":   //其它应收款  减少
                                    sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modasset.DetailId, modasset.Currency, modasset.ExchangeRate, 0, 0, modasset.Qty * modasset.Price, modasset.FormId, modasset.SubjectId, modasset.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5125":   //应付票据  增加
                                    sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
											", accname, seq, modasset.CheckNo, modasset.SubjectId, modasset.BankName, modasset.CheckType, modasset.DetailId, mod.CredenceType, modasset.FormId, modasset.Currency, modasset.Qty * modasset.Price, modasset.ExchangeRate, mod.CredenceDate, modasset.PromiseDate, 0, updateuser, modasset.AssetName);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5145":   //应付帐款   增加
                                    sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modasset.DetailId, modasset.Currency, modasset.ExchangeRate, 0, modasset.Qty * modasset.Price, 0, modasset.FormId, modasset.SubjectId, modasset.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5155":   //其它应付款  增加
                                    sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modasset.DetailId, modasset.Currency, modasset.ExchangeRate, 0, modasset.Qty * modasset.Price, 0, modasset.FormId, modasset.SubjectId, modasset.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1235":   //库存商品
                                    trans.Rollback();
                                    emsg = "副方科目不允许！";
                                    return false;
                            }
                        }
                        #endregion
                        break;
                    case "固定资产处理":
                        #region 审核固定资产处理明细
                        dalAssetSale dalassetsale = new dalAssetSale();
                        BindingCollection<modAssetSale> listassetsale = dalassetsale.GetIList(accname, seq, out emsg);
                        foreach (modAssetSale modassetsale in listassetsale)
                        {
                            sql = string.Format("update asset_list set status=7 where asset_id='{0}'", modassetsale.AssetId);
                            SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                            cmd.ExecuteNonQuery();
                            switch (modassetsale.SubjectId)
                            {
                                case "1075":   //应收票据  增加
                                    sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
											", accname, seq, modassetsale.CheckNo, modassetsale.SubjectId, modassetsale.BankName, modassetsale.CheckType, modassetsale.DetailId, mod.CredenceType, modassetsale.FormId, modassetsale.Currency, modassetsale.SaleMny, modassetsale.ExchangeRate, mod.CredenceDate, modassetsale.PromiseDate, 0, updateuser, modassetsale.AssetName);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1055":   //应收帐款  增加
                                    sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modassetsale.DetailId, modassetsale.Currency, modassetsale.ExchangeRate, 0, modassetsale.SaleMny, 0, modassetsale.FormId, modassetsale.SubjectId, modassetsale.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1060":   //其它应收款  增加
                                    sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modassetsale.DetailId, modassetsale.Currency, modassetsale.ExchangeRate, 0, modassetsale.SaleMny, 0, modassetsale.FormId, modassetsale.SubjectId, modassetsale.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5125":   //应付票据  减少
                                    sql = string.Format(@"insert into acc_check_list(acc_name,acc_seq,check_no,subject_id,bank_name,check_type,account_no,form_type,form_id,currency,mny,exchange_rate,create_date,promise_date,status,update_user,update_time,remark)
											values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},'{15}',getdate(),'{16}')
											", accname, seq, modassetsale.CheckNo, modassetsale.SubjectId, modassetsale.BankName, modassetsale.CheckType, modassetsale.DetailId, mod.CredenceType, modassetsale.FormId, modassetsale.Currency, (-1) * modassetsale.SaleMny, modassetsale.ExchangeRate, mod.CredenceDate, modassetsale.PromiseDate, 0, updateuser, modassetsale.AssetName);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5145":   //应付帐款   减少
                                    sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", mod.AccName, mod.AccSeq, mod.CredenceDate, modassetsale.DetailId, modassetsale.Currency, modassetsale.ExchangeRate, 0, 0, modassetsale.SaleMny, modassetsale.FormId, modassetsale.SubjectId, modassetsale.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5155":   //其它应付款   减少
                                    sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}','{4}',{5},{6},{7},{8},'{9}','{10}','{11}','{12}',getdate())", accname, seq, mod.CredenceDate, modassetsale.DetailId, modassetsale.Currency, modassetsale.ExchangeRate, 0, 0, modassetsale.SaleMny, modassetsale.FormId, modassetsale.SubjectId, modassetsale.Remark, updateuser);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1235":   //库存商品
                                    trans.Rollback();
                                    emsg = "副方科目不允许！";
                                    return false;
                            }
                        }
                        #endregion
                        break;
                    case "固定资产评估":
                        #region 审核固定资产评估明细
                        //dalAssetEvaluate dalassetevaluate = new dalAssetEvaluate();
                        //BindingCollection<modAssetEvaluate> listassetevaluate = dalassetevaluate.GetIList(accname, seq, out emsg);
                        //foreach (modAssetEvaluate modassetevaluate in listassetevaluate)
                        //{
                        //    sql = string.Format("update asset_list set net_mny=net_mny + {0} where asset_id='{1}'", modassetevaluate.EvaluateMny - modassetevaluate.NetMny, modassetevaluate.AssetId);
                        //    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        //    cmd.ExecuteNonQuery();
                        //}
                        #endregion
                        break;
                    case "资产折旧":
                        #region 审核资产折旧明细
                        sql = string.Format("update asset_depre_list set status=1 where acc_name='{0}'", accname);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        #endregion
                        break;
                    case "价格调整":
                        #region 审核价格调整明细
                        dalPriceAdjustForm dalpaf = new dalPriceAdjustForm();
                        BindingCollection<modPriceAdjustDetail> listpaf = dalpaf.GetCredenceList(accname, seq, out emsg);
                        if (listpaf != null && listpaf.Count > 0)
                        {
                            foreach (modPriceAdjustDetail modpaf in listpaf)
                            {
                                sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('{0}',{1},'{2}','{3}',{4},{5},{6},{7},{8},{9},{10},'{11}','{12}','{13}','{14}',getdate())", accname, seq, mod.CredenceDate, modpaf.ProductId, 1, 0, 0, 0, modpaf.Differ, 0, 0, modpaf.FormId, 1, modpaf.Remark, updateuser);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                        break;
                    case "零库清理":
                        sql = string.Format("update acc_product_inout set start_mny=0 where acc_name='{0}' and start_qty2=0 and start_mny2<>0", accname);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
                trans.Commit();
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// reset acc credence
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>seq</returns>
        public bool Reset(string accname, int? seq,string updateuser, out string emsg)
        {
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if(!Exists(accname,seq,1, out emsg))
                {
                    emsg = "Data does not exists!";
                    return false;
                }
                modAccCredenceList mod = GetItem(accname, seq, false, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "It has not audited yet,you can not reset it!";
                    return false;
                }
                string sql = string.Empty;                
                sql = string.Format("select * from acc_credence_detail where acc_name='{0}' and seq={1} order by detail_seq", accname, seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (mod.CredenceType.CompareTo("一般凭证") == 0)
                        {
                            switch (rdr["subject_id"].ToString().Trim())
                            {
                                case "1055":    //应收帐款
                                    sql = string.Format("delete customer_log where action_code='BADDEBTS' and form_id='{0}'", accname + "-" + seq.ToString().Trim());
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "5145":    //应付帐款
                                    sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                                case "1060":    //其它应收款
                                    sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();                                    
                                    break;
                                case "5155":    //其它应付款
                                    sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                                    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                    cmd.ExecuteNonQuery();
                                    break;
                            }
                        }  
                    }
                }                
                switch (mod.CredenceType)
                {
                    case "销售凭证":
                    case "设计加工凭证":
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "采购凭证":
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "费用登记":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
						sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
						SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
						cmd.ExecuteNonQuery();
						sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
						SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
						cmd.ExecuteNonQuery();
						break;
                    case "收款凭证":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "付款凭证":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;                    
                    case "其它应收凭证":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "其它应付凭证":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "仓库进出":  //盘点盈溢，盘点损耗，借料入库，借料还出，借料出库，借料还入
                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "生产凭证":
                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "支票承兑":
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();                        
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        dalAccCheckForm dalacf = new dalAccCheckForm();
                        BindingCollection<modAccCheckForm> listacf = dalacf.GetIList(accname, seq, out emsg);
                        if (listacf != null && listacf.Count > 0)
                        {
                            foreach (modAccCheckForm modacf in listacf)
                            {                                
                                sql = string.Format("update acc_check_list set status=0,get_date=null where id={0}", modacf.CheckId);
                                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        break;
                    case "固定资产增加":
                        dalAssetList dalasset = new dalAssetList();
                        BindingCollection<modAssetList> listasset = dalasset.GetIList(accname, seq, out emsg);
                        foreach (modAssetList modasset in listasset)
                        {
                            if (modasset.RawMny != modasset.NetMny)
                            {
                                emsg = "该资产已有折旧，不能重置！";
                                trans.Rollback();
                                return false;
                            }
                            if (mod.Status == 7)
                            {
                                emsg = "资产[" + modasset.AssetId + "]" + modasset.AssetName + " 已被处理，不能重置！";
                                trans.Rollback();
                                return false;
                            }
                        }
                        sql = string.Format("delete asset_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "固定资产处理":
                        dalAssetSale dalassetsale = new dalAssetSale();
                        BindingCollection<modAssetSale> listassetsale = dalassetsale.GetIList(accname, seq, out emsg);
                        foreach (modAssetSale modassetsale in listassetsale)
                        {
                            sql = string.Format("update asset_list set status=1 where asset_id='{0}'", modassetsale.AssetId);
                            SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                            cmd.ExecuteNonQuery();
                        }
                        sql = string.Format("delete acc_check_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "固定资产评估":
                        //dalAssetEvaluate dalassetevaluate = new dalAssetEvaluate();
                        //BindingCollection<modAssetEvaluate> listassetevaluate = dalassetevaluate.GetIList(accname, seq, out emsg);
                        //foreach (modAssetEvaluate modassetevaluate in listassetevaluate)
                        //{
                        //    sql = string.Format("update asset_list set net_mny=net_mny + {0} where asset_id='{1}'", modassetevaluate.NetMny - modassetevaluate.EvaluateMny, modassetevaluate.AssetId);
                        //    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        //    cmd.ExecuteNonQuery();
                        //}
                        break;
                    case "资产折旧":
                        sql = string.Format("select count(1) from asset_list a inner join asset_depre_list b on a.asset_id=b.asset_id where a.status=7 and b.acc_name='{0}'", accname);
                        if (Convert.ToInt32(SqlHelper.ExecuteScalar(sql)) > 0)
                        {
                            emsg = "资产已被处理，折旧不能重置！";
                            trans.Rollback();
                            return false;
                        }
                        sql = string.Format("update asset_depre_list set status=0 where acc_name='{0}'", accname);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    //case "月末结算":
                    //    sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq={1}", accname, seq);
                    //    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                    //    cmd.ExecuteNonQuery();
                    //    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and acc_seq={1}", accname, seq);
                    //    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                    //    cmd.ExecuteNonQuery();
                    //    sql = string.Format("delete acc_credence_list where acc_name='{0}' and acc_seq={1}", accname, seq);
                    //    SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                    //    cmd.ExecuteNonQuery();
                    //    break;
                    case "价格调整":
                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq={1}", accname, seq);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    case "零库清理":
                        sql = string.Format("update acc_product_inout set start_mny=start_mny2 where acc_name='{0}' and start_qty2=0 and start_mny2<>0", accname);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                        break;
                    default:
                        break;
                }
                sql = string.Format("update acc_credence_list set status=0,audit_man='',audit_time=null where acc_name='{0}' and seq={1}", accname, seq);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                trans.Commit();
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname, int? seq, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_credence_list where acc_name='{0}' and seq={1} ", accname, seq);
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

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=status>status</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname, int? seq, int status, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_credence_list where acc_name='{0}' and seq={1} and status={2} ", accname, seq, status);
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

        /// <summary>
        /// depre exist or not
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ExistDepre(string accname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_credence_list where acc_name='{0}' and credence_type='资产折旧' ", accname);
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
