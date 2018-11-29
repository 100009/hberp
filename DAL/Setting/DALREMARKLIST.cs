using System;
using System.Collections;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalRemarkList
    {
        /// <summary>
        /// get all remark type list
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>ArrayList</returns>
        public ArrayList GetRemarkType(out string emsg)
        {
            ArrayList arr = new ArrayList();
            try
            {
                string sql = "select distinct remark_type from remark_list order by remark_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        arr.Add(rdr["remark_type"].ToString());
                    }
                }
                emsg = null;
                return arr;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all remarklist
        /// <summary>
        /// <param name=remarktype>remarktype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all remarklist</returns>
        public BindingCollection<modRemarkList> GetIList(string remarktype, out string emsg)
        {
            try
            {
                BindingCollection<modRemarkList> modellist = new BindingCollection<modRemarkList>();
                //Execute a query to read the categories
                string sql = string.Format("select remark_type,remark,update_user,update_time from remark_list where remark_type='{0}' order by remark_type,remark",remarktype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modRemarkList model = new modRemarkList();
                        model.RemarkType=dalUtility.ConvertToString(rdr["remark_type"]);
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
        /// <param name=remarktype>remarktype</param>
        /// <param name=remark>remark</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of remarklist</returns>
        public modRemarkList GetItem(string remarktype,string remark,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select remark_type,remark,update_user,update_time from remark_list where remark_type='{0}' and remark='{1}' order by remark_type,remark",remarktype,remark);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modRemarkList model = new modRemarkList();
                        model.RemarkType=dalUtility.ConvertToString(rdr["remark_type"]);
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
        /// insert a remarklist
        /// <summary>
        /// <param name=mod>model object of remarklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modRemarkList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into remark_list(remark_type,remark,update_user,update_time)values('{0}','{1}','{2}',getdate())",mod.RemarkType,mod.Remark,mod.UpdateUser);
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
        /// update a remarklist
        /// <summary>
        /// <param name=remarktype>remarktype</param>
        /// <param name=remark>remark</param>
        /// <param name=mod>model object of remarklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string remarktype,string remark,modRemarkList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update remark_list set update_user='{0}',update_time=getdate() where remark_type='{1}' and remark='{2}'",mod.UpdateUser,remarktype,remark);
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
        /// delete a remarklist
        /// <summary>
        /// <param name=remarktype>remarktype</param>
        /// <param name=remark>remark</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string remarktype,string remark,out string emsg)
        {
            try
            {
                string sql = string.Format("delete remark_list where remark_type='{0}'and remark='{1}'",remarktype,remark);
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
        /// <param name=remarktype>remarktype</param>
        /// <param name=remark>remark</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string remarktype,string remark, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from remark_list where remark_type='{0}',remark='{1}'",remarktype,remark);
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
