using System;
using System.Drawing;
using System.Management;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using BindingCollection;
using LXMS.Model;
using LXMS.DAL;
using Caches;

namespace LXMS
{
    class clsLxms
    {
        public static ImageList GetImageList()
        {
            ImageList imglist = new ImageList();
            imglist.TransparentColor = System.Drawing.Color.Transparent;
            imglist.Images.Add("NodeLevel1", Properties.Resources.NodeLevel1);
            imglist.Images.Add("NodeLevel1_Sel", Properties.Resources.NodeLevel1_Sel);
            imglist.Images.Add("NodeLevel2", Properties.Resources.NodeLevel2);
            imglist.Images.Add("NodeLevel2_Sel", Properties.Resources.NodeLevel2_Sel);
            return imglist;
        }

        public static void SetFormStatus(Form frm)
        {
            dalTaskGrant bll = new dalTaskGrant();
            BindingCollection<modTaskGrant> grantlist = bll.GetUserGrantData(true, false, Util.UserId, string.Empty, string.Empty, out Util.emsg);
            foreach (Control ctl in frm.Controls)
            {
                if (ctl.GetType().ToString() == "LXMS.ButtonEx" || ctl.GetType().ToString() == "System.Windows.Forms.LinkLabel")
                {
                    ctl.Enabled = false;
                    foreach (modTaskGrant mod in grantlist)
                    {
                        if (mod.Url.CompareTo(ctl.Name) == 0)
                        {
                            ctl.Enabled = true;
                            break;
                        }
                    }
                }                
                else
                    SetSubControlStatus(ctl, grantlist);
            }
        }

        public static void SetSubControlStatus(Control ctl, BindingCollection<modTaskGrant> grantlist)
        {
            if (ctl.Controls.Count > 0)
            {
                foreach (Control ch in ctl.Controls)
                {
                    if (ch.GetType().ToString() == "LXMS.ButtonEx" || ch.GetType().ToString() == "System.Windows.Forms.LinkLabel")
                    {
                        ch.Enabled = false;
                        foreach (modTaskGrant mod in grantlist)
                        {
                            if (mod.Url.CompareTo(ch.Name) == 0)
                            {
                                ch.Enabled = true;
                                break;
                            }
                        }
                    }                    
                    else
                        SetSubControlStatus(ch, grantlist);
                }
            }            
        }

        public static string GetParameterValue(string paraid)
        {
            dalSysParameters bllpara = new dalSysParameters();
            modSysParameters modpara = bllpara.GetItem(paraid, out Util.emsg);
            if (modpara != null)
                return modpara.ParaValue;
            else
                return string.Empty;
        }

        public static string GetProductImagePath(string imgpath)
        {
            if (File.Exists(imgpath)) 
                return imgpath;
            else
            {
                string folder = clsLxms.GetParameterValue("PRODUCT_IMAGE_PATH");
                if (!string.IsNullOrEmpty(folder))
                {
                    string path = @folder + imgpath;
                    if (File.Exists(path))
                        return path;
                    else
                    {
                        path = @folder + "\\" + imgpath;
                        if (File.Exists(path))
                            return path;
                        else
                        {
                            path = (folder + imgpath).Replace("\\\\","\\");
                            if (File.Exists(path))
                                return path;
                            else
                                return string.Empty;
                        }
                    }
                }
                return string.Empty;
            }
        }

        public static string GetEmployeeImagePath(string imgpath)
        {
            if (File.Exists(imgpath))
                return imgpath;
            else
            {
                string folder = clsLxms.GetParameterValue("EMPLOYEE_IMAGE_PATH");
                if (!string.IsNullOrEmpty(folder))
                {
                    string path = @folder + imgpath;
                    if (File.Exists(path))
                        return path;
                    else
                    {
                        path = @folder + "\\" + imgpath;
                        if (File.Exists(path))
                            return path;
                        else
                        {
                            path = (folder + imgpath).Replace("\\\\", "\\");
                            if (File.Exists(path))
                                return path;
                            else
                                return string.Empty;
                        }
                    }
                }
                return string.Empty;
            }
        }

        public static string GetDefaultUnitNo()
        {
            return GetParameterValue("DEFAULT_UNIT_NO");            
        }

        public static decimal GetDataBaseQty()
        {
            decimal ret = 10000;
            string retstr = GetParameterValue("DATA_BASE_QTY");
            if (!string.IsNullOrEmpty(retstr))
                ret = Convert.ToDecimal(retstr);

            return ret;
        }

        public static int ShowProductSpecify()
        {
            int ret = 0;
            string retstr = GetParameterValue("SHOW_PRODUCT_SPECIFY");
            if (!string.IsNullOrEmpty(retstr))
                ret = Convert.ToInt32(retstr);

            return ret;
        }

        public static int ShowProductSize()
        {
            int ret = 0;
            string retstr = GetParameterValue("SHOW_PRODUCT_SIZE");
            if (!string.IsNullOrEmpty(retstr))
                ret = Convert.ToInt32(retstr);

            return ret;
        }

