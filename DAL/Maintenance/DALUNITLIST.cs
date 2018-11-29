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
    public class dalUnitList
    {
        /// <summary>
        /// get all unitlist
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all unitlist</returns>
        public BindingCollection<modUnitList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modUnitList> modellist = new BindingCollection<modUnitList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select unit_no,status,update_user,update_time from unit_list where 1=1 " + getwhere + "order by unit_no";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modUnitList model = new modUnitList();
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
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
        /// get all unitlist
        /// <summary>
        /// <param name=unitno>unitno</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all unitlist</returns>
        public BindingCollection<modUnitList> GetIList(string unitno, out string emsg)
        {
            try
            {
                BindingCollection<modUnitList> modellist = new BindingCollection<modUnitList>();
                //Execute a query to read the categories
                string sql = string.Format("select unit_no,status,update_user,update_time from unit_list where unit_no='{0}' order by unit_no",unitno);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modUnitList model = new modUnitList();
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
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
        /// <param name=unitno>unitno</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of unitlist</returns>
        public modUnitList GetItem(string unitno,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select unit_no,status,update_user,update_time from unit_list where unit_no='{0}' order by unit_no",unitno);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modUnitList model = new modUnitList();
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
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
        /// insert a unitlist
        /// <summary>
        /// <param name=mod>model object of unitlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modUnitList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into unit_list(unit_no,status,update_user,update_time)values('{0}',{1},'{2}',getdate())",mod.UnitNo,mod.Status,mod.UpdateUser);
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
        /// update a unitlist
        /// <summary>
        /// <param name=unitno>unitno</param>
        /// <param name=mod>model object of unitlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string unitno,modUnitList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update unit_list set status={0},update_user='{1}',update_time=getdate() where unit_no='{2}'",mod.Status,mod.UpdateUser,unitno);
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
        /// delete a unitlist
        /// <summary>
        /// <param name=unitno>unitno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string unitno,out string emsg)
        {
            try
            {
                string sql = string.Format("delete unit_list where unit_no='{0}'",unitno);
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
        /// change unitlist's status(valid/invalid)
        /// <summary>
        /// <param name=unitno>unitno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string unitno,out string emsg)
        {
            try
            {
                string sql = string.Format("update unit_list set status=1-status where unit_no='{0}'",unitno);
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
        /// <param name=unitno>unitno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string unitno, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from unit_list where unit_no='{0}'",unitno);
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
