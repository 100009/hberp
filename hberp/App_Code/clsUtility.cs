using System;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using unoidl.com.sun.star.lang; 
using unoidl.com.sun.star.uno; 
using unoidl.com.sun.star.bridge; 
using unoidl.com.sun.star.frame; 
using BindingCollection;
using LXMS.DBUtility;
using LXMS.DBUtility.Common;
using LXMS.Model;

namespace LXMS
{
    class Util
    {
        public static bool IsTrialBalance = false;
        public static string language="1";
        public static string UserId;
        public static string UserName;
        public static string RoleId;
        public static string RoleDesc;
        public static modAccPeriodList modperiod;
        public static int? UserStatus;
        public static int? AlarmTimeInterval;
        public static string Currency;

        public static string retValue1;
        public static string retValue2;
        public static string retValue3;
        public static string retValue4;
        public static string retValue5;
        public static DateTime retTime;
        public static string emsg = string.Empty;
        public static string hostname = string.Empty;
        public const string PWD_MASK = "#4c#2@!";
        public const string INI_FILE = @"c:\HongBiao\HBERP.ini";
        //public static string SKINPATH = Application.StartupPath + @"\ssk\";
        public static string HOST_CODE = string.Empty;
        public static string REGISTER_CODE = string.Empty;
        public static bool SOFT_REGISTER = false;
        //public static string TEMPLETE_FILE = Application.StartupPath + @"\excel\templetes.xls";
        public static string JASON_MESSAGE_PATH = Directory.GetParent(Application.StartupPath) + @"\jasonmessage\";
        private const int MODE_HEAD = 1;
        private const int MODE_CONTENT = 2;
        private const int MODE_GRID = 3;
                
        public static void ChangeStatus(Form frm, bool read)
        {
            foreach (Control ct in frm.Controls)
            {
                switch (ct.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                    case "LXMS.WatermarkTextBox":
                        TextBox tb = (TextBox)ct;
                        tb.ReadOnly = read;
                        break;
                    case "System.Windows.Forms.ComboBox":
                        ComboBox cb = (ComboBox)ct;
                        cb.Enabled = !read;
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        DateTimePicker dt = (DateTimePicker)ct;
                        dt.Enabled = !read;
                        break;
                    case "System.Windows.Forms.Button":
                        Button btn = (Button)ct;
                        btn.Enabled = !read;
                        break;
                    case "System.Windows.Forms.CheckBox":
                        CheckBox box = (CheckBox)ct;
                        box.Enabled = !read;
                        break;
                    case "System.Windows.Forms.RadioButton":
                        RadioButton rb = (RadioButton)ct;
                        rb.Enabled = !read;
                        break;
                    default:
                        ChangeControlStatus(ct, read);
                        break;
                }
            }
        }

        private static void ChangeControlStatus(Control ct, bool read)
        {
            if (ct.Controls.Count > 0)
            {
                foreach (Control ctt in ct.Controls)
                {
                    switch (ctt.GetType().ToString())
                    {
                        case "System.Windows.Forms.TextBox":
                        case "LXMS.WatermarkTextBox":
                            TextBox tb = (TextBox)ctt;
                            tb.ReadOnly = read;
                            break;
                        case "System.Windows.Forms.ComboBox":
                            ComboBox cb = (ComboBox)ctt;
                            cb.Enabled = !read;
                            break;
                        case "System.Windows.Forms.DateTimePicker":
                            DateTimePicker dt = (DateTimePicker)ctt;
                            dt.Enabled = !read;
                            break;
                        case "System.Windows.Forms.Button":
                            Button btn = (Button)ctt;
                            btn.Enabled = !read;
                            break;
                        case "System.Windows.Forms.CheckBox":
                            CheckBox box = (CheckBox)ctt;
                            box.Enabled = !read;
                            break;
                        case "System.Windows.Forms.RadioButton":
                            RadioButton rb = (RadioButton)ctt;
                            rb.Enabled = !read;
                            break;
                        case "System.Windows.Forms.DataGridView":
                            DataGridView dbgrid = (DataGridView)ctt;
                            dbgrid.ReadOnly = read;
                            break;
                        case "LXMS.RowMergeView":
                            RowMergeView rmgrid = (RowMergeView)ctt;
                            rmgrid.ReadOnly = read;
                            break;
                        default:
                            ChangeControlStatus(ctt, read);
                            break;
                    }
                }
            }
        }

