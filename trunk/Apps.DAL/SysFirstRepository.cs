using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
   public partial class SysFirstRepository
    {
       public IQueryable<SysFirst> GetFirstByUserID(string userID)
        {
            if (!string.IsNullOrEmpty(userID))
            {
                return from m in Context.SysFirst    
                       
                       where m.UserId == userID
                       orderby m.ShunXu descending
                       select m;
            }
            return null;
        }       
    }
}
