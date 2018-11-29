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
    public class dalSysParameters
    {
        /// <summary>
        /// get all sysparameters
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysparameters</returns>
        public BindingCollection<modSysParameters> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modSysParameters> modellist = new BindingCollection<modSysParameters>();
                //Execute a query to read the categories
                string sql = "select para_id,para_name,para_value,remark,update_user,update_time from sys_parameters order by para_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysParameters model = new modSysParameters();
                        model.ParaId = dalUtility.ConvertToString(rdr["para_id"]);
                        model.ParaName = dalUtility.ConvertToString(rdr["para_name"]);
                        model.ParaValue = dalUtility.ConvertToString(rdr["para_value"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToString(rdr["update_time"]);
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
        /// get all sysparameters
        /// <summary>
        /// <param name=paraid>paraid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysparameters</returns>
        public BindingCollection<modSysParameters> GetIList(string paraid, out string emsg)
        {
            try
            {
                BindingCollection<modSysParameters> modellist = new BindingCollection<modSysParameters>();
                //Execute a query to read the categories
                string sql = string.Format("select para_id,para_name,para_value,remark,update_user,update_time from sys_parameters where para_id='{0}' order by para_id", paraid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysParameters model = new modSysParameters();
                        model.ParaId = dalUtility.ConvertToString(rdr["para_id"]);
                        model.ParaName = dalUtility.ConvertToString(rdr["para_name"]);
                        model.ParaValue = dalUtility.ConvertToString(rdr["para_value"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToString(rdr["update_time"]);
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
        /// <param name=paraid>paraid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of sysparameters</returns>
        public modSysParameters GetItem(string paraid, out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select para_id,para_name,para_value,remark,update_user,update_time from sys_parameters where para_id='{0}' order by para_id", paraid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSysParameters model = new modSysParameters();
                        model.ParaId = dalUtility.ConvertToString(rdr["para_id"]);
                        model.ParaName = dalUtility.ConvertToString(rdr["para_name"]);
                        model.ParaValue = dalUtility.ConvertToString(rdr["para_value"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToString(rdr["update_time"]);
                        emsg = null;
                        return model;
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
        /// get parameter value
        /// <summary>
        /// <param name=paraid>paraid</param>
        ///<returns>get a record detail of sysparameters</returns>
        public string GetParaValue(string paraid)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select para_id,para_name,para_value,remark,update_user,update_time from sys_parameters where para_id='{0}' order by para_id", paraid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                        return dalUtility.ConvertToString(rdr["para_value"]);
                    else
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// insert a sysparameters
        /// <summary>
        /// <param name=mod>model object of sysparameters</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSysParameters mod, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}',getdate())", mod.ParaId, mod.ParaName, mod.ParaValue, mod.Remark, mod.UpdateUser);
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
        /// update a sysparameters
        /// <summary>
        /// <param name=paraid>paraid</param>
        /// <param name=mod>model object of sysparameters</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string paraid, modSysParameters mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_parameters set para_name='{0}',para_value='{1}',remark='{2}',update_user='{3}',update_time=getdate() where para_id='{4}'", mod.ParaName, mod.ParaValue, mod.Remark, mod.UpdateUser, paraid);
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
        /// delete a sysparameters
        /// <summary>
        /// <param name=paraid>paraid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string paraid, out string emsg)
        {
            try
            {
                string sql = string.Format("delete sys_parameters where para_id='{0}'", paraid);
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
        /// change sysparameters's status(valid/invalid)
        /// <summary>
        /// <param name=paraid>paraid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string paraid, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_parameters set status=1-status where para_id='{0}'", paraid);
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
        /// <param name=paraid>paraid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string paraid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from sys_parameters where para_id='{0}'", paraid);
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
