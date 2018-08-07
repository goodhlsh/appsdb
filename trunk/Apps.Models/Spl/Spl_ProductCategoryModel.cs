using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    //大类
    public partial class Spl_ProductCategoryModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "商品大类名")]
        public override string TypeName { get; set; }
        [Display(Name ="展示图片")]
        public override string PicShow { get; set; }
        [Display(Name = "说明")]
        public override string Note { get; set; }
        [Display(Name = "创建时间")]
        public override DateTime? CreateTime { get; set; }
    }
}
