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
    public class dalOtherPayableObject
    {
        /// <summary>
        /// get all otherpayableobject
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all otherpayableobject</returns>
        public BindingCollection<modOtherPayableObject> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modOtherPayableObject> modellist = new BindingCollection<modOtherPayableObject>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select object_name,status,currency,link_man,addr,tel,remark,update_user,update_time from other_payable_object where 1=1 " + getwhere + " order by object_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modOtherPayableObject model = new modOtherPayableObject();
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.LinkMan=dalUtility.ConvertToString(rdr["link_man"]);
                        model.Addr=dalUtility.ConvertToString(rdr["addr"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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
        /// get all otherpayableobject
        /// <summary>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all otherpayableobject</returns>
        public BindingCollection<modOtherPayableObject> GetIList(string objectname, out string emsg)
        {
            try
            {
                BindingCollection<modOtherPayableObject> modellist = new BindingCollection<modOtherPayableObject>();
                //Execute a query to read the categories
                string sql = string.Format("select object_name,status,currency,link_man,addr,tel,remark,update_user,update_time from other_payable_object where object_name='{0}' order by object_name", objectname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modOtherPayableObject model = new modOtherPayableObject();
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.LinkMan=dalUtility.ConvertToString(rdr["link_man"]);
                        model.Addr=dalUtility.ConvertToString(rdr["addr"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of otherpayableobject</returns>
        public modOtherPayableObject GetItem(string objectname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select object_name,status,currency,link_man,addr,tel,remark,update_user,update_time from other_payable_object where object_name='{0}' order by object_name", objectname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modOtherPayableObject model = new modOtherPayableObject();
                        model.ObjectName=dalUtility.ConvertToString(rdr["object_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.LinkMan=dalUtility.ConvertToString(rdr["link_man"]);
                        model.Addr=dalUtility.ConvertToString(rdr["addr"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of otherpayableobject</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from other_payable_object";
                emsg = null;
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// insert a otherpayableobject
        /// <summary>
        /// <param name=mod>model object of otherpayableobject</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modOtherPayableObject mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into other_payable_object(object_name,status,link_man,addr,tel,remark,update_user,update_time,currency)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',getdate(),'{7}')", mod.ObjectName, mod.Status, mod.LinkMan, mod.Addr, mod.Tel, mod.Remark, mod.UpdateUser, mod.Currency);
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
        /// update a otherpayableobject
        /// <summary>
        /// <param name=objectname>objectname</param>
        /// <param name=mod>model object of otherpayableobject</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string objectname,modOtherPayableObject mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update other_payable_object set status={0},link_man='{1}',addr='{2}',tel='{3}',remark='{4}',update_user='{5}',update_time=getdate() where object_name='{6}'",mod.Status,mod.LinkMan,mod.Addr,mod.Tel,mod.Remark,mod.UpdateUser,objectname);
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
        /// delete a otherpayableobject
        /// <summary>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string objectname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete other_payable_object where object_name='{0}' ",objectname);
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
        /// change otherpayableobject's status(valid/invalid)
        /// <summary>
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string objectname,out string emsg)
        {
            try
            {
                string sql = string.Format("update other_payable_object set status=1-status where object_name='{0}' ",objectname);
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
        /// <param name=objectname>objectname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string objectname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from other_payable_object where object_name='{0}' ",objectname);
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
