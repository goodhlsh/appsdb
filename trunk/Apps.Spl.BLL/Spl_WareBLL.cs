﻿using Apps.Models;
using Apps.Models.Spl;
using Apps.Spl.IDAL;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Spl.BLL
{
    public partial class Spl_WareBLL
    {
        [Dependency]
        public ISpl_WareRepository m_Rep { get; set; }
        [Dependency]
        public ISpl_WareInfoRepository minfo_Rep { get; set; }
        [Dependency]
        public ISpl_ActivesRepository ma_Rep { get; set; }

        public List<Spl_Ware> GetPage(string queryStr, int skip, int limit)
        {
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.ProductCategoryId == queryStr);
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<Spl_WareInfo> GetPage(bool queryStr, int skip, int limit)
        {
            List<Spl_WareInfo> query = null;
            IQueryable<Spl_WareInfo> list = minfo_Rep.GetList();
            list = list.Where(a => a.ToTop == queryStr);
            query = list.OrderBy(c => c.UpdateTime).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<Spl_Ware> GetPageLike(string queryStr, int skip, int limit)
        {
            List<Spl_Ware> query = null;
            IQueryable<Spl_Ware> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.Name.Contains(queryStr));
            }
            query = list.OrderBy(c => c.Name).Skip(skip).Take(limit).ToList();
            return query;
        }
        public List<Spl_Actives> GetPageActive(string queryStr, int skip, int limit)
        {
            List<Spl_Actives> query = null;
            IQueryable<Spl_Actives> list = ma_Rep.GetList();
           
            query = list.OrderBy(c => c.UpdateTime).Skip(skip).Take(limit).ToList();
            return query;
        }
    }
}
