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
    public class dalAccCheckForm
    {
        /// <summary>
        /// get all acccheckform
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccheckform</returns>
        public BindingCollection<modAccCheckForm> GetIList(string statuslist, string formlist, string idlist, string subjectlist, string checkno, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckForm> modellist = new BindingCollection<modAccCheckForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formlist) && formlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formlist.Replace(",", "','") + "') ";

                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.check_id in ('" + idlist.Replace(",", "','") + "') ";

                string subjectidwhere = string.Empty;
                if (!string.IsNullOrEmpty(subjectlist) && subjectlist.CompareTo("ALL") != 0)
                    subjectidwhere = "and b.subject_id in ('" + subjectlist.Replace(",", "','") + "') ";

                string checknowhere = string.Empty;
                if (!string.IsNullOrEmpty(checkno))
                    checknowhere = "and b.check_no like '%" + checkno + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select a.form_id,a.form_date,a.status,a.check_id,b.check_no,b.bank_name,b.subject_id,c.subject_name,b.currency,b.mny,b.exchange_rate,a.remark,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where 1=1 " + statuswhere + formidwhere + idwhere + subjectidwhere + checknowhere + formdatewhere + "order by a.form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckForm model = new modAccCheckForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CheckId=dalUtility.ConvertToInt(rdr["check_id"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get all acccheckform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccheckform</returns>
        public BindingCollection<modAccCheckForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckForm> modellist = new BindingCollection<modAccCheckForm>();
                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.form_date,a.status,a.check_id,b.check_no,b.check_type,b.bank_name,b.subject_id,c.subject_name,b.currency,b.mny,b.exchange_rate,a.remark,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where a.acc_name='{0}' and a.acc_seq={1} order by a.form_id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckForm model = new modAccCheckForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CheckId=dalUtility.ConvertToInt(rdr["check_id"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modAccCheckForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckForm> modellist = new BindingCollection<modAccCheckForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.form_id,a.form_date,a.status,a.check_id,b.check_no,b.check_type,b.bank_name,b.subject_id,c.subject_name,b.currency,b.mny,b.exchange_rate,a.remark,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + "order by a.form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckForm model = new modAccCheckForm();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CheckId = dalUtility.ConvertToInt(rdr["check_id"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get table record
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccheckform</returns>
        public modAccCheckForm GetItem(string formid,out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.form_date,a.status,a.check_id,b.check_no,b.check_type,b.bank_name,b.subject_id,c.subject_name,b.currency,b.mny,b.exchange_rate,a.remark,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where a.form_id='{0}' order by form_id", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCheckForm model = new modAccCheckForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CheckId=dalUtility.ConvertToInt(rdr["check_id"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
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
		/// get table record
		/// <summary>
		/// <param name=formid>formid</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of acccheckform</returns>
		public modAccCheckForm GetItembyCheckid(int checkid, out string emsg)
		{
			try
			{
				//Execute a query to read the categories
				string sql = string.Format("select a.form_id,a.form_date,a.status,a.check_id,b.check_no,b.check_type,b.bank_name,b.subject_id,c.subject_name,b.currency,b.mny,b.exchange_rate,a.remark,a.update_user,a.update_time,a.acc_name,a.acc_seq "
						+ "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where a.check_id={0} order by form_id", checkid);
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
				{
					if (rdr.Read())
					{
						modAccCheckForm model = new modAccCheckForm();
						model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
						model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
						model.Status = dalUtility.ConvertToInt(rdr["status"]);
						model.CheckId = dalUtility.ConvertToInt(rdr["check_id"]);
						model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
						model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
						model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
						model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
						model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
						model.Currency = dalUtility.ConvertToString(rdr["currency"]);
						model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
						model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
						model.Remark = dalUtility.ConvertToString(rdr["remark"]);
						model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
						model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
						model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
						model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
						emsg = null;
						return model;
					}
					else
					{
						emsg = "Error on read data";
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				emsg = dalUtility.ErrorMessage(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// get all acccheckformdetail
		/// <summary>
		/// <param name=formid>formid</param>
		/// <param name=out emsg>return error message</param>
		///<returns>details of all acccheckformdetail</returns>
		public BindingCollection<modAccCheckFormDetail> GetDetail(string formid, out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckFormDetail> modellist = new BindingCollection<modAccCheckFormDetail>();
                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.seq,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.currency,mny,a.exchange_rate,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark "
                        + "from acc_check_form_detail a inner join acc_subject_list b on a.subject_id=b.subject_id where a.form_id='{0}' order by a.form_id,a.seq", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckFormDetail model = new modAccCheckFormDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
        /// get new form id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime formdate)
        {
            string temp = formdate.ToString("yyyyMM");
            string formid = "ZP" + temp + "-";
            string sql = "select max(form_id) from acc_check_form where form_id like '" + formid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(formid, "").Trim()) + 1;
                formid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                formid += "0001";
            }
            return formid;
        }

        /// <summary>
        /// insert a account check form
        /// <summary>
        /// <param name=mod>model object of salesshipment</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modAccCheckForm mod, BindingCollection<modAccCheckFormDetail> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string formid = mod.FormId;
                    int? seq = 0;
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            if (Exists(formid, out emsg))
                                formid = GetNewId(mod.FormDate);
                            sql = string.Format("insert into acc_check_form(form_id,form_date,check_id,remark,update_user,update_time)values('{0}','{1}',{2},'{3}','{4}',getdate())", formid, mod.FormDate, mod.CheckId, mod.Remark, mod.UpdateUser);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modAccCheckFormDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into acc_check_form_detail(form_id,seq,subject_id,detail_id,detail_name,currency,mny,exchange_rate,check_no,check_type,bank_name,promise_date,remark)values('{0}',{1},'{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}','{10}','{11}','{12}')", formid, seq, modd.SubjectId, modd.DetailId, modd.DetailName, modd.Currency, modd.Mny, modd.ExchangeRate, modd.CheckNo, modd.CheckType, modd.BankName, modd.PromiseDate, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update acc_check_form set form_date='{0}',check_id={1},remark='{2}' where form_id='{3}'", mod.FormDate, mod.CheckId, mod.Remark, mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete acc_check_form_detail where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modAccCheckFormDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into acc_check_form_detail(form_id,seq,subject_id,detail_id,detail_name,currency,mny,exchange_rate,check_no,check_type,bank_name,promise_date,remark)values('{0}',{1},'{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}','{10}','{11}','{12}')", formid, seq, modd.SubjectId, modd.DetailId, modd.DetailName, modd.Currency, modd.Mny, modd.ExchangeRate, modd.CheckNo, modd.CheckType, modd.BankName, modd.PromiseDate, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete acc_check_form_detail where form_id='{0}'", mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete acc_check_form where form_id='{0}'", mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                    }

                    transaction.Complete();//就这句就可以了。  
                    emsg = formid;
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
        /// audit sales shipment
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modAccCheckForm mod = GetItem(formid, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }
                sql = string.Format("update acc_check_list set status=1,get_date=getdate() where id={0}", mod.CheckId);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update acc_check_form set status={0},audit_man='{1}',audit_time=getdate() where form_id='{2}'", 1, updateuser, formid);
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
        /// reset sales shipment
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modAccCheckForm mod = GetItem(formid, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("update acc_check_list set status=0,get_date=null where id={0}", mod.CheckId);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update acc_check_form set status={0},audit_man='{1}',audit_time=getdate() where form_id='{2}'", 0, updateuser, formid);
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
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string formid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_check_form where form_id='{0}' ",formid);
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
