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
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Data;
using OPCAutomation;
using System.ComponentModel;
using MahApps.Metro.Controls.Dialogs;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Threading;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;
using TitaniumAS.Opc.Client.Da.Browsing;

namespace FAS.ViewModel
{
    public class DataSetting_ViewModel: ViewModelBase
    {
        private DataSetting_Model _mDataSetting_Model;

        public DataSetting_Model mDataSetting_Model
        {
            get { return _mDataSetting_Model; }
            set { _mDataSetting_Model = value; RaisePropertyChanged(() => mDataSetting_Model); }
        }


        public DataSetting_ViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;

            string serverIP = "localhost";
            ObservableCollection<OPCDAVariable> focusList = new ObservableCollection<OPCDAVariable>();
            var opcCfg_tmp = SqlSugarHelper.mDB.Queryable<SqlSugarModel.OpcCfg_Table>().OrderBy(a => a.DatabaseEntryTime, SqlSugar.OrderByType.Desc);
            if (opcCfg_tmp == null || opcCfg_tmp.Count() == 0)
            {
                serverIP = "localhost";
                focusList = new ObservableCollection<OPCDAVariable>();
            }
            else
            {
                //读取配置
                SqlSugarModel.OpcCfg_Table opcCfg = opcCfg_tmp.First();
                serverIP = opcCfg.ServerIP;
                focusList = JasonSerialization.JsonToObject<ObservableCollection<OPCDAVariable>>(opcCfg.PointList);
            }

            mDataSetting_Model = new DataSetting_Model {
                ServerIp = serverIP,
                ServerList = new System.Collections.ObjectModel.ObservableCollection<string>(),
                ItemList = new System.Collections.ObjectModel.ObservableCollection<string>(),                
                FocusList = focusList,
            };
            //FooMessage("提示！", "111");
        }

        //创建新的 Logger 的开销很小，因为它必须获取锁并分配对象。
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        /// <summary>
        /// 获取目标地址的OPC服务器名称
        /// </summary>
        private List<string> GetLocalServer(string IpAddr)
        {
            List<string> serverListStr = new List<string>();
            try
            {
                var enumerator = new OpcServerEnumeratorAuto();
                var serverDescriptions = enumerator.Enumerate(IpAddr, OpcServerCategory.OpcDaServer10);
                foreach (var item in serverDescriptions)
                {
                    serverListStr.Add(item.ProgId);
                }
            }
            catch (Exception err)
            {
                string mes = "枚举本地OPC服务器出错：" + err.Message;
                ShowMessage("错误！", mes);
                logger.Error(mes);
            }

            return serverListStr;
        }

        private void BrowseChildren(IOpcDaBrowser browser, string itemId = null, int indent = 0)
        {
            // Browse elements.
            // When itemId is null, root elements will be browsed.
            OpcDaBrowseElement[] elements = browser.GetElements(itemId);

            // Output elements.
            foreach (OpcDaBrowseElement element in elements)
            {
                // Output the element.
                //Console.Write(new String(' ', indent));
                //Console.WriteLine(element);

                // Skip elements without children.
                if (!element.HasChildren)
                {
                    mDataSetting_Model.ItemList.Add(element.ItemId);
                    continue;
                }

                // Output children of the element.
                BrowseChildren(browser, element.ItemId, indent + 2);
            }
        }


        #region 更新服务器
        private RelayCommand _RefreshServers_Command;
        public RelayCommand RefreshServers_Command
        {
            get
            {
                if (_RefreshServers_Command == null)
                {
                    _RefreshServers_Command = new RelayCommand(_RefreshServers);
                }
                return _RefreshServers_Command;
            }
            set { _RefreshServers_Command = value; }
        }
        /// <summary>
        /// 更新服务器
        /// </summary>
        private void _RefreshServers()
        {
            if (mDataSetting_Model.ServerIp == string.Empty)
            {
                string mes = "输入服务器IP地址不合法！";
                ShowMessage("提示！", mes);
                logger.Info(mes);
                return;
            }

            mDataSetting_Model.ServerList = new System.Collections.ObjectModel.ObservableCollection<string>
                (GetLocalServer(mDataSetting_Model.ServerIp));
            if (mDataSetting_Model.ServerList.Count() == 0)
            {
                string mes = "未找到可用的OPC服务！";
                ShowMessage("提示！", mes);
                logger.Info(mes);
                return;
            }
        }
        #endregion

        #region 更新数据列表
        private RelayCommand _Connect_Command;
        public RelayCommand Connect_Command
        {
            get
            {
                if (_Connect_Command == null)
                {
                    _Connect_Command = new RelayCommand(_Connect);
                }
                return _Connect_Command;
            }
            set { _Connect_Command = value; }
        }
        /// <summary>
        /// 获取所有点表
        /// </summary>
        private void _Connect()
        {
            try
            {
                Uri url = UrlBuilder.Build(mDataSetting_Model.ServerNode, mDataSetting_Model.ServerIp);
                var server = new OpcDaServer(url);
                server.Connect();

                var browser = new OpcDaBrowserAuto(server);
                OpcDaBrowseElement[] opcDaBrowseElements = browser.GetElements(null);
                //int i = opcDaBrowseElements.Count();
                mDataSetting_Model.ItemList = new ObservableCollection<string>();
                BrowseChildren(browser);
                server.Disconnect();                
            }
            catch (Exception ex)
            {
                string mes = "连接失败：" + ex.Message;
                ShowMessage("错误！", mes);
                logger.Error(mes);
                return;
            }
        }

