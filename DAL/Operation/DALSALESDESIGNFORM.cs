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
    public class dalSalesDesignForm
    {
        /// <summary>
        /// get all salesdesignlist
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=formtypelist>formtypelist</param>
        /// <param name=custlist>custlist</param>
        /// <param name=custname>custname</param>
        /// <param name=custorderno>custorderno</param>
        /// <param name=salesmanlist>salesmanlist</param>
        /// <param name=productname>productname</param>
        /// <param name=receivestatuslist>receivestatuslist</param>
        /// <param name=invoicestatuslist>invoicestatuslist</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesdesignlist</returns>
        public BindingCollection<modSalesDesignForm> GetIList(string statuslist, string formtypelist, string custlist, string custname, string custorderno, string salesmanlist, string productname, string receivestatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignForm> modellist = new BindingCollection<modSalesDesignForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formtypewhere = string.Empty;
                if (!string.IsNullOrEmpty(formtypelist) && formtypelist.CompareTo("ALL") != 0)
                    formtypewhere = "and a.form_type in ('" + formtypelist.Replace(",", "','") + "') ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and a.cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and a.cust_name like '%" + custname + "%' ";

                string custordernowhere = string.Empty;
                if (!string.IsNullOrEmpty(custorderno))
                    custordernowhere = "and a.cust_order_no like '%" + custorderno + "%' ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and a.product_name like '%" + productname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and a.sales_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string receivestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(receivestatuslist) && receivestatuslist.CompareTo("ALL") != 0)
                    receivestatuswhere = "and a.receive_status in ('" + receivestatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and a.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string shipdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_design_form a where 1=1 " + statuswhere + formtypewhere + cusidtwhere + custnamewhere + custordernowhere + salesmanwhere + receivestatuswhere + invoicestatuswhere + shipdatewhere + " order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignForm model = new modSalesDesignForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ReceiveStatus=dalUtility.ConvertToInt(rdr["receive_status"]);
                        if (model.ReceiveStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.ReceiveDate = dalUtility.ConvertToString(rdr["receive_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.ReceiveDate = null;
                        }
                        model.InvoiceStatus=dalUtility.ConvertToInt(rdr["invoice_status"]);
                        model.InvoiceNo=dalUtility.ConvertToString(rdr["invoice_no"]);
                        model.InvoiceMny=dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
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
        /// get salesdesignlist by idlist
        /// <summary>
        /// <param name=idlist>idlist</param>        
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesdesignlist</returns>
        public BindingCollection<modSalesDesignForm> GetIList(string idlist, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignForm> modellist = new BindingCollection<modSalesDesignForm>();
                //Execute a query to read the categories
                string idlistwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idlistwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string sql = "select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_design_form a where 1=1 " + idlistwhere + " order by id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignForm model = new modSalesDesignForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ReceiveStatus = dalUtility.ConvertToInt(rdr["receive_status"]);
                        if (model.ReceiveStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.ReceiveDate = dalUtility.ConvertToString(rdr["receive_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.ReceiveDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                        model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
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
        /// get salesdesignlist by idlist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesdesignlist</returns>
        public BindingCollection<modSalesDesignForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignForm> modellist = new BindingCollection<modSalesDesignForm>();
                //Execute a query to read the categories
                
                string sql = string.Format("select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_design_form a where acc_name='{0}' and acc_seq={1} order by id desc", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignForm model = new modSalesDesignForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ReceiveStatus = dalUtility.ConvertToInt(rdr["receive_status"]);
                        if (model.ReceiveStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.ReceiveDate = dalUtility.ConvertToString(rdr["receive_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.ReceiveDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                        model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of salesdesignlist</returns>
        public modSalesDesignForm GetItem(int id,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from sales_design_form where id={0} order by id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSalesDesignForm model = new modSalesDesignForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName=dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty=dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Mny=dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ReceiveStatus=dalUtility.ConvertToInt(rdr["receive_status"]);
                        if (model.ReceiveStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.ReceiveDate = dalUtility.ConvertToString(rdr["receive_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.ReceiveDate = null;
                        }
                        model.InvoiceStatus=dalUtility.ConvertToInt(rdr["invoice_status"]);
                        model.InvoiceNo=dalUtility.ConvertToString(rdr["invoice_no"]);
                        model.InvoiceMny=dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
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
        /// get new credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesDesignForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignForm> modellist = new BindingCollection<modSalesDesignForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select id,status,form_date,form_type,ad_flag,no,cust_id,cust_name,pay_method,sales_man,cust_order_no,product_name,unit_no,qty,mny,sales_mny,currency,exchange_rate,remark,receive_status,receive_date,account_no,invoice_status,invoice_no,invoice_mny,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_design_form where status=1 and (acc_name is null or acc_name='') and acc_seq=0 " + formdatewhere + "order by id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignForm model = new modSalesDesignForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Mny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ReceiveStatus = dalUtility.ConvertToInt(rdr["receive_status"]);
                        if (model.ReceiveStatus == 1)
                        {
                            model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                            model.ReceiveDate = dalUtility.ConvertToString(rdr["receive_date"]);
                            if (model.AccountNo == "ALL")
                                model.AccountNo = string.Empty;
                        }
                        else
                        {
                            model.AccountNo = string.Empty;
                            model.ReceiveDate = null;
                        }
                        model.InvoiceStatus = dalUtility.ConvertToInt(rdr["invoice_status"]);
                        model.InvoiceNo = dalUtility.ConvertToString(rdr["invoice_no"]);
                        model.InvoiceMny = dalUtility.ConvertToDecimal(rdr["invoice_mny"]);
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
        /// get all salesshipment
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesDesignSummary> GetSalesDesignSummary(string statuslist, string formtypelist, string custlist, string custname, string salesmanlist, string receivestatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesDesignSummary> modellist = new BindingCollection<modSalesDesignSummary>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string shiptypewhere = string.Empty;
                if (!string.IsNullOrEmpty(formtypelist) && formtypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and a.form_type in ('" + formtypelist.Replace(",", "','") + "') ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and a.cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and a.cust_name like '%" + custname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and a.sales_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string receivestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(receivestatuslist) && receivestatuslist.CompareTo("ALL") != 0)
                    receivestatuswhere = "and a.receive_status in ('" + receivestatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and a.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string shipdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select cust_id,cust_name,form_type,ad_flag,currency,sum(qty) qty, sum(mny) mny "
                        + "from sales_design_form a where 1=1 " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + salesmanwhere + receivestatuswhere + invoicestatuswhere + shipdatewhere
                        + "group by cust_id,cust_name,form_type,ad_flag,currency order by cust_name,form_type";

                decimal sumqty = 0;
                decimal summny = 0;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesDesignSummary model = new modSalesDesignSummary();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.SumMny = dalUtility.ConvertToDecimal(rdr["mny"]);

                        sumqty += model.Qty * Convert.ToDecimal(model.AdFlag);
                        summny += model.SumMny * Convert.ToDecimal(model.AdFlag);
                        modellist.Add(model);
                    }
                }
                modSalesDesignSummary modsum = new modSalesDesignSummary();
                modsum.FormType = "合计";
                modsum.AdFlag = 0;
                modsum.CustId = "合计";
                modsum.CustName = "所有客户";
                modsum.Qty = sumqty;
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
        /// get new ship id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewNo(DateTime formdate, string custid)
        {
            string temp = formdate.ToString("yyyyMM");
            DateTime dt = Convert.ToDateTime(formdate.ToString("yyyy-MM-01"));
            string sql = string.Format("select no from customer_list where cust_id='{0}'", custid);
            string shipno = SqlHelper.ExecuteScalar(sql).ToString().Trim() + temp + "-";
            try
            {
                sql = "select max(no) from sales_design_form where cust_id = '" + custid + "' and form_date>='" + dt + "' ";
                object ret = SqlHelper.ExecuteScalar(sql);
                if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
                {
                    int no = Convert.ToInt32(ret.ToString().Replace(shipno, "").Trim()) + 1;
                    shipno += no.ToString().PadLeft(3, '0');
                }
                else
                {
                    shipno += "001";
                }
            }
            catch
            {
                shipno += "001";
            }
            return shipno;
        }

        /// <summary>
        /// insert a salesdesignlist
        /// <summary>
        /// <param name=mod>model object of salesdesignlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modSalesDesignForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sales_design_form(status,form_date,form_type,ad_flag,no,cust_id,cust_name,sales_man,cust_order_no,product_name,qty,mny,currency,exchange_rate,remark,receive_status,invoice_status,invoice_no,invoice_mny,update_user,update_time,account_no,receive_date,sales_mny,unit_no,pay_method)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}',{13},'{14}',{15},{16},'{17}',{18},'{19}',getdate(),'{20}','{21}',{22},'{23}','{24}')", mod.Status, mod.FormDate, mod.FormType, mod.AdFlag, mod.No, mod.CustId, mod.CustName, mod.SalesMan, mod.CustOrderNo, mod.ProductName, mod.Qty, mod.Mny, mod.Currency, mod.ExchangeRate, mod.Remark, mod.ReceiveStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.UpdateUser, mod.AccountNo, mod.ReceiveDate, mod.SalesManMny, mod.UnitNo, mod.PayMethod);
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
        /// update a salesdesignlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of salesdesignlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int id,modSalesDesignForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update sales_design_form set status='{0}',form_date='{1}',form_type='{2}',ad_flag='{3}',no='{4}',cust_id='{5}',cust_name='{6}',sales_man='{7}',cust_order_no='{8}',product_name='{9}',qty={10},mny={11},currency='{12}',exchange_rate={13},remark='{14}',receive_status={15},invoice_status={16},invoice_no='{17}',invoice_mny={18},account_no='{19}',receive_date='{20}',sales_mny={21},unit_no='{22}',pay_method='{23}' where id={24}", mod.Status, mod.FormDate, mod.FormType, mod.AdFlag, mod.No, mod.CustId, mod.CustName, mod.SalesMan, mod.CustOrderNo, mod.ProductName, mod.Qty, mod.Mny, mod.Currency, mod.ExchangeRate, mod.Remark, mod.ReceiveStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.AccountNo, mod.ReceiveDate, mod.SalesManMny, mod.UnitNo, mod.PayMethod, id);
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
        /// delete a salesdesignlist
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete sales_design_form where id={0} ",id);
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
        /// audit sales shipment
        /// <summary>
        /// <param name=shipid>shipid</param>
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
                string content = string.Empty;
                string salesman = string.Empty;
                string actioncode = string.Empty;

                modSalesDesignForm mod = GetItem(id, out emsg);
                switch (mod.FormType)
                {
                    case "设计服务":
                    case "来料加工":
                        actioncode = "SHIPMENT";
                        break;
                    case "设计服务退货":
                    case "来料加工退货":
                        actioncode = "WITHDRAWAL";
                        break;
                }
                dalCustomerList dalcust = new dalCustomerList();
                salesman = dalcust.GetSalesMan(mod.CustId);
                dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);

                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }
                content = "产品:" + mod.ProductName + "  数量:" + mod.Qty.ToString() + "  金额:" + mod.Mny.ToString() + mod.Currency;
                sql = string.Format("update sales_design_form set status={0},audit_man='{1}',audit_time=getdate() where id={2}", 1, updateuser, id);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}',getdate())", mod.CustId, mod.CustName, actioncode, mod.FormType, salesman, id.ToString(), string.Empty, content, string.Empty, string.Empty, mod.FormDate, mod.FormDate, modcsr.Scores * mod.Mny * mod.ExchangeRate, mod.AdFlag, mod.UpdateUser);
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
        /// reset sales shipment
        /// <summary>
        /// <param name=shipid>shipid</param>
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
                modSalesDesignForm mod = GetItem(id, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                string actioncode = string.Empty;
                switch (mod.FormType)
                {
                    case "设计服务":
                    case "来料加工":
                        actioncode = "SHIPMENT";
                        break;
                    case "设计服务退货":
                    case "来料加工退货":
                        actioncode = "WITHDRAWAL";
                        break;
                }
                sql = string.Format("delete customer_log where action_code='{0}' and form_id='{1}'", actioncode, id.ToString());
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("update sales_design_form set status={0},audit_man='{1}',audit_time=null where id={2}", 0, updateuser, id);
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
        /// reset sales shipment
        /// <summary>
        /// <param name=receiveflag>receiveflag</param>
        /// <param name=invoiceflag>invoiceflag</param>
        /// <param name=idlist>idlist</param>
        /// <param name=receivestatus>receivestatus</param>
        /// <param name=accountno>accountno</param>
        /// <param name=receivedate>receivedate</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateReceiveStatus(bool receiveflag, bool invoiceflag, string idlist, int? receivestatus, string accountno, string receivedate, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Empty;
                if (receiveflag && invoiceflag)
                    sql = string.Format("update sales_design_form set receive_status={0}, account_no='{1}', receive_date='{2}', invoice_status={3}, invoice_mny={4}, invoice_no='{5}', update_user='{6}' where id in ('" + idlist.Replace(",", "','") + "')", receivestatus == null ? 0 : 1, accountno, receivedate, invoicestatus, invoicemny, invoiceno, updateuser);
                else if (receiveflag)
                    sql = string.Format("update sales_design_form set receive_status={0}, account_no='{1}', receive_date='{2}', update_user='{3}' where id in ('" + idlist.Replace(",", "','") + "')", receivestatus == null ? 0 : 1, accountno, receivedate, updateuser);
                else if (invoiceflag)
                    sql = string.Format("update sales_design_form set invoice_status={0}, invoice_mny={1}, invoice_no='{2}', update_user='{3}' where id in ('" + idlist.Replace(",", "','") + "')", invoicestatus, invoicemny, invoiceno, updateuser);
                else
                {
                    emsg = "receiveflag and invoiceflag are both false";
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
        /// update receive status
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=receivestatus>receivestatus</param>
        /// <param name=receivedate>receivedate</param>
        /// <param name=accountno>accountno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateReceiveStatus(int id, int? receivestatus, string receivedate, string accountno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update sales_design_form set receive_status={0},receive_date='{1}',account_no='{2}',update_user='{3}' "
                    + "where id ={4}", receivestatus == null ? 0 : 1, receivedate, accountno, updateuser, id);
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
        /// <param name=id>id</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoicemny>invoicemny</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateInvoiceStatus(int id, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update sales_design_form set invoice_status={0},invoice_no='{1}',invoice_mny={2},update_user='{3}' "
                            + "where id ={4}", invoicestatus, invoiceno, invoicemny, updateuser, id);
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
        /// update sales man mny
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=salesmanmny>salesmanmny</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateSalesManMny(int id, decimal salesmny, decimal paidsalesmny, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Format("update sales_design_form set sales_mny={0},paid_sales_mny={1} where id ={2} ", salesmny, paidsalesmny, id);
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
        /// sales no exist or not
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=salesno>salesno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ExistsNo(string custid, string salesno, out string emsg)
        {
            try
            {
                emsg = string.Empty;
                string sql = string.Format("select count(1) from sales_design_form where cust_id='{0}' and no='{1}' ", custid, salesno);
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    return true;
                }
                else
                {
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
