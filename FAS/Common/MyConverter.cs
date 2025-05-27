using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using FAS.Model;
namespace FAS.Common
{
    public class AuthConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if (UserInfo.LogedUserInfo.Authorities == null)
                return false;
            string machAu = value.ToString();
            if (machAu == string.Empty)
                return true;//如果该控件权限为空的话则全部有权限访问

            if (UserInfo.LogedUserInfo.Authorities.Contains(machAu))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
