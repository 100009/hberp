using System;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Transactions;
using BindingCollection;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalTaskGrant
    {        
        /// <summary>
        /// get ungranted data by groupid
        /// <summary>
        /// <param name=chkurl>chkurl</param>
        /// <param name=chkweburl>chkweburl</param>
        ///<returns>Details about all TaskGrant</returns>
        public BindingCollection<modTaskGrant> GetReadyData(bool chkurl, bool chkweburl, string roleid, out string emsg)
        {
            try
            {
                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
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
                string sql = string.Format("select task_code,task_name,form_name,group_id,task_type,url,web_url from sys_task_list where 1=1 " + urlwhere 
                    + " except select a.task_code,b.task_name,b.form_name,b.group_id,b.task_type,b.url,b.web_url from sys_task_grant a,sys_task_list b where a.task_code=b.task_code and a.role_id= '{0}' and b.status=1 order by task_code", roleid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();                        
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url= rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = false;
                        modellist.Add(model);
                    }
                    emsg = "";
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
        /// get all already granted data
        /// <summary>
        /// <param name=roleid>roleid</param>  
        /// <param name=chkurl>chkurl</param>
        /// <param name=chkweburl>chkweburl</param>
        ///<returns>Details about all TaskGrant</returns>
        public BindingCollection<modTaskGrant> GetGrantData(bool chkurl, bool chkweburl, string roleid, out string emsg)
        {    
            try
            {
                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
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
                string sql = string.Format("select a.task_code,b.task_name,b.group_id,b.task_type,b.form_name,b.url,b.web_url from sys_task_grant a,sys_task_list b where a.task_code=b.task_code and a.role_id= '{0}' and b.status=1 " + urlwhere + " order by a.task_code", roleid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();                        
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = true;
                        modellist.Add(model);
                    }
                    emsg = "";
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
        /// get all already granted data
        /// <summary>
        /// <param name=roleid>roleid</param>  
        ///<returns>Details about all TaskGrant</returns>
        public BindingCollection<modTaskGrant> GetAllTask(string roleid, out string emsg)
        {
            try
            {
                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
                string sql = string.Format("select a.task_code,b.task_name,b.group_id,b.task_type,b.form_name,b.url,b.web_url from sys_task_grant a,sys_task_list b where a.task_code=b.task_code and a.role_id= '{0}' and b.status=1 order by a.task_code", roleid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = true;
                        modellist.Add(model);
                    }
                }
                sql = string.Format("select a.task_code,a.task_name,a.group_id,a.task_type,a.form_name,a.url,a.web_url from sys_task_list a where a.status=1 and not exists (select '#' from sys_task_grant where task_code =a.task_code and role_id='{0}')", roleid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = false;
                        modellist.Add(model);
                    }                    
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all already granted data
        /// <summary>
        /// <param name=roleid>roleid</param>  
        ///<returns>Details about all TaskGrant</returns>
        public BindingCollection<modTaskGrant> GetAllUserTask(string userid, out string emsg)
        {
            try
            {
                dalUserList daluser = new dalUserList();
                modUserList moduser = daluser.GetItem(userid);

                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
                string sql = string.Format("select a.task_code,b.task_name,b.form_name,b.group_id,b.task_type,b.url,b.web_url from sys_task_grant a,sys_task_list b where a.task_code=b.task_code and a.role_id= '{0}' and b.status=1 order by a.task_code", userid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = true;
                        modellist.Add(model);
                    }
                }
                sql = string.Format("select a.task_code,a.task_name,a.form_name,a.group_id,a.task_type,a.url,a.web_url from sys_task_list a where a.status=1 and not exists (select '#' from sys_task_grant where task_code =a.task_code and (role_id='{0}' or role_id='{1}'))", moduser.RoleId, userid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = false;
                        modellist.Add(model);
                    }
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get granted data by userid and groupid
        /// <summary>
        /// <param name=chkurl>chkurl</param>
        /// <param name=chkweburl>chkweburl</param>
        /// <param name=userid>userid</param>
        /// <param name=groupidlist>groupidlist</param>
        /// <param name=tasktypelist>tasktypelist</param>
        ///<returns>get granted data by userid</returns>
        public BindingCollection<modTaskGrant> GetUserGrantData(bool chkurl, bool chkweburl, string userid, string groupidlist, string tasktypelist, out string emsg)
        {
            try
            {
                string groupwhere = string.Empty;
                if (!string.IsNullOrEmpty(groupidlist) && groupidlist != "ALL")
                    groupwhere = "and a.group_id in ('" + groupidlist.Replace(",", "','") + "') ";

                string tasktypewhere = string.Empty;
                if (!string.IsNullOrEmpty(tasktypelist) && tasktypelist != "ALL")
                    tasktypewhere = "and a.task_type in ('" + tasktypelist.Replace(",", "','") + "') ";

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
                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
                string sql = string.Format("select a.task_code,a.task_name,a.group_id,a.task_type,a.form_name,a.url,a.web_url from sys_task_list a,sys_task_grant b where a.task_code=b.task_code and b.role_id='{0}' " + urlwhere + groupwhere
                    + "union select a.task_code,a.task_name,a.group_id,a.task_type,a.form_name,a.url,a.web_url from sys_task_list a,sys_task_grant b,sys_user_list c where a.task_code=b.task_code and b.role_id=c.role_id "
                    + "and a.status=1 and c.user_id='{1}' " + urlwhere + groupwhere + tasktypewhere, userid, userid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modTaskGrant model = new modTaskGrant();                        
                        model.TaskCode = rdr["task_code"].ToString();
                        model.TaskName = rdr["task_name"].ToString();
                        model.TaskType = rdr["task_type"].ToString();
                        model.FormName = rdr["form_name"].ToString();
                        model.GroupId = rdr["group_id"].ToString();
                        model.Url = rdr["url"].ToString();
                        model.WebUrl = rdr["web_url"].ToString();
                        model.Checked = true;
                        modellist.Add(model);
                    }
                    emsg = "";
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
        /// get web grant form name list
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=groupidlist>groupidlist</param>
        ///<returns>get granted data by userid</returns>
        public string GetWebGrantForm(string userid, out string emsg)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                BindingCollection<modTaskGrant> modellist = new BindingCollection<modTaskGrant>();
                string sql = string.Format("select a.form_name from sys_task_list a,sys_task_grant b where a.task_code=b.task_code and b.role_id='{0}' " 
                    + "union select a.form_name from sys_task_list a,sys_task_grant b,sys_user_list c where a.task_code=b.task_code and b.role_id=c.role_id "
                    + "and a.status=1 and c.user_id='{1}' ", userid, userid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        sb.Append(rdr[0].ToString());
                        sb.Append(";");
                    }
                    emsg = string.Empty;
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// CheckAccess
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=taskcode>taskcode</param>
        /// <returns>true/false</returns>
        public bool CheckAccess(string userid, string taskcode)
        {
            try
            {                
                string sql = string.Format("select count(1) from sys_task_grant a where role_id='{0}' and task_code='{1}' ", userid, taskcode);
                int iret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
                if (iret >= 1)
                    return true;
                else
                {
                    sql = string.Format("select count(1) from sys_task_grant a inner join sys_user_list b on a.role_id=b.role_id where b.user_id='{0}' and a.task_code='{1}' ", userid, taskcode);
                    iret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
                    if (iret >= 1)
                        return true;
                    else
                    {                        
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {                
                return false;
            }
        }

        /// <summary>
        /// Check Access by Form Name
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=formname>formname</param>
        /// <returns>true/false</returns>
        public bool CheckFormAccess(string userid, string formname)
        {
            try
            {
                string sql = string.Format("select count(1) from sys_task_grant a inner join sys_task_list b on a.task_code=b.task_code where a.role_id='{0}' and b.form_name='{1}' ", userid, formname);
                int iret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
                if (iret >= 1)
                    return true;
                else
                {
                    sql = string.Format("select count(1) from sys_task_grant a inner join sys_task_list b on a.task_code=b.task_code inner join sys_user_list c on a.role_id=c.role_id where c.user_id='{0}' and b.form_name='{1}' ", userid, formname);
                    iret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
                    if (iret >= 1)
                        return true;
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// get table record count
        /// <summary>
        ///<returns>get record count of TaskGrant</returns>
        public int TotalRecords()
        {
            string sql = "Select count(1) from sys_task_grant";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// get table record count
        /// <summary>
        ///<returns>get record count of TaskGrant</returns>
        public int GroupRecords()
        {
            string sql = "Select count(1) from role_list";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// save TaskGrant
        /// <summary>
        /// <param name=roleid>roleid</param>
        /// <param name=arrtask>arrtask</param>
        /// <returns>true/false</returns>
        public bool SaveTaskGrant(string roleid, ArrayList arrtask,string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    sql = string.Format("delete sys_task_grant where role_id='{0}'", roleid);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (arrtask != null)
                    {
                        for (int i = 0; i < arrtask.Count; i++)
                        {                            
                            sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", roleid, arrtask[i], updateuser);
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }                    
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

        /// <summary>
        /// save TaskGrant
        /// <summary>
        /// <param name=roleid>userid</param>
        /// <param name=tasklist>tasklist</param>
        /// <returns>true/false</returns>
        public bool SaveUserTaskGrant(string userid, ArrayList arrtask, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    sql=string.Format("delete sys_task_grant where role_id='{0}'", userid);
                    SqlHelper.ExecuteNonQuery(sql);
                    if (arrtask != null)
                    {
                        for (int i = 0; i < arrtask.Count; i++)
                        {
                            sql = string.Format("select count(1) from sys_task_grant a,sys_user_list b where a.role_id=b.role_id and b.user_id='{0}' and a.task_code='{1}'", userid, arrtask[i]);
                            int iret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                            if (iret == 0)
                            {
                                sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", userid, arrtask[i], updateuser);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                        }
                    }
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

        /// <summary>
        /// copy TaskGrant
        /// <summary>
        /// <param name=roleid>userid</param>
        /// <param name=tasklist>tasklist</param>
        /// <returns>true/false</returns>
        public bool CopyUserTaskGrant(string fromuser, string touser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    sql = string.Format("delete sys_task_grant where role_id='{0}'", touser);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time) select '{0}',task_code,update_user,update_time from sys_task_grant where role_id='{1}'", touser, fromuser);
                    SqlHelper.ExecuteNonQuery(sql);
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
