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
    public partial class Spl_WareBLL
    {
        [Dependency]
        public ISpl_WareRepository m_Rep { get; set; }
        [Dependency]
        public ISpl_WareInfoRepository minfo_Rep { get; set; }
        [Dependency]
        public ISpl_ActivesRepository ma_Rep { get; set; }

        public List<Spl_WareShowModel> GetPage(string queryStr, int skip, int limit)
        {
            List<Spl_WareShowModel> wareShowModels = new List<Spl_WareShowModel>();
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.ProductCategoryId == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            foreach (var item in query)
            {
               wareShowModels.Add(new Spl_WareShowModel() {
                    id = item.id,
                    ToTop = (bool)item.Spl_WareInfo.ToTop,
                    Name = item.Name,
                    Description = item.Description,
                    OriginPrice = item.OriginPrice,
                    Price = item.Price,
                    Picture0 = item.Spl_WareInfo.Picture0,
                    Picture1 = item.Spl_WareInfo.Picture1,
                    Picture2 = item.Spl_WareInfo.Picture2,
                    Picture3 = item.Spl_WareInfo.Picture3,
                    Picture4 = item.Spl_WareInfo.Picture4,
                    Picture5 = item.Spl_WareInfo.Picture5,
                    Thumbnail = item.Thumbnail,
                    ShowType = item.ShowType,
                    Stock = item.Stock,
                    Detail = item.Spl_WareInfo.Detail,
                    ProductCategoryId = item.ProductCategoryId,
                    Note = item.Note,
                    Unit = item.Unit,
                    WareInfoId = item.WareInfoId
                });
            }
            return wareShowModels;
        }
        public List<Spl_WareShowModel> GetPage(bool queryStr, int skip, int limit)
        {
            return null;
        }
        public List<Spl_WareInfo> GetPageWareInfo(bool queryStr, int skip, int limit)
        {
            List<Spl_WareInfo> query = null;
            IQueryable<Spl_WareInfo> list = minfo_Rep.GetList();
            list = list.Where(a => a.ToTop == queryStr);
            query = list.OrderBy(c => c.UpdateTime).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<Spl_WareShowModel> GetPageLike(string queryStr, int skip, int limit)
        {
            List<Spl_WareShowModel> wareShowModels = new List<Spl_WareShowModel>();
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Name.Contains(queryStr));
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            foreach (var item in query)
            {
                wareShowModels.Add(new Spl_WareShowModel()
                {
                    id = item.id,
                    ToTop = (bool)item.Spl_WareInfo.ToTop,
                    Name = item.Name,
                    Description = item.Description,
                    OriginPrice = item.OriginPrice,
                    Price = item.Price,
                    Picture0 = item.Spl_WareInfo.Picture0,
                    Picture1 = item.Spl_WareInfo.Picture1,
                    Picture2 = item.Spl_WareInfo.Picture2,
                    Picture3 = item.Spl_WareInfo.Picture3,
                    Picture4 = item.Spl_WareInfo.Picture4,
                    Picture5 = item.Spl_WareInfo.Picture5,
                    Thumbnail = item.Thumbnail,
                    ShowType = item.ShowType,
                    Stock = item.Stock,
                    Detail = item.Spl_WareInfo.Detail,
                    ProductCategoryId = item.ProductCategoryId,
                    Note = item.Note,
                    Unit = item.Unit,
                    WareInfoId = item.WareInfoId
                });
            }
            return wareShowModels;
        }
        public List<Spl_Actives> GetPageActive(bool queryStr, int skip, int limit)
        {
            List<Spl_Actives> query = null;
            IQueryable<Spl_Actives> list = ma_Rep.GetList();
            list = list.Where(a => a.IsShow == queryStr);
            query = list.OrderBy(c => c.UpdateTime).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}
