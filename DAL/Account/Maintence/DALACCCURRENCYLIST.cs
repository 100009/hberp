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
    public class dalAccCurrencyList
    {
        /// <summary>
        /// get all acccurrencylist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccurrencylist</returns>
        public BindingCollection<modAccCurrencyList> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccCurrencyList> modellist = new BindingCollection<modAccCurrencyList>();
                //Execute a query to read the categories
                string sql = "select currency,exchange_rate,owner_flag,update_user,update_time from acc_currency_list order by owner_flag desc,currency";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCurrencyList model = new modAccCurrencyList();
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.OwnerFlag=dalUtility.ConvertToInt(rdr["owner_flag"]);
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
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccurrencylist</returns>
        public modAccCurrencyList GetItem(string currency,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select currency,exchange_rate,owner_flag,update_user,update_time from acc_currency_list where currency='{0}'",currency);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCurrencyList model = new modAccCurrencyList();
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.OwnerFlag=dalUtility.ConvertToInt(rdr["owner_flag"]);
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
        /// get table record
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccurrencylist</returns>
        public modAccCurrencyList GetOwnerItem(out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = "select currency,exchange_rate,owner_flag,update_user,update_time from acc_currency_list where owner_flag=1";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCurrencyList model = new modAccCurrencyList();
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.OwnerFlag = dalUtility.ConvertToInt(rdr["owner_flag"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        emsg = null;
                        return model;
                    }
                    else
                    {
                        emsg = "Error on read data";
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
        /// insert a acccurrencylist
        /// <summary>
        /// <param name=mod>model object of acccurrencylist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCurrencyList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_currency_list(currency,exchange_rate,owner_flag,update_user,update_time)values('{0}',{1},{2},'{3}',getdate())",mod.Currency,mod.ExchangeRate,mod.OwnerFlag,mod.UpdateUser);
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
        /// update a acccurrencylist
        /// <summary>
        /// <param name=currency>currency</param>
        /// <param name=mod>model object of acccurrencylist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string currency,modAccCurrencyList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_currency_list set exchange_rate={0},owner_flag={1},update_user='{2}',update_time=getdate() where currency='{3}'",mod.ExchangeRate,mod.OwnerFlag,mod.UpdateUser,currency);
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
        /// delete a acccurrencylist
        /// <summary>
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string currency,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_currency_list where currency='{0}' ",currency);
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
        /// <param name=currency>currency</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string currency, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_currency_list where currency='{0}' ",currency);
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
