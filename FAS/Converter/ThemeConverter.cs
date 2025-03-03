using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using FAS.Model;
namespace FAS.Converter
{
    public class ThemeConverter : IValueConverter
    {
        //AppThemeMenuData
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int c = System.Convert.ToInt32(parameter);

            if (value == null)
                return true;
            string theme = value.ToString();
            //string theme = ((AppThemeMenuData)value).Name;

            if (theme == "Light")
                return true;
            else
                return false;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
