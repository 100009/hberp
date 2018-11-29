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
    public class dalSysUserPrivilege
    {
        /// <summary>
        /// get all sysuserprivilege
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysuserprivilege</returns>
        public BindingCollection<modSysUserPrivilege> GetIList(string userid, out string emsg)
        {
            try
            {
                BindingCollection<modSysUserPrivilege> modellist = new BindingCollection<modSysUserPrivilege>();
                //Execute a query to read the categories
                string sql = string.Format("select user_id,privilege_name,privilege_value,update_user,update_time from sys_user_privilege where user_id='{0}' order by user_id,privilege_name",userid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysUserPrivilege model = new modSysUserPrivilege();
                        model.UserId=dalUtility.ConvertToString(rdr["user_id"]);
                        model.PrivilegeName=dalUtility.ConvertToString(rdr["privilege_name"]);
                        model.PrivilegeValue=dalUtility.ConvertToString(rdr["privilege_value"]);
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
        /// <param name=userid>userid</param>
        /// <param name=privilegename>privilegename</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of sysuserprivilege</returns>
        public modSysUserPrivilege GetItem(string userid,string privilegename,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select user_id,privilege_name,privilege_value,update_user,update_time from sys_user_privilege where user_id='{0}' and privilege_name='{1}' order by user_id,privilege_name",userid,privilegename);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSysUserPrivilege model = new modSysUserPrivilege();
                        model.UserId=dalUtility.ConvertToString(rdr["user_id"]);
                        model.PrivilegeName=dalUtility.ConvertToString(rdr["privilege_name"]);
                        model.PrivilegeValue=dalUtility.ConvertToString(rdr["privilege_value"]);
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
        /// save a sysuserprivilege
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=list>model object of sysuserprivilege</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string userid, BindingCollection<modSysUserPrivilege> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete sys_user_privilege where user_id='{0}'", userid);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (list != null && list.Count > 0)
                    {
                        foreach (modSysUserPrivilege mod in list)
                        {
                            sql = string.Format("insert into sys_user_privilege(user_id,privilege_name,privilege_value,update_user,update_time)values('{0}','{1}','{2}','{3}',getdate())", mod.UserId, mod.PrivilegeName, mod.PrivilegeValue, mod.UpdateUser);
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

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=privilegename>privilegename</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string userid,string privilegename, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from sys_user_privilege where user_id='{0}' and privilege_name='{1}' ",userid,privilegename);
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
