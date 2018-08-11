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
       List<Spl_WareModel> GetPage(string queryStr, int skip, int limit);
        List<Spl_WareModel> GetPage(bool queryStr, int skip, int limit);
        List<Spl_WareInfo> GetPageWareInfo(bool queryStr, int skip, int limit);
        List<Spl_WareModel> GetPageLike(string queryStr, int skip, int limit);
        List<Spl_Actives> GetPageActive(bool queryStr, int skip, int limit);
        List<Spl_Hotware> GetHotWare(bool queryStr, int skip, int limit);
        List<Spl_Ware> GetProducts(string bid, string tid,int skip,int limit);
        List<P_Spl_GetAllTop_Result> GetAllTopList(bool queryStr, int skip, int limit);
    }
}
