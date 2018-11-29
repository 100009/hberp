using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;
using System.Collections.Generic;

namespace LXMS.DAL
{
    public class dalAccSubjectList
    {
        /// <summary>
        /// get all accsubjectlist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accsubjectlist</returns>
        public List<modAccSubjectList> GetAllList(bool leftpad, out string emsg)
        {
            try
            {
                List<modAccSubjectList> modellist = new List<modAccSubjectList>();
                //Execute a query to read the categories
                string sql = @"select subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,has_children,check_flag,lock_flag,select_flag,update_user,update_time 
							from acc_subject_list where psubject_id is null or psubject_id='' order by subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccSubjectList model = new modAccSubjectList();
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        if (leftpad)
                            model.SubjectName = GetLeftBlank(model.SubjectId) + dalUtility.ConvertToString(rdr["subject_name"]);
                        else
                            model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        //model.PSubjectId = dalUtility.ConvertToString(rdr["psubject_id"]);
                        model.AssistantCode = dalUtility.ConvertToString(rdr["assistant_code"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CheckCurrency = dalUtility.ConvertToInt(rdr["check_currency"]);
                        model.IsTradecompany = dalUtility.ConvertToInt(rdr["is_tradecompany"]);
                        model.IsQuantity = dalUtility.ConvertToInt(rdr["is_quantity"]);
                        model.HasChildren = dalUtility.ConvertToInt(rdr["has_children"]);
                        model.CheckFlag = dalUtility.ConvertToInt(rdr["check_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.SelectFlag = dalUtility.ConvertToInt(rdr["select_flag"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                        List<modAccSubjectList> listsub = GetIList(model.SubjectId, leftpad, out emsg);
                        if (listsub != null && listsub.Count > 0)
                        {
                            foreach (modAccSubjectList modsub in listsub)
                                modellist.Add(modsub);
                        }
                    }
                    emsg = null;
                    return modellist;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// get all accsubjectlist
        /// <summary>
        /// <param name=psubjectid>psubjectid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accsubjectlist</returns>
        public List<modAccSubjectList> GetIList(string psubjectid, bool leftpad, out string emsg)
        {
            try
            {
                List<modAccSubjectList> modellist = new List<modAccSubjectList>();
				//Execute a query to read the categories
				SqlParameter[] parms = {
					new SqlParameter("psubject_id", psubjectid)
				};
				string sql = @"select subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,has_children,check_flag,lock_flag,select_flag,update_user,update_time 
							from acc_subject_list where psubject_id=@psubject_id order by subject_id";

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    while (rdr.Read())
                    {
                        modAccSubjectList model = new modAccSubjectList();
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        if(leftpad)
                            model.SubjectName = GetLeftBlank(model.SubjectId) + dalUtility.ConvertToString(rdr["subject_name"]);
                        else
                            model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.PSubjectId = dalUtility.ConvertToString(rdr["psubject_id"]);
                        model.AssistantCode=dalUtility.ConvertToString(rdr["assistant_code"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CheckCurrency=dalUtility.ConvertToInt(rdr["check_currency"]);
                        model.IsTradecompany=dalUtility.ConvertToInt(rdr["is_tradecompany"]);
                        model.IsQuantity=dalUtility.ConvertToInt(rdr["is_quantity"]);
                        model.HasChildren = dalUtility.ConvertToInt(rdr["has_children"]);
                        model.CheckFlag = dalUtility.ConvertToInt(rdr["check_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.SelectFlag = dalUtility.ConvertToInt(rdr["select_flag"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                        List<modAccSubjectList> listsub = GetIList(model.SubjectId, leftpad, out emsg);
                        if (listsub != null && listsub.Count > 0)
                        {
                            foreach (modAccSubjectList modsub in listsub)
                                modellist.Add(modsub);
                        }
                    }
                    emsg = null;
                    return modellist;
                }                
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        public string GetLeftBlank(string subjectid)
        {
            string tmp=string.Empty;
            for (int i = 1; i < subjectid.Length; i++)
                tmp += "..";

            return tmp;
        }

        /// <summary>
        /// get table record
        /// <summary>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accsubjectlist</returns>
        public modAccSubjectList GetItem(string subjectid, out string emsg)
        {
            try
            {

				//Execute a query to read the categories
				SqlParameter[] parms = {
					new SqlParameter("subject_id", subjectid)
				};
				string sql = @"select subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,has_children,check_flag,lock_flag,select_flag,update_user,update_time 
							from acc_subject_list where subject_id=@subject_id order by subject_id";

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    if (rdr.Read())
                    {
                        modAccSubjectList model = new modAccSubjectList();
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName=dalUtility.ConvertToString(rdr["subject_name"]);
                        model.PSubjectId = dalUtility.ConvertToString(rdr["psubject_id"]);
                        model.AssistantCode=dalUtility.ConvertToString(rdr["assistant_code"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CheckCurrency=dalUtility.ConvertToInt(rdr["check_currency"]);
                        model.IsTradecompany=dalUtility.ConvertToInt(rdr["is_tradecompany"]);
                        model.IsQuantity=dalUtility.ConvertToInt(rdr["is_quantity"]);
                        model.HasChildren = dalUtility.ConvertToInt(rdr["has_children"]);
                        model.CheckFlag = dalUtility.ConvertToInt(rdr["check_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.SelectFlag = dalUtility.ConvertToInt(rdr["select_flag"]);
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
        /// get all accsubjectlist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accsubjectlist</returns>
        public BindingCollection<modAccSubjectList> GetChildrenList(out string emsg)
        {
            try
            {
                BindingCollection<modAccSubjectList> modellist = new BindingCollection<modAccSubjectList>();
                //Execute a query to read the categories
                string sql = @"select subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,has_children,check_flag,lock_flag,select_flag,update_user,update_time 
							from acc_subject_list where has_children=0 order by subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccSubjectList model = new modAccSubjectList();
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = GetLeftBlank(model.SubjectId) + dalUtility.ConvertToString(rdr["subject_name"]);
                        model.PSubjectId = dalUtility.ConvertToString(rdr["psubject_id"]);
                        model.AssistantCode = dalUtility.ConvertToString(rdr["assistant_code"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CheckCurrency = dalUtility.ConvertToInt(rdr["check_currency"]);
                        model.IsTradecompany = dalUtility.ConvertToInt(rdr["is_tradecompany"]);
                        model.IsQuantity = dalUtility.ConvertToInt(rdr["is_quantity"]);
                        model.HasChildren = dalUtility.ConvertToInt(rdr["has_children"]);
                        model.CheckFlag = dalUtility.ConvertToInt(rdr["check_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.SelectFlag = dalUtility.ConvertToInt(rdr["select_flag"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                    emsg = null;
                    return modellist;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get full subject name by id
        /// <summary>
        /// <param name=subjectid>subjectid</param>
        /// <returns>subject name</returns>
        public string GetSubjectName(string subjectid, ref string subjectname)
        {
            try
            {
				SqlParameter[] parms = {
					new SqlParameter("subject_id", subjectid)
				};
				string sql =@"select subject_name,psubject_id from acc_subject_list where subject_id=@subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    if (rdr.Read())
                    {
                        string psubjectid = rdr["psubject_id"].ToString().Trim();
                        if (string.IsNullOrEmpty(subjectname))
                            subjectname = rdr["subject_name"].ToString();
                        else
                            subjectname = rdr["subject_name"].ToString() + " -> " + subjectname;
                        if (!string.IsNullOrEmpty(psubjectid))
                        {
                            return GetSubjectName(psubjectid, ref subjectname);
                        }                        
                    }
                    return subjectname;
                }
            }
            catch (Exception ex)
            {
                subjectname = string.Empty;
                return subjectname;
            }
        }
        
        /// <summary>
        /// judge if the subject has children
        /// <summary>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool HasChildren(string subjectid, out string emsg)
        {
            try
            {
				SqlParameter[] parms = {
					new SqlParameter("psubject_id", subjectid)
				};
				string sql = @"select count(1) from acc_subject_list where psubject_id=@psubject_id";
                int i =Convert.ToInt32(SqlHelper.ExecuteScalar(sql, parms));
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
        /// insert a accsubjectlist
        /// <summary>
        /// <param name=mod>model object of accsubjectlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccSubjectList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_subject_list(subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,has_children,check_flag,lock_flag,select_flag,update_user,update_time)values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},0,1,'{10}',getdate())", mod.SubjectId, mod.SubjectName.Trim(), mod.PSubjectId, mod.AssistantCode, mod.AdFlag, mod.CheckCurrency, mod.IsTradecompany, mod.IsQuantity, mod.HasChildren, mod.CheckFlag, mod.UpdateUser);
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
        /// update a accsubjectlist
        /// <summary>
        /// <param name=subjectid>subjectid</param>
        /// <param name=mod>model object of accsubjectlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string subjectid,modAccSubjectList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_subject_list set subject_name='{0}',assistant_code='{1}',ad_flag={2}, check_currency={3},update_user='{4}',update_time=getdate() where subject_id='{5}'", mod.SubjectName.Trim(), mod.AssistantCode, mod.AdFlag, mod.CheckCurrency, mod.UpdateUser, subjectid);
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
        /// delete a accsubjectlist
        /// <summary>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string subjectid,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_subject_list where subject_id='{0}' ",subjectid);
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
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string subjectid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_subject_list where subject_id='{0}' ",subjectid);
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
