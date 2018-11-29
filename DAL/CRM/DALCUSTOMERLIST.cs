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
    public class dalCustomerList
    {        
        /// <summary>
        /// get all customerlist
        /// <summary>
        /// <param name=validonly>validonly</param>
        /// <param name=custname>custname</param>
        /// <param name=chkAccess>chkAccess</param>
        /// <param name=userid>userid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlist</returns>
        public BindingCollection<modCustomerList> GetIList(string option, string custname, bool chkAccess, string userid, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerList> modellist = new BindingCollection<modCustomerList>();
                //Execute a query to read the categories                
                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and a.cust_name like '%" + custname +"%' ";

                string custaccesswhere = string.Empty;
                if (chkAccess)
                {
                    dalSysUserPrivilege bllpri = new dalSysUserPrivilege();
                    modSysUserPrivilege modpri = bllpri.GetItem(userid, "CUST_ACCESS_OPTION", out emsg);
                    if (modpri == null)
                    {
                        custaccesswhere = "and 1=1 ";
                    }
                    else
                    {
                        switch (modpri.PrivilegeValue)
                        {
                            case "0":
                                custaccesswhere = "and 1=2 ";
                                break;
                            case "1":
                                custaccesswhere = "and a.sales_man='" + userid + "' ";
                                break;
                        }
                    }
                }
                string sql = "select a.cust_id,a.cust_name,a.full_name,a.status,a.currency,a.cust_level,a.cust_type,a.no,a.linkman,a.tel,a.fax,a.addr,a.email,a.qq,a.remark,a.shipment_addr,a.product_type,a.incorporator,a.company_size,a.need_invoice,"
                        + "a.payment_method,a.check_account_date,a.account_bank,a.account_no,a.sales_man,a.shipment_templete,a.update_user,a.update_time "
                        + "from customer_list a where 1=1 " + custnamewhere + custaccesswhere;
                switch (option)
                {
                    case "SHIPMENT30":  //30天内有送货的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SHIPMENT' and update_time>=getdate()-30)";
                        break;
                    case "SHIPMENT60":  //60天内有送货的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SHIPMENT' and update_time>=getdate()-60)";
                        break;
                    case "SAMPLE15":     //15天内有取样的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SAMPLE' and update_time>=getdate()-15)";
                        break;
                    case "QUOTATION15":  //15天内有报价的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='QUOTATION' and update_time>=getdate()-15)";
                        break;
                    case "NEWCUST07":   //7天内新增的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='NEWCUST' and update_time>=getdate()-7)";
                        break;
                    case "NEWCUST15":   //15天内新增的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='NEWCUST' and update_time>=getdate()-15)";
                        break;
                    case "LOST30":      //30天未联系的客户
                        sql += "and not exists (select '#' from customer_log where cust_id=a.cust_id and update_time>=getdate()-30)";
                        break;
                    case "LOST90":      //90天未联系的客户
                        sql += "and not exists (select '#' from customer_log where cust_id=a.cust_id and update_time>=getdate()-90)";
                        break;
                    default:
                        break;
                }
                sql += "order by a.cust_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerList model = new modCustomerList();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.FullName = dalUtility.ConvertToString(rdr["full_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.CustLevel = dalUtility.ConvertToString(rdr["cust_level"]);
                        model.CustType = dalUtility.ConvertToString(rdr["cust_type"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.EMail = dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["shipment_addr"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Incorporator = dalUtility.ConvertToString(rdr["incorporator"]);
                        model.CompanySize = dalUtility.ConvertToString(rdr["company_size"]);                        
                        model.PayMethod = dalUtility.ConvertToString(rdr["payment_method"]);                        
                        model.CheckAccountDate = dalUtility.ConvertToString(rdr["check_account_date"]);
                        model.AccountBank = dalUtility.ConvertToString(rdr["account_bank"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipmentTemplete = dalUtility.ConvertToString(rdr["shipment_templete"]);
                        model.NeedInvoice = dalUtility.ConvertToInt(rdr["need_invoice"]);
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
        /// get all customerlist
        /// <summary>
        /// <param name=option>option</param>
        /// <param name=chkAccess>chkAccess</param>
        /// <param name=userid>userid</param>
        /// <param name=custname>custname</param>
        /// <param name=custtype>custtype</param>
        /// <param name=linkman>linkman</param>
        /// <param name=tel>tel</param>
        /// <param name=fax>fax</param>
        /// <param name=addr>addr</param>
        /// <param name=email>email</param>
        /// <param name=qq>qq</param>
        /// <param name=currentPage>currentPage</param>
        /// <param name=pagesize>pagesize</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlist</returns>
        public BindingCollection<modCustomerList> GetIList(int currentPage, int pagesize, string option, bool chkAccess, string userid, string custname, string custtype, string linkman, string tel, string fax, string addr, string email, string qq, out string emsg)
        {
            try
            {
                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and a.cust_name like '%" + custname + "%' ";

                string custtypewhere = string.Empty;
                if (!string.IsNullOrEmpty(custtype) && custtype!="ALL")
                    custtypewhere = "and a.cust_type like '%" + custtype + "%' ";

                string linkmanwhere = string.Empty;
                if (!string.IsNullOrEmpty(linkman))
                    linkmanwhere = "and a.link_man like '%" + linkman + "%' ";

                string telwhere = string.Empty;
                if (!string.IsNullOrEmpty(tel))
                    telwhere = "and a.tel like '%" + tel + "%' ";

                string faxwhere = string.Empty;
                if (!string.IsNullOrEmpty(fax))
                    faxwhere = "and a.fax like '%" + fax + "%' ";

                string addrwhere = string.Empty;
                if (!string.IsNullOrEmpty(addr))
                    addrwhere = "and a.addr like '%" + addr + "%' ";

                string emailwhere = string.Empty;
                if (!string.IsNullOrEmpty(email))
                    emailwhere = "and a.email like '%" + email + "%' ";

                string qqwhere = string.Empty;
                if (!string.IsNullOrEmpty(qq))
                    qqwhere = "and a.qq like '%" + qq + "%' ";
                string custaccesswhere = string.Empty;
                if (chkAccess)
                {
                    dalSysUserPrivilege bllpri = new dalSysUserPrivilege();
                    modSysUserPrivilege modpri = bllpri.GetItem(userid, "CUST_ACCESS_OPTION", out emsg);
                    if (modpri == null)
                    {
                        custaccesswhere = "and 1=1 ";
                    }
                    else
                    {
                        switch (modpri.PrivilegeValue)
                        {
                            case "0":
                                custaccesswhere = "and 1=2 ";
                                break;
                            case "1":
                                custaccesswhere = "and a.sales_man='" + userid + "' ";
                                break;
                        }
                    }
                }

                int startindex = (currentPage - 1) * pagesize + 1;
                int endindex = currentPage * pagesize;
                BindingCollection<modCustomerList> modellist = new BindingCollection<modCustomerList>();
                //Execute a query to read the categories
                string sql = "select row_number() over(order by cust_id) as rn,cust_id,cust_name,status,cust_level,cust_type,no,linkman,tel,fax,addr,remark,update_user,update_time,shipment_addr,product_type,incorporator,company_size,purchase_man,payment_method,shipment_cost_method,model_cost_method,credit_standarding,check_account_date,account_bank,account_no,sales_man,kickback,full_name,start_mny,start_time,shipment_templete,need_invoice,currency,email,qq "
                        + "from customer_list a where 1=1 " + custnamewhere + custtypewhere + linkmanwhere + telwhere + faxwhere + addrwhere + emailwhere + qqwhere + custaccesswhere;
                switch (option)
                {
                    case "SHIPMENT30":  //30天内有送货的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SHIPMENT' and update_time>=getdate()-30)";
                        break;
                    case "SHIPMENT60":  //60天内有送货的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SHIPMENT' and update_time>=getdate()-60)";
                        break;
                    case "SAMPLE15":     //15天内有取样的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='SAMPLE' and update_time>=getdate()-15)";
                        break;
                    case "QUOTATION15":  //15天内有报价的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='QUOTATION' and update_time>=getdate()-15)";
                        break;
                    case "NEWCUST07":   //7天内新增的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='NEWCUST' and update_time>=getdate()-7)";
                        break;
                    case "NEWCUST15":   //15天内新增的客户
                        sql += "and cust_id in (select cust_id from customer_log where action_code='NEWCUST' and update_time>=getdate()-15)";
                        break;
                    case "LOST30":      //30天未联系的客户
                        sql += "and not exists (select '#' from customer_log where cust_id=a.cust_id and update_time>=getdate()-30)";
                        break;
                    case "LOST90":      //90天未联系的客户
                        sql += "and not exists (select '#' from customer_log where cust_id=a.cust_id and update_time>=getdate()-90)";
                        break;
                    default:
                        break;
                }
                //sql += "order by a.cust_id";
                string sql2 = "select count(1) from (" + sql + ") t";
                sql = string.Format("select * from ("+ sql +") t where rn>='{0}' and rn<='{1}'", startindex, endindex);
                
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerList model = new modCustomerList();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.FullName = dalUtility.ConvertToString(rdr["full_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.CustLevel = dalUtility.ConvertToString(rdr["cust_level"]);
                        model.CustType = dalUtility.ConvertToString(rdr["cust_type"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.EMail = dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["shipment_addr"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Incorporator = dalUtility.ConvertToString(rdr["incorporator"]);
                        model.CompanySize = dalUtility.ConvertToString(rdr["company_size"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["payment_method"]);
                        model.CheckAccountDate = dalUtility.ConvertToString(rdr["check_account_date"]);
                        model.AccountBank = dalUtility.ConvertToString(rdr["account_bank"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipmentTemplete = dalUtility.ConvertToString(rdr["shipment_templete"]);
                        model.NeedInvoice = dalUtility.ConvertToInt(rdr["need_invoice"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        modellist.Add(model);
                    }
                }
                emsg = SqlHelper.ExecuteScalar(sql2).ToString();
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get customerlist by sales man
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlist</returns>
        public BindingCollection<modCustomerList> GetIList(string salesman, out string emsg)
        {
            try
            {
                BindingCollection<modCustomerList> modellist = new BindingCollection<modCustomerList>();
                //Execute a query to read the categories                
                
                string sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.status,a.currency,a.cust_level,a.cust_type,a.no,a.linkman,a.tel,a.fax,a.addr,a.email,a.qq,a.remark,a.shipment_addr,a.product_type,a.incorporator,a.company_size,a.need_invoice,"
                        + "a.payment_method,a.check_account_date,a.account_bank,a.account_no,a.sales_man,a.shipment_templete,a.update_user,a.update_time "
                        + "from customer_list a where sales_man='{0}' order by a.cust_id", salesman);

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerList model = new modCustomerList();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.FullName = dalUtility.ConvertToString(rdr["full_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.CustLevel = dalUtility.ConvertToString(rdr["cust_level"]);
                        model.CustType = dalUtility.ConvertToString(rdr["cust_type"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.EMail = dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["shipment_addr"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Incorporator = dalUtility.ConvertToString(rdr["incorporator"]);
                        model.CompanySize = dalUtility.ConvertToString(rdr["company_size"]);
                        model.PayMethod = dalUtility.ConvertToString(rdr["payment_method"]);
                        model.CheckAccountDate = dalUtility.ConvertToString(rdr["check_account_date"]);
                        model.AccountBank = dalUtility.ConvertToString(rdr["account_bank"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipmentTemplete = dalUtility.ConvertToString(rdr["shipment_templete"]);
                        model.NeedInvoice = dalUtility.ConvertToInt(rdr["need_invoice"]);
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
        /// get customerlist by sales man
        /// <summary>
        /// <param name=salesman>salesman</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all customerlist</returns>
        public BindingCollection<modCustomerSimpleList> GetSimpleList(out string emsg)
        {
            try
            {
                BindingCollection<modCustomerSimpleList> modellist = new BindingCollection<modCustomerSimpleList>();
                //Execute a query to read the categories                

                string sql = "select a.cust_id,a.cust_name from customer_list a where status=1 order by a.cust_name";

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modCustomerSimpleList model = new modCustomerSimpleList();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);                        
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
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of customerlist</returns>
        public modCustomerList GetItem(string custid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.status,a.currency,a.cust_level,a.cust_type,a.no,a.linkman,a.tel,a.fax,a.addr,a.email,a.qq,a.remark,a.shipment_addr,a.product_type,a.incorporator,a.company_size,a.need_invoice,"
                        + "a.payment_method,a.check_account_date,a.account_bank,a.account_no,a.sales_man,a.shipment_templete,a.update_user,a.update_time "
                        + "from customer_list a where a.cust_id='{0}' order by a.cust_id", custid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modCustomerList model = new modCustomerList();
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.FullName = dalUtility.ConvertToString(rdr["full_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.CustLevel = dalUtility.ConvertToString(rdr["cust_level"]);                        
                        model.CustType = dalUtility.ConvertToString(rdr["cust_type"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.EMail = dalUtility.ConvertToString(rdr["email"]);
                        model.QQ = dalUtility.ConvertToString(rdr["qq"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ShipAddr = dalUtility.ConvertToString(rdr["shipment_addr"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Incorporator = dalUtility.ConvertToString(rdr["incorporator"]);
                        model.CompanySize = dalUtility.ConvertToString(rdr["company_size"]);                        
                        model.PayMethod = dalUtility.ConvertToString(rdr["payment_method"]);                        
                        model.CheckAccountDate = dalUtility.ConvertToString(rdr["check_account_date"]);
                        model.AccountBank = dalUtility.ConvertToString(rdr["account_bank"]);
                        model.AccountNo = dalUtility.ConvertToString(rdr["account_no"]);
                        model.SalesMan = dalUtility.ConvertToString(rdr["sales_man"]);
                        model.ShipmentTemplete = dalUtility.ConvertToString(rdr["shipment_templete"]);
                        model.NeedInvoice = dalUtility.ConvertToInt(rdr["need_invoice"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
                        emsg = string.Empty;
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
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of customerlist</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from customer_list";
                emsg = null;
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// get new item no
        /// <summary>
        /// <returns>string</returns>
        public string GetCustId()
        {
            string custid = "CS";
            string sql = "select max(cust_id) from customer_list where cust_id like '" + custid + "%' and len(cust_id)=9 ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Trim().Substring(7)) + 1;
                custid += no.ToString().PadLeft(7, '0');
            }
            else
            {
                custid += "0000001";
            }
            return custid;
        }
        /// <summary>
        /// get new item no
        /// <summary>
        /// <returns>string</returns>
        public string GetCustIdByName(string CustName)
        {
            string sql = "select cust_id from customer_list where cust_name = '" + CustName + "' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            return ret.ToString();
        }
        public string GetSalesMan(string custid)
        {            
            string sql = string.Format("select isnull(employee_name,'') from admin_employee_list a inner join customer_list b on a.employee_id=b.sales_man where b.cust_id='{0}'", custid);
            string salesman = SqlHelper.ExecuteScalar(sql).ToString();
            return salesman;
        }

        /// <summary>
        /// insert a customerlist
        /// <summary>
        /// <param name=mod>model object of customerlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modCustomerList mod,out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string custid = mod.CustId;
                    if (custid=="0" || string.IsNullOrEmpty(custid) || Exists(custid, out emsg))
                        custid = GetCustId();
                    string content = string.Empty;
                    string salesman = string.Empty;
                    string actioncode = "NEWCUST";

                    dalAdminEmployeeList dalemp = new dalAdminEmployeeList();
                    modAdminEmployeeList modemp = dalemp.GetItem(mod.SalesMan, out emsg);
                    if (modemp != null)
                        salesman = modemp.EmployeeName;

                    dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                    modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);

                    string sql = string.Format("insert into customer_list(cust_id,cust_name,status,cust_level,cust_type,linkman,tel,fax,addr,remark,shipment_addr,product_type,incorporator,company_size,purchase_man,payment_method,shipment_cost_method,model_cost_method,kickback,credit_standarding,check_account_date,account_bank,account_no,sales_man,update_user,update_time,start_time,full_name,shipment_templete,need_invoice,no,currency,email,qq"
                            + ")values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},'{19}','{20}','{21}','{22}','{23}','{24}',getdate(),getdate(),'{25}','{26}',{27},'{28}','{29}','{30}','{31}')",
                            custid, mod.CustName, mod.Status, mod.CustLevel, mod.CustType, mod.Linkman, mod.Tel, mod.Fax, mod.Addr, mod.Remark, mod.ShipAddr, mod.ProductType, mod.Incorporator, mod.CompanySize, "", mod.PayMethod, "", "", 0, "", mod.CheckAccountDate, mod.AccountBank, mod.AccountNo, mod.SalesMan, mod.UpdateUser, mod.FullName, mod.ShipmentTemplete, mod.NeedInvoice, mod.No, mod.Currency, mod.EMail, mod.QQ);
                    SqlHelper.ExecuteNonQuery(sql);

                    decimal scorecount = GetScore(mod, out content);
                    sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', getdate(), getdate(),{10},{11},'{12}',getdate())", mod.CustId, mod.CustName, actioncode, "添加客户", salesman, custid, string.Empty, content, string.Empty, string.Empty, modcsr.Scores * scorecount, 1, mod.UpdateUser);
                    SqlHelper.ExecuteNonQuery(sql);

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

        public decimal GetScore(modCustomerList mod, out string content)
        {
            decimal scorecount = 1;
            content = "客户名称：" + mod.CustName;
            if (!string.IsNullOrEmpty(mod.Linkman))
            {
                scorecount++;
                content += "\r\n联系人：" + mod.Linkman;
            }
            if (!string.IsNullOrEmpty(mod.Tel))
            {
                scorecount++;
                content += "\r\n电话：" + mod.Tel;
            }
            if (!string.IsNullOrEmpty(mod.Fax))
            {
                scorecount++;
                content += "\r\n传真：" + mod.Fax;
            }
            if (!string.IsNullOrEmpty(mod.Addr))
            {
                scorecount++;
                content += "\r\n地址：" + mod.Addr;
            }
            else if (!string.IsNullOrEmpty(mod.ShipAddr))
            {
                scorecount++;
                content += "\r\n地址：" + mod.ShipAddr;
            }
            if (!string.IsNullOrEmpty(mod.EMail))
            {
                scorecount++;
                content += "\r\nEMail：" + mod.EMail;
            }
            if (!string.IsNullOrEmpty(mod.QQ))
            {
                scorecount++;
                content += "\r\nQQ：" + mod.QQ;
            }
            return scorecount;
        }

        /// <summary>
        /// update a customerlist
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=mod>model object of customerlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string custid,modCustomerList mod,out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string content = string.Empty;
                    string salesman = string.Empty;
                    string actioncode = "NEWCUST";

                    dalAdminEmployeeList dalemp = new dalAdminEmployeeList();
                    modAdminEmployeeList modemp = dalemp.GetItem(mod.SalesMan, out emsg);
                    if (modemp != null)
                        salesman = modemp.EmployeeName;

                    dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                    modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);

                    string sql = string.Format("update customer_list set cust_name='{0}',status={1},cust_level='{2}',cust_type='{3}',linkman='{4}',tel='{5}',fax='{6}',addr='{7}',remark='{8}',shipment_addr='{9}',product_type='{10}',incorporator='{11}',company_size='{12}',purchase_man='{13}',payment_method='{14}',shipment_cost_method='{15}',model_cost_method='{16}',kickback={17},credit_standarding='{18}',check_account_date='{19}',account_bank='{20}',account_no='{21}',sales_man='{22}',update_user='{23}',update_time=getdate(),full_name='{24}',shipment_templete='{25}',need_invoice={26},no='{27}',email='{28}',qq='{29}' where cust_id='{30}'",
                            mod.CustName, mod.Status, mod.CustLevel, mod.CustType, mod.Linkman, mod.Tel, mod.Fax, mod.Addr, mod.Remark, mod.ShipAddr, mod.ProductType, mod.Incorporator, mod.CompanySize, "", mod.PayMethod, "", "", 0, "", mod.CheckAccountDate, mod.AccountBank, mod.AccountNo, mod.SalesMan, mod.UpdateUser, mod.FullName, mod.ShipmentTemplete, mod.NeedInvoice, mod.No, mod.EMail, mod.QQ, custid);
                    SqlHelper.ExecuteNonQuery(sql);
                    decimal scorecount = GetScore(mod, out content);
                    sql = string.Format("update customer_log set cust_id='{0}',cust_name='{1}',action_type='{2}',action_man='{3}',action_subject='{4}',action_content='{5}',object_name='{6}',venue='{7}',scores={8},ad_flag={9},update_user='{10}',update_time=getdate() where action_code='{11}' and form_id='{12}' ", mod.CustId, mod.CustName, "修改客户资料", salesman, string.Empty, content, string.Empty, string.Empty, modcsr.Scores * scorecount, 1, mod.UpdateUser, actioncode, custid);
                    SqlHelper.ExecuteNonQuery(sql);

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
        /// delete a customerlist
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(modCustomerList mod,out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string content = string.Empty;
                    string salesman = string.Empty;
                    string actioncode = "DELCUST";

                    dalAdminEmployeeList dalemp = new dalAdminEmployeeList();
                    modAdminEmployeeList modemp = dalemp.GetItem(mod.SalesMan, out emsg);
                    if (modemp != null)
                        salesman = modemp.EmployeeName;

                    dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                    modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);

                    decimal scorecount = GetScore(mod, out content);
                    string sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}',getdate())", mod.CustId, mod.CustName, actioncode, "删除客户资料", salesman, mod.CustId, string.Empty, content, string.Empty, string.Empty, string.Empty, string.Empty, modcsr.Scores * scorecount, -1, mod.UpdateUser);
                    SqlHelper.ExecuteNonQuery(sql);

                    sql = string.Format("delete customer_list where cust_id='{0}'", mod.CustId);
                    SqlHelper.ExecuteNonQuery(sql);
                    
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
        /// update sales man of customer
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=mod>model object of customerlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdateSalesMan(string custidlist, string newsalesman, out string emsg)
        {            
            try
            {
                    
                string sql = string.Format("update customer_list set sales_man='{0}' where cust_id in ('" + custidlist.Replace(",","','") +"')", newsalesman);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i > 0)
                {
                    emsg = string.Empty;
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
        /// change customerlist's status(valid/invalid)
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string custid,out string emsg)
        {
            try
            {
                string sql = string.Format("update customer_list set status=1-status where cust_id='{0}'",custid);
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
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string custid, out string emsg)
        {
            try
            {
                emsg = string.Empty;
                string sql = string.Format("select count(1) from customer_list where cust_id='{0}'",custid);
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

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=checktype>checktype</param>
        /// <param name=value>value</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string checktype, string value, out string emsg)
        {
            try
            {
                int i = 0;
                string sql = string.Empty;
                emsg = string.Empty;
                switch (checktype)
                {
                    case "custname":
                        sql = string.Format("select count(1) from customer_list where cust_name='{0}'", value);
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (i > 0)
                        {
                            emsg = "客户名称已存在!";
                            return true;
                        }
                        break;
                    case "tel":
                        sql = string.Format("select count(1) from customer_list where tel='{0}'", value);
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (i > 0)
                        {
                            sql = string.Format("select top 1 cust_name from customer_list where tel='{0}'", value);
                            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                            {
                                if (rdr.Read())
                                {
                                    emsg += "检测到客户[" + rdr["cust_name"].ToString() +"]的电话与此相同";                                    
                                    return true;
                                }
                            }
                            return true;
                        }
                        break;                    
                    case "fax":
                        sql = string.Format("select count(1) from customer_list where fax='{0}'", value);
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (i > 0)
                        {
                            sql = string.Format("select top 1 cust_name from customer_list where fax='{0}'", value);
                            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                            {
                                if (rdr.Read())
                                {
                                    emsg += "检测到客户[" + rdr["cust_name"].ToString() + "]的传真与此相同";
                                    return true;
                                }
                            }
                            return true;
                        }
                        break;
                    case "email":
                        sql = string.Format("select count(1) from customer_list where email='{0}'", value);
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (i > 0)
                        {
                            sql = string.Format("select top 1 cust_name from customer_list where email='{0}'", value);
                            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                            {
                                if (rdr.Read())
                                {
                                    emsg += "检测到客户[" + rdr["cust_name"].ToString() + "]的Email与此相同";
                                    return true;
                                }
                            }
                            return true;
                        }
                        break;
                    case "qq":
                        sql = string.Format("select count(1) from customer_list where qq='{0}'", value);
                        i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (i > 0)
                        {
                            sql = string.Format("select top 1 cust_name from customer_list where qq='{0}'", value);
                            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                            {
                                if (rdr.Read())
                                {
                                    emsg += "检测到客户[" + rdr["cust_name"].ToString() + "]的QQ与此相同";
                                    return true;
                                }
                            }
                            return true;
                        }
                        break;
                }                
                return false;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return true;
            }
        }

        /// <summary>
        /// record exist or not
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(modCustomerList mod, out string emsg)
        {
            try
            {
                emsg = string.Empty;
                string sql = string.Format("select count(1) from customer_list where full_name='{0}'", mod.FullName);
                int i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    emsg = "客户全名重复\r\n";
                    sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                        + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.full_name='{0}'", mod.FullName);
                    using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                    {
                        if (rdr.Read())
                        {
                            emsg += "\r\n客户信息如下:";
                            emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                            emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                            emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                            emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                            emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                            emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                            emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                            emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                            emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                            return true;
                        }
                    }
                }
                sql = string.Format("select count(1) from customer_list where cust_name='{0}'", mod.CustName);
                i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (i > 0)
                {
                    emsg = "客户简称重复\r\n";
                    sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                        + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.cust_name='{0}'", mod.CustName);
                    using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                    {
                        if (rdr.Read())
                        {
                            emsg += "\r\n客户信息如下:";
                            emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                            emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                            emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                            emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                            emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                            emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                            emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                            emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                            emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                            return true;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mod.Tel))
                {
                    sql = string.Format("select count(1) from customer_list where tel='{0}'", mod.Tel);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (i > 0)
                    {
                        emsg = "客户电话重复\r\n";
                        sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                            + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.tel='{0}'", mod.Tel);
                        using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdr.Read())
                            {
                                emsg += "\r\n客户信息如下:";
                                emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                                emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                                emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                                emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                                emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                                emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                                emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                                emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                                emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                                return true;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mod.Fax))
                {
                    sql = string.Format("select count(1) from customer_list where fax='{0}'", mod.Fax);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (i > 0)
                    {
                        emsg = "客户传真重复\r\n";
                        sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                            + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.fax='{0}'", mod.Fax);
                        using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdr.Read())
                            {
                                emsg += "\r\n客户信息如下:";
                                emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                                emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                                emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                                emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                                emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                                emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                                emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                                emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                                emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                                return true;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mod.Addr))
                {
                    sql = string.Format("select count(1) from customer_list where addr='{0}'", mod.Addr);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (i > 0)
                    {
                        emsg = "客户地址重复\r\n";
                        sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                            + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.addr='{0}'", mod.Addr);
                        using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdr.Read())
                            {
                                emsg += "\r\n客户信息如下:";
                                emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                                emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                                emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                                emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                                emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                                emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                                emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                                emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                                emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                                return true;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mod.ShipAddr))
                {
                    sql = string.Format("select count(1) from customer_list where shipment_addr='{0}'", mod.ShipAddr);
                    i = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (i > 0)
                    {
                        emsg = "客户送货地址重复\r\n";
                        sql = string.Format("select a.cust_id,a.cust_name,a.full_name,a.linkman,a.tel,a.fax,a.addr,a.shipment_addr,a.sales_man,b.employee_name "
                            + "from customer_list a left join admin_employee_list b on a.sales_man=b.employee_id where a.shipment_addr='{0}'", mod.ShipAddr);
                        using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                        {
                            if (rdr.Read())
                            {
                                emsg += "\r\n客户信息如下:";
                                emsg += "\r\n客户编号：" + rdr["cust_id"].ToString();
                                emsg += "\r\n客户全称：" + rdr["full_name"].ToString();
                                emsg += "\r\n客户简称：" + rdr["cust_name"].ToString();
                                emsg += "\r\n客户联系人：" + rdr["linkman"].ToString();
                                emsg += "\r\n客户电话：" + rdr["tel"].ToString();
                                emsg += "\r\n客户传真：" + rdr["fax"].ToString();
                                emsg += "\r\n客户地址：" + rdr["addr"].ToString();
                                emsg += "\r\n送货地址：" + rdr["shipment_addr"].ToString();
                                emsg += "\r\n业务员：" + rdr["employee_name"].ToString();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }
    }
}
