using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.BLL
{
   public partial class Spl_ProductCategoryBLL
    {
       // public ISpl_ProductCategoryRepository m_Rep { get; set; }
        public List<Spl_ProductCategoryModel> GetPage(string queryStr, int skip, int limit)
        {
            IQueryable<Spl_ProductCategory> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id == queryStr || a.TypeName == queryStr);
            }
            list = list.OrderBy(c => c.TypeName).Skip(skip).Take(limit);
            List<Spl_ProductCategoryModel> productCategoryInfoList = new List<Spl_ProductCategoryModel>();
            List<Spl_ProductCategory> dataList = list.ToList();
            foreach (var productCategory in dataList)
            {
                Spl_ProductCategoryModel splproCate = new Spl_ProductCategoryModel
                {
                    Id = productCategory.Id,
                    TypeName = productCategory.TypeName,
                    CreateBy=productCategory.CreateBy,
                    CreateTime = productCategory.CreateTime,
                    Note = productCategory.Note,
                    PicShow = productCategory.PicShow
                };
                productCategoryInfoList.Add(splproCate);
            }
            return productCategoryInfoList;
        }
    }
}
