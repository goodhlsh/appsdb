using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_ProductCategoryModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "商品类名")]
        public override string TypeName { get; set; }
    }
}
