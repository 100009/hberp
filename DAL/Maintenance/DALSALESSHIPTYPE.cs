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
    public class dalSalesShipType
    {
        /// <summary>
        /// get all salesshiptype
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshiptype</returns>
        public BindingCollection<modSalesShipType> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipType> modellist = new BindingCollection<modSalesShipType>();
                //Execute a query to read the categories
                string sql = "select ship_type,ad_flag,update_user,update_time from sales_ship_type order by ship_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipType model = new modSalesShipType();
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
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
        /// get all salesshiptype
        /// <summary>
        /// <param name=shiptype>shiptype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshiptype</returns>
        public BindingCollection<modSalesShipType> GetIList(string shiptype, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipType> modellist = new BindingCollection<modSalesShipType>();
                //Execute a query to read the categories
                string sql = string.Format("select ship_type,ad_flag,update_user,update_time from sales_ship_type where ship_type='{0}' order by ship_type",shiptype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipType model = new modSalesShipType();
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
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
        /// <param name=shiptype>shiptype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of salesshiptype</returns>
        public modSalesShipType GetItem(string shiptype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select ship_type,ad_flag,update_user,update_time from sales_ship_type where ship_type='{0}' order by ship_type",shiptype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSalesShipType model = new modSalesShipType();
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
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
        /// insert a salesshiptype
        /// <summary>
        /// <param name=mod>model object of salesshiptype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSalesShipType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sales_ship_type(ship_type,ad_flag,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.ShipType,mod.AdFlag,mod.UpdateUser);
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
        /// update a salesshiptype
        /// <summary>
        /// <param name=shiptype>shiptype</param>
        /// <param name=mod>model object of salesshiptype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string shiptype,modSalesShipType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sales_ship_type set ad_flag={0},update_user='{1}',update_time=getdate() where ship_type='{2}'",mod.AdFlag,mod.UpdateUser,shiptype);
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
        /// delete a salesshiptype
        /// <summary>
        /// <param name=shiptype>shiptype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string shiptype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sales_ship_type where ship_type='{0}' ",shiptype);
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
        /// <param name=shiptype>shiptype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string shiptype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from sales_ship_type where ship_type='{0}' ",shiptype);
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
