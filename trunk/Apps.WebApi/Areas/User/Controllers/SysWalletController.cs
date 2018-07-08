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
        public object GetList(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userId"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["userId"].ToString();
            }

            List<P_Sys_GetUserWallet_Result> datalist = new List<P_Sys_GetUserWallet_Result>();
            
            datalist = sysWBLL.GetUserWallet().Where(a => a.UserId==queryStr).ToList(); ;
            datalist=datalist.OrderByDescending(a => a.CreateTime).Skip(int.Parse(opc["skip"].ToString())).Take(int.Parse(opc["limit"].ToString())).ToList();
            List<SysWalletModel> sysWalletModels = new List<SysWalletModel>();

            foreach (P_Sys_GetUserWallet_Result item in datalist)
            {
                sysWalletModels.Add(new SysWalletModel
                {
                    Id = item.Id,
                    Balance=item.Balance,
                    CreateTime=item.CreateTime,
                    JieYu=item.JieYu,
                    Froms=item.Froms 
                });
            }

            return Json(sysWalletModels);
        }
        [HttpGet]
        public SysWalletModel GetWallet(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }

            return sysWBLL.GetWallByUserID(queryStr);
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
