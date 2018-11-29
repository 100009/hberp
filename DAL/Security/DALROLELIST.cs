using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;
using BindingCollection;

namespace LXMS.DAL
{
    public class dalRoleList
    {        
        /// <summary>
        /// get RoleList by RoleId
        /// <summary>
        /// <param name=RoleId>RoleId</param>
        ///<returns>get record of RoleList</returns>
        ///<returns>Details about all RoleList</returns>
        public modRoleList GetItem(string RoleId,out string emsg)
        {
            try
            {
                string sql = string.Format("select role_id,role_desc,status,update_user,update_time from sys_role_list where role_id = '{0}'", RoleId);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modRoleList model = new modRoleList();
                        model.RoleId = rdr["role_id"].ToString();
                        model.RoleDesc = rdr["role_desc"].ToString();
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = DateTime.Parse(rdr["update_time"].ToString());
                        emsg = string.Empty;
                        return model;
                    }
                    else
                    {
                        emsg = "";
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
        /// get all qstasklist
        /// <summary>
        /// <param name=validonly>validonly</param>
        ///<returns>Details about all RoleList</returns>
        public BindingCollection<modRoleList> GetIList(bool validonly, out string emsg)
        {
            try
            {                
                string sql;
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                sql = "select role_id,role_desc,status,update_user,update_time from sys_role_list where 1=1 " + getwhere + "order by role_id";                
                BindingCollection<modRoleList> modellist = new BindingCollection<modRoleList>();
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modRoleList model = new modRoleList();
                        model.RoleId = rdr["role_id"].ToString();
                        model.RoleDesc = rdr["role_desc"].ToString();
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = DateTime.Parse(rdr["update_time"].ToString());
                        modellist.Add(model);
                    }
                }
                emsg = "";
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get table record count
        /// <summary>
        ///<returns>get record count of RoleList</returns>
        public int TotalRecords()
        {
            string sql = "Select count(1) from sys_role_list";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// insert a RoleList
        /// <summary>
        /// <param name=mod>mod</param>
        /// <returns>true/false</returns>
        public bool Insert(modRoleList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_role_list(role_id,role_desc,status,update_user,update_time)values('{0}','{1}',{2},'{3}',getdate())", mod.RoleId, mod.RoleDesc, mod.Status, mod.UpdateUser);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = "";
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
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// update a RoleList
        /// <summary>
        /// <param name=RoleId>RoleId</param>
        /// <param name=mod>mod</param>
        /// <returns>true/false</returns>
        public bool Update(string RoleId, modRoleList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_role_list set role_desc='{0}',status={1},update_user='{2}',update_time=getdate() where role_id='{3}'", mod.RoleDesc, mod.Status, mod.UpdateUser,RoleId);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = "";
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
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// delete a RoleList
        /// <summary>
        /// <param name=RoleId>RoleId</param>
        /// <returns>true/false</returns>
        public bool Delete(string RoleId, out string emsg)
        {
            try
            {
                string sql = string.Format("delete sys_role_list where role_id='{0}'", RoleId);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = "";
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
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// change RoleList's status(valid/invalid)
        /// <summary>
        /// <param name=RoleId>RoleId</param>
        /// <returns>true/false</returns>
        public bool Inactive(string RoleId, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_role_list set status=1-status where role_id='{0}'", RoleId);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = "";
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
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }
    }
}
