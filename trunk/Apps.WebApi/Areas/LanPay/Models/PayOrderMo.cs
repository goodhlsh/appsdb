namespace Apps.WebApi.Areas.LanPay.Controllers
{
    public class PayOrderMo
    {
        public string openid { get; set; }

        public string order_name { get; set; }
        public string order_num { get; set; }
        public decimal order_price { get; set; }
        public int pay_channel { get; set; }
    }
}