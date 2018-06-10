using Apps.Common;
using Apps.Models;
using Apps.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.BLL
{
    public partial class SysPurchaseHistoryBLL
    {
        public List<SysPurchaseHistoryModel> GetList(string queryStr)
        {
            IQueryable<SysPurchaseHistory> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
                                a => a.Id.Contains(queryStr)
                                || a.UserId.Contains(queryStr)
                                || a.SysUser.UserName.Contains(queryStr)
                                || a.SysUser.TrueName.Contains(queryStr)
                                || a.Froms.Contains(queryStr)

                                || a.Note.Contains(queryStr)

                                );
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            
            List<SysPurchaseHistoryModel> dataList = new List<SysPurchaseHistoryModel>();
            List<SysPurchaseHistory> list = queryData.ToList() ;
            foreach (var model in list)
            {
                SysPurchaseHistoryModel sysPurchase = new SysPurchaseHistoryModel()
                {
                    Id = model.Id,
                    UserId=model.UserId,
                    Froms=model.Froms,
                    ShouRu=model.ShouRu,
                    CreateTime=model.CreateTime,
                    IsShow=model.IsShow,
                    Note=model.Note,
                    TrueName=model.SysUser.TrueName,
                    UserName=model.SysUser.UserName

                };
                dataList.Add(sysPurchase);
            }

            return dataList;
            
        }
    }
}
