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
    public class dalCustomerScoreRule
    {
        /// <summary>
        /// get all customerscorerule
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerscorerule</returns>
        public BindingCollection<modCustomerScoreRule> GetIList(string traceflaglist,out string emsg)
        {
            try
            {
                BindingCollection<modCustomerScoreRule> modellist = new BindingCollection<modCustomerScoreRule>();
                //Execute a query to read the categories
                string traceflagwhere = string.Empty;
                if (!string.IsNullOrEmpty(traceflaglist) && traceflaglist.CompareTo("ALL") != 0)
                    traceflagwhere = "and trace_flag in ('" + traceflaglist.Replace(",", "','") + "') ";
                string sql = "select action_code,action_type,scores,trace_flag,update_user,update_time from customer_score_rule where 1=1 " + traceflagwhere + " order by seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerScoreRule model = new modCustomerScoreRule();
                        model.ActionCode=dalUtility.ConvertToString(rdr["action_code"]);
                        model.ActionType = dalUtility.ConvertToString(rdr["action_type"]);
                        model.Scores = dalUtility.ConvertToDecimal(rdr["scores"]);
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
        /// <param name=actioncode>actioncode</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customerscorerule</returns>
        public modCustomerScoreRule GetItem(string actioncode,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select action_code,action_type,scores,trace_flag,update_user,update_time from customer_score_rule where action_code='{0}' order by action_code", actioncode);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerScoreRule model = new modCustomerScoreRule();
                        model.ActionCode=dalUtility.ConvertToString(rdr["action_code"]);
                        model.ActionType = dalUtility.ConvertToString(rdr["action_type"]);
                        model.Scores=dalUtility.ConvertToDecimal(rdr["scores"]);
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
        /// update a customerscorerule
        /// <summary>
        /// <param name=actioncode>actioncode</param>
        /// <param name=mod>model object of customerscorerule</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string actioncode,modCustomerScoreRule mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_score_rule set action_type='{0}',scores={1},update_user='{2}',update_time=getdate() where action_code='{3}'", mod.ActionType, mod.Scores, mod.UpdateUser, actioncode);
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
        /// <param name=actioncode>actioncode</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string actioncode, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from customer_score_rule where action_code='{0}' ",actioncode);
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
