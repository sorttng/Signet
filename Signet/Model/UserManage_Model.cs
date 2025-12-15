using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Signet.Common;
using System.Collections.ObjectModel;
namespace Signet.Model
{
    public class UserManage_Model: ObservableObject
    {
        //标题
        private string _Header;
        public string Header
        {
            get { return _Header; }
            set { _Header = value; RaisePropertyChanged(() => Header); }
        }

        ////用户信息
        //private SqlSugarModel.User_Table _mUser_Table;
        //public SqlSugarModel.User_Table mUser_Table
        //{
        //    get { return _mUser_Table; }
        //    set { _mUser_Table = value; RaisePropertyChanged(() => mUser_Table); }
        //}

        //用户名
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(() => UserName); }
        }


        //部门列表
        private ObservableCollection<SqlSugarModel.Depart_Table> _Departs;
        public ObservableCollection<SqlSugarModel.Depart_Table> Departs
        {
            get { return _Departs; }
            set { _Departs = value; RaisePropertyChanged(() => Departs); }
        }

        //选中的部门
        private SqlSugarModel.Depart_Table _Selected_Depart;
        public SqlSugarModel.Depart_Table Selected_Depar
        {
            get { return _Selected_Depart; }
            set { _Selected_Depart = value; RaisePropertyChanged(() => Selected_Depar); }
        }

        //角色列表
        private ObservableCollection<SqlSugarModel.Role_Table> _Roles;
        public ObservableCollection<SqlSugarModel.Role_Table> Roles
        {
            get { return _Roles; }
            set { _Roles = value; RaisePropertyChanged(() => Roles); }
        }

        //选中的角色
        private SqlSugarModel.Role_Table _Selected_Role;
        public SqlSugarModel.Role_Table Selected_Role
        {
            get { return _Selected_Role; }
            set { _Selected_Role = value; RaisePropertyChanged(() => Selected_Role); }
        }

        //电话
        private string _PhoneNum;
        public string PhoneNum
        {
            get { return _PhoneNum; }
            set { _PhoneNum = value; RaisePropertyChanged(() => PhoneNum); }
        }

        //邮箱
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; RaisePropertyChanged(() => Email); }
        }

        //出生日期
        private DateTime _Birthday;
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; RaisePropertyChanged(() => Birthday); }
        }
    }
}
