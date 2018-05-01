using Apps.Common;
using Apps.Models;
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
        public Spl.IBLL.ISpl_WareBLL m_BLL { get; set; }
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
    }
}
