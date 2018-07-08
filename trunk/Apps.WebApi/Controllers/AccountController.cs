using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Sys;
using Apps.WebApi.Core;
using Microsoft.Practices.Unity;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace Apps.WebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {

        [Dependency]
        public IAccountBLL accountBLL { get; set; }
        [Dependency]
        public ISysLogBLL logBLL { get; set; }
        [Dependency]
        public ISysUserBLL isuBLL { get; set; }
        [Dependency]
        public ISysLevelsBLL islBll { get; set; }
        [HttpGet]
        public object Login(string filter)
        {
            //var anonymous = new { a = 0, b = String.Empty, c = new int[0], d = new Dictionary<string, int>() };
            var anonymous = new { userName = String.Empty, password = String.Empty };
            var ouser = JsonHandler.DeserializeAnonymousType(filter, anonymous);
            SysUser user = accountBLL.Login(ouser.userName, ValueConvert.MD5(ouser.password));

            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"));
            }
            else if (!Convert.ToBoolean(user.State))//被禁用
            {
                return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"));
            }
            FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, ouser.userName, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", ouser.userName, ouser.password),
                            FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var Token = FormsAuthentication.Encrypt(token);
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[ouser.userName] = Token;
            SysUserInfo userInfo = new SysUserInfo();
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
            if (isuBLL.GetRefSysJiaPu(user.Id)!=null&&islBll.GetById(isuBLL.GetRefSysJiaPu(user.Id).LevelId.ToString())!=null)
            {
                userInfo.Jibie = islBll.GetById(isuBLL.GetRefSysJiaPu(user.Id).LevelId.ToString()).Name;
            }
            else
            {
                userInfo.Jibie = "普通会员";
            }
            return Json(userInfo);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="passsword">密码</param>
        /// <returns></returns>
        [HttpGet]
        public object Login(string userName, string password)
        {
            SysUser user = accountBLL.Login(userName, ValueConvert.MD5(password));
            if (user == null)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"));
            }
            else if (!Convert.ToBoolean(user.State))//被禁用
            {
                return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"));
            }
            FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, userName, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", userName, password),
                            FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var Token = FormsAuthentication.Encrypt(token);
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[userName] = Token;
            return Json(JsonHandler.CreateMessage(1, Token));
        }

        [HttpPost]
        public object Login([FromBody]SysUserLogingData json)
        {
            try
            {
                SysUser user = accountBLL.Login(json.UserName, ValueConvert.MD5(json.Password));
                if (user == null)
                {
                    return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"));
                }
                else if (!Convert.ToBoolean(user.State))//被禁用
                {
                    return Json(JsonHandler.CreateMessage(0, "账户被系统禁用"));
                }
                SysUserModel UserInfo = new SysUserModel();
                UserInfo.UserName = user.UserName;
                UserInfo.Id = user.Id;
                UserInfo.Password = user.Password;
                UserInfo.IsAuth = user.IsAuth;
                UserInfo.MobileNumber = user.MobileNumber;
                FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, json.UserName, DateTime.Now,
                           DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", json.UserName, json.Password),
                           FormsAuthentication.FormsCookiePath);
                //返回登录结果、用户信息、用户验证票据信息
                var Token = FormsAuthentication.Encrypt(token);
                //将身份信息保存在session中，验证当前请求是否是有效请求
                HttpContext.Current.Session[json.UserName] = Token;
                UserInfo.OtherContact = Token;
                return Json(UserInfo);
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
