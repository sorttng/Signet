using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace FAS.Common
{
    public enum EAuthorizationItem
    {
        [Description("日志查询")]
        LogQuery
    }

    public class AuthorizationItemDefine : ObservableObject
    {
        public static AuthorizationItemDefine Default { get { return m_Default; } }
        private static AuthorizationItemDefine m_Default = new AuthorizationItemDefine();
        AuthorizationItemDefine()
        {
        }

        public void RiseProperty()
        {
            RaisePropertyChanged(() => LogQuery);
        }

        public EAuthorizationItem LogQuery
        {
            get
            { return EAuthorizationItem.LogQuery; }
        }
    }
}
