using Apps.Common;
using Apps.IBLL;
using Apps.Locale;
using Apps.Models;
using Apps.Models.Sys;
using Apps.WebApi.Core;
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
    public class SysUserController : BaseApiController
    {
        [Dependency]
        public ISysUserBLL m_BLL { get; set; }
        public ISysAddressBLL ma_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        [HttpPost]
        public object PostCreateOne([FromBody]SysUserLogingData json)
        {
            SysUserModel model = new SysUserModel();
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.UserName = json.UserName;
            model.Password = ValueConvert.MD5(json.Password);
            //model.CreatePerson = GetUserTrueName();
            model.State = true;
            if (m_BLL.Create(ref errors, model))
            {
                LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName, "成功", "创建", "用户设置");
                return JsonHandler.CreateMessage(1, Resource.InsertSucceed);
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName + "," + ErrorCol,

"失败", "创建", "用户设置");
                return JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol);
            }
        }

        /// <summary>
        /// 参数
        /// userid:	"id",
        /// photo:	"头像URL"
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public object ChangePhoto([FromBody]SysUserModel model)
        {
            if (m_BLL.Edit(ref errors, new SysUserEditModel()
            {
                Id = model.Id,
                Photo = model.Photo
            }))
            {
                LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName, "成功", "修改头像", "用户设置");
                return JsonHandler.CreateMessage(1, Resource.InsertSucceed);
            }
            else
            {
                string ErrorCol = errors.Error;
                LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName + "," + ErrorCol,

"失败", "修改头像", "用户设置");
                return JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol);
            }
        }
        /// <summary>
        /// 获取一用户信息
        /// 参数id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetById(string id)
        {
            SysUserModel model = new SysUserModel();
            model = m_BLL.GetById(id);
            if (model != null)
            {
                return Json(model);
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 添加收货人地址
        /// 参数：
        /// "id":不为空就是更改数据
        /// "name":	"收货人姓名",		
        ///"mobile":	"手机号",			
        ///"region":	"所在区域",	Province		
        ///"address":	"具体地址",			
        ///"house":	"楼号门牌",				
        ///"type":	地址类型，Number类型(0:公司，1:住宅，2:学校，3:其它) 
        ///"isDefault":	是否为默认地址,	布尔类型(true	or  false)
        ///"user":	"用户ID"
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public bool SetAddress([FromBody]SysAddressModel info)
        {
            //编辑
            if (ma_BLL.GetById(info.id) != null)
            {
                SysAddressModel model = ma_BLL.GetById(info.id);
                model.Name = info.Name;
                model.Mobile = info.Mobile;
                model.Province = info.Province;
                model.Street = info.Street;
                model.City = info.City;
                model.House = info.House;
                model.Typs = info.Typs;
                model.IsDefault = info.IsDefault;
                model.UserId = info.UserId;
                model.UpdateTime = DateTime.Now;
                bool result = ma_BLL.Edit(ref errors, model);
                return result;
            }
            //新增
            else
            {
                SysAddressModel model = new SysAddressModel();
                model.id = ResultHelper.NewId;
                model.Name = info.Name;
                model.Mobile = info.Mobile;
                model.Province = info.Province;
                model.Street = info.Street;
                model.City = info.City;
                model.House = info.House;
                model.Typs = info.Typs;
                model.UserId = info.UserId;
                model.IsDefault = info.IsDefault;
                model.CreateTime = DateTime.Now;
                bool result = ma_BLL.Create(ref errors, model);
                return result;
            }
        }
        /// <summary>
        /// 获取用户所有地址
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetAllAddress(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["ID"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["ID"].ToString();
            }

            List<SysAddress> list = ma_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        /// <summary>
        /// 获取用户默认地址
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetDefaultAddress(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            
            if (JObject.Parse(opc["where"].ToString())["ID"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["ID"].ToString();
            }

            List<SysAddress> list = ma_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()),true);

            return Json(list);
        }
        /// <summary>
        /// 调用存储过程
        /// 参数：userid 用户id，pid 推荐人id，fje选择级别费
        /// </summary>
        /// <param name="sysJiaPu"></param>
        /// <returns></returns>
        [HttpPost]
        public void PutJiapu([FromBody]SysJiaPu sysJiaPu)
        {
            m_BLL.IntoSysJiaPu(sysJiaPu.UserId, sysJiaPu.ParentId, (decimal)sysJiaPu.FirstJinE);
        }
    }
}

