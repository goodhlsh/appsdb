
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
    
public partial class WC_Group
{

    public WC_Group()
    {

        this.WC_User = new HashSet<WC_User>();

    }


    public string Id { get; set; }

    public string Name { get; set; }

    public int Count { get; set; }

    public string OfficalAccountId { get; set; }



    public virtual WC_OfficalAccounts WC_OfficalAccounts { get; set; }

    public virtual ICollection<WC_User> WC_User { get; set; }

}

}
