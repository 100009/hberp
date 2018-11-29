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
    public class dalAdminDeptList
    {
        /// <summary>
        /// get all admindeptlist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindeptlist</returns>
        public BindingCollection<modAdminDeptList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDeptList> modellist = new BindingCollection<modAdminDeptList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select dept_id,dept_desc,status,update_user,update_time from admin_dept_list where 1=1 " + getwhere + "order by dept_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDeptList model = new modAdminDeptList();
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.DeptDesc=dalUtility.ConvertToString(rdr["dept_desc"]);
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
        /// get all admindeptlist
        /// <summary>
        /// <param name=deptid>deptid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all admindeptlist</returns>
        public BindingCollection<modAdminDeptList> GetIList(string deptid, out string emsg)
        {
            try
            {
                BindingCollection<modAdminDeptList> modellist = new BindingCollection<modAdminDeptList>();
                //Execute a query to read the categories
                string sql = string.Format("select dept_id,dept_desc,status,update_user,update_time from admin_dept_list where dept_id='{0}' order by dept_id",deptid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminDeptList model = new modAdminDeptList();
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.DeptDesc=dalUtility.ConvertToString(rdr["dept_desc"]);
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
        /// <param name=deptid>deptid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of admindeptlist</returns>
        public modAdminDeptList GetItem(string deptid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select dept_id,dept_desc,status,update_user,update_time from admin_dept_list where dept_id='{0}' order by dept_id",deptid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAdminDeptList model = new modAdminDeptList();
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.DeptDesc=dalUtility.ConvertToString(rdr["dept_desc"]);
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
        /// insert a admindeptlist
        /// <summary>
        /// <param name=mod>model object of admindeptlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAdminDeptList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into admin_dept_list(dept_id,dept_desc,status,update_user,update_time)values('{0}','{1}',{2},'{3}',getdate())",mod.DeptId,mod.DeptDesc,mod.Status,mod.UpdateUser);
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
        /// update a admindeptlist
        /// <summary>
        /// <param name=deptid>deptid</param>
        /// <param name=mod>model object of admindeptlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string deptid,modAdminDeptList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_dept_list set dept_desc='{0}',status={1},update_user='{2}',update_time=getdate() where dept_id='{3}'",mod.DeptDesc,mod.Status,mod.UpdateUser,deptid);
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
        /// delete a admindeptlist
        /// <summary>
        /// <param name=deptid>deptid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string deptid,out string emsg)
        {
            try
            {
                string sql = string.Format("delete admin_dept_list where dept_id='{0}'",deptid);
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
        /// change admindeptlist's status(valid/invalid)
        /// <summary>
        /// <param name=deptid>deptid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string deptid,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_dept_list set status=1-status where dept_id='{0}'",deptid);
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
        /// <param name=deptid>deptid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string deptid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from admin_dept_list where dept_id='{0}'",deptid);
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
