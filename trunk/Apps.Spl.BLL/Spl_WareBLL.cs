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
    public partial class Spl_WareBLL
    {
        [Dependency]
        public ISpl_WareRepository m_Rep { get; set; }
        public List<Spl_Ware> GetPage(string queryStr, int skip, int limit)
        {
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.ProductCategoryId == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}
