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
    public class dalVendorOrderList
    {
        /// <summary>
        /// get all vendororderlist
        /// <summary>
        /// <param name=incfinished>incfinished</param>
        /// <param name=custid>custid</param>
        /// <param name=vendorname>vendorname</param>
        /// <param name=vendororderno>vendororderno</param>
        /// <param name=formdate>formdate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendororderlist</returns>
        public BindingCollection<modVendorOrderList> GetIList(bool incfinished, string vendorname, string vendororderno, string salesmanlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modVendorOrderList> modellist = new BindingCollection<modVendorOrderList>();
                //Execute a query to read the categories
                string vendornamewhere = string.Empty;
                if (vendorname!="ALL" && !string.IsNullOrEmpty(vendorname))
                    vendornamewhere = "and vendor_name like '%" + vendorname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and purchase_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                dalSysParameters dalpara = new dalSysParameters();
                modSysParameters modpara = dalpara.GetItem("ORDER_FINISHED_RATIO", out emsg);
                decimal ratio_f = Convert.ToDecimal(modpara.ParaValue);
                string sql = "select id,vendor_name,vendor_order_no,form_date,require_date,pay_method,purchase_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time "
                    + "from vendor_order_list where 1=1 " + vendornamewhere + salesmanwhere + formdatewhere + "order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorOrderList model = new modVendorOrderList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorOrderNo = dalUtility.ConvertToString(rdr["vendor_order_no"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.PurchaseMan = dalUtility.ConvertToString(rdr["purchase_man"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ReceivedQty = GetReceivedQty(model.VendorName, model.VendorOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ReceivedQty;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        if (incfinished || model.ReceivedQty < model.Qty * ratio_f)
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
        /// get all vendororderlist
        /// <summary>
        /// <param name=incfinished>incfinished</param>
        /// <param name=custid>custid</param>
        /// <param name=vendorname>vendorname</param>
        /// <param name=vendororderno>vendororderno</param>
        /// <param name=formdate>formdate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendororderlist</returns>
        public BindingCollection<modVendorOrderList> GetIList(string idlist, out string emsg)
        {
            try
            {
                BindingCollection<modVendorOrderList> modellist = new BindingCollection<modVendorOrderList>();
                //Execute a query to read the categories
                
                string sql = "select id,vendor_name,vendor_order_no,form_date,require_date,pay_method,purchase_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time "
                    + "from vendor_order_list where id in ('" + idlist.Replace(",","','") + "')order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorOrderList model = new modVendorOrderList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorOrderNo = dalUtility.ConvertToString(rdr["vendor_order_no"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.PurchaseMan = dalUtility.ConvertToString(rdr["purchase_man"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ReceivedQty = GetReceivedQty(model.VendorName, model.VendorOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ReceivedQty;
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
        public decimal GetReceivedQty(string vendorname, string vendororderno, string productid)
        {
            decimal purchaseqty = 0;
            string sql = string.Format("select isnull(sum(a.qty*b.ad_flag),0) from purchase_detail a inner join purchase_list b on a.purchase_id=b.purchase_id where b.vendor_name='{0}' and a.cust_order_no like '%{1}%' and a.product_id='{2}'", vendorname, vendororderno, productid);
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
            {
                if (rdr.Read())
                {
                    purchaseqty = Convert.ToDecimal(rdr[0]);
                }
            }

            return purchaseqty;
        }

        /// <summary>
        /// get table record
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of vendororderlist</returns>
        public modVendorOrderList GetItem(int id, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select id,vendor_name,vendor_order_no,form_date,require_date,pay_method,purchase_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time from vendor_order_list where id={0} order by id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modVendorOrderList model = new modVendorOrderList();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorOrderNo = dalUtility.ConvertToString(rdr["vendor_order_no"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.PurchaseMan = dalUtility.ConvertToString(rdr["purchase_man"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.SumMny = model.Qty * model.Price;
                        model.ReceivedQty = GetReceivedQty(model.VendorName, model.VendorOrderNo, model.ProductId);
                        model.Differ = model.Qty - model.ReceivedQty;
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get new purchase id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewNo(DateTime formdate, string vendorname)
        {
            string temp = formdate.ToString("yyyyMM");
            DateTime dt = Convert.ToDateTime(formdate.ToString("yyyy-MM-01"));
            string sql = string.Format("select no from vendor_list where vendor_name='{0}'", vendorname);
            string vendororderno = SqlHelper.ExecuteScalar(sql).ToString().Trim() + temp + "-";
            try
            {
                sql = "select max(vendor_order_no) from vendor_order_list where vendor_name = '" + vendorname + "' and form_date>='" + dt + "' ";
                object ret = SqlHelper.ExecuteScalar(sql);
                if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
                {
                    int no = Convert.ToInt32(ret.ToString().Replace(vendororderno, "").Trim()) + 1;
                    vendororderno += no.ToString().PadLeft(3, '0');
                }
                else
                {
                    vendororderno += "001";
                }
            }
            catch
            {
                vendororderno += "001";
            }
            return vendororderno;
        }
        /// <summary>
        /// save vend order list
        /// <summary>
        /// <param name=mod>modVendorOrderList</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(BindingCollection<modVendorOrderList> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    foreach (modVendorOrderList mod in list)
                    {
                        mod.VendorOrderNo = GetNewNo(mod.FormDate, mod.VendorName);
                        sql = string.Format("insert into vendor_order_list(vendor_name,vendor_order_no,form_date,require_date,pay_method,purchase_man,product_id,product_name,specify,size,unit_no,qty,price,currency,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},{12},'{13}','{14}','{15}',getdate())", mod.VendorName, mod.VendorOrderNo, mod.FormDate, mod.RequireDate, mod.PayMethod, mod.PurchaseMan, mod.ProductId, mod.ProductName, mod.Specify, mod.Size, mod.UnitNo, mod.Qty, mod.Price, mod.Currency, mod.Remark, mod.UpdateUser);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
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
        /// insert a vendororderlist
        /// <summary>
        /// <param name=mod>model object of vendororderlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modVendorOrderList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into vendor_order_list(vendor_name,vendor_order_no,form_date,require_date,pay_method,purchase_man,product_id,product_name,specify,size,unit_no,qty,price,currency,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',{9},'{10}',{11},{12},'{13}','{14}','{15}',getdate())", mod.VendorName, mod.VendorOrderNo, mod.FormDate, mod.RequireDate, mod.PayMethod, mod.PurchaseMan, mod.ProductId, mod.ProductName, mod.Specify, mod.Size, mod.UnitNo, mod.Qty, mod.Price, mod.Currency, mod.Remark, mod.UpdateUser);
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
        /// update a vendororderlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of vendororderlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id, modVendorOrderList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update vendor_order_list set vendor_name='{0}',vendor_order_no='{1}',form_date='{2}',require_date='{3}',pay_method='{4}',purchase_man='{5}',product_id='{6}',product_name='{7}',specify='{8}',size={9},unit_no='{10}',qty={11},price={12},currency='{13}',remark='{14}',update_user='{15}',update_time=getdate() where id={16}", mod.VendorName, mod.VendorOrderNo, mod.FormDate, mod.RequireDate, mod.PayMethod, mod.PurchaseMan, mod.ProductId, mod.ProductName, mod.Specify, mod.Size, mod.UnitNo, mod.Qty, mod.Price, mod.Currency, mod.Remark, mod.UpdateUser, id);
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
        /// delete a vendororderlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id, out string emsg)
        {
            try
            {
                string sql = string.Format("delete vendor_order_list where id={0} ", id);
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
        /// <param name=vendororderno>vendororderno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string vendorname, string vendororderno, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from vendor_order_list where vendor_name='{0}' and vendor_order_no='{1}'", vendorname, vendororderno);
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
        /// <param name=vendororderno>vendororderno</param>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string vendorname, string vendororderno, string productid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from vendor_order_list where vendor_name='{0}' and vendor_order_no='{1}' and product_id='{2}'", vendorname, vendororderno, productid);
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