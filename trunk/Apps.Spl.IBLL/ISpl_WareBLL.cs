using Apps.Models;
using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
   public partial interface ISpl_WareBLL
    {
       List<Spl_Ware> GetPage(string queryStr, int skip, int limit);
    }
}
