using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
   public partial class SysPurchaseHistoryModel
    {
        public string UserName { get; set; }
        public string TrueName { get; set; }
    }
    public class SysAchieveModel
    {
        public decimal ShouRu { get; set; }
        public string Froms { get; set; }
        public DateTime CreateTime { get; set; }
        public string Note { get; set; }
    }
}
