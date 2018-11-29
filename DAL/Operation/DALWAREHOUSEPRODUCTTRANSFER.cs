using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LXMS.DBUtility;
using LXMS.Model;
using BindingCollection;

namespace LXMS.DAL
{
    public class dalWarehouseProductTransfer
    {
        /// <summary>
        /// get all warehouseproducttransfer
        /// <summary>
        /// <param name=getwhere>getwhere</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseproducttransfer</returns>
        public BindingCollection<modWarehouseProductTransfer> GetIList(string getwhere, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseProductTransfer> modellist = new BindingCollection<modWarehouseProductTransfer>();
                //Execute a query to read the categories
                string sql = "select a.id,a.warehouse_from,a.warehouse_to,a.status,a.inv_no,a.transfer_date,a.product_id,b.product_name,a.size,a.qty,a.remark,a.update_user,a.update_time "
                        + "from warehouse_product_transfer a,product_list b where a.product_id=b.product_id " + getwhere + " order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseProductTransfer model = new modWarehouseProductTransfer();
                        model.Id = Convert.ToInt32(rdr["id"]);
                        model.WarehouseFrom = Convert.ToString(rdr["warehouse_from"]);
                        model.WarehouseTo = Convert.ToString(rdr["warehouse_to"]);
                        model.Status = Convert.ToInt32(rdr["status"]);
                        model.InvNo = Convert.ToString(rdr["inv_no"]);
                        model.TransferDate = Convert.ToDateTime(rdr["transfer_date"]);
                        model.ProductId = Convert.ToString(rdr["product_id"]);
                        model.ProductName = Convert.ToString(rdr["product_name"]);
                        model.Size = Convert.ToDecimal(rdr["size"]);
                        model.Qty = Convert.ToDecimal(rdr["qty"]);
                        model.Remark = Convert.ToString(rdr["remark"]);
                        model.UpdateUser = Convert.ToString(rdr["update_user"]);
                        model.UpdateTime = Convert.ToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = null;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// get table record
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of warehouseproducttransfer</returns>
        public modWarehouseProductTransfer GetItem(int id, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.warehouse_from,a.warehouse_to,a.status,a.inv_no,a.transfer_date,a.product_id,b.product_name,a.size,a.qty,a.remark,a.update_user,a.update_time "
                        + "from warehouse_product_transfer a,product_list b where a.product_id=b.product_id and a.id={0} order by a.id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modWarehouseProductTransfer model = new modWarehouseProductTransfer();
                        model.Id = Convert.ToInt32(rdr["id"]);
                        model.WarehouseFrom = Convert.ToString(rdr["warehouse_from"]);
                        model.WarehouseTo = Convert.ToString(rdr["warehouse_to"]);
                        model.Status = Convert.ToInt32(rdr["status"]);
                        model.InvNo = Convert.ToString(rdr["inv_no"]);
                        model.TransferDate = Convert.ToDateTime(rdr["transfer_date"]);
                        model.ProductId = Convert.ToString(rdr["product_id"]);
                        model.ProductName = Convert.ToString(rdr["product_name"]);
                        model.Size = Convert.ToDecimal(rdr["size"]);
                        model.Qty = Convert.ToDecimal(rdr["qty"]);
                        model.Remark = Convert.ToString(rdr["remark"]);
                        model.UpdateUser = Convert.ToString(rdr["update_user"]);
                        model.UpdateTime = Convert.ToDateTime(rdr["update_time"]);
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
                emsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// insert a warehouseproducttransfer
        /// <summary>
        /// <param name=mod>model object of warehouseproducttransfer</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modWarehouseProductTransfer mod, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into warehouse_product_transfer(warehouse_from,warehouse_to,status,inv_no,transfer_date,product_id,size,qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',getdate())",mod.WarehouseFrom,mod.WarehouseTo,mod.Status,mod.InvNo,mod.TransferDate,mod.ProductId,mod.Size,mod.Qty,mod.Remark,mod.UpdateUser);
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
                emsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// update a warehouseproducttransfer
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of warehouseproducttransfer</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id, modWarehouseProductTransfer mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update warehouse_product_transfer set warehouse_from='{0}',warehouse_to='{1}',status='{2}',inv_no='{3}',transfer_date='{4}',product_id='{5}',size='{6}',qty='{7}',remark='{8}',update_user='{9}',update_time=getdate() where id={10}",mod.WarehouseFrom,mod.WarehouseTo,mod.Status,mod.InvNo,mod.TransferDate,mod.ProductId,mod.Size,mod.Qty,mod.Remark,mod.UpdateUser,id);
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
                emsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// delete a warehouseproducttransfer
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id, out string emsg)
        {
            try
            {
                string sql = string.Format("delete warehouse_product_transfer where id={0} ",id);
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
                emsg = ex.Message;
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
                modWarehouseProductTransfer mod = GetItem(id, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }

                sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", mod.WarehouseFrom, mod.ProductId, mod.Size, id, "转仓出库", mod.InvNo, mod.TransferDate, 0, 0, mod.Qty, mod.Remark, updateuser);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", mod.WarehouseTo, mod.ProductId, mod.Size, id, "转仓入库", mod.InvNo, mod.TransferDate, 0, mod.Qty, 0, mod.Remark, updateuser);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("update warehouse_product_transfer set status={0},audit_man='{1}',audit_time=getdate() where id='{2}'", 1, updateuser, id);
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
                modWarehouseProductTransfer mod = GetItem(id, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("delete warehouse_product_inout where form_id='{0}' and (form_type='{1}' or form_type='{2}')", id, "转仓出库", "转仓入库");
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("update warehouse_product_transfer set status={0},audit_man='{1}',audit_time=null where id='{2}'", 0, updateuser, id);
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
                string sql = string.Format("select count(1) from warehouse_product_transfer where id={0} ",id);
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
                emsg = ex.Message;
                return false;
            }
        }

    }
}
