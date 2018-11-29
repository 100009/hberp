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
    public class dalSysBulletin
    {
        /// <summary>
        /// get all sysbulletin
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all sysbulletin</returns>
        public BindingCollection<modSysBulletin> GetIList(bool showall, out string emsg)
        {
            try
            {
                BindingCollection<modSysBulletin> modellist = new BindingCollection<modSysBulletin>();
                //Execute a query to read the categories
                string sql=string.Empty;
                if(showall)
                    sql = "select id,title,msg,attach_file,start_time,end_time,update_user,update_time from sys_bulletin order by id desc";
                else
                    sql = "select id,title,msg,attach_file,start_time,end_time,update_user,update_time from sys_bulletin where start_time<=getdate() and end_time>=getdate() order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSysBulletin model = new modSysBulletin();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Title=dalUtility.ConvertToString(rdr["title"]);
                        model.Msg=dalUtility.ConvertToString(rdr["msg"]);
                        model.AttachFile=dalUtility.ConvertToString(rdr["attach_file"]);
                        model.StartTime=dalUtility.ConvertToDateTime(rdr["start_time"]);
                        model.EndTime=dalUtility.ConvertToDateTime(rdr["end_time"]);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of sysbulletin</returns>
        public modSysBulletin GetItem(int? id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,title,msg,attach_file,start_time,end_time,update_user,update_time from sys_bulletin where id={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSysBulletin model = new modSysBulletin();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Title=dalUtility.ConvertToString(rdr["title"]);
                        model.Msg=dalUtility.ConvertToString(rdr["msg"]);
                        model.AttachFile=dalUtility.ConvertToString(rdr["attach_file"]);
                        model.StartTime=dalUtility.ConvertToDateTime(rdr["start_time"]);
                        model.EndTime=dalUtility.ConvertToDateTime(rdr["end_time"]);
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
        /// insert a sysbulletin
        /// <summary>
        /// <param name=mod>model object of sysbulletin</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSysBulletin mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_bulletin(title,msg,attach_file,start_time,end_time,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}',getdate())",mod.Title,mod.Msg,mod.AttachFile,mod.StartTime,mod.EndTime,mod.UpdateUser);
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
        /// update a sysbulletin
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of sysbulletin</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modSysBulletin mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_bulletin set title='{0}',msg='{1}',attach_file='{2}',start_time='{3}',end_time='{4}',update_user='{5}', update_time=getdate() where id={6}",mod.Title,mod.Msg,mod.AttachFile,mod.StartTime,mod.EndTime,mod.UpdateUser,id);
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
        /// delete a sysbulletin
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sys_bulletin where id={0}",id);
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

    }
}
