﻿
using Apps.Models.Spl;
using System.Collections.Generic;

namespace Apps.Spl.IBLL
{
    public partial interface ISpl_OrdersBLL
    {
        List<Spl_OrdersModel> GetListWithStatus(string queryStr, int skip, int limit);
            
    }
}