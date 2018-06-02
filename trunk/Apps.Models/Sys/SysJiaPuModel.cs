using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public partial class SysJiaPuModel
    {
        public override string id { get; set; }

        public override string UserId { get; set; }
        [Display(Name ="用户账号")]
        public string UserName { get; set; }
        [Display(Name = "用户姓名")]
        public string TrueName { get; set; }
        public override string ParentId { get; set; }
        [Display(Name ="上级领导")]
        public string LeadName { get; set; }
        [Display(Name ="入会金额")]
        public override decimal? FirstJinE { get; set; }
        [Display(Name ="指定位置")]
        public override string ZMPA2 { get; set; }
        public override string TId { get; set; }
        [Display(Name = "推荐人")]
        public string TName { get; set; }
        [Display(Name ="入会时间")]
        public override DateTime? CreateTime { get; set; }

    }
}
