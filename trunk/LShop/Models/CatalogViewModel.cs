using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LShop.Models
{
    /// <summary>
    /// 商品分类视图
    /// </summary>
    public class CatalogViewModel
    {
        public List<Spl_Brand> BrandList;
        public List<Spl_ProductCategory> ProductTypeList;
    }
}