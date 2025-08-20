using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows.Media.Imaging;
namespace Signet.Model
{
    public class About_Model: ObservableObject
    {

        private string _ImgPath;
        public string ImgPath
        {
            get { return _ImgPath; }
            set { _ImgPath = value; RaisePropertyChanged(() => ImgPath); }
        }

        private string _SoftName;
        public string SoftName
        {
            get { return _SoftName; }
            set { _SoftName = value; RaisePropertyChanged(() => SoftName); }
        }

        private string _Version;
        public string Version
        {
            get { return _Version; }
            set { _Version = value; RaisePropertyChanged(() => Version); }
        }

        private string _CopyRight;
        public string CopyRight
        {
            get { return _CopyRight; }
            set { _CopyRight = value; RaisePropertyChanged(() => CopyRight); }
        }

        private string _ContactInfo;
        public string ContactInfo
        {
            get { return _ContactInfo; }
            set { _ContactInfo = value; RaisePropertyChanged(() => ContactInfo); }
        }

        private string _LicnseEndTime;
        public string LicnseEndTime
        {
            get { return _LicnseEndTime; }
            set { _LicnseEndTime = value; RaisePropertyChanged(() => LicnseEndTime); }
        }
    }
}
