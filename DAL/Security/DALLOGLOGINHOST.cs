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
    public class dalLogLoginHost
    {
        /// <summary>
        /// get all logloginhost
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all logloginhost</returns>
        public BindingCollection<modLogLoginHost> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modLogLoginHost> modellist = new BindingCollection<modLogLoginHost>();
                //Execute a query to read the categories
                string sql = "select host_name,host_code,register_code,update_user,update_time from log_login_host order by host_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modLogLoginHost model = new modLogLoginHost();
                        model.HostName=dalUtility.ConvertToString(rdr["host_name"]);
                        model.HostCode=dalUtility.ConvertToString(rdr["host_code"]);
                        model.RegisterCode=dalUtility.ConvertToString(rdr["register_code"]);
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
        /// <param name=hostname>hostname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of logloginhost</returns>
        public modLogLoginHost GetItem(string hostname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select host_name,host_code,register_code,update_user,update_time from log_login_host where host_name='{0}' order by host_name",hostname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modLogLoginHost model = new modLogLoginHost();
                        model.HostName=dalUtility.ConvertToString(rdr["host_name"]);
                        model.HostCode=dalUtility.ConvertToString(rdr["host_code"]);
                        model.RegisterCode=dalUtility.ConvertToString(rdr["register_code"]);
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
        /// insert a logloginhost
        /// <summary>
        /// <param name=mod>model object of logloginhost</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modLogLoginHost mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into log_login_host(host_name,host_code,register_code,update_user,update_time)values('{0}','{1}','{2}','{3}',getdate())",mod.HostName,mod.HostCode,mod.RegisterCode,mod.UpdateUser);
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
        /// update a logloginhost
        /// <summary>
        /// <param name=hostname>hostname</param>
        /// <param name=mod>model object of logloginhost</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string hostname,modLogLoginHost mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update log_login_host set host_code='{0}',register_code='{1}',update_user='{2}',update_time=getdate() where host_name='{3}'",mod.HostCode,mod.RegisterCode,mod.UpdateUser,hostname);
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
        /// delete a logloginhost
        /// <summary>
        /// <param name=hostname>hostname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string hostname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete log_login_host where host_name='{0}' ",hostname);
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
        /// <param name=hostname>hostname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string hostname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from log_login_host where host_name='{0}' ",hostname);
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
