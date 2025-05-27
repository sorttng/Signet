using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace FAS.SqlSugarModel
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("Role_Table")]
    public class Role_Table
    {

        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Role_ID", IsPrimaryKey = true)]
        public long Role_ID { get; set; }

        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Role_Code")]
        public string Role_Code { get; set; } = null;
     
        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Role_Name")]
        public string Role_Name { get; set; } = null;
    }
}