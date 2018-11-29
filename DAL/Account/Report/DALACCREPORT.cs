using System;
using System.Collections.ObjectModel;
using BindingCollection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LXMS.DBUtility;
using LXMS.Model;
using System.Collections.Generic;

namespace LXMS.DAL
{
    public class dalAccReport
    {
        public static List<modAccSubjectList> staticSubjectList = new List<modAccSubjectList>();
        public static List<modSubjectBalance> staticYearSubjectBalance = new List<modSubjectBalance>();
        public static List<modSubjectBalance> staticEndSubjectBalance = new List<modSubjectBalance>();
        
        public List<modSubjectBalance> GetSubjectBalance(string accname, bool onlyStart, bool isTrialBalance, out string emsg)
        {
            try
            {
                string sql = string.Empty;

                string detailtablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string listtablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";
                string startWhere = onlyStart ? "and a.seq=0 " : "";

				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname)
				};
				sql = "select a.subject_id,c.psubject_id,c.ad_flag,isnull(sum(a.zcfz_flag*(a.borrow_money-a.lend_money)*a.exchange_rate),0) balance "
                        + "from " + detailtablename + " a inner join " + listtablename + " b on a.acc_name=b.acc_name and a.seq=b.seq inner join acc_subject_list c on a.subject_id=c.subject_id "
                        + "where a.acc_name=@acc_name and b.status=1 " + startWhere + " group by a.subject_id,c.psubject_id,c.ad_flag";

                List<modSubjectBalance> modellist = new List<modSubjectBalance>();
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    while (rdr.Read())
                    {
                        modSubjectBalance model = new modSubjectBalance();
                        //model.AccName = accname;
                        model.SubjectId = rdr["subject_id"].ToString();
                        //model.SubjectName = rdr["subject_name"].ToString();
                        model.PSubjectId = rdr["psubject_id"].ToString();
                        model.AdFlag = Convert.ToInt32(rdr["ad_flag"]);
                        model.Text = model.AdFlag * decimal.Parse(rdr["balance"].ToString());
                        model.Value = decimal.Parse(rdr["balance"].ToString());
                        modellist.Add(model);
                    }
                }
                //modellist.AddRange(GetPSubjectBalance(accname, modellist, true));
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
        
        public void GetSubjectMnyByLinq(string subjectid, ref decimal startmny, ref decimal endmny, out string emsg)
        {
            emsg = string.Empty;
            var curItem = staticSubjectList.Where(c => c.SubjectId == subjectid).First();
            if (curItem.HasChildren == 0)
            {
                if (curItem.AdFlag != 0)
                {
                    startmny += staticYearSubjectBalance.Where(c => c.SubjectId == subjectid).Sum(c => c.Value);
                    endmny += staticEndSubjectBalance.Where(c => c.SubjectId == subjectid).Sum(c => c.Value);
                }                
            }
            else
            {
				if (subjectid == "9135")    //本年利润 利润结转会直接用这个科目
				{
					startmny += staticYearSubjectBalance.Where(c => c.SubjectId == subjectid).Sum(c => c.Value);
					endmny += staticEndSubjectBalance.Where(c => c.SubjectId == subjectid).Sum(c => c.Value);
				}
				var subItems = staticSubjectList.Where(c => c.PSubjectId == subjectid).ToList();
                foreach(modAccSubjectList item in subItems)                
                {
                    GetSubjectMnyByLinq(item.SubjectId, ref startmny, ref endmny, out emsg);
                };                  
            }
        }
        /// <summary>
        /// get asset debt report
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public void GetAccAssetDebtReport(string accname, string subjectid, bool isTrialBalance, ref List<modAccAssetDebtReport> list,out string emsg)
        {
            List<modAccAssetDebtReport> modellist = new List<modAccAssetDebtReport>();

            dalAccSubjectList dalsubject = new dalAccSubjectList();
            string sql = string.Empty;
            decimal startMny = 0;
            decimal endMny = 0;
            if (subjectid == "1")
            {                
                var items = staticSubjectList.Where(c => c.SubjectId.Substring(0,1).CompareTo("5") < 0).ToList();
                foreach (modAccSubjectList item in items)
                {
                    modAccAssetDebtReport model = new modAccAssetDebtReport();
                    model.AccName = accname;
                    model.SubjectId = item.SubjectId;
                    //model.SubjectName = dalsubject.GetLeftBlank(model.SubjectId) + item.SubjectName;
                    model.SubjectName = item.SubjectName;
                    model.PSubjectId = item.PSubjectId;
                    model.HasChildren = item.HasChildren;
                    model.AdFlag = item.AdFlag;
                    startMny = 0;
                    endMny = 0;
                    if (model.AdFlag != 0)
                    {
                        GetSubjectMnyByLinq(model.SubjectId, ref startMny, ref endMny, out emsg);
                        startMny = model.AdFlag * startMny;
                        endMny= model.AdFlag * endMny;
                    }
                    else
                    {
                        startMny = staticYearSubjectBalance.Where(c => c.SubjectId == model.SubjectId).Sum(c => c.Value);
                        endMny = staticEndSubjectBalance.Where(c => c.SubjectId == model.SubjectId).Sum(c => c.Value);
                    }
                    model.YearStartMny = string.Format("{0:C2}", startMny);
                    model.EndMny = string.Format("{0:C2}", endMny);
                    modellist.Add(model);
                };
            }
            else if (subjectid == "5")
            {
                var items = staticSubjectList.Where(c => c.SubjectId.Substring(0,1).CompareTo("5") >= 0).ToList();
                foreach (modAccSubjectList item in items)
                {
                    modAccAssetDebtReport model = new modAccAssetDebtReport();
                    model.AccName = accname;
                    model.SubjectId = item.SubjectId;
                    //model.SubjectName = dalsubject.GetLeftBlank(model.SubjectId) + item.SubjectName;
                    model.SubjectName = item.SubjectName;
                    model.PSubjectId = item.PSubjectId;
                    model.HasChildren = item.HasChildren;
                    model.AdFlag = item.AdFlag;
                    startMny = 0;
                    endMny = 0;
                    if (model.AdFlag != 0)
                    {
                        GetSubjectMnyByLinq(model.SubjectId, ref startMny, ref endMny, out emsg);
                        startMny = model.AdFlag * startMny;
                        endMny = model.AdFlag * endMny;
                    }
                    else
                    {
                        startMny = staticYearSubjectBalance.Where(c => c.SubjectId == model.SubjectId).Sum(c => c.Value);
                        endMny = staticEndSubjectBalance.Where(c => c.SubjectId == model.SubjectId).Sum(c => c.Value);
                    }
                    model.YearStartMny = string.Format("{0:C2}", startMny);
                    model.EndMny = string.Format("{0:C2}", endMny);
                    modellist.Add(model);
                };
            }
            emsg = string.Empty;
            list.AddRange(modellist);
        }

        public void GetSubjectMny(string accname, string subjectid, bool isTrialBalance, ref decimal startmny, ref decimal endmny, out string emsg)
        {
            emsg = string.Empty;
            string sql = string.Empty;
            dalAccSubjectList dal = new dalAccSubjectList();
            modAccSubjectList mod = dal.GetItem(subjectid, out emsg);
            if (mod.HasChildren == 0)
            {
				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname),
					new SqlParameter("subject_id", subjectid)
				};

