using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace FAS.Common
{
    public class ConfigFileHelper
    {
        /// <summary>
        /// 读取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ConfigRead(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static void ConfigSet(string key,string val)
        {
            Configuration _configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            _configuration.AppSettings.Settings[key].Value = val;
            _configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");            
        }
    }
}
