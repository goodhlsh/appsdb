
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
    
public partial class Spl_Brand
{

    public Spl_Brand()
    {

        this.Spl_Ware = new HashSet<Spl_Ware>();

    }


    public string Id { get; set; }

    public string BrandName { get; set; }

    public Nullable<bool> Promoted { get; set; }

    public string PicShow { get; set; }



    public virtual ICollection<Spl_Ware> Spl_Ware { get; set; }

}

}
