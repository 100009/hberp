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
    public class dalAdminDegreeList
    {
        /// <summary>
        /// get all admindegreelist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindegreelist</returns>
        public BindingCollection<modAdminDegreeList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDegreeList> modellist = new BindingCollection<modAdminDegreeList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select edu_degree,status,update_user,update_time from admin_degree_list where 1=1 " + getwhere + "order by edu_degree";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDegreeList model = new modAdminDegreeList();
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
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
        /// get all admindegreelist
        /// <summary>
        /// <param name=edudegree>edudegree</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindegreelist</returns>
        public BindingCollection<modAdminDegreeList> GetIList(string edudegree, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDegreeList> modellist = new BindingCollection<modAdminDegreeList>();
                //Execute a query to read the categories
                string sql = string.Format("select edu_degree,status,update_user,update_time from admin_degree_list where edu_degree='{0}' order by edu_degree",edudegree);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDegreeList model = new modAdminDegreeList();
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
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
        /// get table record
        /// <summary>
        /// <param name=edudegree>edudegree</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of admindegreelist</returns>
        public modAdminDegreeList GetItem(string edudegree,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select edu_degree,status,update_user,update_time from admin_degree_list where edu_degree='{0}' order by edu_degree",edudegree);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAdminDegreeList model = new modAdminDegreeList();
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
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
        /// insert a admindegreelist
        /// <summary>
        /// <param name=mod>model object of admindegreelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAdminDegreeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into admin_degree_list(edu_degree,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.EduDegree,mod.Status,mod.UpdateUser);
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
        /// update a admindegreelist
        /// <summary>
        /// <param name=edudegree>edudegree</param>
        /// <param name=mod>model object of admindegreelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string edudegree,modAdminDegreeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_degree_list set status={0},update_user='{1}',update_time=getdate() where edu_degree='{2}'",mod.Status,mod.UpdateUser,edudegree);
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
        /// delete a admindegreelist
        /// <summary>
        /// <param name=edudegree>edudegree</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string edudegree,out string emsg)
        {
            try
            {
                string sql = string.Format("delete admin_degree_list where edu_degree='{0}'",edudegree);
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
        /// change admindegreelist's status(valid/invalid)
        /// <summary>
        /// <param name=edudegree>edudegree</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string edudegree,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_degree_list set status=1-status where edu_degree='{0}'",edudegree);
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
        /// <param name=edudegree>edudegree</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string edudegree, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from admin_degree_list where edu_degree='{0}'",edudegree);
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
