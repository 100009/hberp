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
    public class dalQuotationForm
    {
        /// <summary>
        /// get all quotationform
        /// <summary>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all quotationform</returns>
        public BindingCollection<modQuotationForm> GetIList(string formidlist, string custlist, string custname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modQuotationForm> modellist = new BindingCollection<modQuotationForm>();
                //Execute a query to read the categories
                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formidlist.Replace(",", "','") + "') ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and a.cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and b.cust_name like '%" + custname + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.form_id,a.form_date,a.no,a.cust_id,b.cust_name,a.remark,a.contact_person,a.currency,a.update_user,a.update_time from quotation_form a inner join customer_list b on a.cust_id=b.cust_id where 1=1 "
                    + formidwhere + cusidtwhere + custnamewhere + formdatewhere + " order by form_id desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modQuotationForm model = new modQuotationForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ContactPerson = dalUtility.ConvertToString(rdr["contact_person"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.DetailCount = DetailCount(model.FormId);
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
        /// get all quotationform
        /// <summary>
        /// <param name=currentPage>currentPage</param>
        /// <param name=pagesize>pagesize</param>
        /// <param name=custname>custname</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all quotationform</returns>
        public BindingCollection<modQuotationForm> GetIList(int currentPage, int pagesize, string custname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modQuotationForm> modellist = new BindingCollection<modQuotationForm>();
                //Execute a query to read the categories                
                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and b.cust_name like '%" + custname + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                int startindex = (currentPage - 1) * pagesize + 1;
                int endindex = currentPage * pagesize;
                string sql = "select row_number() over(order by a.form_id desc) as rn,a.form_id,a.form_date,a.no,a.cust_id,b.cust_name,a.remark,a.contact_person,a.currency,a.update_user,a.update_time from quotation_form a inner join customer_list b on a.cust_id=b.cust_id where 1=1 "
                    + custnamewhere + formdatewhere;

                string sql2 = "select count(1) from (" + sql + ") t";
                sql = string.Format("select * from (" + sql + ") t where rn>='{0}' and rn<='{1}'", startindex, endindex);
                
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modQuotationForm model = new modQuotationForm();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.ContactPerson = dalUtility.ConvertToString(rdr["contact_person"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.DetailCount = DetailCount(model.FormId);
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
        /// get table record
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of quotationform</returns>
        public modQuotationForm GetItem(string formid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.form_id,a.form_date,a.no,a.cust_id,b.cust_name,a.remark,a.contact_person,a.currency,a.update_user,a.update_time from quotation_form a inner join customer_list b on a.cust_id=b.cust_id where a.form_id='{0}' order by a.form_id", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modQuotationForm model = new modQuotationForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.CustId=dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.ContactPerson = dalUtility.ConvertToString(rdr["contact_person"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.DetailCount = DetailCount(model.FormId);
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
        /// get all quotationdetail
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all quotationdetail</returns>
        public BindingCollection<modQuotationDetail> GetDetail(string formid, out string emsg)
        {
            try
            {
                BindingCollection<modQuotationDetail> modellist = new BindingCollection<modQuotationDetail>();
                //Execute a query to read the categories
                string sql = string.Format("select form_id,seq,product_id,product_name,specify,brand,unit_no,qty,price,remark from quotation_detail where form_id='{0}' order by form_id,seq", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modQuotationDetail model = new modQuotationDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Mny = model.Qty * model.Price;
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
        /// get all quotationdetail
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all quotationdetail</returns>
        public BindingCollection<modVQuotationDetail> GetVDetail(string custlist, string custname, string productidlist, string productname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modVQuotationDetail> modellist = new BindingCollection<modVQuotationDetail>();
                //Execute a query to read the categories
                string productidwhere = string.Empty;
                if (!string.IsNullOrEmpty(productidlist) && productidlist.CompareTo("ALL") != 0)
                    productidwhere = "and b.product_id in ('" + productidlist.Replace(",", "','") + "') ";

                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and b.product_name like '%" + productname + "%' ";

                string cusidtwhere = string.Empty;
                if (!string.IsNullOrEmpty(custlist) && custlist.CompareTo("ALL") != 0)
                    cusidtwhere = "and a.cust_id in ('" + custlist.Replace(",", "','") + "') ";

                string custnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(custname))
                    custnamewhere = "and c.cust_name like '%" + custname + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.form_id,a.form_date,a.no,c.cust_id,c.cust_name,b.seq,b.product_id,b.product_name,b.specify,b.brand,b.size,b.unit_no,b.qty,b.price,b.remark,a.currency "
                        + "from quotation_form a inner join quotation_detail b on a.form_id=b.form_id inner join customer_list c on a.cust_id=c.cust_id "
                        + "where 1=1 " + cusidtwhere + custnamewhere + productidwhere + productnamewhere + formdatewhere +" order by b.product_id,a.form_date";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVQuotationDetail model = new modVQuotationDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Mny = model.Qty * model.Price;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
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
        /// <param name=formid>formid</param>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of quotationform</returns>
        public modVQuotationDetail GetDetailItem(string formid, int seq, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql =string.Format("select a.form_id,a.form_date,a.no,c.cust_id,c.cust_name,b.seq,b.product_id,b.product_name,b.specify,b.brand,b.size,b.unit_no,b.qty,b.price,b.remark,a.currency "
                        + "from quotation_form a inner join quotation_detail b on a.form_id=b.form_id inner join customer_list c on a.cust_id=c.cust_id "
                        + "where a.form_id='{0}' and b.seq={1}", formid, seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modVQuotationDetail model = new modVQuotationDetail();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.CustId = dalUtility.ConvertToString(rdr["cust_id"]);
                        model.CustName = dalUtility.ConvertToString(rdr["cust_name"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);
                        model.Mny = model.Qty * model.Price;
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        emsg = string.Empty;
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
        /// get new form id
        /// <summary>
        /// <returns>string</returns>
        public string GetNewId(DateTime formdate)
        {
            string temp = formdate.ToString("yyyyMM");
            string formid = "QT" + temp + "-";

            string sql = "select max(form_id) from quotation_form where form_id like '" + formid + "%' ";
            object ret = SqlHelper.ExecuteScalar(sql);
            if (ret != null && !string.IsNullOrEmpty(ret.ToString()))
            {
                int no = Convert.ToInt32(ret.ToString().Replace(formid, "").Trim()) + 1;
                formid += no.ToString().PadLeft(4, '0');
            }
            else
            {
                formid += "0001";
            }
            return formid;
        }

        /// <summary>
        /// insert a salesshipment
        /// <summary>
        /// <param name=mod>model object of salesshipment</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modQuotationForm mod, BindingCollection<modQuotationDetail> list, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string formid = mod.FormId;
                    string formno = mod.No;
                    string content = string.Empty;
                    string salesman = string.Empty;
                    string actioncode = "QUOTATION";

                    dalCustomerList dalcust = new dalCustomerList();
                    salesman = dalcust.GetSalesMan(mod.CustId);
                    
                    dalCustomerScoreRule dalcsr = new dalCustomerScoreRule();
                    modCustomerScoreRule modcsr = dalcsr.GetItem(actioncode, out emsg);
                    int? seq = 0;
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            if (Exists(formid, out emsg))
                                formid = GetNewId(mod.FormDate);

                            sql = string.Format("insert into quotation_form(form_id,form_date,no,cust_id,remark,currency,contact_person,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',getdate())", formid, mod.FormDate, mod.No, mod.CustId, mod.Remark, mod.Currency, mod.ContactPerson, mod.UpdateUser);
                            SqlHelper.ExecuteNonQuery(sql);
                            if (list != null && list.Count > 0)
                            {
                                seq = 0;
                                foreach (modQuotationDetail modd in list)
                                {
                                    seq++;
                                    sql = string.Format("insert into quotation_detail(form_id,seq,product_id,product_name,specify,brand,unit_no,qty,price,remark,size)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},'{9}', 1)", formid, seq, modd.ProductId, modd.ProductName, modd.Specify, modd.Brand, modd.UnitNo, modd.Qty, modd.Price, modd.Remark);
                                    SqlHelper.ExecuteNonQuery(sql);
                                    if (string.IsNullOrEmpty(content))
                                        content = "产品:" + modd.ProductName + "  规格:" + modd.Specify + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo;
                                    else
                                        content += "\r\n产品:" + modd.ProductName + "  规格:" + modd.Specify + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo;
                                }
                            }
                            sql = string.Format("insert into customer_log(cust_id,cust_name,action_code,action_type,action_man,form_id,action_subject,action_content,object_name,venue,from_time,to_time,scores,ad_flag,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}',getdate())", mod.CustId, mod.CustName, actioncode, "客户报价", salesman, formid, string.Empty, content, string.Empty, string.Empty, mod.FormDate, mod.FormDate, modcsr.Scores, 1, mod.UpdateUser);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update quotation_form set form_date='{0}',no='{1}',cust_id='{2}',remark='{3}',currency='{4}',contact_person='{5}' where form_id='{6}'", mod.FormDate, mod.No, mod.CustId, mod.Remark, mod.Currency, mod.ContactPerson, formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete quotation_detail where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modQuotationDetail modd in list)
                            {
                                seq++;
                                sql = string.Format("insert into quotation_detail(form_id,seq,product_id,product_name,specify,brand,unit_no,qty,price,remark,size)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},'{9}', 1)", formid, seq, modd.ProductId, modd.ProductName, modd.Specify, modd.Brand, modd.UnitNo, modd.Qty, modd.Price, modd.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                                if (string.IsNullOrEmpty(content))
                                    content = "产品:" + modd.ProductName + "  规格:" + modd.Specify + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo;
                                else
                                    content += "\r\n产品:" + modd.ProductName + "  规格:" + modd.Specify + "  价格:" + modd.Price.ToString() + "/" + modd.UnitNo;
                            }

                            sql = string.Format("update customer_log set cust_id='{0}',cust_name='{1}',action_type='{2}',action_man='{3}',action_subject='{4}',action_content='{5}',object_name='{6}',venue='{7}',from_time='{8}',to_time='{9}',scores={10},ad_flag={11},update_user='{12}',update_time=getdate() where action_code='{13}' and form_id='{14}' ", mod.CustId, mod.CustName, "客户报价", salesman, string.Empty, content, string.Empty, string.Empty, mod.FormDate, mod.FormDate, modcsr.Scores, 1, mod.UpdateUser, actioncode, formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete customer_log where action_code='{0}' and form_id='{1}'", actioncode, mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete quotation_detail where form_id='{0}'", mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete quotation_form where form_id='{0}'", mod.FormId);
                            SqlHelper.ExecuteNonQuery(sql);
                            break;                        
                    }

                    transaction.Complete();//就这句就可以了。  
                    emsg = formid;
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
        /// record exist or not
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string formid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from quotation_form where form_id='{0}' ",formid);
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
        /// detail exist or not
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public int DetailCount(string formid)
        {
            try
            {
                string sql = string.Format("select count(1) from quotation_detail where form_id='{0}' ", formid);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(sql));                
            }
            catch
            {                
                return 0;
            }
        }
    }
}
