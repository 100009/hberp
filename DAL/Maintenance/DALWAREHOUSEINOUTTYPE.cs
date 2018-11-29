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
    public class dalWarehouseInoutType
    {
        /// <summary>
        /// get all warehouseinouttype
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseinouttype</returns>
        public BindingCollection<modWarehouseInoutType> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutType> modellist = new BindingCollection<modWarehouseInoutType>();
                //Execute a query to read the categories
                string sql = "select inout_type,inout_flag,lock_flag,update_user,update_time from warehouse_inout_type order by inout_flag,seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutType model = new modWarehouseInoutType();
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.LockFlag=dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToString(rdr["update_time"]);
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
        /// get all warehouseinouttype
        /// <summary>
        /// <param name=inouttype>inouttype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseinouttype</returns>
        public BindingCollection<modWarehouseInoutType> GetIList(int? inoutflag, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutType> modellist = new BindingCollection<modWarehouseInoutType>();
                //Execute a query to read the categories
                string sql = string.Format("select inout_type,inout_flag,lock_flag,update_user,update_time from warehouse_inout_type where inout_flag={0} order by seq", inoutflag);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutType model = new modWarehouseInoutType();
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.LockFlag=dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToString(rdr["update_time"]);
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
        /// <param name=inouttype>inouttype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of warehouseinouttype</returns>
        public modWarehouseInoutType GetItem(string inouttype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select inout_type,inout_flag,lock_flag,update_user,update_time from warehouse_inout_type where inout_type='{0}' order by inout_type",inouttype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modWarehouseInoutType model = new modWarehouseInoutType();
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.LockFlag=dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToString(rdr["update_time"]);
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
        /// record exist or not
        /// <summary>
        /// <param name=inouttype>inouttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string inouttype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from warehouse_inout_type where inout_type='{0}' ",inouttype);
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
