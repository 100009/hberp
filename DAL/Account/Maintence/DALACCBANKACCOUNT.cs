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
    public class dalAccBankAccount
    {
        /// <summary>
        /// get all accbankaccount
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all accbankaccount</returns>
        public BindingCollection<modAccBankAccount> GetIList(out string emsg)
        {
            try
            {
                BindingCollection<modAccBankAccount> modellist = new BindingCollection<modAccBankAccount>();
                //Execute a query to read the categories
                string sql = @"select a.account_no,a.bank_name,a.currency,b.exchange_rate,a.tax_flag,a.update_user,a.update_time 
						from acc_bank_account a inner join acc_currency_list b on a.currency=b.currency order by a.account_no";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modAccBankAccount model = new modAccBankAccount();
                        model.AccountNo=dalUtility.ConvertToString(rdr["account_no"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.TaxFlag=dalUtility.ConvertToInt(rdr["tax_flag"]);
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
        /// get table record
        /// <summary>
        /// <param name=accountno>accountno</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of accbankaccount</returns>
        public modAccBankAccount GetItem(string accountno,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select a.account_no,a.bank_name,a.currency,b.exchange_rate,a.tax_flag,a.update_user,a.update_time from acc_bank_account a inner join acc_currency_list b on a.currency=b.currency order where account_no='{0}' by a.account_no", accountno);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modAccBankAccount model = new modAccBankAccount();
                        model.AccountNo=dalUtility.ConvertToString(rdr["account_no"]);
                        model.BankName=dalUtility.ConvertToString(rdr["bank_name"]);
                        model.Currency=dalUtility.ConvertToString(rdr["currency"]);
                        model.ExchangeRate = dalUtility.ConvertToDecimal(rdr["exchange_rate"]);
                        model.TaxFlag=dalUtility.ConvertToInt(rdr["tax_flag"]);
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
        /// insert a accbankaccount
        /// <summary>
        /// <param name=mod>model object of accbankaccount</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modAccBankAccount mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into acc_bank_account(account_no,bank_name,currency,tax_flag,update_user,update_time)values('{0}','{1}','{2}',{3},'{4}',getdate())",mod.AccountNo,mod.BankName,mod.Currency,mod.TaxFlag,mod.UpdateUser);
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
        /// update a accbankaccount
        /// <summary>
        /// <param name=accountno>accountno</param>
        /// <param name=mod>model object of accbankaccount</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string accountno,modAccBankAccount mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update acc_bank_account set bank_name='{0}',currency='{1}',tax_flag={2},update_user='{3}',update_time=getdate() where account_no='{4}'",mod.BankName,mod.Currency,mod.TaxFlag,mod.UpdateUser,accountno);
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
        /// delete a accbankaccount
        /// <summary>
        /// <param name=accountno>accountno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Delete(string accountno,out string emsg)
        {
            try
            {
                string sql = string.Format("delete acc_bank_account where account_no='{0}' ",accountno);
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
        /// <param name=accountno>accountno</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string accountno, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from acc_bank_account where account_no='{0}' ",accountno);
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
