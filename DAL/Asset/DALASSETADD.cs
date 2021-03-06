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
    public class dalAssetAdd
    {
        /// <summary>
        /// get all assetadd
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetadd</returns>
        public BindingCollection<modAssetAdd> GetIList(string statuslist, string formidlist, string assetname, string fromdate, string todate, out string emsg)
        {
            try
            {
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formidlist.Replace(",", "','") + "') ";

                string assetnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(assetname) && assetname.CompareTo("ALL") != 0)
                    assetnamewhere = "and a.asset_name like '%" + assetname + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                BindingCollection<modAssetAdd> modellist = new BindingCollection<modAssetAdd>();
                //Execute a query to read the categories
                string sql = "select a.form_id,a.form_date,a.status,a.no,a.asset_name,a.qty,a.price,a.currency,a.exchange_rate,a.remark,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from asset_add a inner join acc_subject_list b on a.subject_id=b.subject_id where 1=1 " + statuswhere + formidwhere + formdatewhere + assetnamewhere + " order by form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetAdd model = new modAssetAdd();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.Qty = dalUtility.ConvertToInt(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        /// get all assetadd
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all assetadd</returns>
        public BindingCollection<modAssetAdd> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAssetAdd> modellist = new BindingCollection<modAssetAdd>();
                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.form_date,a.status,a.no,a.asset_name,a.qty,a.price,a.currency,a.exchange_rate,a.remark,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from asset_add a inner join acc_subject_list b on a.subject_id=b.subject_id where acc_name='{0}' and acc_seq={1} order by form_id",accname,accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetAdd model = new modAssetAdd();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.Qty = dalUtility.ConvertToInt(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName=dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        ///<returns>get a record detail of assetadd</returns>
        public modAssetAdd GetItem(string formid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.form_date,a.status,a.no,a.asset_name,a.qty,a.price,a.currency,a.exchange_rate,a.remark,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from asset_add a inner join acc_subject_list b on a.subject_id=b.subject_id where form_id='{0}' order by form_id", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAssetAdd model = new modAssetAdd();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.Qty = dalUtility.ConvertToInt(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName=dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        public BindingCollection<modAssetAdd> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAssetAdd> modellist = new BindingCollection<modAssetAdd>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.form_id,a.form_date,a.status,a.no,a.asset_name,a.qty,a.price,a.currency,a.exchange_rate,a.remark,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.update_user,a.update_time,a.acc_name,a.acc_seq "
                        + "from asset_add a inner join acc_subject_list b on a.subject_id=b.subject_id where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + " order by a.form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAssetAdd model = new modAssetAdd();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AssetName = dalUtility.ConvertToString(rdr["asset_name"]);
                        model.Qty = dalUtility.ConvertToInt(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
            string formid = "AS" + temp + "-";
            string sql = "select max(form_id) from asset_add where form_id like '" + formid + "%' ";
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
        /// insert a assetadd
        /// <summary>
        /// <param name=mod>model object of assetadd</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAssetAdd mod, out string emsg)
        {
            try
            {
                string formid = GetNewId(mod.FormDate);
                string sql = string.Format("insert into asset_add(form_id,form_date,status,no,asset_name,qty,price,currency,exchange_rate,remark,subject_id,detail_id,detail_name,check_no,check_type,bank_name,promise_date,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',getdate())", formid, mod.FormDate, 0, mod.No, mod.AssetName, mod.Qty, mod.Price, mod.Currency, mod.ExchangeRate, mod.Remark, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, mod.UpdateUser);
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
        /// update a assetadd
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=mod>model object of assetadd</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string formid, modAssetAdd mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update asset_add set form_date='{0}',status='{1}',no='{2}',asset_name='{3}',qty={4},price={5},currency='{6}',exchange_rate={7},remark='{8}',subject_id='{9}',detail_id='{10}',detail_name='{11}',check_no='{12}',check_type='{13}',bank_name='{14}',promise_date='{15}' where form_id='{16}'", mod.FormDate, mod.Status, mod.No, mod.AssetName, mod.Qty, mod.Price, mod.Currency, mod.ExchangeRate, mod.Remark, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, formid);
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
        /// delete a assetadd
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string formid, out string emsg)
        {
            try
            {
                string sql = string.Format("delete asset_add where form_id='{0}' ", formid);
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
                sql = string.Format("update asset_add set status={0},update_user='{1}',update_time=getdate() where form_id='{2}'", 1, updateuser, formid);
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
                sql = string.Format("update asset_add set status={0} where form_id='{1}'", 0, formid);
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
                string sql = string.Format("select count(1) from asset_add where form_id='{0}' ",formid);
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
