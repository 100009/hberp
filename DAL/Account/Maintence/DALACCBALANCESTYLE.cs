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
    public class dalAccBalanceStyle
    {
        /// <summary>
        /// get all accbalancestyle
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accbalancestyle</returns>
        public BindingCollection<modAccBalanceStyle> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccBalanceStyle> modellist = new BindingCollection<modAccBalanceStyle>();
                //Execute a query to read the categories
                string sql = "select balance_style,update_user,update_time from acc_balance_style order by balance_style";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccBalanceStyle model = new modAccBalanceStyle();
                        model.BalanceStyle=dalUtility.ConvertToString(rdr["balance_style"]);
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
        /// <param name=balancestyle>balancestyle</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accbalancestyle</returns>
        public modAccBalanceStyle GetItem(string balancestyle,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select balance_style,update_user,update_time from acc_balance_style where balance_style='{0}' order by balance_style",balancestyle);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccBalanceStyle model = new modAccBalanceStyle();
                        model.BalanceStyle=dalUtility.ConvertToString(rdr["balance_style"]);
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
        /// insert a accbalancestyle
        /// <summary>
        /// <param name=mod>model object of accbalancestyle</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccBalanceStyle mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_balance_style(balance_style,update_user,update_time)values('{0}','{1}',getdate())",mod.BalanceStyle,mod.UpdateUser);
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
        /// update a accbalancestyle
        /// <summary>
        /// <param name=balancestyle>balancestyle</param>
        /// <param name=mod>model object of accbalancestyle</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string balancestyle,modAccBalanceStyle mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_balance_style set update_user='{0}',update_time=getdate() where balance_style='{1}'",mod.UpdateUser,balancestyle);
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
        /// delete a accbalancestyle
        /// <summary>
        /// <param name=balancestyle>balancestyle</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string balancestyle,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_balance_style where balance_style='{0}' ",balancestyle);
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
        /// <param name=balancestyle>balancestyle</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string balancestyle, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_balance_style where balance_style='{0}' ",balancestyle);
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
