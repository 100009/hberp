using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using LXMS.DBUtility;

namespace LXMS.DAL
{
    public class dalUtility
    {
        public static string ErrorMessage(string err)
        {
            string emsg = string.Empty;
            //bool ret = SaveErrorLog(userid, err, "LXMS", userid, out emsg);            
            if (err.IndexOf("Violation of PRIMARY KEY") >= 0)
                return "数据已存在!\r\n" + err;
            else if (err.IndexOf("with unique index") >= 0)
                return "数据重复!\r\n" + err;
            else if (err.IndexOf("FK_") >= 0 && err.IndexOf("conflict") >= 0)
                return "数据外键冲突!\r\n" + err;
            else
            {
                return err;
            }            
        }

        /// <summary>
        /// SaveErrorLog
        /// <summary>
        /// <param name=puserid>puserid</param>
        /// <param name=perrtext>perrtext</param>
        /// <param name=pprogram>pprogram</param>
        /// <param name=pUpdateUser>pUpdateUser</param>
        /// <param name=emsg>return error message</param>
        /// <returns>true/false</returns>
        public static bool SaveErrorLog(string puserid, string perrtext, string pprogram, string pUpdateUser, out string emsg)
        {
            try
            {
                string sql = string.Format("insert into sys_error_log(user_id,err_text,program,update_user,update_time)values('{0}','{1}','{2}','{3}',GETDATE())", puserid, perrtext, pprogram, pUpdateUser);
                int i = SqlHelper.ExecuteNonQuery(sql);
                emsg = string.Empty;
                return i == 1 ? true : false;
            }
            catch (Exception ex)
            {
                emsg = dalUtility.ErrorMessage(ex.Message);
                return false;
            }
        }

        public static string ConvertToString(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return string.Empty;
            else
                return obj.ToString();
        }

        public static int ConvertToInt(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return -1;
            else
                return Convert.ToInt32(obj.ToString());
        }

        public static DateTime ConvertToDateTime(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return Convert.ToDateTime("0001-1-1");
            else
                return Convert.ToDateTime(obj.ToString());
        }

        public static DateTime ConvertToDate(object obj)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", false);
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return Convert.ToDateTime("0001-1-1", culture);
            else
                return Convert.ToDateTime(obj.ToString(), culture);
        }

        public static decimal ConvertToDecimal(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return -1;
            else
                return Decimal.Parse(subZeroAndDot(obj.ToString()));
        }

        public static string subZeroAndDot(string s)
        {   
            if(s.IndexOf(".") > 0){
                string[] a = s.Split('.');
                int idx=a[1].Length;
                for (int i = a[1].Length - 1; i >= 0; i--)
                {
                    if (a[1].Substring(i,1) != "0")
                        break;
                    idx--;
                }
                if (idx >= 1)
                {
                    string tmp = a[1].Substring(0, idx);
                    return a[0] + "." + tmp;
                }
                else
                    return a[0];                
            }   
            else
                return s;   
        }   

        public static Single ConvertToSingle(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return -1;
            else
                return Convert.ToSingle(obj);
        }

        public static double ConvertToDouble(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return -1;
            else
                return Convert.ToDouble(obj);
        }

        public static bool ConvertToBoolean(object obj)
        {
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                return false;
            else
                return Convert.ToBoolean(obj);
        }

        public static string GetUTF8String(object o)
        {
            if (o != null)
                return System.Text.Encoding.GetEncoding(936).GetString(System.Text.Encoding.GetEncoding(1252).GetBytes(o.ToString()));
            else
                return string.Empty;
        }
        
        public static string ConvertStr(string str)
        {
            return System.Text.Encoding.GetEncoding("Big5").GetString(System.Text.Encoding.GetEncoding("GB2312").GetBytes(str));
        }

