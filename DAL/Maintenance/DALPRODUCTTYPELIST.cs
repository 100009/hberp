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
    public class dalProductTypeList
    {
        /// <summary>
        /// get all producttypelist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all producttypelist</returns>
        public BindingCollection<modProductTypeList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modProductTypeList> modellist = new BindingCollection<modProductTypeList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select product_type,status,update_user,update_time from product_type_list where product_type<>'临时' " + getwhere + " order by product_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductTypeList model = new modProductTypeList();
                        model.ProductType=dalUtility.ConvertToString(rdr["product_type"]);
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
        /// <param name=producttype>producttype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of producttypelist</returns>
        public modProductTypeList GetItem(string producttype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select product_type,status,update_user,update_time from product_type_list where product_type='{0}' order by product_type",producttype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductTypeList model = new modProductTypeList();
                        model.ProductType=dalUtility.ConvertToString(rdr["product_type"]);
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
        /// insert a producttypelist
        /// <summary>
        /// <param name=mod>model object of producttypelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modProductTypeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into product_type_list(product_type,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.ProductType,mod.Status,mod.UpdateUser);
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
        /// update a producttypelist
        /// <summary>
        /// <param name=producttype>producttype</param>
        /// <param name=mod>model object of producttypelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string producttype,modProductTypeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update product_type_list set status={0},update_user='{1}',update_time=getdate() where product_type='{2}'",mod.Status,mod.UpdateUser,producttype);
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
		/// delete a producttypelist
		/// <summary>
		/// <param name=producttype>producttype</param>
		/// <param name=out emsg>return error message</param>
		/// <returns>true/false</returns>
		public bool Delete(string producttype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete product_type_list where product_type='{0}' ",producttype);
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
        /// change producttypelist's status(valid/invalid)
        /// <summary>
        /// <param name=producttype>producttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string producttype,out string emsg)
        {
            try
            {
                string sql = string.Format("update product_type_list set status=1-status where product_type='{0}' ",producttype);
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
        /// <param name=producttype>producttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string producttype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from product_type_list where product_type='{0}' ",producttype);
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
