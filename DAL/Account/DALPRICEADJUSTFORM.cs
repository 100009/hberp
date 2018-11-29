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
    public class dalPriceAdjustForm
    {
        /// <summary>
        /// get all priceadjustform
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all priceadjustform</returns>
        public BindingCollection<modPriceAdjustForm> GetIList(string statuslist, string formlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formlist) && formlist.CompareTo("ALL") != 0)
                    formidwhere = "and form_id in ('" + formlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                BindingCollection<modPriceAdjustForm> modellist = new BindingCollection<modPriceAdjustForm>();
                //Execute a query to read the categories
                string sql = "select form_id,form_date,status,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from price_adjust_form where 1=1 " + statuswhere + formidwhere + formdatewhere + "order by form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPriceAdjustForm model = new modPriceAdjustForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan = dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
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
        ///<returns>get a record detail of priceadjustform</returns>
        public modPriceAdjustForm GetItem(string formid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select form_id,form_date,status,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from price_adjust_form where form_id='{0}' order by form_id", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modPriceAdjustForm model = new modPriceAdjustForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan = dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime=dalUtility.ConvertToDateTime(rdr["audit_time"]);
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
        public BindingCollection<modPriceAdjustForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modPriceAdjustForm> modellist = new BindingCollection<modPriceAdjustForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select form_id,form_date,status,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from price_adjust_form a where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + " order by a.form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPriceAdjustForm model = new modPriceAdjustForm();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan = dalUtility.ConvertToString(rdr["audit_man"]);
                        model.AuditTime = dalUtility.ConvertToDateTime(rdr["audit_time"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modPriceAdjustDetail> GetCredenceList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modPriceAdjustDetail> modellist = new BindingCollection<modPriceAdjustDetail>();
                //Execute a query to read the categories

                string sql = string.Format("select a.form_id,a.seq,a.product_id,a.product_name,a.current_price,a.true_price,a.qty,a.remark "
                    + "from price_adjust_detail a, price_adjust_form b where a.form_id=b.form_id and b.acc_name='{0}' and b.acc_seq={1} order by a.form_id,a.seq", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPriceAdjustDetail model = new modPriceAdjustDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.CurrentPrice = dalUtility.ConvertToDecimal(rdr["current_price"]);
                        model.TruePrice = dalUtility.ConvertToDecimal(rdr["true_price"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CurrentMny = model.Qty * model.CurrentPrice;
                        model.TrueMny = model.Qty * model.TruePrice;
                        model.Differ = model.TrueMny - model.CurrentMny;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
        /// get all priceadjustdetail
        /// <summary>
        /// <param name=formidlist>formidlist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all priceadjustdetail</returns>
        public BindingCollection<modPriceAdjustDetail> GetDetail(string formidlist, out string emsg)
        {
            try
            {
                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and form_id in ('" + formidlist.Replace(",", "','") + "') ";

                BindingCollection<modPriceAdjustDetail> modellist = new BindingCollection<modPriceAdjustDetail>();
                //Execute a query to read the categories
                string sql = "select form_id,seq,product_id,product_name,current_price,true_price,qty,remark from price_adjust_detail where 1=1 " + formidwhere + "order by form_id,seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPriceAdjustDetail model = new modPriceAdjustDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.CurrentPrice = dalUtility.ConvertToDecimal(rdr["current_price"]);
                        model.TruePrice = dalUtility.ConvertToDecimal(rdr["true_price"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CurrentMny = model.Qty * model.CurrentPrice;
                        model.TrueMny = model.Qty * model.TruePrice;
                        model.Differ = model.TrueMny - model.CurrentMny;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
            string formid = "PAF" + temp + "-";

            string sql = "select max(form_id) from price_adjust_form where form_id like '" + formid + "%' ";
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
        /// insert a priceadjustform
        /// <summary>
        /// <param name=mod>model object of priceadjustform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modPriceAdjustForm mod, BindingCollection<modPriceAdjustDetail> list,out string emsg)
        {            
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string formid = mod.FormId;
                    
                    int? seq = 0;
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            formid = GetNewId(mod.FormDate);

                            sql = string.Format("select count(1) from price_adjust_form where form_id!='{0}' and acc_seq=0", formid);
                            if (int.Parse(SqlHelper.ExecuteScalar(sql).ToString()) > 0)
                            {
                                emsg = "您当前还有价格调整的单据未做凭证，请先处理这些单据！";
                                return false;
                            }
                            sql = string.Format("insert into price_adjust_form(form_id,form_date,status,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}',getdate())", formid, mod.FormDate, mod.Status, mod.Remark, mod.UpdateUser);
                            SqlHelper.ExecuteNonQuery(sql);
                            if (list != null && list.Count > 0)
                            {
                                seq = 0;
                                foreach (modPriceAdjustDetail modd in list)
                                {
                                    seq++;
                                    sql = string.Format("insert into price_adjust_detail(form_id,seq,product_id,product_name,current_price,true_price,qty,remark)values('{0}',{1},'{2}','{3}',{4},{5},{6},'{7}')", formid, seq, modd.ProductId, modd.ProductName, modd.CurrentPrice, modd.TruePrice, modd.Qty, modd.Remark);
                                    SqlHelper.ExecuteNonQuery(sql);
                                }
                            }
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update price_adjust_form set form_date='{0}',remark='{1}' where form_id='{2}'", mod.FormDate, mod.Remark, formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete price_adjust_detail where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modPriceAdjustDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into price_adjust_detail(form_id,seq,product_id,product_name,current_price,true_price,qty,remark)values('{0}',{1},'{2}','{3}',{4},{5},{6},'{7}')", formid, seq, modd.ProductId, modd.ProductName, modd.CurrentPrice, modd.TruePrice, modd.Qty, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete price_adjust_detail where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete price_adjust_form where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                    }

                    transaction.Complete();//就这句就可以了。  
                    emsg = formid;
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
        /// audit purchase list
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
                modPriceAdjustForm mod = GetItem(formid, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }

                sql = string.Format("update price_adjust_form set status={0},audit_man='{1}',audit_time=getdate() where form_id='{2}'", 1, updateuser, formid);
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
        /// reset purchase list
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
                modPriceAdjustForm mod = GetItem(formid, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("update price_adjust_form set status={0},audit_man='{1}',audit_time=null where form_id='{2}'", 0, updateuser, formid);
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
                string sql = string.Format("select count(1) from price_adjust_form where form_id='{0}' ",formid);
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
