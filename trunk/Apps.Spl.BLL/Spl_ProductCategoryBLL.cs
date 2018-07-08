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
         
        public ISpl_ProductCategorySRepository m_Rep { get; set; }
        public List<Spl_ProductCategorySModel> GetPage(string queryStr, int skip, int limit)
        {

            List<Spl_ProductCategoryS> query = null;
            IQueryable<Spl_ProductCategoryS> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id==queryStr|| a.Name==queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            List<Spl_ProductCategorySModel> productCategoryInfoList = new List<Spl_ProductCategorySModel>();
            List<Spl_ProductCategoryS> dataList = query.ToList();
            foreach (var productCategory in dataList)
            {
                Spl_ProductCategorySModel splproCate = new Spl_ProductCategorySModel
                {
                    Id=productCategory.Id,
                    Name = productCategory.Name,
                    SupID=productCategory.Spl_ProductCategory.Id,
                    Promoted=productCategory.Promoted,
                    Note=productCategory.Note
                };
                productCategoryInfoList.Add(splproCate);
            }
            return productCategoryInfoList;
        }
        public List<Spl_ProductCategoryS> GetListValue(string queryStr, int skip, int limit)
        {
            List<Spl_ProductCategoryS> query = null;
            IQueryable<Spl_ProductCategoryS> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id == queryStr || a.Name == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}

