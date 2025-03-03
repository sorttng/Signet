using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace FAS.Common
{
    public static class JasonSerialization
    {
        /// <summary>
        /// object转json字符串
        /// </summary>
        public static string ObjectToJson(this object obj)
        {
            if (null == obj)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// json字符串转object
        /// </summary>
        public static T JsonToObject<T>(this string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 格式化json字符串
        /// </summary>
        public static string ConvertJsonString(this string str)
        {
            // 格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (null != obj)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}
