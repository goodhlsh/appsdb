using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.DAL
{
    public partial class Spl_BrandRepository
    {
        public List<BrandTypeModel> GetListByProduct()
        {
            var list = from p in Context.Spl_Ware
                       from ps in Context.Spl_ProductCategoryS
                       from pf in Context.Spl_ProductCategory
                       from b in Context.Spl_Brand
                       where p.ProductCategoryId == ps.Id && ps.SupID == pf.Id && p.BrandId == b.Id
                       select new
                       {
                           b.Id,
                           b.Name,
                           ps.SupID,
                           pf.TypeName
                       };
            return list.Select(r => new BrandTypeModel
            {
                BrandID = r.Id,
                BrandName = r.Name,
                TypeID = r.SupID,
                TypeName = r.TypeName
            }).ToList();
        }
    }
}
