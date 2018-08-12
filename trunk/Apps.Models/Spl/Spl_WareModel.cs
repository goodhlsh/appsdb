using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_WareModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "商品名称")]
        public override string Name { get; set; }
        [Display(Name = "品牌ID")]
        public override string BrandId { get; set; }
        [Display(Name = "商品类型ID")]
        public override string ProductCategoryId { get; set; }
        [Display(Name="商品类型名称")]
        public string ProductCategoryName { get; set; }
        [Display(Name ="单位")]
        public override string Unit { get; set; }
        [Display(Name = "原价")]
        public override Nullable<decimal> Price { get; set; }
        [Display(Name = "促销价")]
        public override Nullable<decimal> PromotionPrice { get; set; }
        [Display(Name="库存")]
        public override Nullable<int> Stock { get; set; }
        [Display(Name = "备注说明")]
        public override string Note { get; set; }
        [Display(Name = "缩列图")]
        public override string Thumbnail { get; set; }
        [Display(Name = "显示方式")]
        public override string ShowType { get; set; }
        [Display(Name = "描述")]
        public override string Description { get; set; }
        public string WareInfoId { get; set; }
        [Display(Name = "首页展图")]
        public string Picture0 { get; set; }
        [Display(Name = "产品头部展图")]
        public string Picture1 { get; set; }
        [Display(Name = "产品头部展图")]
        public string Picture2 { get; set; }
        [Display(Name = "产品介绍展图")]
        public string Picture3 { get; set; }
        [Display(Name = "产品介绍展图")]
        public string Picture4 { get; set; }
        [Display(Name = "产品介绍展图")]
        public string Picture5 { get; set; }
        [Display(Name = "其他")]
        public string Detail { get; set; }
        [Display(Name = "是否置顶")]
        public bool ToTop { get; set; }
    }
    public class Spl_WareShopCartModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Thumbnail { get; set; }
        public Nullable<int> WareCount { get; set; }
        public Nullable<bool> WareState { get; set; }
        public string Unit { get; set; }
    }
    

    public partial class Spl_HotwareModel
    {
        public override string Id { get; set; }

        public override string WareId { get; set; }
        [Display(Name ="商品名称")]
        public string WareName { get; set; }
        [Display(Name ="图片")]
        public string Thumbnail { get; set; }
        [Display(Name ="促销价")]
        public Nullable<decimal> PromotionPrice { get; set; }
        [Display(Name ="原价")]
        public Nullable<decimal> Price { get; set; }
        [Display(Name ="销售数量")]
        public override Nullable<int> Amount { get; set; }
        [Display(Name ="销售金额")]
        public override Nullable<decimal> SumJinE { get; set; }
        [Display(Name ="是否显示")]
        public override Nullable<bool> IsShow { get; set; }
        [Display(Name ="显示顺序")]
        public override Nullable<int> ShunXu { get; set; }
    }
}
