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
        public ISpl_ProductCategoryBLL mty_BLL { get; set; }

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

            List<Spl_WareShowModel> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetById(string filter)
        {
            JObject opc = JObject.Parse(filter);            
            if (JObject.Parse(opc["where"].ToString())["ID"] == null)
            {

                return Json("");
            }

            Spl_WareModel model = new Spl_WareModel();
            model = m_BLL.GetById(JObject.Parse(opc["where"].ToString())["ID"].ToString());
            if (model != null)
            {
                Spl_WareShowModel ware = new Spl_WareShowModel();
                ware.id = model.id;
                ware.Name = model.Name;
                ware.OriginPrice = model.OriginPrice;
                ware.Price = model.Price;
                ware.Description = model.Description;
                ware.ProductCategoryId = model.ProductCategoryId;
                ware.ShowType = model.ShowType;
                ware.Stock = model.Stock;
                ware.Thumbnail = model.Thumbnail;
                ware.Unit = model.Unit;
                //ware.WareCount = model.WareCount; //购物车辅助字段
                //ware.WareState = model.WareState; //购物车辅助字段
                Spl_WareInfoModel infoModel= mi_BLL.GetById(model.WareInfoId);
                if (infoModel!=null)
                {
                    ware.Picture0 =  infoModel.Picture0;
                    ware.Picture1 = infoModel.Picture1;
                    ware.Picture2 = infoModel.Picture2;
                    ware.Picture3 = infoModel.Picture3;
                    ware.Picture4 = infoModel.Picture4;
                    ware.Picture5 = infoModel.Picture5;
                    ware.ToTop = (bool)infoModel.ToTop;                
                    
                }
                
                return Json(ware);
            }
            else
            {
                return Json("");
            }

        }
        [HttpGet]
        public object GetAllTop(string filter)
        {
            JObject opc = JObject.Parse(filter);
            bool queryStr = false;
            if (JObject.Parse(opc["where"].ToString())["toTop"] != null)
            {
                queryStr =bool.Parse(JObject.Parse(opc["where"].ToString())["toTop"].ToString());
            }
           
            List<Spl_WareInfo> list = m_BLL.GetPageWareInfo(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            List<Spl_WareShowModel> wareShowModels= new List<Spl_WareShowModel>();
            foreach (var item in list)
            {
                Spl_Ware wareInfo = mi_BLL.GetRefWare(item.id);
                if (wareInfo!=null&&item.Picture0!=null)
                {
                    wareShowModels.Add(new Spl_WareShowModel()
                    {
                        id = wareInfo.id,
                        ToTop = (bool)item.ToTop,
                        Name=wareInfo.Name,
                        Description=wareInfo.Description,
                        OriginPrice=wareInfo.OriginPrice,
                        Price=wareInfo.Price,
                        Picture0=item.Picture0==null?"":item.Picture0,
                        Picture1 = item.Picture1,
                        Picture2 = item.Picture2,
                        Picture3 = item.Picture3,
                        Picture4 = item.Picture4,
                        Picture5 = item.Picture5,
                        Thumbnail=wareInfo.Thumbnail,
                        ShowType=wareInfo.ShowType,
                        Stock=wareInfo.Stock,
                        Detail=item.Detail,
                        ProductCategoryId=wareInfo.ProductCategoryId,
                        Note=wareInfo.Note,
                        Unit=wareInfo.Unit,
                        WareInfoId=wareInfo.WareInfoId
                    });
                }                
            }
            return Json(wareShowModels);
           
        }
  
    
[HttpGet]
        public object GetAllLike(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["like"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["like"].ToString();
            }

            List<Spl_WareShowModel> list = m_BLL.GetPageLike(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

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
        public object GetAllByShopCart(string filter)
        {
            JObject opc = JObject.Parse(filter);
            string[] opcid =  (JObject.Parse(opc["where"].ToString())["id"].ToString()).Replace("[","").Replace("\"","").Replace("]","").Split(',');
            
           
            List<Spl_WareShopCartModel> spl_WareShops = new List<Spl_WareShopCartModel>();
            Spl_WareModel _WareModel = new Spl_WareModel();
            for (int i = 0; i < opcid.Length; i++)
            {
                if (m_BLL.GetById(opcid[i])!=null)
                {
                    _WareModel = m_BLL.GetById(opcid[i]);
                    spl_WareShops.Add(new Spl_WareShopCartModel()
                    {
                        id= _WareModel.id,
                        Name= _WareModel.Name,
                        Price= _WareModel.Price,
                        WareCount= _WareModel.WareCount,
                        WareState=_WareModel.WareState,
                        Thumbnail= _WareModel.Thumbnail
                    });
                    
                }                
            }
            return Json(spl_WareShops);
        }
    }
}
