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
    public class dalWarehouseProductInout
    {
        /// <summary>
        /// get all warehouseproductinout
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all warehouseproductinout</returns>
        public BindingCollection<modWarehouseProductInout> GetIList(string warehouselist, string productidlist, string productname, string sizelist, string formtypelist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseProductInout> modellist = new BindingCollection<modWarehouseProductInout>();
                //Execute a query to read the categories
                string warehousewhere = string.Empty;
                if (!string.IsNullOrEmpty(warehouselist) && warehouselist.CompareTo("ALL") != 0)
                    warehousewhere = "and a.warehouse_id in ('" + warehouselist.Replace(",", "','") + "') ";

                string productidwhere = string.Empty;
                if (!string.IsNullOrEmpty(productidlist) && productidlist.CompareTo("ALL") != 0)
                    productidwhere = "and a.product_id in ('" + productidlist.Replace(",", "','") + "') ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and a.product_name like '%" + productname + "%' ";

                string sizewhere = string.Empty;
                if (!string.IsNullOrEmpty(sizelist) && sizelist.CompareTo("ALL") != 0)
                    sizewhere = "and a.size in ('" + sizelist.Replace(",", "','") + "') ";

                string formtypewhere = string.Empty;
                if (!string.IsNullOrEmpty(formtypelist) && formtypelist.CompareTo("ALL") != 0)
                    formtypewhere = "and a.form_type in ('" + formtypelist.Replace(",", "','") + "') ";

                string inoutdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    inoutdatewhere = "and a.inout_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    inoutdatewhere += "and a.inout_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select a.id,a.warehouse_id,a.product_id,b.product_name,a.size,a.form_id,a.form_type,a.inv_no,a.inout_date,a.start_qty,a.input_qty,a.output_qty,a.remark,a.update_user,a.update_time "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id "
                        + warehousewhere + productidwhere + productnamewhere + sizewhere + formtypewhere + inoutdatewhere + "order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseProductInout model = new modWarehouseProductInout();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.InvNo=dalUtility.ConvertToString(rdr["inv_no"]);
                        model.InoutDate=dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.StartQty=dalUtility.ConvertToString(rdr["start_qty"]);
                        model.InputQty=dalUtility.ConvertToString(rdr["input_qty"]);
                        model.OutputQty=dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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
        ///<returns>get a record detail of warehouseproductinout</returns>
        public modWarehouseProductInout GetItem(int? id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time from warehouse_product_inout where id={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modWarehouseProductInout model = new modWarehouseProductInout();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.WarehouseId=dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.InvNo=dalUtility.ConvertToString(rdr["inv_no"]);
                        model.InoutDate=dalUtility.ConvertToDateTime(rdr["inout_date"]);
                        model.StartQty=dalUtility.ConvertToString(rdr["start_qty"]);
                        model.InputQty=dalUtility.ConvertToString(rdr["input_qty"]);
                        model.OutputQty=dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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

        public bool BalanceWip(int accpreviousseq, DateTime startdate, out string emsg)
        {
            dalAccPeriodList dal = new dalAccPeriodList();
            modAccPeriodList modprevious = dal.GetItem(accpreviousseq, out emsg);
            string inoutdatewhere = "and a.inout_date >= '" + modprevious.StartDate + "' and a.inout_date <= '" + modprevious.EndDate + "' ";
            string sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,start_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
                    + "select a.warehouse_id,a.product_id,a.size,0 form_id,'上月结存','','" + startdate + "',sum(a.start_qty) start_qty,sum(a.input_qty) input_qty,sum(a.output_qty) output_qty,'查询结转','Auto',getdate() "
                    + "from warehouse_product_inout a where 1=1 " + inoutdatewhere + "group by a.warehouse_id,a.product_id,a.size";
            SqlHelper.ExecuteNonQuery(sql);
            emsg = string.Empty;
            return true;
        }

        /// <summary>
        /// get product wip info
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=productid>productid</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modProductWip> GetProductWip(string accname, bool tempflag, out string emsg)
        {
            try
            {
                BindingCollection<modProductWip> modellist = new BindingCollection<modProductWip>();
                //Execute a query to read the categories
                dalAccPeriodList dal = new dalAccPeriodList();
                modAccPeriodList mod = dal.GetItem(accname, out emsg);
                string sql = string.Format("select count(1) from warehouse_product_inout where inout_date='{0}'", mod.StartDate);
                int cnt = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (cnt == 0 && mod.Seq > 1)
                {
                    BalanceWip(mod.Seq - 1, mod.StartDate, out emsg);
                }
                if (mod.LockFlag == 0 && mod.EndDate < DateTime.Today)
                    mod.EndDate = DateTime.Today;

                string tempwhere=string.Empty;
                if (tempflag)
                    tempwhere = "and b.product_type='临时' ";

                string inoutdatewhere = "and a.inout_date >= '" + mod.StartDate + "' and a.inout_date <= '" + mod.EndDate + "' ";
                sql = "select a.product_id,b.product_name,b.min_qty,b.max_qty,sum(a.size * a.start_qty) start_qty,sum(a.size * a.input_qty) input_qty,sum(a.size * a.output_qty) output_qty "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id where 1=1 " + inoutdatewhere + tempwhere
                        + "group by a.product_id,b.product_name,b.min_qty,b.max_qty order by a.product_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductWip model = new modProductWip();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_Name"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.InputQty = dalUtility.ConvertToDecimal(rdr["input_qty"]);
                        model.OutputQty = dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.EndQty = model.StartQty + model.InputQty - model.OutputQty;
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// get product wip info
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=productid>productid</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public modProductWip GetProductWipItem(string accname, string productid, out string emsg)
        {
            try
            {
                dalAccPeriodList dal = new dalAccPeriodList();
                modAccPeriodList mod = dal.GetItem(accname, out emsg);
                string inoutdatewhere = "and a.inout_date >= '" + mod.StartDate + "' and a.inout_date <= '" + mod.EndDate + "' ";
                string sql = string.Format("select a.product_id,b.product_name,b.min_qty,b.max_qty,sum(a.size * a.start_qty) start_qty,sum(a.size * a.input_qty) input_qty,sum(a.size * a.output_qty) output_qty "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id where a.product_id='{0}' " + inoutdatewhere
                        + "group by a.product_id,b.product_name,b.min_qty,b.max_qty", productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductWip model = new modProductWip();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_Name"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.InputQty = dalUtility.ConvertToDecimal(rdr["input_qty"]);
                        model.OutputQty = dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.EndQty = model.StartQty + model.InputQty - model.OutputQty;
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
                        emsg = string.Empty;
                        return model;
                    }
                    else
                    {
                        emsg = "No data found!";
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
        /// get product size wip info
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modProductSizeWip> GetProductSizeWip(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modProductSizeWip> modellist = new BindingCollection<modProductSizeWip>();
                //Execute a query to read the categories
                dalAccPeriodList dal = new dalAccPeriodList();
                modAccPeriodList mod = dal.GetItem(accname, out emsg);
                string sql = string.Format("select count(1) from warehouse_product_inout where inout_date='{0}'", mod.StartDate);
                int cnt = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (cnt == 0 && mod.Seq > 1)
                {
                    BalanceWip(mod.Seq - 1, mod.StartDate, out emsg);
                }

                if (mod.LockFlag == 0 && mod.EndDate < DateTime.Today)
                    mod.EndDate = DateTime.Today;

                string inoutdatewhere = "and a.inout_date >= '" + mod.StartDate + "' and a.inout_date <= '" + mod.EndDate + "' ";
                sql = "select a.product_id,b.product_name,a.size,sum(a.start_qty) start_qty,sum(a.input_qty) input_qty,sum(a.output_qty) output_qty "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id where 1=1 " + inoutdatewhere 
                        + "group by a.product_id,b.product_name,a.size order by a.product_id,a.size";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductSizeWip model = new modProductSizeWip();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_Name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.InputQty = dalUtility.ConvertToDecimal(rdr["input_qty"]);
                        model.OutputQty = dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.EndQty = model.StartQty + model.InputQty - model.OutputQty;
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
        /// get warehouse product info
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modWarehouseProductWip> GetWarehouseProductWip(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseProductWip> modellist = new BindingCollection<modWarehouseProductWip>();
                //Execute a query to read the categories
                dalAccPeriodList dal = new dalAccPeriodList();
                modAccPeriodList mod = dal.GetItem(accname, out emsg);
                string sql = string.Format("select count(1) from warehouse_product_inout where inout_date='{0}'", mod.StartDate);
                int cnt = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (cnt == 0 && mod.Seq > 1)
                {
                    BalanceWip(mod.Seq - 1, mod.StartDate, out emsg);
                }

                if (mod.LockFlag == 0 && mod.EndDate < DateTime.Today)
                    mod.EndDate = DateTime.Today;
                string inoutdatewhere = "and a.inout_date >= '" + mod.StartDate + "' and a.inout_date <= '" + mod.EndDate + "' ";
                sql = "select a.warehouse_id,a.product_id,b.product_name,sum(a.size * a.start_qty) start_qty,sum(a.size * a.input_qty) input_qty,sum(a.size * a.output_qty) output_qty "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id where 1=1 " + inoutdatewhere 
                        + "group by a.warehouse_id,a.product_id,b.product_name order by a.product_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseProductWip model = new modWarehouseProductWip();
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_Name"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.InputQty = dalUtility.ConvertToDecimal(rdr["input_qty"]);
                        model.OutputQty = dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.EndQty = model.StartQty + model.InputQty - model.OutputQty;
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
        /// get warehouse product size info
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public BindingCollection<modWarehouseProductSizeWip> GetWarehouseProductSizeWip(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modWarehouseProductSizeWip> modellist = new BindingCollection<modWarehouseProductSizeWip>();
                //Execute a query to read the categories
                dalAccPeriodList dal = new dalAccPeriodList();
                modAccPeriodList mod = dal.GetItem(accname, out emsg);
                string sql = string.Format("select count(1) from warehouse_product_inout where inout_date='{0}'", mod.StartDate);
                int cnt = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (cnt == 0 && mod.Seq > 1)
                {
                    BalanceWip(mod.Seq - 1, mod.StartDate, out emsg);
                }

                if (mod.LockFlag == 0 && mod.EndDate < DateTime.Today)
                    mod.EndDate = DateTime.Today;
                string inoutdatewhere = "and a.inout_date >= '" + mod.StartDate + "' and a.inout_date <= '" + mod.EndDate + "' ";
                sql = "select a.warehouse_id,a.product_id,b.product_name,a.size,sum(a.start_qty) start_qty,sum(a.input_qty) input_qty,sum(a.output_qty) output_qty "
                        + "from warehouse_product_inout a inner join product_list b on a.product_id=b.product_id where 1=1 " + inoutdatewhere
                        + "group by a.warehouse_id,a.product_id,b.product_name,a.size order by a.product_id,a.size,a.warehouse_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWarehouseProductSizeWip model = new modWarehouseProductSizeWip();
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_Name"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.StartQty = dalUtility.ConvertToDecimal(rdr["start_qty"]);
                        model.InputQty = dalUtility.ConvertToDecimal(rdr["input_qty"]);
                        model.OutputQty = dalUtility.ConvertToDecimal(rdr["output_qty"]);
                        model.EndQty = model.StartQty + model.InputQty - model.OutputQty;
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
        /// insert a warehouseproductinout
        /// <summary>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modWarehouseProductInout mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())",mod.WarehouseId,mod.ProductId,mod.Size,mod.FormId,mod.FormType,mod.InvNo,mod.InoutDate,mod.StartQty,mod.InputQty,mod.OutputQty,mod.Remark,mod.UpdateUser);
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
        /// update a warehouseproductinout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of warehouseproductinout</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modWarehouseProductInout mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update warehouse_product_inout set warehouse_id='{0}',product_id='{1}',size='{2}',form_id='{3}',form_type='{4}',inv_no='{5}',inout_date='{6}',start_qty='{7}',input_qty='{8}',output_qty='{9}',remark='{10}',update_user='{11}',update_time=getdate() where id={12}",mod.WarehouseId,mod.ProductId,mod.Size,mod.FormId,mod.FormType,mod.InvNo,mod.InoutDate,mod.StartQty,mod.InputQty,mod.OutputQty,mod.Remark,mod.UpdateUser,id);
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
        /// delete a warehouseproductinout
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete warehouse_product_inout where id={0} ",id);
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
