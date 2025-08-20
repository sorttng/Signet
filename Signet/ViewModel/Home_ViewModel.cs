using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Signet.Model;
using GalaSoft.MvvmLight.Command;
using System.Net;
using System.Data;
using System.Data.SQLite;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;


namespace Signet.ViewModel
{
    public class Home_ViewModel: ViewModelBase
    {
        private Home_Model _mHome_Model;

        public Home_Model mHome_Model
        {
            get { return _mHome_Model; }
            set { _mHome_Model = value; RaisePropertyChanged(() => mHome_Model); }
        }

        //创建新的 Logger 的开销很小，因为它必须获取锁并分配对象。
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        public Home_ViewModel() {


            _dialogCoordinator = DialogCoordinator.Instance;

            mHome_Model = new Home_Model
            {
            };

            Messenger.Default.Register<(string, string, ObservableCollection<string>)>(this, "OpcCfg", (message) => ReceivedMessage(message));
        }


        #region 消息接收事件
        public void ReceivedMessage((string, string, ObservableCollection<string>) message)
        {

        }
        #endregion


        #region Message
        private readonly IDialogCoordinator _dialogCoordinator;
        // Simple method which can be used on a Button
        public async void FooMessage(string title, string msg)
        {
            await _dialogCoordinator.ShowMessageAsync(this, title, msg);
        }
        #endregion
    }
}
