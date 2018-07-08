using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Spl
{
   public partial class Spl_OrdersModel
    {
        [Display(Name = "ID")]
        public override string Id { get; set; }
        [Display(Name = "UserID")]
        public override string UserId { get; set; }
        [Display(Name = "用户姓名")]
        public override string TrueName { get; set; }
        [Display(Name = "订单编号")]
        public override string OrderNo { get; set; }
        [Display(Name = "订单状态")]
        public override string Status { get; set; }
        [Display(Name = "AddressId")]
        public override string AddressId { get; set; }
        [Display(Name = "收货地址")]
        public override string AddressName { get; set; }
        [Display(Name = "下单日期")]
        public override Nullable<System.DateTime> CreateTime { get; set; }

        public override Nullable<System.DateTime> UpdateTime { get; set; }
        [Display(Name = "订单商品")]
        public List<Spl_Ware> spl_Wares { get; set; }
        [Display(Name = "订单金额")]
        public override Nullable<decimal> DingDanKuan { get; set; }
        [Display(Name ="快递编号")]
        public override string OrderWuliu { get; set; }
    }

    public class Spl_OrderEditModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