        #endregion

        #region 添加
        private RelayCommand _Add_Command;
        public RelayCommand Add_Command
        {
            get
            {
                if (_Add_Command == null)
                {
                    _Add_Command = new RelayCommand(_Add);
                }
                return _Add_Command;
            }
            set { _Add_Command = value; }
        }
        /// <summary>
        /// 新增
        /// </summary>
        private void _Add()
        {
            //if (mDataSetting_Model.SelItem == null)
            //{
            //    return;
            //}


            //如果存在无法添加
            if (mDataSetting_Model.FocusList.Select(a => a.OPCItemID).ToArray().Contains(mDataSetting_Model.SelItem)
                || mDataSetting_Model.SelItem == null)
            {
                ShowMessage("提示！","列表中已经存在改项请勿重复添加！");
                return;
            }

            //点表中不包含改项
            if (!GlobalInfo.GloblePoint_Table.Select(a => a.ProxyTagName).ToArray().Contains(mDataSetting_Model.SelItem))
            {
                ShowMessage("提示！", "点表中不包含改项无法添加！");
                return;
            }

            mDataSetting_Model.FocusList.Add(new OPCDAVariable()
            {
                OPCItemID = mDataSetting_Model.SelItem,
                //ClientHandle = Guid.NewGuid().ToString(),
            });

            for (int i = 0; i < mDataSetting_Model.FocusList.Count; i++)
            {
                mDataSetting_Model.FocusList[i].ClientHandle = i+1;
            }
        }
        #endregion

        #region 删除
        private RelayCommand _Remove_Command;
        public RelayCommand Remove_Command
        {
            get
            {
                if (_Remove_Command == null)
                {
                    _Remove_Command = new RelayCommand(_Remove);
                }
                return _Remove_Command;
            }
            set { _Remove_Command = value; }
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void _Remove()
        {
            if(mDataSetting_Model.SelFocusItem!= null)
            {
                mDataSetting_Model.FocusList.Remove(mDataSetting_Model.SelFocusItem);

                for (int i = 0; i < mDataSetting_Model.FocusList.Count; i++)
                {
                    mDataSetting_Model.FocusList[i].ClientHandle = i + 1;
                }
            }
        }
        #endregion

        #region 保存
        private RelayCommand _Save_Command;
        public RelayCommand Save_Command
        {
            get
            {
                if (_Save_Command == null)
                {
                    _Save_Command = new RelayCommand(_Save);
                }
                return _Save_Command;
            }
            set { _Save_Command = value; }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void _Save()
        {
            #region 卫语句
            if (mDataSetting_Model.ServerIp == string.Empty)
            {
                string mess = "输入服务器IP地址不合法！";
                ShowMessage("提示！", mess);
                logger.Info(mess);
                return;
            }

            if (GetLocalServer(mDataSetting_Model.ServerIp).Count() == 0)
            {
                string mess = "未找到可用的OPC服务！";
                ShowMessage("提示！", mess);
                logger.Info(mess);
                return;
            }

            if (mDataSetting_Model.FocusList == null
                || mDataSetting_Model.FocusList.Count() == 0)
            {
                string mess = "未添加需要监视的标签！";
                ShowMessage("提示！", mess);
                logger.Info(mess);
                return;
            }

            if (UserInfo.LogedUserInfo.UserName == null || 
                UserInfo.LogedUserInfo.UserName == string.Empty)
            {
                string mess = "修改配置需要先登录！";
                ShowMessage("提示！", mess);
                logger.Info(mess);
                return;
            }
            #endregion

            //配置信息入库
            SqlSugarModel.OpcCfg_Table opcCfg_Table = new OpcCfg_Table() {
                ServerIP = mDataSetting_Model.ServerIp,
                ServerNode = mDataSetting_Model.ServerNode,
                PointList = JasonSerialization.ObjectToJson(mDataSetting_Model.FocusList),
            };
            SqlSugarHelper.mDB.Insertable(opcCfg_Table).ExecuteCommand();

            //发送配置信息
            Messenger.Default.Send<(string,string,ObservableCollection<OPCDAVariable>)>
    ((mDataSetting_Model.ServerIp,mDataSetting_Model.ServerNode,mDataSetting_Model.FocusList), "OpcCfg");

            string mes = "OPC配置保存成功！";
            ShowMessage("提示！", mes);
            logger.Info(mes);
        }
        #endregion

        #region Message
        private readonly IDialogCoordinator _dialogCoordinator;
        // Simple method which can be used on a Button
        public async void ShowMessage_async(string title, string msg)
        {
            await _dialogCoordinator.ShowMessageAsync(this, title, msg);
        }

        public void ShowMessage(string title, string msg)
        {
            _dialogCoordinator.ShowModalMessageExternal(this, title, msg);
        }
        #endregion
    }
}
