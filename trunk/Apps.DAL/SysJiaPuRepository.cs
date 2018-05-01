using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public partial class SysJiaPuRepository
    {
       
        public void IntoSysJiaPu(string userId, string pid, decimal fJE)
    {
            Context.P_Sys_PutZi(userId,pid,fJE);
            
            this.SaveChanges();
        }
    }
}
