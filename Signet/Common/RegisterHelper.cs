using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Signet.Common
{
    public class LicenseClass
    {
        public string DevCode { get; set; }
        public string EndTime { get; set; }
    }

    public class RegisterHelper
    {
        public static string CryptographFile = "Cryptograph.Key";
        public static string LicenseFile = "license.Key";

        public static void WriteCryptographFile(string info)
        {
            WriteFile(info, CryptographFile);
        }

        public static void WriteLicenseFile(string info)
        {
            WriteFile(info, LicenseFile);
        }

        public static string ReadCryptographFile()
        {
            return ReadFile(CryptographFile);
        }

        public static string ReadLicenseFile()
        {
            return ReadFile(LicenseFile);
        }

        public static bool ExistCryptographFile()
        {
            return File.Exists(CryptographFile);
        }

        public static bool ExistLicenseFile()
        {
            return File.Exists(LicenseFile);
        }

        private static void WriteFile(string info, string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    writer.Write(info);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ReadFile(string fileName)
        {
            string info = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    info = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return info;
        }

        /// <summary>
        /// 生产设备代码文件
        /// </summary>
        /// <returns></returns>
        public static void GenerateCryptographFile()
        {
            //MD5 Cryptograph of Computer information
            string MD5_CryptoInfo = MD5Helper.GetMD5(ComputerInfoHelper.ComputerInfo());
            //judge the exist of Cryptograph file
            if (!ExistCryptographFile() || MD5_CryptoInfo != ReadCryptographFile())
            {
                WriteCryptographFile(MD5_CryptoInfo);
            }
        }

        /// <summary>
        /// 检查证书
        /// </summary>
        /// <param name="License"></param>
        /// <param name="Mes"></param>
        /// <returns></returns>
        public static bool CheckLicense(string FullLicense, out string Mes)
        {
            //string[] res = FullLicense.Split('-');

            if (FullLicense == string.Empty)
            {
                Mes = "请输入激活码！";
                return false;
            }


            //if (res.Count() != 2)
            //{
            //    Mes= "请输入正确的激活码！";
            //    return false;
            //}
            //解析输入的证书
            string License_Des = string.Empty;
            try
            {
                License_Des = RsaHelper.DecryptByPrivateKey
                    (FullLicense, GlobalInfo.License_PrivateKey);
            //License_Des = DesHelper.DecryptString(License_Des, "ZJCYHJKJ");
        }
            catch
            {
                Mes = "请输入正确的激活码！";
                return false;
            }

    LicenseClass mlicense = JasonSerialization.JsonToObject<LicenseClass>(License_Des);

            DateTime LicenseTime = new DateTime();
            //时间格式错误
            if (!DateTime.TryParse(mlicense.EndTime, out LicenseTime))
            {
                Mes = "请输入正确的激活码！";
                return false;
            }

            //解析设备码
            string MD5_CryptoInfo = MD5Helper.GetMD5(ComputerInfoHelper.ComputerInfo());

            if (mlicense.DevCode == MD5_CryptoInfo)
            {
                GlobalInfo.registerTime = LicenseTime;

                if (DateTime.Now > LicenseTime.AddDays(1))
                {
                    Mes = "激活码已过期！";
                    return false;
                }
                else
                {
                    //写入或更新证书
                    if (!RegisterHelper.ExistLicenseFile() ||
                    FullLicense != RegisterHelper.ReadLicenseFile())
                    {
                        RegisterHelper.WriteLicenseFile(FullLicense);
                    }
                    Mes = "激活成功！";
                    return true;
                }
            }
            else {
                Mes = "请输入正确的激活码！";
                return false;
            }
        }
    }
}
