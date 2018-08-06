using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
   public partial class SysJiaPuBeforeRepository
    {
        public IQueryable<SysJiaPuBefore> GetSysJiaPuBefore(string tid)
        {
            if (!string.IsNullOrEmpty(tid))
            {
                return from m in Context.SysJiaPuBefore
                       where m.tid == tid && m.isdone == false
                       select m;
            }
            return null;
        }
        public IQueryable<SysJiaPuBefore> IsInSysJiaPuBefore(string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                return from m in Context.SysJiaPuBefore
                       where m.uid == uid 
                       select m;
            }
            return null;
        }
    }
}
