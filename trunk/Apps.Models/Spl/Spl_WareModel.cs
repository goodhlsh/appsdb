using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
    public partial class Spl_WareModel
    {

    }
    public class Spl_WareShopCartModel
    {
        public string id { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Thumbnail { get; set; }
        public Nullable<int> WareCount { get; set; }
        public Nullable<bool> WareState { get; set; }
        public string Unit { get; set; }
    }
    public class Spl_WareShowModel
    {
        public  string id { get; set; }
        public  string Name { get; set; }
        public  string ProductCategoryId { get; set; }
        public  string Unit { get; set; }
        public  Nullable<decimal> OriginPrice { get; set; }
        public  Nullable<decimal> Price { get; set; }
        public  Nullable<int> Stock { get; set; }
        public  string Note { get; set; }
        public  string Thumbnail { get; set; }
        public  string ShowType { get; set; }
        public  string WareInfoId { get; set; }
        public  string Description { get; set; }
        public  string Picture0 { get; set; }
        public  string Picture1 { get; set; }
        public  string Picture2 { get; set; }
        public  string Picture3 { get; set; }
        public  string Picture4 { get; set; }
        public  string Picture5 { get; set; }
        public  string Detail { get; set; }
        public  bool ToTop { get; set; }
    }
}
