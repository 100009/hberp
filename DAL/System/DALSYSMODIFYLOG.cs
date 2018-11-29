using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalSysModifyLog
    {
        /// <summary>
        /// get all sysmodifylog
        /// <summary>
        /// <param name=ver>ver</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysmodifylog</returns>
        public BindingCollection<modSysModifyLog> GetIList(string ver, out string emsg)
        {
            try
            {
                BindingCollection<modSysModifyLog> modellist = new BindingCollection<modSysModifyLog>();
                //Execute a query to read the categories
                string sql = string.Format("select ver,title,modify_content,update_user,update_time from sys_modify_log where ver='{0}' order by ver",ver);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysModifyLog model = new modSysModifyLog();
                        model.Version=dalUtility.ConvertToString(rdr["ver"]);
                        model.Title=dalUtility.ConvertToString(rdr["title"]);
                        model.ModifyContent=dalUtility.ConvertToString(rdr["modify_content"]);
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
        /// get all sysmodifylog
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysmodifylog</returns>
        public BindingCollection<modSysModifyLog> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modSysModifyLog> modellist = new BindingCollection<modSysModifyLog>();
                //Execute a query to read the categories
                string sql = "select ver,title,modify_content,update_user,update_time from sys_modify_log order by update_time desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysModifyLog model = new modSysModifyLog();
                        model.Version = dalUtility.ConvertToString(rdr["ver"]);
                        model.Title = dalUtility.ConvertToString(rdr["title"]);
                        model.ModifyContent = dalUtility.ConvertToString(rdr["modify_content"]);
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
        /// <param name=ver>ver</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of sysmodifylog</returns>
        public modSysModifyLog GetItem(string ver,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select ver,title,modify_content,update_user,update_time from sys_modify_log where ver='{0}' order by ver",ver);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSysModifyLog model = new modSysModifyLog();
                        model.Version = dalUtility.ConvertToString(rdr["ver"]);
                        model.Title=dalUtility.ConvertToString(rdr["title"]);
                        model.ModifyContent=dalUtility.ConvertToString(rdr["modify_content"]);
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
        /// get current system version
        /// <summary>
        ///<returns>get current system version</returns>
        public string GetVersion()
        {
            try
            {
                string sql = "select top 1 ver from sys_modify_log order by update_time desc";
                return SqlHelper.ExecuteScalar(sql).ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of sysmodifylog</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from sys_modify_log";
                emsg = null;
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// insert a sysmodifylog
        /// <summary>
        /// <param name=mod>model object of sysmodifylog</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSysModifyLog mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('{0}','{1}','{2}','{3}',getdate())", mod.Version, mod.Title, mod.ModifyContent, mod.UpdateUser);
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
        /// update a sysmodifylog
        /// <summary>
        /// <param name=ver>ver</param>
        /// <param name=mod>model object of sysmodifylog</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string ver,modSysModifyLog mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_modify_log set title='{0}',modify_content='{1}',update_user='{2}', update_time=getdate() where ver='{3}'",mod.Title,mod.ModifyContent,mod.UpdateUser,ver);
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
        /// delete a sysmodifylog
        /// <summary>
        /// <param name=ver>ver</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string ver,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sys_modify_log where ver='{0}'",ver);
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
        /// change sysmodifylog's status(valid/invalid)
        /// <summary>
        /// <param name=ver>ver</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string ver,out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_modify_log set status=1-status where ver='{0}'",ver);
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
        /// <param name=ver>ver</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string ver, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from sys_modify_log where ver='{0}'",ver);
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
        /// clear database operation data
        /// <summary>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ClearOperattionData(out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {                    
                    string sql = string.Empty;
                    string itemname = string.Empty;
                    SqlHelper.ExecuteNonQuery("delete quotation_detail");
                    SqlHelper.ExecuteNonQuery("delete quotation_list");
                    SqlHelper.ExecuteNonQuery("delete piecework_user");
                    SqlHelper.ExecuteNonQuery("delete piecework_list");
                    SqlHelper.ExecuteNonQuery("delete item_shipment_detail");
                    SqlHelper.ExecuteNonQuery("delete item_shipment");
                    SqlHelper.ExecuteNonQuery("delete item_epiboly_detail");
                    SqlHelper.ExecuteNonQuery("delete item_epiboly");                    
                    SqlHelper.ExecuteNonQuery("delete process_trans_detail");
                    SqlHelper.ExecuteNonQuery("delete process_trans");                    
                    SqlHelper.ExecuteNonQuery("delete item_alarm_log");
                    SqlHelper.ExecuteNonQuery("delete item_alarm_user");
                    SqlHelper.ExecuteNonQuery("delete item_alarm");
                    SqlHelper.ExecuteNonQuery("delete item_process_detail");
                    SqlHelper.ExecuteNonQuery("delete item_process");
                    SqlHelper.ExecuteNonQuery("delete item_info");
                    SqlHelper.ExecuteNonQuery("delete item_log");
                    transaction.Complete();//就这句就可以了。     
                    emsg = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                    return false;
                }
            }
        }

    }
}
