using Apps.Models;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;

namespace Apps.Spl.BLL
{
    public partial class Spl_WareInfoBLL
    {        
        [Dependency]
        public ISpl_WareInfoRepository minfo_Rep { get; set; }
        
    }
}
