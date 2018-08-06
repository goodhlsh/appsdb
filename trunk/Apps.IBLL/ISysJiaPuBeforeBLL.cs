using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IBLL
{
   public partial interface ISysJiaPuBeforeBLL
    {
        List<SysJiaPuBefore> GetSysJiaPuBefore(string tid);
        List<SysJiaPuBefore> IsInSysJiaPuBefore(string uid);
    }
}
