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
    
    public partial class SysAreas
    {
        public SysAreas()
        {
            this.SysAreas1 = new HashSet<SysAreas>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public int Sort { get; set; }
        public bool Enable { get; set; }
        public System.DateTime CreateTime { get; set; }
        public bool IsMunicipality { get; set; }
        public bool IsHKMT { get; set; }
        public bool IsOther { get; set; }
    
        public virtual ICollection<SysAreas> SysAreas1 { get; set; }
        public virtual SysAreas SysAreas2 { get; set; }
    }
}
