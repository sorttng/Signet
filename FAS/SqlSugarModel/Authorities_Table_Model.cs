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
    [SugarTable("Authorities_Table")]
    public class Authorities_Model
    {
        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Authorities_ID", IsPrimaryKey = true)]
        public long Authorities_ID { get; set; }

        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "Authority_Name")]
        public string Authority_Name { get; set; }

        /// <summary>
        /// 备  注:
        /// 默认值:
        ///</summary>
        [SugarColumn(ColumnName = "IsEffect")]
        public long? IsEffect { get; set; }
    }

}
