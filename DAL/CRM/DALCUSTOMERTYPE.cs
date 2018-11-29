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
    public class dalCustomerType
    {
        /// <summary>
        /// get all customertype
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customertype</returns>
        public BindingCollection<modCustomerType> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerType> modellist = new BindingCollection<modCustomerType>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select cust_type,status,update_user,update_time from customer_type where 1=1 " + getwhere + "order by cust_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerType model = new modCustomerType(dalUtility.ConvertToString(rdr["cust_type"]),dalUtility.ConvertToInt(rdr["status"]),dalUtility.ConvertToString(rdr["update_user"]),dalUtility.ConvertToDateTime(rdr["update_time"]));
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
        /// get all customertype
        /// <summary>
        /// <param name=custtype>custtype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customertype</returns>
        public BindingCollection<modCustomerType> GetIList(string custtype, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerType> modellist = new BindingCollection<modCustomerType>();
                //Execute a query to read the categories
                string sql = string.Format("select cust_type,status,update_user,update_time from customer_type where cust_type='{0}' order by cust_type",custtype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerType model = new modCustomerType(dalUtility.ConvertToString(rdr["cust_type"]),dalUtility.ConvertToInt(rdr["status"]),dalUtility.ConvertToString(rdr["update_user"]),dalUtility.ConvertToDateTime(rdr["update_time"]));
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
        /// <param name=custtype>custtype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customertype</returns>
        public modCustomerType GetItem(string custtype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select cust_type,status,update_user,update_time from customer_type where cust_type='{0}' order by cust_type",custtype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerType model = new modCustomerType(dalUtility.ConvertToString(rdr["cust_type"]),dalUtility.ConvertToInt(rdr["status"]),dalUtility.ConvertToString(rdr["update_user"]),dalUtility.ConvertToDateTime(rdr["update_time"]));
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
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of customertype</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from customer_type";
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
        /// insert a customertype
        /// <summary>
        /// <param name=mod>model object of customertype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCustomerType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into customer_type(cust_type,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.CustType,mod.Status,mod.UpdateUser);
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
        /// update a customertype
        /// <summary>
        /// <param name=custtype>custtype</param>
        /// <param name=mod>model object of customertype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string custtype,modCustomerType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_type set status={0},update_user='{1}',update_time=getdate() where cust_type='{2}'",mod.Status,mod.UpdateUser,custtype);
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
        /// delete a customertype
        /// <summary>
        /// <param name=custtype>custtype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string custtype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete customer_type where cust_type='{0}'",custtype);
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
        /// change customertype's status(valid/invalid)
        /// <summary>
        /// <param name=custtype>custtype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string custtype,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_type set status=1-status where cust_type='{0}'",custtype);
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
        /// <param name=custtype>custtype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string custtype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from customer_type where cust_type='{0}'",custtype);
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
