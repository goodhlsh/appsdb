using Apps.Models;
using Apps.Models.Spl;
using System.Collections.Generic;

namespace LShop.Models
{
    /// <summary>
    /// 菜单视图
    /// </summary>
    public class NavViewModel
    {
        public List<Spl_Brand> BrandList;
        public List<Spl_ProductCategory> ProductTypeList; 
        public List<BrandTypeModel> BrandTypeList;
    }
}