        public static void EmptyFormBox(Form frm)
        {
            foreach (Control ct in frm.Controls)
            {
                switch (ct.GetType().ToString())
                {
                    case "System.Windows.Forms.TextBox":
                    case "LXMS.WatermarkTextBox":
                        TextBox tb = (TextBox)ct;
                        tb.Text = string.Empty;
                        break;
                    case "System.Windows.Forms.ComboBox":
                        ComboBox cb = (ComboBox)ct;
                        cb.SelectedIndex=-1;
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        DateTimePicker dt = (DateTimePicker)ct;
                        dt.Value=DateTime.Now;
                        break;
                    case "System.Windows.Forms.ListBox":
                        ListBox lb = (ListBox)ct;
                        lb.Items.Clear();
                        break;
                    default:
                        EmptyControlBox(ct);
                        break;
                }
            }
        }

        private static void EmptyControlBox(Control ct)
        {
            if (ct.Controls.Count > 0)
            {
                foreach (Control ctt in ct.Controls)
                {
                    switch (ctt.GetType().ToString())
                    {
                        case "System.Windows.Forms.TextBox":
                        case "LXMS.WatermarkTextBox":
                            TextBox tb = (TextBox)ctt;
                            tb.Text = string.Empty;
                            break;
                        case "System.Windows.Forms.ComboBox":
                            ComboBox cb = (ComboBox)ctt;
                            cb.SelectedIndex = -1;
                            break;
                        case "System.Windows.Forms.DateTimePicker":
                            DateTimePicker dt = (DateTimePicker)ctt;
                            dt.Value=DateTime.Now;
                            break;
                        case "System.Windows.Forms.ListBox":
                            ListBox lb = (ListBox)ctt;
                            lb.Items.Clear();
                            break;
                        default:
                            EmptyControlBox(ctt);
                            break;
                    }
                }
            }
        }

        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new System.Exception("Character is not valid.");
            }
        }

        public static decimal Max(decimal value1, decimal value2)
        {
            return value1 >= value2 ? value1 : value2;
        }

        public static int Max(int value1, int value2)
        {
            return value1 >= value2 ? value1 : value2;
        }

        public static decimal Min(decimal value1, decimal value2)
        {
            return value1 <= value2 ? value1 : value2;
        }

        public static int Min(int value1, int value2)
        {
            return value1 <= value2 ? value1 : value2;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();
        public static void Delay(uint ms)
        {
            uint start = GetTickCount();
            while (GetTickCount() - start < ms)
            {
                Application.DoEvents();
            }
        }

        public static void FlashRow(DataGridViewRow row)
        {
            if (row == null) return;

            int i = 0;
            while (i <= 5)
            {
                i++;
                if (i % 2 == 0)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                    Util.Delay(180);
                }
                else
                {
                    row.DefaultCellStyle.ForeColor = Color.Blue;
                    Util.Delay(180);
                }
            }
        }

        public static string GetTextFile(string filename)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.Append(line);
                        sb.Append("\r\n");
                    }
                }
                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static ArrayList ReadTextFile(string filename)
        {
            ArrayList arr = new ArrayList();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        arr.Add(line);
                    }
                }
                return arr;
            }
            catch
            {
                return null;
            }
        }

        public static void WriteTextFile(string filename, bool overwrite, string content)
        {
            try
            {
                string temp = string.Empty;
                if (!overwrite && File.Exists(filename))
                {
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        String line;
                        // Read and display lines from the file until the end of 
                        // the file is reached.
                        while ((line = sr.ReadLine()) != null)
                        {
                            temp += line + "\r\n";
                        }
                    }
                    temp += content;

                }
                else
                    temp = content;
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    sw.Write(temp);
                }
            }
            catch
            {
                return;
            }
        }

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
                if (nDotPos < 1)
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

        public static string FormatListStr(string liststr, string delimiter)
        {
            if (string.IsNullOrEmpty(liststr))
                return string.Empty;

            string temp = liststr;
            string dbldeli = delimiter + delimiter;
            do
            {
                temp = temp.Replace(dbldeli, delimiter);
            }
            while (temp.IndexOf(dbldeli) >= 0);

            if (temp.Substring(0, 1) == delimiter)
                temp = temp.Substring(1);
            if (!string.IsNullOrEmpty(temp) && temp.Substring(temp.Length - 1, 1) == delimiter)
                temp = temp.Substring(0, temp.Length - 1);
            return temp;
        }

        public static string FormatRepeatStr(string liststr, char delimiter)
        {
            if (string.IsNullOrEmpty(liststr))
                return string.Empty;

            string retstr = string.Empty;
            string strTemp = FormatListStr(liststr, delimiter.ToString());
            if(string.IsNullOrEmpty(strTemp))
                return retstr;

            string[] temp = strTemp.Split(delimiter);
            for (int i = 0; i < temp.Length; i++)
            {
                if (i == 0)
                    retstr = temp[i];
                else
                {
                    string str = delimiter + retstr + delimiter;
                    if (str.IndexOf(delimiter + temp[i] + delimiter) < 0)
                        retstr += delimiter + temp[i];
                }
            }
            return retstr;
        }

        public static string GetListBoxStr(ListBox box, string delimiter, bool onlyselected)
        {
            string temp = string.Empty;
            if (box.Items.Count > 0)
            {
                if (onlyselected)
                {
                    if (box.SelectedItems != null && box.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < box.Items.Count; i++)
                        {
                            if (string.IsNullOrEmpty(temp))
                                temp = box.SelectedItems[i].ToString();
                            else
                                temp += delimiter + box.SelectedItems[i].ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < box.Items.Count; i++)
                    {
                        if (string.IsNullOrEmpty(temp))
                            temp = box.Items[i].ToString();
                        else
                            temp += delimiter + box.Items[i].ToString();
                    }
                }
            }
            return temp;
        }

        public static string GetArrayListStr(ArrayList arr, string delimiter)
        {
            string temp = string.Empty;
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i == 0)
                        temp = arr[i].ToString();
                    else
                        temp += delimiter + arr[i].ToString();
                }
            }
            return temp;
        }

        public static string Encrypt(string sData, string Key)
        {
            string EncryptedString = null;
            if (sData == null || Key == null) return "$#@$";
            byte[] m_nBox;
            RC4Engine.GetKeyBytes(Key, out m_nBox);
            byte[] output;
            if (RC4Engine.GetEncryptBytes(sData, m_nBox, out output))
            {
                // Convert data to hex-data
                EncryptedString = "";
                for (int i = 0; i < output.Length; i++)
                    EncryptedString += output[i].ToString("X2");
                return EncryptedString;
            }
            else
                return "$#@$";
        }

        public static string Decrypt(string EncryptedString, string Key)
        {
            string sData = null;
            if (EncryptedString == null || Key == null) return "$#@$";
            else if (EncryptedString.Length % 2 != 0) return "$#@$";
            byte[] m_nBox;
            RC4Engine.GetKeyBytes(Key, out m_nBox);
            // Convert data from hex-data to string
            byte[] bData = new byte[EncryptedString.Length / 2];
            for (int i = 0; i < bData.Length; i++)
                bData[i] = Convert.ToByte(EncryptedString.Substring(i * 2, 2), 16);

            EncryptedString = Encoding.Unicode.GetString(bData);

            byte[] output;
            if (RC4Engine.GetEncryptBytes(EncryptedString, m_nBox, out output))
            {
                sData = Encoding.Unicode.GetString(output);
                return sData;
            }
            else
                return "$#@$";
        }

        public static string ShowOpen(string P_IniDir, string P_Title, string P_Filter, int P_FilterIndex, bool P_RestoreDirectory)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = P_Title;
                fdlg.InitialDirectory = @P_IniDir;
                fdlg.Filter = P_Filter;
                fdlg.FilterIndex = P_FilterIndex;
                fdlg.RestoreDirectory = P_RestoreDirectory;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    return fdlg.FileName;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void HideToolStripSeparator(ToolStripEx ts)
        {
            if (ts.Items.Count > 1)
            {
                string tooltype = string.Empty;
                for (int k = 0; k < ts.Items.Count; k++)
                {
                    ToolStripItem toolitem = (ToolStripItem)ts.Items[k];
                    if (toolitem.GetType().ToString() == "System.Windows.Forms.ToolStripSeparator")
                        toolitem.Visible = true;
                }
                for (int k = 0; k < ts.Items.Count; k++)
                {
                    ToolStripItem toolitem = (ToolStripItem)ts.Items[k];
                    if (toolitem.GetType().ToString() !="System.Windows.Forms.ToolStripSeparator" )
                    {
                        if(toolitem.Visible==true)
                            tooltype = toolitem.GetType().ToString();
                    }
                    else
                    {
                        if (tooltype == "System.Windows.Forms.ToolStripSeparator")
                        {
                            toolitem.Visible = false;
                        }
                        else if (toolitem.Visible == true)
                            tooltype = "System.Windows.Forms.ToolStripSeparator";
                    }
                }
            }
        }

        public static void FillCombox(ComboBox cbo, DataSet ds, string pValueMember, string pDisplayMember)
        {
            cbo.DataSource = ds.Tables[0];
            cbo.ValueMember = pValueMember;
            cbo.DisplayMember = pDisplayMember;
        }

        public static void FillCombox(ComboBox cbo, ArrayList arr)
        {
            cbo.Items.Clear();
            if (arr == null || arr.Count == 0) return;
            for (int i = 0; i < arr.Count; i++)
            {
                cbo.Items.Add(arr[i]);
            }
        }

        public static void FillCombox(ComboBox cbo, string[] str)
        {
            cbo.Items.Clear();
            if (str == null || str.Length == 0) return;
            for (int i = 0; i < str.Length; i++)
            {
                cbo.Items.Add(str[i]);
            }
        }

        public static void FillCombox<T>(ComboBox cbo, BindingCollection<T> list, string pValueMember, string pDisplayMember)
        {
            cbo.DataSource = list;
            cbo.ValueMember = pValueMember;
            cbo.DisplayMember = pDisplayMember;
        }

        public static void FillComboxAll(ComboBox cbo, DataSet ds, string pDisplayMember)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(pDisplayMember, typeof(string));

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drAll = dt.NewRow();
                drAll[0] = "ALL";
                dt.Rows.Add(drAll);
                for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables[0].Rows[idx][0].ToString();
                    dt.Rows.Add(dr);
                }
            }
            cbo.DataSource = dt.DefaultView;
            cbo.ValueMember = pDisplayMember;
            cbo.DisplayMember = pDisplayMember;
        }

        public static int GetComboBoxIndex(ComboBox cbx, string sText)
        {
            if (cbx.Items.Count == 0)
                return -1;
            for (int idx = 0; idx < cbx.Items.Count - 1; idx++)
            {
                if (cbx.Items[idx].ToString().Trim() == sText)
                    return idx;
            }
            return -1;
        }

        public static void SetComboBoxIndex(ComboBox cbx, string sText)
        {
            if (cbx.Items.Count == 0)
                return;
            for (int idx = 0; idx < cbx.Items.Count - 1; idx++)
            {
                if (cbx.Items[idx].ToString().Trim() == sText)
                {
                    cbx.SelectedIndex = idx;
                    return;
                }
            }
        }


        public static int GetComboBoxIndex(DataGridViewComboBoxColumn cbx, string sText)
        {
            if (cbx.Items.Count == 0)
                return -1;
            for (int idx = 0; idx < cbx.Items.Count - 1; idx++)
            {
                if (cbx.Items[idx].ToString().Trim() == sText)
                    return idx;
            }
            return -1;
        }

        public static void SetComboBoxIndex(DataGridViewComboBoxColumn cbx, string sText)
        {
            if (cbx.Items.Count == 0)
                return;
            for (int idx = 0; idx < cbx.Items.Count - 1; idx++)
            {
                if (cbx.Items[idx].ToString().Trim() == sText)
                {
                    // = idx;
                    return;
                }
            }
        }

        public static void FillListBox(ListBox lbx, DataSet ds, string pValueMember, string pDisplayMember)
        {
            lbx.DataSource = ds.Tables[0];
            lbx.ValueMember = pValueMember;
            lbx.DisplayMember = pDisplayMember;
        }

        public static void FillListBox(ListBox lbx, ArrayList arr, bool keepolditems)
        {
            if (!keepolditems)
                lbx.Items.Clear();
            if (arr == null) return;
            for (int i = 0; i < arr.Count; i++)
            {
                lbx.Items.Add(arr[i]);
            }
        }

        public static void FillListBox(ListBox lbx, string str, bool keepolditems)
        {
            if (!keepolditems)
                lbx.Items.Clear();
            if (string.IsNullOrEmpty(str)) return;
            string[] arr = str.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                lbx.Items.Add(arr[i]);
            }
        }

        public static void InitListView(ListView lvData, string P_HeaderList)
        {
            try
            {
                if (P_HeaderList.Length == 0)
                    return;
                lvData.Items.Clear();
                lvData.Columns.Clear();
                string[] strHeaderList = P_HeaderList.Split(Convert.ToChar(";"));

                //lvData.Columns.Add(strHeaderList[0], lvData.Width -100, HorizontalAlignment.Left);
                for (int i = 0; i < strHeaderList.GetLength(0); i++)
                {
                    string[] strItemList = strHeaderList[i].Split(Convert.ToChar(","));
                    int iWidth = Convert.ToInt32(strItemList[1]);
                    if (iWidth >= 0)
                        if (strItemList[2] == "0")
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), Convert.ToInt32(strItemList[1]), HorizontalAlignment.Left);
                        else if (strItemList[2] == "1")
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), Convert.ToInt32(strItemList[1]), HorizontalAlignment.Right);
                        else
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), Convert.ToInt32(strItemList[1]), HorizontalAlignment.Center);
                    else
                        if (strItemList[2] == "0")
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), lvData.Width, HorizontalAlignment.Left);
                        else if (strItemList[2] == "1")
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), lvData.Width, HorizontalAlignment.Right);
                        else
                            lvData.Columns.Add(clsTranslate.TranslateString(strItemList[0]), lvData.Width, HorizontalAlignment.Center);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void InsertListView(ListView lvData, string P_DataList)
        {
            try
            {
                int iRow = 0;
                if (P_DataList.Length == 0)
                    return;

                string[] strDataList = P_DataList.Split(Convert.ToChar(";"));

                for (iRow = 0; iRow < strDataList.GetLength(0); iRow++)
                {
                    string[] strItemList = strDataList[iRow].ToString().Split(Convert.ToChar(","));
                    for (int iCol = 0; iCol < strItemList.GetLength(0); iCol++)
                    {
                        ListViewItem newItem = new ListViewItem();
                        newItem.SubItems.Clear();
                        newItem.Text =strItemList[iCol];
                        lvData.Items.Add(newItem);
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Fill_lv(DataSet ds, ListView lv, string fieldlist)
        {
            //填充listview
            lv.Columns.Clear();
            lv.Items.Clear();
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            if (ds != null)
            {
                DataTable table = new DataTable();
                table = ds.Tables[0];

                string[] fields = fieldlist.Split(',');
                for (int i = 0; i < fields.Length; i++)
                {
                    ColumnHeader ch = new ColumnHeader();
                    ch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                    ch.Text = fields[i];
                    ch.Width = 100;
                    lv.Columns.Add(ch);
                }

                foreach (DataRow dr in table.Rows)
                {
                    string[] str = new string[fields.Length];
                    for (int i = 0; i < fields.Length; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (fields[i].ToLower() == table.Columns[j].ColumnName.ToString().ToLower())
                            {
                                str[i] = dr[j].ToString();
                                break;
                            }
                        }
                    }
                    ListViewItem lst1 = new ListViewItem(str, 0);
                    //lst1.ImageIndex = Convert.ToInt32(dr["Ste"]);
                    lv.Items.Add(lst1);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lv.Columns[lv.Columns.Count - 1].Width -= 50;
            }
        }

        public static void Fill_lv(DataSet ds, ListView lv)
        {
            //填充listview
            lv.Columns.Clear();
            lv.Items.Clear();
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            if (ds != null)
            {
                DataTable table = new DataTable();
                table = ds.Tables[0];

                foreach (DataColumn col in table.Columns)
                {
                    ColumnHeader ch = new ColumnHeader();
                    ch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                    ch.Text =clsTranslate.TranslateString(col.Caption);
                    ch.Width = 100;
                    lv.Columns.Add(ch);
                }

                foreach (DataRow dr in table.Rows)
                {
                    string[] str = new string[table.Columns.Count];
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        str[i] = dr[i].ToString();
                    }
                    ListViewItem lst1 = new ListViewItem(str, 0);
                    //lst1.ImageIndex = Convert.ToInt32(dr["Ste"]);
                    lv.Items.Add(lst1);
                }
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lv.Columns[lv.Columns.Count - 1].Width -= 50;
            }
        }

        public static void Init_Lv_Header(string HeadList, ListView lv)
        {
            //填充listview
            lv.Columns.Clear();
            lv.Items.Clear();
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            string[] header = HeadList.Split(',');
            for (int i = 0; i < header.Length; i++)
            {
                ColumnHeader ch = new ColumnHeader();
                ch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                ch.Text = clsTranslate.TranslateString(header[i]);
                ch.Width = 150;
                lv.Columns.Add(ch);
            }

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            lv.Columns[lv.Columns.Count - 1].Width -= 20;

        }

        public static SizeF GetSizeF(Control ctl, string text, string fontname, float fontsize)
        {
            Graphics g = Graphics.FromHwnd(ctl.Handle);
            System.Drawing.Font _Font = new System.Drawing.Font(fontname, fontsize);
            return g.MeasureString(text.Replace(" ", "_"), _Font);
        }

        public static int GetStrLength(string str)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = str.ToCharArray();

            int nLength = 0;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {                    
                    nLength += 2;
                }
                else
                {                    
                    nLength = nLength + 1;
                }
            }
            return nLength;
        }

        public static string SubStr(string stringToSub, int start, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();

            int startindex = 0;
            
            int i = 0;
            int nLength = 0;
            if (start > 0)
            {
                for (i = 0; i < stringChar.Length; i++)
                {
                    if (regex.IsMatch((stringChar[i]).ToString()))
                    {
                        nLength += 2;
                        if (nLength > start)
                        {
                            startindex = i;
                            break;
                        }                          
                    }
                    else
                    {
                        nLength += 1;
                        if (nLength >= start)
                        {
                            startindex = i;
                            break;
                        }
                    }
                }
            }

            nLength = 0;
            for (i = startindex; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {                        
                    nLength += 2;
                }
                else
                {
                    nLength += 1;
                }
                if(length>0)
                    if (nLength > length)
                        break;
                sb.Append(stringChar[i]);
            }
            
            return sb.ToString();
        }

        public static void AutoSetColWidth(int intMode, int fixrows, AxMSFlexGridLib.AxMSFlexGrid gdGrid)
        {
            if (gdGrid.Rows == 0)
                return;

            if (gdGrid.Cols == 0)
                return;

            try
            {
                if (intMode == MODE_HEAD)
                {
                    for (int iCol = 0; iCol < gdGrid.Cols; iCol++)
                    {
                        if (gdGrid.get_ColWidth(iCol) > 0)
                        {
                            string b = gdGrid.get_TextMatrix(0, iCol);
                            int a = b.Length;
                            gdGrid.set_ColWidth(iCol, Convert.ToInt32(2 + 10 * a) * 15);
                        }
                    }
                }
                else if (intMode == MODE_CONTENT)
                {
                    AutoSizeTable(gdGrid, fixrows);
                }
                else
                {
                    int ttlWidth = 0;
                    for (int iCol = 0; iCol < gdGrid.Cols; iCol++)
                    {
                        if (gdGrid.get_ColWidth(iCol) > 0)
                            ttlWidth += gdGrid.get_ColWidth(iCol);
                    }
                    for (int iCol = 0; iCol < gdGrid.Cols; iCol++)
                    {
                        if (gdGrid.get_ColWidth(iCol) > 0)
                        {
                            int w = (gdGrid.get_ColWidth(iCol) * gdGrid.Width / ttlWidth) * 15;
                            gdGrid.set_ColWidth(iCol, Convert.ToInt32(w));
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static double PixeltoMM(AxMSFlexGridLib.AxMSFlexGrid gdGrid, double p)
        {
            Graphics g = Graphics.FromHwnd(gdGrid.Handle);
            double temp = 25.4;
            double ret = p * (double)g.DpiX / temp;
            return ret;
        }

        public static double MMtoPixel(AxMSFlexGridLib.AxMSFlexGrid gdGrid, double m)
        {
            Graphics g = Graphics.FromHwnd(gdGrid.Handle);
            double temp = 25.4;
            double ret = m * temp / (double)g.DpiX;
            return ret;
        }

        private static void AutoSizeTable(AxMSFlexGridLib.AxMSFlexGrid gdGrid, int fixrows)
        {
            int i = 0;
            while (i < gdGrid.Cols)
            {
                AutoSizeCol(i, fixrows, gdGrid);
                i++;
            }
        }

        private static void AutoSizeCol(int col, int fixrows, AxMSFlexGridLib.AxMSFlexGrid gdGrid)
        {
            Single width = 0;
            Graphics g = Graphics.FromHwnd(gdGrid.Handle);
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);
            SizeF size;
            int i = fixrows;

            while (i < gdGrid.Rows)
            {
                if (!string.IsNullOrEmpty(gdGrid.get_TextMatrix(i, col)))
                {
                    size = g.MeasureString(gdGrid.get_TextMatrix(i, col), gdGrid.Font, 500, sf);
                    if (size.Width > width)
                        width = size.Width;
                }
                i++;
            }

            g.Dispose();
            //if (width > 0)
            //{
                if (gdGrid.get_ColWidth(col) > 0)
                {
                    gdGrid.set_ColWidth(col, Convert.ToInt32((width + 15) * 15));
                }
            //}
            //else
            //{
            //    gdGrid.set_ColWidth(col, 0);
            //}
        }

        private static void AutoSizeCol(int col, DataGridView gdGrid, bool hidenullcol)
        {
            Single width = 0;
            SizeF size;
            int i = 0;

            if (gdGrid.Columns[col].Visible == false) return;
            if (gdGrid.Columns[col].HeaderText.IndexOf(clsTranslate.TranslateString("date")) > 0)
            {
                size = GetSizeF((Control)gdGrid, "2012/10/10", gdGrid.DefaultCellStyle.Font.Name, gdGrid.DefaultCellStyle.Font.Size);
                gdGrid.Columns[col].Width = Convert.ToInt32(size.Width + 12);
                return;
            }
            while (i < gdGrid.RowCount)
            {
                if (!gdGrid.Rows[i].IsNewRow)
                {
                    if (gdGrid.Rows[i].Cells[col].Value != null && !string.IsNullOrEmpty(gdGrid.Rows[i].Cells[col].Value.ToString()))
                    {
                        size = GetSizeF((Control)gdGrid, gdGrid.Rows[i].Cells[col].Value.ToString(), gdGrid.DefaultCellStyle.Font.Name, gdGrid.DefaultCellStyle.Font.Size);
                        if (size.Width > width)
                            width = size.Width;
                    }
                }
                i++;
            }

            if (width > 0)
            {
                if (gdGrid.Columns[col].Visible == true)
                    if(width<=28 && gdGrid.Columns[col].ReadOnly==false)
                        gdGrid.Columns[col].Width = 40;
                    else
                        gdGrid.Columns[col].Width = Convert.ToInt32(width + 12);
            }
            else if (hidenullcol)
            {
                gdGrid.Columns[col].Visible = false;
            }
            else
            {
                gdGrid.Columns[col].Visible = true;
                gdGrid.Columns[col].Width = 2;
            }
        }

        private static void AutoSizeTable(DataGridView gdGrid, bool HideNullCol)
        {
            int i = 0;
            while (i < gdGrid.ColumnCount)
            {
                AutoSizeCol(i, gdGrid, HideNullCol);
                i++;
            }
        }

        public static void AutoSetColWidth(int intMode, DataGridView gdGrid)
        {
            AutoSetColWidth(intMode, gdGrid, true);
        }

        public static void AutoSetColWidth(int intMode, DataGridView gdGrid, bool HideNullCol)
        {
            if (gdGrid.RowCount == 0)
                return;

            if (gdGrid.ColumnCount == 0)
                return;

            try
            {
                int width = 0;
                SizeF size;
                if (intMode == MODE_HEAD)
                {
                    for (int i = 0; i < gdGrid.ColumnCount; i++)
                    {
                        if (gdGrid.Columns[i].Visible == true)
                        {
                            size = GetSizeF((Control)gdGrid, gdGrid.Columns[i].HeaderText, gdGrid.DefaultCellStyle.Font.Name, gdGrid.DefaultCellStyle.Font.Size);
                            width = Convert.ToInt32(size.Width) + 12;
                            gdGrid.Columns[i].Width = width;
                        }
                    }
                }
                else if (intMode == MODE_CONTENT)
                {
                    AutoSizeTable(gdGrid, HideNullCol);
                }
                else
                {
                    AutoSizeTable(gdGrid, HideNullCol);
                    for (int i = 0; i < gdGrid.ColumnCount; i++)
                    {
                        if (gdGrid.Columns[i].Visible == true)
                        {
                            size = GetSizeF((Control)gdGrid, gdGrid.Columns[i].HeaderText, gdGrid.DefaultCellStyle.Font.Name, gdGrid.DefaultCellStyle.Font.Size);
                            width = Convert.ToInt32(size.Width) + 12;
                            if (gdGrid.Columns[i].Width < width)
                                gdGrid.Columns[i].Width = width;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        public static DataGridViewComboBoxColumn CreateComboBoxColumn(int pWidth, int iDropDownWidth, int iMaxDropDownItems)
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            {
                column.DropDownWidth = pWidth;
                column.Width = iDropDownWidth;
                column.MaxDropDownItems = iMaxDropDownItems;
                column.FlatStyle = FlatStyle.System;
                column.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            }
            return column;
        }

        public static DataGridViewTextBoxColumn CreateTextBoxColumn(int pWidth)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            {
                column.Width = pWidth;
            }
            return column;
        }

        public static int GetColFromPoint(DataGridView grid, int x, int y)
        {
            for (int idx = 0; idx < grid.ColumnCount; idx++)
            {
                Rectangle rec = grid.GetColumnDisplayRectangle(idx, false);

                if (grid.RectangleToScreen(rec).Contains(x, y))
                    return idx;
            }
            return -1;
        }

        public static int GetRowFromPoint(DataGridView grid, int x, int y)
        {
            for (int idx = 0; idx < grid.RowCount; idx++)
            {
                Rectangle rec = grid.GetRowDisplayRectangle(idx, false);

                if (grid.RectangleToScreen(rec).Contains(x, y))
                    return idx;
            }
            return -1;
        }

        public static void AutoCompleteCombox(ComboBox cb, System.Windows.Forms.KeyPressEventArgs e, bool blnLimitToList)
        {
            string strFindStr = "";

            if (e.KeyChar == (char)8)
            {
                if (cb.SelectionStart <= 1)
                {
                    cb.Text = "";
                    return;
                }

                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text.Substring(0, cb.Text.Length - 1);
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1);
            }
            else
            {
                if (cb.SelectionLength == 0)
                    strFindStr = cb.Text + e.KeyChar;
                else
                    strFindStr = cb.Text.Substring(0, cb.SelectionStart) + e.KeyChar;
            }

            int intIdx = -1;

            // Search the string in the ComboBox list.

            intIdx = cb.FindString(strFindStr);

            if (intIdx != -1)
            {
                cb.SelectedText = "";
                cb.SelectedIndex = intIdx;
                cb.SelectionStart = strFindStr.Length;
                cb.SelectionLength = cb.Text.Length;
                e.Handled = true;
            }
            else
            {
                e.Handled = blnLimitToList;
            }
        }

        public static DataSet ConvertToDataSet<T>(BindingCollection<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }

            ds.Tables.Add(dt);

            return ds;
        }

        /// <summary>
        /// 取得汉字拼音的首字母
        /// </summary>
        /// <param name="strText">汉字串</param>
        /// <returns>汉字串的首字母串</returns>
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            StringBuilder myStr = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                myStr.Append(GetSpell(strText.Substring(i, 1)));
            }
            return myStr.ToString();
        }

        /// <summary>
        /// 取得一个汉字的拼音首字母
        /// </summary>
        /// <param name="cnChar">一个汉字</param>
        /// <returns>首字母</returns>
        private static string GetSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }

        //判断字符串中是否有中文，true有，false没有。
        public static bool IsChina(string CString)
        {
            bool BoolValue = false;
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) > Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = true;
                }

            }
            return BoolValue;
        }

        //返回字符串中的中文部分
        public static string GetChinaStr(string CString)
        {
            string rt = "";
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) > Convert.ToInt32(Convert.ToChar(128)))
                {
                    rt += CString.Substring(i, 1).ToString();
                }


            }
            return rt;
        }
    }
}
