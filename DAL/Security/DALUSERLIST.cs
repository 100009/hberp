using System;
using System.Text;
using System.Data;
using System.Transactions;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Transactions;
using LXMS.DBUtility;
using LXMS.Model;
using BindingCollection;

namespace LXMS.DAL
{
    public class dalUserList
    {                
        /// <summary>
        /// get UserList by userid
        /// <summary>
        /// <param name=userid>userid</param>
        ///<returns>get record of UserList</returns>
        ///<returns>Details about all UserList</returns>
        public modUserList GetItem(string userid)
        {            
            string sql = string.Format("select user_id,user_name,status,pwd,role_id,update_user,update_time,email_addr " 
                    + "from sys_user_list where user_id = '{0}'",userid);
            //Execute a query to read the categories
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
            {
                if (rdr.Read())
                {
                    modUserList model = new modUserList();
                    model.UserId = dalUtility.ConvertToString(rdr["user_id"]);
                    model.UserName = dalUtility.ConvertToString(rdr["user_name"]);
                    model.Status = dalUtility.ConvertToInt(rdr["status"]);
                    model.Password = dalUtility.ConvertToString(rdr["pwd"]);
                    model.RoleId = dalUtility.ConvertToString(rdr["role_id"]);
                    model.Email = dalUtility.ConvertToString(rdr["email_addr"]);
                    model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                    model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);                    
                    return model;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// get user list by RoleId
        /// <summary>
        /// <param name=RoleId>RoleId</param>
        /// <param name=emsg>emsg</param>
        ///<returns>Details about all RoleList</returns>
        public BindingCollection<modUserList> GetIList(string RoleId, out string emsg)
        {
            try
            {
                BindingCollection<modUserList> modellist = new BindingCollection<modUserList>();
                //Execute a query to read the categories
                string sql = string.Format("select user_id,user_name,status,pwd,role_id,update_user,update_time,email_addr "
                    + "from sys_user_list where role_id = '{0}' order by user_id", RoleId);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modUserList model = new modUserList();
                        model.UserId = dalUtility.ConvertToString(rdr["user_id"]);
                        model.UserName = dalUtility.ConvertToString(rdr["user_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Password = dalUtility.ConvertToString(rdr["pwd"]);
                        model.RoleId = dalUtility.ConvertToString(rdr["role_id"]);
                        model.Email = dalUtility.ConvertToString(rdr["email_addr"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);                        
                        modellist.Add(model);
                    }
                }
                emsg = "";
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get user list by RoleId
        /// <summary>
        /// <param name=validonly>validonly</param>
        /// <param name=emsg>emsg</param>
        ///<returns>Details about all RoleList</returns>
        public BindingCollection<modUserList> GetIList(bool validonly, out string emsg)
        {
            try
            {
                BindingCollection<modUserList> modellist = new BindingCollection<modUserList>();
                //Execute a query to read the categories
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select user_id,user_name,status,pwd,role_id,update_user,update_time,email_addr from sys_user_list where 1=1 " + getwhere + " order by user_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modUserList model = new modUserList();
                        model.UserId = dalUtility.ConvertToString(rdr["user_id"]);
                        model.UserName = dalUtility.ConvertToString(rdr["user_name"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Password = dalUtility.ConvertToString(rdr["pwd"]);
                        model.RoleId = dalUtility.ConvertToString(rdr["role_id"]);
                        model.Email = dalUtility.ConvertToString(rdr["email_addr"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);                        
                        modellist.Add(model);
                    }
                }
                emsg = "";
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get user list dataset
        /// <summary>
        /// <param name=validonly>validonly</param>
        /// <param name=emsg>emsg</param>
        ///<returns>dataset</returns>
        public DataSet GetDataSet(bool validonly, out string emsg)
        {
            try
            {
                string getwhere = validonly == true ? "and status=1" : string.Empty;
                string sql = "select user_id,user_name,status,pwd,role_id,update_user,update_time,email_addr from sys_user_list where 1=1 " + getwhere + " order by user_id";
                DataSet ds = SqlHelper.ExecuteDs(sql);
                emsg = "";
                return ds;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
        
        /// <summary>
        /// check login
        /// </summary>
        /// <param name="userid">userid</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool Login(string userid, string password, out string emsg)
        {            
            try
            {
                string sql = string.Empty;
                if (Exists(userid, true, out emsg))
                {
                    sql = string.Format("select count(*) from sys_user_list where user_id='{0}' and pwd='{1}'", userid, password);
                    string ret = Convert.ToString(SqlHelper.ExecuteScalar(sql));
                    if (ret == "1")
                    {
                        emsg = "";
                        modUserList mod=GetItem(userid);                        
                        return true;
                    }
                    else
                    {
                        emsg = "Password is incorrect";
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// check login
        /// </summary>
        /// <param name="userid">userid</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool Login(string userid, string password, string key, out string emsg)
        {
            string sql;
            try
            {
                if (Exists(userid, true, out emsg))
                {
                    string encryptpwd = Encrypt(password, key);
                    sql = string.Format("select count(*) from sys_user_list where user_id='{0}' and pwd='{1}'", userid, encryptpwd);
                    string ret = Convert.ToString(SqlHelper.ExecuteScalar(sql));
                    if (ret == "1")
                    {
                        emsg = "";
                        modUserList mod = GetItem(userid);
                        return true;
                    }
                    else
                    {
                        emsg = "Password is incorrect";
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// get server time
        /// </summary>
        /// <param name="emsg">emsg</param>
        /// <returns>DateTime</returns>
        public DateTime GetServerTime(out string emsg)
        {
            try
            {
                string sql = "select getdate()";
                DateTime dt = Convert.ToDateTime(SqlHelper.ExecuteScalar(sql));
                emsg = string.Empty;
                return dt;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return dalUtility.ConvertToDateTime("");
            }
        }

        /// <summary>
        /// update database object
        /// </summary>
        /// <param name="emsg">emsg</param>
        /// <returns>true/false</returns>
        public bool UpdateDatabaseObject(string app_path, out string emsg)
        {
            try
            {
                string sql = string.Empty;
                string ver = string.Empty;
                int iRet = 0;

                #region ver20140218
                //sql = "Select count(1) from SysColumns where Name='need_invoice' and id=Object_id('customer_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table customer_list add need_invoice int not null default 1";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='currency' and id=Object_id('customer_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table customer_list add currency varchar(50) not null default '人民币'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='brand' and id=Object_id('product_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table product_list add brand varchar(50) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='min_qty' and id=Object_id('product_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table product_list add min_qty decimal(18,1) not null default 0";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='max_qty' and id=Object_id('product_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table product_list add max_qty decimal(18,1) not null default 0";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='purchase_sales_form'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[purchase_sales_form]([form_id] [varchar](50) NOT NULL,[form_type] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[status] [int] NOT NULL default 0,"
                //            + "[ad_flag] [int] NOT NULL default 1,[vendor_name] [varchar](50) NULL,[cust_id] [varchar](50) NOT NULL,[purchase_no] [varchar](50) NULL,[warehouse_detail_sum] [decimal](18, 2) NOT NULL default 0,[purchase_detail_sum] [decimal](18, 2) NOT NULL default 0,[purchase_kill_mny] [decimal](18, 2) NOT NULL default 0,"
                //            + "[purchase_other_mny] [decimal](18, 2) NOT NULL default 0,[purchase_other_reason] [varchar](255) NOT NULL,[purchase_pay_status] [int] NOT NULL default 0,"
                //            + "[purchase_invoice_status] [int] NOT NULL default 0,[purchase_invoice_no] [varchar](50) NULL,[purchase_invoice_mny] [decimal](18, 2) NOT NULL,[purchase_currency] [varchar](50) NOT NULL,[purchase_exchange_rate] [decimal](8, 3) NOT NULL default 1,"
                //            + "[sales_no] [varchar](50) NULL,[sales_detail_sum] [decimal](18, 2) NOT NULL default 0,[sales_kill_mny] [decimal](18, 2) NOT NULL default 0,[sales_other_mny] [decimal](18, 2) NOT NULL default 0,"
                //            + "[sales_other_reason] [varchar](255) NOT NULL,[sales_pay_status] [int] NOT NULL default 0,[sales_invoice_status] [int] NOT NULL default 0,"
                //            + "[sales_invoice_no] [varchar](50) NULL,[sales_invoice_mny] [decimal](18, 2) NOT NULL default 0,"
                //            + "[sales_currency] [varchar](50) NOT NULL,[sales_exchange_rate] [decimal](8, 3) NOT NULL default 1,[remark] [varchar](1024) NOT NULL,[sales_man] [varchar](20) NULL,[purchase_man] [varchar](20) NULL,"
                //            + "[ship_man] [varchar](20) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[audit_man] [varchar](20) NULL,"
                //            + "[audit_time] [datetime] NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] NOT NULL default 0) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[purchase_sales_form] ADD CONSTRAINT [PK_purchase_sales_form] PRIMARY KEY CLUSTERED ([form_id] ASC) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_purchase_sales_form_status ON dbo.purchase_sales_form(status) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_purchase_sales_form_date ON dbo.purchase_sales_form(form_date,status) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_purchase_sales_form_credence ON dbo.purchase_sales_form(acc_name,acc_seq) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='purchase_sales_detail'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[purchase_sales_detail]([form_id] [varchar](50) NOT NULL,[seq] [int] NOT NULL,[product_name] [varchar](150) NOT NULL,[brand] [varchar](50) NULL,size decimal(8,1) not null,[unit_no] varchar(50) not null,"
                //        + "[qty] [decimal](12, 2) NOT NULL,[purchase_price] [decimal](12, 2) NOT NULL,[sales_price] [decimal](12, 2) NOT NULL,[salesman_mny] [decimal](12, 2) NOT NULL default 0,[remark] [varchar](255) NULL) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[purchase_sales_detail] ADD CONSTRAINT [PK_purchase_sales_detail] PRIMARY KEY CLUSTERED ([form_id] ASC,[seq] ASC) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='purchase_sales_detail_wh'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[purchase_sales_detail_wh]([form_id] [varchar](50) NOT NULL,[seq] [int] NOT NULL,[product_id] [varchar](150) NOT NULL,[product_name] [varchar](150) NOT NULL,[specify] [varchar](150) NULL,[brand] [varchar](50) NULL,size decimal(8,1) not null,[unit_no] varchar(50) not null,"
                //        + "[qty] [decimal](12, 2) NOT NULL,[cost_price] [decimal](12, 2) NOT NULL,[sales_price] [decimal](12, 2) NOT NULL,[salesman_mny] [decimal](12, 2) NOT NULL default 0,warehouse_id varchar(50) not null,[remark] [varchar](255) NULL) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[purchase_sales_detail_wh] ADD CONSTRAINT [PK_purchase_sales_detail_wh] PRIMARY KEY CLUSTERED ([form_id] ASC,[seq] ASC) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_parameters where para_id='SHOW_PRODUCT_SPECIFY'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values('SHOW_PRODUCT_SPECIFY','是否显示规格',0,'0-不显示;   1-显示', 'SYSADMIN', getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='OPA_010'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_010','Purchase Sales Form',1,'OPERATION','OPA_PURCHASE_SALES_FORM','OPA_PURCHASE_SALES_FORM','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_010", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //ver = "20140218";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加购销单据;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140309
                //dalAccCredenceWord dalword = new dalAccCredenceWord();
                //if (!dalword.Exists("结", out emsg))
                //{
                //    modAccCredenceWord modword = new modAccCredenceWord();
                //    modword.CredenceWord = "结";
                //    modword.UpdateUser = "SYSADMIN";
                //    dalword.Insert(modword, out emsg);
                //}
                //ver = "20140309";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 修改购销单据;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update customer_list set no=replace(no,'*','')";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update vendor_list set no=replace(no,'*','')";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add vendor_name varchar(50) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_no varchar(50) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_pay_status int default 0 not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_invoice_status int default 0 not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_invoice_no varchar(50) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_invoice_mny decimal(18, 2) default 0 not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_currency varchar(50) default '人民币' not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_sales_detail add purchase_exchange_rate decimal(18, 3) default 1 not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "select form_id,vendor_name,purchase_no,purchase_pay_status,purchase_invoice_status,purchase_invoice_no,purchase_invoice_mny,purchase_currency,purchase_exchange_rate from purchase_sales_form";
                //    using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                //    {
                //        while (rdr.Read())
                //        {
                //            sql = string.Format("update purchase_sales_detail set vendor_name='{0}',purchase_no='{1}',purchase_pay_status={2},purchase_invoice_status={3},purchase_invoice_no='{4}',purchase_invoice_mny={5},purchase_currency='{6}',purchase_exchange_rate={7} where form_id='{8}'",
                //                rdr["vendor_name"].ToString(), rdr["purchase_no"].ToString(), Convert.ToInt32(rdr["purchase_pay_status"]), Convert.ToInt32(rdr["purchase_invoice_status"]), rdr["purchase_invoice_no"].ToString(), Convert.ToDecimal(rdr["purchase_invoice_mny"]), rdr["purchase_currency"].ToString(), Convert.ToDecimal(rdr["purchase_exchange_rate"]), rdr["form_id"].ToString());
                //            SqlHelper.ExecuteNonQuery(sql);                            
                //        }
                //    }
                //    sql = "alter table purchase_sales_detail alter column vendor_name varchar(50) not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "select name FROM sysobjects so JOIN sysconstraints sc ON so.id = sc.constid WHERE object_name(so.parent_obj) = 'purchase_sales_form' "
                //        + "AND so.xtype = 'D' AND sc.colid in (SELECT colid FROM syscolumns WHERE id = object_id('purchase_sales_form') AND name in('vendor_name','purchase_no','purchase_pay_status','purchase_invoice_status','purchase_invoice_no','purchase_invoice_mny','purchase_currency','purchase_exchange_rate','purchase_kill_mny','purchase_other_mny','purchase_other_reason','purchase_man'))";
                //    using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                //    {
                //        while (rdr.Read())
                //        {
                //            sql = "alter table purchase_sales_form drop constraint " + rdr[0].ToString();
                //            SqlHelper.ExecuteNonQuery(sql);
                //        }
                //    }
                //    sql = "alter table purchase_sales_form drop column vendor_name,purchase_no,purchase_pay_status,purchase_invoice_status,purchase_invoice_no,purchase_invoice_mny,purchase_currency,purchase_exchange_rate,purchase_kill_mny,purchase_other_mny,purchase_other_reason,purchase_man";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140322
                //sql = "select count(1) from sys_task_list where task_code='QRY_010'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_010','Sales Shipment Summary',1,'QUERY','QRY_SALES_SHIPMENT_SUMMARY','','QRY_SALES_SHIPMENT_SUMMARY','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_010", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_010", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='QRY_015'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_015','Sales Shipment Detail',1,'ACCOUNT','QRY_SALES_SHIPMENT_DETAIL','','QRY_SALES_SHIPMENT_DETAIL','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_015", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_015", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='QRY_020'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_020','Purchase Summary',1,'QUERY','QRY_PURCHASE_SUMMARY','','QRY_PURCHASE_SUMMARY','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_020", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_020", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='QRY_025'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_025','Purchase Detail',1,'ACCOUNT','QRY_PURCHASE_DETAIL','','QRY_PURCHASE_DETAIL','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_025", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_025", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='ACC_085'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('ACC_085','Account Subject Detail',1,'ACCOUNT','ACC_SUBJECT_DETAIL','','ACC_SUBJECT_DETAIL','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_085", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_085", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='brand' and id=Object_id('purchase_detail')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table purchase_detail add brand varchar(50) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_detail drop column specify";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_parameters where para_id='PURCHASE_IMPORT_PATH'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
                //        + "('PURCHASE_IMPORT_PATH','导入购销合同路径','c:\\','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //ver = "20140322";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加采购收货及客户送货单的导入功能;  \r\n\r\n");
                //    sb.Append("2. 增加客户送货统计及明细查询;  \r\n\r\n");
                //    sb.Append("3. 增加采购收货统计及明细查询;  \r\n\r\n");
                //    sb.Append("4. 增加会计科目明细;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140329
                //sql = "select count(1) from sys_task_list where task_code='OPA_007'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('OPA_007','Sales Man Mny',1,'OPERATION','OPA_SALES_MAN_MNY','','OPA_SALES_MAN_MNY','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_007", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_007", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from SysColumns where Name='account_no' and id=Object_id('sales_shipment')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table sales_shipment add account_no varchar(50)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table sales_shipment add pay_date varchar(50)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from SysColumns where Name='account_no' and id=Object_id('purchase_list')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "alter table purchase_list add account_no varchar(50)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table purchase_list add pay_date varchar(50)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from acc_subject_list where subject_id='913518'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql="insert into acc_subject_list(subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,"
                //        + "is_tradecompany,is_quantity,has_children,check_flag,update_user,update_time)values('913518','加工制造成本','9135','JGZZCB',-1,0,0,0,0,0,'SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //ver = "20140329";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 修改客户送货与采购收货的收付款状况功能及发票状况功能;  \r\n\r\n");
                //    sb.Append("2. 完善利润表查询功能;  \r\n\r\n");
                //    sb.Append("3. 增加费用统计表功能;  \r\n\r\n");
                //    sb.Append("4. 增加对帐单导出功能;  \r\n\r\n");
                //    sb.Append("5. 增加业务提成功能;  \r\n\r\n");
                //    sb.Append("6. 为客户列表增加客户名称的唯一键;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_receivable_list set cust_id='CS010' where cust_id='CS021'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update sales_shipment set cust_id='CS010' where cust_id='CS021'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "delete customer_list where cust_id='CS021'";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_receivable_list set cust_id='CS012' where cust_id='CS027'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update sales_shipment set cust_id='CS012' where cust_id='CS027'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "delete customer_list where cust_id='CS027'";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_receivable_list set cust_id='CS006' where cust_id='CS024'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update sales_shipment set cust_id='CS006' where cust_id='CS024'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "delete customer_list where cust_id='CS024'";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE UNIQUE NONCLUSTERED INDEX uk_customer_list_cust_name ON customer_list(cust_name)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "CREATE UNIQUE NONCLUSTERED INDEX uk_customer_list_full_name ON customer_list(full_name)";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "insert into product_list(product_id,product_name,product_type,brand,size_flag,specify,unit_no,update_user,update_time) select product_id,product_name,'成品','HIWIN',0,specify,unit_no,'SYSADMIN',getdate() from sales_shipment_detail where product_id not in (select product_id from product_list)";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[warehouse_product_inout] WITH CHECK ADD CONSTRAINT [FK_warehouse_product_inout] FOREIGN KEY([product_id]) REFERENCES [dbo].[product_list] ([product_id])";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "insert into product_list(product_id,product_name,product_type,brand,size_flag,unit_no,remark,update_user,update_time) "
                //        + "select product_id,product_id as product_name,'成品','',0,'pcs','',update_user,update_time from warehouse_product_inout "
                //        + "where product_id not in (select product_id from product_list)";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140407
                //sql = "Select count(1) from sysobjects where name='asset_list'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [asset_list]([asset_id] [varchar](50) NOT NULL,[asset_name] [varchar](50) NOT NULL,[asset_property] [varchar](1024) NULL,[status] [smallint] NOT NULL,[sign_date] [date] NOT NULL,[purchase_date] [date] NOT NULL,[control_depart] [varchar](50) NOT NULL,[using_depart] [varchar](50) NOT NULL,"
                //        + "[depre_method] [varchar](50) NOT NULL,[raw_qty] [decimal](18, 0) NOT NULL,[left_qty] [decimal](18, 0) NOT NULL,[raw_mny] [decimal](18, 2) NOT NULL,[last_mny] [decimal](18, 2) NOT NULL,[net_mny] [decimal](18, 2) NOT NULL,[depre_unit] [varchar](50) NOT NULL,[remark] [varchar](1024) NULL,"
                //        + "[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_asset_list] PRIMARY KEY CLUSTERED "
                //        + "([asset_id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='asset_add'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[asset_add]([form_id] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[status] [smallint] default 0 NOT NULL,no varchar(50),[detail_sum] [decimal](18, 2) NOT NULL,[currency] [varchar](50) NOT NULL,[exchange_rate] [decimal](8, 4) default 1 NOT NULL,[remark] [varchar](1024) NULL,"
                //          + "subject_id varchar(50) not null,detail_id varchar(50),detail_name varchar(50),check_no varchar(50),check_type varchar(50),bank_name varchar(50),promise_date varchar(50),[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] NOT NULL,CONSTRAINT [PK_asset_add_list] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='asset_add_detail'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[asset_add_detail]([form_id] [varchar](50) NOT NULL,[asset_name] [varchar](50) NOT NULL,[qty] [int] NOT NULL,[price] [decimal](18, 2) NOT NULL,[remark] [varchar](50) NULL,"
                //            + "CONSTRAINT [PK_asset_form_detail] PRIMARY KEY CLUSTERED ([form_id] ASC,[asset_name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='AST_001'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('AST_001','Asset List',1,'ACCOUNT','ASSET_LIST','','ASSET_LIST','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "AST_001", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "AST_001", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='AST_010'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('AST_010','Asset Add',1,'ACCOUNT','ASSET_ADD','','ASSET_ADD','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "AST_010", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "AST_010", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //ver = "20140407";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加固定资产的相关功能;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_subject_list set subject_name='固定资产原值',ad_flag=0 where subject_id='2115'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update acc_subject_list set ad_flag=0 where subject_id='2125'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update acc_subject_list set ad_flag=1 where subject_id='2135'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "insert into acc_subject_list(subject_id,subject_name,psubject_id,assistant_code,ad_flag,check_currency,is_tradecompany,is_quantity,check_flag,has_children,lock_flag,select_flag,update_user,update_time)values"
                //            + "('2120','固定资产净值','20','GDZCJZ',1,1,0,0,0,0,1,0,'SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140412
                //dalAccCredenceWord dalword = new dalAccCredenceWord();
                //if (!dalword.Exists("折", out emsg))
                //{
                //    modAccCredenceWord modword = new modAccCredenceWord();
                //    modword.CredenceWord = "折";
                //    modword.UpdateUser = "SYSADMIN";
                //    dalword.Insert(modword, out emsg);
                //}
                //sql = "Select count(1) from sysobjects where name='asset_work_qty'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [asset_work_qty]([asset_id] [varchar](50) NOT NULL,[acc_name] [varchar](50) NOT NULL,[work_qty] decimal(12,2) not null,remark varchar(50),[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,"
                //        + "CONSTRAINT [PK_asset_work_qty] PRIMARY KEY CLUSTERED ([asset_id] ASC,[acc_name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[asset_work_qty] WITH CHECK ADD CONSTRAINT [FK_asset_work_qty] FOREIGN KEY([asset_id]) REFERENCES [dbo].[asset_list] ([asset_id])";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='asset_add'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[asset_add]([form_id] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[status] [smallint] default 0 NOT NULL,no varchar(50),asset_name varchar(50) NOT NULL,[qty] [int] NOT NULL,[price] [decimal](18, 2) NOT NULL,[currency] [varchar](50) NOT NULL,[exchange_rate] [decimal](8, 4) default 1 NOT NULL,[remark] [varchar](1024) NULL,"
                //          + "subject_id varchar(50) not null,detail_id varchar(50),detail_name varchar(50),check_no varchar(50),check_type varchar(50),bank_name varchar(50),promise_date date,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] default 0 NOT NULL,CONSTRAINT [PK_asset_add] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='asset_sale'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[asset_sale]([form_id] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[status] [smallint] default 0 NOT NULL,no varchar(50),asset_id varchar(50) NOT NULL,asset_name varchar(50) NOT NULL,[net_mny] [decimal](18, 2) NOT NULL,[sale_mny] [decimal](18, 2) NOT NULL,[currency] [varchar](50) NOT NULL,[exchange_rate] [decimal](8, 4) default 1 NOT NULL,[remark] [varchar](1024) NULL,"
                //          + "subject_id varchar(50) not null,detail_id varchar(50),detail_name varchar(50),check_no varchar(50),check_type varchar(50),bank_name varchar(50),promise_date date,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] default 0 NOT NULL,CONSTRAINT [PK_asset_sale] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='asset_depre_list'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [asset_depre_list]([acc_name] [varchar](50) NOT NULL,status smallint default 0 not null,[asset_id] [varchar](50) NOT NULL,[asset_name] [varchar](50) NOT NULL,[depre_method] [varchar](50) NOT NULL,depre_unit varchar(50) not null,[net_mny] decimal(18,2) NOT NULL,depre_mny decimal(18,2) NOT NULL,depre_qty decimal(12,2) not null,net_qty decimal(12,2) not null,remark varchar(50),[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,"
                //        + "CONSTRAINT [PK_asset_depre_list] PRIMARY KEY CLUSTERED ([acc_name] ASC,[asset_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[asset_depre_list] WITH CHECK ADD CONSTRAINT [FK_asset_depre_list] FOREIGN KEY([asset_id]) REFERENCES [dbo].[asset_list] ([asset_id])";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_parameters where para_id='NEED_ASSET_NO'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
                //        + "('NEED_ASSET_NO','固定资产新增No不可为空','T','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_task_list where task_code='AST_060'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('AST_060','Asset Sale',1,'ACCOUNT','ASSET_SALE','','ASSET_SALE','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "AST_060", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "AST_060", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //ver = "20140412";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 改善产品列表及客户列表等的查找方式;  \r\n\r\n");
                //    sb.Append("2. 增加固定资产的相关功能;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_subject_list set check_currency=0 where subject_id='2120'";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "update acc_subject_list set has_children=0 where subject_id='2115'";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "update acc_subject_list set has_children=0 where subject_id='2125'";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[warehouse_product_inout] WITH CHECK ADD CONSTRAINT [FK_warehouse_product_inout] FOREIGN KEY([product_id]) REFERENCES [dbo].[product_list] ([product_id])";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table sales_shipment_detail alter column size decimal(8,2) not null";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table purchase_detail alter column size decimal(8,2) not null";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table acc_credence_list alter column remark varchar(1024)";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "drop table asset_add_detail";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "drop table asset_add";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table asset_list drop column left_qty";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table asset_list drop column net_mny";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table asset_list add acc_name varchar(50)";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table asset_list add acc_seq int default 0 not null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140413
                //sql = "Select count(1) from sysobjects where name='asset_evaluate'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[asset_evaluate]([form_id] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[status] [smallint] default 0 NOT NULL,no varchar(50),asset_id varchar(50) NOT NULL,asset_name varchar(50) NOT NULL,[net_mny] [decimal](18, 2) NOT NULL,[evaluate_mny] [decimal](18, 2) NOT NULL,[remark] [varchar](1024) NULL,"
                //          + "[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] default 0 NOT NULL,CONSTRAINT [PK_asset_evaluate] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[asset_evaluate] WITH CHECK ADD CONSTRAINT [FK_asset_evaluate] FOREIGN KEY([asset_id]) REFERENCES [dbo].[asset_list] ([asset_id])";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='AST_070'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('AST_070','Asset Evaluate',1,'ACCOUNT','ASSET_EVALUATE','','ASSET_EVALUATE','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "AST_070", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "AST_070", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //ver = "20140413";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加固定资产的评估功能;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "ALTER TABLE [dbo].[asset_sale] WITH CHECK ADD CONSTRAINT [FK_asset_sale] FOREIGN KEY([asset_id]) REFERENCES [dbo].[asset_list] ([asset_id])";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140415
                //ver = "20140415";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加固定资产的评估凭证;  \r\n\r\n");
                //    sb.Append("1. 修改银行明细帐功能;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140418
                //ver = "20140418";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 调整购销合同,增加行数;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "delete product_type_list where product_type=''";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140419
                //sql = "select count(1) from sys_parameters where para_id='NEED_QUOTATION_NO'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
                //        + "('NEED_QUOTATION_NO','客户报价单No不可为空','F','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_parameters where para_id='SHOW_PRODUCT_SIZE'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
                //        + "('SHOW_PRODUCT_SIZE','是否显示产品尺寸','1','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='quotation_form'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[quotation_form]([form_id] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,no varchar(50),cust_id varchar(50) NOT NULL,[remark] [varchar](1024) NULL,currency varchar(50) not null,"
                //          + "[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_quotation_form] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE TABLE [dbo].[quotation_detail]([form_id] [varchar](50) NOT NULL, seq int not null,product_id varchar(50) NOT NULL,product_name varchar(50) NOT NULL,specify varchar(50) NULL,size decimal(8,2) default 1 not null,unit_no varchar(50) NOT NULL,price decimal(8,2) not null,[remark] [varchar](1024) NULL "
                //          + "CONSTRAINT [PK_quotation_detail] PRIMARY KEY CLUSTERED "
                //          + "([form_id] ASC,seq asc)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_task_list where task_code='OPA_002'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('OPA_002','Quotation Form',1,'OPERATION','OPA_QUOTATION_FORM','','OPA_QUOTATION_FORM','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_002", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_002", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //ver = "20140419";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加客户报价单;  \r\n\r\n");
                //    sb.Append("2. 在固定资产列表中增加资产评估的明细查询;  \r\n\r\n");
                //    sb.Append("3. 修改采购收货付款状况及库存结算的Bug;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
                //            + "select warehouse_id,product_id,size,0,'上月结存','','2014-04-01',sum(start_qty+input_qty-output_qty),0,0,'','SYSADMIN',getdate() "
                //            + "from warehouse_product_inout where inout_date>='2014-03-01' and inout_date<='2014-03-31' group by warehouse_id,product_id,size ";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140424
                //sql = "Select count(1) from SysColumns where Name='pay_status' and id=Object_id('sales_shipment')";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 1)
                //{
                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.pay_status', N'Tmp_pay_status', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.Tmp_pay_status', N'receive_status', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.pay_date', N'Tmp_pay_date', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.Tmp_pay_date', N'receive_date', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.payment_method', N'Tmp_payment_method', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "EXECUTE sp_rename N'dbo.sales_shipment.Tmp_payment_method', N'pay_method', 'COLUMN' ";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "Select count(1) from sysobjects where name='acc_expense_form'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_expense_form]([id] [int] IDENTITY(1,1) NOT NULL,[status] [int] NOT NULL,[form_date] [date] NOT NULL,[no] [varchar](50) NULL,[expense_id] [varchar](50) NOT NULL,[expense_name] [varchar](50) NOT NULL,[expense_man] [varchar](50) NOT NULL,[currency] [varchar](50) NOT NULL,[exchange_rate] [decimal](8, 3) NOT NULL,[expense_mny] [decimal](12, 2) NOT NULL,"
                //        + "[subject_id] [varchar](50) NOT NULL,[detail_id] [varchar](50) NULL,[detail_name] [varchar](50) NULL,[check_no] [varchar](50) NULL,[check_type] [varchar](50) NULL,[bank_name] [varchar](50) NULL,[promise_date] [date] NULL,[remark] [varchar](150) NULL,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,[audit_man] [varchar](50) NULL,[audit_time] [datetime] NULL,[acc_name] [varchar](50) NULL,[acc_seq] [int] default 0 NOT NULL,"
                //        + "CONSTRAINT [PK_acc_expense_form] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_parameters where para_id='NEED_EXPENSE_NO'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
                //        + "('NEED_EXPENSE_NO','费用单据No不可为空','T','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_task_list where task_code='QRY_005'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_005','Quotation Detail',1,'QUERY','QRY_QUOTATION_DETAIL','','QRY_QUOTATION_DETAIL','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_005", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_005", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //sql = "select count(1) from sys_task_list where task_code='ACC_086'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('ACC_086','Account Expense Summary',1,'ACCOUNT','ACC_EXPENSE_SUMMARY','','ACC_EXPENSE_SUMMARY','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_086", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_086", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}

                //ver = "20140424";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver+ "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加费用单及相关查询;  \r\n\r\n");
                //    sb.Append("2. 增加样品单;  \r\n\r\n");
                //    sb.Append("3. 增加报价查询程序;  \r\n\r\n");
                //    sb.Append("4. 在报价单时，增加历史明细右键查询;  \r\n\r\n");
                //    sb.Append("5. 在开送货单时，增加历史明细右键查询;  \r\n\r\n");
                //    sb.Append("6. 修改付款状态为收款状况等;  \r\n\r\n");
                    
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_sales_shipment_detail_pdt ON dbo.sales_shipment_detail(product_id) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_purchase_detail_pdt ON dbo.purchase_detail(product_id) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "CREATE NONCLUSTERED INDEX IX_quotation_detail_pdt ON dbo.quotation_detail(product_id) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140427
                //sql = "Select count(1) from sysobjects where name='acc_analyze_profit'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_analyze_profit]([acc_name] [varchar](50) NOT NULL,[acc_year] int not null,[acc_month] int not null,[subject_id] [varchar](50) NOT NULL,[subject_name] [varchar](50) NOT NULL,[sum_mny] [decimal](18, 2) NOT NULL,ad_flag smallint not null "
                //        + "CONSTRAINT [PK_acc_analyze_profit] PRIMARY KEY CLUSTERED ([acc_name] ASC,[subject_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='acc_analyze_sales'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_analyze_sales]([acc_name] [varchar](50) NOT NULL,[acc_year] int not null,[acc_month] int not null,[ship_type] varchar(50) not null,[cust_id] [varchar](50) NOT NULL,[cust_name] [varchar](50) NOT NULL,[sum_mny] [decimal](18, 2) NOT NULL,ad_flag smallint not null "
                //        + "CONSTRAINT [PK_acc_analyze_sales] PRIMARY KEY CLUSTERED ([acc_name] ASC,[ship_type] ASC,[cust_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='acc_analyze_purchase'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_analyze_purchase]([acc_name] [varchar](50) NOT NULL,[acc_year] int not null,[acc_month] int not null,[purchase_type] varchar(50) not null,[vendor_name] [varchar](50) NOT NULL,[sum_mny] [decimal](18, 2) NOT NULL,ad_flag smallint not null "
                //        + "CONSTRAINT [PK_acc_analyze_purchase] PRIMARY KEY CLUSTERED ([acc_name] ASC,[purchase_type] ASC,[vendor_name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='acc_analyze_waste'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_analyze_waste]([acc_name] [varchar](50) NOT NULL,[acc_year] int not null,[acc_month] int not null,[subject_id] varchar(50) not null,[subject_name] [varchar](50) NOT NULL,[sum_mny] [decimal](18, 2) NOT NULL,ad_flag smallint not null "
                //        + "CONSTRAINT [PK_acc_analyze_waste] PRIMARY KEY CLUSTERED ([acc_name] ASC,[subject_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "Select count(1) from sysobjects where name='acc_analyze_product'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "CREATE TABLE [dbo].[acc_analyze_product]([acc_name] [varchar](50) NOT NULL,[acc_year] int not null,[acc_month] int not null,[product_id] varchar(50) not null,[product_name] [varchar](50) NOT NULL,[start_qty] [decimal](18, 2) NOT NULL,[start_mny] [decimal](18, 2) NOT NULL,[purchase_qty] [decimal](18, 2) NOT NULL,[purchase_mny] [decimal](18, 2) NOT NULL,[sales_qty] [decimal](18, 2) NOT NULL,[sales_mny] [decimal](18, 2) NOT NULL,[used_qty] [decimal](18, 2) NOT NULL,[used_mny] [decimal](18, 2) NOT NULL,[production_qty] [decimal](18, 2) NOT NULL,[production_mny] [decimal](18, 2) NOT NULL,[surplus_qty] [decimal](18, 2) NOT NULL,[surplus_mny] [decimal](18, 2) NOT NULL,[waste_qty] [decimal](18, 2) NOT NULL,[waste_mny] [decimal](18, 2) NOT NULL,[wip_price] [decimal](18, 8) NOT NULL "
                //        + "CONSTRAINT [PK_acc_analyze_product] PRIMARY KEY CLUSTERED ([acc_name] ASC,[product_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //sql = "select count(1) from sys_task_list where task_code='ACC_088'";
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('ACC_088','Account Analyze Report',1,'ACCOUNT','ACC_ANALYZE_REPORT','','ACC_ANALYZE_REPORT','','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_088", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_088", "SYSADMIN");
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                //ver = "20140427";
                //sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
                //iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                //if (iRet == 0)
                //{
                //    string title = ver + "日修改";
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("1. 增加蓝图对帐单;  \r\n\r\n");
                //    sb.Append("2. 为费用登记统计作饼图;  \r\n\r\n");
                //    sb.Append("3. 增加财务分析报表;  \r\n\r\n");
                //    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
                //    SqlHelper.ExecuteNonQuery(sql);

                //    sql = "alter table customer_list alter column addr varchar(150) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //    sql = "alter table customer_list alter column shipment_addr varchar(150) null";
                //    SqlHelper.ExecuteNonQuery(sql);
                //}
                #endregion

                #region ver20140501
                sql = "Select count(1) from SysColumns where Name='process_mny' and id=Object_id('production_form')";
                iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                if (iRet == 0)
                {
                    sql = "alter table production_form add process_mny decimal(12,2) default 0 not null";
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = "update production_form set process_mny=detail_sum";
                    SqlHelper.ExecuteNonQuery(sql);
                    sql = "select form_id,sum(size*qty*cost_price) mny from production_form_material group by form_id";
                    using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                    {
                        while (rdr.Read())
                        {
                            sql = string.Format("update production_form set detail_sum={0} where form_id='{1}'", Convert.ToDecimal(rdr["mny"]), rdr["form_id"].ToString());
                            SqlHelper.ExecuteNonQuery(sql);
                        }
                    }
                }

				//sql = "select count(1) from sys_task_list where task_code='QRY_069'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_069','Asset Log',1,'QUERY','QRY_ASSET_LOG','','QRY_ASSET_LOG','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_069", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_069", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "select count(1) from sys_task_list where task_code='QRY_035'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,web_url,form_name,remark,update_user,update_time)values('QRY_035','Production Summary',1,'QUERY','QRY_PRODUCTION_SUMMARY','','QRY_PRODUCTION_SUMMARY','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_035", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_035", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140501";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加系统注册功能;  \r\n\r\n");
				//    sb.Append("2. 为生产及固定资产增加查询功能;  \r\n\r\n");
				//    sb.Append("3. 修复从采购单导入送货的bug;  \r\n\r\n");
				//    sb.Append("4. 修改生产单据detail_sum的含义为成本，另外加一个字段：process_mny;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "alter table acc_other_payable drop constraint PK_acc_other_payable";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "alter table acc_other_payable add constraint PK_acc_other_payable primary key clustered([ID])";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140505
				//ver = "20140505";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 在选择费用科目的页面中增加新增和删除按钮;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update acc_subject_list set lock_flag=1,update_user='SYSADMIN' where subject_id in ('91353080','91353082','91353535')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140510
				//sql = "select count(1) from sys_parameters where para_id='PRODUCT_CLEAR_DAYS'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values('PRODUCT_CLEAR_DAYS','清理多少天未有进出库的产品',60,'', 'SYSADMIN', getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='status' and id=Object_id('product_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table product_list add status smallint not null default 1";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='email' and id=Object_id('customer_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table customer_list add email varchar(50) null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='qq' and id=Object_id('customer_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table customer_list add qq varchar(50) null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='customer_score_rule'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[customer_score_rule](action_code varchar(50) not null,action_type varchar(150) not null,[scores] numeric(18,1) NOT NULL,seq int not null default 0,trace_flag smallint not null default 0,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_customer_score_rule] PRIMARY KEY CLUSTERED ([action_code] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('NEWCUST','新客户，名称、联系人、电话、地址、QQ、Email每项分数',20,10,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('DELCUST','新客户，名称、联系人、电话、地址、QQ、Email每项扣数',20,15,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('CALL','电话联系',50,20,1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('EMAIL','EMail客户',50,25,1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('QQ','QQ客户',50,30,1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('VISIT','拜访客户',100,35,1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('WELCOME','客户来访',100,40,1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('QUOTATION','报价加分',100,50,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('SAMPLE','取样加分',0,60,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('SHIPMENT','送货按金额每元加分',1,70,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('GATHERING','收款按金额每元加分',1,80,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('WITHDRAWAL','退货单按金额每元扣分',1,90,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_score_rule(action_code,action_type,scores,seq,trace_flag,update_user,update_time)values('BADDEBTS','货款收不回按金额每元扣分',2,100,0,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='customer_log'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[customer_log]([ID] [int] IDENTITY(1,1) NOT NULL,[cust_id] [varchar](50) NOT NULL,[cust_name] [varchar](50) NOT NULL,[action_code] [varchar](50) NOT NULL,[action_type] [varchar](50) NOT NULL,[action_man] [varchar](50) NULL,"
				//            + "[form_id] [varchar](50) NULL,[action_subject] [varchar](50) NULL,[action_content] [varchar](1024) NULL,[object_name] [varchar](50) NULL,[venue] [varchar](50) NULL,[from_time] [varchar](50) NULL,[to_time] [varchar](50) NULL,"
				//            + "[scores] numeric(18,1) NOT NULL,[ad_flag] [smallint] NOT NULL,trace_flag smallint not null default 0,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_customer_log] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "CREATE NONCLUSTERED INDEX IX_customer_log ON dbo.customer_log(cust_id asc,update_time asc,action_code asc) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_group where group_id='CRM'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time)values('CRM','Customer Relation Manage',1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "select count(1) from sys_task_list where task_code='CRM_005'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('CRM_005','Customer Score Rule',1,'CRM','CUSTOMER_SCORE_RULE','CUSTOMER_SCORE_RULE','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "CRM_005", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "CRM_005", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='MTN_003'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('MTN_003','Product Clear',1,'MAINTENANCE','MTN_PRODUCT_CLEAR','MTN_PRODUCT_CLEAR','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "MTN_003", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "MTN_003", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140510";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 新增客户资料时，添加重复客户的提示功能;  \r\n\r\n");
				//    sb.Append("2. 给客户资料增加左边选项树;  \r\n\r\n");
				//    sb.Append("3. 给客户资料增加跟进功能;  \r\n\r\n");
				//    sb.Append("4. 给客户资料增加Email,QQ;  \r\n\r\n");
				//    sb.Append("5. 增加业务积分处理功能;  \r\n\r\n");
				//    sb.Append("6. 增加产品清理及恢复的功能;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "ALTER TABLE [dbo].[product_list] WITH CHECK ADD CONSTRAINT [FK_product_list_type] FOREIGN KEY([product_type]) REFERENCES [dbo].[product_type_list] ([product_type])";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140511
				//ver = "20140511";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修改应付账款为应付帐款;  \r\n\r\n");
				//    sb.Append("2. 优化月结的处理方式;  \r\n\r\n");
				//    sb.Append("3. 优化仓库出入及生产配料的成本设置的处理方式;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update acc_subject_list set subject_name='应付帐款' where subject_id ='5145'";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update acc_credence_detail set subject_name='应付帐款' where subject_id ='5145'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140517
				//sql = "select count(1) from sys_parameters where para_id='CUSTOMER_LOG_DELAY_DAYS'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values('CUSTOMER_LOG_DELAY_DAYS','工作日志最多能推迟几天完成',1,'', 'SYSADMIN', getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_parameters where para_id='ACTION_SCHEDULE_AHEAD_DAYS'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values('ACTION_SCHEDULE_AHEAD_DAYS','工作计划能提前多少天提交',30,'', 'SYSADMIN', getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='crm_action_schedule'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[crm_action_schedule]([action_man] [varchar](50) NOT NULL,[action_date] [date] NOT NULL,[action_content] [varchar](1024) NOT NULL,[status] [smallint] NOT NULL,[status_desc] [varchar](50) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,"
				//        + "CONSTRAINT [PK_crm_action_schedule] PRIMARY KEY CLUSTERED ([action_man] ASC,[action_date] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "drop table customer_log";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE TABLE [dbo].[customer_log]([ID] [int] IDENTITY(1,1) NOT NULL,[cust_id] [varchar](50) NOT NULL,[cust_name] [varchar](50) NOT NULL,[action_code] [varchar](50) NOT NULL,[action_type] [varchar](50) NOT NULL,[action_man] [varchar](50) NULL,"
				//            + "[form_id] [varchar](50) NULL,[action_subject] [varchar](50) NULL,[action_content] [varchar](1024) NULL,[object_name] [varchar](50) NULL,[venue] [varchar](50) NULL,[from_time] [datetime] not null,[to_time] [datetime] null,"
				//            + "[scores] numeric(18,1) NOT NULL,[ad_flag] [smallint] NOT NULL,trace_flag smallint not null default 0,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_customer_log] PRIMARY KEY CLUSTERED ([ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "CREATE NONCLUSTERED INDEX IX_customer_log ON dbo.customer_log(cust_id asc,update_time asc,action_code asc) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='CRM_050'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('CRM_050','Customer Log',1,'CRM','CRM_CUSTOMER_LOG','CRM_CUSTOMER_LOG','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "CRM_050", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "CRM_050", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='CRM_060'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('CRM_060','Data Calendar',1,'CRM','CRM_DATA_CALENDAR','CRM_DATA_CALENDAR','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "CRM_060", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "CRM_060", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='CRM_070'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('CRM_070','Action Scores Summary',1,'CRM','ACTION_SCORES_SUMMARY','ACTION_SCORES_SUMMARY','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "CRM_070", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "CRM_070", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='ACC_087'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_087','Account Credence Summary',1,'CRM','ACC_CREDENCE_SUMMARY','ACC_CREDENCE_SUMMARY','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_087", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_087", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140517";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加CRM的日历报表;  \r\n\r\n");
				//    sb.Append("2. 增加记帐凭证汇总表;  \r\n\r\n");
				//    sb.Append("3. 业务员积分统计表;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140523
				//sql = "Select count(1) from sysobjects where name='sales_design_form'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[sales_design_form]([id] [int] IDENTITY(1,1) NOT NULL,[status] [smallint] default 0 not null,form_date date not null,[form_type] [varchar](50) NOT NULL,[ad_flag] [smallint] NOT NULL,[no] [varchar](50) NULL,cust_id varchar(50) not null,cust_name varchar(50) not null,sales_man varchar(50) not null,[cust_order_no] varchar(50) null,product_name varchar(150) not null,unit_no varchar(50) not null,qty decimal(18,2) not null,mny decimal(18,2) not null, sales_mny decimal(18,2) not null,currency varchar(50) not null,exchange_rate decimal(8,3) default 1 not null,remark varchar(1024) null,[receive_status] [int] NOT NULL,[account_no] varchar(50) null,receive_date varchar(50) null,[invoice_status] [int] NOT NULL,[invoice_no] [varchar](50) NULL,[invoice_mny] [decimal](12, 2) NOT NULL,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,[audit_man] [varchar](50) NULL,[audit_time] [datetime] NULL,acc_name varchar(50) null,acc_seq int default 0 not null,"
				//            + "CONSTRAINT [PK_sales_design_form] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='OPA_004'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_004','Sales Design Form',1,'OPERATION','SALES_DESIGN_FORM','SALES_DESIGN_FORM','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_004", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_004", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='QRY_012'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('QRY_012','Sales Design Summary',1,'OPERATION','QRY_SALES_DESIGN_SUMMARY','QRY_SALES_DESIGN_SUMMARY','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_012", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_012", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='QRY_017'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('QRY_017','Sales Design Detail',1,'OPERATION','QRY_SALES_DESIGN_DETAIL','QRY_SALES_DESIGN_DETAIL','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "QRY_017", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "QRY_017", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='ACC_099'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_099','Account Balance',1,'ACCOUNT','ACC_ACCOUNT_BALANCE','ACC_ACCOUNT_BALANCE','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_099", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_099", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='CRM_010'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('CRM_010','Change Sales Man',1,'OPERATION','CRM_CHANGE_SALES_MAN','CRM_CHANGE_SALES_MAN','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "CRM_010", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "CRM_010", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140523";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 月末结算时，增加成本价格为0的提示;  \r\n\r\n");
				//    sb.Append("2. 支票列表中将默认的状态条件改为未承兑;  \r\n\r\n");
				//    sb.Append("3. 增加财务平衡表;  \r\n\r\n");
				//    sb.Append("4. 增加设计加工处理程序;  \r\n\r\n");
				//    sb.Append("5. 在送货单明细查询中增加客户订单号的查询条件;  \r\n\r\n");
				//    sb.Append("6. 增加批量更改业务员的功能;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "alter table acc_credence_detail alter column borrow_money decimal(18, 8) not null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "alter table acc_credence_detail alter column lend_money decimal(18, 8) not null";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    dalSysParameters dalp = new dalSysParameters();
				//    string company = dalp.GetParaValue("COMPANY_NAME");
				//    if (company.IndexOf("宏镖") >= 0)
				//    {
				//        sql = "update acc_credence_detail set borrow_money=borrow_money+0.80578115 where acc_name<>'2014年03月财务区间' and subject_id='1235' and seq=0 and (detail_seq=803 or detail_seq=802)";
				//        SqlHelper.ExecuteNonQuery(sql);
				//        sql = "update acc_credence_detail set lend_money=lend_money+0.80578115 where acc_name<>'2014年03月财务区间' and subject_id='9015' and seq=0";
				//        SqlHelper.ExecuteNonQuery(sql);

				//        sql = "update acc_credence_detail set borrow_money=borrow_money+0.80477500 where acc_name='2014年03月财务区间' and subject_id='1235' and seq=0";
				//        SqlHelper.ExecuteNonQuery(sql);
				//        sql = "update acc_credence_detail set lend_money=lend_money+0.80477500 where acc_name='2014年03月财务区间' and subject_id='9015' and seq=0";
				//        SqlHelper.ExecuteNonQuery(sql);
				//    }
				//}
				#endregion

				#region ver20140527
				//sql = "Select count(1) from sysobjects where name='customer_order_list'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[customer_order_list]([id] [int] IDENTITY(1,1) NOT NULL,[cust_id] [varchar](50) NOT NULL,[cust_name] [varchar](50) NOT NULL,[cust_order_no] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[require_date] [date] NOT NULL,[pay_method] [varchar](50) NULL,[sales_man] [varchar](50) NOT NULL,"
				//        + "[product_id] [varchar](50) NOT NULL,[product_name] [varchar](50) NOT NULL,[specify] [varchar](50) NULL,[size] [decimal](18, 2) NOT NULL,[unit_no] [varchar](50) NOT NULL,[qty] [decimal](18, 2) NOT NULL,[price] [decimal](18, 2) NOT NULL,currency varchar(50) not null,[remark] [varchar](1024) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,"
				//        + "CONSTRAINT [PK_customer_order_list] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "CREATE NONCLUSTERED INDEX [IX_customer_order_list] ON [dbo].[customer_order_list] ([cust_id] ASC,[cust_order_no] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE NONCLUSTERED INDEX [IX_customer_order_list_date] ON [dbo].[customer_order_list] ([form_date] ASC,[cust_id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "Select count(1) from SysColumns where Name='paid_sales_mny' and id=Object_id('sales_shipment_detail')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sales_shipment_detail add paid_sales_mny decimal(8,2) not null default 0";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='paid_sales_mny' and id=Object_id('sales_design_form')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sales_design_form add paid_sales_mny decimal(8,2) not null default 0";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='pay_method' and id=Object_id('sales_design_form')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sales_design_form add pay_method varchar(50) null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='cust_order_no' and id=Object_id('purchase_detail')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table purchase_detail add cust_order_no varchar(50) null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='OPA_001'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_001','Customer Order List',1,'OPERATION','OPA_CUSTOMER_ORDER_LIST','OPA_CUSTOMER_ORDER_LIST','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_001", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_001", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_parameters where para_id='NEED_CUST_ORDER_NO'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
				//        + "('NEED_CUST_ORDER_NO','送货单客户订单号不可为空','F','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_parameters where para_id='ORDER_FINISHED_RATIO'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
				//        + "('ORDER_FINISHED_RATIO','订单已送货多少比率记为已完成','0.9','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_parameters where para_id='ORDER_OVERFLOW_RATIO'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
				//        + "('ORDER_OVERFLOW_RATIO','订单已送货多少比率记为过量','1.1','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140527";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 优化产品查找功能,能匹配*查找;  \r\n\r\n");
				//    sb.Append("2. 将一些查询挪到CRM模块中;  \r\n\r\n");
				//    sb.Append("3. 在报价单中增加结算方式及已付提成;  \r\n\r\n");
				//    sb.Append("4. 增加客户订单及订单跟踪、查询等功能;  \r\n\r\n");
				//    sb.Append("5. 删除购销一体单的功能及数据表;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update sys_task_list set url='OPA_SALES_DESIGN_FORM',form_name='OPA_SALES_DESIGN_FORM' where task_code='OPA_004'";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "delete sys_task_grant where task_code='OPA_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "delete sys_task_list where task_code='OPA_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "drop table purchase_sales_detail";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "drop table purchase_sales_detail_wh";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "drop table purchase_sales_form";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140529
				//sql = "Select count(1) from SysColumns where Name='product_id' and id=Object_id('acc_analyze_sales')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "drop table acc_analyze_sales";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE TABLE [dbo].[acc_analyze_sales]([acc_name] [varchar](50) NOT NULL,[acc_year] [int] NOT NULL,[acc_month] [int] NOT NULL,[ship_type] [varchar](50) NOT NULL,[cust_id] [varchar](50) NOT NULL,[cust_name] [varchar](50) NOT NULL,"
				//        + "[ad_flag] [smallint] NOT NULL,[product_id] [varchar](50) NOT NULL,[product_name] [varchar](50) NOT NULL,[qty] [decimal](18, 2) NOT NULL,[price] [decimal](18, 2) NOT NULL,[cost_price] [decimal](18, 2) NOT NULL,[mny] [decimal](18, 2) NOT NULL,[cost_mny] [decimal](18, 2) NOT NULL,"
				//        + "CONSTRAINT [PK_acc_analyze_sales] PRIMARY KEY CLUSTERED ([acc_name] ASC,[cust_id] ASC,[ship_type] ASC,[product_id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_parameters where para_id='HIDE_NOT_MATCH_PRODUCT'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_parameters(para_id,para_name,para_value,remark,update_user,update_time)values"
				//        + "('HIDE_NOT_MATCH_PRODUCT','隐藏搜索未匹配的产品','T','T-隐藏;   F-不隐藏','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140529";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加参数HIDE_NOT_MATCH_PRODUCT;  \r\n\r\n");
				//    sb.Append("2. 增加客户利润分析表;  \r\n\r\n");
				//    sb.Append("3. 增加资产负债表导出功能;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update sys_parameters set remark='T-是;   F-否' where para_value in ('T','F')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140530
				//sql = "Select count(1) from SysColumns where Name='start_qty2' and id=Object_id('acc_product_inout')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table acc_product_inout add start_qty2 decimal(12,3) not null default 0,start_mny2 decimal(18,8) not null default 0";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_product_inout set start_qty2=start_qty,start_mny2=start_mny where acc_seq=0";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140530";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加零库存清理凭证;  \r\n\r\n");
				//    sb.Append("2. 修正资产负债表导出时日期未导出的bug;  \r\n\r\n");
				//    sb.Append("3. 结算时允许更改成本价;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140604
				//sql = "Select count(1) from SysColumns where Name='contact_person' and id=Object_id('quotation_form')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table quotation_form add contact_person varchar(50)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='qty' and id=Object_id('quotation_detail')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table quotation_detail add brand varchar(50)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "alter table quotation_detail add qty decimal(12,2) not null default 0";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140604";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 在报价单中增加品牌、数量及金额;  \r\n\r\n");
				//    sb.Append("2. 修复采购收货规格与品牌混淆的Bug;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140610
				//ver = "20140610";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修复期初商品库存的bug;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    dalSysParameters dalp = new dalSysParameters();
				//    string company = dalp.GetParaValue("COMPANY_NAME");
				//    if (company.IndexOf("蓝图") >= 0)
				//    {
				//        sql = "select product_id,start_qty from warehouse_product_inout where warehouse_id='一号仓' and form_type='期初数据' and inout_date='2014-05-01'";
				//        using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
				//        {
				//            while (rdr.Read())
				//            {
				//                sql="insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_mny,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time)values('2014年05月财务区间',0,'2014-05-01','"+ rdr[0].ToString() +"',1,'"+ Convert.ToDecimal(rdr[1]) +"',0,0,0,0,0,'0','期初数据','一号仓','SYSADMIN',getdate())";
				//                SqlHelper.ExecuteNonQuery(sql);
				//            }
				//        }
				//    }

				//    sql = "CREATE NONCLUSTERED INDEX IX_pur_cust_order_no ON dbo.purchase_detail(cust_order_no asc) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140612
				//ver = "20140612";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 优化月末结算程序,增加销售额和毛利润列,方便比较;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140617
				//ver = "20140617";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修改蓝图送货单格式;  \r\n\r\n");
				//    sb.Append("2. 修改对帐单,退货单导出为负值;  \r\n\r\n");
				//    sb.Append("3. 在客户列表中增加高阶查询;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140627
				//ver = "20140627";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修改客户报价的Bug(数量与单价错位了);  \r\n\r\n");
				//    sb.Append("2. 增加从报价单导入客户订单的功能;  \r\n\r\n");
				//    sb.Append("3. 优化客户订单界面;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140707
				//sql = "select count(1) from acc_credence_detail where acc_name='2014年05月财务区间' and seq=22 and detail_seq=2 and borrow_money=20933";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 1)
				//{
				//    sql = "insert into acc_credence_detail(acc_name,seq,detail_seq,subject_id,subject_name,zcfz_flag,ad_flag,detail_id,detail_name,digest,borrow_money,lend_money,exchange_rate,currency)values("
				//        + "'2014年05月财务区间',22,3,'1030','现金银行',1,1,'现金','','收客户货款',14695,0,1,'人民币')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set borrow_money=borrow_money-14695 where acc_name='2014年05月财务区间' and seq=22 and detail_seq=2 and borrow_money=20933";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set borrow_money=borrow_money-14695 where acc_name='2014年06月财务区间' and seq=0 and detail_id='32721'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set borrow_money=borrow_money+14695 where acc_name='2014年06月财务区间' and seq=0 and detail_id='现金'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "select count(1) from acc_credence_detail where acc_name='2014年05月财务区间' and seq=20 and detail_seq=2 and lend_money=13882";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 1)
				//{
				//    sql = "insert into acc_credence_detail(acc_name,seq,detail_seq,subject_id,subject_name,zcfz_flag,ad_flag,detail_id,detail_name,digest,borrow_money,lend_money,exchange_rate,currency)values("
				//        + "'2014年05月财务区间',20,3,'1030','现金银行',1,1,'现金','','付供应商货款',0,1125,1,'人民币')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-1125 where acc_name='2014年05月财务区间' and seq=20 and detail_seq=2 and lend_money=13882";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-1125 where acc_name='2014年06月财务区间' and seq=0 and detail_id='32721'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money+1125 where acc_name='2014年06月财务区间' and seq=0 and detail_id='现金'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//ver = "20140707";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修改收付款等凭证的bug,当一张凭证里既有现金，又有银行或都多个帐号收付款时，凭证分析有误;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140710
				//sql = "Select count(1) from SysColumns where Name='task_type' and id=Object_id('sys_task_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sys_task_list add task_type varchar(50)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set task_type='MAINTENANCE' where group_id='ACCOUNT' and task_code<='ACC_008'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set task_type='OPERATION' where group_id='ACCOUNT' and task_code<='ACC_008'";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time,seq)values('SALES', 'SALES' ,1, 'SYSADMIN',getdate(),200)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time,seq)values('PURCHASE', 'PURCHASE' ,1, 'SYSADMIN',getdate(),300)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time,seq)values('PRODUCTION', 'PRODUCTION' ,1, 'SYSADMIN',getdate(),400)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time,seq)values('WAREHOUSE', 'WAREHOUSE' ,1, 'SYSADMIN',getdate(),500)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into sys_task_group(group_id,group_desc,status,update_user,update_time,seq)values('ASSET', 'ASSET' ,1, 'SYSADMIN',getdate(),700)";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update sys_task_group set seq=100 where group_id='CRM'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set seq=600 where group_id='ACCOUNT'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set seq=800 where group_id='SECURITY'";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_006'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_007A'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_008'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_016'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_020'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_024'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_030'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_036'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_040'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_046'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_056'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_063'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_066'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_073'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_076'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_080'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_081'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_082'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_083'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_085'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_086'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_087'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_088'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_091'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_095'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_099'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_008'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_013'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_016'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_031'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_032'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_033'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_034'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_036'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_051'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_052'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_015'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PRODUCTION',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_025'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_026'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_012'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_015'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_017'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_020'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_025'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PRODUCTION',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_035'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_069'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_090'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_099'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SET_090'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SYS_001'";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "delete sys_task_group where group_id in ('OPERATION','HELP','MAINTENANCE','QUERY','SETTING')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140710";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 启动hbnet项目;  \r\n\r\n");
				//    sb.Append("2. 修复报价目单导入客户订单的bug;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140724
				//sql = "select count(1) from sys_task_list where task_code='ACC_022'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_022','Price Adjust',1,'ACCOUNT','ACC_PRICE_ADJUST','ACC_PRICE_ADJUST','pages/ACCOUNT/ACC_PRICE_ADJUST.aspx','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_022", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_022", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "Select count(1) from sysobjects where name='price_adjust_form'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[price_adjust_form](form_id varchar(50) not null, form_date date not null,status smallint default 0 not null,[remark] [varchar](1024) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,audit_man varchar(20) null,audit_time datetime null,acc_name varchar(50) null,acc_seq int default 0 not null,"
				//        + "CONSTRAINT [PK_price_adjust_form] PRIMARY KEY CLUSTERED ([form_id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "CREATE TABLE [dbo].[price_adjust_detail](form_id varchar(50) not null, seq int not null,product_id varchar(50) not null,product_name varchar(50) not null,current_price decimal(18,3),true_price decimal(18,3),qty decimal(18,3) not null,remark varchar(50),"
				//        + "CONSTRAINT [PK_price_adjust_detail] PRIMARY KEY CLUSTERED ([form_id] ASC, seq ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//ver = "20140724";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加价格调整功能;  \r\n\r\n");
				//    sb.Append("2. 在仓库库存信息及财务期末库存表中增加了查找功能;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20140810
				//sql = "select count(1) from acc_credence_detail where acc_name='2014年06月财务区间' and seq=19 and detail_seq=2 and lend_money=8194.7";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 1)
				//{
				//    sql = "insert into acc_credence_detail(acc_name,seq,detail_seq,subject_id,subject_name,zcfz_flag,ad_flag,detail_id,detail_name,digest,borrow_money,lend_money,exchange_rate,currency)values("
				//        + "'2014年06月财务区间',19,9,'1030','现金银行',1,1,'32721','','付费用',0,3106.7,1,'人民币')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-3106.7 where acc_name='2014年06月财务区间' and seq=19 and detail_seq=2 and lend_money=8194.7";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money+3106.7 where acc_name='2014年07月财务区间' and seq=0 and detail_id='32721'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-3106.7 where acc_name='2014年07月财务区间' and seq=0 and detail_id='现金'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "select count(1) from acc_credence_detail where acc_name='2014年06月财务区间' and seq=25 and detail_seq=2 and lend_money=1085";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 1)
				//{
				//    sql = "insert into acc_credence_detail(acc_name,seq,detail_seq,subject_id,subject_name,zcfz_flag,ad_flag,detail_id,detail_name,digest,borrow_money,lend_money,exchange_rate,currency)values("
				//        + "'2014年06月财务区间',25,4,'1030','现金银行',1,1,'32721','','付费用',0,700,1,'人民币')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-700 where acc_name='2014年06月财务区间' and seq=25 and detail_seq=2 and lend_money=1085";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money+700 where acc_name='2014年07月财务区间' and seq=0 and detail_id='32721'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update acc_credence_detail set lend_money=lend_money-700 where acc_name='2014年07月财务区间' and seq=0 and detail_id='现金'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20140810";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修改收付款等凭证的bug,当一张凭证里既有现金，又有银行或都多个帐号收付款时，凭证分析有误;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_006'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_007A'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_008'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_016'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_020'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_024'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_030'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_036'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_040'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_046'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_056'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_063'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_066'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_073'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_076'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_080'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_081'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_082'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_083'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_085'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_086'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_087'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_088'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_091'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_095'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_099'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='AST_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_060'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='CRM_070'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='H003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_008'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_013'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_016'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_031'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_032'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_033'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_034'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_036'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_051'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='MTN_052'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_015'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PRODUCTION',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_025'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_026'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='OPA_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='CRM',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_010'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_012'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_015'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_017'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_020'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PURCHASE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_025'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='PRODUCTION',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_035'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='WAREHOUSE',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_050'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ASSET',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_069'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_090'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='QUERY',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='QRY_099'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_002'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_003'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_004'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_005'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SECURITY',task_type='OPERATION',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SEC_007'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='SALES',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SET_090'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',task_type='MAINTENANCE',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='SYS_001'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_list set group_id='ACCOUNT',web_url='pages/'+group_id+'/'+task_type+'/'+url+'.aspx' where task_code='ACC_022'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150202
				//ver = "20150202";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 期初库存设置增加“复制此行”右键菜单;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150330
				//sql = "Select count(1) from sysobjects where name='vendor_order_list'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[vendor_order_list]([id] [int] IDENTITY(1,1) NOT NULL,[vendor_name] [varchar](50) NOT NULL,[vendor_order_no] [varchar](50) NOT NULL,[form_date] [date] NOT NULL,[require_date] [date] NOT NULL,[pay_method] [varchar](50) NULL,"
				//        + "[purchase_man] [varchar](50) NOT NULL,[product_id] [varchar](50) NOT NULL,[product_name] [varchar](50) NOT NULL,[specify] [varchar](50) NULL,[size] [decimal](18, 2) NOT NULL,[unit_no] [varchar](50) NOT NULL,[qty] [decimal](18, 2) NOT NULL,[price] [decimal](18, 2) NOT NULL,"
				//        + "[currency] [varchar](50) NOT NULL,[remark] [varchar](1024) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,CONSTRAINT [PK_vendor_order_list] PRIMARY KEY CLUSTERED ([id] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, "
				//        + "IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE NONCLUSTERED INDEX IX_vendor_order_list_vendor ON dbo.vendor_order_list(vendor_name) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE NONCLUSTERED INDEX IX_purchase_list_vendor ON dbo.purchase_list(vendor_name) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}                
				//sql = "select count(1) from sys_task_list where task_code='OPA_011'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_011','Purchase Order',1,'ACCOUNT','OPA_VENDOR_ORDER_LIST','OPA_VENDOR_ORDER_LIST','pages/OPERATION/OPA_VENDOR_ORDER_LIST.aspx','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_011", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_011", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20150330";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加采购订单功能;  \r\n\r\n");
				//    sb.Append("2. 资产增加凭证科目由资产净值改为原值;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150415
				//sql = "Select count(1) from sysobjects where name='acc_trial_credence_list'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[acc_trial_credence_list]([acc_name] [varchar](50) NOT NULL,[seq] [int] NOT NULL,[status] [int] NOT NULL,[credence_type] [varchar](50) NOT NULL,[credence_word] [varchar](50) NOT NULL,[credence_date] [date] NOT NULL,[attach_count] [int] NOT NULL,[remark] [varchar](1024) NULL,[update_user] [varchar](20) NOT NULL,[update_time] [datetime] NOT NULL,[audit_man] [varchar](20) NULL,[audit_time] [datetime] NULL,"
				//        + "CONSTRAINT [PK_acc_trial_credence_list] PRIMARY KEY CLUSTERED ([acc_name] ASC,[seq] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "ALTER TABLE [dbo].[acc_trial_credence_list] ADD  CONSTRAINT [DF_acc_trial_crede_statu_116A8EFB]  DEFAULT ((0)) FOR [status]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='acc_trial_credence_detail'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[acc_trial_credence_detail]([acc_name] [varchar](50) NOT NULL,[seq] [int] NOT NULL,[detail_seq] [int] NOT NULL,[subject_id] [varchar](50) NOT NULL,[subject_name] [varchar](255) NOT NULL,[zcfz_flag] [int] NOT NULL,"
				//        + "[ad_flag] [int] NOT NULL,[detail_id] [varchar](50) NULL,[detail_name] [varchar](150) NULL,[digest] [varchar](150) NULL,[borrow_money] [decimal](18, 8) NOT NULL,[lend_money] [decimal](18, 8) NOT NULL,[exchange_rate] [decimal](8, 4) NOT NULL,[currency] [varchar](50) NOT NULL,"
				//        + "CONSTRAINT [PK_acc_trial_credence_detail] PRIMARY KEY CLUSTERED ([acc_name] ASC,[seq] ASC,[detail_seq] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "ALTER TABLE [dbo].[acc_trial_credence_detail] ADD  CONSTRAINT [DF__acc_trial_crede_detail__excha]  DEFAULT ((1)) FOR [exchange_rate]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "ALTER TABLE [dbo].[acc_trial_credence_detail] ADD  CONSTRAINT [DF_acc_trial_credence_detail_currency]  DEFAULT ('人民币') FOR [currency]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='acc_trial_product_inout'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[acc_trial_product_inout]([ID] [int] IDENTITY(1,1) not null,[acc_name] [varchar](50) NOT NULL,[acc_seq] [int] NOT NULL,[acc_date] [date] NOT NULL,[product_id] [varchar](50) NOT NULL,[size] [decimal](8, 2) NOT NULL,[start_qty] [decimal](12, 3) NOT NULL,[start_mny] [decimal](18, 8) NOT NULL,[input_qty] [decimal](12, 3) NOT NULL,"
				//        + "[input_mny] [decimal](18, 8) NOT NULL,[output_qty] [decimal](12, 3) NOT NULL,[output_mny] [decimal](18, 8) NOT NULL,[form_id] [varchar](50) NULL,[form_type] [varchar](50) NOT NULL,[remark] [varchar](150) NULL,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,"
				//        + "[start_qty2] [decimal](12, 3) NOT NULL,[start_mny2] [decimal](18, 8) NOT NULL,CONSTRAINT [PK_acc_trial_product_inout] PRIMARY KEY CLUSTERED (ID ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "ALTER TABLE [dbo].[acc_trial_product_inout] ADD  DEFAULT ((0)) FOR [start_qty2]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "ALTER TABLE [dbo].[acc_trial_product_inout] ADD  DEFAULT ((0)) FOR [start_mny2]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE NONCLUSTERED INDEX IX_acc_trial_product_inout ON dbo.acc_trial_product_inout(acc_name asc,acc_seq asc) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='ACC_091'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_091','Account Trial Balance',1,'ACCOUNT','ACC_TRIAL_BALANCE','ACC_TRIAL_BALANCE','pages/OPERATION/ACC_TRIAL_BALANCE.aspx','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_091", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_091", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20150415";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加财务试算功能;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150509
				//ver = "20150509";
				//sql = "Select count(1) from sysobjects where name='customer_level'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[customer_level]([cust_level] [varchar](50) NOT NULL,[description] [varchar](150) NOT NULL,[status] [smallint] NOT NULL,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,"
				//            + "CONSTRAINT [PK_customer_level] PRIMARY KEY CLUSTERED ([cust_level] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "insert into customer_level(cust_level,description,status,update_user,update_time)values('A','A级客户',1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_level(cust_level,description,status,update_user,update_time)values('B','B级客户',1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_level(cust_level,description,status,update_user,update_time)values('C','C级客户',1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into customer_level(cust_level,description,status,update_user,update_time)values('D','D级客户',1,'SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='product_sale_price'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[product_sale_price]([product_id] [varchar](50) NOT NULL,[cust_level] [varchar](50) NOT NULL,[price] [numeric](8, 2) NOT NULL,[update_user] [varchar](50) NOT NULL,[update_time] [datetime] NOT NULL,"
				//            + "CONSTRAINT [PK_product_sale_price] PRIMARY KEY CLUSTERED ([product_id] ASC,[cust_level] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='MTN_006'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('MTN_006','Customer Level',1,'CRM','MTN_CUSTOMER_LEVEL','MTN_CUSTOMER_LEVEL','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "MTN_006", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "MTN_006", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加产品建议销售价格设置;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150516
				//ver = "20150516";                
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "update acc_credence_detail set borrow_money=0,lend_money=886.67000000 where acc_name='2015年03月财务区间' and subject_id='2125' and borrow_money=886.67000000";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修复折旧的错误数据;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150522
				//ver = "20150522";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "delete warehouse_product_inout where form_type='上月结存' and inout_date='2015-04-01'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
				//        + "select warehouse_id,product_id,size,0,'上月结存','','2015-04-01',sum(start_qty+input_qty-output_qty),0,0,'','SYSADMIN',getdate() "
				//        + "from warehouse_product_inout where inout_date>='2015-03-01' and inout_date<='2015-03-31' group by warehouse_id,product_id,size";
				//    SqlHelper.ExecuteNonQuery(sql);

				//    sql = "delete warehouse_product_inout where form_type='上月结存' and inout_date='2015-05-01'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
				//        + "select warehouse_id,product_id,size,0,'上月结存','','2015-05-01',sum(start_qty+input_qty-output_qty),0,0,'','SYSADMIN',getdate() "
				//        + "from warehouse_product_inout where inout_date>='2015-04-01' and inout_date<='2015-04-30' group by warehouse_id,product_id,size";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 修复仓库库存数据;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20150527
				//sql = "Select count(1) from SysColumns where Name='icon' and id=Object_id('sys_task_group')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sys_task_group add icon varchar(50)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='widgets.png' where group_id='ACCOUNT'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='house.png' where group_id='ASSET'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='group_gear.png' where group_id='CRM'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='add_on.png' where group_id='PRODUCTION'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='participation_rate.png' where group_id='PURCHASE'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='page_white_paste.png' where group_id='SALES'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='cog_edit.png' where group_id='SECURITY'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update sys_task_group set icon='bricks.png' where group_id='WAREHOUSE'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='icon' and id=Object_id('sys_task_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table sys_task_list add icon varchar(50)";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "Select count(1) from sysobjects where name='sys_task_shortcut'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "CREATE TABLE [dbo].[sys_task_shortcut]([task_code] [varchar](50) NOT NULL,[user_id] [varchar](15) NOT NULL,[seq][int] NOT NULL,[update_time] [datetime] NOT NULL "
				//            + "CONSTRAINT [PK_sys_task_shortcut] PRIMARY KEY CLUSTERED ([user_id] ASC, [task_code] ASC)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "insert into sys_task_shortcut(task_code,user_id,seq,update_time) select task_code,'SYSADMIN',999,getdate() from sys_task_list where task_code in ('SEC_001','SEC_002','SEC_005','SEC_007','MTN_008','MTN_013')";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20151215
				//sql = "select count(1) from sys_task_list where task_code='ACC_092'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_092','Account Book',1,'ACCOUNT','ACC_BOOK','ACC_BOOK','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_092", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_092", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='ACC_093'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_093','Account Expense Column',1,'ACCOUNT','ACC_EXPENSE_COLUMN','ACC_EXPENSE_COLUMN','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_093", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_093", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from sysobjects where name='warehouse_product_transfer'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "create table [dbo].[warehouse_product_transfer]([id] [int] identity(1,1) not null,[warehouse_from] [varchar](50) not null,[warehouse_to] [varchar](50) not null,[status] [bit] not null,[inv_no] [varchar](50) null,[transfer_date] [date] not null,[product_id] [varchar](50) not null,[size] [numeric](8, 2) not null,[qty] [numeric](8, 3) not null,[remark] [varchar](150) null,[update_user] [varchar](50) not null,[update_time] [datetime] not null,[audit_man] [varchar](50) null,[audit_time] [datetime] null,constraint [pk_warehouse_product_transfer] primary key clustered "
				//        + "([id] asc)with (pad_index = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [primary]) on [primary]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "CREATE NONCLUSTERED INDEX IX_warehouse_product_transfer_date ON dbo.warehouse_product_transfer(transfer_date asc) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='OPA_055'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_055','Warehouse Product Transfer',1,'WAREHOUSE','OPA_WAREHOUSE_PRODUCT_TRANSFER','OPA_WAREHOUSE_PRODUCT_TRANSFER','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_055", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_055", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "select count(1) from sys_task_list where task_code='OPA_006'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_006','Collect Camp',1,'SALES','EditCollectCamp','EditCollectCamp','','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_006", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_006", "SYSADMIN");
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='barcode' and id=Object_id('product_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table product_list add barcode varchar(50) null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//sql = "Select count(1) from SysColumns where Name='default_flag' and id=Object_id('warehouse_list')";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    sql = "alter table warehouse_list add default_flag int default 0 not null";
				//    SqlHelper.ExecuteNonQuery(sql);
				//    sql = "update warehouse_list set default_flag=1 where warehouse_id='一号仓'";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}
				//ver = "20151215";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//    string title = ver + "日修改";
				//    StringBuilder sb = new StringBuilder();
				//    sb.Append("1. 增加应收应付帐本;  \r\n\r\n");
				//    sb.Append("2. 增加转仓单;  \r\n\r\n");
				//    sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//    SqlHelper.ExecuteNonQuery(sql);
				//}

				#endregion

				#region ver20170821
				//sql = "Select count(1) from sysobjects where name='warehouse_inout_daily_report'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//	sql = "CREATE TABLE [dbo].[warehouse_inout_daily_report]([id][bigint] IDENTITY(1, 1) NOT NULL,[report_date] [date] NOT NULL,[product_id] [varchar] (50) NOT NULL,[start_qty] [numeric] (18, 2) NOT NULL,[end_qty] [numeric] (18, 2) NOT NULL,"
				//		+ "[purchase_qty] [numeric] (18, 2) NOT NULL,[sale_qty] [numeric] (18, 2) NOT NULL,[overflow_qty] [numeric] (18, 2) NOT NULL,[loss_qty] [numeric] (18, 2) NOT NULL,"
				//		+ "[production_output] [numeric] (18, 2) NOT NULL,[production_input] [numeric] (18, 2) NOT NULL,[transfer_output] [numeric] (18, 2) NOT NULL,[transfer_input] [numeric] (18, 2) NOT NULL,"
				//		+ "CONSTRAINT[PK_warehouse_inout_daily_report] PRIMARY KEY CLUSTERED([id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]";
				//	SqlHelper.ExecuteNonQuery(sql);

				//	sql = "CREATE NONCLUSTERED INDEX [IX_warehouse_inout_daily_report] ON [dbo].[warehouse_inout_daily_report]"
				//		+ "([report_date] ASC,[product_id] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]";
				//	SqlHelper.ExecuteNonQuery(sql);
				//}

				//sql = "select count(1) from sys_task_list where task_code='OPA_058'";
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//	sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('OPA_058','Warehouse Daily Report',1,'WAREHOUSE','OPA_WAREHOUSE_DAILY_REPORT','OPA_WAREHOUSE_DAILY_REPORT','','SYSADMIN',getdate())";
				//	SqlHelper.ExecuteNonQuery(sql);
				//	sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "OPA_058", "SYSADMIN");
				//	SqlHelper.ExecuteNonQuery(sql);
				//	sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "OPA_058", "SYSADMIN");
				//	SqlHelper.ExecuteNonQuery(sql);
				//}

				//ver = "20170821";
				//sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				//iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				//if (iRet == 0)
				//{
				//	string title = ver + "日修改";
				//	StringBuilder sb = new StringBuilder();
				//	sb.Append("1. 增加仓库进销存日报表;  \r\n\r\n");
				//	sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
				//	SqlHelper.ExecuteNonQuery(sql);
				//}
				#endregion

				#region ver20180129
				sql = "select count(1) from sys_task_list where task_code='ACC_094'";
				iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				if (iRet == 0)
				{
					sql = "insert into sys_task_list(task_code,task_name,status,group_id,url,form_name,remark,update_user,update_time)values('ACC_094','Account Product Book',1,'ACCOUNT','ACC_PRODUCT_BOOK','ACC_PRODUCT_BOOK','','SYSADMIN',getdate())";
					SqlHelper.ExecuteNonQuery(sql);
					sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R001", "ACC_094", "SYSADMIN");
					SqlHelper.ExecuteNonQuery(sql);
					sql = string.Format("insert into sys_task_grant(role_id,task_code,update_user,update_time)values('{0}','{1}','{2}',getdate())", "R002", "ACC_094", "SYSADMIN");
					SqlHelper.ExecuteNonQuery(sql);
				}

				ver = "20180129";
				sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				if (iRet == 0)
				{
					string title = ver + "日修改";
					StringBuilder sb = new StringBuilder();
					sb.Append("1. 增加商品出入库帐本;  \r\n\r\n");
					sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
					SqlHelper.ExecuteNonQuery(sql);
				}

				#endregion

				#region ver20180828
				sql = "Select count(1) from SysColumns where Name='remark' and id=Object_id('acc_check_list')";
				iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				if (iRet == 0)
				{
					sql = "alter table acc_check_list add remark varchar(150) null";
					SqlHelper.ExecuteNonQuery(sql);
				}
				ver = "20180828";
				sql = string.Format("Select count(1) from sys_modify_log where ver='{0}'", ver);
				iRet = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
				if (iRet == 0)
				{
					string title = ver + "日修改";
					StringBuilder sb = new StringBuilder();
					sb.Append("1. 增加商品出入库帐本;  \r\n\r\n");
					sql = "insert into sys_modify_log(ver,title,modify_content,update_user,update_time)values('" + ver + "','" + title + "','" + sb.ToString() + "','SYSADMIN',getdate())";
					SqlHelper.ExecuteNonQuery(sql);
				}

				#endregion
				emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
            
        }

        private int GetNewDetailSeq(string accname)
        {
            string sql = string.Format("Select isnull(max(detail_seq),5000) + 1 from acc_credence_detail where acc_name='{0}' and seq=0 and detail_seq>=5001 and detail_seq<=5999", accname);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
        }

        private bool IsInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// change password
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool ChangePassword(string userid, string old_password, string new_password, out string emsg)
        {
            try
            {
                if (!Exists(userid, true, out emsg))
                {                    
                    return false;
                }
                if (!Login(userid, old_password, out emsg))
                {
                    emsg = "Password is incorrect";
                    return false;
                }
                string sql = string.Format("update sys_user_list set pwd='{0}' where user_id='{1}'", new_password, userid);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "OK";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }                    
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool ResetPassword(string userid, string new_password, out string emsg)
        {            
            try
            {
                string sql = string.Empty;
                if (!Exists(userid, true, out emsg))
                {                    
                    return false;
                }
                sql = string.Format("update sys_user_list set pwd='{0}' where user_id='{1}'", new_password, userid);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }                
            }
            catch(Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        private string Encrypt(string sData, string Key)
        {
            string EncryptedString = null;
            if (sData == null || Key == null) return "$#@$";
            byte[] m_nBox;
            DBUtility.Common.RC4Engine.GetKeyBytes(Key, out m_nBox);
            byte[] output;
            if (DBUtility.Common.RC4Engine.GetEncryptBytes(sData, m_nBox, out output))
            {
                // Convert data to hex-data
                EncryptedString = "";
                for (int i = 0; i < output.Length; i++)
                    EncryptedString += output[i].ToString("X2");
                return EncryptedString;
            }
            else
                return "$#@$";
        }

        /// <summary>
        /// init users password when it is null
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="password">password</param>
        /// <returns>true/false</returns>
        public bool InitPassword(out string emsg)
        {
            string sql;
            try
            {
                sql = "select user_id from sys_user_list where pwd is null";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        string PWD_MASK = "#4c#2@!";
                        string userid = rdr["user_id"].ToString();
                        string new_password = Encrypt(userid.Trim().ToString(), PWD_MASK);
                        sql = string.Format("update sys_user_list set pwd='{0}' where user_id='{1}'", new_password, userid);
                        //SqlParameter[] parms2 = SqlHelper.GetCachedParameters(sql);
                        SqlHelper.ExecuteNonQuery(sql);                        
                    }
                }
                emsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// get table record count
        /// <summary>
        ///<returns>get record count of UserList</returns>
        public int TotalRecords()
        {
            string sql = "Select count(1) from sys_user_list";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// insert a UserList
        /// <summary>
        /// <param name=UserList>model object of UserList</param>
        /// <returns>true/false</returns>
        public bool Insert(modUserList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_user_list(user_id,user_name,pwd,role_id,email_addr,update_user,update_time)values"
                                + "('{0}','{1}','{2}','{3}','{4}','{5}',GETDATE())", mod.UserId, mod.UserName, mod.Password, mod.RoleId, mod.Email, mod.UpdateUser);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }            
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// update a UserList
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=UserList>model object of UserList</param>
        /// <returns>true/false</returns>
        public bool Update(string userid, modUserList mod, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_user_list set user_name='{0}',status={1},role_id='{2}',update_user='{3}',email_addr='{4}', update_time=getdate() where user_id='{5}'", mod.UserName, mod.Status, mod.RoleId, mod.UpdateUser, mod.Email, userid);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }            
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// update a role of user
        /// <summary>
        /// <param name=userid>userid</param>
        /// <param name=RoleId>new role id</param>
        /// <returns>true/false</returns>
        public bool UpdateRole(string userid, string RoleId, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_user_list set role_id='{0}' where user_id='{1}'", RoleId, userid);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }            
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// delete a UserList
        /// <summary>
        /// <param name=userid>userid</param>
        /// <returns>true/false</returns>
        public bool Delete(string userid, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete sys_user_privilege where user_id='{0}'", userid);
                    SqlHelper.ExecuteNonQuery(sql);

                    sql = string.Format("delete sys_user_list where user_id='{0}'", userid);
                    SqlHelper.ExecuteNonQuery(sql);

                    transaction.Complete();//就这句就可以了。  
                    emsg = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        /// change UserList's status(valid/invalid)
        /// <summary>
        /// <param name=userid>userid</param>
        /// <returns>true/false</returns>
        public bool Inactive(string userid, out string emsg)
        {
            try
            {
                string sql = string.Format("update sys_user_list set status=1-status where user_id='{0}'", userid);
                int i = SqlHelper.ExecuteNonQuery(sql);
                if (i == 1)
                {
                    emsg = "";
                    return true;
                }
                else
                {
                    emsg = "Error when execute sql";
                    return false;
                }            
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// user exists
        /// </summary>
        /// <param name="userid">userid</param>
        /// <param name="onlyvalid">onlyvalid</param>
        /// <returns>true/false</returns>
        public bool Exists(string userid, bool onlyvalid, out string emsg)
        {
            try
            {
                emsg = "";
                string onlyvalidwhere = onlyvalid ? " and status=1" : string.Empty;
                string sql = string.Format("select count(*) from sys_user_list where user_id='{0}'" + onlyvalidwhere, userid);
                object o = SqlHelper.ExecuteScalar(sql);
                if (o.ToString().Trim() == "1")
                {
                    return true;
                }
                else
                {
                    if (onlyvalid)
                        emsg = "User does not exists or is a invalid user!";
                    else
                        emsg = "User does not exists!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return false;
            }
        }
    }
}
