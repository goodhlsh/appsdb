using Apps.IDAL;
using Apps.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
   public partial class SysJiaPuBeforeBLL
    {
        [Dependency]
        public ISysJiaPuBeforeRepository jpbRep { get; set; }
        
        //获取指定用户的家谱前信息
        public List<SysJiaPuBefore> GetSysJiaPuBefore(string tid)
        {
            var jiapuList = jpbRep.GetSysJiaPuBefore(tid);
            if (jiapuList != null && jiapuList.Count() > 0)
            {
                return jiapuList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<SysJiaPuBefore> IsInSysJiaPuBefore(string uid)
        {
            var jiapuList = jpbRep.IsInSysJiaPuBefore(uid);
            if (jiapuList != null && jiapuList.Count() > 0)
            {
                return jiapuList.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
