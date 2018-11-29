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
    public class dalAccCommonDigest
    {
        /// <summary>
        /// get all acccommondigest
        /// <summary>
        /// <param name=digesttypelist>digesttypelist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccommondigest</returns>
        public BindingCollection<modAccCommonDigest> GetIList(string digesttypelist, out string emsg)
        {
            try
            {
                BindingCollection<modAccCommonDigest> modellist = new BindingCollection<modAccCommonDigest>();
                //Execute a query to read the categories
                string digesttypewhere = string.Empty;
                if (!string.IsNullOrEmpty(digesttypelist) && digesttypelist.CompareTo("ALL") != 0)
                    digesttypewhere = "and digest_type in ('" + digesttypelist.Replace(",", "','") + "') ";
                string sql = "select digest,digest_type,update_user,update_time from acc_common_digest where 1=1 " + digesttypewhere + "order by digest_type, digest";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCommonDigest model = new modAccCommonDigest();
                        model.Digest=dalUtility.ConvertToString(rdr["digest"]);
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
        /// <param name=digest>digest</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccommondigest</returns>
        public modAccCommonDigest GetItem(string digest,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select digest,digest_type,update_user,update_time from acc_common_digest where digest='{0}' order by digest",digest);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCommonDigest model = new modAccCommonDigest();
                        model.Digest=dalUtility.ConvertToString(rdr["digest"]);
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
        /// get digest type
        /// <summary>        
        ///<returns>get digest type</returns>
        public string GetDigestType(out string emsg)
        {
            try
            {
                string retstr=string.Empty;
                //Execute a query to read the categories
                string sql = "select distinct digest_type from acc_common_digest order by digest_type";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (string.IsNullOrEmpty(retstr))
                            retstr = rdr["digest_type"].ToString();
                        else
                            retstr += "," + rdr["digest_type"].ToString();                        
                    }
                    emsg=string.Empty;
                    return retstr;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// insert a acccommondigest
        /// <summary>
        /// <param name=mod>model object of acccommondigest</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCommonDigest mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_common_digest(digest,digest_type,update_user,update_time)values('{0}','{1}','{2}',getdate())",mod.Digest,mod.DigestType,mod.UpdateUser);
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
        /// update a acccommondigest
        /// <summary>
        /// <param name=digest>digest</param>
        /// <param name=mod>model object of acccommondigest</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string digest,modAccCommonDigest mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_common_digest set digest_type='{0}',update_user='{1}',update_time=getdate() where digest='{2}'",mod.DigestType,mod.UpdateUser,digest);
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
        /// delete a acccommondigest
        /// <summary>
        /// <param name=digest>digest</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string digest,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_common_digest where digest='{0}' ",digest);
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
        /// <param name=digest>digest</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string digest, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_common_digest where digest='{0}' ",digest);
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
