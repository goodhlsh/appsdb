
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
    
public partial class MIS_Article_Comment
{

    public string Id { get; set; }

    public string ArticleId { get; set; }

    public string UserId { get; set; }

    public string TrueName { get; set; }

    public string IP { get; set; }

    public string BodyContent { get; set; }

    public Nullable<System.DateTime> CreateTime { get; set; }

    public Nullable<int> IsReply { get; set; }

    public string ReplyContent { get; set; }

    public Nullable<System.DateTime> ReplyTime { get; set; }



    public virtual MIS_Article MIS_Article { get; set; }

    public virtual SysUser SysUser { get; set; }

}

}
