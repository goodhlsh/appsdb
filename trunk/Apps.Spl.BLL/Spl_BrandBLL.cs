using Apps.Models;
using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.BLL
{
    public partial class Spl_BrandBLL
    {
        public List<Spl_Brand> GetListValue(string queryStr, int skip, int limit)
        {
            List<Spl_Brand> query = null;
            IQueryable<Spl_Brand> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Id == queryStr || a.Name == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<BrandTypeModel> GetListByProduct()
        {
            List<BrandTypeModel> query = m_Rep.GetListByProduct();
            return query;
        }
    }
}
