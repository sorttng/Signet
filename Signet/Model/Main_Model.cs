using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Signet.Common;
using Signet.ViewModel;
namespace Signet.Model
{
    public class Main_Model : ObservableObject
    {
        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(() => UserName); }
        }

        private string _UserHeadSculpturePath;
        /// <summary>
        /// 用户头像路径
        /// </summary>
        public string UserHeadSculpturePath
        {
            get { return _UserHeadSculpturePath; }
            set { _UserHeadSculpturePath = value; RaisePropertyChanged(() => UserHeadSculpturePath); }
        }

        private string _Company;
        /// <summary>
        /// 公司名
        /// </summary>
        public string Company
        {
            get { return _Company; }
            set { _Company = value; RaisePropertyChanged(() => Company); }
        }

        private string _SoftwareLogoIconPath;
        /// <summary>
        /// 图标路径
        /// </summary>
        public string SoftwareLogoIconPath
        {
            get { return _SoftwareLogoIconPath; }
            set { _SoftwareLogoIconPath = value; RaisePropertyChanged(() => SoftwareLogoIconPath); }
        }        
    }
}
