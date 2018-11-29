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
    public class dalCrmActionSchedule
    {
        /// <summary>
        /// get all crmactionschedule
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=salesman>salesman</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all crmactionschedule</returns>
        public BindingCollection<modCrmActionSchedule> GetIList(string statuslist, string salesman, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modCrmActionSchedule> modellist = new BindingCollection<modCrmActionSchedule>();
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string actiondatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    actiondatewhere = "and action_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    actiondatewhere += "and action_date <= '" + Convert.ToDateTime(todate) + "' ";
                //Execute a query to read the categories
                string sql = string.Format("select action_man,action_date,action_content,status,status_desc,update_user,update_time "
                        + "from crm_action_schedule where action_man='{0}' " + statuswhere + actiondatewhere + "order by action_man,action_date", salesman);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCrmActionSchedule model = new modCrmActionSchedule();
                        model.ActionMan=dalUtility.ConvertToString(rdr["action_man"]);
                        model.ActionDate=dalUtility.ConvertToDateTime(rdr["action_date"]);
                        model.ActionContent=dalUtility.ConvertToString(rdr["action_content"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.StatusDesc=dalUtility.ConvertToString(rdr["status_desc"]);
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
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of crmactionschedule</returns>
        public modCrmActionSchedule GetItem(string salesman,DateTime actiondate,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select action_man,action_date,action_content,status,status_desc,update_user,update_time from crm_action_schedule where action_man='{0}' and action_date='{1}' order by action_man,action_date",salesman,actiondate);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCrmActionSchedule model = new modCrmActionSchedule();
                        model.ActionMan=dalUtility.ConvertToString(rdr["action_man"]);
                        model.ActionDate=dalUtility.ConvertToDateTime(rdr["action_date"]);
                        model.ActionContent=dalUtility.ConvertToString(rdr["action_content"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.StatusDesc=dalUtility.ConvertToString(rdr["status_desc"]);
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
        /// insert a crmactionschedule
        /// <summary>
        /// <param name=mod>model object of crmactionschedule</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCrmActionSchedule mod,out string emsg)
        {
            try
            {
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(mod.ActionDate.Ticks);
                TimeSpan ts = ts2.Subtract(ts1).Duration();
                if (ts.Days < 0)
                {
                    emsg = "您不能新增过去的工作计划！";
                    return false;
                }
                dalSysParameters dalp = new dalSysParameters();
                int aheaddays = Convert.ToInt32(dalp.GetParaValue("ACTION_SCHEDULE_AHEAD_DAYS"));
                if (ts.Days > aheaddays)
                {
                    emsg = "您不能提交" + aheaddays.ToString() + "天后的工作计划";
                    return false;
                }

                string sql = string.Format("insert into crm_action_schedule(action_man,action_date,action_content,status,status_desc,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}',getdate())",mod.ActionMan,mod.ActionDate,mod.ActionContent,mod.Status,mod.StatusDesc,mod.UpdateUser);
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
        /// update a crmactionschedule
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=mod>model object of crmactionschedule</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string salesman,DateTime actiondate,modCrmActionSchedule mod,out string emsg)
        {
            try
            {
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(mod.ActionDate.Ticks);
                TimeSpan ts = ts2.Subtract(ts1).Duration();
                if (ts.Days < 0)
                {
                    emsg = "您不能更新过去的工作计划！";
                    return false;
                }
                dalSysParameters dalp = new dalSysParameters();
                int aheaddays = Convert.ToInt32(dalp.GetParaValue("ACTION_SCHEDULE_AHEAD_DAYS"));
                if (ts.Days > aheaddays)
                {
                    emsg = "您不能提交" + aheaddays.ToString() + "天后的工作计划";
                    return false;
                }
                string sql = string.Format("update crm_action_schedule set action_content='{0}',update_user='{1}',update_time=getdate() where action_man='{2}' and action_date='{3}'",mod.ActionContent,mod.UpdateUser,salesman,actiondate);
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
        /// delete a crmactionschedule
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string salesman,DateTime actiondate,out string emsg)
        {
            try
            {
                modCrmActionSchedule mod = GetItem(salesman, actiondate, out emsg);
                if (mod == null)
                {
                    emsg = null;
                    return true;
                }
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(mod.ActionDate.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Days > 0)
                {
                    emsg = "您不能删除过去的工作计划！";
                    return false;
                }
                string sql = string.Format("delete crm_action_schedule where action_man='{0}' and action_date='{1}' ",salesman,actiondate);
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
        /// update a crmactionschedule
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=status>status</param>
        /// <param name=statusdesc>statusdesc</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateStatus(string salesman, DateTime actiondate, int status, string statusdesc, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Format("update crm_action_schedule set status={0}, status_desc='{1}',update_user='{2}',update_time=getdate() where action_man='{3}' and action_date='{4}'", status, statusdesc, updateuser, salesman, actiondate);
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
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string salesman,DateTime actiondate, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from crm_action_schedule where action_man='{0}' and action_date='{1}' ",salesman,actiondate);
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
        /// record exist or not
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=actiondate>actiondate</param>
        /// <param name=actioncontent>actioncontent</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string salesman, DateTime actiondate, string actioncontent, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from crm_action_schedule where action_man='{0}' and action_date='{1}' and action_content='{2}' ", salesman, actiondate, actioncontent);
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
