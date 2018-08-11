using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.DAL
{
   public partial class Spl_WareRepository
    {
        public List<P_Spl_GetAllTop_Result> GetAllTopList(bool queryStr, int skip, int limit)
        {
            return Context.P_Spl_GetAllTop().Skip(skip).Take(limit).ToList();
        }
    }
}
