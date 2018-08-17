using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
   public partial class SysFirstModel
    {
        public override string Id { get; set; }
        [Display(Name ="用户Id")]
        public override string UserId { get; set; }
        [Display(Name = "用户名")]
        public string TrueName { get; set; }
        [Display(Name = "订单Id")]
        public override string OrderId { get; set; }
        [Display(Name = "当次金额")]
        public override Nullable<decimal> JinE { get; set; }
        [Display(Name = "总金额")]
        public override Nullable<decimal> CurrentFirst { get; set; }
        [Display(Name = "本次交易时间")]
        public override Nullable<System.DateTime> CreateTime { get; set; }
        [Display(Name = "说明")]
        public override string Note { get; set; }
        [Display(Name = "显示顺序")]
        public override long ShunXu { get; set; }
    }
}
