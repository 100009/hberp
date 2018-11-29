using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalAccOtherReceivableForm
    {
        /// <summary>
        /// get all accotherreceivableform
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accotherreceivableform</returns>
        public BindingCollection<modAccOtherReceivableForm> GetIList(string statuslist, string idlist, string objectlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherReceivableForm> modellist = new BindingCollection<modAccOtherReceivableForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string objectwhere = string.Empty;
                if (!string.IsNullOrEmpty(objectlist) && objectlist.CompareTo("ALL") != 0)
                    objectwhere = "and a.object_name in ('" + objectlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.id,a.status,a.form_date,a.form_type,a.ad_flag,a.no,a.object_name,a.currency,a.exchange_rate,a.get_mny,a.receivable_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_other_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where 1=1 " + statuswhere + idwhere + objectwhere + formdatewhere + " order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccOtherReceivableForm model = new modAccOtherReceivableForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.GetMny=dalUtility.ConvertToDecimal(rdr["get_mny"]);
                        model.ReceivableMny=dalUtility.ConvertToDecimal(rdr["receivable_mny"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// get all accotherreceivableform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accotherreceivableform</returns>
        public BindingCollection<modAccOtherReceivableForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherReceivableForm> modellist = new BindingCollection<modAccOtherReceivableForm>();
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.status,a.form_date,a.form_type,a.ad_flag,a.no,a.object_name,a.currency,a.exchange_rate,a.get_mny,a.receivable_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_other_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.acc_name='{0}' and a.acc_seq={1} order by a.id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccOtherReceivableForm model = new modAccOtherReceivableForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.GetMny=dalUtility.ConvertToDecimal(rdr["get_mny"]);
                        model.ReceivableMny=dalUtility.ConvertToDecimal(rdr["receivable_mny"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        ///<returns>get a record detail of accotherreceivableform</returns>
        public modAccOtherReceivableForm GetItem(int id, out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.status,a.form_date,a.form_type,a.ad_flag,a.no,a.object_name,a.currency,a.exchange_rate,a.get_mny,a.receivable_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_other_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.id={0} order by a.id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccOtherReceivableForm model = new modAccOtherReceivableForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.GetMny=dalUtility.ConvertToDecimal(rdr["get_mny"]);
                        model.ReceivableMny=dalUtility.ConvertToDecimal(rdr["receivable_mny"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modAccOtherReceivableForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccOtherReceivableForm> modellist = new BindingCollection<modAccOtherReceivableForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.id,a.status,a.form_date,a.form_type,a.ad_flag,a.no,a.object_name,a.currency,a.exchange_rate,a.get_mny,a.receivable_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_other_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + " order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccOtherReceivableForm model = new modAccOtherReceivableForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ObjectName = dalUtility.ConvertToString(rdr["object_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.GetMny = dalUtility.ConvertToDecimal(rdr["get_mny"]);
                        model.ReceivableMny = dalUtility.ConvertToDecimal(rdr["receivable_mny"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan = dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime = dalUtility.ConvertToDateTime(rdr["audit_time"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// insert a accotherreceivableform
        /// <summary>
        /// <param name=mod>model object of accotherreceivableform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccOtherReceivableForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_other_receivable_form(status,form_date,form_type,no,ad_flag,object_name,currency,exchange_rate,get_mny,receivable_mny,subject_id,detail_id,detail_name,check_no,check_type,bank_name,promise_date,remark,update_user,update_time)values({0},'{1}','{2}','{3}',{4},'{5}','{6}',{7},{8},{9},'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',getdate())", mod.Status, mod.FormDate, mod.FormType, mod.No, mod.AdFlag, mod.ObjectName, mod.Currency, mod.ExchangeRate, mod.GetMny, mod.ReceivableMny, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, mod.Remark, mod.UpdateUser);
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
        /// update a accotherreceivableform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accotherreceivableform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id,modAccOtherReceivableForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_other_receivable_form set status={0},form_date='{1}',form_type='{2}',no='{3}',ad_flag={4},object_name='{5}',currency='{6}',exchange_rate={7},get_mny={8},receivable_mny={9},subject_id='{10}',detail_id='{11}',detail_name='{12}',check_no='{13}',check_type='{14}',bank_name='{15}',promise_date='{16}',remark='{17}' where id={18}", mod.Status, mod.FormDate, mod.FormType, mod.No, mod.AdFlag, mod.ObjectName, mod.Currency, mod.ExchangeRate, mod.GetMny, mod.ReceivableMny, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, mod.Remark, id);
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
        /// delete a accotherreceivableform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_other_receivable_form where id='{0}' ",id);
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
        /// audit
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(int id, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                sql = string.Format("update acc_other_receivable_form set status={0},audit_man='{1}',audit_time=getdate() where id={2}", 1, updateuser, id);
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
            finally
            {
                trans.Dispose();
                cmd.Dispose();
                if (conn.State != ConnectionState.Closed)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// reset
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(int id, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                sql = string.Format("update acc_other_receivable_form set status={0},audit_man='{1}',audit_time=null where id={2}", 0, updateuser, id);
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
            finally
            {
                trans.Dispose();
                cmd.Dispose();
                if (conn.State != ConnectionState.Closed)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string id, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_other_receivable_form where id='{0}' ",id);
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
