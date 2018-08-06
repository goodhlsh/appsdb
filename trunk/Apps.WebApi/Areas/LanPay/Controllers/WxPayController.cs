using OSS.PaySdk.Wx;
using OSS.PaySdk.Wx.Pay;
using OSS.Common.ComModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OSS.PaySdk.Wx.Pay.Mos;

namespace Apps.WebApi.Areas.LanPay.Controllers
{
    public class WxPayController : ApiController
    {
        // 声明配置
        private static WxPayCenterConfig config = new WxPayCenterConfig()
        {
            AppId = "wxbf083887994f7d4d",
            MchId = "1509392701",
            Key = "Lanjiyoupin18102161781lanjiyoupi",
            //AppSecret = "51c56b886b5be869567dd389b3e5d1d6",

            CertPassword = "1509392701",
            CertPath = @"C:\cert\apiclient_cert.p12"
        };
        
        private static readonly string _callBackDomain = "http://154.8.159.50:1693";
        //  调用示例
        private static readonly WxPayTradeApi m_Api = new WxPayTradeApi(config);
        [HttpPost]
        public async Task<object> GetAppPayInfo([FromBody]WxAddPayUniOrderReq orderReq)
        {
            var order = GetUniorder(orderReq.out_trade_no,orderReq.body,orderReq.total_fee);
            
            var orderRes = await m_Api.AddUniOrderAsync(order);
            if (!orderRes.IsSuccess()) return Json(orderRes);
            else return null;
            //var appPara = m_Api.GetAppClientParaResp(orderRes.prepay_id);
            //return Json(appPara);
        }
        public static WxAddPayUniOrderReq GetUniorder(string out_trade_no, string body,int total_fee)
        {
            return new WxAddPayUniOrderReq
            {
                notify_url = string.Concat(_callBackDomain, "/api/WxPay/WxCallBack"),
                body = body,
                device_info = "WEB",
                openid = "",
                out_trade_no = out_trade_no,

                spbill_create_ip = "192.168.1.1",
                total_fee = total_fee,
                trade_type = "APP"
            };            
        }
        /// <summary>
        /// 支付后回调
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object WxCallBack()
        {
            string strPayResult= Request.Content.ToString();
            
            var wxPayRes = m_Api.DecryptPayResult(strPayResult);
            //  do something with wxPayRes
            var returnXml = m_Api.GetCallBackReturnXml(new ResultMo());
            return Json(returnXml);
        }

        //  退款示例
        private static readonly WxPayRefundApi _refundApi = new WxPayRefundApi();
        public async Task<object> refund(string orderId)
        {
            var refundRes = await _refundApi.RefundOrderAsync(new WxPayRefundReq()
            {
                out_trade_no = orderId,
                total_fee = 1,
                refund_fee = 1,
                out_refund_no = orderId
            });

            return Json(refundRes);
        }
    }
}
