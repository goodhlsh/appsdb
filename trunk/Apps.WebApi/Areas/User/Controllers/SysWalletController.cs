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
        /// <summary>
        /// 获取所有人当前余额
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public object GetList(string filter)
        {
            JObject opc = JObject.Parse(filter);            
            
            List<P_Sys_GetUserWallet_Result> datalist = new List<P_Sys_GetUserWallet_Result>();
            
            datalist = sysWBLL.GetUserWallet().ToList(); ;
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
                    Froms=item.Froms,
                    Note=item.Note
                });
            }

            return Json(sysWalletModels);
        }
        /// <summary>
        /// 获取某人当前余额
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
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
        /// 获取某人余额账单
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetAllWalletByUserId(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }
            return Json(sysWBLL.GetAllWallByUserID(queryStr)); 
        }
        /// <summary>
        /// 购买或充值都可以调用此方法
        /// </summary>
        /// <param name="wallet">消费</param>
        /// <param name="wallet">充值</param>
        /// <param name="wallet">分红</param>
        /// <param name="wallet">奖励</param>
        /// <returns></returns>
        [HttpPost]
        public object PostWallet([FromBody]SysWallet wallet)
        {
            SysWalletModel newmodel = new SysWalletModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.UserId = wallet.UserId;
            newmodel.Balance = wallet.Balance;
            newmodel.Froms = wallet.Froms;
            if (wallet.Froms.Contains("消费"))
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
