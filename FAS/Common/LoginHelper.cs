using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace FAS.Common
{
    public class LoginHelper
    {
        public enum LoginOpration
        {
            [Description("登录")]
            Login,
            [Description("退出")]
            Quit,
        }
    }
}
