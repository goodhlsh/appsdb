using Apps.IDAL;
using Apps.Models;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
   public partial class SysFirstBLL
    {
        [Dependency]
        public ISysFirstRepository m_Rep { get; set; }
        public SysFirstModel GetCurrrentFirstByUserID(string userID)
        {
            IQueryable<SysFirst> sw = m_Rep.GetFirstByUserID(userID);
            if (sw == null || sw.Count() <= 0) return null;
            SysFirst s = sw.First<SysFirst>();
            SysFirstModel sm = new SysFirstModel();
            sm.Id = s.Id;
            sm.OrderId = s.OrderId;
            sm.CreateTime = s.CreateTime;
            sm.JinE = s.JinE;
            sm.CurrentFirst = s.CurrentFirst;
            sm.Note = s.Note;
            sm.UpdateTime = s.UpdateTime;
            sm.UserId = s.UserId;
            sm.ShunXu = s.ShunXu;
            sm.TrueName = s.SysUser.TrueName;


            return sm;
        }
    }
}
