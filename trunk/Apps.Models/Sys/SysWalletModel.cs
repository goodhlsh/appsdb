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
        [Display(Name = "ID")]
        public override string Id { get; set; }

        public override string UserId { get; set; }
        [Display(Name ="用户账号")]
        public string UserName { get; set; }
        [Display(Name ="姓名")]
        public string TrueName { get; set; }
        [Display(Name = "本次消费")]
        public override Nullable<decimal> Balance { get; set; }
        [Display(Name = "余额来源")]
        public override string Froms { get; set; }
        [Display(Name = "当前余额")]
        public override Nullable<decimal> JieYu { get; set; }

        public override string Note { get; set; }
        [Display(Name = "进账日期")]
        public override Nullable<System.DateTime> CreateTime { get; set; }

        public override Nullable<System.DateTime> UpdateTime { get; set; }

        public override int ShunXu { get; set; }

        
    }
}
