using Apps.Models;
using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
    public partial interface ISpl_BrandBLL
    {
        List<Spl_Brand> GetListValue(string queryStr, int skip, int limit);
        List<BrandTypeModel> GetListByProduct();
    }
}
