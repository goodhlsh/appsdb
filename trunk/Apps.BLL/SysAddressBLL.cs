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
   public partial class SysAddressBLL
    {
        [Dependency]
        public ISysAddressRepository m_Rep { get; set; }
        public List<SysAddressModel> GetPage(string queryStr, int skip, int limit)
        {
            List<SysAddressModel> query = new List<SysAddressModel>();
            IQueryable<SysAddress> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                list = list.Where(a => a.UserId == queryStr);
            }
            list=list.OrderBy(c => c.Name).Skip(skip).Take(limit);
            foreach (SysAddress item in list)
            {
                query.Add(new SysAddressModel() {
                    Id = item.Id,
                    IsDefault=item.IsDefault,
                    City=item.City,
                    CreateTime=item.CreateTime,
                    House=item.House,
                    Mobile=item.Mobile,
                    Name=item.Name,
                    Province=item.Province,
                    Street=item.Street,
                    TrueName=item.SysUser.TrueName,
                    Typs=item.Typs,
                    UpdateTime=item.UpdateTime,
                    UserId=item.UserId 
                });
            }
            
            return query;
        }
        public List<SysAddressModel> GetPage(string queryStr,string id, int skip, int limit,bool IsDefault)
        {
            List<SysAddressModel> query = new List<SysAddressModel>();
            IQueryable<SysAddress> list = m_Rep.GetList();
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                if (string.IsNullOrEmpty(id)) { list = list.Where(a => a.UserId == queryStr && a.IsDefault == IsDefault); }
                else
                {
                    list = list.Where(a => a.UserId == queryStr && a.Id == id && a.IsDefault == IsDefault);
                }                
            }
            list = list.OrderBy(c => c.Name).Skip(skip).Take(limit);
            foreach (SysAddress item in list)
            {
                query.Add(new SysAddressModel()
                {
                    Id = item.Id,
                    IsDefault = item.IsDefault,
                    City = item.City,
                    CreateTime = item.CreateTime,
                    House = item.House,
                    Mobile = item.Mobile,
                    Name = item.Name,
                    Province = item.Province,
                    Street = item.Street,
                    TrueName = item.SysUser.TrueName,
                    Typs = item.Typs,
                    UpdateTime = item.UpdateTime,
                    UserId = item.UserId
                });
            }
            return query;
        }
    }
}
