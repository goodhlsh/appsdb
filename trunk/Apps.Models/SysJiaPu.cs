
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
    
public partial class SysJiaPu
{

    public string Id { get; set; }

    public string UserId { get; set; }

    public string ParentId { get; set; }

    public string PPId { get; set; }

    public string ZiMu { get; set; }

    public string ShuZi { get; set; }

    public Nullable<int> ErZiShu { get; set; }

    public decimal FirstJinE { get; set; }

    public string LevelId { get; set; }

    public string ZMP15 { get; set; }

    public string ZMPA2 { get; set; }

    public string ZMPB2 { get; set; }

    public string ZMPC2 { get; set; }

    public string ZMPD2 { get; set; }

    public string ZMPE2 { get; set; }

    public string ZMPF2 { get; set; }

    public string ZMPG2 { get; set; }

    public string ZMPH2 { get; set; }

    public string ZMPI2 { get; set; }

    public string ZMPJ2 { get; set; }

    public string Comment { get; set; }

    public Nullable<System.DateTime> CreateTime { get; set; }

    public Nullable<System.DateTime> UpdateTime { get; set; }

    public string TopId { get; set; }

    public string TId { get; set; }



    public virtual SysLevels SysLevels { get; set; }

    public virtual SysUser SysUser { get; set; }

}

}
