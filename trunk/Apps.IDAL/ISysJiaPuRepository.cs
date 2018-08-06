using Apps.Models;
using System.Linq;

namespace Apps.IDAL
{
    public partial interface ISysJiaPuRepository
    {
        SysJiaPu GetRefSysJiaPu(string userId);
        
        int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE);
        IQueryable<P_GetRecursiveChildren_Result> P_GetRecursiveChildren(string uid);
        IQueryable<P_GetAllSons_Result> GetAllSons(string queryStr);
    }
}
