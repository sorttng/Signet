using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using FAS.Common;
using MahApps.Metro.Controls.Dialogs;
using FAS.View;
using NLog;
using SqlSugar;

namespace FAS.ViewModel
{
    public class Login_Window_ViewModel: ViewModelBase
    {

        private Login_Window_Model _mLogin_Window_Model;

        public Login_Window_Model mLogin_Window_Model
        {
            get { return _mLogin_Window_Model; }
            set { _mLogin_Window_Model = value; RaisePropertyChanged(() => mLogin_Window_Model); }
        }

        //创建新的 Logger 的开销很小，因为它必须获取锁并分配对象。
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        LoginHelper.LoginOpration mloginOpration;
        public Login_Window_ViewModel(LoginHelper.LoginOpration loginOpration)
        {
            
            mloginOpration = loginOpration;

            _dialogCoordinator = DialogCoordinator.Instance;

           //MappedDiagnosticsLogicalContext.Set("userId", string.Empty);

            mLogin_Window_Model = new Login_Window_Model()
            {
                UserName = "user1",
                Password = "123456",
            };

            //RegisterHelper.GenerateCryptographFile();
        }

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


        #region 登录
        private RelayCommand _Login_Command;
        public RelayCommand Login_Command
        {
            get
            {
                if (_Login_Command == null)
                {
                    _Login_Command = new RelayCommand(_Login);
                }
                return _Login_Command;
            }
            set { _Login_Command = value; }
        }

        private void _Login()
        {
            #region 卫语句
            //bool needLicense = false;
            ////软件授权检测
            //if (RegisterHelper.ExistLicenseFile())
            //{
            //    string mes = string.Empty;

            //    if (RegisterHelper.CheckLicense(RegisterHelper.ReadLicenseFile(), out mes))
            //    {
            //        needLicense = false;
            //    }
            //    else
            //    {
            //        needLicense = true;
            //        ShowMessage("提示！", mes);
            //        logger.Info(mes);
            //    }
            //}
            //else
            //{
            //    needLicense = true;
            //}
            //GlobalInfo.registerState = !needLicense;

            //if (needLicense)
            //{
            //    #region 输入证书弹窗
            //    var result = _dialogCoordinator.ShowModalLoginExternal(this, "未激活", "请输入您的激活码！",
            //    new LoginDialogSettings(((Login_Window)(Application.Current.MainWindow)).MetroDialogOptions)
            //    {
            //        InitialUsername = RegisterHelper.ReadCryptographFile(),
            //        UsernameWatermark = "设备编码",
            //        EnablePasswordPreview = true,
            //        PasswordWatermark = "请输入激活码",
            //        AffirmativeButtonText = "激活",
            //    });
            //    #endregion

            //    string mes = string.Empty;
            //    GlobalInfo.registerState =
            //    RegisterHelper.CheckLicense(result.Password, out mes);
            //    ShowMessage("提示！", mes);
            //    logger.Info(mes);
            //    if (!GlobalInfo.registerState)
            //    {
            //        return;
            //    }
            //}

            if (mLogin_Window_Model.UserName == string.Empty
                || mLogin_Window_Model.Password == string.Empty
                || mLogin_Window_Model.UserName == null
                || mLogin_Window_Model.Password == null)
            { ShowMessage("提示！", "用户名或密码不能为空！"); logger.Info("用户名或密码不能为空！"); return; }

            var userInfo = SqlSugarHelper.mDB.Queryable<SqlSugarModel.User_Table>().
                Where(a => a.UserName == mLogin_Window_Model.UserName);
            if (userInfo.Count() == 0 || userInfo == null)
            {
                ShowMessage("提示！", "用户名或密码错误！"); logger.Info("用户名或密码错误！"); return;
            }

            SqlSugarModel.User_Table info = userInfo.First();
            if (info.Password != mLogin_Window_Model.Password)
            {
                ShowMessage("提示！", "用户名或密码错误！"); logger.Info("用户名或密码错误！"); return;
            }
            #endregion

            if (mloginOpration == LoginHelper.LoginOpration.Login)
            {
                //登录用户全局使用
                var uinfo = SqlSugarHelper.mDB.Queryable<SqlSugarModel.User_Table, SqlSugarModel.Role_Table, SqlSugarModel.Role_Auth_Relationship_Table, SqlSugarModel.Authorities_Model>((ut, rt, rart, at) => new JoinQueryInfos(
                   JoinType.Left, ut.UserRole == rt.Role_ID,
                   JoinType.Left, rt.Role_ID == rart.Role_ID,
                   JoinType.Left, rart.Authority_ID == at.Authorities_ID
                   ))
                   .Where(ut => ut.UserID == info.UserID)
                   .GroupBy((ut, rt, rart, at) => ut.UserID)
                   .Select((ut, rt, rart, at) => new
                   {
                       uid = ut.UserID,
                       uname = ut.UserName,
                       udepar = ut.UserDepar,
                       ubirthday = ut.Birthday,
                       rtrolecode = rt.Role_Code,
                       rtrolename = rt.Role_Name,
                       rtroleid = rt.Role_ID,
                       atauthority = SqlFunc.MappingColumn<string>("string", "GROUP_CONCAT(DISTINCT at.Authority_Name) "),
                   }).ToList().FirstOrDefault();

                UserInfo.LogedUserInfo = new UserInfo_Model
                {
                    UserID = uinfo.uid,
                    UserName = uinfo.uname,
                    UserDepar = uinfo.udepar,
                    Birthday = uinfo.ubirthday,
                    UserRoleCode = uinfo.rtrolecode,
                    UserRole = uinfo.rtrolename,
                    UserRoleID = uinfo.rtroleid,
                    Authorities = uinfo.atauthority == null ? new List<string>() : uinfo.atauthority.Split(',').ToList(),
                };

                AuthorizationItemDefine.Default.RiseProperty();//刷新属性
                //传参给nlog配置文件
                MappedDiagnosticsLogicalContext.Set("userId", info.UserID);
                logger.Info("登录成功！");
            }
            else
            {
                logger.Info("系统退出成功！");
                System.Environment.Exit(0);
            }

            //关闭本窗口打开新窗口
            //WindowManager.Show("MainWindow", new MainWindow_ViewModel());

            //login_Model.ISvisible = Visibility.Hidden;
            //LogHelper.LogInfo logInfo = new LogHelper.LogInfo()
            //{ Describe = "登录成功" };
            //LogHelper.Logger.Info(LogHelper.JasonLog(logInfo));
            ToClose = true;
        }
        #endregion

        #region 关闭窗口函数
        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                toClose = value;
                if (toClose)
                {
                    this.RaisePropertyChanged("ToClose");
                }
            }
        }
        #endregion
    }
}
