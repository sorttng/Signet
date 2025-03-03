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
    [SugarTable("Location_Table")]
    public class Location_Table
    {
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "ID", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Location_ID")]
        public string LocationId { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Location_Name_EN")]
        public string LocationNameEn { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Location_Name_ZH")]
        public string LocationNameZh { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "ISO_3166_1")]
        public string Iso31661 { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Country_Region_EN")]
        public string CountryRegionEn { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Country_Region_ZH")]
        public string CountryRegionZh { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Adm1_Name_EN")]
        public string Adm1NameEn { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Adm1_Name_ZH")]
        public string Adm1NameZh { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Adm2_Name_EN")]
        public string Adm2NameEn { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Adm2_Name_ZH")]
        public string Adm2NameZh { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Timezone")]
        public string Timezone { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Latitude")]
        public string Latitude { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Longitude")]
        public string Longitude { get; set; }
        /// <summary>
        ///  
        /// 默认值: 
        ///</summary>
        [SugarColumn(ColumnName = "Adcode")]
        public string Adcode { get; set; }
    }
}
