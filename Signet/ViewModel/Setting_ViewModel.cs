﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Signet.Model;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using System.Windows.Media;
//using System.Windows;
using System.Configuration;
using Signet.Common;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using GalaSoft.MvvmLight.Messaging;
namespace Signet.ViewModel
{
    public class Setting_ViewModel: ViewModelBase
    {
        private Setting_Model _mSetting_Model;

        public Setting_Model mSetting_Model
        {
            get { return _mSetting_Model; }
            set { _mSetting_Model = value; RaisePropertyChanged(() => mSetting_Model); }
        }


        public Setting_ViewModel()
        {
            mSetting_Model = new Setting_Model()
            {
                // create accent color menu items for the demo
                AccentColors = ThemeManager.Current.Themes
                                            .GroupBy(x => x.ColorScheme)
                                            .OrderBy(a => a.Key)
                                            .Select(a => new AccentColorMenuData { Name = a.Key, ColorBrush = a.First().ShowcaseBrush })
                                            .ToList(),
                // create metro theme color menu items for the demo
                AppThemes = ThemeManager.Current.Themes
                                         .GroupBy(x => x.BaseColorScheme)
                                         .Select(x => x.First())
                                         .Select(a => new AppThemeMenuData { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as Brush, ColorBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as Brush })
                                         .ToList(),

                Locations = GlobalInfo.Locations,
                Sel_Location = GlobalInfo.CurLocation,
            };

            #region 配置主题
            mSetting_Model.Sel_Theme = mSetting_Model.AppThemes.Where(a => a.Name == Theme_Model.Theme).First();
            mSetting_Model.IsOn = ThemeConvert(mSetting_Model.Sel_Theme);

            mSetting_Model.Sel_AccentColors = mSetting_Model.AccentColors.Where(a => a.Name == Theme_Model.Colors).First();            
            #endregion
        }


        private bool ThemeConvert(AppThemeMenuData appThemeMenuData)
        {
            bool rt = false;
            if (appThemeMenuData.Name == "Light")
                rt = true;
            else
                rt = false;

            return rt;
        }


        #region 设置主题颜色
        private RelayCommand _ChangeColors_Command;
        public RelayCommand ChangeColors_Command
        {
            get
            {
                if (_ChangeColors_Command == null)
                {
                    _ChangeColors_Command = new RelayCommand(_ChangeColors);
                }
                return _ChangeColors_Command;
            }
            set { _ChangeColors_Command = value; }
        }

        private void _ChangeColors()
        {
            string colors = mSetting_Model.Sel_AccentColors.Name;
            ThemeManager.Current.ChangeThemeColorScheme(System.Windows.Application.Current, colors);

            ConfigFileHelper.ConfigSet("Colors", colors);
        }
        #endregion

        #region 设置主题
        private RelayCommand<ToggleSwitch> _ChangeThemes_Command;
        public RelayCommand<ToggleSwitch> ChangeThemes_Command
        {
            get
            {
                if (_ChangeThemes_Command == null)
                {
                    _ChangeThemes_Command = new RelayCommand<ToggleSwitch>(_ChangeThemes);
                }
                return _ChangeThemes_Command;
            }
            set { _ChangeThemes_Command = value; }
        }

        private void _ChangeThemes(ToggleSwitch mtoggleSwitch)
        {
            string theme = string.Empty;
            //if (mtoggleSwitch.IsOn)
            //    theme = mtoggleSwitch.OnContent.ToString();
            //else
            //    theme = mtoggleSwitch.OffContent.ToString();

            if (mSetting_Model.IsOn)
                theme = mtoggleSwitch.OnContent.ToString();
            else
                theme = mtoggleSwitch.OffContent.ToString();
            ThemeManager.Current.ChangeThemeBaseColor(System.Windows.Application.Current, theme);

            ConfigFileHelper.ConfigSet("Theme", theme);
        }
        #endregion

        #region 设置语言
        private RelayCommand _ChangeLanguage_Command;
        public RelayCommand ChangeLanguage_Command
        {
            get
            {
                if (_ChangeLanguage_Command == null)
                {
                    _ChangeLanguage_Command = new RelayCommand(_ChangeLanguage);
                }
                return _ChangeLanguage_Command;
            }
            set { _ChangeLanguage_Command = value; }
        }

        private void _ChangeLanguage()
        {
            //string PythonScriptPath = @"C:\Users\10525\Desktop\py" + "\\1.py";
            //ScriptEngine PyEngine = Python.CreateEngine();
            //dynamic py = PyEngine.ExecuteFile(PythonScriptPath);
            //py.main();

            //CultureInfo culture = new CultureInfo("en");
            //Thread.CurrentThread.CurrentUICulture = culture;
        }
        #endregion

        #region 设置地区
        private RelayCommand _Location_Changed_Command;
        public RelayCommand Location_Changed_Command
        {
            get
            {
                if (_Location_Changed_Command == null)
                {
                    _Location_Changed_Command = new RelayCommand(Location_Changed);
                }
                return _Location_Changed_Command;
            }
            set { _Location_Changed_Command = value; }
        }

        private void Location_Changed()
        {
            if (mSetting_Model.Sel_Location != null)
            {
                Messenger.Default.Send<SqlSugarModel.Location_Table>(mSetting_Model.Sel_Location, "Location");
                GlobalInfo.CurLocation = mSetting_Model.Sel_Location;
                ConfigFileHelper.ConfigSet("Location_ID", mSetting_Model.Sel_Location.LocationId);
            }
        }
        #endregion
    }
}
