using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace Apps.Spl.BLL
{
    public partial class Spl_Order_WareBLL
    {
        [Dependency]
        public ISpl_WareRepository Spl_WareBll { get; set; }
        [Dependency]
        public ISpl_Order_WareRepository Spl_Order_WareRepository { get; set; }
        public List<Spl_Order_WareModel> GetSpl_Order_WareModelsByOrderId(string queryStr, int skip, int limit)
        {
           
                List<Spl_Order_WareModel> orders = new List<Spl_Order_WareModel>();
            List<Spl_Order_Ware> _Orders = new List<Spl_Order_Ware>();
                IQueryable<Spl_Order_Ware> spl_Order_Wares = Spl_Order_WareRepository.GetList();
                if (!string.IsNullOrWhiteSpace(queryStr))
                {
                spl_Order_Wares = spl_Order_Wares.Where(a => a.OrderID == queryStr);
                }
                _Orders = spl_Order_Wares.OrderByDescending(a => a.OrderID).Skip(skip).Take(limit).ToList();
                foreach (Spl_Order_Ware item in _Orders)
                {
                Spl_Order_WareModel spl = new Spl_Order_WareModel();
                    spl.Id = item.Id;
                    spl.OrderID = item.OrderID;
                    spl.WaresId = item.WaresId;
                    spl.Name = item.Name;
                    spl.SumJinE = item.SumJinE;
                    spl.Amount = item.Amount;

                    spl.Thumbnail = Spl_WareBll.GetById(item.WaresId).Thumbnail;
                                        
                   
                    orders.Add(spl);
                }
                return orders;
            }
       
    }
}