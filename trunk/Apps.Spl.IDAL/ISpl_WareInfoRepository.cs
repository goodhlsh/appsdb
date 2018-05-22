using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IDAL
{
    public partial interface ISpl_WareInfoRepository
    {
        IQueryable<Spl_Ware> GetRefWare(string id);
    }
}
