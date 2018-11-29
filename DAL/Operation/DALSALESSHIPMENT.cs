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
    public class dalSalesShipment
    {
        /// <summary>
        /// get all salesshipment
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesShipment> GetIList(string statuslist, string shiptypelist, string custlist, string custname, string salesmanlist, string receivestatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipment> modellist = new BindingCollection<modSalesShipment>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string shiptypewhere = string.Empty;
                if (!string.IsNullOrEmpty(shiptypelist) && shiptypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and a.ship_type in ('" + shiptypelist.Replace(",", "','") + "') ";

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
                    shipdatewhere = "and a.ship_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and a.ship_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,account_no,receive_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_shipment a where 1=1 " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + salesmanwhere + receivestatuswhere + invoicestatuswhere + shipdatewhere + " order by ship_id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipment model = new modSalesShipment();
                        model.ShipId=dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate=dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.ShipNo=dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.ShipAddr=dalUtility.ConvertToString(rdr["ship_addr"]);
                        model.PayMethod=dalUtility.ConvertToString(rdr["pay_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipMan=dalUtility.ConvertToString(rdr["ship_man"]);
                        model.MotorMan=dalUtility.ConvertToString(rdr["motor_man"]);
                        model.MakeMan=dalUtility.ConvertToString(rdr["make_man"]);
                        model.MakeDate=dalUtility.ConvertToDateTime(rdr["make_date"]);
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
        /// get all salesshipment
        /// <summary>
        /// <param name=shiplist>shiplist</param>        
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesShipment> GetIList(string shiplist, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipment> modellist = new BindingCollection<modSalesShipment>();
                //Execute a query to read the categories
                string shipwhere = string.Empty;
                if (!string.IsNullOrEmpty(shiplist) && shiplist.CompareTo("ALL") != 0)
                    shipwhere = "and a.ship_id in ('" + shiplist.Replace(",", "','") + "') ";

                string sql = "select ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,account_no,receive_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_shipment a where 1=1 " + shipwhere + " order by ship_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipment model = new modSalesShipment();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.ShipNo = dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["ship_addr"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
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
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.MotorMan = dalUtility.ConvertToString(rdr["motor_man"]);
                        model.MakeMan = dalUtility.ConvertToString(rdr["make_man"]);
                        model.MakeDate = dalUtility.ConvertToDateTime(rdr["make_date"]);
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
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesShipment> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipment> modellist = new BindingCollection<modSalesShipment>();
                //Execute a query to read the categories
                string sql = string.Format("select ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,account_no,receive_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_shipment where acc_name='{0}' and acc_seq={1} order by ship_id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipment model = new modSalesShipment();
                        model.ShipId=dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate=dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.ShipNo=dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.ShipAddr=dalUtility.ConvertToString(rdr["ship_addr"]);
                        model.PayMethod=dalUtility.ConvertToString(rdr["pay_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipMan=dalUtility.ConvertToString(rdr["ship_man"]);
                        model.MotorMan=dalUtility.ConvertToString(rdr["motor_man"]);
                        model.MakeMan=dalUtility.ConvertToString(rdr["make_man"]);
                        model.MakeDate=dalUtility.ConvertToDateTime(rdr["make_date"]);
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

        public BindingCollection<modCollectCamp> GetCollectCamp(string salesMan, string shipDate, out string emsg)
        {
            try
            {
                BindingCollection<modCollectCamp> modellist = new BindingCollection<modCollectCamp>();
                //Execute a query to read the categories
                string getwhere = "and a.sales_man='"+ salesMan +"' and a.ship_date >= '" + Convert.ToDateTime(shipDate) + "' ";                
                string sql = "select ship_id,ship_type,ship_date,cust_id,cust_name,detail_sum,sales_man "
                        + "from sales_shipment a where a.ship_type='收营单' " + getwhere + "order by ship_id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCollectCamp model = new modCollectCamp();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["ship_date"]);                        
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);                        
                        model.DetailSum = dalUtility.ConvertToDecimal(rdr["detail_sum"]);                        
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);                        
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
        public BindingCollection<modSalesShipment> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipment> modellist = new BindingCollection<modSalesShipment>();
                //Execute a query to read the categories
                string shipdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and a.ship_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and a.ship_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,account_no,receive_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_shipment a where a.status=1 and a.ship_type<>'样品单' and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + shipdatewhere + "order by ship_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipment model = new modSalesShipment();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.ShipNo = dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo = dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["ship_addr"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
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
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.MotorMan = dalUtility.ConvertToString(rdr["motor_man"]);
                        model.MakeMan = dalUtility.ConvertToString(rdr["make_man"]);
                        model.MakeDate = dalUtility.ConvertToDateTime(rdr["make_date"]);
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
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of salesshipment</returns>
        public modSalesShipment GetItem(string shipid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,account_no,receive_date,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from sales_shipment where ship_id='{0}' order by ship_id", shipid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modSalesShipment model = new modSalesShipment();
                        model.ShipId=dalUtility.ConvertToString(rdr["ship_id"]);
                        model.ShipType=dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate=dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.ShipNo=dalUtility.ConvertToString(rdr["no"]);
                        model.CustOrderNo=dalUtility.ConvertToString(rdr["cust_order_no"]);
                        model.AdFlag=dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Tel=dalUtility.ConvertToString(rdr["tel"]);
                        model.ShipAddr=dalUtility.ConvertToString(rdr["ship_addr"]);
                        model.PayMethod=dalUtility.ConvertToString(rdr["pay_method"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
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
                        model.SalesMan=dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipMan=dalUtility.ConvertToString(rdr["ship_man"]);
                        model.MotorMan=dalUtility.ConvertToString(rdr["motor_man"]);
                        model.MakeMan=dalUtility.ConvertToString(rdr["make_man"]);
                        model.MakeDate=dalUtility.ConvertToDateTime(rdr["make_date"]);
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
        /// get all salesshipmentdetail
        /// <summary>
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipmentdetail</returns>
        public BindingCollection<modSalesShipmentDetail> GetDetail(string shipid, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipmentDetail> modellist = new BindingCollection<modSalesShipmentDetail>();
                //Execute a query to read the categories
                string sql = string.Format("select ship_id,seq,product_id,product_name,specify,size,unit_no,qty,price,sales_mny,currency,exchange_rate,warehouse_id,remark from sales_shipment_detail where ship_id='{0}' order by ship_id,seq", shipid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipmentDetail model = new modSalesShipmentDetail();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
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
        /// get all salesshipment
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipment</returns>
        public BindingCollection<modSalesShipmentSummary> GetSalesShipmentSummary(string statuslist, string shiptypelist, string custlist, string custname, string salesmanlist, string receivestatuslist, string invoicestatuslist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesShipmentSummary> modellist = new BindingCollection<modSalesShipmentSummary>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string shiptypewhere = string.Empty;
                if (!string.IsNullOrEmpty(shiptypelist) && shiptypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and a.ship_type in ('" + shiptypelist.Replace(",", "','") + "') ";

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
                    shipdatewhere = "and a.ship_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and a.ship_date <= '" + Convert.ToDateTime(todate) + "' ";
                string sql = "select cust_id,cust_name,ship_type,ad_flag,currency,sum(detail_sum) detail_sum,sum(kill_mny) kill_mny,sum(other_mny) other_mny "
                        + "from sales_shipment a where ship_type<>'样品单' " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + salesmanwhere + receivestatuswhere + invoicestatuswhere + shipdatewhere
                        + "group by cust_id,cust_name,ship_type,ad_flag,currency order by cust_name,ship_type";

                decimal sumdetail = 0;
                decimal sumkill = 0;
                decimal sumother = 0;
                decimal summny = 0;
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipmentSummary model = new modSalesShipmentSummary();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);                      
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
                modSalesShipmentSummary modsum = new modSalesShipmentSummary();
                modsum.ShipType = "合计";
                modsum.AdFlag = 0;
                modsum.CustId = "合计";
                modsum.CustName = "合计";
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
        /// get all salesshipmentdetail
        /// <summary>
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipmentdetail</returns>
        public BindingCollection<modVSalesShipmentDetail> GetVDetail(string statuslist, string shiptypelist, string custlist, string custname, string invno, string custorderno, string salesmanlist, string receivestatuslist, string invoicestatuslist, string currencylist, string productidlist, string productname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modVSalesShipmentDetail> modellist = new BindingCollection<modVSalesShipmentDetail>();
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and b.status in ('" + statuslist.Replace(",", "','") + "') ";

                string shiptypewhere = string.Empty;
                if (!string.IsNullOrEmpty(shiptypelist) && shiptypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and b.ship_type in ('" + shiptypelist.Replace(",", "','") + "') ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and b.cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and b.cust_name like '%" + custname + "%' ";

                string custordernowhere = string.Empty;
                if (!string.IsNullOrEmpty(custorderno))
                    custordernowhere = "and b.cust_order_no like '%" + custorderno + "%' ";
                
                string invnowhere = string.Empty;
                if (!string.IsNullOrEmpty(invno))
                    invnowhere = "and b.inv_no like '" + invno + "%' ";

                string productidwhere = string.Empty;
                if (!string.IsNullOrEmpty(productidlist) && productidlist.CompareTo("ALL") != 0)
                    productidwhere = "and a.product_id in ('" + productidlist.Replace(",", "','") + "') ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and a.product_name like '%" + productname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and b.sales_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string receivestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(receivestatuslist) && receivestatuslist.CompareTo("ALL") != 0)
                    receivestatuswhere = "and b.receive_status in ('" + receivestatuslist.Replace(",", "','") + "') ";

                string invoicestatuswhere = string.Empty;
                if (!string.IsNullOrEmpty(invoicestatuslist) && invoicestatuslist.CompareTo("ALL") != 0)
                    invoicestatuswhere = "and b.invoice_status in ('" + invoicestatuslist.Replace(",", "','") + "') ";

                string currencywhere = string.Empty;
                if (!string.IsNullOrEmpty(currencylist) && currencylist.CompareTo("ALL") != 0)
                    currencywhere = "and b.currency in ('" + currencylist.Replace(",", "','") + "') ";

                string shipdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and b.ship_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and b.ship_date <= '" + Convert.ToDateTime(todate) + "' ";
                //Execute a query to read the categories
                string sql = "select b.ship_id,b.ship_type,b.ship_date,b.status,b.no,b.cust_order_no,b.ad_flag,b.cust_id,b.cust_name,a.product_id,a.product_name,a.specify,a.size,a.unit_no,a.qty,a.price,a.sales_mny,a.currency,a.exchange_rate,a.warehouse_id,a.remark,a.seq "
                        + "from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id "
                        + "where 1=1 " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + invnowhere + custordernowhere + salesmanwhere + receivestatuswhere + invoicestatuswhere + currencywhere + productidwhere + productnamewhere + shipdatewhere + "order by a.ship_id,a.seq";
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
        /// get all salesshipmentdetail
        /// <summary>
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipmentdetail</returns>
        public BindingCollection<modSalesManMny> GetSalesManMny(string statuslist, string shiptypelist, string custlist, string custname, string salesmanlist, string productname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modSalesManMny> modellist = new BindingCollection<modSalesManMny>();
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and status in ('" + statuslist.Replace(",", "','") + "') ";

                string shiptypewhere = string.Empty;
                if (!string.IsNullOrEmpty(shiptypelist) && shiptypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and b.ship_type in ('" + shiptypelist.Replace(",", "','") + "') ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and cust_name like '%" + custname + "%' ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and product_name like '%" + productname + "%' ";

                string salesmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(salesmanlist) && salesmanlist.CompareTo("ALL") != 0)
                    salesmanwhere = "and sales_man in ('" + salesmanlist.Replace(",", "','") + "') ";

                string shipdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and b.ship_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and b.ship_date <= '" + Convert.ToDateTime(todate) + "' ";
                //Execute a query to read the categories
                string sql = "select b.ship_id,b.ship_type,b.ship_date,b.status,b.ad_flag,b.cust_id,b.cust_name,b.pay_method,a.product_name,a.unit_no,a.qty,a.price,a.sales_mny,a.paid_sales_mny,a.currency,a.remark,a.seq "
                        + "from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id "
                        + "where 1=1 " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + salesmanwhere + productnamewhere + shipdatewhere + "order by a.ship_id,a.seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesManMny model = new modSalesManMny();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["ship_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["ship_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.SumMny = model.Qty * model.Price;
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.PaidSalesMny = dalUtility.ConvertToDecimal(rdr["paid_sales_mny"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        modellist.Add(model);
                    }
                }

                if (!string.IsNullOrEmpty(shiptypelist) && shiptypelist.CompareTo("ALL") != 0)
                    shiptypewhere = "and form_type in ('" + shiptypelist.Replace(",", "','") + "') ";

                if (!string.IsNullOrEmpty(fromdate))
                    shipdatewhere = "and form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    shipdatewhere += "and form_date <= '" + Convert.ToDateTime(todate) + "' ";

                sql = "select id,form_type,form_date,status,ad_flag,cust_id,cust_name,pay_method,product_name,unit_no,qty,mny,sales_mny,paid_sales_mny,currency,remark "
                        + "from sales_design_form where 1=1 " + statuswhere + shiptypewhere + cusidtwhere + custnamewhere + salesmanwhere + productnamewhere + shipdatewhere + "order by id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesManMny model = new modSalesManMny();
                        model.ShipId = dalUtility.ConvertToString(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.ShipType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.ShipDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["pay_method"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);                        
                        model.SumMny = dalUtility.ConvertToDecimal(rdr["mny"]);
                        if (model.Qty > 0)
                            model.Price = decimal.Round(model.SumMny / model.Qty);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.SalesManMny = dalUtility.ConvertToDecimal(rdr["sales_mny"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Seq = 1;
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
        /// get all sales shipment cost
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all salesshipmentdetail</returns>
        public BindingCollection<modSalesShipmentCost> GetSalesShipmentCost(string accname, out string emsg)
        {
            try
            {
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);

                BindingCollection<modSalesShipmentCost> modellist = new BindingCollection<modSalesShipmentCost>();
                //Execute a query to read the categories
                string sql = "select b.ship_id,b.ship_type,b.ship_date,b.status,b.no,b.cust_order_no,b.ad_flag,b.cust_id,b.cust_name,a.seq,a.product_id,a.product_name,a.specify,a.size,a.unit_no,(b.ad_flag * a.qty) qty,a.price,a.warehouse_id,a.remark "
                        + "from sales_shipment_detail a inner join sales_shipment b on a.ship_id=b.ship_id where status=1 and b.ship_date >= '" + modp.StartDate + "' and b.ship_date <= '" + modp.EndDate + "'  order by a.ship_id,a.seq";
                dalAccProductInout dalapi = new dalAccProductInout();
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modSalesShipmentCost model = new modSalesShipmentCost();
                        model.ShipId = dalUtility.ConvertToString(rdr["ship_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);                        
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice = dalapi.GetPrice(accname, model.ProductId, out emsg);
                        model.CostMny = decimal.Round(model.Size * model.Qty * model.CostPrice, 2);
                        model.SalesMny = decimal.Round(model.Qty * dalUtility.ConvertToDecimal(rdr["price"]), 2);
                        model.Profit = model.SalesMny - model.CostMny;
                        model.WarehouseId = dalUtility.ConvertToString(rdr["warehouse_id"]);
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
        /// get new ship id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewNo(DateTime shipdate, string custid)
        {
            string temp = shipdate.ToString("yyyyMM");
            DateTime dt =Convert.ToDateTime(shipdate.ToString("yyyy-MM-01"));
            string sql = string.Format("select no from customer_list where cust_id='{0}'", custid);
            string shipno = SqlHelper.ExecuteScalar(sql).ToString().Trim() + temp + "-";
            try
            {
                sql = "select max(no) from sales_shipment where cust_id = '" + custid + "' and ship_date>='" + dt + "' ";
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
        /// get new ship id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime shipdate)
        {

            string temp = shipdate.ToString("yyyyMM");
            string shipid = "SP" + temp + "-";

            string sql = "select max(ship_id) from sales_shipment where ship_id like '" + shipid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(shipid, "").Trim()) + 1;
                shipid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                shipid += "0001";
            }            
            return shipid;
        }

        /// <summary>
        /// insert a salesshipment
        /// <summary>
        /// <param name=mod>model object of salesshipment</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modSalesShipment mod, BindingCollection<modSalesShipmentDetail> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string shipid = mod.ShipId;
                    string shipno = mod.ShipNo;
                    
                    int? seq = 0;
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            if (Exists(shipid, out emsg))
                                shipid = GetNewId(mod.ShipDate);

                            if (string.IsNullOrEmpty(shipno) || ExistsNo(mod.CustId, mod.ShipNo, out emsg))
                                shipno = GetNewNo(mod.ShipDate, mod.CustId);
                            sql = string.Format("insert into sales_shipment(ship_id,ship_type,ship_date,no,cust_order_no,ad_flag,cust_id,cust_name,tel,ship_addr,pay_method,status,receive_status,invoice_status,invoice_no,invoice_mny,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,sales_man,ship_man,motor_man,make_man,make_date,update_user,update_time,account_no,receive_date)values('{0}','{1}','{2}','{3}','{4}',{5},'{6}','{7}','{8}','{9}','{10}',{11},{12},{13},'{14}',{15},'{16}',{17},{18},{19},{20},'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}',getdate(),'{29}','{30}')", shipid, mod.ShipType, mod.ShipDate, shipno, mod.CustOrderNo, mod.AdFlag, mod.CustId, mod.CustName, mod.Tel, mod.ShipAddr, mod.PayMethod, mod.Status, mod.ReceiveStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.Currency, mod.ExchangeRate, mod.DetailSum, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.SalesMan, mod.ShipMan, mod.MotorMan, mod.MakeMan, mod.MakeDate, mod.UpdateUser, mod.AccountNo, mod.ReceiveDate);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modSalesShipmentDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into sales_shipment_detail(ship_id,seq,product_id,product_name,specify,size,unit_no,qty,price,sales_mny,currency,exchange_rate,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},{9},'{10}',{11},'{12}','{13}')", shipid, seq, modd.ProductId, modd.ProductName, modd.Specify, modd.Size, modd.UnitNo, modd.Qty, modd.Price, modd.SalesManMny, mod.Currency, mod.ExchangeRate, modd.WarehouseId, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);                                
                            }                            
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update sales_shipment set ship_type='{0}',ship_date='{1}',no='{2}',cust_order_no='{3}',ad_flag={4},cust_id='{5}',cust_name='{6}',tel='{7}',ship_addr='{8}',pay_method='{9}',status={10},receive_status={11},invoice_status={12},invoice_no='{13}',invoice_mny={14},currency='{15}',exchange_rate={16},detail_sum={17},kill_mny={18},other_mny={19},other_reason='{20}',remark='{21}',sales_man='{22}',ship_man='{23}',motor_man='{24}',make_man='{25}',make_date='{26}',account_no='{27}',receive_date='{28}' where ship_id='{29}'", mod.ShipType, mod.ShipDate, mod.ShipNo, mod.CustOrderNo, mod.AdFlag, mod.CustId, mod.CustName, mod.Tel, mod.ShipAddr, mod.PayMethod, mod.Status, mod.ReceiveStatus, mod.InvoiceStatus, mod.InvoiceNo, mod.InvoiceMny, mod.Currency, mod.ExchangeRate, mod.DetailSum, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.SalesMan, mod.ShipMan, mod.MotorMan, mod.MakeMan, mod.MakeDate, mod.AccountNo, mod.ReceiveDate, mod.ShipId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete sales_shipment_detail where ship_id='{0}'", shipid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modSalesShipmentDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into sales_shipment_detail(ship_id,seq,product_id,product_name,specify,size,unit_no,qty,price,sales_mny,currency,exchange_rate,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}',{5},'{6}',{7},{8},{9},'{10}',{11},'{12}','{13}')", mod.ShipId, seq, modd.ProductId, modd.ProductName, modd.Specify, modd.Size, modd.UnitNo, modd.Qty, modd.Price, modd.SalesManMny, mod.Currency, mod.ExchangeRate, modd.WarehouseId, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);                                
                            }                            
                            break;
                        case "DEL":
                        case "DELETE":                            
                            sql = string.Format("delete sales_shipment_detail where ship_id='{0}'", mod.ShipId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete sales_shipment where ship_id='{0}'", mod.ShipId);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                    }

                    transaction.Complete();//就这句就可以了。  
                    emsg = shipid;
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
        /// audit sales shipment
        /// <summary>
        /// <param name=shipid>shipid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(string shipid, string updateuser, out string emsg)
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

                modSalesShipment mod = GetItem(shipid, out emsg);
                switch (mod.ShipType)
                {
                    case "送货单":
                    case "收营单":
                        actioncode = "SHIPMENT";
                        break;
                    case "样品单":
                        actioncode = "SAMPLE";
                        break;
                    case "退货单":
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
                BindingCollection<modSalesShipmentDetail> listdetail = GetDetail(shipid, out emsg);
                if (listdetail != null && listdetail.Count>0)
                {
                    foreach (modSalesShipmentDetail modd in listdetail)
                    {
                        sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", modd.WarehouseId, modd.ProductId, modd.Size, shipid, mod.ShipType, mod.ShipNo, mod.ShipDate, 0, 0, mod.AdFlag * modd.Qty, modd.Remark, updateuser);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(content))
                            content = "产品:" + modd.ProductName + "  规格:" + modd.Specify + "  数量:" + modd.Qty.ToString() + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo + "  金额:" + (modd.Qty * modd.Price).ToString() + modd.Currency;
                        else
                            content += "\r\n产品:" + modd.ProductName + "  规格:" + modd.Specify + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo + "  金额:" + (modd.Qty * modd.Price).ToString() + modd.Currency;
                    }
                }
                sql = string.Format("update sales_shipment set status={0},audit_man='{1}',audit_time=getdate() where ship_id='{2}'", 1, updateuser, shipid);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();

                sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}',getdate())", mod.CustId, mod.CustName, actioncode, mod.ShipType, salesman, shipid, string.Empty, content, string.Empty, string.Empty, mod.ShipDate, mod.ShipDate, modcsr.Scores * (mod.DetailSum + mod.OtherMny - mod.KillMny) * mod.ExchangeRate, mod.AdFlag, mod.UpdateUser);
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
        public bool Reset(string shipid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modSalesShipment mod = GetItem(shipid, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                string actioncode = string.Empty;
                switch (mod.ShipType)
                {
                    case "送货单":
                    case "收营单":
                        actioncode = "SHIPMENT";
                        break;
                    case "样品单":
                        actioncode = "SAMPLE";
                        break;
                    case "退货单":
                        actioncode = "WITHDRAWAL";
                        break;
                }
                sql = string.Format("delete customer_log where action_code='{0}' and form_id='{1}'", actioncode, shipid);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("delete warehouse_product_inout where form_id='{0}' and form_type='{1}'", shipid, mod.ShipType);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update sales_shipment set status={0},audit_man='{1}',audit_time=null where ship_id='{2}'", 0, updateuser, shipid);
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
        /// <param name=shipidlist>shipidlist</param>
        /// <param name=receivestatus>receivestatus</param>
        /// <param name=accountno>accountno</param>
        /// <param name=receivedate>receivedate</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateReceiveStatus(bool receiveflag, bool invoiceflag, string shipidlist, int? receivestatus, string accountno, string receivedate, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Empty;
                if(receiveflag && invoiceflag)
                    sql = string.Format("update sales_shipment set receive_status={0}, account_no='{1}', receive_date='{2}', invoice_status={3}, invoice_mny={4}, invoice_no='{5}', update_user='{6}' where ship_id in ('" + shipidlist.Replace(",", "','") + "')", receivestatus == null ? 0 : 1, accountno, receivedate, invoicestatus, invoicemny, invoiceno, updateuser);
                else if(receiveflag)
                    sql = string.Format("update sales_shipment set receive_status={0}, account_no='{1}', receive_date='{2}', update_user='{3}' where ship_id in ('" + shipidlist.Replace(",", "','") + "')", receivestatus == null ? 0 : 1, accountno, receivedate, updateuser);
                else if (invoiceflag)
                    sql = string.Format("update sales_shipment set invoice_status={0}, invoice_mny={1}, invoice_no='{2}', update_user='{3}' where ship_id in ('" + shipidlist.Replace(",", "','") + "')", invoicestatus, invoicemny, invoiceno, updateuser);
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
        /// <param name=shipid>shipid</param>
        /// <param name=receivestatus>receivestatus</param>
        /// <param name=receivedate>receivedate</param>
        /// <param name=accountno>accountno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateReceiveStatus(string shipid, int? receivestatus, string receivedate, string accountno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update sales_shipment set receive_status={0},receive_date='{1}',account_no='{2}',update_user='{3}' "
                    + "where ship_id ='{4}'", receivestatus == null ? 0 : 1, receivedate, accountno, updateuser, shipid);
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
        /// <param name=shipid>shipid</param>
        /// <param name=invoicestatus>invoicestatus</param>
        /// <param name=invoicemny>invoicemny</param>
        /// <param name=invoiceno>invoiceno</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateInvoiceStatus(string shipid, int invoicestatus, decimal invoicemny, string invoiceno, string updateuser, out string emsg)
        {
            try
            {
                string sql = sql = string.Format("update sales_shipment set invoice_status={0},invoice_no='{1}',invoice_mny={2},update_user='{3}' "
                            + "where ship_id ='{4}'", invoicestatus, invoiceno, invoicemny, updateuser, shipid);
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
        /// <param name=shipid>shipid</param>
        /// <param name=seq>seq</param>
        /// <param name=salesmanmny>salesmanmny</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateSalesManMny(string shipid, int seq, decimal salesmny, decimal paidsalesmny, string updateuser, out string emsg)
        {
            try
            {
                string sql = string.Format("update sales_shipment_detail set sales_mny={0},paid_sales_mny={1} where ship_id ='{2}' and seq={3}", salesmny, paidsalesmny, shipid, seq);
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
        /// <param name=shipid>shipid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string shipid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from sales_shipment where ship_id='{0}' ",shipid);
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
                string sql = string.Format("select count(1) from purchase_sales_form where cust_id='{0}' and sales_no='{1}' ", custid, salesno);
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    sql = string.Format("select count(1) from sales_shipment where cust_id='{0}' and no='{1}' ", custid, salesno);
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
