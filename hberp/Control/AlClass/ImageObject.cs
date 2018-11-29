//作者：阿龙(Along)
//QQ号：646494711
//QQ群：57218890
//网站：http://www.8timer.com
//博客：http://www.cnblogs.com/Along729/
//声明：未经作者许可，任何人不得发布出售该源码，请尊重别人的劳动成果，谢谢大家支持
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AlSkin.AlClass
{
    public static class ImageObject
    {
        /// <summary>
        /// 得到要绘置的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        public static Bitmap GetResBitmap(string str)
        {
            Stream sm;
            sm = FindStream(str);
            if (sm == null) return null;
            return new Bitmap(sm);
        }

        /// <summary>
        /// 得到图程序集中的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        private static Stream FindStream(string str)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resNames = assembly.GetManifestResourceNames();
            foreach (string s in resNames)
            {
                if (s == str)
                {
                    return assembly.GetManifestResourceStream(s);
                }
            }
            return null;
        }
    }
}
