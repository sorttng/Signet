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
using Signet.Common;
namespace Signet.View
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterKey_View : UserControl
    {
        public RegisterKey_View()
        {
            InitializeComponent();
        }

        private void Copy_Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(DevCode.Text);
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DevCode.Text = RegisterHelper.ReadCryptographFile();
        }
    }
}
