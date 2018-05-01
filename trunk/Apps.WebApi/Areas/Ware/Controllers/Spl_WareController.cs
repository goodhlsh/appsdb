using Apps.Common;
using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IBLL;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.Ware.Controllers
{
    public class Spl_WareController : ApiController
    {
        [Dependency]
        public ISpl_WareBLL m_BLL { get; set; }
        [Dependency]
        public ISpl_WareInfoBLL mi_BLL { get; set; }
        [Dependency]
        public ISpl_ProductCategoryBLL mty_BLL { get; set; }
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

            List<Spl_Ware> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetById(string id)
        {
            Spl_WareModel model = new Spl_WareModel();
            model = m_BLL.GetById(id);
            if (model != null)
            {
                Spl_Ware ware = new Spl_Ware();
                ware.id = model.id;
                ware.Name = model.Name;
                ware.OriginPrice = model.OriginPrice;
                ware.Price = model.Price;
                ware.Description = model.Description;
                ware.ShowType = model.ShowType;
                ware.Stock = model.Stock;
                ware.Thumbnail = model.Thumbnail;
                ware.Unit = model.Unit;
                //ware.WareCount = model.WareCount; //购物车辅助字段
                //ware.WareState = model.WareState; //购物车辅助字段
                Spl_WareInfoModel infoModel= mi_BLL.GetById(model.WareInfoId);
                if (infoModel!=null)
                {
                    ware.Spl_WareInfo.Picture0 = infoModel.Picture0;
                    ware.Spl_WareInfo.Picture1 = infoModel.Picture1;
                    ware.Spl_WareInfo.Picture2 = infoModel.Picture2;
                    ware.Spl_WareInfo.Picture3 = infoModel.Picture3;
                    ware.Spl_WareInfo.Picture4 = infoModel.Picture4;
                    ware.Spl_WareInfo.Picture5 = infoModel.Picture5;
                    ware.Spl_WareInfo.ToTop = infoModel.ToTop;
                }
                Spl_ProductCategoryModel category = mty_BLL.GetById(model.ProductCategoryId);
                if (category!=null)
                {
                    ware.Spl_ProductCategory.Name = category.Name;
                }
                
                return Json(ware);
            }
            else
            {
                return null;
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
           
            List<Spl_WareInfo> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
            
        }
        [HttpGet]
        public object GetAllLike(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["keyword"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["keyword"].ToString();
            }

            List<Spl_Ware> list = m_BLL.GetPageLike(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetAllActive(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";            

            List<Spl_Actives> list = m_BLL.GetPageActive(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
    }
}
