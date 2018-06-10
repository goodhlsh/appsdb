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
        [Dependency]
        public ISysJiaPuBLL mj_BLL { get; set; }
        [Dependency]
        public ISysJiaPuBeforeBLL mjb_BLL { get; set; }
        [Dependency]
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
            model.MobileNumber = json.UserName;
            //model.CreatePerson = GetUserTrueName();
            model.State = true;
            model.DepId = "20140724111955028255487bb419149";
            model.PosId = "201408071548164259039f26de27e49";
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
        public SysUserModel GetById(string id)
        {
            SysUserModel model = new SysUserModel();
            model = m_BLL.GetById(id);
            if (model != null)
            {
                return model;
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
            if (ma_BLL.GetById(info.Id) != null)
            {
                SysAddressModel model = ma_BLL.GetById(info.Id);
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
                model.Id = ResultHelper.NewId;
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

            if (JObject.Parse(opc["where"].ToString())["userId"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["userId"].ToString();
            }

            List<SysAddress> list = ma_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()), true);

            return Json(list);
        }
        /// <summary>
        /// 调用存储过程
        /// 参数：userid 用户id，pid 推荐人id，fje选择级别费
        /// </summary>
        /// <param name="sysJiaPu"></param>
        /// <returns></returns>
        [HttpPost]
        public void PutJiaPuBefore([FromBody]SysJiaPuBefore sysJiaPu)
        {
            if(sysJiaPu.uid.Length==32&&sysJiaPu.tid.Length==32&&sysJiaPu.fje>0)
               m_BLL.IntoSysJiaPuBefore(sysJiaPu.uid, sysJiaPu.tid, (decimal)sysJiaPu.fje);
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
            if (sysJiaPu.UserId.Length == 32 && sysJiaPu.ParentId.Length == 32 && m_BLL.GetRefSysJiaPu(sysJiaPu.UserId) == null)
                m_BLL.IntoSysJiaPu(sysJiaPu.UserId, sysJiaPu.TId,sysJiaPu.ParentId,sysJiaPu.ZMPB2, (decimal)sysJiaPu.FirstJinE);
        }
        public object GetByIdFromJiaPu(string userId)
        {
            if (mj_BLL.GetById(userId)!=null)
            {
                return Json(mj_BLL.GetById(userId));
            }
            else
            {
                return null;
            }
                   
        }
        /// <summary>
        /// 根据推荐人tid获取未指定位置的儿子
        /// </summary>
        /// <param name="tid_"></param>
        /// <returns></returns>
        public object GetListByTID(string tid_)
        {
            if (m_BLL.GetSysJiaPuBefore(tid_) != null)
            {
                return Json(m_BLL.GetSysJiaPuBefore(tid_));
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public object PostUserAuth([FromBody]SysUserEditModel user)
        {

            SysUserEditModel model = new SysUserEditModel();
            SysUserModel user2 = GetById(user.Id);
            model.Id = user2.Id;
            model.TrueName = user.TrueName;
            model.IdentityCardFile = user.IdentityCardFile;
            model.IdentityCardBackFile = user.IdentityCardBackFile;
            model.MobileNumber = user.MobileNumber;
            model.Card = user.Card;
            model.IsAuth = true;
            bool ret = m_BLL.Edit(ref errors, model);
            if (ret)
            {
                user2= GetById(user.Id);
                return Json(user2);
            }
            else
            {
                RetBool rb = new RetBool();
                rb.ret = ret;
                return Json(rb);
            }
        }


    }
}

