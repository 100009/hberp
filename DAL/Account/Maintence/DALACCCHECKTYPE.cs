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
    public class dalAccCheckType
    {
        /// <summary>
        /// get all accchecktype
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecktype</returns>
        public BindingCollection<modAccCheckType> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccCheckType> modellist = new BindingCollection<modAccCheckType>();
                //Execute a query to read the categories
                string sql = "select check_type,update_user,update_time from acc_check_type order by check_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCheckType model = new modAccCheckType();
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
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
        /// <param name=checktype>checktype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accchecktype</returns>
        public modAccCheckType GetItem(string checktype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select check_type,update_user,update_time from acc_check_type where check_type='{0}' order by check_type",checktype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCheckType model = new modAccCheckType();
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
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
        /// insert a accchecktype
        /// <summary>
        /// <param name=mod>model object of accchecktype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCheckType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_check_type(check_type,update_user,update_time)values('{0}','{1}',getdate())",mod.CheckType,mod.UpdateUser);
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
        /// update a accchecktype
        /// <summary>
        /// <param name=checktype>checktype</param>
        /// <param name=mod>model object of accchecktype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string checktype,modAccCheckType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_check_type set update_user='{0}',update_time=getdate() where check_type='{1}'",mod.UpdateUser,checktype);
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
        /// delete a accchecktype
        /// <summary>
        /// <param name=checktype>checktype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string checktype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_check_type where check_type='{0}' ",checktype);
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
        /// change accchecktype's status(valid/invalid)
        /// <summary>
        /// <param name=checktype>checktype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string checktype,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_check_type set status=1-status where check_type='{0}' ",checktype);
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
        /// <param name=checktype>checktype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string checktype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_check_type where check_type='{0}' ",checktype);
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
