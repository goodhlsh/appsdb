using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Apps.Common;
using Apps.IBLL;
using Apps.Models;
using Apps.Models.Spl;
using Apps.Models.Sys;
using Apps.Spl.IBLL;
using Apps.WebApi.Core;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.Ware.Controllers
{
    public class Spl_OrdersController : BaseApiController
    {
        [Dependency]
        public ISpl_OrdersBLL SplOdersBLL { get; set; }
        [Dependency]
        public ISpl_Order_WareBLL order_WareBLL { get; set; }
        [Dependency]
        public ISysUserBLL SysUserBLL { get; set; }
        [Dependency]
        public ISysAddressBLL SysAddressBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [HttpPost]
        public object PostOrder([FromBody]Spl_OrdersModel spl_Orders)
        {
            Spl_OrdersModel newmodel = new Spl_OrdersModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.Status = spl_Orders.Status;
            newmodel.DingDanKuan = spl_Orders.DingDanKuan;
            newmodel.AddressId = spl_Orders.AddressId;
            newmodel.CreateTime = DateTime.Now;
            newmodel.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff") + spl_Orders.UserId;
            newmodel.UserId = spl_Orders.UserId;
            newmodel.TrueName = SysUserBLL.GetById(spl_Orders.UserId).TrueName;

            SysAddressModel sysAddress = new SysAddressModel();
            sysAddress = SysAddressBLL.GetById(spl_Orders.AddressId);
            if (sysAddress != null)
            {
                newmodel.AddressName = sysAddress.Province + "省" + sysAddress.City + "市" + sysAddress.Street + '-' + sysAddress.House;
            }
            else
            {
                newmodel.AddressName = "";
            }
            // newmodel.spl_Wares = spl_Orders.Spl_Order_Ware.Where(a=>a.OrderID== newmodel.Id).ToList();
            bool ret = false;
            ret = SplOdersBLL.Create(ref errors, newmodel);
            if (ret)
            {
                //写入order-ware表
                List<Spl_Ware> wares = spl_Orders.spl_Wares;
                foreach (Spl_Ware item in wares)
                {
                    Spl_Order_WareModel order_Ware = new Spl_Order_WareModel();
                    order_Ware.Id = ResultHelper.NewId;
                    order_Ware.OrderID = newmodel.Id;
                    order_Ware.WaresId = item.Id;
                    order_Ware.Name = "订单:" + newmodel.OrderNo;
                    order_Ware.Amount = item.WareCount;
                    order_Ware.SumJinE = item.WareCount * item.Price;
                    order_WareBLL.Create(ref errors, order_Ware);

                }
                return Json(newmodel);
            }
            else
            {
                return null;
            }


        }
        [HttpPost]
        public object PostEditOrder([FromBody]Spl_OrderEditModel spl_OrderEdit)
        {
            RetBool retb = new RetBool();

            Spl_OrdersModel spl_OrdersModel = new Spl_OrdersModel();
            spl_OrdersModel = SplOdersBLL.GetById(spl_OrderEdit.Id);
            if (spl_OrdersModel != null)
            {
                spl_OrdersModel.Status = spl_OrderEdit.Status;
                retb.ret = SplOdersBLL.Edit(ref errors, spl_OrdersModel);
            }
            return Json(retb);
        }
        [HttpGet]
        public object GetOrderListByStatus(string filter)
        {
            List<Spl_OrdersModel> spl_s = new List<Spl_OrdersModel>();
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["status"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["status"].ToString();
            }
            if (queryStr == "0")
            {
                queryStr = "待付款";
            }
            if (queryStr == "1")
            {
                queryStr = "待发货";
            }
            if (queryStr == "2")
            {
                queryStr = "已完成";
            }
            spl_s = SplOdersBLL.GetListWithStatus(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(spl_s);

        }
        public object alipay ()
        {
            //请求网关(gateway)，应用id(app_id)，应用私钥(private_key)，编码格式(charset)，支付宝公钥(alipay_public_key)，签名类型(sign_type)
            //IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do", APPID, APP_PRIVATE_KEY, "json", "1.0", "RSA2", ALIPAY_PUBLIC_KEY, CHARSET, false);
            ////实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.open.public.template.message.industry.modify 
            //AlipayOpenPublicTemplateMessageIndustryModifyRequest request = new AlipayOpenPublicTemplateMessageIndustryModifyRequest();
            ////SDK已经封装掉了公共参数，这里只需要传入业务参数
            ////此次只是参数展示，未进行字符串转义，实际情况下请转义
            //request.BizContent = "{" +
            //"    \"primary_industry_name\":\"IT科技/IT软件与服务\"," +
            //"    \"primary_industry_code\":\"10001/20102\"," +
            //"    \"secondary_industry_code\":\"10001/20102\"," +
            //"    \"secondary_industry_name\":\"IT科技/IT软件与服务\"" +
            //"  }";
            //AlipayOpenPublicTemplateMessageIndustryModifyResponse response = client.execute(request);
            ////调用成功，则处理业务逻辑
            //if (response.isSuccess())
            //{
            //    //.....
            //}
            return null;
        }
    }
}
