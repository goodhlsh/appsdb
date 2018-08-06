using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
    public partial interface ISysMessageBLL
    {
        List<SysMessageModel> GetListWithCategory(string queryStr, string userId, int skip, int limit);
    }
}