        public static string GetDefaultWarehouseId()
        {
            string defaultWarehouseId = string.Empty;
            string key = CacheKeys.DefaultWarehouseId.ToString();
            object obj = Cache.Get(key);
            if (obj == null)
            {
                dalWarehouseList dal = new dalWarehouseList();
                defaultWarehouseId = dal.GetDefaultWarehouseId(out Util.emsg);
                Cache.Set(key, defaultWarehouseId);
            }
            else
            {
                defaultWarehouseId = obj.ToString();
            }
            return defaultWarehouseId;
        }

        public static string GetDefaultCustId()
        {
            string defaultCustId = string.Empty;
            string key = CacheKeys.DefaultCustId.ToString();
            object obj = Cache.Get(key);
            if (obj == null)
            {
                dalCustomerList dal = new dalCustomerList();
                defaultCustId = dal.GetCustIdByName("散客");
                Cache.Set(key, defaultCustId);
            }
            else
            {
                defaultCustId = obj.ToString();
            }
            return defaultCustId;
        }

        public static FontStyle GetFontStyle(string style)
        {
            switch (style.ToLower())
            {
                case "bold":
                    return FontStyle.Bold;
                case "italic":
                    return FontStyle.Italic;
                case "strikeout":
                    return FontStyle.Strikeout;
                case "underline":
                    return FontStyle.Underline;
                default:
                    return FontStyle.Regular;
            }
        }

        public static void ShowImportTemplete(string TempleteName)
        {
            try
            {                
                string filename = clsLxms.GetParameterValue("EXCEL_TEMPLETE_FILE");
                if (!File.Exists(filename))
                {
                    MessageBox.Show(clsTranslate.TranslateString("Can not find the templete file") + ": \r\n" + filename, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string destfile = @Path.GetTempPath() + TempleteName + ".xls";
                Util.retValue1 = destfile;
                File.Copy(filename, destfile, true);
                Excel.Application m_objExcel = new Excel.Application();
                m_objExcel.DisplayAlerts = false;
                Excel.Workbooks m_objBooks = m_objExcel.Workbooks;
                m_objBooks.Open(destfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Excel.Workbook m_objBook = (Excel.Workbook)m_objBooks.get_Item(1);
                Excel.Sheets sm_objSheets = (Excel.Sheets)m_objBook.Worksheets;

                Excel.Worksheet m_objSheet = (Excel.Worksheet)sm_objSheets.get_Item(TempleteName);
                for (int i = sm_objSheets.Count; i >= 1; i--)
                {
                    Excel.Worksheet m_Sheet = sm_objSheets.get_Item(i);
                    if (m_Sheet.Name.ToLower().CompareTo(TempleteName.ToLower()) != 0)
                        m_Sheet.Delete();
                }
                m_objSheet.Name = "Sheet1";
                //m_objSheet.Activate();
                System.Windows.Forms.Application.DoEvents();
                m_objExcel.DisplayAlerts = false;
                m_objExcel.AlertBeforeOverwriting = false;
                //保存工作簿 
                m_objExcel.Visible = true;
                return;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, clsTranslate.TranslateString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            } 
        }
        
        public static bool CheckSoftwareRegister()
        {
            Util.HOST_CODE = clsLxms.getCpu() + clsLxms.GetDiskVolumeSerialNumber();
            Util.REGISTER_CODE = Util.Encrypt(Util.HOST_CODE, Util.PWD_MASK);
            Util.SOFT_REGISTER = false;
            RWReg reg=new RWReg("CURRENT_USER");
            string regvalue = reg.GetRegValue(@"software\HBERP", Util.HOST_CODE, string.Empty);
            if (!string.IsNullOrEmpty(regvalue))
            {
                if (Util.REGISTER_CODE.CompareTo(regvalue) == 0)
                {
                    Util.SOFT_REGISTER = true;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        //获得CPU的序列号
        public static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        //取得设备硬盘的卷标号
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"d:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        public static decimal GetExchangeRate(string accountno)
        {
            dalAccBankAccount dal = new dalAccBankAccount();
            modAccBankAccount mod = dal.GetItem(accountno, out Util.emsg);
            if (mod != null)
            {
                dalAccCurrencyList dal2 = new dalAccCurrencyList();
                modAccCurrencyList mod2 = dal2.GetItem(mod.Currency, out Util.emsg);
                if (mod2 != null)
                    return mod2.ExchangeRate;
                else
                    return 1;
            }
            else
                return 1;
        }

        public static void SetEmployeeId(TextBox box)
        {
            MTN_EMPLOYEE_LIST frm = new MTN_EMPLOYEE_LIST();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                box.Text = Util.retValue1;
            }
        }

        public static void SetEmployeeName(TextBox box)
        {
            MTN_EMPLOYEE_LIST frm = new MTN_EMPLOYEE_LIST();
            frm.SelectVisible = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                box.Text = Util.retValue2;
            }
        }

        public static decimal GetPrice(string productid)
        {
            dalAccProductInout dal = new dalAccProductInout();
            return dal.GetPrice(Util.modperiod.AccName, productid, out Util.emsg);
        }

        public static int GetProductSizeFlag(string productid)
        {
            dalProductList dal = new dalProductList();
            modProductList mod = dal.GetItem(productid, out Util.emsg);
            if (mod != null)
            {
                return mod.SizeFlag;
            }
            else
            {
                return 0;
            }
        }
    }
}
