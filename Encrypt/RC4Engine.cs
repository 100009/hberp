using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Encrypt
{
    public class RC4Engine
    {
        private static long m_nBoxLen = 255;
        /// <summary>
        /// Avoid this class to be inited
        /// </summary>
        protected RC4Engine()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void GetKeyBytes(string Key, out byte[] m_nBox)
        {
            //
            // Used to populate m_nBox
            //
            long index2 = 0;

            m_nBox = new byte[m_nBoxLen];
            //
            // Create two different encoding 
            //

            Encoding ascii = Encoding.ASCII;

            Encoding unicode = Encoding.Unicode;
            //
            // Perform the conversion of the encryption key from unicode to ansi
            //
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(Key));
            //
            // Convert the new byte[] into a char[] and then to string
            //
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];

            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            //this.m_sEncryptionKeyAscii = new string(asciiChars);
            //
            // Populate m_nBox
            //
            long KeyLen = Key.Length;
            //
            // First Loop
            //
            for (long count = 0; count < m_nBoxLen; count++)
            {
                m_nBox[count] = (byte)count;
            }
            //
            // Second Loop
            //
            for (long count = 0; count < m_nBoxLen; count++)
            {
                index2 = (index2 + m_nBox[count] + asciiChars[count % KeyLen]) % m_nBoxLen;
                byte temp = m_nBox[count];
                m_nBox[count] = m_nBox[index2];
                m_nBox[index2] = temp;
            }
        }

        /// <summary>
        /// Get encrypted bytes
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="m_nBox"></param>
        /// <param name="EncryptedBytes"></param>
        /// <returns></returns>
        public static bool GetEncryptBytes(string sData, byte[] m_nBox, out byte[] EncryptedBytes)
        {
            EncryptedBytes = null;
            bool toRet = true;
            try
            {
                //
                // indexes used below
                //
                long i = 0;
                long j = 0;
                //
                // Put input string in temporary byte array
                //
                Encoding enc_default = Encoding.Unicode;
                byte[] input = enc_default.GetBytes(sData);
                // 
                // Output byte array
                //
                EncryptedBytes = new byte[input.Length];
                //
                // Local copy of m_nBoxLen
                //
                byte[] n_LocBox = new byte[m_nBoxLen];
                m_nBox.CopyTo(n_LocBox, 0);
                //
                //  Len of Chipher
                //
                long ChipherLen = input.Length + 1;
                //
                // Run Alghoritm
                //
                for (long offset = 0; offset < input.Length; offset++)
                {
                    i = (i + 1) % m_nBoxLen;
                    j = (j + n_LocBox[i]) % m_nBoxLen;
                    byte temp = n_LocBox[i];
                    n_LocBox[i] = n_LocBox[j];
                    n_LocBox[j] = temp;
                    byte a = input[offset];
                    byte b = n_LocBox[(n_LocBox[i] + n_LocBox[j]) % m_nBoxLen];
                    EncryptedBytes[offset] = (byte)((int)a ^ (int)b);
                }
            }
            catch
            {
                EncryptedBytes = null;
                //
                // error occured - set retcode to false.
                // 
                toRet = false;
            }
            return toRet;
        }

        /// <summary>
        /// Encrypt data through specific key value
        /// </summary>
        /// <param name="sData"></param>
        /// <param name="Key"></param>
        /// <param name="EncryptedString"></param>
        /// <returns></returns>
        public static bool Encrypt(string sData, string Key, out string EncryptedString)
        {
            EncryptedString = null;
            if (sData == null || Key == null) return false;
            byte[] m_nBox;
            GetKeyBytes(Key, out m_nBox);
            byte[] output;
            if (GetEncryptBytes(sData, m_nBox, out output))
            {
                // Convert data to hex-data
                EncryptedString = "";
                for (int i = 0; i < output.Length; i++)
                    EncryptedString += output[i].ToString("X2");
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Decrypt data using specific key
        /// </summary>
        /// <param name="EncryptedString"></param>
        /// <param name="Key"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        public bool Decrypt(string EncryptedString, string Key, out string sData)
        {
            sData = null;
            if (EncryptedString == null || Key == null) return false;
            else if (EncryptedString.Length % 2 != 0) return false;
            byte[] m_nBox;
            GetKeyBytes(Key, out m_nBox);
            // Convert data from hex-data to string
            byte[] bData = new byte[EncryptedString.Length / 2];
            for (int i = 0; i < bData.Length; i++)
                bData[i] = Convert.ToByte(EncryptedString.Substring(i * 2, 2), 16);

            EncryptedString = Encoding.Unicode.GetString(bData);

            byte[] output;
            if (GetEncryptBytes(EncryptedString, m_nBox, out output))
            {
                sData = Encoding.Unicode.GetString(output);
                return true;
            }
            else
                return false;
        }
    }
}
