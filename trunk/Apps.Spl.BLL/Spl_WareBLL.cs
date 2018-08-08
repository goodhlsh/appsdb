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

        public override Spl_WareModel GetById(string id)
        {
            Spl_WareModel model = new Spl_WareModel();
            Spl_Ware entity = new Spl_Ware();
            entity = m_Rep.GetById(id);
            if (entity!=null)
            {
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.Note = entity.Note;
                model.BrandId = entity.BrandId;
                model.CreateTime = entity.CreateTime;
                model.Creator = entity.Creator;
                model.Description = entity.Description;                
                model.Editor = entity.Editor;
                model.Price = entity.Price;
                model.ProductCategoryId = entity.ProductCategoryId;
                model.ProductCategoryName =entity.Spl_ProductCategoryS==null?"":entity.Spl_ProductCategoryS.SonTypeName;
                model.PromotionPrice = entity.PromotionPrice;
                model.ShowType = entity.ShowType;
                model.Stock = entity.Stock;
                model.Thumbnail = entity.Thumbnail;
                model.Unit = entity.Unit;
                model.UpdateTime = entity.UpdateTime;
                model.WareInfoId= entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Id;
                model.Picture0 = entity.Spl_WareInfo.FirstOrDefault()==null?"": entity.Spl_WareInfo.FirstOrDefault().Picture0;
                model.Picture1 = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Picture1;
                model.Picture2 = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Picture2;
                model.Picture3 = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Picture3;
                model.Picture4 = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Picture4;
                model.Picture5 = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Picture5;
                model.ToTop = entity.Spl_WareInfo.FirstOrDefault() == null ? false : (bool)entity.Spl_WareInfo.FirstOrDefault().ToTop;
                model.Detail = entity.Spl_WareInfo.FirstOrDefault() == null ? "" : entity.Spl_WareInfo.FirstOrDefault().Detail; 
            }
            return model;
        }
        public override List<Spl_WareModel> CreateModelList(ref IQueryable<Spl_Ware> queryData)
        {

            List<Spl_WareModel> modelList = (from r in queryData
                                             select new Spl_WareModel
                                             {

                                                 Id = r.Id,

                                                 Name = r.Name,

                                                 ProductCategoryId = r.ProductCategoryId,
                                                 ProductCategoryName =r.Spl_ProductCategoryS==null?"": r.Spl_ProductCategoryS.SonTypeName,
                                                 Unit = r.Unit,

                                                 Price = r.Price,

                                                 Stock = r.Stock,

                                                 Note = r.Note,

                                                 Thumbnail = r.Thumbnail,

                                                 ShowType = r.ShowType,

                                                 WareCount = r.WareCount,

                                                 WareState = r.WareState,

                                                 CreateTime = r.CreateTime,

                                                 Creator = r.Creator,

                                                 UpdateTime = r.UpdateTime,

                                                 Editor = r.Editor,

                                                 Description = r.Description,

                                                 BrandId = r.BrandId,

                                                 PromotionPrice = r.PromotionPrice,

                                             }).ToList();

            return modelList;
        }
        public List<Spl_WareModel> GetPage(string queryStr, int skip, int limit)
        {
            try
            {
                List<Spl_WareModel> wareShowModels = new List<Spl_WareModel>();
                List<Spl_Ware> query = null;
                IQueryable<Spl_Ware> list = m_Rep.GetList();
                if (!string.IsNullOrWhiteSpace(queryStr))
                {
                    list = list.Where(a => a.ProductCategoryId == queryStr||a.Name.Contains(queryStr));
                }
                query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
                foreach (var item in query)
                {
                    wareShowModels.Add(new Spl_WareModel()
                    {
                        Id = item.Id,
                        ToTop = item.Spl_WareInfo.Count > 0 ? (bool)item.Spl_WareInfo.FirstOrDefault().ToTop : false,
                        Name = item.Name,
                        Description = item.Description,
                        PromotionPrice = item.PromotionPrice,
                        Price = item.Price,
                        Picture0 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture0 : null,
                        Picture1 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture1 : null,
                        Picture2 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture2 : null,
                        Picture3 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture3 : null,
                        Picture4 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture4 : null,
                        Picture5 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture5 : null,
                        Thumbnail = item.Thumbnail,
                        ShowType = item.ShowType,
                        Stock = item.Stock,
                        Detail = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Detail : null,
                        ProductCategoryId = item.ProductCategoryId,
                        Note = item.Note,
                        Unit = item.Unit
                    });
                }
                return wareShowModels;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<Spl_WareModel> GetPage(bool queryStr, int skip, int limit)
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
        public List<Spl_WareModel> GetPageLike(string queryStr, int skip, int limit)
        {
            try
            {
                List<Spl_WareModel> wareShowModels = new List<Spl_WareModel>();
                List<Spl_Ware> query = null;
                IQueryable<Spl_Ware> list = m_Rep.GetList();
                if (!string.IsNullOrWhiteSpace(queryStr))
                {
                    list = list.Where(a => a.Name.Contains(queryStr));
                }
                query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
                foreach (var item in query)
                {
                    wareShowModels.Add(new Spl_WareModel()
                    {
                        Id = item.Id,
                        ToTop = item.Spl_WareInfo.Count > 0 ? (bool)item.Spl_WareInfo.FirstOrDefault().ToTop:false,
                        Name = item.Name,
                        Description = item.Description,
                        PromotionPrice = item.PromotionPrice,
                        Price = item.Price,
                        Picture0 = item.Spl_WareInfo.Count>0 ? item.Spl_WareInfo.FirstOrDefault().Picture0:null,
                        Picture1 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture1:null,
                        Picture2 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture2 : null,
                        Picture3 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture3 : null,
                        Picture4 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture4 : null,
                        Picture5 = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Picture5:null,
                        Thumbnail = item.Thumbnail,
                        ShowType = item.ShowType,
                        Stock = item.Stock,
                        Detail = item.Spl_WareInfo.Count > 0 ? item.Spl_WareInfo.FirstOrDefault().Detail : null,
                        ProductCategoryId = item.ProductCategoryId,
                        Note = item.Note,
                        Unit = item.Unit
                    });
                }
                return wareShowModels;
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
        public List<Spl_Actives> GetPageActive(bool queryStr, int skip, int limit)
        {
            List<Spl_Actives> query = null;
            IQueryable<Spl_Actives> list = ma_Rep.GetList();
            list = list.Where(a => a.IsShow == queryStr);
            query = list.OrderBy(c => c.UpdateTime).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<Spl_Ware> GetProducts(string bid, string tid,int skip,int limit)
        {
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(bid)& !string.IsNullOrWhiteSpace(tid))
            {
                list = list.Where(a => a.BrandId == bid && a.ProductCategoryId == tid);
            }
            if (!string.IsNullOrWhiteSpace(bid) & string.IsNullOrWhiteSpace(tid))
            {
                list = list.Where(a => a.BrandId == bid );
            }
            if (string.IsNullOrWhiteSpace(bid) & !string.IsNullOrWhiteSpace(tid))
            {
                list = list.Where(a =>  a.ProductCategoryId == tid);
            }

            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}
