using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IBLL;
using Apps.WebApi.Core;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;

namespace Apps.WebApi.Areas.Ware.Controllers
{
    public class Spl_WareController : BaseApiController
    {

        [Dependency]
        public ISpl_ProductCategorySBLL mty_BLL { get; set; }

        [Dependency]
        public ISpl_WareBLL m_BLL { get; set; }
        [Dependency]
        public ISpl_WareInfoBLL mi_BLL { get; set; }

        Common.ValidationErrors errors = new Common.ValidationErrors();

        //[SupportFilter]
        [HttpGet]
        public object GetListByCategoryID(string filter)
        {
            // var anonymous = new { queryStr = String.Empty, skip = new int(), limit = new int() };
            // var opc = JsonHandler.DeserializeAnonymousType(filter, anonymous);
            // List<Spl_Ware> list = m_BLL.GetPage(opc.queryStr, opc.skip, opc.limit);

            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["ID"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["ID"].ToString();
            }

            List<Spl_WareModel> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }

        [HttpGet]
        public object GetListByWareName(string filter)
        {
            // var anonymous = new { queryStr = String.Empty, skip = new int(), limit = new int() };
            // var opc = JsonHandler.DeserializeAnonymousType(filter, anonymous);
            // List<Spl_Ware> list = m_BLL.GetPage(opc.queryStr, opc.skip, opc.limit);

            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["warename"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["warename"].ToString();
            }

            List<Spl_WareModel> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetById(string filter)
        {
            JObject opc = JObject.Parse(filter);
            if (JObject.Parse(opc["where"].ToString())["Id"] == null)
            {
                return Json("");
            }

            Spl_WareModel model = new Spl_WareModel();
            model = m_BLL.GetById(JObject.Parse(opc["where"].ToString())["Id"].ToString());
            if (model != null)
            {                
                return Json(model);
            }
            else
            {
                return Json("");
            }

        }
        [HttpGet]
        public object GetAllWareInfo(string filter)
        {
            JObject opc = JObject.Parse(filter);
            bool queryStr = false;
            if (JObject.Parse(opc["where"].ToString())["toTop"] != null)
            {
                queryStr = bool.Parse(JObject.Parse(opc["where"].ToString())["toTop"].ToString());
            }
            List<Spl_WareInfo> list = m_BLL.GetPageWareInfo(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            List<Spl_WareModel> wareShowModels = new List<Spl_WareModel>();
            foreach (var item in list)
            {
                Spl_WareModel ware = m_BLL.GetById(item.WareId);
                if (ware != null && item.Picture0 != null)
                {
                    wareShowModels.Add(new Spl_WareModel()
                    {
                        Id = ware.Id,
                        ToTop = (bool)item.ToTop,
                        Name = ware.Name,
                        Description = ware.Description,
                        PromotionPrice = ware.PromotionPrice,
                        Price = ware.Price,
                        Picture0 = item.Picture0 == null ? "" : item.Picture0,
                        Picture1 = item.Picture1 == null ? "" : item.Picture1,
                        Picture2 = item.Picture2 == null ? "" : item.Picture2,
                        Picture3 = item.Picture3 == null ? "" : item.Picture3,
                        Picture4 = item.Picture4 == null ? "" : item.Picture4,
                        Picture5 = item.Picture5 == null ? "" : item.Picture5,
                        Thumbnail = ware.Thumbnail,
                        ShowType = ware.ShowType,
                        Stock = ware.Stock,
                        Detail = item.Detail,
                        ProductCategoryId = ware.ProductCategoryId,
                        Note = ware.Note,
                        Unit = ware.Unit,
                        ShunXu=ware.ShunXu                        
                    });
                }
            }
            return Json(wareShowModels);
        }
        [HttpGet]
        public object GetAllTop(string filter)
        {
            JObject opc = JObject.Parse(filter);
            bool queryStr = false;
            if (JObject.Parse(opc["where"].ToString())["toTop"] != null)
            {
                queryStr = bool.Parse(JObject.Parse(opc["where"].ToString())["toTop"].ToString());
            }
            List<P_Spl_GetAllTop_Result> getAllTop_Results = new List<P_Spl_GetAllTop_Result>();
            getAllTop_Results=m_BLL.GetAllTopList(queryStr,int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (getAllTop_Results!=null)
            {
                return Json(getAllTop_Results);
            }
            else
            {
                return null;
            }
        }


        [HttpGet]
        public object GetAllLike(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["likes"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["likes"].ToString();
            }

            List<Spl_WareModel> list = m_BLL.GetPageLike(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetAllActive(string filter)
        {
            JObject opc = JObject.Parse(filter);
            bool queryStr = false;
            if (JObject.Parse(opc["where"].ToString())["isShow"] != null)
            {

                queryStr = bool.Parse(JObject.Parse(opc["where"].ToString())["isShow"].ToString());
            }
            List<Spl_Actives> list = m_BLL.GetPageActive(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetHotWare(string filter)
        {
            JObject opc = JObject.Parse(filter);
            bool queryStr = false;
            if (JObject.Parse(opc["where"].ToString())["isShow"] != null)
            {
                queryStr = bool.Parse(JObject.Parse(opc["where"].ToString())["isShow"].ToString());
            }
            List<Spl_Hotware> list = m_BLL.GetHotWare(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (list==null)
            {
                return null;
            }
            List<Spl_HotwareModel> Hotwares = new List<Spl_HotwareModel>();
            foreach (Spl_Hotware item in list)
            {
                Hotwares.Add(new Spl_HotwareModel()
                {
                    Id=item.Id,
                    WareId=item.WareId,
                    WareName=item.Spl_Ware.Name,
                    Price=item.Spl_Ware.Price==null?0:item.Spl_Ware.Price,
                    PromotionPrice=item.Spl_Ware.PromotionPrice==null?0:item.Spl_Ware.PromotionPrice,                    
                    Thumbnail=item.Spl_Ware.Thumbnail,
                    Amount=item.Amount,
                    SumJinE=item.SumJinE==null?0:item.SumJinE,
                    IsShow=item.IsShow,
                    ShunXu=item.ShunXu,
                    CreateTime=item.CreateTime
                });
            }
            return Json(Hotwares);
        }
        [HttpGet]
        public object GetAllByShopCart(string filter)
        {
            JObject opc = JObject.Parse(filter);
            string[] opcid = (JObject.Parse(opc["where"].ToString())["id"].ToString()).Replace("[", "").Replace("\"", "").Replace("]", "").Split(',');


            List<Spl_WareShopCartModel> spl_WareShops = new List<Spl_WareShopCartModel>();
            Spl_WareModel _WareModel = new Spl_WareModel();
            for (int i = 0; i < opcid.Length; i++)
            {
                if (m_BLL.GetById(opcid[i]) != null)
                {
                    _WareModel = m_BLL.GetById(opcid[i]);
                    spl_WareShops.Add(new Spl_WareShopCartModel()
                    {
                        Id = _WareModel.Id,
                        Name = _WareModel.Name,
                        Price = _WareModel.Price,
                        WareCount = _WareModel.WareCount==null?0: _WareModel.WareCount,
                        WareState = _WareModel.WareState==null?false: _WareModel.WareState,
                        Thumbnail = _WareModel.Thumbnail
                    });

                }
            }
            return Json(spl_WareShops);
        }
    }
}
