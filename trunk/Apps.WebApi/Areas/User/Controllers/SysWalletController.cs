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

        ValidationErrors errors =new ValidationErrors();

        [HttpGet]
        public SysWalletModel GetWallet(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userid"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["userid"].ToString();
            }
            
            return  GetWalletByUserID(queryStr);
        }
        public SysWalletModel GetWalletByUserID(string userID)
        {
           return sysWBLL.GetWallByUserID(userID);
        }
        [HttpPost]
        public bool PostWallet([FromBody]SysWallet wallet)
        {
            if (sysWBLL.GetById(wallet.id)!=null)
            {
                SysWalletModel walletmodel = sysWBLL.GetById(wallet.id);
                walletmodel.UserId = wallet.UserId;
                walletmodel.Balance = wallet.Balance;
                walletmodel.Froms = wallet.Froms;
                if (wallet.Froms=="消费")
                {
                    walletmodel.JieYu = walletmodel.JieYu - wallet.Balance;
                }
                else
                {
                    walletmodel.JieYu = walletmodel.JieYu + wallet.Balance;
                }
                walletmodel.UpdateTime = DateTime.Now;
                return sysWBLL.Edit(ref errors, walletmodel);                 
            }
            else
            {
                SysWalletModel newmodel = new SysWalletModel();
                newmodel.id = ResultHelper.NewId;
                newmodel.UserId = wallet.UserId;
                newmodel.Balance = wallet.Balance;
                newmodel.Froms = wallet.Froms;
                newmodel.JieYu = newmodel.JieYu + wallet.Balance;
                newmodel.CreateTime = DateTime.Now;
                return sysWBLL.Create(ref errors, newmodel);
            }
            
        }
    }
}
