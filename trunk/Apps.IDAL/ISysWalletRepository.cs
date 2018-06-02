using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.IDAL
{
    public partial interface ISysWalletRepository
    {
        IQueryable<SysWallet> GetWallByUserID(string userID);
        List<P_Sys_GetUserWallet_Result> GetUserWallet();
    }
}
