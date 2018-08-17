using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.User.Controllers
{
    public class SysFirstController : ApiController
    {
        [Dependency]
        public ISysFirstBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [HttpGet]
        public SysFirstModel GetCurrrentFirst(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }

            return m_BLL.GetCurrrentFirstByUserID(queryStr);
        }
        [HttpPost]
        public object PostFirst([FromBody]SysFirst fir)
        {
            SysFirstModel newmodel = new SysFirstModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.UserId = fir.UserId;
            newmodel.OrderId = fir.OrderId;
            newmodel.JinE = fir.JinE;
            newmodel.CurrentFirst = fir.JinE + fir.CurrentFirst;
            newmodel.Note = fir.Note;
            newmodel.CreateTime = DateTime.Now;
            newmodel.ShunXu = fir.ShunXu + 1;
            bool ret = false;
            ret = m_BLL.Create(ref errors, newmodel);
            if (ret)
            {
                return Json(newmodel);
            }
            else
            {
                return null;
            }
        }
    }
}
