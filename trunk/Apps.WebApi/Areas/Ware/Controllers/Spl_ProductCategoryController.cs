using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Apps.Models.Spl;
using Apps.Models;
using Apps.WebApi.Core;
using Apps.Common;
using Newtonsoft.Json.Linq;

namespace Apps.WebApi.Areas.Ware.Controllers
{
    public class Spl_ProductCategoryController : ApiController
    {
        //[Dependency]
        //public Spl.IBLL.ISpl_WareBLL m2_BLL { get; set; }
        [Dependency]
        public Spl.IBLL.ISpl_ProductCategoryBLL m_BLL { get; set; }
        Common.ValidationErrors errors = new Common.ValidationErrors();

        //[SupportFilter]
        [HttpGet]
        public object GetList(string filter)
        {
            // var anonymous = new { queryStr = String.Empty, skip = new int(),limit=new int() };
            // var opc = JsonHandler.DeserializeAnonymousType(filter, anonymous);
            //List<Spl_ProductCategoryModel> list = m_BLL.GetPage(opc.queryStr,opc.skip,opc.limit);            
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["ID"] != null) {

                queryStr = JObject.Parse(opc["where"].ToString())["ID"].ToString();
            }
                
            List<Spl_ProductCategoryModel> list = m_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            return Json(list);
        }
    }
}
