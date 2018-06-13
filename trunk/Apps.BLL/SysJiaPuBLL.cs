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
    public partial class SysJiaPuBLL
    {
        [Dependency]
        public ISysJiaPuRepository jpRep { get; set; }
        [Dependency]
        public ISysUserRepository mu_Rep { get; set; }
        public override List<SysJiaPuModel> GetList(ref GridPager pager, string queryStr)
        {
            List<SysJiaPu> query = null;
            IQueryable<SysJiaPu> list = m_Rep.GetList();
            pager.totalRows = list.Count();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.UserId.Contains(queryStr) || a.SysUser.TrueName.Contains(queryStr));
                pager.totalRows = list.Count();
            }
            else
            {
                pager.totalRows = list.Count();
            }
            if (pager.order == "desc")
            {
                if (pager.order == "UserId")
                {
                    query = list.OrderBy(c => c.UserId).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
                }
                else//createtime
                {
                    query = list.OrderBy(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
                }
            }
            else
            {
                if (pager.order == "UserId")
                {
                    query = list.OrderByDescending(c => c.UserId).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
                }
                else//createtime
                {
                    query = list.OrderByDescending(c => c.CreateTime).Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
                }
            }

            List<SysJiaPuModel> jiapuInfoList = new List<SysJiaPuModel>();
            List<SysJiaPu> dataList = query.ToList();
            foreach (var user in dataList)
            {
                SysUser sysUser = new SysUser();
                if (user.ParentId != null)
                {
                    sysUser = mu_Rep.GetById(user.ParentId);
                }


                SysJiaPuModel jiapuModel = new SysJiaPuModel()
                {
                    Id = user.Id,
                    UserId = user.UserId,
                    UserName = user.SysUser.UserName,
                    TrueName = user.SysUser.TrueName,
                    ParentId = user.ParentId,
                    LevelId = user.LevelId,
                    ParentName = sysUser == null ? null : sysUser.TrueName,
                    CreateTime = user.CreateTime,
                    FirstJinE = user.FirstJinE,
                    ErZiShu = user.ErZiShu,
                    ZMPA2 = user.ZMPA2,
                    TId = user.TId,
                    TName = user.SysUser.Recommendor
                };
                jiapuInfoList.Add(jiapuModel);
            }

            return jiapuInfoList;
        }

        public SysJiaPuRModel CreateTree(SysJiaPuRModel node, string id)
        {
            SysJiaPuRModel jiaPuRModel = new SysJiaPuRModel();
            List<SysJiaPu> list = m_Rep.GetList().Where(a=>a.ParentId==id).ToList();
        
            List<SysJiaPuModel> list_m = new List<SysJiaPuModel>();
            
            foreach (var item in list)
            {
                SysJiaPuModel jiaPuModel = new SysJiaPuModel();
                jiaPuModel.Id = item.Id;
                jiaPuModel.LevelId = item.LevelId;
                jiaPuModel.ParentId = item.ParentId;
                jiaPuModel.PPId = item.PPId;
                jiaPuModel.ShuZi = item.ShuZi;
                jiaPuModel.TId = item.TId;
                jiaPuModel.TopId = item.TopId;
                jiaPuModel.TrueName = item.SysUser.TrueName;
                jiaPuModel.UserId = item.UserId;
                jiaPuModel.UserName = item.SysUser.Id;
                jiaPuModel.Comment = item.Comment;
                jiaPuModel.CreateTime = item.CreateTime;
                jiaPuModel.ErZiShu = item.ErZiShu;
                jiaPuModel.FirstJinE = item.FirstJinE;
                jiaPuModel.ZiMu = item.ZiMu;
                jiaPuModel.ZMP15 = item.ZMP15;
                jiaPuModel.ZMPA2 = item.ZMPA2;
                list_m.Add(jiaPuModel);
            }


            for (int i = 0; i < list_m.Count(); i++)
            {
                SysJiaPuRModel nd = new SysJiaPuRModel();
                nd.ParentId = list_m[i].ParentId;
                nd.TrueName = list_m[i].TrueName;
                CreateTree(nd, list_m[i].ParentId);
                jiaPuRModel.sjp.Add(nd);
            }
            return jiaPuRModel;

        }
    }
}
