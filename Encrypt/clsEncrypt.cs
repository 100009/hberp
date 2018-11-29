using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Encrypt
{    
    public class clsEncrypt
    {
        private static string sKey = "12345678";

        public static string Encrypt(string pToEncrypt)
        {
            if (string.IsNullOrEmpty(pToEncrypt.Trim())) return string.Empty;
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pToEncrypt);
                provider.Key = Encoding.ASCII.GetBytes(sKey);
                provider.IV = Encoding.ASCII.GetBytes(sKey);
                //provider.Mode = CipherMode.CBC;
                //provider.Padding = PaddingMode.PKCS7;
                MemoryStream stream = new MemoryStream();
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }
                string str = Convert.ToBase64String(stream.ToArray());
                stream.Close();
                return str;
            }
        }

        public static string Encrypt(string pToEncrypt,string sKey)
        {
            if (string.IsNullOrEmpty(pToEncrypt.Trim())) return string.Empty;
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pToEncrypt);
                provider.Key = Encoding.ASCII.GetBytes(sKey);
                provider.IV = Encoding.ASCII.GetBytes(sKey);
                //provider.Mode = CipherMode.CBC;
                //provider.Padding = PaddingMode.PKCS7;
                MemoryStream stream = new MemoryStream();
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }
                string str = Convert.ToBase64String(stream.ToArray());
                stream.Close();
                return str;
            }
        }

        public static string Decrypt(string pToDecrypt)
        {
            if (string.IsNullOrEmpty(pToDecrypt.Trim())) return string.Empty;
            byte[] buffer = Convert.FromBase64String(pToDecrypt);
            //byte[] buffer = Encoding.UTF8.GetBytes(pToDecrypt);
            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(sKey);
                provider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream stream = new MemoryStream();
                using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }
                string str = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return str;
            }
        }


    }
}