        public static string ISO8859_GB2312(string read)
        {
            if (!string.IsNullOrEmpty(read))
            {
                System.Text.Encoding WIN1252 = System.Text.Encoding.GetEncoding(1252);
                System.Text.Encoding GB2312 = System.Text.Encoding.Default;
                byte[] iso = GB2312.GetBytes(read);
                return WIN1252.GetString(iso);
            }
            else
            {
                return string.Empty;
            }
        }

        public bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static bool IsInt(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        public static bool IsUnsign(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        public static bool IsEmailAddr(string emailaddr)
        {
            int i, j;
            string strTmp, strResult;
            string strWords = "abcdefghijklmnopqrstuvwxyz_-.0123456789";
            //定义合法字符范围   
            bool blResult = true;
            strTmp = emailaddr.Trim();
            //检测输入字符串是否为空，不为空时才执行代码。   
            if (!(strTmp == "" || strTmp.Length == 0))
            {
                //判断邮件地址中是否存在“@”号      
                if ((strTmp.IndexOf("@") < 0))
                {
                    blResult = false;
                    return blResult;
                }
                //以“@”号为分割符，把地址切分成两部分，分别进行验证。      
                string[] strChars = strTmp.Split(new char[] { '@' });
                foreach (string strChar in strChars)
                {
                    i = strChar.Length;
                    //“@”号前部分或后部分为空时。          
                    if (i == 0)
                    {
                        blResult = false;
                        return blResult;
                    }
                    //逐个字进行验证，如果超出所定义的字符范围strWords，则表示地址非法。          
                    for (j = 0; j < i; j++)
                    {
                        strResult = strChar.Substring(j, 1).ToLower();
                        //逐个字符取出比较              
                        if (strWords.IndexOf(strResult) < 0)
                        {
                            blResult = false;
                            return blResult;
                        }
                    }
                }
                //验证@后面的点的位置      
                var nDotPos = strChars[1].ToString().IndexOf(".");
                if (nDotPos < 3)
                {
                    blResult = false;
                    return blResult;
                }
                if (nDotPos == strChars[1].ToString().Length)
                {
                    blResult = false;
                    return blResult;
                }
            }
            return blResult;
        }
    }

    [Serializable]
    public class modSysErrorLog
    {
        string _userid;
        string _hostname;
        string _errtext;
        string _program;
        string _UpdateUser;
        DateTime _updatetime;

        public modSysErrorLog() { }

        public modSysErrorLog(string user_id, string host_name, string err_text, string program, string update_user, DateTime update_time)
        {
            //Convert.ToString(rdr[0]),Convert.ToString(rdr[1]),Convert.ToString(rdr[2]),Convert.ToString(rdr[3]),Convert.ToString(rdr[4]),Convert.ToDateTime(rdr[5])
            //dalUtility.ConvertToString(rdr[0]),dalUtility.ConvertToString(rdr[1]),dalUtility.ConvertToString(rdr[2]),dalUtility.ConvertToString(rdr[3]),dalUtility.ConvertToString(rdr[4]),dalUtility.ConvertToDateTime(rdr[5])
            //dalUtility.ConvertToString(rdr["user_id"]),dalUtility.ConvertToString(rdr["host_name"]),dalUtility.ConvertToString(rdr["err_text"]),dalUtility.ConvertToString(rdr["program"]),dalUtility.ConvertToString(rdr["update_user"]),dalUtility.ConvertToDateTime(rdr["update_time"])
            //user_id,host_name,err_text,program,update_user,update_time
            _userid = user_id;
            _hostname = host_name;
            _errtext = err_text;
            _program = program;
            _UpdateUser = update_user;
            _updatetime = update_time;
        }

        public string UserId
        {
            get { return _userid; }
            set { _userid = value; }
        }
        public string HostName
        {
            get { return _hostname; }
            set { _hostname = value; }
        }
        public string ErrText
        {
            get { return _errtext; }
            set { _errtext = value; }
        }
        public string Program
        {
            get { return _program; }
            set { _program = value; }
        }
        public string UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }
    }
}