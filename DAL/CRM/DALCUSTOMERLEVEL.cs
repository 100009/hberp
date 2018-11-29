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
    public class dalCustomerLevel
    {        
        /// <summary>
        /// get all customerlevel
        /// <summary>
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlevel</returns>
        public BindingCollection<modCustomerLevel> GetIList(string getwhere, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerLevel> modellist = new BindingCollection<modCustomerLevel>();
                //Execute a query to read the categories
                string sql = "select cust_level,description,status,update_user,update_time from customer_level where 1=1 " + getwhere + " order by cust_level";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerLevel model = new modCustomerLevel();
                        model.CustLevel=dalUtility.ConvertToString(rdr["cust_level"]);
                        model.Description=dalUtility.ConvertToString(rdr["description"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customerlevel</returns>
        public modCustomerLevel GetItem(string custlevel,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select cust_level,description,status,update_user,update_time from customer_level where cust_level='{0}' order by cust_level",custlevel);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerLevel model = new modCustomerLevel();
                        model.CustLevel=dalUtility.ConvertToString(rdr["cust_level"]);
                        model.Description=dalUtility.ConvertToString(rdr["description"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
        /// insert a customerlevel
        /// <summary>
        /// <param name=mod>model object of customerlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCustomerLevel mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into customer_level(cust_level,description,status,update_user,update_time)values('{0}','{1}','{2}','{3}',getdate())",mod.CustLevel,mod.Description,mod.Status,mod.UpdateUser);
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
        /// update a customerlevel
        /// <summary>
        /// <param name=custlevel>custlevel</param>
        /// <param name=mod>model object of customerlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string custlevel,modCustomerLevel mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_level set description='{0}',status='{1}',update_user='{2}',update_time=getdate() where cust_level='{3}'",mod.Description,mod.Status,mod.UpdateUser,custlevel);
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
        /// delete a customerlevel
        /// <summary>
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string custlevel,out string emsg)
        {
            try
            {
                string sql = string.Format("delete customer_level where cust_level='{0}' ",custlevel);
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
        /// change customerlevel's status(valid/invalid)
        /// <summary>
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string custlevel,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_level set status=1-status where cust_level='{0}' ",custlevel);
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
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string custlevel, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from customer_level where cust_level='{0}' ",custlevel);
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
