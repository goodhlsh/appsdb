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
        public ISysJiaPuBeforeBLL mjb_BLL { get; set; }
        [Dependency]
        public ISysAddressBLL ma_BLL { get; set; }
        [Dependency]
        public ISysLevelsBLL islBll { get; set; }
        [Dependency]
        public ISysTuiUserBLL m_tuBLL { get; set; }
        [Dependency]
        public ISysJiaPuBLL mj_BLL { get; set; }

        ValidationErrors errors = new ValidationErrors();
        [HttpPost]
        public object PostPasswd([FromBody]SysUserChangePwd sysUser)
        {
            SysUserModel userModel = new SysUserModel();
            userModel = m_BLL.GetById(sysUser.UserId);
            if (userModel != null)
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
        public object PostCreateOne([FromBody]SysUserLogingData user)
        {
            SysUser sysUser = new SysUser();
            sysUser = m_BLL.GetBySelUserName(user.UserName);
            if (sysUser != null)
            {
                return null;
            }
            SysUserModel model = new SysUserModel();
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            model.UserName = user.UserName;
            model.Password = ValueConvert.MD5(user.Password);
            model.MobileNumber = user.UserName;
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
            SysUserModel user = new SysUserModel();
            user = GetById(model.Id);
            if (m_BLL.Edit(ref errors, new SysUserEditModel()
            {
                Id = model.Id,
                //截取沙箱路径picture

                Photo = model.Photo.Substring(model.Photo.IndexOf("/pic")),
                TrueName = user.TrueName,
                Card = user.Card,
                MobileNumber = user.MobileNumber,
                PhoneNumber = user.PhoneNumber,
                QQ = user.QQ,
                EmailAddress = user.EmailAddress,
                OtherContact = user.OtherContact,
                Province = user.Province,
                City = user.City,
                Village = user.Village,
                Address = user.Address,
                State = user.State,
                CreateTime = user.CreateTime,
                CreatePerson = user.CreatePerson,
                Sex = user.Sex,
                Birthday = user.Birthday.ToString(),
                JoinDate = user.JoinDate.ToString(),
                Marital = user.Marital,
                Political = user.Political,
                Nationality = user.Nationality,
                Native = user.Native,
                School = user.School,
                Professional = user.Professional,
                Degree = user.Degree,
                DepId = user.DepId == null ? "20140724111955028255487bb419149" : user.DepId,
                PosId = user.PosId == null ? "201408071548164259039f26de27e49" : user.PosId,
                Expertise = user.Expertise,
                JobState = user.JobState == null ? false : (bool)user.JobState,
                Attach = user.Attach,
                Lead = user.Lead,
                LeadName = user.LeadName,
                IsSelLead = user.IsSelLead == null ?false:(bool)user.IsSelLead,
                IsReportCalendar = user.IsReportCalendar == null ?false: (bool)user.IsReportCalendar,
                IsSecretary = user.IsSecretary == null ? false : (bool)user.IsSecretary,


                HomePhone = user.HomePhone,
                WXID = user.WXID,
                Signature = user.Signature,
                QRCode = user.QRCode,
                IdentityCardFile = user.IdentityCardFile,
                IdentityCardBackFile = user.IdentityCardBackFile,
                IsAuth = user.IsAuth == null ? false : (bool)user.IsAuth,
                AuditStatus = user.AuditStatus,
                Note = user.Note,
                SortCode = user.SortCode,
                RecommendID = user.RecommendID,
                Recommendor = user.Recommendor,
                RecommendTime = user.RecommendTime,
                EditorID = user.EditorID,
                UpdateTime = DateTime.Now,
                IsDeleted = user.IsDeleted == null ? false : (bool)user.IsDeleted,
                Questions = user.Questions,
                Answer = user.Answer,
                TuiCount = user.TuiCount
            }))
            {
                //LogHandler.WriteServiceLog(model.UserName, "Id:" + model.Id + ",Name:" + model.UserName, "成功", "修改头像", "用户设置");
                // return JsonHandler.CreateMessage(1, Resource.InsertSucceed);

                SysUserInfo userInfo = new SysUserInfo();
                user = GetById(model.Id);
                userInfo.Id = user.Id;
                userInfo.UserName = user.UserName;
                userInfo.TrueName = user.TrueName;
                userInfo.MobileNumber = user.MobileNumber;
                userInfo.Token = "";// Token;
                userInfo.Card = user.Card;
                userInfo.shfzh = user.IdentityCardFile;
                userInfo.State = user.State;
                userInfo.IsAuth = (user.IsAuth != null) ? user.IsAuth : false;
                if (mj_BLL.GetRefSysJiaPu(user.Id) != null)
                {
                    userInfo.TId = mj_BLL.GetRefSysJiaPu(user.Id).TId;
                    userInfo.TName = mj_BLL.GetRefSysJiaPu(user.Id).TName;
                    userInfo.PId = mj_BLL.GetRefSysJiaPu(user.Id).ParentId;
                    userInfo.PName = mj_BLL.GetRefSysJiaPu(user.Id).ParentName;
                }
                if (mj_BLL.GetRefSysJiaPu(user.Id) != null && islBll.GetById(mj_BLL.GetRefSysJiaPu(user.Id).LevelId.ToString()) != null)
                {
                    userInfo.Jibie = islBll.GetById(mj_BLL.GetRefSysJiaPu(user.Id).LevelId.ToString()).Name;
                }
                else
                {
                    userInfo.Jibie = "普通会员";
                }
                userInfo.TuiCount = user.TuiCount;
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
        /// 获取一前台用用户信息
        /// 参数id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet]
        public object GetUserInfoById(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var id = "";
            if (JObject.Parse(opc["where"].ToString())["Id"] != null)
            {
                id = JObject.Parse(opc["where"].ToString())["Id"].ToString();
            }
            SysUserModel model = new SysUserModel();
            model = m_BLL.GetById(id);
            if (model != null)
            {
                SysUserInfo userInfo = new SysUserInfo();
                userInfo.Id = model.Id;
                userInfo.UserName = model.UserName;
                userInfo.TrueName = model.TrueName;
                userInfo.MobileNumber = model.MobileNumber;
                userInfo.Token = "";// Token;
                userInfo.Card = model.Card;
                userInfo.shfzh = model.IdentityCardFile;
                userInfo.State = model.State;
                userInfo.IsAuth = (model.IsAuth != null) ? model.IsAuth : false;
                if (mj_BLL.GetRefSysJiaPu(model.Id) != null)
                {
                    userInfo.TId = mj_BLL.GetRefSysJiaPu(model.Id).TId;
                    userInfo.TName = mj_BLL.GetRefSysJiaPu(model.Id).TName;
                    userInfo.PId = mj_BLL.GetRefSysJiaPu(model.Id).ParentId;
                    userInfo.PName = mj_BLL.GetRefSysJiaPu(model.Id).ParentName;
                    userInfo.FrozenMoney = mj_BLL.GetRefSysJiaPu(model.Id).FrozenMoney;
                }
                if (mj_BLL.GetRefSysJiaPu(model.Id) != null && islBll.GetById(mj_BLL.GetRefSysJiaPu(model.Id).LevelId.ToString()) != null)
                {
                    userInfo.Jibie = islBll.GetById(mj_BLL.GetRefSysJiaPu(model.Id).LevelId.ToString()).Name;
                }
                else
                {
                    userInfo.Jibie = "普通会员";
                }
                userInfo.TuiCount = model.TuiCount;
                return Json(userInfo);
            }
            else
            {
                return null;
            }

        }
        [HttpPost]
        public object DeletAddress([FromBody]SysAddressModel model)
        {
            bool result = false;
            RetBool rt = new RetBool();
            if (!string.IsNullOrEmpty(model.Id))
            {
                result = ma_BLL.Delete(ref errors, model.Id);
            }
            rt.ret = result;
            return Json(rt);
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
            if (!string.IsNullOrEmpty(info.Id) && ma_BLL.GetById(info.Id) != null)
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
            if (JObject.Parse(opc["where"].ToString())["userId"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["userId"].ToString();
            }

            List<SysAddressModel> list = ma_BLL.GetPage(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

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
            string id = JObject.Parse(opc["where"].ToString())["Id"].ToString();
            List<SysAddressModel> list = ma_BLL.GetPage(queryStr, id, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()), true);
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
            AddressModel.TrueName = list[0].TrueName;

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
            if (!string.IsNullOrEmpty(sysJiaPu.uid))
            {
                List<SysJiaPuBefore> before = new List<SysJiaPuBefore>();
                before = mjb_BLL.IsInSysJiaPuBefore(sysJiaPu.uid);
                if (before != null)
                {
                    return null;
                }

                if (sysJiaPu.tid.Length > 28 && sysJiaPu.fje > 0)
                {
                    SysJiaPuBeforeModel model = new SysJiaPuBeforeModel()
                    {
                        Id = ResultHelper.NewId,
                        uid = sysJiaPu.uid,
                        tid = sysJiaPu.tid,
                        fje = sysJiaPu.fje,
                        zmp15 = sysJiaPu.zmp15,
                        createTime = DateTime.Now,
                        isdone = false
                    };

                    result_bool = mjb_BLL.Create(ref errors, model);
                }
                RetBool ri = new RetBool();
                ri.ret = result_bool;
                return Json(ri);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public object UpdateJiaPuBefore([FromBody]SysJiaPuBeforeModel sysJiaPu)
        {

            if (!string.IsNullOrEmpty(sysJiaPu.uid) && sysJiaPu.tid.Length > 28 && sysJiaPu.fje > 0)
                result_bool = mjb_BLL.Edit(ref errors, sysJiaPu);
            RetBool ri = new RetBool();
            ri.ret = result_bool;
            return Json(ri);
        }
        [HttpPost]
        public object EditJiaPu([FromBody]SysJiaPu model)
        {
            SysJiaPuModel newmodel = new SysJiaPuModel();
            SysJiaPuModel sysJia = new SysJiaPuModel();
            sysJia = mj_BLL.GetRefSysJiaPu(model.UserId);
            if (sysJia != null)
            {
                newmodel.FirstJinE = model.FirstJinE;
                newmodel.UpdateTime = DateTime.Now;
                newmodel.LevelId = model.LevelId;
                newmodel.Id = sysJia.Id;
                newmodel.ParentId = sysJia.ParentId;
                newmodel.PPId = sysJia.PPId;
                newmodel.TopId = sysJia.TopId;
                newmodel.ShuZi = sysJia.ShuZi;
                newmodel.ZiMu = sysJia.ZiMu;
                newmodel.ErZiShu = sysJia.ErZiShu;
                newmodel.Comment = sysJia.Comment;
                newmodel.CreateTime = sysJia.CreateTime;
                newmodel.UserId = sysJia.UserId;
                newmodel.ZMP15 = sysJia.ZMP15;
                newmodel.ZMPA2 = sysJia.ZMPA2;
                bool ret;
                ret = mj_BLL.Edit(ref errors, newmodel);
                if (ret)
                {
                    return Json(model);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
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
            RetInt ri = new RetInt();

            string ZMP15 = sysJiaPu_.zmp15 == null ? "Y2" : sysJiaPu_.zmp15;
            SysJiaPuModel sysJiaPu = new SysJiaPuModel();
            if (string.IsNullOrEmpty(sysJiaPu_.uid))
            {
                return null;
            }
            sysJiaPu = mj_BLL.GetRefSysJiaPu(sysJiaPu_.uid);
            if (sysJiaPu != null)
            {
                return null;
            }
            if (sysJiaPu_.tid != null)
            {
                sysJiaPu = mj_BLL.GetRefSysJiaPu(sysJiaPu_.tid);
                int loc;
                char[] z = ZMP15.ToCharArray();
                if (z[1] == '2')
                {
                    return null;
                }
                loc = z[0] - 'A';
                char[] ZM = sysJiaPu.ZMP15.Substring(loc, 2).ToCharArray();

                if (z[1] == '0')
                {
                    if (ZM[0] == '1')
                    {
                        return null;
                    }
                }
                if (z[1] == '1')
                {
                    if (ZM[1] == '1')
                    {
                        return null;
                    }
                }
                #region 自动匹配位置
                /*
                                if (sysJiaPu.ZMP15 != null)
                                {
                                    Regex r = new Regex("0");
                                    Match m = r.Match(sysJiaPu.ZMP15);
                                    if (m.Success)
                                    {
                                        switch (m.Index)
                                        {
                                            case 0:
                                                ZMP15 = "A0";
                                                break;
                                            case 1:
                                                ZMP15 = "A1";
                                                break;
                                            case 3:
                                                ZMP15 = "B0";
                                                break;
                                            case 4:
                                                ZMP15 = "B1";
                                                break;
                                            case 5:
                                                ZMP15 = "C0";
                                                break;
                                            case 6:
                                                ZMP15 = "C1";
                                                break;
                                            case 7:
                                                ZMP15 = "D0";
                                                break;
                                            case 8:
                                                ZMP15 = "D1";
                                                break;
                                            case 9:
                                                ZMP15 = "E0";
                                                break;
                                            case 10:
                                                ZMP15 = "E1";
                                                break;
                                            case 11:
                                                ZMP15 = "F0";
                                                break;
                                            case 12:
                                                ZMP15 = "F1";
                                                break;
                                            case 13:
                                                ZMP15 = "G0";
                                                break;
                                            case 14:
                                                ZMP15 = "G1";
                                                break;
                                            case 15:
                                                ZMP15 = "H0";
                                                break;
                                            case 16:
                                                ZMP15 = "H1";
                                                break;
                                            case 17:
                                                ZMP15 = "I0";
                                                break;
                                            case 18:
                                                ZMP15 = "I1";
                                                break;
                                            case 19:
                                                ZMP15 = "J0";
                                                break;
                                            case 20:
                                                ZMP15 = "J1";
                                                break;
                                            case 21:
                                                ZMP15 = "K0";
                                                break;
                                            case 22:
                                                ZMP15 = "K1";
                                                break;
                                            case 23:
                                                ZMP15 = "L0";
                                                break;
                                            case 24:
                                                ZMP15 = "L1";
                                                break;
                                            case 25:
                                                ZMP15 = "M0";
                                                break;
                                            case 26:
                                                ZMP15 = "M1";
                                                break;
                                            case 27:
                                                ZMP15 = "N0";
                                                break;
                                            case 28:
                                                ZMP15 = "N1";
                                                break;
                                            case 29:
                                                ZMP15 = "O0";
                                                break;
                                            case 30:
                                                ZMP15 = "O1";
                                                break;
                                            case 31:
                                                ZMP15 = "P0";
                                                break;
                                            case 32:
                                                ZMP15 = "P1";
                                                break;
                                            case 33:
                                                ZMP15 = "Q0";
                                                break;
                                            case 34:
                                                ZMP15 = "Q1";
                                                break;
                                            case 35:
                                                ZMP15 = "R0";
                                                break;
                                            case 36:
                                                ZMP15 = "R1";
                                                break;
                                            case 37:
                                                ZMP15 = "S0";
                                                break;
                                            case 38:
                                                ZMP15 = "S1";
                                                break;
                                            case 39:
                                                ZMP15 = "T0";
                                                break;
                                            case 40:
                                                ZMP15 = "T1";
                                                break;
                                            case 41:
                                                ZMP15 = "U0";
                                                break;
                                            case 42:
                                                ZMP15 = "U1";
                                                break;
                                            case 43:
                                                ZMP15 = "V0";
                                                break;
                                            case 44:
                                                ZMP15 = "V1";
                                                break;
                                            case 45:
                                                ZMP15 = "W0";
                                                break;
                                            case 46:
                                                ZMP15 = "W1";
                                                break;
                                            case 47:
                                                ZMP15 = "X0";
                                                break;
                                            case 48:
                                                ZMP15 = "X1";
                                                break;
                                            case 49:
                                                ZMP15 = "Y0";
                                                break;
                                            case 50:
                                                ZMP15 = "Y1";
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        ZMP15 = "A0";
                                    }
                                }
                                */
                #endregion

                if (!string.IsNullOrEmpty(sysJiaPu_.uid) && sysJiaPu_.tid.Length > 28)
                    ri.ret = mj_BLL.IntoSysJiaPu(sysJiaPu_.uid, sysJiaPu_.tid, sysJiaPu_.tid, ZMP15, sysJiaPu_.fje);
                //更新jiapubefore表
                if (ri.ret == 0)
                {
                    if (sysJiaPu_.Id != null)
                    {
                        //UpdateJiaPuBefore()
                        SysJiaPuBeforeModel sysJiaPuB = new SysJiaPuBeforeModel();
                        SysJiaPuBeforeModel sysJiaPuBefore = new SysJiaPuBeforeModel();
                        sysJiaPuBefore = mjb_BLL.GetById(sysJiaPu_.Id);
                        if (sysJiaPuBefore!=null)
                        {
                            sysJiaPuB.Id = sysJiaPuBefore.Id;
                            sysJiaPuB.uid = sysJiaPu_.uid;
                            sysJiaPuB.tid = sysJiaPu_.tid;
                            sysJiaPuB.fje = sysJiaPu_.fje;
                            sysJiaPuB.zmp15 = ZMP15;
                            sysJiaPuB.isdone = true;
                            sysJiaPuB.createTime = sysJiaPuBefore.createTime;                            
                        }
                        

                        bool res = mjb_BLL.Edit(ref errors, sysJiaPuB);
                        if (res)
                        {
                           
                        }
                    }
                    return Json(ri);
                }
                else
                {
                    return null;
                }

            }
            else
            {
                ri.ret = mj_BLL.IntoSysJiaPu(sysJiaPu_.uid, null, null, ZMP15, sysJiaPu_.fje);
                if (ri.ret == 0)
                {
                    return Json(ri);
                }
                else
                {
                    return null;
                }
            }
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
            if (mj_BLL.GetRefSysJiaPu(queryStr) != null)
            {
                return Json(mj_BLL.GetRefSysJiaPu(queryStr));
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
            jpb = mjb_BLL.GetSysJiaPuBefore(queryStr);
            if (jpb != null && jpb.Count > 0)
            {
                foreach (SysJiaPuBefore item in jpb)
                {
                    SysUserForJiaPu u = new SysUserForJiaPu();
                    u.Id = item.Id;
                    u.uid = item.uid;
                    u.tid = item.tid;
                    SysUserModel user = new SysUserModel();
                    user = m_BLL.GetById(item.uid);
                    if (user != null)
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
        public object GetSons(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["Id"] != null)
            {

                queryStr = JObject.Parse(opc["where"].ToString())["Id"].ToString();
            }
            int skip, limit;
            skip = int.Parse(opc["skip"].ToString());
            limit = int.Parse(opc["limit"].ToString());
            List<SysSons> sonUsers = new List<SysSons>();
            sonUsers = mj_BLL.GetAllSon(queryStr, skip, limit);
            if (sonUsers != null)
            {
                return Json(sonUsers);
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
            childUsers = mj_BLL.GetAllChildren(queryStr);
            if (childUsers != null)
            {
                return Json(childUsers);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        public object PostTuiGuang([FromBody]SysUserForTui user)
        {
            SysTuiUserModel model = new SysTuiUserModel();
            model.Id = ResultHelper.NewId;
            model.UserId = user.uid;
            model.TuiId = user.tid;
            model.CreateTime = DateTime.Now;
            if (m_tuBLL.Create(ref errors, model))
            {
                //用户表TuiCount+1
                SysUserModel sysUser = new SysUserModel();
                sysUser = m_BLL.GetById(user.uid);
                if (sysUser != null)
                {
                    SysUserEditModel emodel = new SysUserEditModel();
                    emodel.Id = sysUser.Id;
                    emodel.UserName = sysUser.UserName;
                    emodel.TuiCount = sysUser.TuiCount + 1;
                    if (m_BLL.Edit(ref errors, emodel))
                    {

                    }
                }

                return Json(model);
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

