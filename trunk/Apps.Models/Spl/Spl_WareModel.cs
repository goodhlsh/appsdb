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
        public override string Unit { get; set; }
        [Display(Name = "原价")]
        public override Nullable<decimal> Price { get; set; }
        [Display(Name = "促销价")]
        public override Nullable<decimal> PromotionPrice { get; set; }
        public override Nullable<int> Stock { get; set; }
        public override string Note { get; set; }
        public override string Thumbnail { get; set; }
        public override string ShowType { get; set; }
        [Display(Name = "描述")]
        public override string Description { get; set; }
        public string Picture0 { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public string Picture4 { get; set; }
        public string Picture5 { get; set; }
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
    public class Spl_WareShowModel
    {
        [Display(Name = "ID")]
        public  string Id { get; set; }
        [Display(Name = "商品名")]
        public  string Name { get; set; }
        [Display(Name = "品牌ID")]
        public string BrandId { get; set; }
        [Display(Name = "商品类型ID")]
        public  string ProductCategoryId { get; set; }
        public  string Unit { get; set; }
        [Display(Name = "原价")]
        public  Nullable<decimal> Price { get; set; }
        [Display(Name = "促销价")]
        public  Nullable<decimal> PromotionPrice { get; set; }
        public  Nullable<int> Stock { get; set; }
        public  string Note { get; set; }
        public  string Thumbnail { get; set; }
        public  string ShowType { get; set; }
       
        public  string Description { get; set; }
        public  string Picture0 { get; set; }
        public  string Picture1 { get; set; }
        public  string Picture2 { get; set; }
        public  string Picture3 { get; set; }
        public  string Picture4 { get; set; }
        public  string Picture5 { get; set; }
        public  string Detail { get; set; }
        [Display(Name = "是否置顶")]
        public  bool ToTop { get; set; }
    }
}
