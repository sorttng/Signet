using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signet.Common
{
    public class DateHelper
    {
        /// <summary>
        /// 获取实时时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTimeNow()
        {

            //var timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += new EventHandler(async (object s, EventArgs a) =>
            //{
            //    this.SetTime();
            //});
            //timer.Start();

            return DateTime.Now;
        }


    }
}
