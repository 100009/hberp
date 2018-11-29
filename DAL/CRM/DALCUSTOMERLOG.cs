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
    public class dalCustomerLog
    {
        /// <summary>
        /// get customer daily log
        /// <summary>
        /// <param name=custidlist>custidlist</param>
        /// <param name=actioncodelist>actioncodelist</param>
        /// <param name=actionmanlist>actionmanlist</param>
        /// <param name=traceflaglist>traceflaglist</param>
        /// <param name=fromtime>fromtime</param>
        /// <param name=totime>totime</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlog</returns>
        public BindingCollection<modCustomerDailyLog> GetCustomerDailyLog(string actioncodelist, string actionman, string fromtime, string totime, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerDailyLog> modellist = new BindingCollection<modCustomerDailyLog>();
                //Execute a query to read the categories                
                string actioncodewhere = string.Empty;
                if (!string.IsNullOrEmpty(actioncodelist) && actioncodelist.CompareTo("ALL") != 0)
                    actioncodewhere = "and action_code in ('" + actioncodelist.Replace(",", "','") + "') ";

                string actiontimewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromtime))
                    actiontimewhere = "and from_time >= '" + Convert.ToDateTime(fromtime) + "' ";
                if (!string.IsNullOrEmpty(totime))
                    actiontimewhere += "and from_time <= '" + Convert.ToDateTime(totime).AddDays(1).AddSeconds(-1) + "' ";

                string sql = string.Format("select cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores "
                    + "from customer_log where trace_flag=1 and action_man='{0}' " + actioncodewhere + actiontimewhere + " order by id", actionman);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (modellist==null || modellist.Count == 0)
                        {
                            modCustomerDailyLog model = new modCustomerDailyLog();
                            model.ActionMan = dalUtility.ConvertToString(rdr["action_man"]);
                            model.ActionDate = dalUtility.ConvertToDateTime(rdr["from_time"]).ToString("yyyy-MM-dd");
                            model.ActionSubject = dalUtility.ConvertToString(rdr["action_subject"]);
                            if (string.IsNullOrEmpty(model.ActionSubject))
                                model.ActionSubject = dalUtility.ConvertToString(rdr["action_type"]);
                            model.ActionContent = "客户：" + dalUtility.ConvertToString(rdr["cust_name"]) + "\r\n内容：" + dalUtility.ConvertToString(rdr["action_content"]);
                            modellist.Add(model);
                        }
                        else
                        {
                            bool exists = false;
                            for (int i = 0; i < modellist.Count; i++)
                            {
                                if (modellist[i].ActionDate == dalUtility.ConvertToDateTime(rdr["from_time"]).ToString("yyyy-MM-dd"))
                                {
                                    if(string.IsNullOrEmpty(dalUtility.ConvertToString(rdr["action_subject"])))
                                        modellist[i].ActionSubject += "\r\n\r\n" + dalUtility.ConvertToString(rdr["action_subject"]);

                                    modellist[i].ActionContent += "\r\n\r\n客户：" + dalUtility.ConvertToString(rdr["cust_name"]) + "\r\n内容：" + dalUtility.ConvertToString(rdr["action_content"]);
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists)
                            {
                                modCustomerDailyLog model = new modCustomerDailyLog();
                                model.ActionMan = dalUtility.ConvertToString(rdr["action_man"]);
                                model.ActionDate = dalUtility.ConvertToDateTime(rdr["from_time"]).ToString("yyyy-MM-dd");
                                model.ActionSubject = dalUtility.ConvertToString(rdr["action_subject"]);
                                if (string.IsNullOrEmpty(model.ActionSubject))
                                    model.ActionSubject = dalUtility.ConvertToString(rdr["action_type"]);
                                model.ActionContent = dalUtility.ConvertToString(rdr["action_content"]);
                                modellist.Add(model);
                            }
                        }                        
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
        /// get customer daily scores
        /// <summary>
        /// <param name=actioncodelist>actioncodelist</param>
        /// <param name=actionmanlist>actionmanlist</param>
        /// <param name=fromtime>fromtime</param>
        /// <param name=totime>totime</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlog</returns>
        public BindingCollection<modCustomerDailyScores> GetCustomerDailyScores(string actioncodelist, string actionman, string fromtime, string totime, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerDailyScores> modellist = new BindingCollection<modCustomerDailyScores>();
                //Execute a query to read the categories                
                string actioncodewhere = string.Empty;
                if (!string.IsNullOrEmpty(actioncodelist) && actioncodelist.CompareTo("ALL") != 0)
                    actioncodewhere = "and action_code in ('" + actioncodelist.Replace(",", "','") + "') ";

                string actiontimewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromtime))
                    actiontimewhere = "and from_time >= '" + Convert.ToDateTime(fromtime) + "' ";
                if (!string.IsNullOrEmpty(totime))
                    actiontimewhere += "and from_time <= '" + Convert.ToDateTime(totime).AddDays(1).AddSeconds(-1) + "' ";

                string sql = string.Format("select action_man,CONVERT(varchar(100), from_time, 23) action_date,sum(scores*ad_flag) scores "
                    + "from customer_log where action_man='{0}' " + actioncodewhere + actiontimewhere
                    + " group by action_man,CONVERT(varchar(100), from_time, 23) order by action_man", actionman);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerDailyScores model = new modCustomerDailyScores();
                        model.ActionMan = dalUtility.ConvertToString(rdr["action_man"]);
                        model.ActionDate = dalUtility.ConvertToDate(rdr["action_date"]).ToString("yyyy-MM-dd");
                        model.Scores = Convert.ToDecimal(rdr["scores"]);
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
        /// get action scores summary
        /// <summary>
        /// <param name=actioncodelist>actioncodelist</param>
        /// <param name=fromtime>fromtime</param>
        /// <param name=totime>totime</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlog</returns>
        public BindingCollection<modActionScoresSummary> GetActionScoresSummary(string actioncodelist, string fromtime, string totime, out string emsg)
        {
            try
            {
                BindingCollection<modActionScoresSummary> modellist = new BindingCollection<modActionScoresSummary>();
                //Execute a query to read the categories                
                string actioncodewhere = string.Empty;
                if (!string.IsNullOrEmpty(actioncodelist) && actioncodelist.CompareTo("ALL") != 0)
                    actioncodewhere = "and action_code in ('" + actioncodelist.Replace(",", "','") + "') ";

                string actiontimewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromtime))
                    actiontimewhere = "and from_time >= '" + Convert.ToDateTime(fromtime) + "' ";
                if (!string.IsNullOrEmpty(totime))
                    actiontimewhere += "and from_time <= '" + Convert.ToDateTime(totime).AddDays(1).AddSeconds(-1) + "' ";

                string sql = "select action_man,action_type,sum(scores*ad_flag) scores from customer_log where 1=1 " + actioncodewhere + actiontimewhere
                    + " group by action_man,action_type order by action_man";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modActionScoresSummary model = new modActionScoresSummary();
                        model.ActionMan = dalUtility.ConvertToString(rdr["action_man"]);
                        model.ActionType = dalUtility.ConvertToString(rdr["action_type"]);
                        model.Scores = Convert.ToDecimal(rdr["scores"]);
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
        /// get all customerlog
        /// <summary>
        /// <param name=custidlist>custidlist</param>
        /// <param name=actioncodelist>actioncodelist</param>
        /// <param name=actiontypelist>actiontypelist</param>
        /// <param name=actionmanlist>actionmanlist</param>
        /// <param name=traceflaglist>traceflaglist</param>
        /// <param name=fromtime>fromtime</param>
        /// <param name=totime>totime</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlog</returns>
        public BindingCollection<modCustomerLog> GetIList(string custidlist, string actioncodelist, string actiontypelist, string actionmanlist, string traceflaglist, string fromtime, string totime, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerLog> modellist = new BindingCollection<modCustomerLog>();
                //Execute a query to read the categories
                string custidwhere = string.Empty;
                if (!string.IsNullOrEmpty(custidlist) && custidlist.CompareTo("ALL") != 0)
                    custidwhere = "and cust_id in ('" + custidlist.Replace(",", "','") + "') ";

                string actioncodewhere = string.Empty;
                if (!string.IsNullOrEmpty(actioncodelist) && actioncodelist.CompareTo("ALL") != 0)
                    actioncodewhere = "and action_code in ('" + actioncodelist.Replace(",", "','") + "') ";

                string actiontypewhere = string.Empty;
                if (!string.IsNullOrEmpty(actiontypelist) && actiontypelist.CompareTo("ALL") != 0)
                    actiontypewhere = "and action_type in ('" + actiontypelist.Replace(",", "','") + "') ";

                string actionmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(actionmanlist) && actionmanlist.CompareTo("ALL") != 0)
                    actionmanwhere = "and action_man in ('" + actionmanlist.Replace(",", "','") + "') ";

                string traceflagwhere = string.Empty;
                if (!string.IsNullOrEmpty(traceflaglist) && traceflaglist.CompareTo("ALL") != 0)
                    traceflagwhere = "and trace_flag in ('" + traceflaglist.Replace(",", "','") + "') ";

                string fromtimewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromtime))
                    fromtimewhere = "and from_time >= '" + Convert.ToDateTime(fromtime) + "' ";
                if (!string.IsNullOrEmpty(totime))
                    fromtimewhere += "and from_time <= '" + Convert.ToDateTime(totime).AddDays(1).AddSeconds(-1) + "' ";

                string sql = "select id,cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,trace_flag,update_user,update_time "
                    + "from customer_log where 1=1 " + custidwhere + actioncodewhere + actiontypewhere + actionmanwhere + traceflagwhere + fromtimewhere + " order by id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerLog model = new modCustomerLog();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ActionCode=dalUtility.ConvertToString(rdr["action_code"]);
                        model.ActionType=dalUtility.ConvertToString(rdr["action_type"]);
                        model.ActionMan=dalUtility.ConvertToString(rdr["action_man"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.ActionSubject=dalUtility.ConvertToString(rdr["action_subject"]);
                        model.ActionContent=dalUtility.ConvertToString(rdr["action_content"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Venue=dalUtility.ConvertToString(rdr["venue"]);
                        model.FromTime=dalUtility.ConvertToString(rdr["from_time"]);
                        model.ToTime=dalUtility.ConvertToString(rdr["to_time"]);
                        model.Scores=dalUtility.ConvertToDecimal(rdr["scores"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.TraceFlag = dalUtility.ConvertToInt(rdr["trace_flag"]);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customerlog</returns>
        public modCustomerLog GetItem(int? id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,trace_flag,update_user,update_time from customer_log where ID={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerLog model = new modCustomerLog();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ActionCode=dalUtility.ConvertToString(rdr["action_code"]);
                        model.ActionType=dalUtility.ConvertToString(rdr["action_type"]);
                        model.ActionMan=dalUtility.ConvertToString(rdr["action_man"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.ActionSubject=dalUtility.ConvertToString(rdr["action_subject"]);
                        model.ActionContent=dalUtility.ConvertToString(rdr["action_content"]);
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Venue=dalUtility.ConvertToString(rdr["venue"]);
                        model.FromTime=dalUtility.ConvertToString(rdr["from_time"]);
                        model.ToTime=dalUtility.ConvertToString(rdr["to_time"]);
                        model.Scores = dalUtility.ConvertToDecimal(rdr["scores"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.TraceFlag = dalUtility.ConvertToInt(rdr["trace_flag"]);
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
        /// insert a customerlog
        /// <summary>
        /// <param name=mod>model object of customerlog</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCustomerLog mod,out string emsg)
        {
            try
            {                
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime.Parse(mod.ToTime).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Days < 0)
                {
                    emsg = "您不能新增未来的工作日志！";
                    return false;
                }

                dalSysParameters dalp = new dalSysParameters();
                int delaydays = Convert.ToInt32(dalp.GetParaValue("CUSTOMER_LOG_DELAY_DAYS"));
                if (ts.Days >= delaydays)
                {
                    emsg = "你提交的日期不能超过" + delaydays.ToString() + "天";
                    return false;
                }

                string sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,trace_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},{14},'{15}',getdate())", mod.CustId, mod.CustName, mod.ActionCode, mod.ActionType, mod.ActionMan, mod.FormId, mod.ActionSubject, mod.ActionContent, mod.ObjectName, mod.Venue, mod.FromTime, mod.ToTime, mod.Scores, mod.AdFlag, mod.TraceFlag, mod.UpdateUser);
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
        /// update a customerlog
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of customerlog</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id,modCustomerLog mod,out string emsg)
        {
            try
            {                
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime.Parse(mod.ToTime).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Days < 0)
                {
                    emsg = "您不能更新未来的工作日志！";
                    return false;
                }

                dalSysParameters dalp = new dalSysParameters();
                int delaydays = Convert.ToInt32(dalp.GetParaValue("CUSTOMER_LOG_DELAY_DAYS"));
                if (ts.Days >= delaydays)
                {
                    emsg = "你提交的日期不能超过" + delaydays.ToString() + "天";
                    return false;
                }

                string sql = string.Format("update customer_log set cust_id='{0}',cust_name='{1}',action_code='{2}',action_type='{3}',action_man='{4}',form_id='{5}',action_subject='{6}',action_content='{7}',object_name='{8}',venue='{9}',from_time='{10}',to_time='{11}',scores={12},ad_flag={13},update_user='{14}',update_time=getdate() where id={15}",mod.CustId,mod.CustName,mod.ActionCode,mod.ActionType,mod.ActionMan,mod.FormId,mod.ActionSubject,mod.ActionContent,mod.ObjectName,mod.Venue,mod.FromTime,mod.ToTime,mod.Scores,mod.AdFlag,mod.UpdateUser,id);
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
        /// delete a customerlog
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id,out string emsg)
        {
            try
            {
                modCustomerLog mod = GetItem(id, out emsg);
                if (mod == null)
                {
                    emsg = null;
                    return true;
                }
                dalUserList dalu = new dalUserList();
                DateTime svrtime = dalu.GetServerTime(out emsg);
                TimeSpan ts1 = new TimeSpan(svrtime.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime.Parse(mod.ToTime).Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                if (ts.Days < 0)
                {
                    emsg = "您不能删除未来的工作日志！";
                    return false;
                }
                dalSysParameters dalp = new dalSysParameters();
                int delaydays = Convert.ToInt32(dalp.GetParaValue("CUSTOMER_LOG_DELAY_DAYS"));
                if (ts.Days >= delaydays)
                {
                    emsg = "您提交的日期不能超过" + delaydays.ToString() + "天";
                    return false;
                }

                string sql = string.Format("delete customer_log where id={0} ",id);
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

    }
}
