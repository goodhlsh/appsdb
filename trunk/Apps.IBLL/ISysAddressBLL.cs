using Apps.Models;
using System.Collections.Generic;

namespace Apps.IBLL
{
    public partial interface ISysAddressBLL
    {
        List<SysAddress> GetPage(string queryStr, int skip, int limit);
    }
}
