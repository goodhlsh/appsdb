
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
    
public partial class SysDcitTpe
{

    public SysDcitTpe()
    {

        this.SysDictData = new HashSet<SysDictData>();

    }


    public string Id { get; set; }

    public string Name { get; set; }

    public string Remarek { get; set; }

    public string SortCode { get; set; }

    public string Creator { get; set; }

    public Nullable<System.DateTime> CreateTime { get; set; }

    public byte[] Editor { get; set; }

    public Nullable<System.DateTime> EditTime { get; set; }

    public string Pid { get; set; }



    public virtual ICollection<SysDictData> SysDictData { get; set; }

}

}
