﻿using System;
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
using MenuItem = FAS.ViewModel.MenuItem;
using FAS.Common;
using FAS.View;
using NLog;
using NLog.Targets;
namespace FAS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Navigation.NavigationServiceEx navigationServiceEx;

        public MainWindow()
        {
            InitializeComponent();

            this.navigationServiceEx = new Navigation.NavigationServiceEx();
            this.navigationServiceEx.Navigated += this.NavigationServiceEx_OnNavigated;
            this.HamburgerMenuControl.Content = this.navigationServiceEx.Frame;

            // Navigate to the home page.
            this.Loaded += (sender, args) => this.navigationServiceEx.Navigate(new Uri("View/Home_View.xaml", UriKind.RelativeOrAbsolute));

        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs e)
        {
            if (e.InvokedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                this.navigationServiceEx.Navigate(menuItem.NavigationDestination);
                this.navigationServiceEx.Frame.Tag = menuItem.Label;
            }
        }

        private void NavigationServiceEx_OnNavigated(object sender, NavigationEventArgs e)
        {      
            // select the menu item
            this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
                                                         .Items
                                                         .OfType<MenuItem>()
                                                         .FirstOrDefault(x => x.NavigationDestination == e.Uri);
            this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
                                                                .OptionsItems
                                                                .OfType<MenuItem>()
                                                                .FirstOrDefault(x => x.NavigationDestination == e.Uri);

            // or when using the NavigationType on menu item
            this.HamburgerMenuControl.SelectedItem = this.HamburgerMenuControl
                                                         .Items
                                                         .OfType<MenuItem>()
                                                         .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());
            this.HamburgerMenuControl.SelectedOptionsItem = this.HamburgerMenuControl
                                                                .OptionsItems
                                                                .OfType<MenuItem>()
                                                                .FirstOrDefault(x => x.NavigationType == e.Content?.GetType());

            // update back button
            this.GoBackButton.Visibility = this.navigationServiceEx.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            this.navigationServiceEx.GoBack();
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            //需要在进入应用程序关闭阶段之前停止线程/计时器。如果不这样做，将导致未经处理的异常和分段错误，以及其他不可预知的行为。
            NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
            
            System.Environment.Exit(0);
        }

    }
}
