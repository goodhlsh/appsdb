using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public partial class SysJiaPuRepository
    {

        public int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE)
        {
            if (fJE <=0)
            {
                fJE = 0;
            }
            Context.P_Sys_PutZi(userId, tid, pid, erbiao, fJE);

            return this.SaveChanges();
        }
        public int IntoSysJiaPuBefore(string userId, string tid, decimal? fJE)
        {
            if (fJE ==null)
            {
                fJE = 0;
            }
            Context.P_Sys_PutZiBefore(userId, tid, fJE);

            return this.SaveChanges();
        }
        public IQueryable<P_GetRecursiveChildren_Result> P_GetRecursiveChildren(string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
               return Context.P_GetRecursiveChildren(uid).AsQueryable(); ;
            }
            else
            {
                return null;
            }
        }
    }
}
