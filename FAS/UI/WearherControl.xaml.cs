using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using FAS.Common;
using FAS.Model;
using FAS.WeatherApiModel;
using MahApps.Metro.IconPacks;
using MahApps.Metro;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace FAS.UI
{
    /// <summary>
    /// WearherControl.xaml 的交互逻辑
    /// </summary>
    public partial class WearherControl : UserControl
    {
        public WearherControl()
        {
            InitializeComponent();
            this.Time.Text =
            string.Format("{0} {1}",
            DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"),
            GetDayOfWeek_CN(DateTime.Now.DayOfWeek));

            var weather = GetWeatherAsync(GlobalInfo.CurLocation);

            #region 天气
            var weather_timer = new DispatcherTimer();
            weather_timer.Interval = TimeSpan.FromSeconds(600);//10分钟;
            weather_timer.Tick += new EventHandler(async (object s, EventArgs a) =>
            {
                weather = GetWeatherAsync(GlobalInfo.CurLocation);
            });
            weather_timer.Start();
            #endregion

            #region 注册消息
            Messenger.Default.Register<SqlSugarModel.Location_Table>(this, "Location", (message) => ReceivedLocation(message));
            #endregion
        }


        #region 消息接收事件
        public async void ReceivedLocation(SqlSugarModel.Location_Table message)
        {
            await GetWeatherAsync(message);
        }
        #endregion

        /// <summary>
        /// 获取中文星期
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns>中文星期</returns>
        private string GetDayOfWeek_CN(DayOfWeek dayOfWeek)
        {            
            string chineseDay = "";

            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    chineseDay = "星期日";
                    break;
                case DayOfWeek.Monday:
                    chineseDay = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    chineseDay = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    chineseDay = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    chineseDay = "星期四";
                    break;
                case DayOfWeek.Friday:
                    chineseDay = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    chineseDay = "星期六";
                    break;
            }
            return chineseDay;
        }

        public string GetIcon(string weatherIcon)
        {
            string iconPath = string.Format("pack://application:,,,/FAS;component/Images/{0}.svg", weatherIcon);

            return iconPath;
            //PackIconUnicons packIconUnicons;
            //if (weathertext == "阴")
            //{
            //    packIconUnicons = new PackIconUnicons() { Kind = PackIconUniconsKind.CloudSun, Width = 50, Height = 50 };
            //}
            //else if (weathertext == "晴")
            //{
            //    packIconUnicons = new PackIconUnicons() { Kind = PackIconUniconsKind.Brightness, Width = 50, Height = 50 };
            //}
            //else if (weathertext == "雨")
            //{
            //    packIconUnicons = new PackIconUnicons() { Kind = PackIconUniconsKind.CloudRain, Width = 50, Height = 50 };
            //}
            //else if (weathertext == "雪")
            //{
            //    packIconUnicons = new PackIconUnicons() { Kind = PackIconUniconsKind.SnowFlake, Width = 50, Height = 50 };
            //}
            //else
            //{
            //    packIconUnicons = new PackIconUnicons() { Kind = PackIconUniconsKind.Brightness, Width = 50, Height = 50 };
            //}
            //return packIconUnicons;
        }

        public string Week(DateTime d)
        {
            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = weekdays[Convert.ToInt32(d.DayOfWeek)];
            return week;
        }

        //,out Weather weather
        private async Task<Weather> GetWeatherAsync(SqlSugarModel.Location_Table location)
        {            
            string code = "200";

            #region 获取城市locationid
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("location", locationName);
            //dic.Add("key", WeatherHelper.ApiKey);

            //string location_resp = await WeatherHelper.GetWeatherApi("https://geoapi.qweather.com/v2/city/lookup", dic);
            //Location location = JasonSerialization.JsonToObject<Location>(location_resp);
            //if (location.code != "200")
            //{
            //    //return code = location.code;
            //}
            #endregion

            #region 获取城市实时天气
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic = new Dictionary<string, string>();
            dic.Add("location", location.LocationId);
            dic.Add("key", WeatherHelper.ApiKey);

            string weather_resp = await WeatherHelper.GetWeatherApi("https://devapi.qweather.com/v7/weather/now", dic);
            Weather weather = JasonSerialization.JsonToObject<Weather>(weather_resp);
            if (weather.code != "200")
            {
                //return code = location.code;
            }
            #endregion
            SetWeather(location, weather);
            
            return weather;
        }

        private void SetWeather(SqlSugarModel.Location_Table location, Weather weather)
        {
            Location.Text = location.LocationNameZh;
            weathertext.Text = weather.now.text;
            temperature.Text = weather.now.temp + "℃";
            updataTime.Text = Convert.ToDateTime(weather.updateTime).ToString("HH:mm") + " 更新";
            icon.Source = new Uri(GetIcon(weather.now.icon), UriKind.Absolute);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
