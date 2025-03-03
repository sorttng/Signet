using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FAS.Model;
using FAS.WeatherApiModel;
namespace FAS.Common
{
    public class WeatherHelper
    {
        public static readonly string ApiKey = "e4b0d99605cc4125a3aa883e67b331a1";
        /// <summary>
        /// 和风天气的api函数
        /// </summary>
        /// <param name="url">api接口</param>
        /// <param name="dic">参数</param>
        /// <returns>返回数据</returns>
        public static async Task<string> GetWeatherApi(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
                    i++;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            //添加参数

            HttpWebResponse resp = (HttpWebResponse)(await req.GetResponseAsync());

            Stream stream = new System.IO.Compression.GZipStream(resp.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress);
            //Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }

    }
}
