using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
   public partial class SysAddressModel
    {
        public override string Id { get; set; }

        public override string UserId { get; set; }

        public override string Name { get; set; }

        public override string Mobile { get; set; }

        public override string Province { get; set; }

        public override string City { get; set; }

        public override string Street { get; set; }

        public override string House { get; set; }

        public override string Typs { get; set; }

        public override bool? IsDefault { get; set; }

        public override System.DateTime? CreateTime { get; set; }

        public override System.DateTime? UpdateTime { get; set; }
        public string TrueName { get; set; }
    }
}
