using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BindingCollection;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalTaskList
    {        
        /// <summary>
        /// get all qstasklist
        /// <summary>
        /// <param name=groupidlist>groupidlist</param>
        /// <param name=chkurl>chkurl</param>
        /// <param name=chkweburl>chkweburl</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all qstasklist</returns>
        public BindingCollection<modTaskList> GetIList(string groupidlist, bool chkurl, bool chkweburl, out string emsg)
        {
            try
            {
                string groupwhere = string.Empty;
                if (!string.IsNullOrEmpty(groupidlist) && groupidlist != "ALL")
                    groupwhere = "and group_id in ('" + groupidlist.Replace(",","','") + "') ";

                string urlwhere = string.Empty;
                if (chkurl)
                {
                    if (chkweburl)
                        urlwhere = "and LEN(rtrim(ltrim(url)))>0 and LEN(rtrim(ltrim(web_url)))>0 ";
                    else
                        urlwhere = "and LEN(rtrim(ltrim(url)))>0 ";
                }
                else
                {
                    if (chkweburl)
                        urlwhere = "and LEN(rtrim(ltrim(web_url)))>0 ";
                    else
                        urlwhere = string.Empty;
                }
                string sql = "select task_code,task_name,status,group_id,task_type,url,web_url,form_name,remark,update_user,update_time from sys_task_list where 1=1 "
                    + groupwhere + urlwhere + " order by group_id, task_code ";
                BindingCollection<modTaskList> modellist = new BindingCollection<modTaskList>();
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskList model = new modTaskList(dalUtility.ConvertToString(rdr["task_code"]), dalUtility.ConvertToString(rdr["task_name"]), dalUtility.ConvertToInt(rdr["status"]), dalUtility.ConvertToString(rdr["group_id"]), dalUtility.ConvertToString(rdr["task_type"]), dalUtility.ConvertToString(rdr["url"]), dalUtility.ConvertToString(rdr["web_url"]), dalUtility.ConvertToString(rdr["form_name"]), dalUtility.ConvertToString(rdr["remark"]), dalUtility.ConvertToString(rdr["update_user"]), dalUtility.ConvertToDateTime(rdr["update_time"]));
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
        /// get table record
        /// <summary>
        /// <param name=taskcode>taskcode</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of qstasklist</returns>
        public modTaskList GetItem(string taskcode,out string emsg)
        {
            try
            {
                string sql = string.Format("select task_code,task_name,status,group_id,task_type,url,web_url,form_name,remark,update_user,update_time from sys_task_list where task_code='{0}'", taskcode);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modTaskList model = new modTaskList(dalUtility.ConvertToString(rdr["task_code"]), dalUtility.ConvertToString(rdr["task_name"]), dalUtility.ConvertToInt(rdr["status"]), dalUtility.ConvertToString(rdr["group_id"]), dalUtility.ConvertToString(rdr["task_type"]), dalUtility.ConvertToString(rdr["url"]), dalUtility.ConvertToString(rdr["web_url"]), dalUtility.ConvertToString(rdr["form_name"]), dalUtility.ConvertToString(rdr["remark"]), dalUtility.ConvertToString(rdr["update_user"]), dalUtility.ConvertToDateTime(rdr["update_time"]));
                        emsg = "";
                        return model;
                    }
                    else
                    {
                        emsg = "";
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
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of qstasklist</returns>
        public int TotalRecords()
        {
            try
            {
                string sql = "Select count(1) from sys_task_list";
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// insert a qstasklist
        /// <summary>
        /// <param name=mod>mod</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modTaskList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_task_list(task_code,task_name,status,group_id,task_type,url,web_url,form_name,remark,update_user,update_time)values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}',getdate())", mod.TaskCode, mod.TaskName, mod.Status, mod.GroupId, mod.TaskType, mod.Url, mod.WebUrl, mod.FormName, mod.Remark, mod.UpdateUser);
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
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update a qstasklist
        /// <summary>
        /// <param name=taskcode>taskcode</param>
        /// <param name=mod>mod</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string taskcode,modTaskList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_task_list set task_name='{0}',status={1},url='{2}',web_url='{3}',form_name='{4}',remark='{5}',task_type='{6}',update_user='{7}',update_time=getdate() where task_code='{8}'", mod.TaskName, mod.Status, mod.Url, mod.WebUrl, mod.FormName, mod.Remark, mod.TaskType, mod.UpdateUser, taskcode);
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
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update a group of task
        /// <summary>
        /// <param name=taskcode>taskcode</param>
        /// <param name=group_id>group_id</param>
        /// <returns>true/false</returns>
        public bool UpdateTaskGroup(string taskcode, string groupid, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_task_list set group_id='{0}' where task_code='{1}'", groupid, taskcode);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
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
        /// delete a qstasklist
        /// <summary>
        /// <param name=taskcode>taskcode</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string taskcode,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sys_task_list where task_code='{0}'", taskcode);
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
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }        
    }
}
