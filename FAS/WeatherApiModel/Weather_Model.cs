using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAS.WeatherApiModel
{
    public class Now
    {
        /// <summary>
        /// 
        /// </summary>
        public string obsTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string temp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string feelsLike { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 晴
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wind360 { get; set; }
        /// <summary>
        /// 南风
        /// </summary>
        public string windDir { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windScale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windSpeed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string humidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string precip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pressure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vis { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cloud { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string dew { get; set; }
    }

    public class Refer
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> sources { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> license { get; set; }
    }

    public class LocationItem
    {
        /// <summary>
        /// 嘉兴
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lon { get; set; }
        /// <summary>
        /// 嘉兴
        /// </summary>
        public string adm2 { get; set; }
        /// <summary>
        /// 浙江省
        /// </summary>
        public string adm1 { get; set; }
        /// <summary>
        /// 中国
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string utcOffset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isDst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fxLink { get; set; }
    }

    public class Location
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LocationItem> location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Refer refer { get; set; }
    }

    public class Weather
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fxLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Now now { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Refer refer { get; set; }

        public static explicit operator Weather(Task<Weather> v)
        {
            throw new NotImplementedException();
        }
    }
}
