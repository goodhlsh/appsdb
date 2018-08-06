using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
   public partial interface ISysJiaPuBeforeRepository
    {
        IQueryable<SysJiaPuBefore> GetSysJiaPuBefore(string tid);
        IQueryable<SysJiaPuBefore> IsInSysJiaPuBefore(string uid);
    }
}
