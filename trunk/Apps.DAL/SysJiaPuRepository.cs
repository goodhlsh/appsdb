using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public partial class SysJiaPuRepository
    {

        public void IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal? fJE)
        {
            if (fJE == null)
            {
                fJE = 0;
            }
            Context.P_Sys_PutZi(userId, tid, pid, erbiao, fJE);

            this.SaveChanges();
        }
        public void IntoSysJiaPuBefore(string userId, string tid, decimal? fJE)
        {
            if (fJE == null)
            {
                fJE = 0;
            }
            Context.P_Sys_PutZiBefore(userId, tid, fJE);

            this.SaveChanges();
        }
    }
}
