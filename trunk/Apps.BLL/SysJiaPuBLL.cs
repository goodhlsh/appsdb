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
                    TName = mu_Rep.GetNameById(user.TId),
                    FrozenMoney=user.FrozenMoney,
                    ZMP15=user.ZMP15
                };
                jiapuInfoList.Add(jiapuModel);
            }

            return jiapuInfoList;
        }

        public SysJiaPuRModel CreateTree(SysJiaPuRModel node, string id)
        {
            SysJiaPuRModel jiaPuRModel = new SysJiaPuRModel();
            List<SysJiaPu> list = m_Rep.GetList().Where(a => a.ParentId == id).ToList();

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
                jiaPuModel.FrozenMoney = item.FrozenMoney;
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

        /// <summary>
        /// jiapu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pId"></param>
        /// <param name="fJE"></param>
        public int IntoSysJiaPu(string userId, string tid, string pid, string erbiao, decimal fJE)
        {
            return jpRep.IntoSysJiaPu(userId, tid, pid, erbiao, fJE);
        }
       
        //获取指定用户的家谱个人信息
        public SysJiaPuModel GetRefSysJiaPu(string userId)
        {
            SysJiaPuModel jiaPuModel = new SysJiaPuModel();
            SysJiaPu jiaPu = new SysJiaPu();
            jiaPu = jpRep.GetRefSysJiaPu(userId);
            if (jiaPu != null) //&&jiapuList.Count()>0)
            {
                jiaPuModel.Id = jiaPu.Id;
                jiaPuModel.LevelId = jiaPu.LevelId;
                jiaPuModel.ParentId = jiaPu.ParentId;
                jiaPuModel.ParentName = mu_Rep.GetById(jiaPu.ParentId) == null ? "" : mu_Rep.GetById(jiaPu.ParentId).TrueName;
                jiaPuModel.TrueName = jiaPu.SysUser.TrueName;
                jiaPuModel.UserId = jiaPu.UserId;
                jiaPuModel.UserName = mu_Rep.GetById(jiaPu.UserId) == null ? "" : mu_Rep.GetById(jiaPu.UserId).UserName;
                jiaPuModel.ZiMu = jiaPu.ZiMu;
                jiaPuModel.ZMP15 = jiaPu.ZMP15;
                jiaPuModel.ZMPA2 = jiaPu.ZMPA2;
                jiaPuModel.ErZiShu = jiaPu.ErZiShu;
                jiaPuModel.FirstJinE = jiaPu.FirstJinE;
                jiaPuModel.CreateTime = jiaPu.CreateTime;
                jiaPuModel.PPId = jiaPu.PPId;
                jiaPuModel.ShuZi = jiaPu.ShuZi;
                jiaPuModel.TId = jiaPu.TId;
                jiaPuModel.TName = mu_Rep.GetById(jiaPu.TId) == null ? "" : mu_Rep.GetById(jiaPu.TId).TrueName;
                jiaPuModel.TopId = jiaPu.TopId;
                jiaPuModel.ZMPB2 = jiaPu.ZMPB2;
                jiaPuModel.ZMPC2 = jiaPu.ZMPC2;
                jiaPuModel.ZMPD2 = jiaPu.ZMPD2;
                jiaPuModel.ZMPE2 = jiaPu.ZMPE2;
                jiaPuModel.ZMPF2 = jiaPu.ZMPF2;
                jiaPuModel.ZMPG2 = jiaPu.ZMPG2;
                jiaPuModel.ZMPH2 = jiaPu.ZMPH2;
                jiaPuModel.ZMPI2 = jiaPu.ZMPI2;
                jiaPuModel.ZMPJ2 = jiaPu.ZMPJ2;
                jiaPuModel.FrozenMoney = jiaPu.FrozenMoney;

                return jiaPuModel;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取所有家族信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public List<SysAllChildUser> GetAllChildren(string uid)
        {
            List<SysAllChildUser> modelList = new List<SysAllChildUser>();
            IQueryable<P_GetRecursiveChildren_Result> queryData = jpRep.P_GetRecursiveChildren(uid);
            if (queryData != null)
            {
                modelList = (from r in queryData
                             select new SysAllChildUser
                             {
                                 userId = r.UserId,
                                 trueName = r.TrueName,
                                 parentId = r.ParentId,
                                 levelName = r.LevelId == "3" ? "8800会员" : ((r.LevelId == "2" ? "1314会员" : (r.LevelId == "1" ? "398会员" : "普通会员"))),
                                 levelMan = r.LevelMan == null ? 0 : (int)r.LevelMan,
                                 levelMax = r.LevelMax
                             }).ToList();
            }
            return modelList;
        }
        /// <summary>
        /// 获取儿子们的实际位置 
        /// 表示形式'2_1'
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> GetRealSon(string userID)
        {
            List<string> realSon = new List<string>();
            char[,] sons = new char[25, 2];
            sons = GetZmp15(userID);
            if (sons.Length>0)
            {
                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (sons[i,j]=='1')
                        {
                            realSon.Add((i+1).ToString()+"_"+ (j+1).ToString());
                        }
                    }
                }
            }
            return realSon;
        }
        /// <summary>
        /// 获取所有儿子的位置信息，二维数组格式
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public char[,] GetZmp15(string userID)
        {
            char[,] sons = new char[25, 2];
            SysJiaPu model = new SysJiaPu();

            model = m_Rep.GetRefSysJiaPu(userID);

            if (model != null)
            {
                char[] zmp15 = model.ZMP15.ToCharArray();

                for (int m = 0, j = 0; j < 50;)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (k == 0)
                        {
                            sons[m, k] = zmp15[j];
                        }
                        else
                        {
                            sons[m, k] = zmp15[j + 1];
                        }
                    }
                    m = m + 1;
                    j = j + 2;
                }
            }
            return sons;
        }
        /// <summary>
        /// 获取所有儿子的组别及该组左右护法姓名
        /// </summary>
        /// <param name="queryStr"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<SysSons> GetAllSon(string queryStr, int skip, int limit)
        {

            List<SysSons> sons = new List<SysSons>();
            List<P_GetAllSons_Result> sons_Result = new List<P_GetAllSons_Result>();
            IQueryable<P_GetAllSons_Result> p_GetAllSons = m_Rep.GetAllSons(queryStr);
            if (p_GetAllSons != null)
            {
                sons_Result = (from r in p_GetAllSons
                               select new P_GetAllSons_Result
                               {
                                   userid = r.userid,
                                   TrueName = r.TrueName,
                                   ZiMu = r.ZiMu,
                                   ShuZi = r.ShuZi,
                                   LevelId = r.LevelId,
                                   jiapuid = r.jiapuid

                               }).Skip(skip).Take(limit).OrderBy(a => a.ZiMu).ToList();
                string zm, sh;
                int cout;
                
                StringBuilder builder = new StringBuilder();
                foreach (P_GetAllSons_Result item in sons_Result)
                {
                    zm = item.ZiMu.Substring(item.ZiMu.Length - 1);
                    builder.Append(zm);
                    builder.Append(",");
                }
                builder.Remove(builder.Length - 1, 1);
                string tmp;
                tmp= string.Join("", builder.ToString().Split(',').Distinct().ToArray());
                char[] tmpchar = tmp.ToArray();
                cout = tmp.Length;
                for (int i = 0; i < cout; i++)
                {

                    SysSons ss = new SysSons();
                    ss.zuname = tmpchar[i].ToString();

                    for (int j = i; j < sons_Result.Count; j++)
                    {
                        zm = sons_Result[j].ZiMu.Substring(sons_Result[j].ZiMu.Length - 1);
                        if (ss.zuname == zm)
                        {
                            if (sons_Result[j].ShuZi != null)
                            {
                                sh = sons_Result[j].ShuZi.Substring(sons_Result[j].ShuZi.Length - 1);
                                if (sh == "0")
                                {
                                    ss.leftTrueName = sons_Result[j].TrueName;
                                }
                                if (sh == "1")
                                {
                                    ss.rightTrueName = sons_Result[j].TrueName;
                                }
                                for (int k = j + 1; k < sons_Result.Count; k++)
                                {
                                    if (sons_Result[k].ZiMu.Substring(sons_Result[k].ZiMu.Length - 1) == zm)
                                    {
                                        if (sons_Result[k].ShuZi != null)
                                        {
                                            sh = sons_Result[k].ShuZi.Substring(sons_Result[k].ShuZi.Length - 1);
                                            if (sh == "0")
                                            {
                                                ss.leftTrueName = sons_Result[k].TrueName;
                                            }
                                            if (sh == "1")
                                            {
                                                ss.rightTrueName = sons_Result[k].TrueName;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    sons.Add(ss);
                }               
                return sons;
            }
            else
            {
                return null;
            }
        }
    }
}
