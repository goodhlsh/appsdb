using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
   public partial interface ISysPurchaseHistoryBLL
    {
        List<SysPurchaseHistoryModel> GetList(string queryStr);
    }
}
