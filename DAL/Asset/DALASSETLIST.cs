using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using LXMS.DBUtility;
using LXMS.Model;

namespace LXMS.DAL
{
    public class dalAssetList
    {
        /// <summary>
        /// get all assetlist
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=assetidlist>assetidlist</param>
        /// <param name=assetname>assetname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetlist</returns>
        public BindingCollection<modAssetList> GetIList(string statuslist, string assetidlist, string assetname, string endaccname, out string emsg)
        {
            try
            {
                BindingCollection<modAssetList> modellist = new BindingCollection<modAssetList>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string assetidwhere = string.Empty;
                if (!string.IsNullOrEmpty(assetidlist) && assetidlist.CompareTo("ALL") != 0)
                    assetidwhere = "and asset_id in ('" + assetidlist.Replace(",", "','") + "') ";

                string assetnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(assetname) && assetname.CompareTo("ALL") != 0)
                    assetnamewhere = "and asset_name like '%" + assetname + "%' ";

                string sql = "select asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name,acc_seq "
                        + "from asset_list where 1=1 " + statuswhere + assetidwhere + assetnamewhere + " order by asset_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetList model = new modAssetList();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AssetProperty = dalUtility.ConvertToString(rdr["asset_property"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.SignDate = dalUtility.ConvertToDateTime(rdr["sign_date"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.ControlDepart = dalUtility.ConvertToString(rdr["control_depart"]);
                        model.UsingDepart = dalUtility.ConvertToString(rdr["using_depart"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.RawQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                        model.RawMny = dalUtility.ConvertToDecimal(rdr["raw_mny"]);
                        model.LastMny = dalUtility.ConvertToDecimal(rdr["last_mny"]);
                        model.DepreMny = GetDepreMny(model.AssetId, endaccname);
                        model.EvaluateLost = GetEvaluateLost(model.AssetId, endaccname);
                        model.NetMny = model.RawMny - model.DepreMny - model.EvaluateLost;
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
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

        public decimal GetDepreMny(string assetid, string endaccname)
        {
            string sql = string.Empty;
            if(!string.IsNullOrEmpty(endaccname))
                sql =  string.Format("select isnull(sum(depre_mny),0) from asset_depre_list where asset_id='{0}' and status=1 and acc_name<='{1}'", assetid, endaccname);
            else
                sql = string.Format("select isnull(sum(depre_mny),0) from asset_depre_list where asset_id='{0}' and status=1", assetid);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
        }

        public decimal GetEvaluateLost(string assetid, string endaccname)
        {
            string sql = string.Empty;
            if(!string.IsNullOrEmpty(endaccname))
                sql = string.Format("select isnull(sum(net_mny - evaluate_mny),0) from asset_evaluate where asset_id='{0}' and status=1 and acc_name<='{1}'", assetid, endaccname);
            else
                sql = string.Format("select isnull(sum(net_mny - evaluate_mny),0) from asset_evaluate where asset_id='{0}' and status=1", assetid);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
        }

        public decimal GetWorkQty(string assetid)
        {
            string sql = string.Format("select isnull(sum(work_qty),0) from asset_work_qty where asset_id='{0}'", assetid);
            return Convert.ToDecimal(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// get all assetlist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetlist</returns>
        public BindingCollection<modAssetList> GetIList(string accname, int? seq, out string emsg)
        {
            try
            {
                BindingCollection<modAssetList> modellist = new BindingCollection<modAssetList>();
                //Execute a query to read the categories
                string sql =string.Format("select asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name,acc_seq "
                        + "from asset_list where acc_name='{0}' and acc_seq={1} order by asset_id", accname, seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetList model = new modAssetList();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AssetProperty = dalUtility.ConvertToString(rdr["asset_property"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.SignDate = dalUtility.ConvertToDateTime(rdr["sign_date"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.ControlDepart = dalUtility.ConvertToString(rdr["control_depart"]);
                        model.UsingDepart = dalUtility.ConvertToString(rdr["using_depart"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.RawQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                        model.RawMny = dalUtility.ConvertToDecimal(rdr["raw_mny"]);
                        model.LastMny = dalUtility.ConvertToDecimal(rdr["last_mny"]);
                        model.DepreMny = GetDepreMny(model.AssetId, string.Empty);
                        model.EvaluateLost = GetEvaluateLost(model.AssetId, string.Empty);
                        model.NetMny = model.RawMny - model.DepreMny - model.EvaluateLost;
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
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
        /// get all assetlist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetlist</returns>
        public BindingCollection<modAssetList> GetStartAssetList(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAssetList> modellist = new BindingCollection<modAssetList>();
                //Execute a query to read the categories
                string sql = string.Format("select asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name,acc_seq "
                        + "from asset_list where acc_name='{0}' and acc_seq=0 order by asset_id", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetList model = new modAssetList();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AssetProperty = dalUtility.ConvertToString(rdr["asset_property"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.SignDate = dalUtility.ConvertToDateTime(rdr["sign_date"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.ControlDepart = dalUtility.ConvertToString(rdr["control_depart"]);
                        model.UsingDepart = dalUtility.ConvertToString(rdr["using_depart"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.RawQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                        model.RawMny = dalUtility.ConvertToDecimal(rdr["raw_mny"]);
                        model.LastMny = dalUtility.ConvertToDecimal(rdr["last_mny"]);
                        model.DepreMny = GetDepreMny(model.AssetId, string.Empty);
                        model.EvaluateLost = GetEvaluateLost(model.AssetId, string.Empty);
                        model.NetMny = model.RawMny - model.DepreMny - model.EvaluateLost;
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
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
        /// get table record
        /// <summary>
        /// <param name=assetid>assetid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of assetlist</returns>
        public modAssetList GetItem(string assetid, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name,acc_seq from asset_list where asset_id='{0}' order by asset_id", assetid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAssetList model = new modAssetList();
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.AssetProperty = dalUtility.ConvertToString(rdr["asset_property"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.SignDate = dalUtility.ConvertToDateTime(rdr["sign_date"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.ControlDepart = dalUtility.ConvertToString(rdr["control_depart"]);
                        model.UsingDepart = dalUtility.ConvertToString(rdr["using_depart"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.RawQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                        model.RawMny = dalUtility.ConvertToDecimal(rdr["raw_mny"]);
                        model.LastMny = dalUtility.ConvertToDecimal(rdr["last_mny"]);
                        model.DepreMny = GetDepreMny(model.AssetId, string.Empty);
                        model.EvaluateLost = GetEvaluateLost(model.AssetId, string.Empty);
                        model.NetMny = model.RawMny - model.DepreMny - model.EvaluateLost;
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["acc_seq"]);
                        emsg = null;
                        return model;
                    }
                    else
                    {
                        emsg = "Error on read data";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get asset depre list
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of assetlist</returns>
        public BindingCollection<modAssetDepreList> GetDepreList(out string emsg)
        {
            try
            {
                BindingCollection<modAssetDepreList> modellist = new BindingCollection<modAssetDepreList>();
                //Execute a query to read the categories
                string sql = "select acc_name,asset_id,asset_name,depre_method,depre_unit,net_mny,depre_mny,depre_qty,net_qty,remark,update_user,update_time from asset_depre_list order by asset_id, acc_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetDepreList model = new modAssetDepreList();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);
                        model.NetMny = dalUtility.ConvertToDecimal(rdr["net_mny"]);
                        model.DepreMny = dalUtility.ConvertToDecimal(rdr["depre_mny"]);
                        model.DepreQty = dalUtility.ConvertToDecimal(rdr["depre_qty"]);
                        model.NetQty = dalUtility.ConvertToDecimal(rdr["net_qty"]);
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
        /// get asset depre list
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of assetlist</returns>
        public BindingCollection<modAssetDepreList> GetWaitDepreList(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modAssetDepreList> modellist = new BindingCollection<modAssetDepreList>();
                //Execute a query to read the categories
                string sql = "select asset_id,asset_name,depre_method,raw_qty,raw_mny,(raw_mny-last_mny) ttl_mny,depre_unit from asset_list where status<=1 order by asset_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetDepreList model = new modAssetDepreList();
                        model.AccName = accname;
                        model.AssetId = dalUtility.ConvertToString(rdr["asset_id"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);                        
                        model.DepreMethod = dalUtility.ConvertToString(rdr["depre_method"]);
                        model.NetMny = Convert.ToDecimal(rdr["raw_mny"]) - GetDepreMny(model.AssetId, string.Empty);
                        model.DepreUnit = dalUtility.ConvertToString(rdr["depre_unit"]);

                        switch (model.DepreMethod)
                        {
                            case "平均年限法":
                                model.NetQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                                if (model.NetQty > 0)
                                {
                                    model.DepreQty = 1;
                                    model.DepreMny = decimal.Round(Convert.ToDecimal(rdr["ttl_mny"]) / model.NetQty, 2);
                                }
                                break;
                            case "双倍余额递减法":
                                model.NetQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                                if (model.NetQty > 0)
                                {
                                    model.DepreMny = decimal.Round(Convert.ToDecimal(rdr["ttl_mny"]) * 2 / model.NetQty, 2);
                                    if(Convert.ToDecimal(rdr["ttl_mny"])>0)
                                        model.DepreQty = decimal.Round(model.DepreMny * model.NetQty / Convert.ToDecimal(rdr["ttl_mny"]), 2);
                                }
                                break;
                            case "工作量法":
                                dalAssetWorkQty dalwork = new dalAssetWorkQty();
                                modAssetWorkQty modwork = dalwork.GetItem(model.AssetId, model.AccName, out emsg);
                                if (modwork == null)
                                {
                                    emsg = model.AssetName + "未做工量设置";
                                    return null;
                                }
                                else
                                {
                                    model.NetQty = dalUtility.ConvertToDecimal(rdr["raw_qty"]);
                                    if (model.NetQty > 0)
                                    {
                                        model.DepreQty = modwork.WorkQty;
                                        model.DepreMny = decimal.Round(Convert.ToDecimal(rdr["ttl_mny"]) * modwork.WorkQty / model.NetQty, 2);
                                    }
                                }
                                break;
                            default:
                                emsg = model.AssetName + "未设置折旧方法";
                                return null;
                        }
                        if (model.DepreMny > model.NetMny)
                            model.DepreMny = model.NetMny;                        
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
        /// save init check list
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=list>list of mod asset list</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool SaveStartAssetList(string accname, BindingCollection<modAssetList> list, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete asset_list where acc_name='{0}' and acc_seq=0", accname);
                    SqlHelper.ExecuteNonQuery(sql);
                    decimal summny = 0;
                    int assid = 0;
                    string assetid = string.Empty;
                    dalAssetAdd daladd = new dalAssetAdd();
                    foreach (modAssetList mod in list)
                    {
                        assid++;
                        if (assid == 1)
                            assetid = daladd.GetNewId(mod.SignDate);
                        else
                            assetid = assetid.Substring(0, 9) + (Convert.ToInt32(assetid.Substring(9, 4)) + 1).ToString().Trim().PadLeft(4, '0');

                        sql = string.Format("insert into asset_list(asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,raw_mny,last_mny,depre_unit,remark,update_user,update_time,acc_name)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},{11},'{12}','{13}','{14}',getdate(),'{15}')", assetid, mod.AssetName, mod.AssetProperty, mod.Status, mod.SignDate, mod.PurchaseDate, mod.ControlDepart, mod.UsingDepart, mod.DepreMethod, mod.RawQty, mod.NetMny, mod.LastMny, mod.DepreUnit, mod.Remark, mod.UpdateUser, accname);
                        SqlHelper.ExecuteNonQuery(sql);
                        summny += mod.NetMny;
                    }
                    int detailseq = GetNewDetailSeq(accname);
                    sql = string.Format("delete acc_credence_detail where acc_name='{0}' and subject_id in ('2115','2120') and seq=0", accname);
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq, "期初数据", "2115", "固定资产原值", "", "", summny, 0, 1, 1, 0, "人民币");
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},{12},'{13}')", accname, 0, detailseq+1, "期初数据", "2120", "固定资产净值", "", "", summny, 0, 1, 1, 1, "人民币");
                    SqlHelper.ExecuteNonQuery(sql);
                    transaction.Complete();//就这句就可以了。  
                    emsg = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    emsg = dalUtility.ErrorMessage(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// get new asset id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime formdate)
        {
            string temp = formdate.ToString("yyyyMM");
            string assetid = "AS" + temp + "-";
            string sql = "select max(asset_id) from asset_list where asset_id like '" + assetid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(assetid, "").Trim()) + 1;
                assetid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                assetid += "0001";
            }
            return assetid;
        }

        private int GetNewDetailSeq(string accname)
        {
            string sql = string.Format("Select isnull(max(detail_seq),2000) + 2 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=2002 and detail_seq<=2099", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        /// <summary>
        /// insert a assetlist
        /// <summary>
        /// <param name=mod>model object of assetlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAssetList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into asset_list(asset_id,asset_name,asset_property,status,sign_date,purchase_date,control_depart,using_depart,depre_method,raw_qty,last_mny,depre_unit,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},{10},'{11}','{12}','{13}',getdate())", mod.AssetId, mod.AssetName, mod.AssetProperty, mod.Status, mod.SignDate, mod.PurchaseDate, mod.ControlDepart, mod.UsingDepart, mod.DepreMethod, mod.RawQty, mod.RawMny, mod.LastMny, mod.DepreUnit, mod.Remark, mod.UpdateUser);
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
        /// update a assetlist
        /// <summary>
        /// <param name=assetid>assetid</param>
        /// <param name=mod>model object of assetlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string assetid, modAssetList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update asset_list set asset_name='{0}',asset_property='{1}',status='{2}',sign_date='{3}',purchase_date='{4}',control_depart='{5}',using_depart='{6}',depre_method='{7}',raw_qty={8},raw_mny={9},last_mny={10},depre_unit='{11}',remark='{12}' where asset_id='{13}'", mod.AssetName, mod.AssetProperty, 1, mod.SignDate, mod.PurchaseDate, mod.ControlDepart, mod.UsingDepart, mod.DepreMethod, mod.RawQty, mod.RawMny, mod.LastMny, mod.DepreUnit, mod.Remark, assetid);
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
        /// delete a assetlist
        /// <summary>
        /// <param name=assetid>assetid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string assetid, out string emsg)
        {
            try
            {
                string sql = string.Format("delete asset_list where asset_id='{0}' ", assetid);
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
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string assetid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from asset_list where asset_id='{0}' ", assetid);
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