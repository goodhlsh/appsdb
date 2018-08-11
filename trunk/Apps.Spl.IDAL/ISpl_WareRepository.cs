using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IDAL
{
  public partial  interface ISpl_WareRepository
    {        
        List<P_Spl_GetAllTop_Result> GetAllTopList(bool queryStr, int skip, int limit);
    }
}
