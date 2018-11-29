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
    public class dalAssetWorkQty
    {
        /// <summary>
        /// get all assetworkqty
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetworkqty</returns>
        public BindingCollection<modAssetWorkQty> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAssetWorkQty> modellist = new BindingCollection<modAssetWorkQty>();
                //Execute a query to read the categories
                string sql = "select a.asset_id,b.asset_name,a.acc_name,a.work_qty,a.remark,a.update_user,a.update_time from asset_work_qty a inner join asset_list b on a.asset_id=b.asset_id order by a.asset_id,a.update_time";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetWorkQty model = new modAssetWorkQty();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.WorkQty = dalUtility.ConvertToDecimal(rdr["work_qty"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get all assetworkqty
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetworkqty</returns>
        public BindingCollection<modAssetWorkQty> GetIList(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAssetWorkQty> modellist = new BindingCollection<modAssetWorkQty>();
                //Execute a query to read the categories
                string sql = string.Format("select a.asset_id,b.asset_name,a.acc_name,a.work_qty,a.remark,a.update_user,a.update_time from asset_work_qty a inner join asset_list b on a.asset_id=b.asset_id where a.acc_name='{0}' order by a.asset_id,a.acc_name", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetWorkQty model = new modAssetWorkQty();
                        model.AssetId=dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.WorkQty=dalUtility.ConvertToDecimal(rdr["work_qty"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
        /// <param name=assetid>assetid</param>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of assetworkqty</returns>
        public modAssetWorkQty GetItem(string assetid,string accname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.asset_id,b.asset_name,a.acc_name,a.work_qty,a.remark,a.update_user,a.update_time from asset_work_qty a inner join asset_list b on a.asset_id=b.asset_id where a.asset_id='{0}' and a.acc_name='{1}' order by a.asset_id,a.acc_name", assetid, accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAssetWorkQty model = new modAssetWorkQty();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.WorkQty = dalUtility.ConvertToDecimal(rdr["work_qty"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// insert a assetworkqty
        /// <summary>
        /// <param name=mod>model object of assetworkqty</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAssetWorkQty mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into asset_work_qty(asset_id,acc_name,work_qty,remark,update_user,update_time)values('{0}','{1}',{2},'{3}','{4}',getdate())", mod.AssetId, mod.AccName, mod.WorkQty, mod.Remark, mod.UpdateUser);
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
        /// update a assetworkqty
        /// <summary>
        /// <param name=assetid>assetid</param>
        /// <param name=accname>accname</param>
        /// <param name=mod>model object of assetworkqty</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string assetid,string accname,modAssetWorkQty mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update asset_work_qty set work_qty={0},remark='{1}' where asset_id='{2}' and acc_name='{3}'", mod.WorkQty, mod.Remark, assetid, accname);
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
        /// delete a assetworkqty
        /// <summary>
        /// <param name=assetid>assetid</param>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string assetid,string accname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete asset_work_qty where asset_id='{0}' and acc_name='{1}' ",assetid,accname);
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
        /// <param name=assetid>assetid</param>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string assetid,string accname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from asset_work_qty where asset_id='{0}' and acc_name='{1}' ",assetid,accname);
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
