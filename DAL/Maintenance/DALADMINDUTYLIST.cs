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
    public class dalAdminDutyList
    {
        /// <summary>
        /// get all admindutylist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindutylist</returns>
        public BindingCollection<modAdminDutyList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDutyList> modellist = new BindingCollection<modAdminDutyList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select duty,status,update_user,update_time from admin_duty_list where 1=1 " + getwhere + "order by duty";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDutyList model = new modAdminDutyList();
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
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
        /// get all admindutylist
        /// <summary>
        /// <param name=duty>duty</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindutylist</returns>
        public BindingCollection<modAdminDutyList> GetIList(string duty, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDutyList> modellist = new BindingCollection<modAdminDutyList>();
                //Execute a query to read the categories
                string sql = string.Format("select duty,status,update_user,update_time from admin_duty_list where duty='{0}' order by duty",duty);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDutyList model = new modAdminDutyList();
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
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
        /// <param name=duty>duty</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of admindutylist</returns>
        public modAdminDutyList GetItem(string duty,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select duty,status,update_user,update_time from admin_duty_list where duty='{0}' order by duty",duty);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAdminDutyList model = new modAdminDutyList();
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
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
        /// insert a admindutylist
        /// <summary>
        /// <param name=mod>model object of admindutylist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAdminDutyList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into admin_duty_list(duty,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.Duty,mod.Status,mod.UpdateUser);
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
        /// update a admindutylist
        /// <summary>
        /// <param name=duty>duty</param>
        /// <param name=mod>model object of admindutylist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string duty,modAdminDutyList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_duty_list set status={0},update_user='{1}',update_time=getdate() where duty='{2}'",mod.Status,mod.UpdateUser,duty);
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
        /// delete a admindutylist
        /// <summary>
        /// <param name=duty>duty</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string duty,out string emsg)
        {
            try
            {
                string sql = string.Format("delete admin_duty_list where duty='{0}'",duty);
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
        /// change admindutylist's status(valid/invalid)
        /// <summary>
        /// <param name=duty>duty</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string duty,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_duty_list set status=1-status where duty='{0}'",duty);
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
        /// <param name=duty>duty</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string duty, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from admin_duty_list where duty='{0}'",duty);
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
