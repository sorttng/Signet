using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.Model;
using GalaSoft.MvvmLight.Command;
using FAS.Common;
using FAS.SqlSugarModel;
using MahApps.Metro.Controls.Dialogs;
namespace FAS.ViewModel
{
    public class Log_ViewModel: ViewModelBase
    {
        private Log_Model _mLog_Model;

        public Log_Model mLog_Model
        {
            get { return _mLog_Model; }
            set { _mLog_Model = value; RaisePropertyChanged(() => mLog_Model); }
        }
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public Log_ViewModel()
        {
            mLog_Model = new Log_Model()
            {
                LogStartTime = DateTime.Now.Date.AddDays(-1),
                LogEndTime = DateTime.Now.Date,
                LogList = new System.Collections.ObjectModel.ObservableCollection<SqlSugarModel.Nlog_Model>(),
            };
        }

        #region 查询
        private RelayCommand _Search_Command;
        public RelayCommand Search_Command
        {
            get
            {
                if (_Search_Command == null)
                {
                    _Search_Command = new RelayCommand(_Search);
                }
                return _Search_Command;
            }
            set { _Search_Command = value; }
        }
        /// <summary>
        /// 查询
        /// </summary>
        private void _Search()
        {
            try
            {
                mLog_Model.LogList = new System.Collections.ObjectModel.ObservableCollection<Nlog_Model>
                    (SqlSugarHelper.mDB.Queryable<Nlog_Model>()
                    .Where(a => a.Time >= mLog_Model.LogStartTime && a.Time <= mLog_Model.LogEndTime)
                    .ToList());
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            logger.Info("日志查询成功！");
        }
        #endregion

    }
}
