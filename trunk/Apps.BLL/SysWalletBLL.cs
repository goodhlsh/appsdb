using Apps.Common;
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
    public partial class SysWalletBLL
    {
        [Dependency]
        public ISysWalletRepository sysWRep { get; set; }

        public SysWalletModel GetWallByUserID(string userID)
        {
            IQueryable<SysWallet> sw = sysWRep.GetWallByUserID(userID);
            if (sw == null || sw.Count() <= 0) return null;
            SysWallet s = sw.First<SysWallet>();
            SysWalletModel sm = new SysWalletModel();
            sm.Id = s.Id;
            sm.Balance = s.Balance;
            sm.CreateTime = s.CreateTime;
            sm.Froms = s.Froms;
            sm.JieYu = s.JieYu;
            sm.Note = s.Note;
            sm.UpdateTime = s.UpdateTime;
            sm.UserId = s.UserId;
            sm.ShunXu = s.ShunXu;
            sm.TrueName = s.SysUser.TrueName;


            return sm;
        }
        public List<P_Sys_GetUserWallet_Result> GetUserWallet()
        {
            return m_Rep.GetUserWallet();
        }        
    }
}
