using Apps.Models;
using Apps.Models.Spl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.IBLL
{
   public partial interface ISpl_WareBLL
    {
       List<Spl_WareShowModel> GetPage(string queryStr, int skip, int limit);
        List<Spl_WareShowModel> GetPage(bool queryStr, int skip, int limit);
        List<Spl_WareInfo> GetPageWareInfo(bool queryStr, int skip, int limit);
        List<Spl_WareShowModel> GetPageLike(string queryStr, int skip, int limit);
        List<Spl_Actives> GetPageActive(bool queryStr, int skip, int limit);
        List<Spl_Ware> GetProducts(string bid, string tid,int skip,int limit);
  
    }
}
