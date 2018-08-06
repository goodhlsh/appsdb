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
   public partial  class SysMessageBLL
    {
        [Dependency]
        public ISysMessageRepository sysMessageRepository { get; set; }
        public List<SysMessageModel> GetListWithCategory(string queryStr, string userId, int skip, int limit)
        {
            List<SysMessageModel> sysMes = new List<SysMessageModel>();
            List<SysMessage> _Messages = new List<SysMessage>();
            IQueryable<SysMessage> sysMessages = sysMessageRepository.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                sysMessages = sysMessages.Where(a => a.Category == queryStr || a.ToWho == userId);
            }
            _Messages = sysMessages.OrderByDescending(a => a.CreateTime).Skip(skip).Take(limit).ToList();
            foreach (SysMessage item in _Messages)
            {
                SysMessageModel sysMess = new SysMessageModel();
                sysMess.Id = item.Id;
                sysMess.Title = item.Title;
                sysMess.FromWho = item.FromWho;
                sysMess.ToWho = item.ToWho;
                sysMess.Category = item.Category;
                sysMess.Cont = item.Cont;
                sysMess.CreateTime = item.CreateTime;
                sysMess.UpdateTime = item.UpdateTime;
                sysMess.IsRead = item.IsRead;
                sysMes.Add(sysMess);
            }
            return sysMes;
        }
    }
}
