using Apps.IBLL;
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
    public class SysAchievementController : ApiController
    {
        [Dependency]
        public ISysPurchaseHistoryBLL m_BLL { get; set; }
        [HttpGet]
        public object GetAchListByUser(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["uid"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["uid"].ToString();
            }
            var startdate = "";
            var enddate = "";
            if (JObject.Parse(opc["where"].ToString())["startdate"] != null)
            {
                startdate = JObject.Parse(opc["where"].ToString())["startdate"].ToString();
            }
            if (JObject.Parse(opc["where"].ToString())["enddate"] != null)
            {
                enddate = JObject.Parse(opc["where"].ToString())["enddate"].ToString();
            }
            
            List<SysAchieveModel> models = new List<SysAchieveModel>();
            List<SysPurchaseHistoryModel> sysPurchases = m_BLL.GetList(queryStr).Where(a=>a.CreateTime>=Convert.ToDateTime(startdate)&&a.CreateTime<= Convert.ToDateTime(enddate)).OrderByDescending(a => a.CreateTime).Skip(int.Parse(opc["skip"].ToString())).Take(int.Parse(opc["limit"].ToString())).ToList();
            foreach (SysPurchaseHistoryModel item in sysPurchases)
            {
                models.Add(new SysAchieveModel
                {
                    ShouRu = item.ShouRu.Value,
                    CreateTime= Convert.ToDateTime(item.CreateTime.Value.ToString("yyyy/M/d")),
                    Froms=item.Froms,
                    Note=item.Note
                });
            }
            if (models!=null)
            {
                return Json(models);
            }
            else
            {
                return null;
            }


        }
    }
}
