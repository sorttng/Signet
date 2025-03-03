using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.Model;
namespace FAS.ViewModel
{
    public class ModbusServerSetting_ViewModel : ViewModelBase
    {
        private ModbusServerSetting_Model _mModbusServerSetting_Model;

        public ModbusServerSetting_Model mModbusServerSetting_Model
        {
            get { return _mModbusServerSetting_Model; }
            set { _mModbusServerSetting_Model = value; RaisePropertyChanged(() => mModbusServerSetting_Model); }
        }

        public ModbusServerSetting_ViewModel()
        {

        }
    }
}
