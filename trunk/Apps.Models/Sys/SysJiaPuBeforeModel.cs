using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
   public partial class SysJiaPuBeforeModel
    {
        public override string Id { get; set; }

        public override string uid { get; set; }

        public override string tid { get; set; }

        public override decimal fje { get; set; }

        public override Nullable<bool> isdone { get; set; }

        public override Nullable<System.DateTime> createTime { get; set; }
    }
}
