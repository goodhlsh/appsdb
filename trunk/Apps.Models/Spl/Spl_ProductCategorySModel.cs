using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_ProductCategorySModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "分类名")]
        public override string SonTypeName { get; set; }
        [Display(Name = "大类ID")]
        public override string SupID { get; set; }
        [Display(Name = "是否推荐")]
        public override bool? Promoted { get; set; }
        [Display(Name = "备注")]
        public override string Note { get; set; }

        public override string PicShow { get; set; }
        public override DateTime? CreateTime { get; set; }
    }
}
