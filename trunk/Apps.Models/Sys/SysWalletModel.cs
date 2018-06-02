using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public partial class SysWalletModel
    {
        public string id { get; set; }

        public string UserId { get; set; }
        [Display(Name ="用户账号")]
        public string UserName { get; set; }
        [Display(Name ="姓名")]
        public string TrueName { get; set; }
        [Display(Name = "本次消费")]
        public Nullable<decimal> Balance { get; set; }
        [Display(Name = "余额来源")]
        public string Froms { get; set; }
        [Display(Name = "当前余额")]
        public Nullable<decimal> JieYu { get; set; }

        public string Note { get; set; }
        [Display(Name = "进账日期")]
        public Nullable<System.DateTime> CreateTime { get; set; }

        public Nullable<System.DateTime> UpdateTime { get; set; }

        
    }
}
