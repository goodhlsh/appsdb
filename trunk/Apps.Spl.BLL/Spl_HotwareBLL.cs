using Apps.Common;
using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.BLL
{
   public partial class Spl_HotwareBLL
    {
        [Dependency]
        public ISpl_HotwareRepository m_Rep { get; set; }
        //public override List<Spl_HotwareModel> GetList(ref GridPager pager, string queryStr)
        //{

        //    IQueryable<Spl_Hotware> queryData = null;
        //    if (!string.IsNullOrWhiteSpace(queryStr))
        //    {
        //        queryData = m_Rep.GetList(
        //        a => a.Id.Contains(queryStr)
        //        || a.WareId.Contains(queryStr)
        //        );
        //    }
        //    else
        //    {
        //        queryData = m_Rep.GetList();
        //    }
        //    pager.totalRows = queryData.Count();
        //    //排序
        //    queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
        //    return CreateModelList(ref queryData);
        //}
        public override List<Spl_HotwareModel> CreateModelList(ref IQueryable<Spl_Hotware> queryData)
        {

            List<Spl_HotwareModel> modelList = (from r in queryData
                                                select new Spl_HotwareModel
                                                {
                                                    Id = r.Id,
                                                    WareId = r.WareId,
                                                    WareName=r.Spl_Ware.Name,
                                                    Thumbnail=r.Spl_Ware.Thumbnail,
                                                    Amount = r.Amount,
                                                    SumJinE = r.SumJinE,
                                                    IsShow = r.IsShow,
                                                    ShunXu = r.ShunXu,
                                                }).ToList();

            return modelList;
        }

    }
}
