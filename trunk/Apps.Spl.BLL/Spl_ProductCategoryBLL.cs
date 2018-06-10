using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.BLL
{
    public partial class Spl_ProductCategoryBLL
    {
        [Dependency]
        public ISpl_ProductCategoryRepository m_Rep { get; set; }
        public List<Spl_ProductCategoryModel> GetPage(string queryStr, int skip, int limit)
        {

            List<Spl_ProductCategory> query = null;
            IQueryable<Spl_ProductCategory> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id==queryStr|| a.TypeName==queryStr);
            }
            query = list.OrderBy(c => c.CreateBy).Skip(skip).Take(limit).ToList();
            List<Spl_ProductCategoryModel> productCategoryInfoList = new List<Spl_ProductCategoryModel>();
            List<Spl_ProductCategory> dataList = query.ToList();
            foreach (var productCategory in dataList)
            {
                Spl_ProductCategoryModel splproCate = new Spl_ProductCategoryModel
                {
                    Id=productCategory.Id,
                    TypeName = productCategory.TypeName
                };
                productCategoryInfoList.Add(splproCate);
            }
            return productCategoryInfoList;
        }
        public List<Spl_ProductCategory> GetListValue(string queryStr, int skip, int limit)
        {
            List<Spl_ProductCategory> query = null;
            IQueryable<Spl_ProductCategory> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id == queryStr || a.TypeName == queryStr);
            }
            query = list.OrderBy(c => c.CreateBy).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}

