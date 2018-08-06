using Apps.Common;
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
    public class SysMessageController : ApiController
    {
        [Dependency]
        public ISysMessageBLL SysMessageBLL { get; set; }

        ValidationErrors errors = new ValidationErrors();
        [HttpGet]
        public object GetMessageListByCategory(string filter)
        {
            List<SysMessageModel> messages = new List<SysMessageModel>();
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["Category"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["Category"].ToString();
            }
            var userId = "";
            if (JObject.Parse(opc["where"].ToString())["ToWho"] != null)
            {
                userId = JObject.Parse(opc["where"].ToString())["ToWho"].ToString();
            }
            messages = SysMessageBLL.GetListWithCategory(queryStr, userId, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(messages);
        }
        [HttpGet]
        public object GetById(string filter)
        {
            SysMessageModel model = new SysMessageModel();
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["Id"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["Id"].ToString();
                model = SysMessageBLL.GetById(queryStr);
                return Json(model);
            }
            else
            {
                return null;
            }

        }
        [HttpPost]
        public object UpdateMessage([FromBody]SysMessageModel sysMessage)
        {
            SysMessageModel model = new SysMessageModel();
            model = SysMessageBLL.GetById(sysMessage.Id);
            SysMessageModel newmodel = new SysMessageModel();
            newmodel.Id = sysMessage.Id;
            newmodel.Title = model.Title;
            newmodel.Cont = model.Cont;
            newmodel.FromWho = model.FromWho;
            newmodel.ToWho = model.ToWho;
            newmodel.CreateTime = model.CreateTime;
            newmodel.Category = model.Category;
            newmodel.UpdateTime = DateTime.Now;
            newmodel.IsRead = true;
            bool ret = false;
            ret = SysMessageBLL.Edit(ref errors, newmodel);
            if (ret)
            {
                return Json(newmodel);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public object PutMessage([FromBody]SysMessageModel model)
        {
            SysMessageModel newmodel = new SysMessageModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.Title = model.Title;
            newmodel.Cont = model.Cont;
            newmodel.FromWho = model.FromWho;
            newmodel.ToWho = model.ToWho;
            newmodel.CreateTime = DateTime.Now;
            newmodel.Category = model.Category;
            newmodel.UpdateTime = model.UpdateTime;
            bool ret = false;
            ret = SysMessageBLL.Create(ref errors, newmodel);
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
