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
    public class dalSalesDesignType
    {
        /// <summary>
        /// get all salesdesigntype
        /// <summary>
        /// <param name=formtype>formtype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesdesigntype</returns>
        public BindingCollection<modSalesDesignType> GetIList(string formtype, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignType> modellist = new BindingCollection<modSalesDesignType>();
                //Execute a query to read the categories
                string sql = string.Format("select form_type,ad_flag,update_user,update_time from sales_design_type where form_type='{0}' order by form_type",formtype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignType model = new modSalesDesignType();
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
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
        /// <param name=formtype>formtype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of salesdesigntype</returns>
        public modSalesDesignType GetItem(string formtype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select form_type,ad_flag,update_user,update_time from sales_design_type where form_type='{0}' order by form_type",formtype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSalesDesignType model = new modSalesDesignType();
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
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
        /// insert a salesdesigntype
        /// <summary>
        /// <param name=mod>model object of salesdesigntype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSalesDesignType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sales_design_type(form_type,ad_flag,update_user,update_time)values('{0}','{1}','{2}',getdate())",mod.FormType,mod.AdFlag,mod.UpdateUser);
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
        /// update a salesdesigntype
        /// <summary>
        /// <param name=formtype>formtype</param>
        /// <param name=mod>model object of salesdesigntype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string formtype,modSalesDesignType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sales_design_type set ad_flag='{0}',update_user='{1}',update_time=getdate() where form_type='{2}'",mod.AdFlag,mod.UpdateUser,formtype);
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
        /// delete a salesdesigntype
        /// <summary>
        /// <param name=formtype>formtype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string formtype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sales_design_type where form_type='{0}' ",formtype);
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

    }
}
