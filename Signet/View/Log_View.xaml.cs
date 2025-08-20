using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
namespace Signet.View
{
    /// <summary>
    /// Log_View.xaml 的交互逻辑
    /// </summary>
    public partial class Log_View : Page
    {
        public Log_View()
        {
            InitializeComponent();
        }


        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding_StartTime_Dtpk = StartTime_Dtpk.GetBindingExpression(MahApps.Metro.Controls.DateTimePicker.SelectedDateTimeProperty);
            binding_StartTime_Dtpk.UpdateSource();

            BindingExpression binding_EndTime_Dtpk = EndTime_Dtpk.GetBindingExpression(MahApps.Metro.Controls.DateTimePicker.SelectedDateTimeProperty);
            binding_StartTime_Dtpk.UpdateSource();
        }
    }
}
