//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Spl_Ware
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string ProductCategoryId { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> OriginPrice { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> Stock { get; set; }
        public string Note { get; set; }
        public string Thumbnail { get; set; }
        public string ShowType { get; set; }
        public string WareInfoId { get; set; }
        public Nullable<int> WareCount { get; set; }
        public Nullable<bool> WareState { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Creator { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string Editor { get; set; }
    
        public virtual Spl_ProductCategory Spl_ProductCategory { get; set; }
    }
}
