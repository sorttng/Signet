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
using System.Windows.Threading;
namespace FAS.UI
{
    /// <summary>
    /// TimeControl.xaml 的交互逻辑
    /// </summary>
    public partial class TimeControl : UserControl
    {
        public TimeControl()
        {
            InitializeComponent();
            this.SetTime();
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(async (object s, EventArgs a) =>
            {
                this.SetTime();
            });
            timer.Start();
        }

        void SetTime()
        {
            this.txtTime.Text = DateTime.Now.ToString("HH:mm");
            this.txtTimeSecond.Text = DateTime.Now.ToString("ss");
            this.txtTimeAMPM.Text = DateTime.Now.ToString("tt");
        }

    }
}
