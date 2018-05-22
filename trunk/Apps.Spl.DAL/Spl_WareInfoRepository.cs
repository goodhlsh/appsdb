using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.DAL
{
    public partial class Spl_WareInfoRepository
    {
       public IQueryable<Spl_Ware> GetRefWare(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return from m in Context.Spl_WareInfo
                       from f in m.Spl_Ware
                       where m.id == id
                       select f;
            }
            return null;
        }
    }
}
