using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public partial interface ISysJiaPuRepository
    {
        int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal? fJE);
        void IntoSysJiaPuBefore(string uid, string tid, decimal? fJE);
    }
}
