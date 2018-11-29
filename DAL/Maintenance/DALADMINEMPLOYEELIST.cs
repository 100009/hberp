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
    public class dalAdminEmployeeList
    {
        /// <summary>
        /// get employee dataset
        /// <summary>
        /// <param name=validonly>validonly</param>
        /// <param name=emsg>emsg</param>
        ///<returns>dataset</returns>
        public DataSet GetDataSet(bool validonly, out string emsg)
        {
            try
            {
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select employee_id EmployeeId,employee_name EmployeeName,dept_id DeptId,duty,status from admin_employee_list where 1=1 " + getwhere + " order by employee_id";
                DataSet ds = SqlHelper.ExecuteDs(sql);
                emsg = "";
                return ds;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get all adminemployeelist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=employeelist>employeelist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all adminemployeelist</returns>
        public BindingCollection<modAdminEmployeeList> GetIList(bool validonly, string employeelist, out string emsg)
        {
            try
            {
                BindingCollection<modAdminEmployeeList> modellist = new BindingCollection<modAdminEmployeeList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;

                string employeelistwhere = string.Empty;
                if (!string.IsNullOrEmpty(employeelist) && employeelist != "ALL")
                    employeelistwhere = "and employee_id in ('" + employeelist.Replace(",", "','") + "') ";

                string sql = "select employee_id,employee_name,status,dept_id,duty,join_date,leave_date,salary,sex,id_card,birthday,school,speciality,addr,edu_degree,phone,email,qq,remark,img_path,update_user,update_time "
                        + "from admin_employee_list where 1=1 " + getwhere + employeelistwhere + "order by employee_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminEmployeeList model = new modAdminEmployeeList();
                        model.EmployeeId=dalUtility.ConvertToString(rdr["employee_id"]);
                        model.EmployeeName=dalUtility.ConvertToString(rdr["employee_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
                        model.JoinDate=dalUtility.ConvertToDateTime(rdr["join_date"]);
                        model.LeaveDate=dalUtility.ConvertToDateTime(rdr["leave_date"]);
                        model.Salary=dalUtility.ConvertToInt(rdr["salary"]);
                        model.Sex=dalUtility.ConvertToString(rdr["sex"]);
                        model.IdCard=dalUtility.ConvertToString(rdr["id_card"]);
                        model.Birthday = dalUtility.ConvertToDateTime(rdr["birthday"]);
                        model.School=dalUtility.ConvertToString(rdr["school"]);
                        model.Speciality = dalUtility.ConvertToString(rdr["speciality"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
                        model.Phone=dalUtility.ConvertToString(rdr["phone"]);
                        model.Email=dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ImgPath=dalUtility.ConvertToString(rdr["img_path"]);
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
        /// get all adminemployeelist
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=employeeid>employeeid</param>
        /// <param name=employeename>employeename</param>
        /// <param name=deptlist>deptlist</param>
        /// <param name=sexlist>sexlist</param>
        /// <param name=idcard>idcard</param>
        /// <param name=degreelist>degreelist</param>
        /// <param name=from_indate>from_indate</param>
        /// <param name=to_indate>to_indate</param>
        /// <param name=from_outdate>from_outdate</param>
        /// <param name=to_outdate>to_outdate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all adminemployeelist</returns>
        public BindingCollection<modAdminEmployeeList> GetIList(int currentPage, int pagesize, string statuslist, string employeeid, string employeename, string deptlist, string sexlist, string idcard, string degreelist, string from_indate, string to_indate, string from_outdate, string to_outdate, out string emsg)
        {
            try
            {
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist != "ALL")
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";
                
                string employeeidwhere = string.Empty;
                if (!string.IsNullOrEmpty(employeeid))
                    employeeidwhere = "and employee_id like '" + employeeid + "%' ";

                string employeenamewhere = string.Empty;
                if (!string.IsNullOrEmpty(employeename))
                    employeenamewhere = "and employee_name like '" + employeename + "%' ";

                string deptwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptlist) && deptlist != "ALL")
                    deptwhere = "and dept_id in ('" + deptlist.Replace(",", "','") + "') ";

                string sexwhere = string.Empty;
                if (!string.IsNullOrEmpty(sexlist) && sexlist != "ALL")
                    sexwhere = "and sex in ('" + sexlist.Replace(",", "','") + "') ";

                string idcardwhere = string.Empty;
                if (!string.IsNullOrEmpty(idcard))
                    idcardwhere = "and id_card like '" + idcard + "%' ";

                string edudegreewhere = string.Empty;
                if (!string.IsNullOrEmpty(degreelist) && degreelist != "ALL")
                    edudegreewhere = "and edu_degree in ('" + degreelist.Replace(",", "','") + "') ";

                string fromindatewhere = string.Empty;
                if (!string.IsNullOrEmpty(from_indate))
                    fromindatewhere = "and join_date >= '" + Convert.ToDateTime(from_indate) + "' ";

                string toindatewhere = string.Empty;
                if (!string.IsNullOrEmpty(to_indate))
                    toindatewhere += "and join_date <= '" + Convert.ToDateTime(to_indate) + "' ";

                string fromoutdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(from_outdate))
                    fromoutdatewhere = "and leave_date >= '" + Convert.ToDateTime(from_outdate) + "' ";

                string tooutdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(to_outdate))
                    tooutdatewhere += "and leave_date <= '" + Convert.ToDateTime(to_outdate) + "' ";

                int startindex = (currentPage - 1) * pagesize + 1;
                int endindex = currentPage * pagesize;
                BindingCollection<modAdminEmployeeList> modellist = new BindingCollection<modAdminEmployeeList>();
                //Execute a query to read the categories
                string sql = "select row_number() over(order by employee_id) as rn,employee_id,employee_name,status,dept_id,duty,join_date,leave_date,salary,sex,id_card,birthday,school,speciality,addr,edu_degree,phone,email,qq,remark,img_path,update_user,update_time "
                        + "from admin_employee_list where 1=1 " + statuswhere + employeeidwhere + employeenamewhere + deptwhere + sexwhere + idcardwhere + edudegreewhere + fromindatewhere + toindatewhere + fromoutdatewhere + tooutdatewhere;

                string sql2 = "select count(1) from (" + sql + ") t";
                sql = string.Format("select * from (" + sql + ") t where rn>='{0}' and rn<='{1}'", startindex, endindex);

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAdminEmployeeList model = new modAdminEmployeeList();
                        model.EmployeeId=dalUtility.ConvertToString(rdr["employee_id"]);
                        model.EmployeeName=dalUtility.ConvertToString(rdr["employee_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
                        model.JoinDate=dalUtility.ConvertToDateTime(rdr["join_date"]);
                        model.LeaveDate=dalUtility.ConvertToDateTime(rdr["leave_date"]);
                        model.Salary=dalUtility.ConvertToInt(rdr["salary"]);
                        model.Sex=dalUtility.ConvertToString(rdr["sex"]);
                        model.IdCard=dalUtility.ConvertToString(rdr["id_card"]);
                        model.Birthday = dalUtility.ConvertToDateTime(rdr["birthday"]);
                        model.School = dalUtility.ConvertToString(rdr["school"]);
                        model.Speciality = dalUtility.ConvertToString(rdr["speciality"]);
                        model.Addr=dalUtility.ConvertToString(rdr["addr"]);
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
                        model.Phone=dalUtility.ConvertToString(rdr["phone"]);
                        model.Email=dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ImgPath=dalUtility.ConvertToString(rdr["img_path"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = SqlHelper.ExecuteScalar(sql2).ToString();
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
        /// <param name=employeeid>employeeid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of adminemployeelist</returns>
        public modAdminEmployeeList GetItem(string employeeid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select employee_id,employee_name,status,dept_id,duty,join_date,leave_date,salary,sex,id_card,birthday,school,speciality,addr,edu_degree,phone,email,qq,remark,img_path,update_user,update_time from admin_employee_list where employee_id='{0}' order by employee_id",employeeid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAdminEmployeeList model = new modAdminEmployeeList();
                        model.EmployeeId=dalUtility.ConvertToString(rdr["employee_id"]);
                        model.EmployeeName=dalUtility.ConvertToString(rdr["employee_name"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.Duty=dalUtility.ConvertToString(rdr["duty"]);
                        model.JoinDate=dalUtility.ConvertToDateTime(rdr["join_date"]);
                        model.LeaveDate=dalUtility.ConvertToDateTime(rdr["leave_date"]);
                        model.Salary=dalUtility.ConvertToInt(rdr["salary"]);
                        model.Sex=dalUtility.ConvertToString(rdr["sex"]);
                        model.IdCard=dalUtility.ConvertToString(rdr["id_card"]);
                        model.Birthday = dalUtility.ConvertToDateTime(rdr["birthday"]);
                        model.School = dalUtility.ConvertToString(rdr["school"]);
                        model.Speciality = dalUtility.ConvertToString(rdr["speciality"]);
                        model.Addr=dalUtility.ConvertToString(rdr["addr"]);
                        model.EduDegree=dalUtility.ConvertToString(rdr["edu_degree"]);
                        model.Phone=dalUtility.ConvertToString(rdr["phone"]);
                        model.Email=dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ImgPath=dalUtility.ConvertToString(rdr["img_path"]);
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
        /// get count
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=deptlist>deptlist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get count</returns>
        public int GetCount(string statuslist, string deptlist, out string emsg)
        {
            try
            {
                emsg = string.Empty;
                BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string deptwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptlist) && deptlist.CompareTo("ALL") != 0)
                    deptwhere = "and dept_id in ('" + deptlist.Replace(",", "','") + "') ";

                string sql = "select count(1) from admin_employee_list where 1=1 " + statuswhere + deptwhere;
                int ret = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                return ret;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// insert a adminemployeelist
        /// <summary>
        /// <param name=mod>model object of adminemployeelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAdminEmployeeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into admin_employee_list(employee_id,employee_name,status,dept_id,duty,join_date,leave_date,salary,sex,id_card,birthday,school,speciality,addr,edu_degree,phone,email,qq,remark,img_path,update_user,update_time)values('{0}','{1}',{2},'{3}','{4}','{5}','{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',getdate())", mod.EmployeeId, mod.EmployeeName, mod.Status, mod.DeptId, mod.Duty, mod.JoinDate, mod.LeaveDate, mod.Salary, mod.Sex, mod.IdCard, mod.Birthday, mod.School, mod.Speciality, mod.Addr, mod.EduDegree, mod.Phone, mod.Email, mod.QQ, mod.Remark, mod.ImgPath, mod.UpdateUser);
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
        /// update a adminemployeelist
        /// <summary>
        /// <param name=employeeid>employeeid</param>
        /// <param name=mod>model object of adminemployeelist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string employeeid,modAdminEmployeeList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_employee_list set employee_name='{0}',status={1},duty='{2}',join_date='{3}',leave_date='{4}',sex='{5}',id_card='{6}',birthday='{7}',school='{8}',speciality='{9}',addr='{10}',edu_degree='{11}',phone='{12}',email='{13}',qq='{14}',remark='{15}',img_path='{16}',update_user='{17}',update_time=getdate() where employee_id='{18}'", mod.EmployeeName, mod.Status, mod.Duty, mod.JoinDate, mod.LeaveDate, mod.Sex, mod.IdCard, mod.Birthday, mod.School, mod.Speciality, mod.Addr, mod.EduDegree, mod.Phone, mod.Email, mod.QQ, mod.Remark, mod.ImgPath, mod.UpdateUser, employeeid);
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
        /// update dept id
        /// <summary>
        /// <param name=employeeid>employeeid</param>
        /// <param name=deptid>deptid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateDeptId(string employeeid, string deptid, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_employee_list set dept_id='{0}',update_user='{1}',update_time=getdate() where employee_id='{2}'", deptid, updateuser, employeeid);
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
        /// delete a adminemployeelist
        /// <summary>
        /// <param name=employeeid>employeeid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string employeeid, out string emsg)
        {
            try
            {
                string sql = string.Format("delete admin_employee_list where employee_id='{0}'",employeeid);
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
        /// change adminemployeelist's status(valid/invalid)
        /// <summary>
        /// <param name=employeeid>employeeid</param>
        /// <param name=outdate>outdate</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string employeeid, DateTime outdate, out string emsg)
        {
            try
            {
                string sql = string.Format("update admin_employee_list set status=1-status,leave_date='{0}' where employee_id='{1}'", outdate, employeeid);
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
        /// <param name=employeeid>employeeid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string employeeid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from admin_employee_list where employee_id='{0}'",employeeid);
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
