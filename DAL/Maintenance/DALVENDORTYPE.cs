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
    public class dalVendorType
    {
        /// <summary>
        /// get all vendortype
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendortype</returns>
        public BindingCollection<modVendorType> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modVendorType> modellist = new BindingCollection<modVendorType>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select vendor_type,status,update_user,update_time from vendor_type where 1=1 " + getwhere + "order by vendor_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorType model = new modVendorType();
                        model.VendorType=dalUtility.ConvertToString(rdr["vendor_type"]);
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
        /// get all vendortype
        /// <summary>
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendortype</returns>
        public BindingCollection<modVendorType> GetIList(string vendortype, out string emsg)
        {
            try
            {
                BindingCollection<modVendorType> modellist = new BindingCollection<modVendorType>();
                //Execute a query to read the categories
                string sql = string.Format("select vendor_type,status,update_user,update_time from vendor_type where vendor_type='{0}' order by vendor_type",vendortype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorType model = new modVendorType();
                        model.VendorType=dalUtility.ConvertToString(rdr["vendor_type"]);
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
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of vendortype</returns>
        public modVendorType GetItem(string vendortype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select vendor_type,status,update_user,update_time from vendor_type where vendor_type='{0}' order by vendor_type",vendortype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modVendorType model = new modVendorType();
                        model.VendorType=dalUtility.ConvertToString(rdr["vendor_type"]);
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
        /// insert a vendortype
        /// <summary>
        /// <param name=mod>model object of vendortype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modVendorType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into vendor_type(vendor_type,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.VendorType,mod.Status,mod.UpdateUser);
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
        /// update a vendortype
        /// <summary>
        /// <param name=vendortype>vendortype</param>
        /// <param name=mod>model object of vendortype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string vendortype,modVendorType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update vendor_type set status={0},update_user='{1}',update_time=getdate() where vendor_type='{2}'",mod.Status,mod.UpdateUser,vendortype);
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
        /// delete a vendortype
        /// <summary>
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string vendortype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete vendor_type where vendor_type='{0}'",vendortype);
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
        /// change vendortype's status(valid/invalid)
        /// <summary>
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string vendortype,out string emsg)
        {
            try
            {
                string sql = string.Format("update vendor_type set status=1-status where vendor_type='{0}'",vendortype);
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
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string vendortype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from vendor_type where vendor_type='{0}'",vendortype);
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
