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
    public class dalAccCommonDigestType
    {
        /// <summary>
        /// get all acccommondigesttype
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccommondigesttype</returns>
        public BindingCollection<modAccCommonDigestType> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccCommonDigestType> modellist = new BindingCollection<modAccCommonDigestType>();
                //Execute a query to read the categories
                string sql = "select digest_type,update_user,update_time from acc_common_digest_type order by digest_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCommonDigestType model = new modAccCommonDigestType();
                        model.DigestType=dalUtility.ConvertToString(rdr["digest_type"]);
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
        /// <param name=digesttype>digesttype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccommondigesttype</returns>
        public modAccCommonDigestType GetItem(string digesttype,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select digest_type,update_user,update_time from acc_common_digest_type where digest_type='{0}' order by digest_type",digesttype);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCommonDigestType model = new modAccCommonDigestType();
                        model.DigestType=dalUtility.ConvertToString(rdr["digest_type"]);
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
        /// insert a acccommondigesttype
        /// <summary>
        /// <param name=mod>model object of acccommondigesttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCommonDigestType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_common_digest_type(digest_type,update_user,update_time)values('{0}','{1}',getdate())",mod.DigestType,mod.UpdateUser);
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
        /// update a acccommondigesttype
        /// <summary>
        /// <param name=digesttype>digesttype</param>
        /// <param name=mod>model object of acccommondigesttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string digesttype,modAccCommonDigestType mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_common_digest_type set update_user='{0}',update_time=getdate() where digest_type='{1}'",mod.UpdateUser,digesttype);
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
        /// delete a acccommondigesttype
        /// <summary>
        /// <param name=digesttype>digesttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string digesttype,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_common_digest_type where digest_type='{0}' ",digesttype);
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
        /// <param name=digesttype>digesttype</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string digesttype, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_common_digest_type where digest_type='{0}' ",digesttype);
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
