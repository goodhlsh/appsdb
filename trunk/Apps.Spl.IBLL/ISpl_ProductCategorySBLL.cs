﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Common;
using Apps.Models;
using Apps.Models.Spl;

namespace Apps.Spl.IBLL
{
  public partial interface ISpl_ProductCategorySBLL
    {
        List<Spl_ProductCategorySModel> GetPage(string queryStr,int skip,int limit);
        List<Spl_ProductCategoryS> GetListValue(string queryStr, int skip, int limit);
       // new List<Spl_ProductCategorySModel>  GetList(ref GridPager pager, string queryStr);
    }
}
