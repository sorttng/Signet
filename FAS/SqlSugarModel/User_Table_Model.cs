using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace FAS.SqlSugarModel
{
    /// <summary>
    /// User_Table
    /// </summary>
    [SugarTable("User_Table")]
    public class User_Table
    {
        /// <summary>
        /// User_Table
        /// </summary>
        public User_Table()
        {
        }

        private System.Int64 _UserID;
        /// <summary>
        /// UserID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int64 UserID { get { return this._UserID; } set { this._UserID = value; } }

        private System.String _UserName;
        /// <summary>
        /// UserName
        /// </summary>
        public System.String UserName { get { return this._UserName; } set { this._UserName = value; } }

        private System.String _UserDepar;
        /// <summary>
        /// UserDepar
        /// </summary>
        public System.String UserDepar { get { return this._UserDepar; } set { this._UserDepar = value; } }

        private System.String _Birthday;
        /// <summary>
        /// Birthday
        /// </summary>
        public System.String Birthday { get { return this._Birthday; } set { this._Birthday = value; } }

        private System.String _Password;
        /// <summary>
        /// Password
        /// </summary>
        public System.String Password { get { return this._Password; } set { this._Password = value; } }

        private System.String _UserRole;
        /// <summary>
        /// UserRole
        /// </summary>
        public System.String UserRole { get { return this._UserRole; } set { this._UserRole = value; } }
    }
}
