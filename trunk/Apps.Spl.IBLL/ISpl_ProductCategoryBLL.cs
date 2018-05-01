using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Models.Spl;

namespace Apps.Spl.IBLL
{
  public partial interface ISpl_ProductCategoryBLL
    {
        List<Spl_ProductCategoryModel> GetPage(string queryStr,int skip,int limit);
    }
}
