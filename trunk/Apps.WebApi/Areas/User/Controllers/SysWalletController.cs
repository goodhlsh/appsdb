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
    public class SysWalletController : ApiController
    {
        [Dependency]
        public ISysWalletBLL sysWBLL { get; set; }

        ValidationErrors errors = new ValidationErrors();

        [HttpGet]
        public SysWalletModel GetWallet(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }

            return GetWalletByUserID(queryStr);
        }
        public SysWalletModel GetWalletByUserID(string userID)
        {
            return sysWBLL.GetWallByUserID(userID);
        }
        /// <summary>
        /// 购买或充值都可以调用此方法
        /// </summary>
        /// <param name="wallet">消费</param>
        /// <param name="wallet">充值</param>
        /// <returns></returns>
        [HttpPost]
        public object PostWallet([FromBody]SysWallet wallet)
        {
            SysWalletModel newmodel = new SysWalletModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.UserId = wallet.UserId;
            newmodel.Balance = wallet.Balance;
            newmodel.Froms = wallet.Froms;
            if (wallet.Froms == "消费")
            {
                newmodel.JieYu = wallet.JieYu - wallet.Balance;
            }
            else
            {
                newmodel.JieYu = wallet.JieYu + wallet.Balance;
            }
            newmodel.Note = wallet.Note;
            newmodel.CreateTime = DateTime.Now;
            newmodel.ShunXu = wallet.ShunXu + 1;
            bool ret = false;
            ret=sysWBLL.Create(ref errors, newmodel);
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
