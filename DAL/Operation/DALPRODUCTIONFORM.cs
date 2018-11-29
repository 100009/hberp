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
    public class dalProductionForm
    {
        /// <summary>
        /// get production summary
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=formidlist>formidlist</param>
        /// <param name=formtypelist>formtypelist</param>
        /// <param name=deptlist>deptlist</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionform</returns>
        public BindingCollection<modProductionSummary> GetProductionSummary(string statuslist, string formidlist, string formtypelist, string deptlist, string nolist, string deptid, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modProductionSummary> modellist = new BindingCollection<modProductionSummary>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formidlist.Replace(",", "','") + "') ";

                string formtypewhere = string.Empty;
                if (!string.IsNullOrEmpty(formtypelist) && formtypelist.CompareTo("ALL") != 0)
                    formtypewhere = "and a.form_type in ('" + formtypelist.Replace(",", "','") + "') ";

                string deptwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptlist) && deptlist.CompareTo("ALL") != 0)
                    deptwhere = "and a.dept_id in ('" + deptlist.Replace(",", "','") + "') ";

                string nolistwhere = string.Empty;
                if (!string.IsNullOrEmpty(nolist) && nolist.CompareTo("ALL") != 0)
                    nolistwhere = "and a.no in ('" + nolist.Replace(",", "','") + "') ";

                string deptidwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptid) && deptid.CompareTo("ALL") != 0)
                    deptidwhere = "and a.dept_id like '" + deptid + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                decimal processmny = 0;
                decimal sumdetail = 0;
                decimal sumkill = 0;
                decimal sumother = 0;
                string sql = "select form_type,dept_id,currency,sum(process_mny) process_mny,sum(detail_sum) detail_sum, sum(kill_mny) kill_mny, sum(other_mny) other_mny from production_form a where 1=1 "
                        + statuswhere + formidwhere + formtypewhere + deptwhere + nolistwhere + deptidwhere + formdatewhere + "group by form_type,dept_id,currency order by form_type,dept_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionSummary model = new modProductionSummary();                        
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.DeptId = dalUtility.ConvertToString(rdr["dept_id"]);
                        model.ProcessMny = dalUtility.ConvertToDecimal(rdr["process_mny"]);
                        model.DetailSum = dalUtility.ConvertToDecimal(rdr["detail_sum"]);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);                        
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        processmny += model.ProcessMny;
                        sumdetail += model.DetailSum;
                        sumkill += model.KillMny;
                        sumother += model.OtherMny;
                        modellist.Add(model);
                    }
                }
                modProductionSummary modsum = new modProductionSummary();
                modsum.FormType = "合计";
                modsum.DeptId = "合计";
                modsum.ProcessMny = processmny;
                modsum.DetailSum = sumdetail;
                modsum.KillMny = sumkill;
                modsum.OtherMny = sumother;
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
        /// get production form list
        /// <summary>
        /// <param name=statuslist>statuslist</param>
        /// <param name=formidlist>formidlist</param>
        /// <param name=formtypelist>formtypelist</param>
        /// <param name=deptlist>deptlist</param>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionform</returns>
        public BindingCollection<modProductionForm> GetIList(string statuslist, string formidlist, string formtypelist, string deptlist, string nolist, string deptid, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modProductionForm> modellist = new BindingCollection<modProductionForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string formidwhere = string.Empty;
                if (!string.IsNullOrEmpty(formidlist) && formidlist.CompareTo("ALL") != 0)
                    formidwhere = "and a.form_id in ('" + formidlist.Replace(",", "','") + "') ";

                string formtypewhere = string.Empty;
                if (!string.IsNullOrEmpty(formtypelist) && formtypelist.CompareTo("ALL") != 0)
                    formtypewhere = "and a.form_type in ('" + formtypelist.Replace(",", "','") + "') ";

                string deptwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptlist) && deptlist.CompareTo("ALL") != 0)
                    deptwhere = "and a.dept_id in ('" + deptlist.Replace(",", "','") + "') ";

                string nolistwhere = string.Empty;
                if (!string.IsNullOrEmpty(nolist) && nolist.CompareTo("ALL") != 0)
                    nolistwhere = "and a.no in ('" + nolist.Replace(",", "','") + "') ";

                string deptidwhere = string.Empty;
                if (!string.IsNullOrEmpty(deptid) && deptid.CompareTo("ALL") != 0)
                    deptidwhere = "and a.dept_id like '" + deptid + "%' ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select form_id,status,price_status,form_date,require_date,form_type,dept_id,no,process_mny,detail_sum,kill_mny,other_mny,other_reason,ship_man,remark,currency,exchange_rate,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from production_form a where 1=1 " + statuswhere + formidwhere + formtypewhere + deptwhere + nolistwhere + deptidwhere + formdatewhere + "order by form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionForm model = new modProductionForm();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.DeptId = dalUtility.ConvertToString(rdr["dept_id"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ProcessMny = dalUtility.ConvertToDecimal(rdr["process_mny"]);
                        model.MaterialMny = GetMaterialMny(model.FormId);
                        model.WareMny = GetWareMny(model.FormId);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
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
        /// get all productionform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionform</returns>
        public BindingCollection<modProductionForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modProductionForm> modellist = new BindingCollection<modProductionForm>();
                //Execute a query to read the categories
                string sql = string.Format("select form_id,status,price_status,form_date,require_date,form_type,dept_id,no,process_mny,detail_sum,kill_mny,other_mny,other_reason,ship_man,remark,currency,exchange_rate,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from production_form where acc_name='{0}' and acc_seq={1} order by form_id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionForm model = new modProductionForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.ProcessMny = dalUtility.ConvertToDecimal(rdr["process_mny"]);
                        model.MaterialMny = GetMaterialMny(model.FormId);
                        model.WareMny = GetWareMny(model.FormId);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
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
        /// get table record
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productionform</returns>
        public modProductionForm GetItem(string formid,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select form_id,status,price_status,form_date,require_date,form_type,dept_id,no,process_mny,detail_sum,kill_mny,other_mny,other_reason,ship_man,remark,currency,exchange_rate,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from production_form where form_id='{0}' order by form_id", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductionForm model = new modProductionForm();
                        model.FormId=dalUtility.ConvertToString(rdr["form_id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.RequireDate = dalUtility.ConvertToDateTime(rdr["require_date"]);
                        model.FormType=dalUtility.ConvertToString(rdr["form_type"]);
                        model.DeptId=dalUtility.ConvertToString(rdr["dept_id"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.ProcessMny = dalUtility.ConvertToDecimal(rdr["process_mny"]);
                        model.MaterialMny = GetMaterialMny(model.FormId);
                        model.WareMny = GetWareMny(model.FormId);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
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

        public decimal GetMaterialMny(string formid)
        {
            string sql = string.Format("select sum(size*qty*cost_price) from production_form_material where form_id='{0}'", formid);
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null)
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }
        public decimal GetWareMny(string formid)
        {
            string sql = string.Format("select sum(size*qty*cost_price) from production_form_ware where form_id='{0}'", formid);
            object obj = SqlHelper.ExecuteScalar(sql);
            if (obj != null)
                return decimal.Parse(obj.ToString());
            else
                return 0;
        }
        /// <summary>
        /// get form for credence list
        /// <summary>
        /// <param name=fromdate>fromdate</param>
        /// <param name=todate>todate</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionform</returns>
        public BindingCollection<modProductionForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modProductionForm> modellist = new BindingCollection<modProductionForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select form_id,status,price_status,form_date,require_date,form_type,dept_id,no,process_mny,detail_sum,kill_mny,other_mny,other_reason,ship_man,remark,currency,exchange_rate,update_user,update_time,audit_man,audit_time,acc_name,acc_seq "
                        + "from production_form a where a.status=1 and a.price_status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + "order by form_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionForm model = new modProductionForm();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.PriceStatus = dalUtility.ConvertToInt(rdr["price_status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.FormType = dalUtility.ConvertToString(rdr["form_type"]);
                        model.DeptId = dalUtility.ConvertToString(rdr["dept_id"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ProcessMny = dalUtility.ConvertToDecimal(rdr["process_mny"]);
                        model.MaterialMny = GetMaterialMny(model.FormId);
                        model.WareMny = GetWareMny(model.FormId);
                        model.KillMny = dalUtility.ConvertToDecimal(rdr["kill_mny"]);
                        model.OtherMny = dalUtility.ConvertToDecimal(rdr["other_mny"]);
                        model.OtherReason = dalUtility.ConvertToString(rdr["other_reason"]);
                        model.ShipMan = dalUtility.ConvertToString(rdr["ship_man"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
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
        /// get all productionformware
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionformware</returns>
        public BindingCollection<modProductionFormWare> GetProductionFormWare(string formid, out string emsg)
        {
            try
            {
                BindingCollection<modProductionFormWare> modellist = new BindingCollection<modProductionFormWare>();
                //Execute a query to read the categories
                string sql = string.Format("select form_id,seq,product_id,product_name,specify,size,qty,process_price,cost_price,warehouse_id,remark from production_form_ware where form_id='{0}' order by form_id,seq", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionFormWare model = new modProductionFormWare();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.ProcessPrice = dalUtility.ConvertToDecimal(rdr["process_price"]);
                        model.CostPrice = dalUtility.ConvertToDecimal(rdr["cost_price"]);
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
        /// get all productionformmaterial
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productionformmaterial</returns>
        public BindingCollection<modProductionFormMaterial> GetProductionFormMaterial(string formid, out string emsg)
        {
            try
            {
                BindingCollection<modProductionFormMaterial> modellist = new BindingCollection<modProductionFormMaterial>();
                //Execute a query to read the categories
                string sql = string.Format("select form_id,seq,product_id,product_name,specify,size,qty,cost_price,warehouse_id,remark from production_form_material where form_id='{0}' order by form_id,seq", formid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductionFormMaterial model = new modProductionFormMaterial();
                        model.FormId = dalUtility.ConvertToString(rdr["form_id"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Size = dalUtility.ConvertToDecimal(rdr["size"]);
                        model.Qty = dalUtility.ConvertToDecimal(rdr["qty"]);
                        model.CostPrice = dalUtility.ConvertToDecimal(rdr["cost_price"]);
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
        public string GetNewFormId(DateTime formdate)
        {
            string temp = formdate.ToString("yyyyMM");
            string formid = "PD" + temp + "-";
            string sql = "select max(form_id) from production_form where form_id like '" + formid + "%' ";
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
        /// insert a production form
        /// <summary>
        /// <param name=mod>model object of production form</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(string oprtype, modProductionForm mod, BindingCollection<modProductionFormWare> listware, BindingCollection<modProductionFormMaterial> listmaterial, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    string formid = mod.FormId;
                    int? seq = 0;
                    switch (oprtype)
                    {
                        case "ADD":
                        case "NEW":
                            if (Exists(formid, out emsg))
                                formid = GetNewFormId(mod.FormDate);
                            sql = string.Format("insert into production_form(form_id,status,price_status,form_date,form_type,dept_id,no,currency,exchange_rate,detail_sum,kill_mny,other_mny,other_reason,remark,ship_man,update_user,update_time,require_date,process_mny)values('{0}',{1},{2},'{3}','{4}','{5}','{6}','{7}',{8},{9},{10},{11},'{12}','{13}','{14}','{15}',getdate(),'{16}',{17})", formid, 0, 0, mod.FormDate, mod.FormType, mod.DeptId, mod.No, mod.Currency, mod.ExchangeRate, mod.MaterialMny, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.ShipMan, mod.UpdateUser, mod.RequireDate, mod.ProcessMny);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modProductionFormWare modware in listware)
                            {
                                seq++;
                                sql = string.Format("insert into production_form_ware(form_id,seq,product_id,product_name,specify,size,qty,process_price,cost_price,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", formid, seq, modware.ProductId, modware.ProductName, modware.Specify, modware.Size, modware.Qty, modware.ProcessPrice, modware.CostPrice, modware.WarehouseId, modware.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            seq = 0;
                            foreach (modProductionFormMaterial modmaterial in listmaterial)
                            {
                                seq++;
                                sql = string.Format("insert into production_form_material(form_id,seq,product_id,product_name,specify,size,qty,cost_price,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", formid, seq, modmaterial.ProductId, modmaterial.ProductName, modmaterial.Specify, modmaterial.Size, modmaterial.Qty, modmaterial.CostPrice, modmaterial.WarehouseId, modmaterial.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "EDIT":
                        case "UPDATE":
                        case "MODIFY":
                            sql = string.Format("update production_form set status={0},price_status={1},form_date='{2}',form_type='{3}',dept_id='{4}',no='{5}',currency='{6}',exchange_rate={7},detail_sum={8},kill_mny={9},other_mny={10},other_reason='{11}',remark='{12}',ship_man='{13}',require_date='{14}',process_mny={15} where form_id='{16}'", 0, 0, mod.FormDate, mod.FormType, mod.DeptId, mod.No, mod.Currency, mod.ExchangeRate, mod.MaterialMny, mod.KillMny, mod.OtherMny, mod.OtherReason, mod.Remark, mod.ShipMan, mod.RequireDate, mod.ProcessMny, formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete production_form_ware where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete production_form_material where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            seq = 0;
                            foreach (modProductionFormWare modware in listware)
                            {
                                seq++;
                                sql = string.Format("insert into production_form_ware(form_id,seq,product_id,product_name,specify,size,qty,process_price,cost_price,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", formid, seq, modware.ProductId, modware.ProductName, modware.Specify, modware.Size, modware.Qty, modware.ProcessPrice, modware.CostPrice, modware.WarehouseId, modware.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            seq = 0;
                            foreach (modProductionFormMaterial modmaterial in listmaterial)
                            {
                                seq++;
                                sql = string.Format("insert into production_form_material(form_id,seq,product_id,product_name,specify,size,qty,cost_price,warehouse_id,remark)values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", formid, seq, modmaterial.ProductId, modmaterial.ProductName, modmaterial.Specify, modmaterial.Size, modmaterial.Qty, modmaterial.CostPrice, modmaterial.WarehouseId, modmaterial.Remark);
                                SqlHelper.ExecuteNonQuery(sql);
                            }
                            break;
                        case "DEL":
                        case "DELETE":
                            sql = string.Format("delete production_form_ware where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete production_form_material where form_id='{0}'", formid);
                            SqlHelper.ExecuteNonQuery(sql);
                            sql = string.Format("delete production_form where form_id='{0}'", formid);
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
        /// update price of production form
        /// <summary>
        /// <param name=mod>model object of production form</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool UpdatePrice(BindingCollection<modProductionFormWare> listware, BindingCollection<modProductionFormMaterial> listmaterial, decimal detailsum, decimal processmny, string updateuser, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Empty;
                    foreach (modProductionFormWare modware in listware)
                    {
                        sql = string.Format("update production_form_ware set cost_price={0} where form_id='{1}' and seq='{2}'", modware.CostPrice, modware.FormId, modware.Seq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    foreach (modProductionFormMaterial modmaterial in listmaterial)
                    {
                        sql = string.Format("update production_form_material set cost_price={0} where form_id='{1}' and seq='{2}'", modmaterial.CostPrice, modmaterial.FormId, modmaterial.Seq);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    sql = string.Format("update production_form set detail_sum={0},process_mny={1},price_status={2},audit_man='{3}',audit_time=getdate() where form_id='{4}'", detailsum, processmny, 1, updateuser, listware[0].FormId);
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
        /// audit production form
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Audit(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modProductionForm mod = GetItem(formid, out emsg);
                if (mod.Status == 1)
                {
                    emsg = "这张单据已经审核,您无须再审";
                    return false;
                }
                BindingCollection<modProductionFormWare> listware = GetProductionFormWare(formid, out emsg);
                if (listware != null && listware.Count > 0)
                {
                    foreach (modProductionFormWare modware in listware)
                    {
                        sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", modware.WarehouseId, modware.ProductId, modware.Size, formid, mod.FormType, mod.No, mod.FormDate, 0, modware.Qty, 0, modware.Remark, updateuser);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                    }
                }
                BindingCollection<modProductionFormMaterial> listmaterial = GetProductionFormMaterial(formid, out emsg);
                if (listmaterial != null && listmaterial.Count > 0)
                {
                    foreach (modProductionFormMaterial modmaterial in listmaterial)
                    {
                        sql = string.Format("insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',getdate())", modmaterial.WarehouseId, modmaterial.ProductId, modmaterial.Size, formid, mod.FormType, mod.No, mod.FormDate, 0, 0, modmaterial.Qty, modmaterial.Remark, updateuser);
                        SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                        cmd.ExecuteNonQuery();
                    }
                }
                sql = string.Format("update production_form set status={0},audit_man='{1}',audit_time=getdate() where form_id='{2}'", 1, updateuser, formid);
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
        /// reset production form
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Reset(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modProductionForm mod = GetItem(formid, out emsg);
                if (mod.Status == 0)
                {
                    emsg = "这张单据尚未审核,您无须重置";
                    return false;
                }
                sql = string.Format("delete warehouse_product_inout where form_id='{0}' and form_type='{1}'", formid, mod.FormType);
                SqlHelper.PrepareCommand(cmd, conn, trans, CommandType.Text, sql, null);
                cmd.ExecuteNonQuery();
                sql = string.Format("update production_form set status={0},audit_man='{1}',audit_time=null where form_id='{2}'", 0, updateuser, formid);
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
        /// reset production form
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=updateuser>updateuser</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ResetPrice(string formid, string updateuser, out string emsg)
        {
            string sql = string.Empty;
            SqlConnection conn = SqlHelper.getConn();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            try
            {
                modProductionForm mod = GetItem(formid, out emsg);
                sql = string.Format("update production_form set price_status={0} where form_id='{2}'", 0, updateuser, formid);
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
        /// record exist or not
        /// <summary>
        /// <param name=formid>formid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string formid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from production_form where form_id='{0}' ",formid);
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
