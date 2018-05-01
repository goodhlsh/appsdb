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
    public class SysWalletController : ApiController
    {
        [Dependency]
        public ISysWalletBLL sysWBLL { get; set; }
        [HttpGet]
        public decimal? GetWallet(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }
            if (GetWalletByUserID(queryStr)==null)
            {
                return null;
            }
            return  GetWalletByUserID(queryStr).JieYu;
        }
        public SysWalletModel GetWalletByUserID(string userID)
        {
           return sysWBLL.GetWallByUserID(userID);
        }
    }
}
