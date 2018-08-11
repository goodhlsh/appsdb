using Apps.Common;
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
    public partial class Spl_ProductCategorySBLL
    {
        [Dependency]
        public ISpl_ProductCategorySRepository m_Rep { get; set; }
        public List<Spl_ProductCategorySModel> GetPage(string queryStr, int skip, int limit)
        {
            IQueryable<Spl_ProductCategoryS> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id==queryStr|| a.SonTypeName == queryStr);
            }
            list = list.OrderBy(c => c.SonTypeName).Skip(skip).Take(limit);
            List<Spl_ProductCategorySModel> productCategoryInfoList = new List<Spl_ProductCategorySModel>();
            List<Spl_ProductCategoryS> dataList = list.ToList();
            foreach (var productCategory in dataList)
            {
                Spl_ProductCategorySModel splproCate = new Spl_ProductCategorySModel
                {
                    Id=productCategory.Id,
                    SonTypeName = productCategory.SonTypeName,
                    SupID=productCategory.Spl_ProductCategory.Id,
                    SupName=productCategory.Spl_ProductCategory.TypeName,
                    Promoted=productCategory.Promoted,
                    Note=productCategory.Note,
                    PicShow=productCategory.PicShow,
                    ShunXu=productCategory.ShunXu
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
                list = list.Where(a => a.Id == queryStr || a.SonTypeName == queryStr);
            }
            query = list.OrderBy(c => c.SonTypeName).Skip(skip).Take(limit).ToList();
            return query;
        }        
    }
}

