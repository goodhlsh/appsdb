using Apps.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Apps.Spl.BLL
{
   public partial class Spl_WareInfoBLL
    {
        public Spl_Ware GetRefWare(string id)
        {
          var warelist=  m_Rep.GetRefWare(id);
            if (warelist!=null)
            {
                foreach (var item in warelist)
                {
                    return new Spl_Ware() {
                        id = item.id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        OriginPrice=item.OriginPrice,
                        Stock=item.Stock,
                        Thumbnail=item.Thumbnail,
                        WareInfoId=item.WareInfoId,
                        ShowType=item.ShowType,
                        ProductCategoryId=item.ProductCategoryId                        
                };
                }
                
            }
            return null;

        }
    }
}