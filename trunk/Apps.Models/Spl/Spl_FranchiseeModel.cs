using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
   public partial class Spl_FranchiseeModel
    {

        public override string Id { get; set; }
        [Display(Name ="店名")]
        public override string FranchiseeName { get; set; }
        [Display(Name = "类型")]
        public override string FranchiseeType { get; set; }
        [Display(Name = "联系方式")]
        public override string Tel { get; set; }
        [Display(Name = "地址")]
        public override string Addr { get; set; }
        [Display(Name = "地区")]
        public override string Area { get; set; }
        [Display(Name = "加盟时间")]
        public override Nullable<System.DateTime> CreateTime { get; set; }
        [Display(Name = "备注")]
        public override string Note { get; set; }
    }
}
