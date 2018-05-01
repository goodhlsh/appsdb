using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
   public partial class SysAddressBLL
    {
        [Dependency]
        public ISysAddressRepository m_Rep { get; set; }
        public List<SysAddress> GetPage(string queryStr, int skip, int limit)
        {
            List<SysAddress> query = null;
            IQueryable<SysAddress> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.UserId == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<SysAddress> GetPage(string queryStr, int skip, int limit,bool IsDefault)
        {
            List<SysAddress> query = null;
            IQueryable<SysAddress> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.UserId == queryStr && a.IsDefault==IsDefault);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}
