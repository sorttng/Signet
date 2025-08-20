using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Signet.Model;
using Signet.Common;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.IO;
namespace Signet.ViewModel
{
    public class About_ViewModel : ViewModelBase
    {
        private About_Model _mAbout_Model;

        public About_Model mAbout_Model
        {
            get { return _mAbout_Model; }
            set { _mAbout_Model = value; RaisePropertyChanged(() => mAbout_Model); }
        }

        public About_ViewModel()
        {
            mAbout_Model = new About_Model()
            {
                //Img = SoftwareInfoHelper.SoftwareLogoImg,
                ImgPath = SoftwareInfoHelper.SoftwareLogoPath,
                SoftName = SoftwareInfoHelper.SoftwareName,
                Version = SoftwareInfoHelper.Version,
                CopyRight = SoftwareInfoHelper.Copyright,
                ContactInfo = SoftwareInfoHelper.ContactInfo,
                LicnseEndTime = "证书有效期："+ GlobalInfo.registerTime.ToString("yyyy/MM/dd"),
            };
        }

        #region 检查更新
        private RelayCommand _Update_Command;
        public RelayCommand Update_Command
        {
            get
            {
                if (_Update_Command == null)
                {
                    _Update_Command = new RelayCommand(Update);
                }
                return _Update_Command;
            }
            set { _Update_Command = value; }
        }

        private void Update()
        {
            FtpHelper ftpHelper = new FtpHelper(SqlSugarHelper.ServerIP, "","Visitor","123456789a+");            
            string severVersion = ftpHelper.ReadFile("Version");
            //如果服务器指向版本合本地不同则更新
            //if (severVersion != SoftwareInfoHelper.Version)
            {
                //以下代码用以运行升级程序并传参当前版本号
                ProcessStartInfo versionUpdatePrp = new ProcessStartInfo("update.exe", severVersion);
                versionUpdatePrp.WorkingDirectory = Directory.GetCurrentDirectory();
                Process newProcess = new Process();
                newProcess.StartInfo = versionUpdatePrp;
                newProcess.Start();
            }
        }
        #endregion

    }
}
