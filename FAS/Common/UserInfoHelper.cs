using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAS.SqlSugarModel;
namespace FAS.Model
{
    public class UserInfo_Model
    {
        private System.Int64 _UserID;
        /// <summary>
        /// UserID
        /// </summary>
        public System.Int64 UserID { get; set; }

        private System.String _UserName;
        /// <summary>
        /// UserName
        /// </summary>
        public System.String UserName { get; set; }

        private System.String _UserDepar;
        /// <summary>
        /// UserDepar
        /// </summary>
        public System.String UserDepar { get; set; }

        private System.String _Birthday;
        /// <summary>
        /// Birthday
        /// </summary>
        public System.String Birthday { get; set; }

        private System.String _Password;
        /// <summary>
        /// Password
        /// </summary>
        public System.String Password { get; set; }

        private long _UserRoleID;
        /// <summary>
        /// UserRoleID
        /// </summary>
        public long UserRoleID { get; set; }

        private System.String _UserRoleCode;
        /// <summary>
        /// UserRoleCode
        /// </summary>
        public System.String UserRoleCode { get; set; }

        private System.String _UserRole;
        /// <summary>
        /// UserRole
        /// </summary>
        public System.String UserRole { get; set; }

        private List<string> _Authorities;
        /// <summary>
        /// Authorities
        /// </summary>
        public List<string> Authorities { get; set; }
    }

    public class UserInfo
    {
        public static UserInfo_Model LogedUserInfo;
    }
}
