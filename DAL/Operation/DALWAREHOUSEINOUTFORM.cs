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
    public class dalWarehouseInoutForm
    {
        /// <summary>
        /// get all warehouseinoutform
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseinoutform</returns>
        public BindingCollection<modWarehouseInoutForm> GetIList(string statuslist, string inouttypelist, string inoutflaglist, string warehouselist, string productlist, string productname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutForm> modellist = new BindingCollection<modWarehouseInoutForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string inoutypewhere = string.Empty;
                if (!string.IsNullOrEmpty(inouttypelist) && inouttypelist.CompareTo("ALL") != 0)
                    inoutypewhere = "and a.inout_type in ('" + inouttypelist.Replace(",", "','") + "') ";

                string warehousewhere = string.Empty;
                if (!string.IsNullOrEmpty(warehouselist) && warehouselist.CompareTo("ALL") != 0)
                    warehousewhere = "and a.warehouse_id in ('" + warehouselist.Replace(",", "','") + "') ";

                string productidwhere = string.Empty;
                if (!string.IsNullOrEmpty(productlist) && productlist.CompareTo("ALL") != 0)
                    productidwhere = "and a.product_id in ('" + productlist.Replace(",", "','") + "') ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and a.product_name like '%" + productname + "%' ";

                string inoutdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    inoutdatewhere = "and a.inout_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    inoutdatewhere += "and a.inout_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select id,inout_type,inout_flag,inout_date,no,status,price_status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from warehouse_inout_form a where 1=1 " + statuswhere + inoutypewhere + warehousewhere + productidwhere + productnamewhere + inoutdatewhere + " order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutForm model = new modWarehouseInoutForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.InoutDate=dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice=dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modWarehouseInoutForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutForm> modellist = new BindingCollection<modWarehouseInoutForm>();
                //Execute a query to read the categories
                string inoutdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    inoutdatewhere = "and a.inout_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    inoutdatewhere += "and a.inout_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select id,inout_type,inout_flag,inout_date,no,status,price_status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from warehouse_inout_form a where a.status=1 and a.price_status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + inoutdatewhere + " order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutForm model = new modWarehouseInoutForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.InoutType = dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag = dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.InoutDate = dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice = dalUtility.ConvertToDecimal(rdr["price"]);
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
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modWarehouseInoutForm> GetIList(string idlist, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutForm> modellist = new BindingCollection<modWarehouseInoutForm>();
                //Execute a query to read the categories
                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string sql = "select id,inout_type,inout_flag,inout_date,no,status,price_status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from warehouse_inout_form a where 1=1 " + idwhere + " order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutForm model = new modWarehouseInoutForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.InoutType = dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag = dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.InoutDate = dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice = dalUtility.ConvertToDecimal(rdr["price"]);
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
        /// get all warehouseinoutform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseinoutform</returns>
        public BindingCollection<modWarehouseInoutForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseInoutForm> modellist = new BindingCollection<modWarehouseInoutForm>();
                //Execute a query to read the categories
                string sql = string.Format("select id,inout_type,inout_flag,inout_date,no,status,price_status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from warehouse_inout_form where acc_name='{0}' and acc_seq={1} order by id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseInoutForm model = new modWarehouseInoutForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.InoutDate=dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice=dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of warehouseinoutform</returns>
        public modWarehouseInoutForm GetItem(int id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,inout_type,inout_flag,inout_date,no,status,price_status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from warehouse_inout_form where id={0} order by id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modWarehouseInoutForm model = new modWarehouseInoutForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.InoutType=dalUtility.ConvertToString(rdr["inout_type"]);
                        model.InoutFlag=dalUtility.ConvertToInt(rdr["inout_flag"]);
                        model.InoutDate=dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice=dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        model.AuditMan=dalUtility.ConvertToString(rdr["audit_man"]);
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
        /// insert a warehouseinoutform
        /// <summary>
        /// <param name=mod>model object of warehouseinoutform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modWarehouseInoutForm mod,out string emsg)
        {
            try
            {
                dalWarehouseInoutType daltype = new dalWarehouseInoutType();
                modWarehouseInoutType modtype = daltype.GetItem(mod.InoutType, out emsg);
                string sql = string.Format("insert into warehouse_inout_form(inout_type,inout_flag,inout_date,status,warehouse_id,product_id,product_name,size,qty,price,remark,update_user,update_time,no)values('{0}',{1},'{2}',{3},'{4}','{5}','{6}',{7},{8},{9},'{10}','{11}',getdate(),'{12}')", mod.InoutType, modtype.InoutFlag, mod.InoutDate, mod.Status, mod.WarehouseId, mod.ProductId, mod.ProductName, mod.Size, mod.Qty, mod.CostPrice, mod.Remark, mod.UpdateUser, mod.No);
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
        /// update a warehouseinoutform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of warehouseinoutform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id,modWarehouseInoutForm mod,out string emsg)
        {
            try
            {
                dalWarehouseInoutType daltype = new dalWarehouseInoutType();
                modWarehouseInoutType modtype = daltype.GetItem(mod.InoutType, out emsg);
                string sql = string.Format("update warehouse_inout_form set inout_type='{0}',inout_flag={1},inout_date='{2}',status={3},warehouse_id='{4}',product_id='{5}',product_name='{6}',size={7},qty={8},price={9},remark='{10}',no='{11}',price_status={12} where id={13}", mod.InoutType, modtype.InoutFlag, mod.InoutDate, mod.Status, mod.WarehouseId, mod.ProductId, mod.ProductName, mod.Size, mod.Qty, mod.CostPrice, mod.Remark, mod.No, 0, id);
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
        /// update price
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of warehouseinoutform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdatePrice(int id, decimal price, out string emsg)
        {
            try
            {
                string sql = string.Format("update warehouse_inout_form set price={0},price_status={1} where id={2}", price, 1, id);
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
        /// delete a warehouseinoutform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete warehouse_inout_form where id={0} ",id);
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
        /// audit warehouse inout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(int id, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modWarehouseInoutForm mod = GetItem(id, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }
                if(mod.InoutFlag==1)
                    sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", mod.WarehouseId, mod.ProductId, mod.Size, id, mod.InoutType, mod.No, mod.InoutDate, 0, mod.Qty, 0, mod.Remark, updateuser);
                else
                    sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", mod.WarehouseId, mod.ProductId, mod.Size, id, mod.InoutType, mod.No, mod.InoutDate, 0, 0, mod.Qty, mod.Remark, updateuser);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update warehouse_inout_form set status={0},audit_man='{1}',audit_time=getdate() where id='{2}'", 1, updateuser, id);
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
        /// reset warehouse inout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(int id, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modWarehouseInoutForm mod = GetItem(id, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("delete warehouse_product_inout where form_id='{0}' and form_type='{1}'", id, mod.InoutType);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update warehouse_inout_form set status={0},audit_man='{1}',audit_time=null where id='{2}'", 0, updateuser, id);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(int id, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from warehouse_inout_form where id={0} ",id);
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
