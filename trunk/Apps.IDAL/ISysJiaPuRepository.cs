using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public partial interface ISysJiaPuRepository
    {
        void IntoSysJiaPu(string userId, string pid, decimal fJE);
    }
}
