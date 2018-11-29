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
    public class dalVendorList
    {        
        /// <summary>
        /// get all vendorlist
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=storelist>storelist</param>
        /// <param name=vendortype>vendortype</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendorlist</returns>
        public BindingCollection<modVendorList> GetIList(string statuslist, string vendortype, out string emsg)
        {
            try
            {
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string vendortypewhere = string.Empty;
                if (!string.IsNullOrEmpty(vendortype))
                    vendortypewhere = "and a.vendor_type = '"+ vendortype +"' ";
                BindingCollection<modVendorList> modellist = new BindingCollection<modVendorList>();
                //Execute a query to read the categories
                string sql = "select a.vendor_name,a.vendor_type,a.status,a.currency,a.no,a.linkman,a.tel,a.fax,a.addr,a.remark,a.need_invoice,a.update_user,a.update_time "
                        + "from vendor_list a where 1=1 " + statuswhere + vendortypewhere + " order by a.vendor_name";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorList model = new modVendorList();
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorType = dalUtility.ConvertToString(rdr["vendor_type"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
        /// get all vendorlist
        /// <summary>
        /// <param name=currentPage>currentPage</param>
        /// <param name=pagesize>pagesize</param>
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all vendorlist</returns>
        public BindingCollection<modVendorList> GetIList(int currentPage, int pagesize, string vendorname, out string emsg)
        {
            try
            {
                string vendornamewhere = string.Empty;
                if (!string.IsNullOrEmpty(vendorname))
                    vendornamewhere = "and product_name like '" + vendorname + "%' ";

                int startindex = (currentPage - 1) * pagesize + 1;
                int endindex = currentPage * pagesize;
                BindingCollection<modVendorList> modellist = new BindingCollection<modVendorList>();
                //Execute a query to read the categories
                string sql = "select row_number() over(order by vendor_name) as rn,a.vendor_name,a.vendor_type,a.status,a.currency,a.no,a.linkman,a.tel,a.fax,a.addr,a.remark,a.need_invoice,a.update_user,a.update_time "
                        + "from vendor_list a where 1=1 " + vendornamewhere ;
                string sql2 = "select count(1) from (" + sql + ") t";
                sql = string.Format("select * from (" + sql + ") t where rn>='{0}' and rn<='{1}'", startindex, endindex);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modVendorList model = new modVendorList();
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorType = dalUtility.ConvertToString(rdr["vendor_type"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
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
        /// get table record
        /// <summary>
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of vendorlist</returns>
        public modVendorList GetItem(string vendorname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.vendor_name,a.vendor_type,a.status,a.currency,a.no,a.linkman,a.tel,a.fax,a.addr,a.remark,a.need_invoice,a.update_user,a.update_time "
                        + "from vendor_list a where 1=1 and a.vendor_name='{0}' order by vendor_name", vendorname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modVendorList model = new modVendorList();
                        model.VendorName = dalUtility.ConvertToString(rdr["vendor_name"]);
                        model.VendorType = dalUtility.ConvertToString(rdr["vendor_type"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.Linkman = dalUtility.ConvertToString(rdr["linkman"]);
                        model.Tel = dalUtility.ConvertToString(rdr["tel"]);
                        model.Fax = dalUtility.ConvertToString(rdr["fax"]);
                        model.Addr = dalUtility.ConvertToString(rdr["addr"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.NeedInvoice = dalUtility.ConvertToInt(rdr["need_invoice"]);
                        model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
                        model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
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
        /// get table record count
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get record count of vendorlist</returns>
        public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from vendor_list";
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
        /// insert a vendorlist
        /// <summary>
        /// <param name=mod>model object of vendorlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modVendorList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into vendor_list(vendor_name,vendor_type,status,linkman,tel,fax,addr,remark,update_user,update_time,start_time,need_invoice,no,currency"
                        + ")values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}',getdate(),getdate(),{9},'{10}','{11}')", mod.VendorName, mod.VendorType, mod.Status, mod.Linkman, mod.Tel, mod.Fax, mod.Addr, mod.Remark, mod.UpdateUser, mod.NeedInvoice, mod.No, mod.Currency);
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
        /// update a vendorlist
        /// <summary>
        /// <param name=vendorname>vendorname</param>
        /// <param name=mod>model object of vendorlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string vendorname,modVendorList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update vendor_list set vendor_type='{0}',status={1},linkman='{2}',tel='{3}',fax='{4}',addr='{5}',remark='{6}',update_user='{7}',update_time=getdate(), need_invoice={8},no='{9}' where vendor_name='{10}'", mod.VendorType, mod.Status, mod.Linkman, mod.Tel, mod.Fax, mod.Addr, mod.Remark, mod.UpdateUser, mod.NeedInvoice, mod.No, vendorname);
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
        /// delete a vendorlist
        /// <summary>
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string vendorname,out string emsg)
        {
            try
            {
                string sql = string.Format("delete vendor_list where vendor_name='{0}'",vendorname);
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
        /// change vendorlist's status(valid/invalid)
        /// <summary>
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string vendorname,out string emsg)
        {
            try
            {
                string sql = string.Format("update vendor_list set status=1-status where vendor_name='{0}'",vendorname);
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
        /// <param name=vendorname>vendorname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string vendorname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from vendor_list where vendor_name='{0}'",vendorname);
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
