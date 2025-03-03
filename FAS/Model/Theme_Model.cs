using System;
using GalaSoft.MvvmLight.Command;
using ControlzEx.Theming;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;

namespace FAS.Model
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }

        public Brush BorderColorBrush { get; set; }

        public Brush ColorBrush { get; set; }

        public AccentColorMenuData()
        {
            this.ChangeAccentCommand = new RelayCommand<string>(this.DoChangeTheme);
        }

        public ICommand ChangeAccentCommand { get; }

        protected virtual void DoChangeTheme(string name)
        {
            if (name != null)
            {
                ThemeManager.Current.ChangeThemeColorScheme(Application.Current, name);
            }
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(string name)
        {
            if (name != null)
            {
                ThemeManager.Current.ChangeThemeBaseColor(Application.Current, name);
            }
        }
    }


    public class Theme_Model
    {
        public static string Theme = "Light";
        public static string Colors = "Amber";
    }
}
