
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
    
public partial class SysErCi
{

    public string Id { get; set; }

    public string UserId { get; set; }

    public string OrderId { get; set; }

    public Nullable<decimal> JinE { get; set; }

    public Nullable<System.DateTime> CreateTime { get; set; }

    public string Note { get; set; }



    public virtual SysUser SysUser { get; set; }

}

}
