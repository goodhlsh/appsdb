using Apps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LShop.Models
{
    public class BrandViewModel
    {
        public List<Spl_Brand> BrandList;
        public Spl_Brand CurentBrand { get; set; }
    }
}