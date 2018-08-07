using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
    public partial interface ISpl_ProductCategoryBLL
    {
        List<Spl_ProductCategoryModel> GetPage(string v1, int v2, int v3);
    }
}
