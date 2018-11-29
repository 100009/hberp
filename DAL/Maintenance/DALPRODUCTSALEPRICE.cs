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
    public class dalProductSalePrice
    {        
        /// <summary>
        /// get all productsaleprice
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productsaleprice</returns>
        public BindingCollection<modProductSalePrice> GetIList(string productid, out string emsg)
        {
            try
            {
                BindingCollection<modProductSalePrice> modellist = new BindingCollection<modProductSalePrice>();
                //Execute a query to read the categories
                string sql = string.Format("select product_id,cust_level,price,update_user,update_time from product_sale_price where product_id='{0}' order by product_id,cust_level",productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductSalePrice model = new modProductSalePrice();
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.CustLevel=dalUtility.ConvertToString(rdr["cust_level"]);
                        model.Price=dalUtility.ConvertToDecimal(rdr["price"]);
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
        /// <param name=productid>productid</param>
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productsaleprice</returns>
        public modProductSalePrice GetItem(string productid,string custlevel,out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select product_id,cust_level,price,update_user,update_time from product_sale_price where product_id='{0}' and cust_level='{1}' order by product_id,cust_level",productid,custlevel);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductSalePrice model = new modProductSalePrice();
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.CustLevel=dalUtility.ConvertToString(rdr["cust_level"]);
                        model.Price=dalUtility.ConvertToDecimal(rdr["price"]);
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
        /// get all productsaleprice
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productsaleprice</returns>
        public BindingCollection<modProductSalePrice> GetProductSalePrice(string productid, out string emsg)
        {
            try
            {
                BindingCollection<modProductSalePrice> modellist = new BindingCollection<modProductSalePrice>();
                //Execute a query to read the categories
                string sql = string.Format("select b.product_id,a.cust_level,isnull(b.price, 0) price from customer_level a left join "
                        + "(select * from product_sale_price where product_id='{0}') b on a.cust_level=b.cust_level order by b.product_id,a.cust_level", productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductSalePrice model = new modProductSalePrice();
                        model.ProductId = productid;
                        model.CustLevel = dalUtility.ConvertToString(rdr["cust_level"]);
                        model.Price = dalUtility.ConvertToDecimal(rdr["price"]);                        
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
        /// get cust product price
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=custid>custid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productsaleprice</returns>
        public decimal GetPrice(string productid, string custid, out string emsg)
        {
            try
            {

                //Execute a query to read the categories
                string sql = string.Format("select price from product_sale_price a inner join customer_list b on a.cust_level=b.cust_level where a.product_id='{0}' and b.cust_id='{1}' order by a.product_id,b.cust_level", productid, custid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        emsg = null;
                        return dalUtility.ConvertToDecimal(rdr["price"]);
                    }
                    else
                    {
                        emsg = "No data found";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// get default product price
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productsaleprice</returns>
        public decimal GetDefaultPrice(string productid, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select price from product_sale_price where product_id='{0}' and cust_level='{1}'", productid, "A");
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        emsg = null;
                        return dalUtility.ConvertToDecimal(rdr["price"]);
                    }
                    else
                    {
                        emsg = "No data found";
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// insert a productsaleprice
        /// <summary>
        /// <param name=mod>model object of productsaleprice</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Save(BindingCollection<modProductSalePrice> list,out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = string.Format("delete product_sale_price where product_id='{0}'", list[0].ProductId);
                    SqlHelper.ExecuteNonQuery(sql);
                    foreach(modProductSalePrice mod in list)
                    { 
                        sql = string.Format("insert into product_sale_price(product_id,cust_level,price,update_user,update_time)values('{0}','{1}','{2}','{3}',getdate())", mod.ProductId, mod.CustLevel, mod.Price, mod.UpdateUser);
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

        /// <summary>
        /// update a productsaleprice
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=custlevel>custlevel</param>
        /// <param name=mod>model object of productsaleprice</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string productid,string custlevel,modProductSalePrice mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update product_sale_price set price='{0}',update_user='{1}',update_time=getdate() where product_id='{2}' and cust_level='{3}'",mod.Price,mod.UpdateUser,productid,custlevel);
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
		/// delete a productsaleprice
		/// <summary>
		/// <param name=productid>productid</param>
		/// <param name=custlevel>custlevel</param>
		/// <param name=out emsg>return error message</param>
		/// <returns>true/false</returns>
		public bool Delete(string productid,string custlevel,out string emsg)
        {
            try
            {
                string sql = string.Format("delete product_sale_price where product_id='{0}' and cust_level='{1}' ",productid,custlevel);
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
        /// <param name=productid>productid</param>
        /// <param name=custlevel>custlevel</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string productid,string custlevel, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from product_sale_price where product_id='{0}' and cust_level='{1}' ",productid,custlevel);
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
