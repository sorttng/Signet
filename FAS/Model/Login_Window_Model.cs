using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace FAS.Model
{
    public class Login_Window_Model: ObservableObject
    {
        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(() => UserName); }
        }

        private string _Password=string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(() => Password); }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(this.UserName) && this.UserName == string.Empty)
                {
                    return "Number is not greater than 10!";
                }

                return null;
            }
        }


    }
}
