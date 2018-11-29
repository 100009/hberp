using System;
using System.Collections;
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
    public class dalPurchaseList
    {
        /// <summary>
        /// get all purchaselist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all purchaselist</returns>
        public BindingCollection<modPurchaseList> GetIList(string statuslist, string purchasetypelist, string vendorname, string paystatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseList> modellist = new BindingCollection<modPurchaseList>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string purchasetypewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchasetypelist) && purchasetypelist.CompareTo("ALL") != 0)
                    purchasetypewhere = "and a.purchase_type in ('" + purchasetypelist.Replace(",", "','") + "') ";

                string vendorwhere = string.Empty;
                if (!string.IsNullOrEmpty(vendorname))
                    vendorwhere = "and a.vendor_name like '%" + vendorname + "%' ";

                string paystatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(paystatuslist) && paystatuslist.CompareTo("ALL") != 0)
                    paystatuswhere = "and a.pay_status in ('" + paystatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and a.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string purchasedatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    purchasedatewhere = "and a.purchase_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    purchasedatewhere += "and a.purchase_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,account_no,pay_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from purchase_list a where 1=1 " + statuswhere + purchasetypewhere + vendorwhere + paystatuswhere + invoicestatuswhere + purchasedatewhere + " order by purchase_id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseList model = new modPurchaseList();
                        model.PurchaseId=dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.PurchaseType=dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.PurchaseDate=dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.PurchaseNo=dalUtility.ConvertToString(rdr["no"]);
                        model.VendorName=dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.txtPayMethod=dalUtility.ConvertToString(rdr["payment_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.PayStatus=dalUtility.ConvertToInt(rdr["pay_status"]);
                        if (model.PayStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.PayDate = dalUtility.ConvertToString(rdr["pay_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.PayDate = null;
                        }
                        model.InvoiceStatus=dalUtility.ConvertToInt(rdr["invoice_status"]);
                        if (model.InvoiceStatus == 2)
                        {
                            model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                            model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
                        }
                        else
                        {
                            model.InvoiceNo = string.Empty;
                            model.InvoiceMny = 0;
                        }
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.DetailSum=dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny=dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny=dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason=dalUtility.ConvertToString(rdr["other_reason"]);
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
        /// get all purchase list
        /// <summary>
        /// <param name=purchaselist>purchaselist</param>        
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all purchase list</returns>
        public BindingCollection<modPurchaseList> GetIList(string purchaselist, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseList> modellist = new BindingCollection<modPurchaseList>();
                //Execute a query to read the categories
                string purchasewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchaselist) && purchaselist.CompareTo("ALL") != 0)
                    purchasewhere = "and a.purchase_id in ('" + purchaselist.Replace(",", "','") + "') ";

                string sql = "select purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,account_no,pay_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from purchase_list a where 1=1 " + purchasewhere + " order by purchase_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseList model = new modPurchaseList();
                        model.PurchaseId = dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.PurchaseType = dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.PurchaseNo = dalUtility.ConvertToString(rdr["no"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.txtPayMethod = dalUtility.ConvertToString(rdr["payment_method"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PayStatus = dalUtility.ConvertToInt(rdr["pay_status"]);
                        if (model.PayStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.PayDate = dalUtility.ConvertToString(rdr["pay_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.PayDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        if (model.InvoiceStatus == 2)
                        {
                            model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                            model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
                        }
                        else
                        {
                            model.InvoiceNo = string.Empty;
                            model.InvoiceMny = 0;
                        }
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.DetailSum = dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
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
        /// get all purchaselist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all purchaselist</returns>
        public BindingCollection<modPurchaseList> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseList> modellist = new BindingCollection<modPurchaseList>();
                //Execute a query to read the categories
                string sql = string.Format("select purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,account_no,pay_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from purchase_list where acc_name='{0}' and acc_seq={1} order by purchase_id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseList model = new modPurchaseList();
                        model.PurchaseId=dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.PurchaseType=dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.PurchaseDate=dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.PurchaseNo=dalUtility.ConvertToString(rdr["no"]);
                        model.VendorName=dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.txtPayMethod=dalUtility.ConvertToString(rdr["payment_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.PayStatus = dalUtility.ConvertToInt(rdr["pay_status"]);
                        if (model.PayStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.PayDate = dalUtility.ConvertToString(rdr["pay_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.PayDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        if (model.InvoiceStatus == 2)
                        {
                            model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                            model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
                        }
                        else
                        {
                            model.InvoiceNo = string.Empty;
                            model.InvoiceMny = 0;
                        }
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.DetailSum=dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny=dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny=dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason=dalUtility.ConvertToString(rdr["other_reason"]);
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
        /// get all customerorderlist
        /// <summary>
        /// <param name=formdate>formdate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerorderlist</returns>
        public BindingCollection<modCustomerOrderList> GetImportOrderData(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerOrderList> modellist = new BindingCollection<modCustomerOrderList>();
                //Execute a query to read the categories                
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select id,cust_id,cust_name,cust_order_no,form_date,require_date,pay_method,sales_man,product_id,product_name,specify,size,unit_no,qty,price,remark,currency,update_user,update_time "
                    + "from customer_order_list where 1=1 " + formdatewhere + "and not exists (select '#' from purchase_detail where cust_order_no=customer_order_list.cust_order_no and product_id=customer_order_list.product_id) order by id";
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
                        model.ShipQty = 0;
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

        /// <summary>
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salespurchasement</returns>
        public BindingCollection<modPurchaseList> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseList> modellist = new BindingCollection<modPurchaseList>();
                //Execute a query to read the categories
                string purchasedatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    purchasedatewhere = "and a.purchase_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    purchasedatewhere += "and a.purchase_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,account_no,pay_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from purchase_list a where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + purchasedatewhere + "order by purchase_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseList model = new modPurchaseList();
                        model.PurchaseId = dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.PurchaseType = dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.PurchaseNo = dalUtility.ConvertToString(rdr["no"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.txtPayMethod = dalUtility.ConvertToString(rdr["payment_method"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PayStatus = dalUtility.ConvertToInt(rdr["pay_status"]);
                        if (model.PayStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.PayDate = dalUtility.ConvertToString(rdr["pay_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.PayDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        if (model.InvoiceStatus == 2)
                        {
                            model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                            model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
                        }
                        else
                        {
                            model.InvoiceNo = string.Empty;
                            model.InvoiceMny = 0;
                        }
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.DetailSum = dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
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
        /// get table record
        /// <summary>
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of purchaselist</returns>
        public modPurchaseList GetItem(string purchaseid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,account_no,pay_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from purchase_list where purchase_id='{0}' order by purchase_id", purchaseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modPurchaseList model = new modPurchaseList();
                        model.PurchaseId=dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.PurchaseType=dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.PurchaseDate=dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.PurchaseNo=dalUtility.ConvertToString(rdr["no"]);
                        model.VendorName=dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.txtPayMethod=dalUtility.ConvertToString(rdr["payment_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.PayStatus = dalUtility.ConvertToInt(rdr["pay_status"]);
                        if (model.PayStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.PayDate = dalUtility.ConvertToString(rdr["pay_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.PayDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        if (model.InvoiceStatus == 2)
                        {
                            model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                            model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
                        }
                        else
                        {
                            model.InvoiceNo = string.Empty;
                            model.InvoiceMny = 0;
                        }
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.DetailSum=dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny=dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny=dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason=dalUtility.ConvertToString(rdr["other_reason"]);
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
        /// get all purchasedetail
        /// <summary>
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all purchasedetail</returns>
        public BindingCollection<modPurchaseDetail> GetDetail(string purchaseid, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseDetail> modellist = new BindingCollection<modPurchaseDetail>();
                //Execute a query to read the categories
                string sql = string.Format("select a.purchase_id,a.seq,a.product_id,a.product_name,b.specify,a.brand,a.size,a.unit_no,a.qty,a.price,a.currency,a.exchange_rate,a.warehouse_id,a.remark,a.cust_order_no,b.product_type,b.size_flag "
                        + "from purchase_detail a inner join product_list b on a.product_id=b.product_id where a.purchase_id='{0}' order by a.purchase_id,a.seq", purchaseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseDetail model = new modPurchaseDetail();
                        model.PurchaseId = dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
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
        /// get all purchaselist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all purchaselist</returns>
        public BindingCollection<modPurchaseSummary> GetPurchaseSummary(string statuslist, string purchasetypelist, string vendorlist, string paystatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modPurchaseSummary> modellist = new BindingCollection<modPurchaseSummary>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string purchasetypewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchasetypelist) && purchasetypelist.CompareTo("ALL") != 0)
                    purchasetypewhere = "and a.purchase_type in ('" + purchasetypelist.Replace(",", "','") + "') ";

                string vendorwhere = string.Empty;
                if (!string.IsNullOrEmpty(vendorlist))
                    vendorwhere = "and a.vendor_name in ('" + vendorlist.Replace(",", "','") + "') ";

                string paystatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(paystatuslist) && paystatuslist.CompareTo("ALL") != 0)
                    paystatuswhere = "and a.pay_status in ('" + paystatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and a.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string purchasedatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    purchasedatewhere = "and a.purchase_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    purchasedatewhere += "and a.purchase_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select purchase_type,ad_flag,vendor_name,status,currency,sum(detail_sum) detail_sum,sum(kill_mny) kill_mny,sum(other_mny) other_mny "
                        + "from purchase_list a where 1=1 " + statuswhere + purchasetypewhere + vendorwhere + paystatuswhere + invoicestatuswhere + purchasedatewhere 
                        + "group by purchase_type,ad_flag,vendor_name,status,currency order by vendor_name";

                decimal sumdetail = 0;
                decimal sumkill = 0;
                decimal sumother = 0;
                decimal summny = 0;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modPurchaseSummary model = new modPurchaseSummary();
                        model.PurchaseType = dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.DetailSum = dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.SumMny = model.DetailSum + model.OtherMny - model.KillMny;
                        sumdetail += model.DetailSum * Convert.ToDecimal(model.AdFlag);
                        sumkill += model.KillMny * Convert.ToDecimal(model.AdFlag);
                        sumother += model.OtherMny * Convert.ToDecimal(model.AdFlag);
                        summny += model.SumMny * Convert.ToDecimal(model.AdFlag);
                        modellist.Add(model);
                    }
                }
                modPurchaseSummary modsum = new modPurchaseSummary();
                modsum.PurchaseType = "合计";
                modsum.AdFlag = 0;
                modsum.VendorName = "合计";
                modsum.DetailSum = sumdetail;
                modsum.KillMny = sumkill;
                modsum.OtherMny = sumother;
                modsum.SumMny = summny;
                modellist.Add(modsum);
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
        /// get all salespurchasementdetail
        /// <summary>
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salespurchasementdetail</returns>
        public BindingCollection<modVPurchaseDetail> GetVDetail(string statuslist, string purchasetypelist, string vendorlist, string vendorname, string invno, string paystatuslist, string invoicestatuslist, string currencylist, string productidlist, string productname, string custorderno, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modVPurchaseDetail> modellist = new BindingCollection<modVPurchaseDetail>();
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and b.status in ('" + statuslist.Replace(",", "','") + "') ";

                string purchasetypewhere = string.Empty;
                if (!string.IsNullOrEmpty(purchasetypelist) && purchasetypelist.CompareTo("ALL") != 0)
                    purchasetypewhere = "and b.purchase_type in ('" + purchasetypelist.Replace(",", "','") + "') ";

                string vendorwhere = string.Empty;
                if (!string.IsNullOrEmpty(vendorlist) && vendorlist.CompareTo("ALL") != 0)
                    vendorwhere = "and b.vendor_name in ('" + vendorlist.Replace(",", "','") + "') ";

                string vendornamewhere = string.Empty;
                if (!string.IsNullOrEmpty(vendorname))
                    vendornamewhere = "and b.vendor_name like '%" + vendorname + "%' ";

                string invnowhere = string.Empty;
                if (!string.IsNullOrEmpty(invno))
                    invnowhere = "and b.inv_no like '" + invno + "%' ";

                string productidwhere = string.Empty;
                if (!string.IsNullOrEmpty(productidlist) && productidlist.CompareTo("ALL") != 0)
                    productidwhere = "and a.product_id in ('" + productidlist.Replace(",", "','") + "') ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and a.product_name like '%" + productname + "%' ";

                string custordernowhere = string.Empty;
                if (!string.IsNullOrEmpty(custorderno))
                    custordernowhere = "and a.cust_order_no like '%" + custorderno + "%' ";

                string paystatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(paystatuslist) && paystatuslist.CompareTo("ALL") != 0)
                    paystatuswhere = "and b.pay_status in ('" + paystatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and b.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string currencywhere = string.Empty;
                if (!string.IsNullOrEmpty(currencylist) && currencylist.CompareTo("ALL") != 0)
                    currencywhere = "and b.currency in ('" + currencylist.Replace(",", "','") + "') ";

                string purchasedatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    purchasedatewhere = "and b.purchase_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    purchasedatewhere += "and b.purchase_date <= '" + Convert.ToDateTime(todate) + "' ";
                //Execute a query to read the categories
                string sql = "select b.purchase_id,b.purchase_type,b.purchase_date,b.status,b.no,b.ad_flag,b.vendor_name,a.product_id,a.product_name,c.specify,a.size,a.unit_no,a.qty,a.price,a.currency,a.exchange_rate,a.warehouse_id,a.remark,a.cust_order_no "
                        + "from purchase_detail a inner join purchase_list b on a.purchase_id=b.purchase_id inner join product_list c on a.product_id=c.product_id "
                        + "where 1=1 " + statuswhere + purchasetypewhere + vendorwhere + vendornamewhere + invnowhere + paystatuswhere + invoicestatuswhere + currencywhere + productidwhere + productnamewhere + custordernowhere + purchasedatewhere + "order by a.purchase_id,a.seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVPurchaseDetail model = new modVPurchaseDetail();
                        model.PurchaseId = dalUtility.ConvertToString(rdr["purchase_id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PurchaseType = dalUtility.ConvertToString(rdr["purchase_type"]);
                        model.PurchaseDate = dalUtility.ConvertToDateTime(rdr["purchase_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
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
        /// get new purchase id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewNo(DateTime purchasedate, string vendorname)
        {
            string temp = purchasedate.ToString("yyyyMM");
            DateTime dt = Convert.ToDateTime(purchasedate.ToString("yyyy-MM-01"));
            string sql = string.Format("select no from vendor_list where vendor_name='{0}'", vendorname);
            string purchaseno = SqlHelper.ExecuteScalar(sql).ToString().Trim() + temp + "-";
            try
            {
                sql = "select max(no) from purchase_list where vendor_name = '" + vendorname + "' and purchase_date>='" + dt + "' ";
                object ret = SqlHelper.ExecuteScalar(sql);
                if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
                {
                    int no = Convert.ToInt32(ret.ToString().Replace(purchaseno, "").Trim()) + 1;
                    purchaseno += no.ToString().PadLeft(3, '0');
                }
                else
                {
                    purchaseno += "001";
                }
            }
            catch
            {
                purchaseno += "001";
            }
            return purchaseno;
        }

        /// <summary>
        /// get new purchase id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime purchasedate)
        {
            string temp = purchasedate.ToString("yyyyMM");
            string purchaseid = "PC" + temp + "-";
            string sql = "select max(purchase_id) from purchase_list where purchase_id like '" + purchaseid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(purchaseid, "").Trim()) + 1;
                purchaseid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                purchaseid += "0001";
            }
            return purchaseid;
        }

        public bool InsertTempData(string productnamelist, string brandlist, string unitlist, string producttypelist, string sizeflaglist, string updateuser, out string emsg)
        {
            try
            {
                string[] productname = productnamelist.Split(',');
                string[] brand = brandlist.Split(',');
                string[] unitno = unitlist.Split(',');
                string[] producttype = producttypelist.Split(',');
                string[] sizeflag = sizeflaglist.Split(',');
                for (int i = 0; i < productname.Length; i++)
                {
                    dalProductTypeList dalptype = new dalProductTypeList();
                    if (!dalptype.Exists(producttype[i], out emsg))
                    {
                        modProductTypeList modptype = new modProductTypeList();
                        modptype.ProductType = producttype[i];
                        modptype.Status = 0;
                        modptype.UpdateUser = updateuser;
                        dalptype.Insert(modptype, out emsg);
                    }
                    dalProductList dalpdt = new dalProductList();
                    if (!dalpdt.ExistsProductName(productname[i], out emsg))
                    {
                        modProductList modpdt = new modProductList();
                        modpdt.ProductId = productname[i];
                        modpdt.ProductName = productname[i];
                        modpdt.SizeFlag = Convert.ToInt32(sizeflag[i]);
                        modpdt.ProductType = producttype[i];
                        modpdt.Specify = "";
                        modpdt.UnitNo = unitno[i];
                        modpdt.Brand = brand[i].ToUpper();
                        modpdt.MinQty = 0;
                        modpdt.MaxQty = 0;
                        modpdt.Remark = "";
                        modpdt.UpdateUser = updateuser;
                        dalpdt.Insert(modpdt, out emsg);
                    }
                }
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// insert a purchase list
        /// <summary>
        /// <param name=mod>model object of salespurchasement</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modPurchaseList mod, BindingCollection<modPurchaseDetail> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string purchaseid = mod.PurchaseId;
                    string purchaseno = mod.PurchaseNo;
                    int? seq = 0;

                    if (oprtype == "IMPORT")
                    {
                        string productnamelist = string.Empty;
                        string brandlist = string.Empty;
                        string unitlist = string.Empty;
                        string producttypelist = string.Empty;
                        string sizeflaglist = string.Empty;
                        foreach (modPurchaseDetail modd in list)
                        {
                            if (string.IsNullOrEmpty(productnamelist))
                            {
                                productnamelist = modd.ProductName;
                                brandlist = modd.Brand;
                                unitlist = modd.UnitNo;
                                producttypelist = modd.ProductType;
                                sizeflaglist = modd.SizeFlag.ToString().Trim();
                            }
                            else
                            {
                                productnamelist += "," + modd.ProductName;
                                brandlist += "," + modd.Brand;
                                unitlist += "," + modd.UnitNo;
                                producttypelist += "," + modd.ProductType;
                                sizeflaglist += "," + modd.SizeFlag.ToString().Trim();
                            }
                        }
                        bool ret = InsertTempData(productnamelist, brandlist, unitlist, producttypelist, sizeflaglist, mod.UpdateUser, out emsg);
                        if (!ret)
                            return false;
                    }

                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                        case "IMPORT":
                            if (Exists(purchaseid, out emsg))
                                purchaseid = GetNewId(mod.PurchaseDate);
                            if (string.IsNullOrEmpty(purchaseno) || ExistsNo(mod.VendorName, purchaseno, out emsg))
                                purchaseno = GetNewNo(mod.PurchaseDate, mod.VendorName);
                            sql = string.Format("insert into purchase_list(purchase_id,purchase_type,ad_flag,purchase_date,no,vendor_name,payment_method,status,pay_status,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,update_user,update_time,account_no,pay_date)values('{0}','{1}',{2},'{3}','{4}','{5}','{6}',{7},{8},{9},'{10}',{11},'{12}',{13},{14},{15},{16},'{17}','{18}','{19}',getdate(),'{20}','{21}')", purchaseid, mod.PurchaseType, mod.AdFlag, mod.PurchaseDate, purchaseno, mod.VendorName, mod.txtPayMethod, mod.Status, mod.PayStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.Currency, mod.ExchangeRate, mod.DetailSum, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.UpdateUser, mod.AccountNo, mod.PayDate);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modPurchaseDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into purchase_detail(purchase_id,seq,product_id,product_name,brand,size,unit_no,qty,price,currency,exchange_rate,warehouse_id,remark,cust_order_no)values('{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},'{9}',{10},'{11}','{12}','{13}')", purchaseid, seq, modd.ProductId, modd.ProductName, modd.Brand, modd.Size, modd.UnitNo, modd.Qty, modd.Price, mod.Currency, mod.ExchangeRate, modd.WarehouseId, modd.Remark, modd.CustOrderNo);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update purchase_list set purchase_type='{0}',ad_flag={1},purchase_date='{2}',no='{3}',vendor_name='{4}',payment_method='{5}',status={6},pay_status={7},invoice_status={8},invoice_no='{9}',invoice_mny={10},currency='{11}',exchange_rate={12},detail_sum={13},kill_mny={14},other_mny={15},other_reason='{16}',remark='{17}',account_no='{18}',pay_date='{19}' where purchase_id='{20}'", mod.PurchaseType, mod.AdFlag, mod.PurchaseDate, mod.PurchaseNo, mod.VendorName, mod.txtPayMethod, mod.Status, mod.PayStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.Currency, mod.ExchangeRate, mod.DetailSum, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.AccountNo, mod.PayDate, mod.PurchaseId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete purchase_detail where purchase_id='{0}'", purchaseid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modPurchaseDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into purchase_detail(purchase_id,seq,product_id,product_name,brand,size,unit_no,qty,price,currency,exchange_rate,warehouse_id,remark,cust_order_no)values('{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},'{9}',{10},'{11}','{12}','{13}')", purchaseid, seq, modd.ProductId, modd.ProductName, modd.Brand, modd.Size, modd.UnitNo, modd.Qty, modd.Price, mod.Currency, mod.ExchangeRate, modd.WarehouseId, modd.Remark, modd.CustOrderNo);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete purchase_detail where purchase_id='{0}'", mod.PurchaseId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete purchase_list where purchase_id='{0}'", mod.PurchaseId);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                    }

                    transaction.Complete();//就这句就可以了。  
                    emsg = purchaseid;
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
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(string purchaseid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modPurchaseList mod = GetItem(purchaseid, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }
                BindingCollection<modPurchaseDetail> listdetail = GetDetail(purchaseid, out emsg);
                if (listdetail != null && listdetail.Count > 0)
                {
                    foreach (modPurchaseDetail modd in listdetail)
                    {
                        sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", modd.WarehouseId, modd.ProductId, modd.Size, purchaseid, mod.PurchaseType, mod.PurchaseNo, mod.PurchaseDate, 0, mod.AdFlag * modd.Qty, 0, modd.Remark, updateuser);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                    }
                }
                sql = string.Format("update purchase_list set status={0},audit_man='{1}',audit_time=getdate() where purchase_id='{2}'", 1, updateuser, purchaseid);
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
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(string purchaseid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modPurchaseList mod = GetItem(purchaseid, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("delete warehouse_product_inout where form_id='{0}' and form_type='{1}'", purchaseid, mod.PurchaseType);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update purchase_list set status={0},audit_man='{1}',audit_time=null where purchase_id='{2}'", 0, updateuser, purchaseid);
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
        /// update paystatus
        /// <summary>
        /// <param name=payflag>payflag</param>
        /// <param name=invoiceflag>invoiceflag</param>
        /// <param name=purchaseidlist>purchaseidlist</param>
        /// <param name=paystatus>paystatus</param>
        /// <param name=accountno>accountno</param>
        /// <param name=paydate>paydate</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdatePayStatus(bool payflag, bool invoiceflag, string purchaseidlist, int? paystatus, string accountno, string paydate, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Empty;
                if (payflag && invoiceflag)
                    sql = string.Format("update purchase_list set pay_status={0}, account_no='{1}', pay_date='{2}', invoice_status={3}, invoice_mny={4}, invoice_no='{5}', update_user='{6}' where purchase_id in ('" + purchaseidlist.Replace(",", "','") + "')", paystatus == null ? 0 : 1, accountno, paydate, invoicestatus, invoicemny, invoiceno, updateuser);
                else if (payflag)
                    sql = string.Format("update purchase_list set pay_status={0}, account_no='{1}', pay_date='{2}', update_user='{3}' where purchase_id in ('" + purchaseidlist.Replace(",", "','") + "')", paystatus == null ? 0 : 1, accountno, paydate, updateuser);
                else if (invoiceflag)
                    sql = string.Format("update purchase_list set invoice_status={0}, invoice_mny={1}, invoice_no='{2}', update_user='{3}' where purchase_id in ('" + purchaseidlist.Replace(",", "','") + "')", invoicestatus, invoicemny, invoiceno, updateuser);
                else
                {
                    emsg = "payflag and invoiceflag are both false";
                    return false;
                }
                SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update pay status
        /// <summary>
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=paystatus>paystatus</param>
        /// <param name=paydate>paydate</param>
        /// <param name=accountno>accountno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdatePayStatus(string purchaseid, int? paystatus, string paydate, string accountno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update purchase_list set pay_status={0},pay_date='{1}',account_no='{2}',update_user='{3}' "
                    + "where purchase_id ='{4}'", paystatus == null ? 0 : 1, paydate, accountno, updateuser, purchaseid);
                SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update invoice status
        /// <summary>
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoicemny>invoicemny</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateInvoiceStatus(string purchaseid, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update purchase_list set invoice_status={0},invoice_no='{1}',invoice_mny={2},update_user='{3}' "
                            + "where purchase_id ='{4}'", invoicestatus, invoiceno, invoicemny, updateuser, purchaseid);
                SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return true;
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
        /// <param name=purchaseid>purchaseid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string purchaseid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from purchase_list where purchase_id='{0}' ",purchaseid);
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
        /// purchase no exist or not
        /// <summary>
        /// <param name=vendorname>vendorname</param>
        /// <param name=purchaseno>purchaseno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ExistsNo(string vendorname, string purchaseno, out string emsg)
        {
            try
            {
                emsg = string.Empty;
                string sql = string.Format("select count(1) from purchase_sales_form where vendor_name='{0}' and purchase_no='{1}' ", vendorname, purchaseno);
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    sql = string.Format("select count(1) from purchase_list where vendor_name='{0}' and no='{1}' ", vendorname, purchaseno);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
