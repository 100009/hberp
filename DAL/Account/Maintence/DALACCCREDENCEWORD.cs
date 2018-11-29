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
    public class dalAccCredenceWord
    {
        /// <summary>
        /// get all acccredenceword
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredenceword</returns>
        public BindingCollection<modAccCredenceWord> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceWord> modellist = new BindingCollection<modAccCredenceWord>();
                //Execute a query to read the categories
                string sql = "select credence_word,update_user,update_time from acc_credence_word order by credence_word";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceWord model = new modAccCredenceWord();
                        model.CredenceWord=dalUtility.ConvertToString(rdr["credence_word"]);
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
        /// <param name=credenceword>credenceword</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of acccredenceword</returns>
        public modAccCredenceWord GetItem(string credenceword,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select credence_word,update_user,update_time from acc_credence_word where credence_word='{0}' order by credence_word",credenceword);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccCredenceWord model = new modAccCredenceWord();
                        model.CredenceWord=dalUtility.ConvertToString(rdr["credence_word"]);
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
        /// insert a acccredenceword
        /// <summary>
        /// <param name=mod>model object of acccredenceword</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccCredenceWord mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_credence_word(credence_word,update_user,update_time)values('{0}','{1}',getdate())",mod.CredenceWord,mod.UpdateUser);
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
        /// update a acccredenceword
        /// <summary>
        /// <param name=credenceword>credenceword</param>
        /// <param name=mod>model object of acccredenceword</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string credenceword,modAccCredenceWord mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_credence_word set update_user='{0}',update_time=getdate() where credence_word='{1}'",mod.UpdateUser,credenceword);
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
        /// delete a acccredenceword
        /// <summary>
        /// <param name=credenceword>credenceword</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string credenceword,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_credence_word where credence_word='{0}' ",credenceword);
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
        /// <param name=credenceword>credenceword</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string credenceword, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_credence_word where credence_word='{0}' ",credenceword);
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
