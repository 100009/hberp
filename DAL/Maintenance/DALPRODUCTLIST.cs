using System;
using System.Collections;
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
    public class dalProductList
    {
        /// <summary>
        /// get all productlist
        /// <summary>
        /// <param name=producttypelist>producttypelist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productlist</returns>
        public BindingCollection<modProductList> GetIList(string producttypelist, string product, out string emsg)
        {
            try
            {
                string producttypewhere = string.Empty;
                if (producttypelist.CompareTo("临时") == 0)
                    producttypewhere = "and product_type = '临时' ";
                else if (!string.IsNullOrEmpty(producttypelist) && producttypelist != "ALL")
                    producttypewhere = "and product_type in ('" + producttypelist.Replace(",", "','") + "') ";

				string productwhere = string.Empty;
				if (!string.IsNullOrEmpty(product))
					productwhere = "and (product_id like '%" + product  + "% ' or product_name like '%" + product + "%') ";
				
				BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                string sql = "select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time "
					+ "from product_list where status<>7 " + producttypewhere + productwhere  + "order by product_type,product_id";

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType=dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag=dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify=dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// get all productlist
        /// <summary>
        /// <param name=producttypelist>producttypelist</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productlist</returns>
        public BindingCollection<modProductList> GetIListByBarcode(string barcode, out string emsg)
        {
            try
            {                
                BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                string sql = string.Format("select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time "
                    + "from product_list where status<>7 and barcode = '{0}' order by product_type,product_id", barcode);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// get productlist by page
        /// <summary>
        /// <param name=currentPage>currentPage</param>
        /// <param name=pagesize>pagesize</param>
        /// <param name=productname>productname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productlist</returns>
        public BindingCollection<modProductList> GetIList(int currentPage, int pagesize, string productname, out string emsg)
        {
            try
            {
                string productnamewhere = string.Empty;
                if (!string.IsNullOrEmpty(productname))
                    productnamewhere = "and product_name like '" + productname + "%' ";

                BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                int startindex = (currentPage - 1) * pagesize + 1;
                int endindex = currentPage * pagesize;
                string sql = "select row_number() over(order by product_id) as rn,product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time from product_list where status<>7 " + productnamewhere;
                string sql2 = "select count(1) from (" + sql + ") t";
                sql = string.Format("select * from (" + sql + ") t where rn>='{0}' and rn<='{1}'", startindex, endindex);

                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productlist</returns>
        public modProductList GetItem(string productid, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time from product_list where product_id='{0}' order by product_type,product_id", productid);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId=dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName=dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType=dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag=dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify=dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo=dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark=dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// <param name=productname>productname</param>
        /// <param name=out emsg>return error message</param>
        ///<returns>get a record detail of productlist</returns>
        public modProductList GetItembyName(string productname, out string emsg)
        {
            try
            {
                //Execute a query to read the categories
                string sql = string.Format("select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time from product_list where product_name='{0}' order by product_type,product_id", productname);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// get brand list
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>get brand list in productlist</returns>
        public ArrayList GetBrandList(out string emsg)
        {
            try
            {
                ArrayList arr = new ArrayList();
                emsg = string.Empty;
                //Execute a query to read the categories
                string sql = "select distinct brand from product_list order by brand";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        arr.Add(rdr[0].ToString());
                    }
                    return arr;
                }
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// get useless product
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productlist</returns>
        public BindingCollection<modProductList> GetUselessProduct(string accname, out string emsg)
        {
            try
            {
                BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                int inoutdays=30;
                dalSysParameters dalpara = new dalSysParameters();
                modSysParameters modpara = dalpara.GetItem("PRODUCT_CLEAR_DAYS", out emsg);
                if (!string.IsNullOrEmpty(modpara.ParaValue) && Convert.ToInt32(modpara.ParaValue) >= inoutdays)
                    inoutdays = Convert.ToInt32(modpara.ParaValue);

                dalAccPeriodList dalp = new dalAccPeriodList();
                modAccPeriodList modp = dalp.GetItem(accname, out emsg);

                string sql = string.Format(@"select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time 
							 from product_list a
							where status<>7 and update_time<=getdate()-7 
							  and not exists(select '#' from acc_product_inout where acc_name='{0}' and product_id=a.product_id)
							  and not exists(select '#' from warehouse_product_inout where product_id=a.product_id and (inout_date>=getdate()-{1} or inout_date>='{2}'))
							order by update_time", accname, inoutdays, modp.StartDate);
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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
        /// get clear productlist
        /// <summary>
        /// <param name=out emsg>return error message</param>
        ///<returns>details of all productlist</returns>
        public BindingCollection<modProductList> GetClearProduct(out string emsg)
        {
            try
            {                
                BindingCollection<modProductList> modellist = new BindingCollection<modProductList>();
                //Execute a query to read the categories
                string sql = "select product_id,product_name,product_type,barcode,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time from product_list where status=7 order by product_type,product_id";
                using (SqlDataReader rdr = SqlHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        modProductList model = new modProductList();
                        model.ProductId = dalUtility.ConvertToString(rdr["product_id"]);
                        model.ProductName = dalUtility.ConvertToString(rdr["product_name"]);
                        model.ProductType = dalUtility.ConvertToString(rdr["product_type"]);
                        model.Barcode = dalUtility.ConvertToString(rdr["barcode"]);
                        model.SizeFlag = dalUtility.ConvertToInt(rdr["size_flag"]);
                        model.Specify = dalUtility.ConvertToString(rdr["specify"]);
                        model.Brand = dalUtility.ConvertToString(rdr["brand"]);
                        model.UnitNo = dalUtility.ConvertToString(rdr["unit_no"]);
                        model.Remark = dalUtility.ConvertToString(rdr["remark"]);
                        model.MinQty = dalUtility.ConvertToDecimal(rdr["min_qty"]);
                        model.MaxQty = dalUtility.ConvertToDecimal(rdr["max_qty"]);
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

        public bool DeleteUselessProduct(string productlist, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = "update product_list set status=7 where product_id in ('" + productlist.Replace(",", "','") + "')";
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

        public bool RestoreUselessProduct(string productlist, out string emsg)
        {
            using (TransactionScope transaction = new TransactionScope())//使用事务            
            {
                try
                {
                    string sql = "update product_list set status=1 where product_id in ('" + productlist.Replace(",", "','") + "')";
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
        /// insert a productlist
        /// <summary>
        /// <param name=mod>model object of productlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Insert(modProductList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("insert into product_list(product_id,product_name,product_type,status,size_flag,specify,brand,unit_no,remark,min_qty,max_qty,update_user,update_time,barcode)values('{0}','{1}','{2}',1,{3},'{4}','{5}','{6}','{7}',{8},{9},'{10}',getdate(),'{11}')", mod.ProductId, mod.ProductName, mod.ProductType, mod.SizeFlag, mod.Specify, mod.Brand, mod.UnitNo, mod.Remark, mod.MinQty, mod.MaxQty, mod.UpdateUser, mod.Barcode);
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
        /// update a productlist
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=mod>model object of productlist</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Update(string productid,modProductList mod,out string emsg)
        {
            try
            {
                string sql = string.Format("update product_list set product_name='{0}',product_type='{1}',size_flag={2},specify='{3}',brand='{4}',unit_no='{5}',remark='{6}',min_qty={7},max_qty={8},update_user='{9}',update_time=getdate(), barcode='{10}', status=1 where product_id='{11}'", mod.ProductName, mod.ProductType, mod.SizeFlag, mod.Specify, mod.Brand, mod.UnitNo, mod.Remark, mod.MinQty, mod.MaxQty, mod.UpdateUser, mod.Barcode, productid);
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
		/// update a productsaleprice
		/// <summary>
		/// <param name=productid>productid</param>
		/// <param name=custlevel>custlevel</param>
		/// <param name=mod>model object of productsaleprice</param>
		/// <param name=out emsg>return error message</param>
		/// <returns>true/false</returns>
		public bool UpdateProductType(string productId, string productType, string updateUser, out string emsg)
		{
			try
			{
				string sql = string.Format("update product_list set product_type='{0}',update_user='{1}',update_time=getdate() where product_id='{2}'", productType, updateUser, productId);
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
		/// delete a productlist
		/// <summary>
		/// <param name=productid>productid</param>
		/// <param name=out emsg>return error message</param>
		/// <returns>true/false</returns>
		public bool Delete(string productid,out string emsg)
        {
            try
            {
                string sql = string.Format("delete product_list where product_id='{0}' ",productid);
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
        /// change productlist's status(valid/invalid)
        /// <summary>
        /// <param name=productid>productid</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Inactive(string productid,out string emsg)
        {
            try
            {
                string sql = string.Format("update product_list set status=1-status where product_id='{0}' ",productid);
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
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool Exists(string productid, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from product_list where product_id='{0}' ",productid);
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
        /// record exist or not
        /// <summary>
        /// <param name=productname>productname</param>
        /// <param name=out emsg>return error message</param>
        /// <returns>true/false</returns>
        public bool ExistsProductName(string productname, out string emsg)
        {
            try
            {
                string sql = string.Format("select count(1) from product_list where product_name='{0}' ", productname);
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
