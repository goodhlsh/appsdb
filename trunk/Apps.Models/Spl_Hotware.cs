
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
    
public partial class Spl_Hotware
{

    public string Id { get; set; }

    public string WareId { get; set; }

    public Nullable<int> Amount { get; set; }

    public Nullable<decimal> SumJinE { get; set; }

    public Nullable<bool> IsShow { get; set; }

    public Nullable<int> ShunXu { get; set; }



    public virtual Spl_Ware Spl_Ware { get; set; }

}

}
