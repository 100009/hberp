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
    public class dalAccExpenseForm
    {
        /// <summary>
        /// get all accexpensesummary
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accexpenseform</returns>
        public BindingCollection<modAccExpenseSummary> GetExpenseSummary(string statuslist, string idlist, string expenseidlist, string expensemanlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseSummary> modellist = new BindingCollection<modAccExpenseSummary>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string expenseidwhere = string.Empty;
                if (!string.IsNullOrEmpty(expenseidlist) && expenseidlist.CompareTo("ALL") != 0)
                    expenseidwhere = "and a.expense_id in ('" + expenseidlist.Replace(",", "','") + "') ";

                string expensemanwhere = string.Empty;
                if (!string.IsNullOrEmpty(expensemanlist) && expensemanlist.CompareTo("ALL") != 0)
                    expensemanwhere = "and a.expense_man in ('" + expensemanlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.expense_id,a.expense_name,a.expense_man,sum(a.exchange_rate*a.expense_mny) expense_mny from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id "
                        + "where 1=1 " + statuswhere + idwhere + expenseidwhere + expensemanwhere + formdatewhere + " group by a.expense_id,a.expense_name,a.expense_man order by a.expense_man,a.expense_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseSummary model = new modAccExpenseSummary();
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId = dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName = dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);                        
                        model.ExpenseMny = dalUtility.ConvertToDecimal(rdr["expense_mny"]);                        
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
        /// get all accexpenseform
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accexpenseform</returns>
        public BindingCollection<modAccExpenseForm> GetIList(string statuslist, string idlist, string expenseidlist, string expensemanlist, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseForm> modellist = new BindingCollection<modAccExpenseForm>();
                //Execute a query to read the categories
                string statuswhere = string.Empty;
                if (!string.IsNullOrEmpty(statuslist) && statuslist.CompareTo("ALL") != 0)
                    statuswhere = "and a.status in ('" + statuslist.Replace(",", "','") + "') ";

                string idwhere = string.Empty;
                if (!string.IsNullOrEmpty(idlist) && idlist.CompareTo("ALL") != 0)
                    idwhere = "and a.id in ('" + idlist.Replace(",", "','") + "') ";

                string expenseidwhere = string.Empty;
                if (!string.IsNullOrEmpty(expenseidlist) && expenseidlist.CompareTo("ALL") != 0)
                    expenseidwhere = "and a.expense_id in ('" + expenseidlist.Replace(",", "','") + "') ";

                string expensemanwhere = string.Empty;
                if (!string.IsNullOrEmpty(expensemanlist) && expensemanlist.CompareTo("ALL") != 0)
                    expensemanwhere = "and a.expense_man in ('" + expensemanlist.Replace(",", "','") + "') ";

                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.id,a.status,a.form_date,a.no,a.expense_id,a.expense_name,a.expense_man,a.currency,a.exchange_rate,a.expense_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where 1=1 " + statuswhere + idwhere + expenseidwhere + expensemanwhere + formdatewhere + " order by id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseForm model = new modAccExpenseForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId=dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName=dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.ExpenseMny=dalUtility.ConvertToDecimal(rdr["expense_mny"]);                        
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName=dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
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

        private string GetExpenseType(string expenseid)
        {
            string sql = string.Format("select subject_name from acc_subject_list where subject_id =(select psubject_id from acc_subject_list where subject_id='{0}')", expenseid);
            return SqlHelper.ExecuteScalar(sql).ToString();
        }

        /// <summary>
        /// get all accexpenseform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accexpenseform</returns>
        public BindingCollection<modAccExpenseForm> GetIList(string accname, int? accseq, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseForm> modellist = new BindingCollection<modAccExpenseForm>();
                //Execute a query to read the categories
                //string sql = string.Format("select id,status,form_date,no,expense_id,expense_name,currency,exchange_rate,expense_mny,subject_id,detail_id,detail_name,check_no,check_type,bank_name,promise_date,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from acc_expense_form where acc_name='{0}' and acc_seq={1} order by id",accname,accseq);
                string sql = string.Format("select a.id,a.status,a.form_date,a.no,a.expense_id,a.expense_name,a.expense_man,a.currency,a.exchange_rate,a.expense_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.acc_name='{0}' and a.acc_seq={1} order by a.id", accname, accseq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseForm model = new modAccExpenseForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId=dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName=dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.ExpenseMny=dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName=dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        /// get all accexpenseform
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=accseq>accseq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accexpenseform</returns>
        public BindingCollection<modAccExpenseForm> GetIList(string accname, string expenseid, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseForm> modellist = new BindingCollection<modAccExpenseForm>();
                //Execute a query to read the categories
                //string sql = string.Format("select id,status,form_date,no,expense_id,expense_name,currency,exchange_rate,expense_mny,subject_id,detail_id,detail_name,check_no,check_type,bank_name,promise_date,remark,update_user,update_time,audit_man,audit_time,acc_name,acc_seq from acc_expense_form where acc_name='{0}' and acc_seq={1} order by id",accname,accseq);
                string sql = string.Format("select a.id,a.status,a.form_date,a.no,a.expense_id,a.expense_name,a.expense_man,a.currency,a.exchange_rate,a.expense_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.acc_name='{0}' and a.acc_seq={1} order by a.id", accname, expenseid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseForm model = new modAccExpenseForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId = dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName = dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.ExpenseMny = dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        /// get all accexpenseform
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accexpenseform</returns>
        public BindingCollection<modAccExpenseColumn> GetExpenseColumn(string accname, string expenseid, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseColumn> modellist = new BindingCollection<modAccExpenseColumn>();
				//Execute a query to read the categories
				//          string sql =@"select a.form_date,a.expense_id,a.expense_name,a.detail_name,a.exchange_rate*a.expense_mny expense_mny,a.acc_name,a.acc_seq 
				//                  from acc_expense_form a where a.acc_name=@acc_name and a.expense_id like @expense_id  
				//union all select b.credence_date,'91353070' expense_id,'折旧费' expense_name,asset_name detail_name,a.depre_mny expense_mny,a.acc_name,b.seq 
				//from asset_depre_list a inner join acc_credence_list b on a.acc_name=b.acc_name where a.acc_name=@acc_name and b.credence_type='资产折旧' 
				//and a.depre_mny!=0 order by acc_name,acc_seq,expense_id";
				string sql = @"select b.credence_date,a.subject_id,c.subject_name,a.digest,round((a.borrow_money-a.lend_money)*a.exchange_rate,2) expense_mny,a.acc_name,a.seq 
                        from acc_credence_detail a, acc_credence_list b,acc_subject_list c where a.subject_id=c.subject_id and a.acc_name=b.acc_name and a.seq=b.seq 
						and b.status=1 and a.seq>0 and a.acc_name=@acc_name and a.subject_id like @subject_id 
						order by acc_name,seq,subject_id";
				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname),
					new SqlParameter("subject_id", expenseid + '%')
				};
				
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    DateTime formDate = DateTime.Today;
                    string expenseId = string.Empty;
                    int accSeq = -1;
                    while (rdr.Read())
                    {
                        if(string.IsNullOrEmpty(expenseId))
                        {
                            formDate = dalUtility.ConvertToDateTime(rdr["credence_date"]);
                            expenseId = dalUtility.ConvertToString(rdr["subject_id"]);
                            accSeq = dalUtility.ConvertToInt(rdr["seq"]);
                            modAccExpenseColumn model = new modAccExpenseColumn();
                            model.FormDate = dalUtility.ConvertToDateTime(rdr["credence_date"]);
                            model.ExpenseId = dalUtility.ConvertToString(rdr["subject_id"]);
                            model.ExpenseName = dalUtility.ConvertToString(rdr["subject_name"]);
                            model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                            model.ExpenseMny = dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                            modellist.Add(model);
                        }
                        else
                        {
                            if(formDate == dalUtility.ConvertToDateTime(rdr["credence_date"]) && 
                                expenseId == dalUtility.ConvertToString(rdr["subject_id"]) &&
                                accSeq == dalUtility.ConvertToInt(rdr["seq"]))
                            {
                                modellist[modellist.Count - 1].Digest += "\r\n" + dalUtility.ConvertToString(rdr["digest"]);
                                modellist[modellist.Count - 1].ExpenseMny += dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                            }
                            else
                            {
                                modAccExpenseColumn model = new modAccExpenseColumn();
                                model.FormDate = dalUtility.ConvertToDateTime(rdr["credence_date"]);
                                model.ExpenseId = dalUtility.ConvertToString(rdr["subject_id"]);
                                model.ExpenseName = dalUtility.ConvertToString(rdr["subject_name"]);
                                model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                                model.ExpenseMny = dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                                model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                                model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                                modellist.Add(model);
                                formDate = dalUtility.ConvertToDateTime(rdr["credence_date"]);
                                expenseId = dalUtility.ConvertToString(rdr["subject_id"]);
                                accSeq = dalUtility.ConvertToInt(rdr["seq"]);
                            }
                        }
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
        ///<returns>get a record detail of accexpenseform</returns>
        public modAccExpenseForm GetItem(int id, out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.id,a.status,a.form_date,a.no,a.expense_id,a.expense_name,a.expense_man,a.currency,a.exchange_rate,a.expense_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.id = {0} order by a.id", id);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccExpenseForm model = new modAccExpenseForm();
                        model.Id=dalUtility.ConvertToInt(rdr["id"]);
                        model.Status=dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate=dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No=dalUtility.ConvertToString(rdr["no"]);
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId=dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName=dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate=dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.ExpenseMny=dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                        model.SubjectId=dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId=dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName=dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo=dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType=dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate=dalUtility.ConvertToDateTime(rdr["promise_date"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
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
        public BindingCollection<modAccExpenseForm> GetWaitCredenceList(string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseForm> modellist = new BindingCollection<modAccExpenseForm>();
                //Execute a query to read the categories
                string formdatewhere = string.Empty;
                if (!string.IsNullOrEmpty(fromdate))
                    formdatewhere = "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' ";
                if (!string.IsNullOrEmpty(todate))
                    formdatewhere += "and a.form_date <= '" + Convert.ToDateTime(todate) + "' ";

                string sql = "select a.id,a.status,a.form_date,a.no,a.expense_id,a.expense_name,a.expense_man,a.currency,a.exchange_rate,a.expense_mny,a.subject_id,b.subject_name,a.detail_id,a.detail_name,a.check_no,a.check_type,a.bank_name,a.promise_date,a.remark,a.update_user,a.update_time,a.audit_man,a.audit_time,a.acc_name,a.acc_seq "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where a.status=1 and (a.acc_name is null or a.acc_name='') and a.acc_seq=0 " + formdatewhere + " order by a.id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseForm model = new modAccExpenseForm();
                        model.Id = dalUtility.ConvertToInt(rdr["id"]);
                        model.Status = dalUtility.ConvertToInt(rdr["status"]);
                        model.FormDate = dalUtility.ConvertToDateTime(rdr["form_date"]);
                        model.No = dalUtility.ConvertToString(rdr["no"]);
                        model.ExpenseType = GetExpenseType(rdr["expense_id"].ToString());
                        model.ExpenseId = dalUtility.ConvertToString(rdr["expense_id"]);
                        model.ExpenseName = dalUtility.ConvertToString(rdr["expense_name"]);
                        model.ExpenseMan = dalUtility.ConvertToString(rdr["expense_man"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.ExpenseMny = dalUtility.ConvertToDecimal(rdr["expense_mny"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.CheckNo = dalUtility.ConvertToString(rdr["check_no"]);
                        model.CheckType = dalUtility.ConvertToString(rdr["check_type"]);
                        model.BankName = dalUtility.ConvertToString(rdr["bank_name"]);
                        model.PromiseDate = dalUtility.ConvertToDateTime(rdr["promise_date"]);
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
        /// insert a accexpenseform
        /// <summary>
        /// <param name=mod>model object of accexpenseform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccExpenseForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_expense_form(status,form_date,no,expense_id,expense_name,currency,exchange_rate,expense_mny,subject_id,detail_id,detail_name,check_no,check_type,bank_name,promise_date,remark,expense_man,update_user,update_time)values({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',getdate())", mod.Status, mod.FormDate, mod.No, mod.ExpenseId, mod.ExpenseName, mod.Currency, mod.ExchangeRate, mod.ExpenseMny, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, mod.Remark, mod.ExpenseMan, mod.UpdateUser);
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
        /// update a accexpenseform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=mod>model object of accexpenseform</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(int? id,modAccExpenseForm mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_expense_form set status={0},form_date='{1}',no='{2}',expense_id='{3}',expense_name='{4}',currency='{5}',exchange_rate={6},expense_mny={7},subject_id='{8}',detail_id='{9}',detail_name='{10}',check_no='{11}',check_type='{12}',bank_name='{13}',promise_date='{14}',remark='{15}',expense_man='{16}' where id={17}", mod.Status, mod.FormDate, mod.No, mod.ExpenseId, mod.ExpenseName, mod.Currency, mod.ExchangeRate, mod.ExpenseMny, mod.SubjectId, mod.DetailId, mod.DetailName, mod.CheckNo, mod.CheckType, mod.BankName, mod.PromiseDate, mod.Remark, mod.ExpenseMan, id);
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
        /// delete a accexpenseform
        /// <summary>
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(int? id,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_expense_form where id={0} ",id);
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
        /// audit
        /// <summary>
        /// <param name=id>id</param>
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
                sql = string.Format("update acc_expense_form set status={0},audit_man='{1}',audit_time=getdate() where id={2}", 1, updateuser, id);
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
        /// reset
        /// <summary>
        /// <param name=id>id</param>
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
                sql = string.Format("update acc_expense_form set status={0},audit_man='{1}',audit_time=null where id={2}", 0, updateuser, id);
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
        /// <param name=id>id</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(int? id, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_expense_form where id={0} ",id);
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
