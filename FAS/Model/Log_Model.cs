using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.SqlSugarModel;
using System.Collections.ObjectModel;
namespace FAS.Model
{
    public  class Log_Model: ObservableObject
    {
        /// <summary>
        /// 日志查询开始时间
        /// </summary>
        private DateTime _LogStartTime;
        public DateTime LogStartTime
        {
            get { return _LogStartTime; }
            set { _LogStartTime = value; RaisePropertyChanged(() => LogStartTime); }
        }

        /// <summary>
        /// 日志查询结束时间
        /// </summary>
        private DateTime _LogEndTime;
        public DateTime LogEndTime
        {
            get { return _LogEndTime; }
            set { _LogEndTime = value; RaisePropertyChanged(() => LogEndTime); }
        }


        /// <summary>
        /// 日志数据
        /// </summary>
        private ObservableCollection<Nlog_Model> _LogList;
        public ObservableCollection<Nlog_Model> LogList
        {
            get { return _LogList; }
            set { _LogList = value; RaisePropertyChanged(() => LogList); }
        }

    }
}
