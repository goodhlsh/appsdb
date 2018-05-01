using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
   public partial class SysWalletRepository
    {
       public IQueryable<SysWallet> GetWallByUserID(string userID)
        {
            if (!string.IsNullOrEmpty(userID))
            {
                return from m in Context.SysWallet    
                       
                       where m.UserId == userID
                       orderby m.UpdateTime descending
                       select m;
            }
            return null;
        }
    }
}
