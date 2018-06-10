using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IDAL
{
   public partial interface ISpl_BrandRepository
    {
        List<BrandTypeModel> GetListByProduct();
    }
}
