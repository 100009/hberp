using System;
using System.Collections;
using System.Windows.Forms;

namespace LXMS
{
    /// <summary>   
    /// ¶ÔListViewµã»÷ÁÐ±êÌâ×Ô¶¯ÅÅÐò¹¦ÄÜ   
    /// </summary>   
    public class ListViewHelper
    {
        /// <summary>   
        /// ¹¹Ôìº¯Êý   
        /// </summary>   
        public ListViewHelper()
        {
            //   
            // TODO: ÔÚ´Ë´¦Ìí¼Ó¹¹Ôìº¯ÊýÂß¼­   
            //   
        }

        public static void ListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView lv = sender as System.Windows.Forms.ListView;
            // ¼ì²éµã»÷µÄÁÐÊÇ²»ÊÇÏÖÔÚµÄÅÅÐòÁÐ.   
            if (e.Column == (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn)
            {
                // ÖØÐÂÉèÖÃ´ËÁÐµÄÅÅÐò·½·¨.   
                if ((lv.ListViewItemSorter as ListViewColumnSorter).Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // ÉèÖÃÅÅÐòÁÐ£¬Ä¬ÈÏÎªÕýÏòÅÅÐò   
                (lv.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
                (lv.ListViewItemSorter as ListViewColumnSorter).Order = System.Windows.Forms.SortOrder.Ascending;
            }
            // ÓÃÐÂµÄÅÅÐò·½·¨¶ÔListViewÅÅÐò   
            ((System.Windows.Forms.ListView)sender).Sort();
        }
    }

    /// <summary>   
    /// ¼Ì³Ð×ÔIComparer   
    /// </summary>   
    public class ListViewColumnSorter : System.Collections.IComparer
    {
        /// <summary>   
        /// Ö¸¶¨°´ÕÕÄÄ¸öÁÐÅÅÐò   
        /// </summary>   
        private int ColumnToSort;
        /// <summary>   
        /// Ö¸¶¨ÅÅÐòµÄ·½Ê½   
        /// </summary>   
        private System.Windows.Forms.SortOrder OrderOfSort;
        /// <summary>   
        /// ÉùÃ÷CaseInsensitiveComparerÀà¶ÔÏó   
        /// </summary>   
        private System.Collections.CaseInsensitiveComparer ObjectCompare;

        /// <summary>   
        /// ¹¹Ôìº¯Êý   
        /// </summary>   
        public ListViewColumnSorter()
        {
            // Ä¬ÈÏ°´µÚÒ»ÁÐÅÅÐò   
            ColumnToSort = 0;

            // ÅÅÐò·½Ê½Îª²»ÅÅÐò   
            OrderOfSort = System.Windows.Forms.SortOrder.None;

            // ³õÊ¼»¯CaseInsensitiveComparerÀà¶ÔÏó   
            ObjectCompare = new System.Collections.CaseInsensitiveComparer();
        }

        /// <summary>   
        /// ÖØÐ´IComparer½Ó¿Ú.   
        /// </summary>   
        /// <param name="x">Òª±È½ÏµÄµÚÒ»¸ö¶ÔÏó</param>   
        /// <param name="y">Òª±È½ÏµÄµÚ¶þ¸ö¶ÔÏó</param>   
        /// <returns>±È½ÏµÄ½á¹û.Èç¹ûÏàµÈ·µ»Ø0£¬Èç¹ûx´óÓÚy·µ»Ø1£¬Èç¹ûxÐ¡ÓÚy·µ»Ø-1</returns>   
        public int Compare(object x, object y)
        {
            int compareResult;
            System.Windows.Forms.ListViewItem listviewX, listviewY;

            // ½«±È½Ï¶ÔÏó×ª»»ÎªListViewItem¶ÔÏó   
            listviewX = (System.Windows.Forms.ListViewItem)x;
            listviewY = (System.Windows.Forms.ListViewItem)y;

            string xText = listviewX.SubItems[ColumnToSort].Text;
            string yText = listviewY.SubItems[ColumnToSort].Text;

            int xInt, yInt;
            double xDouble, yDouble;
            DateTime xDate, yDate;
            // ±È½Ï,Èç¹ûÖµÎªIPµØÖ·£¬Ôò¸ù¾ÝIPµØÖ·µÄ¹æÔòÅÅÐò¡£   
            if (IsIP(xText) && IsIP(yText))
            {
                compareResult = CompareIp(xText, yText);
            }
            else if (int.TryParse(xText, out xInt) && int.TryParse(yText, out yInt)) //ÊÇ·ñÈ«ÎªÊý×Ö   
            {
                //±È½ÏÊý×Ö   
                compareResult = CompareInt(xInt, yInt);
            }
            else if (double.TryParse(xText, out xDouble) && double.TryParse(yText, out yDouble)) //ÕûÊÇ·ñÈ«ÊÇ¸¡µãÐÍ   
            {

                //±È½Ï¸¡µãÐÍÊý×Ö   
                compareResult = CompareDouble(xDouble, yDouble);
            }
            else if (int.TryParse(xText, out xInt) && double.TryParse(yText, out yDouble)) //ÕûÐÍºÍ¸¡µãÐÍ   
            {

                //±È½Ï¸¡µãÐÍÊý×Ö   
                compareResult = CompareIntAndDouble(xInt, yDouble);
            }
            else if (DateTime.TryParse(xText, out xDate) && DateTime.TryParse(yText, out yDate)) //ÕûÐÍºÍ¸¡µãÐÍ   
            {

                //±È½ÏÈÕÆÚ   
                compareResult = CompareDate(xDate, yDate);
            }
            else
            {
                //±È½Ï¶ÔÏó   
                compareResult = ObjectCompare.Compare(xText, yText);
            }
            // ¸ù¾ÝÉÏÃæµÄ±È½Ï½á¹û·µ»ØÕýÈ·µÄ±È½Ï½á¹û   
            if (OrderOfSort == System.Windows.Forms.SortOrder.Ascending)
            {
                // ÒòÎªÊÇÕýÐòÅÅÐò£¬ËùÒÔÖ±½Ó·µ»Ø½á¹û   
                return compareResult;
            }
            else if (OrderOfSort == System.Windows.Forms.SortOrder.Descending)
            {
                // Èç¹ûÊÇ·´ÐòÅÅÐò£¬ËùÒÔÒªÈ¡¸ºÖµÔÙ·µ»Ø   
                return (-compareResult);
            }
            else
            {
                // Èç¹ûÏàµÈ·µ»Ø0   
                return 0;
            }
        }

        /// <summary>   
        /// ÅÐ¶ÏÊÇ·ñÎªÕýÈ·µÄIPµØÖ·£¬IP·¶Î§£¨0.0.0.0¡«255.255.255£©   
        /// </summary>   
        /// <param name="ip">ÐèÑéÖ¤µÄIPµØÖ·</param>   
        /// <returns></returns>   
        public bool IsIP(String ip)
        {
            return System.Text.RegularExpressions.Regex.Match(ip, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$").Success;
        }

        /// <summary>   
        /// ±È½ÏÁ½¸öÊý×ÖµÄ´óÐ¡   
        /// </summary>   
        /// <param name="ipx">Òª±È½ÏµÄµÚÒ»¸ö¶ÔÏó</param>   
        /// <param name="ipy">Òª±È½ÏµÄµÚ¶þ¸ö¶ÔÏó</param>   
        /// <returns>±È½ÏµÄ½á¹û.Èç¹ûÏàµÈ·µ»Ø0£¬Èç¹ûx´óÓÚy·µ»Ø1£¬Èç¹ûxÐ¡ÓÚy·µ»Ø-1</returns>   
        private int CompareInt(int x, int y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private int CompareDouble(double x, double y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        private int CompareIntAndDouble(int x, double y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        private int CompareDate(DateTime x, DateTime y)
        {
            if (x > y)
            {
                return 1;
            }
            else if (x < y)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>   
        /// ±È½ÏÁ½¸öIPµØÖ·µÄ´óÐ¡   
        /// </summary>   
        /// <param name="ipx">Òª±È½ÏµÄµÚÒ»¸ö¶ÔÏó</param>   
        /// <param name="ipy">Òª±È½ÏµÄµÚ¶þ¸ö¶ÔÏó</param>   
        /// <returns>±È½ÏµÄ½á¹û.Èç¹ûÏàµÈ·µ»Ø0£¬Èç¹ûx´óÓÚy·µ»Ø1£¬Èç¹ûxÐ¡ÓÚy·µ»Ø-1</returns>   
        private int CompareIp(string ipx, string ipy)
        {
            string[] ipxs = ipx.Split('.');
            string[] ipys = ipy.Split('.');

            for (int i = 0; i < 4; i++)
            {
                if (Convert.ToInt32(ipxs[i]) > Convert.ToInt32(ipys[i]))
                {
                    return 1;
                }
                else if (Convert.ToInt32(ipxs[i]) < Convert.ToInt32(ipys[i]))
                {
                    return -1;
                }
                else
                {
                    continue;
                }
            }
            return 0;
        }

        /// <summary>   
        /// »ñÈ¡»òÉèÖÃ°´ÕÕÄÄÒ»ÁÐÅÅÐò.   
        /// </summary>   
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>   
        /// »ñÈ¡»òÉèÖÃÅÅÐò·½Ê½.   
        /// </summary>   
        public System.Windows.Forms.SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}