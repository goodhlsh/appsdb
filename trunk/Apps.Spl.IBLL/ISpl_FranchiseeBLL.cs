using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
    public partial interface ISpl_FranchiseeBLL
    {
        List<Spl_FranchiseeModel> GetFranchiseeList(int skip, int limit);
        List<Spl_FranchiseeModel> GetFranchiseeListByQueryStr(string queryStr, int skip, int limit);
    }
}
