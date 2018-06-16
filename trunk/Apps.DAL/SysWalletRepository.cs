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
                       orderby m.ShunXu descending
                       select m;
            }
            return null;
        }
        public List<P_Sys_GetUserWallet_Result> GetUserWallet()
        {
            return Context.P_Sys_GetUserWallet().ToList();
            
        }
    }
}
