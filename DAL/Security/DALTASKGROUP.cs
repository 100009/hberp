using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Xml.Linq;
using LXMS.DBUtility;
using LXMS.Model;
using BindingCollection;

namespace LXMS.DAL
{
   public class dalTaskGroup
   {      
      /// <summary>
      /// get TaskGroup by groupid
      /// <summary>
      /// <param name=groupid>groupid</param>
      ///<returns>get record of TaskGroup</returns>
      ///<returns>Details about all TaskGroup</returns>
      public modTaskGroup GetItem(string groupid, out string emsg)
      {
          string sql = string.Format("select group_id,group_desc,status,seq,update_user,update_time from sys_task_group where group_id='{0}'", groupid);
          using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
          {
              if (rdr.Read())
              {
                  modTaskGroup model = new modTaskGroup();
                  model.GroupId = dalUtility.ConvertToString(rdr["group_id"]);
                  model.GroupDesc = dalUtility.ConvertToString(rdr["group_id"]);
                  model.Status = dalUtility.ConvertToInt(rdr["status"]);
                  model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                  model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                  model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                  emsg = "";
                  return model;
              }
              else
              {
                  emsg = "";
                  return null;
              }
          }
      }
       
      /// <summary>
      /// get all task group
      /// <summary>
      /// <param name=validonly>validonly</param>
      ///<returns>Details about all TaskGroup</returns>
      public BindingCollection<modTaskGroup> GetIList(bool validonly, out string emsg)
      {
          try
          {
              string sql;
              string getwhere = validonly == true ? "and status=1" : string.Empty;              
              sql = "select group_id,group_desc,status,seq,update_user,update_time from sys_task_group where 1=1 "
                  + getwhere + "order by seq";
              BindingCollection<modTaskGroup> modellist = new BindingCollection<modTaskGroup>();
              using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
              {
                  while (rdr.Read())
                  {
                      modTaskGroup model = new modTaskGroup();
                      model.GroupId = dalUtility.ConvertToString(rdr["group_id"]);
                      model.GroupDesc = dalUtility.ConvertToString(rdr["group_id"]);
                      model.Status = dalUtility.ConvertToInt(rdr["status"]);
                      model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                      model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                      model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                      modellist.Add(model);
                  }
              }
              emsg = "";
              return modellist;
          }
          catch (Exception ex)
          {
              emsg = dalUtility.ErrorMessage(ex.Message);
              return null;
          }
      }

      /// <summary>
      /// get table record count
      /// <summary>
      ///<returns>get record count of TaskGroup</returns>
      public int TotalRecords()
      {
         string sql = "Select count(1) from sys_task_group";
         return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
      }

      /// <summary>
      /// insert a TaskGroup
      /// <summary>
      /// <param name=mod>mod</param>
      /// <returns>true/false</returns>
      public bool Insert(modTaskGroup mod, out string emsg)
      {
          try
          {
              string sql = string.Format("insert into sys_task_group(group_id,group_desc,status,seq,update_user,update_time)values('{0}','{1}',{2},{3},'{4}',getdate())", mod.GroupId, mod.GroupDesc, mod.Status, mod.Seq, mod.UpdateUser);
              int i = SqlHelper.ExecuteNonQuery(sql);
              if (i > 0)
              {
                  emsg = "";
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
              emsg = dalUtility.ErrorMessage(ex.Message.ToString());
              return false;
          }
      }

      /// <summary>
      /// update a TaskGroup
      /// <summary>
      /// <param name=groupid>groupid</param>
      /// <param name=mod>mod</param>
      /// <returns>true/false</returns>
      public bool Update(string groupid, modTaskGroup mod, out string emsg)
      {
          try
          {
              string sql = string.Format("update sys_task_group set group_desc='{0}',status={1}, seq= {2}, update_user='{3}',update_time=getdate() where group_id='{4}'", mod.GroupDesc, mod.Status, mod.Seq, mod.UpdateUser, groupid);
              int i = SqlHelper.ExecuteNonQuery(sql);
              if (i > 0)
              {
                  emsg = "";
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
              emsg = dalUtility.ErrorMessage(ex.Message.ToString());
              return false;
          }
      }

      /// <summary>
      /// delete a TaskGroup
      /// <summary>
      /// <param name=groupid>groupid</param>
      /// <returns>true/false</returns>
      public bool Delete(string groupid, out string emsg)
      {
          try
          {
              string sql = string.Format("delete sys_task_group where group_id='{0}'", groupid);
              int i = SqlHelper.ExecuteNonQuery(sql);
              if (i > 0)
              {
                  emsg = "";
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
              emsg = dalUtility.ErrorMessage(ex.Message.ToString());
              return false;
          }
      }

      /// <summary>
      /// change TaskGroup's status(valid/invalid)
      /// <summary>
      /// <param name=groupid>groupid</param>
      /// <returns>true/false</returns>
      public bool Inactive(string groupid, out string emsg)
      {
          try
          {
              string sql = string.Format("update sys_task_group set status=1-status where group_id='{0}'", groupid);
              int i = SqlHelper.ExecuteNonQuery(sql);
              if (i > 0)
              {
                  emsg = "";
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
              emsg = dalUtility.ErrorMessage(ex.Message.ToString());
              return false;
          }
      }
   }
}
