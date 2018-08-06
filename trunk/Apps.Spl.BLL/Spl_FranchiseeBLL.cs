using Apps.Models;
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
    public partial class Spl_FranchiseeBLL
    {
        [Dependency]
        public ISpl_FranchiseeRepository franchiseeRepository { get; set; }

        public List<Spl_FranchiseeModel> GetFranchiseeList(int skip, int limit)
        {
            List<Spl_FranchiseeModel> spl_Franchisees = new List<Spl_FranchiseeModel>();
            IQueryable<Spl_Franchisee> _Franchisees = franchiseeRepository.GetList();
            List<Spl_Franchisee> franchisees = new List<Spl_Franchisee>();
            if (_Franchisees != null && _Franchisees.Count() > 0)
            {
                franchisees = _Franchisees.OrderBy(a => a.FranchiseeName).Skip(skip).Take(limit).ToList();
            }
            else
            {
                return null;
            }
            foreach (var item in franchisees)
            {
                spl_Franchisees.Add(
                new Spl_FranchiseeModel()
                {
                    Id = item.Id,
                    FranchiseeName = item.FranchiseeName,
                    FranchiseeType = item.FranchiseeType,
                    Addr = item.Addr,
                    Area = item.Area,
                    CreateTime = item.CreateTime,
                    Tel = item.Tel,
                    Note = item.Note

                });
            }
            return spl_Franchisees;
        }
        
        public List<Spl_FranchiseeModel> GetFranchiseeListByQueryStr(string queryStr, int skip, int limit)
        {
            List<Spl_FranchiseeModel> spl_Franchisees = new List<Spl_FranchiseeModel>();
            IQueryable<Spl_Franchisee> _Franchisees = franchiseeRepository.GetList(
                a => a.FranchiseeName.Contains(queryStr)
                || a.FranchiseeType.Contains(queryStr)
                || a.Addr.Contains(queryStr)
                || a.Area.Contains(queryStr)
                || a.Tel.Contains(queryStr)
                );
            List<Spl_Franchisee> franchisees = new List<Spl_Franchisee>();
            if (_Franchisees != null && _Franchisees.Count() > 0)
            {
                franchisees = _Franchisees.OrderBy(a => a.FranchiseeName).Skip(skip).Take(limit).ToList();
            }
            else
            {
                return null;
            }
            foreach (var item in franchisees)
            {
                spl_Franchisees.Add(
                new Spl_FranchiseeModel()
                {
                    Id = item.Id,
                    FranchiseeName = item.FranchiseeName,
                    FranchiseeType = item.FranchiseeType,
                    Addr = item.Addr,
                    Area = item.Area,
                    CreateTime = item.CreateTime,
                    Tel = item.Tel,
                    Note = item.Note

                });
            }
            return spl_Franchisees;
        }

    }
}
