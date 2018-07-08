using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public partial interface ISysJiaPuRepository
    {
        int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE);
        int IntoSysJiaPuBefore(string uid, string tid, decimal? fJE);
        IQueryable<P_GetRecursiveChildren_Result> P_GetRecursiveChildren(string uid);
    }
}
