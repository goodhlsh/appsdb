using Apps.Models;
using Apps.Models.Sys;
using System.Collections.Generic;

namespace Apps.IBLL
{
    public partial interface ISysAddressBLL
    {
        List<SysAddressModel> GetPage(string queryStr, int skip, int limit);
        List<SysAddressModel> GetPage(string queryStr, string id, int skip, int limit, bool IsDefault=true);
    }
}
