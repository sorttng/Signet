using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Signet.Common
{
    public enum CryptoKey
    {
        KeyA, KeyB
    }

    public class EncDecKeyHelper
    {
        private string KeyA = "ZJCYHJGS";
        private string KeyB = "ABCDEFGH";
        private string MD5strBegin = "ALSIDJF";
        private string MD5strEnd = "ASDFG";
        private string ExecuteKey = string.Empty;

        public EncDecKeyHelper()
        {
            InitKey();//KeyA
        }

        public EncDecKeyHelper(CryptoKey Key)
        {
            InitKey(Key);//Custom
        }

        private void InitKey(CryptoKey Key = CryptoKey.KeyA)
        {
            switch (Key)
            {
                case CryptoKey.KeyA:
                    ExecuteKey = KeyA;
                    break;
                case CryptoKey.KeyB:
                    ExecuteKey = KeyB;
                    break;
            }
        }

        public string EncryptOfKey(string str)
        {
            return Encrypt(str, ExecuteKey);
        }

        public string DecryptOfKey(string str)
        {
            return Decrypt(str, ExecuteKey);
        }

        public string EncryptOfMD5(string str)
        {
            str = string.Concat(MD5strBegin, str, MD5strEnd);//trim
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromdata = Encoding.Unicode.GetBytes(str);
            byte[] todata = md5.ComputeHash(fromdata);
            string MD5str = string.Empty;
            foreach (var data in todata)
            {
                MD5str += data.ToString("x2");
            }
            return MD5str;
        }

        private string Encrypt(string str, string sKey)
        {
            DESCryptoServiceProvider DESCsp = new DESCryptoServiceProvider();
            byte[] InputBytes = Encoding.Default.GetBytes(str);
            DESCsp.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DESCsp.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, DESCsp.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(InputBytes, 0, InputBytes.Length);
            cs.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                builder.AppendFormat("{0:X2}", b);
            }
            builder.ToString();
            return builder.ToString();
        }

        private string Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider DESCsp = new DESCryptoServiceProvider();
            byte[] InputBytes = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                InputBytes[x] = (byte)i;
            }
            DESCsp.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DESCsp.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, DESCsp.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(InputBytes, 0, InputBytes.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }


    }
}
