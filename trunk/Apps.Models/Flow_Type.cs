
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
    
public partial class Flow_Type
{

    public Flow_Type()
    {

        this.Flow_Form = new HashSet<Flow_Form>();

        this.Flow_FormAttr = new HashSet<Flow_FormAttr>();

    }


    public string Id { get; set; }

    public string Name { get; set; }

    public string Remark { get; set; }

    public System.DateTime CreateTime { get; set; }

    public int Sort { get; set; }



    public virtual ICollection<Flow_Form> Flow_Form { get; set; }

    public virtual ICollection<Flow_FormAttr> Flow_FormAttr { get; set; }

}

}
