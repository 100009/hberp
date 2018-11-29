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
    public class dalAccBankList
    {
        /// <summary>
        /// get all accbanklist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accbanklist</returns>
        public BindingCollection<modAccBankList> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccBankList> modellist = new BindingCollection<modAccBankList>();
                //Execute a query to read the categories
                string sql = "select bank_name,update_user,update_time from acc_bank_list order by bank_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccBankList model = new modAccBankList();
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
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
        /// get all accbanklist
        /// <summary>
        /// <param name=bankname>bankname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accbanklist</returns>
        public BindingCollection<modAccBankList> GetIList(string bankname, out string emsg)
        {
            try
            {
                BindingCollection<modAccBankList> modellist = new BindingCollection<modAccBankList>();
                //Execute a query to read the categories
                string sql = string.Format("select bank_name,update_user,update_time from acc_bank_list where bank_name='{0}' order by bank_name",bankname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccBankList model = new modAccBankList();
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
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
        /// <param name=bankname>bankname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accbanklist</returns>
        public modAccBankList GetItem(string bankname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select bank_name,update_user,update_time from acc_bank_list where bank_name='{0}' order by bank_name",bankname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccBankList model = new modAccBankList();
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
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
        ///<returns>get record count of accbanklist</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from acc_bank_list";
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
        /// insert a accbanklist
        /// <summary>
        /// <param name=mod>model object of accbanklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccBankList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_bank_list(bank_name,update_user,update_time)values('{0}','{1}',getdate())",mod.BankName,mod.UpdateUser);
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
        /// update a accbanklist
        /// <summary>
        /// <param name=bankname>bankname</param>
        /// <param name=mod>model object of accbanklist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string bankname,modAccBankList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_bank_list set update_user='{0}',update_time=getdate() where bank_name='{1}'",mod.UpdateUser,bankname);
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
        /// delete a accbanklist
        /// <summary>
        /// <param name=bankname>bankname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string bankname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_bank_list where bank_name='{0}' ",bankname);
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
        /// change accbanklist's status(valid/invalid)
        /// <summary>
        /// <param name=bankname>bankname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string bankname,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_bank_list set status=1-status where bank_name='{0}' ",bankname);
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
        /// <param name=bankname>bankname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string bankname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_bank_list where bank_name='{0}' ",bankname);
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
