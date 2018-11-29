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
    public class dalCustomerOrderList
    {
        /// <summary>
        /// get all customerorderlist
        /// <summary>
        /// <param name=incfinished>incfinished</param>
        /// <param name=custid>custid</param>
        /// <param name=custname>custname</param>
        /// <param name=custorderno>custorderno</param>
        /// <param name=formdate>formdate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerorderlist</returns>
        public BindingCollection<modCustomerOrderList> GetIList(bool incfinished, string custlist, string custname, string custorderno, string salesmanlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerOrderList> modellist = new BindingCollection<modCustomerOrderList>();
                //Execute a query to read the categories
                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and cust_name like '%" + custname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and sales_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                dalSysParameters dalpara = new dalSysParameters();
                modSysParameters modpara = dalpara.GetItem("ORDER_FINISHED_RATIO", out emsg);
                decimal ratio_f = Convert.ToDecimal(modpara.ParaValue);
                string sql = "select id,cust_id,cust_name,cust_order_no,form_date,require_date,pay_method,sales_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time "
                    + "from customer_order_list where 1=1 " + cusidtwhere + custnamewhere + salesmanwhere + formdatewhere + "order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerOrderList model = new modCustomerOrderList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod=dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify=dalUtility.ConvertToString(rdr["specify"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ShipQty = GetShipQty(model.CustId, model.CustOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ShipQty;                        
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.UpdateUser=dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime=dalUtility.ConvertToDateTime(rdr["update_time"]);
                        if (incfinished || model.ShipQty < model.Qty * ratio_f)
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
        public BindingCollection<modCustomerOrderList> GetIList(string idlist, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerOrderList> modellist = new BindingCollection<modCustomerOrderList>();
                //Execute a query to read the categories
                string idtwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idtwhere = "and id in ('" + idlist.Replace(",", "','") + "') ";

                string sql = "select id,cust_id,cust_name,cust_order_no,form_date,require_date,pay_method,sales_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time "
                    + "from customer_order_list where 1=1 " + idtwhere + "order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerOrderList model = new modCustomerOrderList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ShipQty = GetShipQty(model.CustId, model.CustOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ShipQty;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
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
        public decimal GetShipQty(string custid, string custorderno, string productid)
        {
            decimal shipqty = 0;
            string sql = string.Format("select isnull(sum(a.qty*b.ad_flag),0) from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id where b.cust_id='{0}' and b.cust_order_no like '%{1}%' and a.product_id='{2}'", custid, custorderno, productid);
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
            {
                if (rdr.Read())
                {
                    shipqty = Convert.ToDecimal(rdr[0]);
                }
            }

            sql = string.Format("select isnull(sum(qty*ad_flag),0) from sales_design_form where cust_id='{0}' and cust_order_no='{1}'", custid, custorderno);
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
            {
                if (rdr.Read())
                {
                    shipqty += Convert.ToDecimal(rdr[0]);
                }
            }
            return shipqty;
        }

        /// <summary>
        /// get table record
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customerorderlist</returns>
        public modCustomerOrderList GetItem(int id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,cust_id,cust_name,cust_order_no,form_date,require_date,pay_method,sales_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time from customer_order_list where id={0} order by id",id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerOrderList model = new modCustomerOrderList();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod=dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify=dalUtility.ConvertToString(rdr["specify"]);
                        model.Size=dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ShipQty = GetShipQty(model.CustId, model.CustOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ShipQty;
                        model.Price=dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
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
        /// get all salesshipmentdetail
        /// <summary>
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipmentdetail</returns>
        public BindingCollection<modVSalesShipmentDetail> GetShipDetail(string custid, string custorderno, string productid, out string emsg)
        {
            try
            {
                BindingCollection<modVSalesShipmentDetail> modellist = new BindingCollection<modVSalesShipmentDetail>();
                
                //Execute a query to read the categories
                string sql =string.Format("select b.ship_id,b.ship_type,b.ship_date,b.status,b.no,b.cust_order_no,b.ad_flag,b.cust_id,b.cust_name,a.product_id,a.product_name,a.specify,a.size,a.unit_no,a.qty,a.price,a.sales_mny,a.currency,a.exchange_rate,a.warehouse_id,a.remark,a.seq "
                        + "from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id "
                        + "where b.cust_id='{0}' and b.cust_order_no='{1}' and a.product_id='{2}' order by a.ship_id,a.seq",custid, custorderno, productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVSalesShipmentDetail model = new modVSalesShipmentDetail();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        //model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        //model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        modellist.Add(model);
                    }
                }

                sql = string.Format("select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_design_form a where b.cust_id='{0}' and b.cust_order_no='{1}'",custid, custorderno, productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVSalesShipmentDetail model = new modVSalesShipmentDetail();
                        model.ShipId = dalUtility.ConvertToString(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
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
        /// insert a customerorderlist
        /// <summary>
        /// <param name=mod>model object of customerorderlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCustomerOrderList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into customer_order_list(cust_id,cust_name,cust_order_no,form_date,require_date,pay_method,sales_man,product_id,product_name,specify,size,unit_no,qty,price,currency,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}',{12},{13},'{14}','{15}','{16}',getdate())", mod.CustId, mod.CustName, mod.CustOrderNo, mod.FormDate, mod.RequireDate, mod.PayMethod, mod.SalesMan, mod.ProductId, mod.ProductName, mod.Specify, mod.Size, mod.UnitNo, mod.Qty, mod.Price, mod.Currency, mod.Remark, mod.UpdateUser);
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
        /// update a customerorderlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of customerorderlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id, modCustomerOrderList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_order_list set cust_id='{0}',cust_name='{1}',cust_order_no='{2}',form_date='{3}',require_date='{4}',pay_method='{5}',sales_man='{6}',product_id='{7}',product_name='{8}',specify='{9}',size={10},unit_no='{11}',qty={12},price={13},currency='{14}',remark='{15}',update_user='{16}',update_time=getdate() where id={17}", mod.CustId, mod.CustName, mod.CustOrderNo, mod.FormDate, mod.RequireDate, mod.PayMethod, mod.SalesMan, mod.ProductId, mod.ProductName, mod.Specify, mod.Size, mod.UnitNo, mod.Qty, mod.Price, mod.Currency, mod.Remark, mod.UpdateUser, id);
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
        /// delete a customerorderlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id, out string emsg)
        {
            try
            {
                string sql = string.Format("delete customer_order_list where id={0} ",id);
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
        /// <param name=custid>custid</param>
        /// <param name=custorderno>custorderno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string custid, string custorderno, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from customer_order_list where cust_id='{0}' and cust_order_no='{1}'", custid, custorderno);
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

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=custorderno>custorderno</param>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string custid, string custorderno, string productid, out string emsg)
        {            
            try
            {
                string sql = string.Format("select count(1) from customer_order_list where cust_id='{0}' and cust_order_no='{1}' and product_id='{2}'", custid, custorderno, productid);
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