				if (isTrialBalance)
                {
					sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate*abs(ad_flag)),0) 
							from acc_trial_credence_detail a inner join acc_trial_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and a.seq=0 and b.acc_name=@acc_name and a.subject_id=@subject_id";					
					startmny += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));

                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate*abs(ad_flag)),0) 
							from acc_trial_credence_detail a inner join acc_trial_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    endmny += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));
                }
                else
                {
                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate*abs(ad_flag)),0) 
							from acc_credence_detail a inner join acc_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and a.seq=0 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    startmny += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));

                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate*abs(ad_flag)),0) 
							from acc_credence_detail a inner join acc_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    endmny += Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));
                }
            }
            else
            {
                sql = "select subject_id from acc_subject_list where psubject_id=@psubject_id";
				SqlParameter[] parms = {
					new SqlParameter("psubject_id", accname)
				};
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    while (rdr.Read())
                    {
                        GetSubjectMny(accname, rdr["subject_id"].ToString(), isTrialBalance, ref startmny, ref endmny, out emsg);
                    }
                }
            }
        }

        public void GetOwnMny(string accname, string subjectid, bool isTrialBalance, ref decimal startmny, ref decimal endmny, out string emsg)
        {
            emsg = string.Empty;
            string sql = string.Empty;
            dalAccSubjectList dal = new dalAccSubjectList();
            modAccSubjectList mod = dal.GetItem(subjectid, out emsg);
            if (mod.HasChildren == 0)
            {
				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname),
					new SqlParameter("subject_id", subjectid)
				};
				if (isTrialBalance)
                {
                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate),0) 
							from acc_trial_credence_detail a inner join acc_trial_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and a.seq=0 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    startmny = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));

                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate),0) 
							from acc_trial_credence_detail a inner join acc_trial_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    endmny = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));
                }
                else
                {
                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate),0) 
							from acc_credence_detail a inner join acc_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and a.seq=0 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    startmny = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));

                    sql = @"select isnull(sum(zcfz_flag*(borrow_money-lend_money)*exchange_rate),0) 
							from acc_credence_detail a inner join acc_credence_list b on a.acc_name=b.acc_name and a.seq=b.seq 
							where b.status=1 and b.acc_name=@acc_name and a.subject_id=@subject_id";
                    endmny = Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parms));
                }
            }            
        }

        /// <summary>
        /// get profit report
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccProfitReport> GetAccProfitReport(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccProfitReport> modellist = new BindingCollection<modAccProfitReport>();
                //Execute a query to read the categories                
                string sql = string.Empty;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                decimal monthsum = 0;
                decimal yearsum = 0;
				
				sql = "select subject_id,subject_name, ad_flag, has_children from acc_subject_list where (psubject_id='9135') order by subject_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccProfitReport model = new modAccProfitReport();
                        model.AccName = accname;
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = rdr["subject_name"].ToString();
												
						if (rdr["has_children"].ToString().Trim() == "0")
                        {
							SqlParameter[] parmM = {
								new SqlParameter("acc_name", accname),
								new SqlParameter("subject_id", model.SubjectId)
							};
							SqlParameter[] parmsY = {
								new SqlParameter("acc_year", modp.AccYear),
								new SqlParameter("subject_id", model.SubjectId),
								new SqlParameter("acc_seq", modp.Seq)
							};
							if (isTrialBalance)
                            {
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_trial_credence_detail a,acc_trial_credence_list b
										where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name=@acc_name and a.seq>0 and a.subject_id=@subject_id";
                                model.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmM)) * Convert.ToDecimal(rdr["ad_flag"]), 2);

								sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_trial_credence_detail a,acc_trial_credence_list b 
										where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name in (select acc_name from acc_period_list where acc_year=@acc_year and seq<=@acc_seq) 
										and a.seq>0 and a.subject_id=@subject_id";
                                model.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmsY)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                            }
                            else
                            {
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_credence_detail a,acc_credence_list b 
										where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name=@acc_name and a.seq>0 and a.subject_id=@subject_id";
                                model.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmM)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_credence_detail a,acc_credence_list b  
										where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name in (select acc_name from acc_period_list where acc_year=@acc_year and seq<=@acc_seq) 
										and a.seq>0 and a.subject_id=@subject_id";
                                model.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmsY)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                            }
                        }
                        else
                        {
							SqlParameter[] parmM = {
								new SqlParameter("acc_name", accname),
								new SqlParameter("subject_id", model.SubjectId+"%")
							};
							SqlParameter[] parmsY = {
								new SqlParameter("acc_year", modp.AccYear),
								new SqlParameter("subject_id", model.SubjectId+"%"),
								new SqlParameter("acc_seq", modp.Seq)
							};
							if (isTrialBalance)
                            {
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_trial_credence_detail a,acc_trial_credence_list b
									where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name=@acc_name and a.seq>0 and a.subject_id like @subject_id";
                                model.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmM)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_trial_credence_detail a,acc_trial_credence_list b
									where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name in (select acc_name from acc_period_list where acc_year=@acc_year and seq<=@acc_seq) 
									and a.seq>0 and a.subject_id like @subject_id";
                                model.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmsY)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                            }
                            else
                            {
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_credence_detail a,acc_credence_list b 
									where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name=@acc_name and a.seq>0 and a.subject_id like @subject_id";
                                model.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmM)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                                sql = @"select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from acc_credence_detail a,acc_credence_list b 
									where a.acc_name=b.acc_name and a.seq=b.seq and b.status=1 and a.acc_name in (select acc_name from acc_period_list where acc_year=@acc_year and seq<=@acc_seq) 
									and a.seq>0 and a.subject_id like @subject_id";
                                model.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql, parmsY)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                            }
                        }
                        model.AdFlag = Convert.ToInt32(rdr["ad_flag"]);
                        modellist.Add(model);
                        monthsum += model.ThisMonth * Convert.ToDecimal(rdr["ad_flag"]);
                        yearsum += model.ThisYear * Convert.ToDecimal(rdr["ad_flag"]);
                    }
                }
                modAccProfitReport modelsum = new modAccProfitReport();
                modelsum.AccName = accname;
                modelsum.SubjectId = "合计";
                modelsum.SubjectName = "净利润";
                modelsum.ThisMonth = decimal.Round(monthsum, 2);
                modelsum.ThisYear = decimal.Round(yearsum, 2);
                modelsum.AdFlag = 1;
                modellist.Add(modelsum);
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
        /// get expense report
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccExpenseReport> GetAccExpenseReport(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseReport> modellist = new BindingCollection<modAccExpenseReport>();
                //Execute a query to read the categories 
                decimal monthexpense = 0;
                decimal yearexpense = 0;
                BindingCollection<modAccExpenseReport> list = GetAccExpenseReport(accname, "913530", "管理费用", isTrialBalance, out emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccExpenseReport mod in list)
                    {
                        modellist.Add(mod);
                        if (mod.SubjectId == "913530")
                        {
                            monthexpense += mod.ThisMonth;
                            yearexpense += mod.ThisYear;
                        }
                    }
                }

                list = GetAccExpenseReport(accname, "913535", "销售费用", isTrialBalance, out emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccExpenseReport mod in list)
                    {
                        modellist.Add(mod);
                        if (mod.SubjectId == "913535")
                        {
                            monthexpense += mod.ThisMonth;
                            yearexpense += mod.ThisYear;
                        }
                    }
                }

                list = GetAccExpenseReport(accname, "913540", "财务费用", isTrialBalance, out emsg);
                if (list != null && list.Count > 0)
                {
                    foreach (modAccExpenseReport mod in list)
                    {
                        modellist.Add(mod);
                        if (mod.SubjectId == "913540")
                        {
                            monthexpense += mod.ThisMonth;
                            yearexpense += mod.ThisYear;
                        }
                    }
                }

                string sql = string.Empty;
                string credenceTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string inoutTablename = isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout";
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                modAccExpenseReport model0 = new modAccExpenseReport();
                model0.AccName = accname;
                model0.SubjectId = "-";
                model0.SubjectName = "费用总计";
                model0.ThisMonth = monthexpense;
                model0.ThisYear = yearexpense;
                model0.AdFlag = -1;
                modellist.Add(model0);

                modAccExpenseReport model1 = new modAccExpenseReport();
                model1.AccName = accname;
                model1.SubjectId = "-";
                model1.SubjectName = "总经营额";
                sql = string.Format("select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from " + credenceTablename + " where acc_name='{0}' and subject_id='{1}'", accname, "913505");
                model1.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                sql = string.Format("select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from " + credenceTablename + " where acc_name in (select acc_name from acc_period_list where acc_year={0}) and subject_id='{1}'", modp.AccYear, "913505");
                model1.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                model1.AdFlag = 1;
                modellist.Add(model1);

                modAccExpenseReport model2 = new modAccExpenseReport();
                model2.AccName = accname;
                model2.SubjectId = "-";
                model2.SubjectName = "总采购额";
                sql = string.Format("select isnull(sum((borrow_money-lend_money)),0) from " + credenceTablename + " where acc_name='{0}' and subject_id='{1}'", accname, "913510");
                model2.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                sql = string.Format("select isnull(sum((borrow_money-lend_money)),0) from " + credenceTablename + " where acc_name in (select acc_name from acc_period_list where acc_year={0}) and subject_id='{1}'", modp.AccYear, "913510");
                model2.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                model2.AdFlag = -1;
                modellist.Add(model2);

                modAccExpenseReport model3 = new modAccExpenseReport();
                model3.AccName = accname;
                model3.SubjectId = "-";
                model3.SubjectName = "平均库存金额";
                sql = string.Format("select isnull(sum(start_mny+input_mny-output_mny),0) from " + inoutTablename + " where acc_name='{0}'", accname);
                model3.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                sql = string.Format("select avg(end_mny) from (select acc_name, isnull(sum(start_mny+input_mny-output_mny),0) end_mny from " + inoutTablename + " where acc_name in (select acc_name from acc_period_list where acc_year={0}) group by acc_name) t", modp.AccYear);
                model3.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)), 2);
                model3.AdFlag = 1;
                modellist.Add(model3);

                modAccExpenseReport model4 = new modAccExpenseReport();
                model4.AccName = accname;
                model4.SubjectId = "-";
                model4.SubjectName = "企业净利";                
                decimal startmny = 0;
                decimal endmny = 0;
                GetSubjectMny(accname, "9135", isTrialBalance, ref startmny, ref endmny, out emsg);
                model4.ThisMonth = decimal.Round(endmny - startmny, 2);
                model4.ThisYear = decimal.Round(endmny, 2);
                model4.AdFlag = 1;
                modellist.Add(model4);

                modAccExpenseReport model5 = new modAccExpenseReport();
                model5.AccName = accname;
                model5.SubjectId = "-";
                model5.SubjectName = "企业人数";
                model5.ThisMonth = modp.EmployeeCount;
                sql = string.Format("select round(avg(employee_count),2) from acc_period_list where acc_name in (select acc_name from acc_period_list where acc_year={0})", modp.AccYear);
                model5.ThisYear = Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                model5.AdFlag = 1;
                modellist.Add(model5);

                modAccExpenseReport model6 = new modAccExpenseReport();
                model6.AccName = accname;
                model6.SubjectId = "-";
                model6.SubjectName = "人均销售额";
                model6.ThisMonth = decimal.Round(model1.ThisMonth / model5.ThisMonth, 2);
                model6.ThisYear = decimal.Round(model1.ThisYear / model5.ThisYear, 2);
                model6.AdFlag = 1;
                modellist.Add(model6);

                modAccExpenseReport model7 = new modAccExpenseReport();
                model7.AccName = accname;
                model7.SubjectId = "-";
                model7.SubjectName = "人均采购额";
                model7.ThisMonth = decimal.Round(model2.ThisMonth / model5.ThisMonth, 2);
                model7.ThisYear = decimal.Round(model2.ThisYear / model5.ThisYear, 2);
                model7.AdFlag = -1;
                modellist.Add(model7);

                modAccExpenseReport model8 = new modAccExpenseReport();
                model8.AccName = accname;
                model8.SubjectId = "-";
                model8.SubjectName = "人均库存额";
                model8.ThisMonth = decimal.Round(model3.ThisMonth / model5.ThisMonth, 2);
                model8.ThisYear = decimal.Round(model3.ThisYear / model5.ThisYear, 2);
                model8.AdFlag = 1;
                modellist.Add(model8);

                modAccExpenseReport model9 = new modAccExpenseReport();
                model9.AccName = accname;
                model9.SubjectId = "-";
                model9.SubjectName = "人均净利";
                model9.ThisMonth = decimal.Round(model4.ThisMonth / model5.ThisMonth, 2);
                model9.ThisYear = decimal.Round(model4.ThisYear / model5.ThisYear, 2);
                model9.AdFlag = 1;
                modellist.Add(model9);

                modAccExpenseReport model10 = new modAccExpenseReport();
                model10.AccName = accname;
                model10.SubjectId = "-";
                model10.SubjectName = "人均费用";
                model10.ThisMonth = decimal.Round(monthexpense / model5.ThisMonth, 2);
                model10.ThisYear = decimal.Round(yearexpense / model5.ThisMonth, 2);
                model10.AdFlag = -1;
                modellist.Add(model10);

                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get expense report
        /// <summary>
        /// <param name=validonly>status is valid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accchecklist</returns>
        public BindingCollection<modAccExpenseReport> GetAccExpenseReport(string accname, string subjectid, string subjectname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccExpenseReport> modellist = new BindingCollection<modAccExpenseReport>();
                //Execute a query to read the categories                
                string sql = string.Empty;
                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);
                decimal monthsum = 0;
                decimal yearsum = 0;
                string credenceTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                sql = string.Format("select subject_id,subject_name, ad_flag, has_children from acc_subject_list where psubject_id='{0}' order by subject_id", subjectid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccExpenseReport model = new modAccExpenseReport();
                        model.AccName = accname;
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = rdr["subject_name"].ToString();
                        sql = string.Format("select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from " + credenceTablename + " where acc_name='{0}' and seq>0 and subject_id='{1}'", accname, model.SubjectId);
                        model.ThisMonth = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                        sql = string.Format("select isnull(sum((borrow_money-lend_money) * zcfz_flag),0) from " + credenceTablename + " where acc_name in (select acc_name from acc_period_list where acc_year={0}) and seq>0 and subject_id='{1}'", modp.AccYear, model.SubjectId);
                        model.ThisYear = decimal.Round(Convert.ToDecimal(SqlHelper.ExecuteScalar(sql)) * Convert.ToDecimal(rdr["ad_flag"]), 2);
                        model.AdFlag = Convert.ToInt32(rdr["ad_flag"]);
                        modellist.Add(model);
                        monthsum += (-1) * model.ThisMonth * Convert.ToDecimal(rdr["ad_flag"]);
                        yearsum += (-1) * model.ThisYear * Convert.ToDecimal(rdr["ad_flag"]);
                    }
                }
                modAccExpenseReport modelsum = new modAccExpenseReport();
                modelsum.AccName = accname;
                modelsum.SubjectId = subjectid;
                modelsum.SubjectName = subjectname;
                modelsum.ThisMonth = decimal.Round(monthsum, 2);
                modelsum.ThisYear = decimal.Round(yearsum, 2);
                modelsum.AdFlag = -1;
                modellist.Add(modelsum);
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
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public void GetCredenceDetail(bool startflag, string accname, string subjectid, bool isTrialBalance, ref BindingCollection<modAccCredenceDetail> modellist, out string emsg)
        {
            try
            {
                string startwhere=string.Empty;
                if (!startflag)
                    startwhere = "and a.seq>0 ";


                string detailTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string listTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";
                dalAccSubjectList dalsub=new dalAccSubjectList();
                modAccSubjectList modsub = dalsub.GetItem(subjectid, out emsg);
				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname),
					new SqlParameter("subject_id", subjectid)
				};
				if (modsub.HasChildren==1)
                {
                    List<modAccSubjectList> listsub = dalsub.GetIList(subjectid, true, out emsg);
					if (subjectid == "9135")
					{
						listsub.Add(new modAccSubjectList{ SubjectId = subjectid, SubjectName= "本年利润", AdFlag=1 });
					}
                    foreach (modAccSubjectList modchild in listsub)
                    {
                        if (modchild.HasChildren == 0 || modchild.SubjectId =="9135")
                        {
                            string sql = "select a.acc_name,a.seq,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name,a.digest,a.lend_money,a.borrow_money,a.exchange_rate,a.currency "
                                    + "from " + detailTablename + " a inner join " + listTablename + " b on a.acc_name=b.acc_name and a.seq=b.seq where b.status=1 and a.acc_name=@acc_name and a.subject_id=@subject_id " 
									+ startwhere + "order by a.seq";
							parms[0].Value = accname;
							parms[1].Value = modchild.SubjectId;

							decimal thisBalance = 0;
							decimal lastBalance = 0;
							decimal borrowSum = 0;
							decimal lendSum = 0;
							using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                            {
                                while (rdr.Read())
                                {
                                    modAccCredenceDetail model = new modAccCredenceDetail();
                                    model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                                    model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                                    model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                                    model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                                    model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                                    model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                                    model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                                    model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                                    model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                                    model.Digest = dalUtility.ConvertToString(rdr["digest"]);
									if (model.Digest == "上月结存")
									{
										model.LastBalance = (decimal)model.AdFlag * (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"]));
										thisBalance += model.LastBalance;
										lastBalance += model.LastBalance;
									}
									else
									{
										model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
										model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
										lendSum += model.LendMoney;
										borrowSum += model.BorrowMoney;
									}									
                                    model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                                    model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                                    modellist.Add(model);
                                }
                            }

							if (modellist.Count > 0)
							{
								modAccCredenceDetail model = new modAccCredenceDetail();
								model.AccName = modellist[0].AccName;
								model.AdFlag = modellist[0].AdFlag;
								model.SubjectName = "本期结存";
								model.ThisBalance = lastBalance + ((decimal)model.AdFlag) * (borrowSum - lendSum);
								model.LastBalance = lastBalance;
								model.BorrowMoney = borrowSum;
								model.LendMoney = lendSum;
								modellist.Add(model);
							}
						}
                    }
                }
                else
                {
                    if (modellist == null)
                        modellist = new BindingCollection<modAccCredenceDetail>();
                    //Execute a query to read the categories
                    string sql = "select a.acc_name,a.seq,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name,a.digest,a.lend_money,a.borrow_money,a.exchange_rate,a.currency "
                            + "from " + detailTablename + " a inner join " + listTablename + " b on a.acc_name=b.acc_name and a.seq=b.seq where b.status=1 and a.acc_name=@acc_name and a.subject_id=@subject_id "
							+ startwhere + "order by a.seq";

					decimal thisBalance = 0;
					decimal lastBalance = 0;
					decimal borrowSum = 0;
					decimal lendSum = 0;
					using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                    {
                        while (rdr.Read())
                        {
                            modAccCredenceDetail model = new modAccCredenceDetail();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                            model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                            model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                            model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                            model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                            model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                            model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                            model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                            model.Digest = dalUtility.ConvertToString(rdr["digest"]);
							if (model.Digest == "上月结存")
							{
								model.LastBalance = (decimal)model.AdFlag * (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"]));
								thisBalance += model.LastBalance;
								lastBalance += model.LastBalance;
							}
							else
							{
								model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
								model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
								lendSum += model.LendMoney;
								borrowSum += model.BorrowMoney;
							}
							model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                            modellist.Add(model);
                        }
                    }
					if (modellist.Count > 0)
					{
						modAccCredenceDetail model = new modAccCredenceDetail();
						model.AccName = modellist[0].AccName;
						model.AdFlag = modellist[0].AdFlag;
						model.SubjectName = "本期结存";
						model.ThisBalance = lastBalance + ((decimal)model.AdFlag) * (borrowSum - lendSum);
						model.LastBalance = lastBalance;
						model.BorrowMoney = borrowSum;
						model.LendMoney = lendSum;
						modellist.Add(model);
					}
				}
                emsg = null;
                return;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return;
            }
        }

        /// <summary>
        /// get account credence summary
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>credende summary</returns>
        public BindingCollection<modAccCredenceSummary> GetCredenceSummary(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceSummary> modellist = new BindingCollection<modAccCredenceSummary>();

                decimal borrowsum = 0;
                decimal lendsum = 0;
                string credenceTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";

				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname)
				};

				string sql = "select b.subject_id, b.subject_name,sum(a.lend_money) lend_money,sum(a.borrow_money) borrow_money "
                            + "from " + credenceTablename + " a inner join acc_subject_list b on a.subject_id=b.subject_id "
							+ "where a.seq>0 and a.ad_flag!=0 and a.acc_name=@acc_name and a.subject_id!='2125' "
							+ "group by b.subject_id,b.subject_name order by b.subject_id,b.subject_name ";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceSummary model = new modAccCredenceSummary();
                        model.AccName = accname;
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);   
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);                            
                        model.LendMoney = string.Format("{0:N2}", rdr["lend_money"]);
                        model.BorrowMoney = string.Format("{0:N2}", rdr["borrow_money"]);
                        modellist.Add(model);
                        borrowsum += Convert.ToDecimal(rdr["borrow_money"]);
                        lendsum += Convert.ToDecimal(rdr["lend_money"]);
                    }
                }

                modAccCredenceSummary modelsum = new modAccCredenceSummary();
                modelsum.AccName = "合计";
                modelsum.SubjectId = "合计";
                modelsum.SubjectName = "所有科目";
                modelsum.LendMoney = string.Format("{0:N2}", lendsum);
                modelsum.BorrowMoney = string.Format("{0:N2}", borrowsum);
                modellist.Add(modelsum);
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
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=detailid>detailid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modAccCredenceDetail> GetCredenceDetail(string accname, string subjectid, string detailid, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceDetail> modellist = new BindingCollection<modAccCredenceDetail>();
                string detailTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string listTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";

				SqlParameter[] parms = {
					new SqlParameter("acc_name", accname),
					new SqlParameter("subject_id", subjectid),
					new SqlParameter("detail_id", detailid)
				};
				string sql = "select a.acc_name,a.seq,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name,a.digest,a.lend_money,a.borrow_money,a.exchange_rate,a.currency "
                        + "from " + detailTablename + " a inner join " + listTablename + " b on a.acc_name=b.acc_name and a.seq=b.seq where b.status=1 and a.acc_name=@acc_name and a.subject_id=@subject_id and a.detail_id=@detail_id order by a.seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql, parms))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceDetail model = new modAccCredenceDetail();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                        model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
                        model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        modellist.Add(model);
                    }
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=detailid>detailid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modAccCredenceDetail> GetCashAndBankDetail(string accname, string detailid, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceDetail> modellist = new BindingCollection<modAccCredenceDetail>();
                string detailTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string listTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";

                string sql = string.Format("select a.acc_name,a.seq,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name,isnull(a.digest,d.credence_type) digest,a.borrow_money,a.lend_money,a.exchange_rate,a.currency "
                        + "from " + detailTablename + " a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.seq=d.seq where d.status=1 and (d.seq=0 or d.credence_type='一般凭证') and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);
                
                sql += string.Format("union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,b.cust_name detail_name,'收'+ b.cust_name +'货款' digest,a.get_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from acc_receivable_form a inner join customer_list b on a.cust_id=b.cust_id inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.vendor_name detail_name,'付'+a.vendor_name+'货款' digest,0 borrow_money,a.get_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_payable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when -1 then a.get_mny else 0 end as borrow_money,case ad_flag when 1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_receivable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when 1 then a.get_mny else 0 end as borrow_money,case ad_flag when -1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_payable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.expense_id,a.expense_name detail_name,a.remark digest,0 borrow_money,a.expense_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_expense_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,1 detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.asset_name detail_name,'购资产'+a.remark digest,0 borrow_money,a.qty*a.price,a.exchange_rate,a.currency "
                        + "from asset_add a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,1 detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.asset_name detail_name,'处理资产'+a.remark digest,a.sale_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from asset_sale a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030' and a.detail_id='{1}'", accname, detailid);

                sql += string.Format("union all select a.acc_name,a.acc_seq,id detail_seq,b.subject_id,'现金银行',1 zcfz_flag,case c.subject_id when '1075' then 1 else -1 end as ad_flag,b.detail_id,c.check_type detail_name,'支票[' +c.check_no +']到期兑帐' digest,case c.subject_id when '1075' then b.mny else 0 end borrow_money,case c.subject_id when '5125' then b.mny else 0 end lend_money,c.exchange_rate,c.currency "
                        + "from acc_check_form a inner join acc_check_form_detail b on a.form_id=b.form_id inner join acc_check_list c on a.check_id=c.id inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and b.subject_id='1030' and b.detail_id='{1}'", accname, detailid);

				sql = "select * from (" + sql + ") t order by seq,detail_seq";

				decimal thisBalance = 0;
				decimal lastBalance = 0;
				decimal borrowSum = 0;
				decimal lendSum = 0;
				using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceDetail model = new modAccCredenceDetail();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.Digest = dalUtility.ConvertToString(rdr["digest"]);
						if (model.Digest == "上月结存")
						{
							model.LastBalance = (decimal)model.AdFlag * (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"]));
							thisBalance += model.LastBalance;
							lastBalance += model.LastBalance;
						}
						else
						{
							model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
							model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
							lendSum += model.LendMoney;
							borrowSum += model.BorrowMoney;
						}
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        modellist.Add(model);
                    }
                }
				if (modellist.Count > 0)
				{
					modAccCredenceDetail model = new modAccCredenceDetail();
					model.AccName = modellist[0].AccName;
					model.AdFlag = modellist[0].AdFlag;
					model.SubjectName = "本期结存";
					model.ThisBalance = lastBalance + ((decimal)model.AdFlag) * (borrowSum- lendSum);
					model.LastBalance = lastBalance;
					model.BorrowMoney = borrowSum;
					model.LendMoney = lendSum;
					modellist.Add(model);
				}
				emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }
                
        /// <summary>
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modAccCredenceDetail> GetCashAndBankDetail(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceDetail> modellist = new BindingCollection<modAccCredenceDetail>();
                string detailTablename = isTrialBalance ? "acc_trial_credence_detail" : "acc_credence_detail";
                string listTablename = isTrialBalance ? "acc_trial_credence_list" : "acc_credence_list";

                string sql = string.Format("select a.acc_name,a.seq,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name,isnull(a.digest,d.credence_type) digest,a.borrow_money,a.lend_money,a.exchange_rate,a.currency "
                        + "from " + detailTablename + " a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.seq=d.seq where (d.seq=0 or d.credence_type='一般凭证') and d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format("union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,b.cust_name detail_name,'收'+ b.cust_name +'货款' digest,a.get_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from acc_receivable_form a inner join customer_list b on a.cust_id=b.cust_id inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.vendor_name detail_name,'付'+a.vendor_name+'货款' digest,0 borrow_money,a.get_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_payable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when -1 then a.get_mny else 0 end as borrow_money,case ad_flag when 1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_receivable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when 1 then a.get_mny else 0 end as borrow_money,case ad_flag when -1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_payable_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.expense_id,a.expense_name detail_name,a.remark digest,0 borrow_money,a.expense_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_expense_form a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,1 detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.asset_name detail_name,'购资产'+a.remark digest,0 borrow_money,a.qty*a.price,a.exchange_rate,a.currency "
                        + "from asset_add a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,1 detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.asset_name detail_name,'处理资产'+a.remark digest,a.sale_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from asset_sale a inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and a.subject_id='1030'", accname);

                sql += string.Format("union all select a.acc_name,a.acc_seq,id detail_seq,b.subject_id,'现金银行',1 zcfz_flag,case c.subject_id when '1075' then 1 else -1 end as ad_flag,b.detail_id,c.check_type detail_name,'支票[' +c.check_no +']到期兑帐' digest,case c.subject_id when '1075' then b.mny else 0 end borrow_money,case c.subject_id when '5125' then b.mny else 0 end lend_money,c.exchange_rate,c.currency "
                        + "from acc_check_form a inner join acc_check_form_detail b on a.form_id=b.form_id inner join acc_check_list c on a.check_id=c.id inner join " + listTablename + " d on a.acc_name=d.acc_name and a.acc_seq=d.seq where d.status=1 and a.acc_name='{0}' and b.subject_id='1030'", accname);

                sql += " order by acc_name,seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccCredenceDetail model = new modAccCredenceDetail();
                        model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                        model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]);
                        model.DetailSeq = dalUtility.ConvertToInt(rdr["detail_seq"]);
                        model.SubjectId = dalUtility.ConvertToString(rdr["subject_id"]);
                        model.SubjectName = dalUtility.ConvertToString(rdr["subject_name"]);
                        model.ZcfzFlag = dalUtility.ConvertToInt(rdr["zcfz_flag"]);
                        model.AdFlag = dalUtility.ConvertToInt(rdr["ad_flag"]);
                        model.DetailId = dalUtility.ConvertToString(rdr["detail_id"]);
                        model.DetailName = dalUtility.ConvertToString(rdr["detail_name"]);
                        model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                        model.LendMoney = dalUtility.ConvertToDecimal(rdr["lend_money"]);
                        model.BorrowMoney = dalUtility.ConvertToDecimal(rdr["borrow_money"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.Currency = dalUtility.ConvertToString(rdr["currency"]);
                        modellist.Add(model);
                    }
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get acccredencedetail
        /// <summary>
        /// <param name=accname>accname</param>
        /// <param name=subjectid>subjectid</param>
        /// <param name=detailid>detailid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all acccredencedetail</returns>
        public BindingCollection<modAccCredenceBook> GetCashAndBankBook(string detailid, DateTime startDate, DateTime endDate, out string emsg)
        {
            try
            {
                BindingCollection<modAccCredenceBook> modellist = new BindingCollection<modAccCredenceBook>();
                string detailTablename = "acc_credence_detail";
                string listTablename = "acc_credence_list";

                string sql = string.Format("select a.acc_name,a.seq,b.credence_date as form_date,a.detail_seq,a.subject_id,a.subject_name,a.zcfz_flag,a.ad_flag,a.detail_id,a.detail_name detail_name,isnull(a.digest,b.credence_type) digest,a.borrow_money,a.lend_money,a.exchange_rate,a.currency "
                        + "from " + detailTablename + " a inner join " + listTablename + " b on a.acc_name=b.acc_name and a.seq=b.seq where (b.seq=0 or b.credence_type='一般凭证') and b.credence_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' ", startDate, endDate, detailid);

                sql += string.Format("union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,b.cust_name detail_name,'收'+ b.cust_name +'货款' digest,a.get_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from acc_receivable_form a inner join customer_list b on a.cust_id=b.cust_id where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.vendor_name detail_name,'付'+a.vendor_name+'货款' digest,0 borrow_money,a.get_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_payable_form a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when -1 then a.get_mny else 0 end as borrow_money,case ad_flag when 1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_receivable_form a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.object_name detail_name,a.object_name + a.form_type digest,case ad_flag when 1 then a.get_mny else 0 end as borrow_money,case ad_flag when -1 then a.get_mny else 0 end as lend_money,a.exchange_rate,a.currency "
                        + "from acc_other_payable_form a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.expense_name detail_name,a.remark digest,0 borrow_money,a.expense_mny lend_money,a.exchange_rate,a.currency "
                        + "from acc_expense_form a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,1 detail_seq,a.subject_id,'现金银行',-1 zcfz_flag,-1 ad_flag,a.detail_id,a.asset_name detail_name,'购资产'+a.remark digest,0 borrow_money,a.qty*a.price,a.exchange_rate,a.currency "
                        + "from asset_add a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format(" union all select a.acc_name,a.acc_seq,a.form_date,1 detail_seq,a.subject_id,'现金银行',1 zcfz_flag,1 ad_flag,a.detail_id,a.asset_name detail_name,'处理资产'+a.remark digest,a.sale_mny borrow_money,0 lend_money,a.exchange_rate,a.currency "
                        + "from asset_sale a where a.form_date between '{0}' and '{1}' and a.subject_id='1030' and a.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += string.Format("union all select a.acc_name,a.acc_seq,a.form_date,id detail_seq,b.subject_id,'现金银行',1 zcfz_flag,case c.subject_id when '1075' then 1 else -1 end as ad_flag,b.detail_id,c.check_type detail_name,'支票[' +c.check_no +']到期兑帐' digest,case c.subject_id when '1075' then b.mny else 0 end borrow_money,case c.subject_id when '5125' then b.mny else 0 end lend_money,c.exchange_rate,c.currency "
                        + "from acc_check_form a inner join acc_check_form_detail b on a.form_id=b.form_id inner join acc_check_list c on a.check_id=c.id where a.form_date between '{0}' and '{1}' and b.subject_id='1030' and b.detail_id='{2}' and a.acc_name is not null ", startDate, endDate, detailid);

                sql += " order by acc_name,seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    string accName = string.Empty;
                    decimal totalstart = 0;
                    decimal totaladding = 0;
                    decimal totalpaid = 0;
                    modAccCredenceBook model;
                    while (rdr.Read())
                    {
                        if (string.IsNullOrEmpty(accName))
                        {
                            accName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model = new modAccCredenceBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.AccDate = dalUtility.ConvertToDate(rdr["form_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                            decimal startmny = 0;
                            decimal addingmny = 0;
                            decimal paidmny = 0;
                            if (model.AccSeq == "上月结存")
                                startmny = (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"])) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            else
                            {
                                startmny = totalstart;
                                addingmny = dalUtility.ConvertToDecimal(rdr["borrow_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                                paidmny = dalUtility.ConvertToDecimal(rdr["lend_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            }
                            
                            model.StartMny = startmny == 0 ? "" : startmny.ToString("#0.00");
                            model.AddingMny = addingmny == 0 ? "" : addingmny.ToString("#0.00");
                            model.PaidMny = paidmny == 0 ? "" : paidmny.ToString("#0.00");
                            model.EndMny = (startmny + addingmny - paidmny).ToString("#0.00");
                            
                            modellist.Add(model);
                            totalstart += startmny + addingmny - paidmny;
                            totaladding += addingmny;
                            totalpaid += paidmny;
                        }
                        else if (accName != dalUtility.ConvertToString(rdr["acc_name"]))
                        {
                            model = new modAccCredenceBook();
                            model.AccName = accName;
                            model.Digest = "";
                            model.AccSeq = "本月合计";
                            model.StartMny = "";
                            model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                            model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
                            model.EndMny = totalstart.ToString("#0.00");
                            modellist.Add(model);

                            accName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model = new modAccCredenceBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                            model.AccDate = dalUtility.ConvertToDate(rdr["form_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            decimal startmny = 0;
                            decimal addingmny = 0;
                            decimal paidmny = 0;
                            if (model.AccSeq == "上月结存")
                                startmny = (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"])) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            else
                            {
                                startmny = totalstart;
                                addingmny = dalUtility.ConvertToDecimal(rdr["borrow_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                                paidmny = dalUtility.ConvertToDecimal(rdr["lend_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            }
                            model.StartMny = startmny == 0 ? "" : startmny.ToString("#0.00");
                            model.AddingMny = addingmny == 0 ? "" : addingmny.ToString("#0.00");
                            model.PaidMny = paidmny == 0 ? "" : paidmny.ToString("#0.00");
                            model.EndMny = (startmny + addingmny - paidmny).ToString("#0.00");
                            modellist.Add(model);

                            totalstart = startmny;
                            totaladding = addingmny;
                            totalpaid = paidmny;
                                                        
                        }
                        else
                        {
                            model = new modAccCredenceBook();
                            model.AccName = dalUtility.ConvertToString(rdr["acc_name"]);
                            model.Digest = dalUtility.ConvertToString(rdr["digest"]);
                            model.AccDate = dalUtility.ConvertToDate(rdr["form_date"]).ToString("yyyy-MM-dd");
                            model.AccSeq = dalUtility.ConvertToInt(rdr["seq"]) == 0 ? "上月结存" : dalUtility.ConvertToString(rdr["seq"]);
                            decimal startmny = 0;
                            decimal addingmny = 0;
                            decimal paidmny = 0;
                            if (model.AccSeq == "上月结存")
                                startmny = (dalUtility.ConvertToDecimal(rdr["borrow_money"]) - dalUtility.ConvertToDecimal(rdr["lend_money"])) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            else
                            {
                                startmny = totalstart;
                                addingmny = dalUtility.ConvertToDecimal(rdr["borrow_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                                paidmny = dalUtility.ConvertToDecimal(rdr["lend_money"]) * dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                            }
                            model.StartMny = startmny == 0 ? "" : startmny.ToString("#0.00");
                            model.AddingMny = addingmny == 0 ? "" : addingmny.ToString("#0.00");
                            model.PaidMny = paidmny == 0 ? "" : paidmny.ToString("#0.00");
                            model.EndMny = (startmny + addingmny - paidmny).ToString("#0.00");
                            modellist.Add(model);

                            totalstart = startmny + addingmny - paidmny;
                            totaladding += addingmny;
                            totalpaid += paidmny;
                        }
                    }
                    model = new modAccCredenceBook();
                    model.AccName = accName;
                    model.Digest = "";
                    model.AccSeq = "本月合计";
                    model.StartMny = "";
                    model.AddingMny = totaladding == 0 ? "" : totaladding.ToString("#0.00");
                    model.PaidMny = totalpaid == 0 ? "" : totalpaid.ToString("#0.00");
                    model.EndMny = totalstart.ToString("#0.00");
                    modellist.Add(model);
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        public decimal GetCashAndBankSum(string accname, bool isTrialBalance, out string emsg)
        {
            decimal sum = 0;
            BindingCollection<modAccCredenceDetail> list2 = GetCashAndBankDetail(accname, isTrialBalance, out emsg);
            foreach (modAccCredenceDetail mod2 in list2)
            {
                sum += (mod2.BorrowMoney - mod2.LendMoney) * mod2.ExchangeRate;
            }
            return sum;
        }

        /// <summary>
        /// get account balance
        /// <summary>
        /// <param name=accname>accname</param>
        ///<returns>check if balance of total and detail account subject</returns>
        public BindingCollection<modAccountBalance> GetAccountBalance(string accname, bool isTrialBalance, out string emsg)
        {
            try
            {
                decimal startmny=0;
                decimal endmny=0;
                string sql=string.Empty;
                BindingCollection<modAccountBalance> modellist = new BindingCollection<modAccountBalance>();
                modAccountBalance model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "1030";
                model.SubjectName = "现金银行";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                model.DetailSum = GetCashAndBankSum(accname, isTrialBalance, out emsg);
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "1235";
                model.SubjectName = "库存商品";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum(start_mny+input_mny-output_mny),0) from " + (isTrialBalance ? "acc_trial_product_inout" : "acc_product_inout") + " where acc_name='{0}'", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "1055";
                model.SubjectName = "应收帐款";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum((start_mny+adding_mny-paid_mny) * exchange_rate),0) from acc_receivable_list where acc_name='{0}'", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]),2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "5145";
                model.SubjectName = "应付账款";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum((start_mny+adding_mny-paid_mny) * exchange_rate),0) from acc_payable_list where acc_name='{0}'", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "1060";
                model.SubjectName = "其它应收款";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
				model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum((start_mny+adding_mny-paid_mny) * exchange_rate),0) from acc_other_receivable where acc_name='{0}'", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "5155";
                model.SubjectName = "其他应付款";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum((start_mny+adding_mny-paid_mny) * exchange_rate),0) from acc_other_payable where acc_name='{0}'", accname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "1075";
                model.SubjectName = "应收票据";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format(@"select isnull(sum(mny * exchange_rate),0) from acc_check_list a where acc_name<='{0}' and subject_id='{1}' and (status=0
								or (status=1 and not exists(select '#' from acc_check_form b inner join acc_credence_list c on b.acc_name=c.acc_name and b.acc_seq=c.seq 
								where c.status=1 and b.check_id=a.id)))", accname, model.SubjectId);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "5125";
                model.SubjectName = "应付票据";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);
                sql = string.Format("select isnull(sum(mny * exchange_rate),0) from acc_check_list where acc_name<='{0}' and subject_id='{1}' and status=0", accname, model.SubjectId);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        model.DetailSum = decimal.Round(Convert.ToDecimal(rdr[0]), 2);
                    }
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                startmny = 0;
                endmny = 0;
                model = new modAccountBalance();
                model.AccName = accname;
                model.SubjectId = "2120";
                model.SubjectName = "固定资产净值";
                GetSubjectMny(accname, model.SubjectId, isTrialBalance, ref startmny, ref endmny, out emsg);
                model.TotalSum = decimal.Round(endmny, 2);

                model.DetailSum = 0;
                dalAssetList dalasset = new dalAssetList();
                BindingCollection<modAssetList> listasset = dalasset.GetIList("0,1", string.Empty, string.Empty, accname, out emsg);
                foreach (modAssetList modasset in listasset)
                {
                    model.DetailSum += modasset.NetMny;
                }
                model.Differ = decimal.Round(model.TotalSum - model.DetailSum, 2);
                modellist.Add(model);

                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get waiting audit list
        /// </summary>
        /// <param name="userid">userid</param>
        /// <param name="emsg">emsg</param>
        /// <returns>waiting audit list</returns>
        public BindingCollection<modWaitingAuditList> GetWaitingAuditList(string accname, string fromdate, string todate, out string emsg)
        {
            try
            {
                BindingCollection<modWaitingAuditList> modellist = new BindingCollection<modWaitingAuditList>();
                string sql = "select a.ship_id,a.ship_type,a.ship_date,a.status,c.cust_name,a.no,a.detail_sum,a.other_mny,a.other_reason,a.kill_mny,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from sales_shipment a inner join customer_list c on a.cust_id=c.cust_id where (a.status=0 or (a.ship_type<>'样品单' and a.acc_seq=0)) "
                        + "and a.ship_date >= '" + Convert.ToDateTime(fromdate) + "' and a.ship_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["ship_id"].ToString();
                        model.FormType = rdr["ship_type"].ToString();
                        model.FormDate = rdr["ship_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "客户: " + rdr["cust_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "票号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "货款金额: " + rdr["detail_sum"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "附加费用: " + rdr["other_mny"].ToString() + "  附加原因：" + rdr["other_reason"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "冲减金额: " + rdr["kill_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_type,a.form_date,a.status,c.cust_name,a.no,a.product_name,a.qty,a.mny,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from sales_design_form a inner join customer_list c on a.cust_id=c.cust_id where a.acc_seq=0 "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = rdr["form_type"].ToString();
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "客户: " + rdr["cust_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "票号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "品名: " + rdr["product_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "数量: " + rdr["qty"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "金额: " + rdr["mny"].ToString();                        
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.purchase_id,a.purchase_type,a.purchase_date,a.status,a.vendor_name,a.no,a.detail_sum,a.other_mny,a.other_reason,a.kill_mny,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from purchase_list a where (a.status=0 or a.acc_seq=0) and a.purchase_date >= '" + Convert.ToDateTime(fromdate) + "' and a.purchase_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["purchase_id"].ToString();
                        model.FormType = rdr["purchase_type"].ToString();
                        model.FormDate = rdr["purchase_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "供应商: " + rdr["vendor_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "票号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "货款金额: " + rdr["detail_sum"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "附加费用: " + rdr["other_mny"].ToString() + "  附加原因：" + rdr["other_reason"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "冲减金额: " + rdr["kill_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.form_id,a.form_type,a.form_date,a.status,a.price_status,a.dept_id,a.no,a.detail_sum,a.require_date,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from production_form a where (a.status=0 or a.acc_seq=0) and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["form_id"].ToString();
                        model.FormType = rdr["form_type"].ToString();
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                        {
                            if (Convert.ToInt32(rdr["price_status"]) == 0)
                                model.StatusDesc = "未设置成本价格";
                            else
                                model.StatusDesc = "未做凭证";
                        }
                        model.Remark = "供应商: " + rdr["dept_id"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "票号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "成本金额: " + rdr["detail_sum"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "要求日期: " + rdr["require_date"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.inout_type,a.inout_date,a.status,a.price_status,a.no,a.product_id,a.product_name,a.size,a.qty,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from warehouse_inout_form a where (a.status=0 or a.acc_seq=0) and a.inout_date >= '" + Convert.ToDateTime(fromdate) + "' and a.inout_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = rdr["inout_type"].ToString();
                        model.FormDate = rdr["inout_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                        {
                            if (Convert.ToInt32(rdr["price_status"]) == 0)
                                model.StatusDesc = "未设置成本价格";
                            else
                                model.StatusDesc = "未做凭证";
                        }                        
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "产品: " + rdr["product_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "尺寸: " + rdr["size"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "数量: " + rdr["qty"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_date,a.status,a.expense_name,a.expense_man,a.no,a.currency,a.expense_mny,a.exchange_rate,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_expense_form a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = "费用登记";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "费用名称: " + rdr["expense_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "报销人: " + rdr["expense_man"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "费用金额: " + rdr["expense_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_date,a.status,c.cust_name,a.no,a.currency,a.receivable_mny,a.exchange_rate,a.get_mny,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id inner join customer_list c on a.cust_id=c.cust_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = "收款单";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "客户: " + rdr["cust_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "实付金额: " + rdr["get_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "帐款金额: " + rdr["receivable_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_date,a.status,a.vendor_name,a.no,a.currency,a.payable_mny,a.exchange_rate,a.get_mny,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_payable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = "付款单";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "供应商: " + rdr["vendor_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "实付金额: " + rdr["get_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "帐款金额: " + rdr["payable_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_type,a.form_date,a.status,a.object_name,a.no,a.get_mny,a.receivable_mny,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_other_receivable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = rdr["form_type"].ToString();
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "对象: " + rdr["object_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "实收金额: " + rdr["get_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "帐款金额: " + rdr["receivable_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.id,a.form_type,a.form_date,a.status,a.object_name,a.no,a.get_mny,a.payable_mny,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_other_payable_form a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["id"].ToString();
                        model.FormType = rdr["form_type"].ToString();
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "对象: " + rdr["object_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "实付金额: " + rdr["get_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "帐款金额: " + rdr["payable_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.form_id,a.form_date,a.status,b.check_no,b.bank_name,b.check_type,c.subject_name,b.mny,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from acc_check_form a inner join acc_check_list b on a.check_id=b.id inner join acc_subject_list c on b.subject_id=c.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["form_id"].ToString();
                        model.FormType = rdr["subject_name"].ToString();
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark += "支票金额: " + rdr["mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "支票号码: " + rdr["check_no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark = "银行名称: " + rdr["bank_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "支票类型: " + rdr["check_type"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.form_id,a.form_date,a.status,a.asset_name,a.no,a.qty,a.price,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from asset_add a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["form_id"].ToString();
                        model.FormType = "固定资产增加";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "对象: " + rdr["asset_name"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "数量: " + rdr["qty"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "单价: " + rdr["price"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.form_id,a.form_date,a.status,a.asset_id,a.asset_name,a.no,a.net_mny,a.sale_mny,b.subject_name,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from asset_sale a inner join acc_subject_list b on a.subject_id=b.subject_id where (a.status=0 or a.acc_seq=0) "
                        + "and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["form_id"].ToString();
                        model.FormType = "固定资产处理";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "对象: " + rdr["asset_id"].ToString() + "[" + rdr["asset_name"].ToString() + "]";
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "\r\n";
                        model.Remark += "会计科目: " + rdr["subject_name"].ToString();
                        model.Remark += "资产净值: " + rdr["net_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "销售额: " + rdr["sale_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                sql = "select a.form_id,a.form_date,a.status,a.asset_id,a.asset_name,a.no,a.net_mny,a.evaluate_mny,a.remark,a.update_user,a.update_time,a.acc_name "
                        + "from asset_evaluate a where (a.status=0 or a.acc_seq=0) and a.form_date >= '" + Convert.ToDateTime(fromdate) + "' and a.form_date <= '" + Convert.ToDateTime(todate) + "'";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["form_id"].ToString();
                        model.FormType = "固定资产评估";
                        model.FormDate = rdr["form_date"].ToString();
                        if (Convert.ToInt32(rdr["status"]) == 0)
                            model.StatusDesc = "未审核";
                        else
                            model.StatusDesc = "未做凭证";
                        model.Remark = "对象: " + rdr["asset_id"].ToString() + "[" + rdr["asset_name"].ToString() + "]";
                        model.Remark += "\r\n";
                        model.Remark += "单号: " + rdr["no"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "\r\n";
                        model.Remark += "资产净值: " + rdr["net_mny"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "评估值: " + rdr["evaluate_mny"].ToString();
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }

                dalAccCredenceList dalcre = new dalAccCredenceList();
                if (!dalcre.ExistDepre(accname, out emsg))
                {
                    sql = "select count(1) from asset_list";
                    int assetcount=Convert.ToInt32(SqlHelper.ExecuteScalar(sql));
                    if (assetcount > 0)
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = "";
                        model.FormType = "资产折旧";
                        model.FormDate = todate;
                        model.StatusDesc = "未做凭证";
                        model.Remark = "凭证字: 折";
                        model.Remark += "\r\n";
                        model.Remark += "固定资产个数: " + assetcount.ToString();                        
                        model.UpdateUser = "";
                        model.UpdateTime = DateTime.Now.ToString();
                        modellist.Add(model);
                    }
                }
                
                sql = "select a.acc_name,a.seq,a.status,a.credence_type,a.credence_word,a.credence_date,a.attach_count,a.remark,a.update_user,a.update_time "
                        + "from acc_credence_list a where a.status=0 and a.credence_date >= '" + Convert.ToDateTime(fromdate) + "' and a.credence_date <= '" + Convert.ToDateTime(todate) + "' order by a.seq";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modWaitingAuditList model = new modWaitingAuditList();
                        model.FormId = rdr["seq"].ToString();
                        model.FormType = rdr["credence_type"].ToString();
                        model.FormDate = rdr["credence_date"].ToString();
                        model.StatusDesc = "凭证未审核";
                        model.Remark = "凭证字: " + rdr["credence_word"].ToString();
                        model.Remark += "\r\n";
                        model.Remark += "附件张数: " + rdr["attach_count"].ToString();
                        model.Remark += "\r\n";
                        if (!string.IsNullOrEmpty(rdr["remark"].ToString()))
                        {
                            model.Remark += "\r\n";
                            model.Remark += rdr["remark"].ToString();
                        }
                        model.UpdateUser = rdr["update_user"].ToString();
                        model.UpdateTime = rdr["update_time"].ToString();
                        modellist.Add(model);
                    }
                }
                emsg = string.Empty;
                return modellist;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message.ToString());
                return null;
            }
        }
    }
}
