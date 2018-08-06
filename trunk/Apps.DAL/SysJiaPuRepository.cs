using Apps.Models;
using System.Linq;

namespace Apps.DAL
{
    public partial class SysJiaPuRepository
    {
        //获取家谱信息
        public SysJiaPu GetRefSysJiaPu(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                try
                {
                    return (from m in Context.SysJiaPu
                            where m.UserId == userId
                            select m) == null ? null : (from m in Context.SysJiaPu
                                                        where m.UserId == userId
                                                        select m).First();
                }
                catch (System.Exception ex)
                {
                    return null;
                }          
                
            }
            return null;
        }

        public int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE)
        {
            if (fJE <= 0)
            {
                fJE = 0;
            }
            Context.P_Sys_PutSon(userId, tid, pid, erbiao, fJE);

            return this.SaveChanges();
        }

        public IQueryable<P_GetRecursiveChildren_Result> P_GetRecursiveChildren(string uid)
        {
            if (!string.IsNullOrEmpty(uid))
            {
                return Context.P_GetRecursiveChildren(uid).AsQueryable(); ;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<P_GetAllSons_Result> GetAllSons(string queryStr)
        {
            if (!string.IsNullOrEmpty(queryStr))
            {
                return Context.P_GetAllSons(queryStr).AsQueryable(); 
            }
            else
            {
                return null;
            }
        }
    }
}
