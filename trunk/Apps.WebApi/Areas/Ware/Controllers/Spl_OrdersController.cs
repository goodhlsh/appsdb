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
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="spl_Orders"></param>
        /// <returns></returns>
        [HttpPost]
        public object PostOrder([FromBody]Spl_OrdersModel spl_Orders)
        {
            Spl_OrdersModel newmodel = new Spl_OrdersModel();
            newmodel.Id = ResultHelper.NewId;
            newmodel.Status = spl_Orders.Status;
            newmodel.DingDanKuan = spl_Orders.DingDanKuan;
            newmodel.AddressId = spl_Orders.AddressId;
            newmodel.CreateTime = DateTime.Now;
            newmodel.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newmodel.UserId = spl_Orders.UserId;
            newmodel.TrueName = SysUserBLL.GetById(spl_Orders.UserId).TrueName;
            newmodel.Description = spl_Orders.Description;

            SysAddressModel sysAddress = new SysAddressModel();
            if (spl_Orders.AddressId != null)
            {
                sysAddress = SysAddressBLL.GetById(spl_Orders.AddressId);
            }
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
            if (ret)//充值和提现的均为空
            {
                if (spl_Orders.spl_Wares != null)
                {
                    //将商品信息写入order-ware表
                    List<Spl_Ware> wares = spl_Orders.spl_Wares;
                    foreach (Spl_Ware item in wares)
                    {
                        Spl_Order_WareModel order_Ware = new Spl_Order_WareModel();
                        order_Ware.Id = ResultHelper.NewId;
                        order_Ware.OrderID = newmodel.Id;
                        order_Ware.WaresId = item.Id;
                        order_Ware.Name = item.Name;// "订单:" + newmodel.OrderNo;
                        order_Ware.Amount = item.WareCount;
                        order_Ware.SumJinE = item.WareCount * item.Price;
                        order_WareBLL.Create(ref errors, order_Ware);
                    }
                    return Json(newmodel);
                }
                else
                {
                    return Json(newmodel);
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 编辑订单
        /// </summary>
        /// <param name="spl_OrderEdit"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取某状态下的我的订单
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
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
            var userId = "";
            if (JObject.Parse(opc["where"].ToString())["userId"] != null)
            {
                userId = JObject.Parse(opc["where"].ToString())["userId"].ToString();
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
            spl_s = SplOdersBLL.GetListWithStatus(queryStr, userId, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (spl_s != null)
            {
                return Json(spl_s);
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 获取某个订单
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetOrderById(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["orderId"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["orderId"].ToString();
            }
            Spl_OrdersModel model = SplOdersBLL.GetById(queryStr);
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
        /// 获取某订单的商品
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetOrderWaresById(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["orderId"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["orderId"].ToString();
            }
            List<Spl_Order_WareModel> spl_Order_Wares = order_WareBLL.GetSpl_Order_WareModelsByOrderId(queryStr, int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (spl_Order_Wares != null)
            {
                return Json(spl_Order_Wares);
            }
            else
            {
                return null;
            }
        }
    }
}
