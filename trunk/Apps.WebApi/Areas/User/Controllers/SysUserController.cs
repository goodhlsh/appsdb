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
using System.Text.RegularExpressions;
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
        [Dependency]
        public ISysLevelsBLL islBll { get; set; }
        [Dependency]
        public ISysUserBLL isuBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [HttpPost]
        public object PostPasswd([FromBody]SysUserChangePwd sysUser)
        {
            SysUserModel userModel = new SysUserModel();
            userModel = m_BLL.GetById(sysUser.UserId);
            if (userModel != null && userModel.Password == sysUser.OldPassword)
            {
                SysUserEditModel model = new SysUserEditModel();
                model.Id = sysUser.UserId;
                model.Password = sysUser.NewPassword;

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName, "成功", "更新密码", "用户设置");
                    return JsonHandler.CreateMessage(1, Resource.InsertSucceed);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName + "," + ErrorCol,

    "失败", "更新密码", "用户设置");
                    return JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol);
                }
            }
            else
            {
                return JsonHandler.CreateMessage(0, "输入密码有误！");
            }

        }
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
                //LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName, "成功", "修改头像", "用户设置");
                // return JsonHandler.CreateMessage(1, Resource.InsertSucceed);
                SysUserModel user = new SysUserModel();
                SysUserInfo userInfo = new SysUserInfo();
                user = GetById(model.Id);
                userInfo.Id = user.Id;
                userInfo.UserName = user.UserName;
                userInfo.TrueName = user.TrueName;
                userInfo.Card = user.Card;
                //userInfo.shfzh = user.IdentityCardFile;
                userInfo.MobileNumber = user.MobileNumber;
                userInfo.Token = "";// Token;
                //userInfo.State = user.State;
                userInfo.Photo = user.Photo;
                userInfo.QRCode = user.QRCode;
                userInfo.IsAuth = (user.IsAuth != null) ? user.IsAuth : false;
                userInfo.RecommendID = user.RecommendID;
                if (isuBLL.GetRefSysJiaPu(user.Id) != null && islBll.GetById(isuBLL.GetRefSysJiaPu(user.Id).LevelId.ToString()) != null)
                {
                    userInfo.Jibie = islBll.GetById(isuBLL.GetRefSysJiaPu(user.Id).LevelId.ToString()).Name;
                }
                else
                {
                    userInfo.Jibie = "普通会员";
                }
                return Json(userInfo);
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
        public object SetAddress([FromBody]SysAddressModel info)
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
                if (result)
                {
                    return Json(model);
                }
                else
                {
                    RetBool rt = new RetBool();
                    rt.ret = result;
                    return Json(rt);
                }
                
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
                if (result)
                {
                    return Json(model);
                }
                else
                {
                    RetBool rt = new RetBool();
                    rt.ret = result;
                    return Json(rt);
                }
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
            SysAddressModel AddressModel = new SysAddressModel();
            AddressModel.Id = list[0].Id;
            AddressModel.City = list[0].City;
            AddressModel.CreateTime = list[0].CreateTime;
            AddressModel.House = list[0].House;
            AddressModel.IsDefault = true;
            AddressModel.Mobile = list[0].Mobile;
            AddressModel.Name = list[0].Name;
            AddressModel.Province = list[0].Province;
            AddressModel.Street = list[0].Street;
            AddressModel.Typs = list[0].Typs;
            AddressModel.UpdateTime = list[0].UpdateTime;
            AddressModel.UserId = list[0].UserId;
            AddressModel.TrueName = list[0].SysUser.TrueName;

            return Json(AddressModel);
        }
        int result;
        bool result_bool;
        /// <summary>
        /// 调用存储过程
        /// 参数：userid 用户id，pid 推荐人id，fje选择级别费
        /// </summary>
        /// <param name="sysJiaPu"></param>
        /// <returns></returns>
        [HttpPost]
        public object PutJiaPuBefore([FromBody]SysJiaPuBefore sysJiaPu)
        {
            
            if (sysJiaPu.uid.Length == 31 && sysJiaPu.tid.Length == 31 && sysJiaPu.fje > 0)
              result   = m_BLL.IntoSysJiaPuBefore(sysJiaPu.uid, sysJiaPu.tid, sysJiaPu.fje);
            RetInt ri = new RetInt();
            ri.ret = result;
            return Json(ri);
        }
        [HttpPost]
        public object UpdateJiaPuBefore([FromBody]SysJiaPuBeforeModel sysJiaPu)
        {

            if (sysJiaPu.uid.Length == 31 && sysJiaPu.tid.Length == 31 && sysJiaPu.fje > 0)
                result_bool = mjb_BLL.Edit(ref errors,sysJiaPu);
            RetBool ri = new RetBool();
            ri.ret = result_bool;
            return Json(ri);
        }
        /// <summary>
        /// 调用存储过程
        /// 参数：userid 用户id，pid 推荐人id，fje选择级别费
        /// </summary>
        /// <param name="sysJiaPu"></param>
        /// <returns></returns>
        [HttpPost]
        public object PutJiapu([FromBody]SysJiaPuBefore sysJiaPu_)
        {
            if (m_BLL.GetRefSysJiaPu(sysJiaPu_.uid)!=null)
            {
                return null;
            }
            string ZMPB2 = "";
            RetInt ri = new RetInt();
            SysJiaPu sysJiaPu = new SysJiaPu();
            sysJiaPu = m_BLL.GetRefSysJiaPu(sysJiaPu_.tid);
            if (sysJiaPu==null)
            {
                return null;
            }
            Regex r = new Regex("0");
            Match m = r.Match(sysJiaPu.ZMP15);
            if (m.Success)
            {
                switch (m.Index)
                {
                    case 1:
                        ZMPB2 = "A0";
                        break;
                    case 2:
                        ZMPB2 = "A1";
                        break;
                    case 3:
                        ZMPB2 = "B0";
                        break;
                    case 4:
                        ZMPB2 = "B1";
                        break;
                    case 5:
                        ZMPB2 = "C0";
                        break;
                    case 6:
                        ZMPB2 = "C1";
                        break;
                    case 7:
                        ZMPB2 = "D0";
                        break;
                    case 8:
                        ZMPB2 = "D1";
                        break;
                    case 9:
                        ZMPB2 = "E0";
                        break;
                    case 10:
                        ZMPB2 = "E1";
                        break;
                    case 11:
                        ZMPB2 = "F0";
                        break;
                    case 12:
                        ZMPB2 = "F1";
                        break;
                    case 13:
                        ZMPB2 = "G0";
                        break;
                    case 14:
                        ZMPB2 = "G1";
                        break;
                    case 15:
                        ZMPB2 = "H0";
                        break;
                    case 16:
                        ZMPB2 = "H1";
                        break;
                    case 17:
                        ZMPB2 = "I0";
                        break;
                    case 18:
                        ZMPB2 = "I1";
                        break;
                    case 19:
                        ZMPB2 = "J0";
                        break;
                    case 20:
                        ZMPB2 = "J1";
                        break;
                    case 21:
                        ZMPB2 = "K0";
                        break;
                    case 22:
                        ZMPB2 = "K1";
                        break;
                    case 23:
                        ZMPB2 = "L0";
                        break;
                    case 24:
                        ZMPB2 = "L1";
                        break;
                    case 25:
                        ZMPB2 = "M0";
                        break;
                    case 26:
                        ZMPB2 = "M1";
                        break;
                    case 27:
                        ZMPB2 = "N0";
                        break;
                    case 28:
                        ZMPB2 = "N1";
                        break;
                    case 29:
                        ZMPB2 = "O0";
                        break;
                    case 30:
                        ZMPB2 = "O1";
                        break;
                    case 31:
                        ZMPB2 = "P0";
                        break;
                    case 32:
                        ZMPB2 = "P1";
                        break;
                    case 33:
                        ZMPB2 = "Q0";
                        break;
                    case 34:
                        ZMPB2 = "Q1";
                        break;
                    case 35:
                        ZMPB2 = "R0";
                        break;
                    case 36:
                        ZMPB2 = "R1";
                        break;
                    case 37:
                        ZMPB2 = "S0";
                        break;
                    case 38:
                        ZMPB2 = "S1";
                        break;
                    case 39:
                        ZMPB2 = "T0";
                        break;
                    case 40:
                        ZMPB2 = "T1";
                        break;
                    case 41:
                        ZMPB2 = "U0";
                        break;
                    case 42:
                        ZMPB2 = "U1";
                        break;
                    case 43:
                        ZMPB2 = "V0";
                        break;
                    case 44:
                        ZMPB2 = "V1";
                        break;
                    case 45:
                        ZMPB2 = "W0";
                        break;
                    case 46:
                        ZMPB2 = "W1";
                        break;
                    case 47:
                        ZMPB2 = "X0";
                        break;
                    case 48:
                        ZMPB2 = "X1";
                        break;
                    case 49:
                        ZMPB2 = "Y0";
                        break;
                    case 50:
                        ZMPB2 = "Y1";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ZMPB2 = "A0";
            }
            if (sysJiaPu_.uid.Length == 31 && sysJiaPu_.tid.Length == 31 && m_BLL.GetRefSysJiaPu(sysJiaPu_.uid) == null)
                ri.ret = m_BLL.IntoSysJiaPu(sysJiaPu_.uid, sysJiaPu_.tid, sysJiaPu_.tid, ZMPB2, (decimal)sysJiaPu_.fje);
            return Json(ri);
        }
        [HttpGet]
        public object GetByIdFromJiaPu(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["userId"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["userId"].ToString();
            }
            if (mj_BLL.GetById(queryStr) != null)
            {
                return Json(mj_BLL.GetById(queryStr));
            }
            else
            {
                return null;
            }

        }
        [HttpGet]
        /// <summary>
        /// 根据推荐人tid获取未指定位置的儿子
        /// </summary>
        /// <param name="tid_"></param>
        /// <returns></returns>
        public object GetListByTID(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["tid"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["tid"].ToString();
            }
            List<SysUserForJiaPu> ujp = new List<SysUserForJiaPu>();
            List<SysJiaPuBefore> jpb = new List<SysJiaPuBefore>();
            jpb = m_BLL.GetSysJiaPuBefore(filter);
            if (jpb!= null&&jpb.Count>0)
            {
                foreach (SysJiaPuBefore item in jpb)
                {
                    SysUserForJiaPu u = new SysUserForJiaPu();
                    u.uid = item.uid;
                    u.tid = item.tid;
                    SysUserModel user = new SysUserModel();
                    user = m_BLL.GetById(item.uid);
                    if (user!=null)
                    {
                        u.truename = user.TrueName;
                        u.username = user.UserName;
                    }
                    else
                    {
                        u.truename = "";
                        u.username = "";
                    }
                    u.fje = item.fje;
                    if ((bool)item.isdone)
                    {
                        u.isdone = "已通过";
                    }
                    else
                    {
                        u.isdone = "未通过";
                    }
                    
                    ujp.Add(u);
                }
                return Json(ujp);
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public object GetAllChildren(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["uid"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["uid"].ToString();
            }
            List<SysAllChildUser> childUsers = new List<SysAllChildUser>();
            childUsers = m_BLL.GetAllChildren(queryStr);
            if (childUsers!=null)
            {
                return Json(childUsers);
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
                user2 = GetById(user.Id);
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

