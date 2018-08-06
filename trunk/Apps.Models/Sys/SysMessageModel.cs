using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public partial class SysMessageModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "标题")]
        public override string Title { get; set; }
        [Display(Name = "消息内容")]
        public override string Cont { get; set; }
        [Display(Name = "消息类型")]
        public override string Category { get; set; }
        [Display(Name = "来自于")]
        public override string FromWho { get; set; }
        [Display(Name = "发于")]
        public override string ToWho { get; set; }
        [Display(Name = "发生时间")]
        public override System.DateTime? CreateTime { get; set; }
        [Display(Name = "更新时间")]
        public override System.DateTime? UpdateTime { get; set; }
        public override bool? IsRead { get; set; }
    }
}
