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
    public class dalWarehouseList
    {
        /// <summary>
        /// get all warehouselist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouselist</returns>
        public BindingCollection<modWarehouseList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseList> modellist = new BindingCollection<modWarehouseList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select warehouse_id,warehouse_desc,status,default_flag,update_user,update_time from warehouse_list where warehouse_id<>'¡Ÿ ±' " + getwhere + " order by warehouse_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseList model = new modWarehouseList();
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.WarehouseDesc=dalUtility.ConvertToString(rdr["warehouse_desc"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.DefaultFlag = dalUtility.ConvertToInt(rdr["default_flag"]);
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
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of warehouselist</returns>
        public modWarehouseList GetItem(string warehouseid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select warehouse_id,warehouse_desc,status,default_flag,update_user,update_time from warehouse_list where warehouse_id='{0}' order by warehouse_id",warehouseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modWarehouseList model = new modWarehouseList();
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.WarehouseDesc=dalUtility.ConvertToString(rdr["warehouse_desc"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.DefaultFlag = dalUtility.ConvertToInt(rdr["default_flag"]);
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
        /// get default warehouse id
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>default warehouse id</returns>
        public string GetDefaultWarehouseId(out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = "select warehouse_id from warehouse_list where default_flag=1";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        emsg = string.Empty;
                        return dalUtility.ConvertToString(rdr["warehouse_id"]);
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
        /// insert a warehouselist
        /// <summary>
        /// <param name=mod>model object of warehouselist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modWarehouseList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into warehouse_list(warehouse_id,warehouse_desc,status,default_flag,update_user,update_time)values('{0}','{1}',{2},{3},'{4}',getdate())", mod.WarehouseId, mod.WarehouseDesc, mod.Status, mod.DefaultFlag, mod.UpdateUser);
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
        /// update a warehouselist
        /// <summary>
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=mod>model object of warehouselist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string warehouseid,modWarehouseList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update warehouse_list set warehouse_desc='{0}',status={1},default_flag={2},update_user='{3}',update_time=getdate() where warehouse_id='{4}'", mod.WarehouseDesc, mod.Status, mod.DefaultFlag, mod.UpdateUser, warehouseid);
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
        /// delete a warehouselist
        /// <summary>
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string warehouseid,out string emsg)
        {
            try
            {
                string sql = string.Format("delete warehouse_list where warehouse_id='{0}' ",warehouseid);
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
        /// change warehouselist's status(valid/invalid)
        /// <summary>
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string warehouseid,out string emsg)
        {
            try
            {
                string sql = string.Format("update warehouse_list set status=1-status where warehouse_id='{0}' ",warehouseid);
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
        /// <param name=warehouseid>warehouseid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string warehouseid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from warehouse_list where warehouse_id='{0}' ",warehouseid);
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
