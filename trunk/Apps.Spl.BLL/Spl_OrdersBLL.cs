﻿using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Apps.Spl.BLL
{
    public partial class Spl_OrdersBLL
    {
        [Dependency]
        public ISpl_OrdersRepository spl_OrdersRepository { get; set; }
        [Dependency]
        public ISpl_Order_WareRepository Spl_Order_WareRepository { get; set; }
        public List<Spl_OrdersModel> GetListWithStatus(string queryStr,int skip,int limit)
        {
            List<Spl_OrdersModel> orders = new List<Spl_OrdersModel>();
            List<Spl_Orders> _Orders = new List<Spl_Orders>();
            IQueryable<Spl_Orders> spl_Orders = spl_OrdersRepository.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                spl_Orders = spl_Orders.Where(a => a.Status == queryStr);
            }
            _Orders = spl_Orders.OrderByDescending(a=>a.CreateTime).Skip(skip).Take(limit).ToList();
            foreach (Spl_Orders item in _Orders)
            {
                Spl_OrdersModel spl = new Spl_OrdersModel();
                spl.Id = item.Id;
                spl.OrderNo = item.OrderNo;
                spl.OrderWuliu = item.OrderWuliu;
                spl.Status = item.Status;
                spl.TrueName = item.TrueName;
                spl.UserId = item.UserId;
                spl.AddressId = item.AddressId;
                spl.AddressName = item.AddressName;
                spl.CreateTime = item.CreateTime;
                spl.DingDanKuan = item.DingDanKuan;
                spl.UpdateTime = item.UpdateTime;
                List<Spl_Ware> _Wares = new List<Spl_Ware>();
                List<Spl_Order_Ware> spl_Order_Wares = new List<Spl_Order_Ware>();
                spl_Order_Wares = Spl_Order_WareRepository.GetList(a=>a.OrderID==item.Id).ToList();
                foreach (Spl_Order_Ware p in spl_Order_Wares)
                {
                    Spl_Ware _Ware = new Spl_Ware();
                    _Ware = p.Spl_Ware;
                    if (_Ware!=null)
                    {
                        _Wares.Add(_Ware);
                    }                   
                }
                //spl.spl_Wares = _Wares;
                orders.Add(spl);
            }
            return orders;
        }
    }
}