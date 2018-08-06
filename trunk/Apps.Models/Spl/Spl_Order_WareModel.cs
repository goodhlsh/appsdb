using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_Order_WareModel
    {
        [Display(Name ="编号")]
        public override string Id { get; set; }
        [Display(Name = "订单编号")]
        public override string OrderID { get; set; }
        [Display(Name = "商品编号")]
        public override string WaresId { get; set; }
        [Display(Name = "商品名称")]
        public override string Name { get; set; }
        [Display(Name = "商品数量")]
        public override int? Amount { get; set; }
        [Display(Name = "商品款合计")]
        public override decimal? SumJinE { get; set; }
        [Display(Name = "商品缩略图")]
        public string Thumbnail { get; set; }
    }
}
