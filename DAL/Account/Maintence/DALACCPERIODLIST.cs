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
    public class dalAccPeriodList
    {
        /// <summary>
        /// get all accperiodlist
        /// <summary>
        /// <param name=accyear>accyear</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accperiodlist</returns>
        public BindingCollection<modAccPeriodList> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccPeriodList> modellist = new BindingCollection<modAccPeriodList>();
                //Execute a query to read the categories
                string sql = "select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list order by seq desc";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccPeriodList model = new modAccPeriodList();
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccYear=dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth=dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.StartDate=dalUtility.ConvertToDateTime(rdr["start_date"]);
                        model.EndDate=dalUtility.ConvertToDateTime(rdr["end_date"]);
                        model.CostFlag=dalUtility.ConvertToInt(rdr["cost_flag"]);
                        model.LockFlag=dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.EmployeeCount=dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get all accperiodlist
		/// <summary>
		/// <param name=accyear>accyear</param>
		/// <param name=out emsg>return error message</param>
		///<returns>details of all accperiodlist</returns>
		public BindingCollection<modAccPeriodList> GetEndList(string startAccName, out string emsg)
		{
			try
			{
				BindingCollection<modAccPeriodList> modellist = new BindingCollection<modAccPeriodList>();
				//Execute a query to read the categories
				string sql = @"select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time 
						from acc_period_list where start_date>(select end_date from acc_period_list where acc_name=@start_accName) order by seq desc";
				SqlParameter[] parms =
				{
					new SqlParameter("start_accName", startAccName)
				};

				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
				{
					while (rdr.Read())
					{
						modAccPeriodList model = new modAccPeriodList();
						model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
						model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
						model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
						model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
						model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
						model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
						model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
						model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
						model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get table record
		/// <summary>
		/// <param name=accname>accname</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of accperiodlist</returns>
		public modAccPeriodList GetItem(string accname,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list where acc_name='{0}' order by acc_name",accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccPeriodList model = new modAccPeriodList();
                        model.AccName=dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq=dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccYear=dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth=dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.StartDate=dalUtility.ConvertToDateTime(rdr["start_date"]);
                        model.EndDate=dalUtility.ConvertToDateTime(rdr["end_date"]);
                        model.CostFlag=dalUtility.ConvertToInt(rdr["cost_flag"]);
                        model.LockFlag=dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.EmployeeCount=dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get table record
		/// <summary>
		/// <param name=accname>accname</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of accperiodlist</returns>
		public modAccPeriodList GetDuringItem(DateTime duringDate, out string emsg)
		{
			try
			{
				//Execute a query to read the categories
				string sql = "select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time "
					+ "from acc_period_list where start_date<=@duringDate and end_date>=@duringDate order by acc_name";
				SqlParameter[] parm = {
					new SqlParameter("duringDate", SqlDbType.DateTime)
				};
				parm[0].Value = duringDate;

				using (SqlDataReader rdr = SqlHelper.ExecuteReader(CommandType.Text, sql, parm))
				{
					if (rdr.Read())
					{
						modAccPeriodList model = new modAccPeriodList();
						model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
						model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
						model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
						model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
						model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
						model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
						model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
						model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
						model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
						model.UpdateUser = dalUtility.ConvertToString(rdr["update_user"]);
						model.UpdateTime = dalUtility.ConvertToDateTime(rdr["update_time"]);
						emsg = null;
						return model;
					}
					else
					{
						emsg = "未找到财务区间数据！";
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
		/// get table record
		/// <summary>
		/// <param name=seq>seq</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of accperiodlist</returns>
		public modAccPeriodList GetItem(int seq, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list where seq={0} order by acc_name", seq);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccPeriodList model = new modAccPeriodList();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
                        model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
                        model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
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
        /// get table record
        /// <summary>
        /// <param name=seq>seq</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accperiodlist</returns>
        public modAccPeriodList GetYearStartItem(string accname, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list "
                    + "where seq=(select min(seq) from acc_period_list where acc_year = (select acc_year from acc_period_list where acc_name = '{0}'))", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccPeriodList model = new modAccPeriodList();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
                        model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
                        model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
                        model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
                        model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
                        model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
                        model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get table record
		/// <summary>
		/// <param name=seq>seq</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of accperiodlist</returns>
		public modAccPeriodList GetLastYearEndItem(string accname, out string emsg)
		{
			try
			{
				//Execute a query to read the categories
				string sql = string.Format("select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list "
					+ "where seq=(select max(seq) from acc_period_list where acc_year = ((select acc_year from acc_period_list where acc_name = '{0}'))-1)", accname);
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
				{
					if (rdr.Read())
					{
						modAccPeriodList model = new modAccPeriodList();
						model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
						model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
						model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
						model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
						model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
						model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
						model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
						model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
						model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get table record
		/// <summary>
		/// <param name=seq>seq</param>
		/// <param name=out emsg>return error message</param>
		///<returns>get a record detail of accperiodlist</returns>
		public modAccPeriodList GetFirstItem(out string emsg)
		{
			try
			{
				//Execute a query to read the categories
				string sql = "select acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time from acc_period_list "
					+ "where seq=(select min(seq) from acc_period_list)";
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
				{
					if (rdr.Read())
					{
						modAccPeriodList model = new modAccPeriodList();
						model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
						model.Seq = dalUtility.ConvertToInt(rdr["seq"]);
						model.AccYear = dalUtility.ConvertToInt(rdr["acc_year"]);
						model.AccMonth = dalUtility.ConvertToInt(rdr["acc_month"]);
						model.StartDate = dalUtility.ConvertToDateTime(rdr["start_date"]);
						model.EndDate = dalUtility.ConvertToDateTime(rdr["end_date"]);
						model.CostFlag = dalUtility.ConvertToInt(rdr["cost_flag"]);
						model.LockFlag = dalUtility.ConvertToInt(rdr["lock_flag"]);
						model.EmployeeCount = dalUtility.ConvertToInt(rdr["employee_count"]);
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
		/// get table record count
		/// <summary>
		/// <param name=out emsg>return error message</param>
		///<returns>get record count of accperiodlist</returns>
		public int TotalRecords(out string emsg)
        {
            try
            {
                string sql = "select count(1) from acc_period_list";
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
        /// save init receivable data
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=startdate>startdate</param>
        /// <param name=currency>currency</param>
        /// <param name=exchangerate>exchangerate</param>
        /// <param name=list>list of modStartReceivable</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccPeriodList mod, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = "select count(1) from acc_period_list where lock_flag=0";
                    int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (count >= 1)
                    {
                        emsg = "前一个财务区间还未结算，不能创建新的财务区间!";
                        return false;
                    }
                    
                    sql = "select isnull(max(seq),0) +1 from acc_period_list";
                    int seq = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if(seq > 1)
                    {
                        sql = "select count(1) from acc_period_list where end_date='" + mod.StartDate.AddDays(-1) + "'";
                        count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                        if (count <= 0)
                        {
                            emsg = "前一个财务区间与当前财务区间的日期不连续!";
                            return false;
                        }
                    }
                    sql = string.Format("insert into acc_period_list(acc_name,seq,acc_year,acc_month,start_date,end_date,cost_flag,lock_flag,employee_count,update_user,update_time)values('{0}',{1},{2},{3},'{4}','{5}',{6},{7},{8},'{9}',getdate())", mod.AccName, seq, mod.AccYear, mod.AccMonth, mod.StartDate, mod.EndDate, mod.CostFlag, mod.LockFlag, mod.EmployeeCount, mod.UpdateUser);
                    SqlHelper.ExecuteNonQuery(sql);

                    if (seq == 1)
                    {
                        sql = string.Format("delete acc_credence_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_credence_list(acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time)values('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',getdate())", mod.AccName, 0, 1, "期初数据", "初", mod.StartDate, 1, "期初设定", mod.UpdateUser);
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                    else
                    {                        
                        sql = string.Format("delete acc_credence_detail where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("delete acc_credence_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        modAccPeriodList modpre = GetItem(seq - 1, out emsg);
                        sql = string.Format("insert into acc_credence_list(acc_name,seq,status,credence_type,credence_word,credence_date,attach_count,remark,update_user,update_time)values('{0}',{1},{2},'{3}','{4}','{5}',{6},'{7}','{8}',getdate())", mod.AccName, 0, 1, "上月结存", "初", mod.StartDate, 1, "上月结存", mod.UpdateUser);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',cust_id,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_receivable_list where acc_name='{3}' group by cust_id,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',vendor_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_payable_list where acc_name='{3}' group by vendor_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',object_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_other_receivable where acc_name='{3}' group by object_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',object_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_other_payable where acc_name='{3}' group by object_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_qty2,start_mny,start_mny2,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',product_id,size,sum(start_qty+input_qty-output_qty) start_qty,sum(start_qty+input_qty-output_qty) start_qty2,sum(start_mny+input_mny-output_mny) start_mny,sum(start_mny+input_mny-output_mny) start_mny2,0,0,0,0,'0','上月结存','','{2}',getdate() from acc_product_inout "
                                + "where acc_name='{3}' group by product_id,size having (sum(start_qty+input_qty-output_qty)<>0 or sum(start_mny+input_mny-output_mny)<>0) ", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete warehouse_product_inout where form_type='{0}' and inout_date='{1}'", "上月结存", mod.StartDate);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
                            + "select warehouse_id,product_id,size,0,'上月结存','','"+ mod.StartDate +"',sum(start_qty+input_qty-output_qty),0,0,'','"+ mod.UpdateUser +"',getdate() "
                            + "from warehouse_product_inout where inout_date>='" + modpre.StartDate + "' and inout_date<='" + modpre.EndDate + "' group by warehouse_id,product_id,size";
                        SqlHelper.ExecuteNonQuery(sql);
                                                
                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency) "
                                + "select '{0}',0,row_number() over(order by subject_id) as detail_seq,'上月结存',subject_id,subject_name,detail_id,'' detail_name,sum(borrow_money),sum(lend_money),exchange_rate,zcfz_flag,ad_flag,currency "
                                + "from acc_credence_detail where acc_name='{1}' and subject_id='1030' group by subject_id,subject_name,detail_id,exchange_rate,zcfz_flag,ad_flag,currency", mod.AccName, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency) "
                                + "select '{0}',0,(row_number() over(order by subject_id)+800) as detail_seq,'上月结存',subject_id,subject_name,'' detail_id,'' detail_name,sum(borrow_money),sum(lend_money),exchange_rate,zcfz_flag,ad_flag,currency "
                                + "from acc_credence_detail where acc_name='{1}' and subject_id<>'1030' group by subject_id,subject_name,exchange_rate,zcfz_flag,ad_flag,currency", mod.AccName, modpre.AccName);
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

        public bool RebuildStartData(string accname, string subjectid, out string emsg)
        {
            try
            {
                string sql=string.Empty;
                modAccPeriodList mod = GetItem(accname, out emsg);
                modAccPeriodList modpre = GetItem(mod.Seq - 1, out emsg);
                switch (subjectid)
                {
                    case "1030":   //现金银行
                        sql = string.Format("delete acc_credence_detail where acc_name='{0}' and subject_id='1030' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_credence_detail(acc_name,seq,detail_seq,digest,subject_id,subject_name,detail_id,detail_name,borrow_money,lend_money,exchange_rate,zcfz_flag,ad_flag,currency) "
                                + "select '{0}',0,row_number() over(order by subject_id) as detail_seq,'上月结存',subject_id,subject_name,detail_id,'' detail_name,sum(borrow_money),sum(lend_money),exchange_rate,zcfz_flag,ad_flag,currency "
                                + "from acc_credence_detail where acc_name='{1}' and subject_id='1030' group by subject_id,subject_name,detail_id,exchange_rate,zcfz_flag,ad_flag,currency", mod.AccName, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    case "1235":   //库存商品
                        sql = string.Format("delete acc_product_inout where acc_name='{0}' and acc_seq=0", accname);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_product_inout(acc_name,acc_seq,acc_date,product_id,size,start_qty,start_qty2,start_mny,start_mny2,input_qty,input_mny,output_qty,output_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',product_id,size,sum(start_qty+input_qty-output_qty) start_qty,sum(start_qty+input_qty-output_qty) start_qty2,sum(start_mny+input_mny-output_mny) start_mny,sum(start_mny+input_mny-output_mny) start_mny2,0,0,0,0,'0','上月结存','','{2}',getdate() from acc_product_inout "
                                + "where acc_name='{3}' group by product_id,size having (sum(start_qty+input_qty-output_qty)<>0 or sum(start_mny+input_mny-output_mny)<>0) ", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);

                        sql = string.Format("delete warehouse_product_inout where form_type='{0}' and inout_date='{1}'", "上月结存", mod.StartDate);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = "insert into warehouse_product_inout(warehouse_id,product_id,size,form_id,form_type,inv_no,inout_date,start_qty,input_qty,output_qty,remark,update_user,update_time) "
                            + "select warehouse_id,product_id,size,0,'上月结存','','"+ mod.StartDate +"',sum(start_qty+input_qty-output_qty),0,0,'','"+ mod.UpdateUser +"',getdate() "
                            + "from warehouse_product_inout where inout_date>='" + modpre.StartDate + "' and inout_date<='" + modpre.EndDate + "' group by warehouse_id,product_id,size";
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    case "1055":   //应收帐款
                        sql = string.Format("delete acc_receivable_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_receivable_list(acc_name,seq,acc_date,cust_id,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',cust_id,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_receivable_list where acc_name='{3}' group by cust_id,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    case "5145":   //应付账款
                        sql = string.Format("delete acc_payable_list where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_payable_list(acc_name,seq,acc_date,vendor_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',vendor_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_payable_list where acc_name='{3}' group by vendor_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    case "1060":   //其它应收款
                        sql = string.Format("delete acc_other_receivable where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_other_receivable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',object_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_other_receivable where acc_name='{3}' group by object_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    case "5155":   //其他应付款
                        sql = string.Format("delete acc_other_payable where acc_name='{0}' and seq=0", mod.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        sql = string.Format("insert into acc_other_payable(acc_name,seq,acc_date,object_name,currency,exchange_rate,start_mny,adding_mny,paid_mny,form_id,form_type,remark,update_user,update_time) "
                                + "select '{0}',0,'{1}',object_name,currency,exchange_rate,sum(start_mny+adding_mny-paid_mny) start_mny,0,0,'0','上月结存','','{2}',getdate() from acc_other_payable where acc_name='{3}' group by object_name,currency,exchange_rate", mod.AccName, mod.StartDate, mod.UpdateUser, modpre.AccName);
                        SqlHelper.ExecuteNonQuery(sql);
                        break;
                    default:
                        emsg = "无法重建该科目的期初数据";
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// update a accperiodlist
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=mod>model object of accperiodlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string accname,modAccPeriodList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_period_list set employee_count={0},update_user='{1}',update_time=getdate() where acc_name='{2}'",mod.EmployeeCount,mod.UpdateUser,accname);
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
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_period_list where acc_name='{0}' ",accname);
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
