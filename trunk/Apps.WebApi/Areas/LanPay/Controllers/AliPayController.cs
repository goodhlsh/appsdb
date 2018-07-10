using OSS.PaySdk.Ali;
using OSS.PaySdk.Ali.Pay;
using OSS.PaySdk.Ali.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.LanPay.Controllers
{
    public class AliPayController : ApiController
    {
        private static string returnUrl = "你的域名/alipay/return_url";
        private static string receiveUrl = "你的域名/alipay/receive";
        protected static ZPayConfig ZPayConfig { get; set; } = new ZPayConfig() {
         AppId= "2018062660423192",
         AppPrivateKey= "MIIEpAIBAAKCAQEA1qt1Htd3P778vKxCmiJ3P0WdSVX5stMAosQC2N5+nXJlzzsAU/Xf8P6ad1vJbG9dEryfRYuBMLKX4d4DIidIeSkMnYCNugR9jO0leFcEajSFxBikKXi+ydko91BlIYFHoVKV4fkc3lH8kfYy/SP2c4efUZ0453XBsNT5vSXad2xHV2E3ebzRNLUar7GTkyTaqNJjLu3yGgTKlgl0s7hG1CtlFYPPMc4DAbAH7kvc54boR9+E3YG1SnNEaYshCejZ8Ri08uKQUG1MfqjggYc84L0tLMJQlFBDygQKOnOHby2feq93TBwtmWAscs6NIHJ5YG0jvbL9GFmIsnbTEGH+XwIDAQABAoIBAQCYAitaHorzmcMPuw8iY/t600mwL3A0P5O4rgGyHcMcrHAJUSdHTNk5pqAsmtNDtIv8g5JoxYNCv4QrTXF8ksKQARBMy0YQjbN1wkuBLmUS64Q5OpWsRDMdbWfD0Kr8EbZuNrOpoyPdDcNCfErY08llP4kCWsl7pPf1xqndVVugqcDIx2WADcfEMELM5N2pc8vfo6N++D8CrN46rKHd9MHygyXJFza/1xoSjhGc155QieF/JyyF/2BUDJ5hozJEoO8rednq0jCAd2oHxbhtm2dksWjTI8ms7RmmvjRNS1a7jzpyGKLqxhacHkrc2gww2nlonmW2JDxEQNisJyH5zgiBAoGBAO0x5dgP1W3UHaOZlYZZqICcbNdsbfd/VM9i1nl8fPKv/D2DBNoQ9vv7eMsHzZhXtmLnvrXuVkbdK8FiWZsqFMInSoitcRK7bgBoGF9rmP9jlpzLf17QLb/BTtzu649Zd6YEkAKwwbEfM1tLN82hyy3JHcYWxS7aru0dpeDqhlCfAoGBAOewY76QUY7YxwtVX6QinLhh05NTLiHPTyK9Fov39eqOR+Ciq4fvqXfIFWvfghhoVolUoNW+/tLpU76uXtzit8zlME7EZbpQZrMW6dMoMNyjAV0t2cfU/RsVz1uJTkzmcOGjyCAJvJ2TFnQenqLhtT6tN30uTrL4d81ROBzxzLpBAoGBAL2rpvXtQ2f9tG28RJsYWuvtKgPhitXclj739DVVXLzcCUJO4LRX2IiLAH6qELd/fDL+ybvFiGxGM9UCBlFThyHyNWGx/7dQ9ZeRpdu3uLKQOkHLYGC667poo917mBYbHtg80cO1AE3Ye0LHlSnz9Pr51bsvPiJHcJXfnWOLZjhVAoGABPz8OMccqmmqZ2kQJRWFsEaS6pIY5St0dbgCe0L8bW9gxspZzRRw3p5VL7xhLatZaZ2D1PZGwD27ytgwPKs479VjY97AnfFLNMHiiORNoQJ1bg8lqDLCvEmM7FiZcfhoJ8OB9IahI/ddvWRHYBRh9ZZ/IlfT4/CLi8Ua4yyljsECgYBBQFAVdNMuBqsUZWk5fHPKYC9rEWASs2hBTXjqEOMiV44MIzzUKvHAPYS6V1v6o8kCBSs0hHmoVqyeswUy8MFnslTzcYMqc2P/VAv91aRUHprXd1jsgNad8skHWz+Kt674Xztl/bZaLLNxqn8eyY2R0t4SjWrBQE2Wu6QIKt/A1g==",
         AppPublicKey= "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA1qt1Htd3P778vKxCmiJ3P0WdSVX5stMAosQC2N5+nXJlzzsAU/Xf8P6ad1vJbG9dEryfRYuBMLKX4d4DIidIeSkMnYCNugR9jO0leFcEajSFxBikKXi+ydko91BlIYFHoVKV4fkc3lH8kfYy/SP2c4efUZ0453XBsNT5vSXad2xHV2E3ebzRNLUar7GTkyTaqNJjLu3yGgTKlgl0s7hG1CtlFYPPMc4DAbAH7kvc54boR9+E3YG1SnNEaYshCejZ8Ri08uKQUG1MfqjggYc84L0tLMJQlFBDygQKOnOHby2feq93TBwtmWAscs6NIHJ5YG0jvbL9GFmIsnbTEGH+XwIDAQAB",
        };

        private static readonly ZPayTradeApi _api = new ZPayTradeApi(ZPayConfig);
        // 支付结果回调接收
        //[HttpPost]
        //public IActionResult receive(ZPayCallBackResp pay)
        //{
        //    var dics = Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString());
        //    var res = _api.CheckCallBackSign(dics);
        //    if (res.IsSuccess())
        //    {
        //        // do something with res
        //    }
        //    return Content("success");
        //}
        [HttpPost]
        public object GetAppTradeContent(PayOrderMo order)
        {  
            var payReq = new ZAddAppTradeReq
            {
                body = order.order_name,
                out_trade_no = order.order_num,
                total_amount = order.order_price,
                subject = order.order_name
            };
            var res = _api.GetAppTradeContent(payReq);
            return Json(res);
        }
        private static readonly ZPayRefundApi _refundApi = new ZPayRefundApi();
        /// <summary>
        /// 退款接口
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async System.Threading.Tasks.Task<IActionResult> refund(string orderId)
        //{
        //    var req = new ZPayRefundReq
        //    {
        //        out_trade_no = orderId,
        //        out_request_no = orderId,
        //        refund_amount = 0.01m
        //    };

        //    var refundRes = await _refundApi.RefunPayAsync(req);

        //    return Json(refundRes);
        //}
    }
}
