using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
   public partial interface ISpl_Order_WareBLL
    {
        List<Spl_Order_WareModel> GetSpl_Order_WareModelsByOrderId(string queryStr, int skip, int limit);
    }
}
