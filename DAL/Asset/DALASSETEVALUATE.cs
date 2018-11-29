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
    public class dalAssetEvaluate
    {
        /// <summary>
        /// get all asset sale
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=formidlist>formidlist</param>
        /// <param name=assetidlist>assetidlist</param>
        /// <param name=assetname>assetname</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetadd</returns>
        public BindingCollection<modAssetEvaluate> GetIList(string statuslist, string formidlist, string assetidlist, string assetname, string fromdate, string todate, out string emsg)
        {
            try
            {
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formidlist.Replace(",", "','") + "') ";

                string assetidwhere = string.Empty;
                if (!string.IsNullOrEmpty(assetidlist) && assetidlist.CompareTo("ALL") != 0)
                    assetidwhere = "and a.asset_id in ('" + assetidlist.Replace(",", "','") + "') ";

                string assetnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(assetname) && assetname.CompareTo("ALL") != 0)
                    assetnamewhere = "and a.asset_name like '%" + assetname + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                BindingCollection<modAssetEvaluate> modellist = new BindingCollection<modAssetEvaluate>();
                //Execute a query to read the categories
                string sql = "select form_id,form_date,status,no,asset_id,asset_name,net_mny,evaluate_mny,remark,update_user,update_time,acc_name,acc_seq "
                        + "from asset_evaluate a where 1=1 " + statuswhere + formidwhere + formdatewhere + assetidwhere + assetnamewhere + " order by asset_id,form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetEvaluate model = new modAssetEvaluate();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.NetMny = dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.EvaluateMny = dalUtility.ConvertToDecimal(rdr["evaluate_mny"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// get all assetevaluate
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetevaluate</returns>
        public BindingCollection<modAssetEvaluate> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAssetEvaluate> modellist = new BindingCollection<modAssetEvaluate>();
                //Execute a query to read the categories
                string sql = string.Format("select form_id,form_date,status,no,asset_id,asset_name,net_mny,evaluate_mny,remark,update_user,update_time,acc_name,acc_seq from asset_evaluate where acc_name='{0}' and acc_seq={1} order by form_id",accname,accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetEvaluate model = new modAssetEvaluate();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.AssetId=dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName=dalUtility.ConvertToString(rdr["asset_name"]);
                        model.NetMny=dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.EvaluateMny=dalUtility.ConvertToDecimal(rdr["evaluate_mny"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of assetevaluate</returns>
        public modAssetEvaluate GetItem(string formid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select form_id,form_date,status,no,asset_id,asset_name,net_mny,evaluate_mny,remark,update_user,update_time,acc_name,acc_seq from asset_evaluate where form_id='{0}' order by form_id",formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAssetEvaluate model = new modAssetEvaluate();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.AssetId=dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName=dalUtility.ConvertToString(rdr["asset_name"]);
                        model.NetMny=dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.EvaluateMny=dalUtility.ConvertToDecimal(rdr["evaluate_mny"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq=dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modAssetEvaluate> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAssetEvaluate> modellist = new BindingCollection<modAssetEvaluate>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select form_id,form_date,status,no,asset_id,asset_name,net_mny,evaluate_mny,remark,update_user,update_time,acc_name,acc_seq "
                        + "from asset_evaluate a where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + " order by form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetEvaluate model = new modAssetEvaluate();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.NetMny = dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.EvaluateMny = dalUtility.ConvertToDecimal(rdr["evaluate_mny"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
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
        /// get new form id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime formdate)
        {
            string temp = formdate.ToString("yyyyMM");
            string formid = "ASS" + temp + "-";
            string sql = "select max(form_id) from asset_evaluate where form_id like '" + formid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(formid, "").Trim()) + 1;
                formid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                formid += "0001";
            }
            return formid;
        }

        /// <summary>
        /// insert a assetevaluate
        /// <summary>
        /// <param name=mod>model object of assetevaluate</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAssetEvaluate mod,out string emsg)
        {
            try
            {
                string formid = GetNewId(mod.FormDate);
                string sql = string.Format("insert into asset_evaluate(form_id,form_date,status,no,asset_id,asset_name,net_mny,evaluate_mny,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}',getdate())", formid, mod.FormDate, mod.Status, mod.No, mod.AssetId, mod.AssetName, mod.NetMny, mod.EvaluateMny, mod.Remark, mod.UpdateUser);
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
        /// update a assetevaluate
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=mod>model object of assetevaluate</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string formid,modAssetEvaluate mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update asset_evaluate set form_date='{0}',status='{1}',no='{2}',asset_id='{3}',asset_name='{4}',net_mny={5},evaluate_mny={6},remark='{7}' where form_id='{8}'", mod.FormDate, mod.Status, mod.No, mod.AssetId, mod.AssetName, mod.NetMny, mod.EvaluateMny, mod.Remark, formid);
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
        /// delete a assetevaluate
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string formid,out string emsg)
        {
            try
            {
                string sql = string.Format("delete asset_evaluate where form_id='{0}' ",formid);
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
        /// audit
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                sql = string.Format("update asset_evaluate set status={0},update_user='{1}',update_time=getdate() where form_id='{2}'", 1, updateuser, formid);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                trans.Commit();
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
            finally
            {
                trans.Dispose();
                cmd.Dispose();
                if (conn.State != ConnectionState.Closed)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// reset
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                sql = string.Format("update asset_evaluate set status={0} where form_id='{1}'", 0, formid);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                trans.Commit();
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
            finally
            {
                trans.Dispose();
                cmd.Dispose();
                if (conn.State != ConnectionState.Closed)
                    conn.Dispose();
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string formid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from asset_evaluate where form_id='{0}' ",formid);
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
