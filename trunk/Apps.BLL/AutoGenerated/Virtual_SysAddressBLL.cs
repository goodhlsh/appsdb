
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using Apps.Models;
using Apps.Common;
using Microsoft.Practices.Unity;
using System.Transactions;
using Apps.IBLL;
using Apps.IDAL;
using Apps.BLL.Core;
using Apps.Locale;
using Apps.Models.Sys;
namespace Apps.BLL
{
	public class Virtual_SysAddressBLL
	{
        [Dependency]
        public ISysAddressRepository m_Rep { get; set; }

		public virtual List<SysAddressModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysAddress> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
				
				a=>a.Id.Contains(queryStr)
				

				|| a.UserId.Contains(queryStr)
				

				|| a.Name.Contains(queryStr)
				

				|| a.Mobile.Contains(queryStr)
				

				|| a.Province.Contains(queryStr)
				

				|| a.City.Contains(queryStr)
				

				|| a.Street.Contains(queryStr)
				

				|| a.House.Contains(queryStr)
				

				|| a.Typs.Contains(queryStr)
				

				
				

				
				

				
				

				);
            }
            else
            {
                queryData = m_Rep.GetList();
            }
            pager.totalRows = queryData.Count();
            //排序
            queryData = LinqHelper.SortingAndPaging(queryData, pager.sort, pager.order, pager.page, pager.rows);
            return CreateModelList(ref queryData);
        }
        public virtual List<SysAddressModel> CreateModelList(ref IQueryable<SysAddress> queryData)
        {

            List<SysAddressModel> modelList = (from r in queryData
                                              select new SysAddressModel
                                              {

													Id = r.Id,

													UserId = r.UserId,

													Name = r.Name,

													Mobile = r.Mobile,

													Province = r.Province,

													City = r.City,

													Street = r.Street,

													House = r.House,

													Typs = r.Typs,

													IsDefault = r.IsDefault,

													CreateTime = r.CreateTime,

													UpdateTime = r.UpdateTime,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysAddressModel model)
        {
            try
            {
                SysAddress entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysAddress();
               
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.Name = model.Name;

				entity.Mobile = model.Mobile;

				entity.Province = model.Province;

				entity.City = model.City;

				entity.Street = model.Street;

				entity.House = model.House;

				entity.Typs = model.Typs;

				entity.IsDefault = model.IsDefault;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;
  

                if (m_Rep.Create(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.InsertFail);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }



         public virtual bool Delete(ref ValidationErrors errors, string id)
        {
            try
            {
                if (m_Rep.Delete(id) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

        public virtual bool Delete(ref ValidationErrors errors, string[] deleteCollection)
        {
            try
            {
                if (deleteCollection != null)
                {
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        if (m_Rep.Delete(deleteCollection) == deleteCollection.Length)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                        else
                        {
                            Transaction.Current.Rollback();
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysAddressModel model)
        {
            try
            {
                SysAddress entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.Name = model.Name;

				entity.Mobile = model.Mobile;

				entity.Province = model.Province;

				entity.City = model.City;

				entity.Street = model.Street;

				entity.House = model.House;

				entity.Typs = model.Typs;

				entity.IsDefault = model.IsDefault;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;
 


                if (m_Rep.Edit(entity))
                {
                    return true;
                }
                else
                {
                    errors.Add(Resource.NoDataChange);
                    return false;
                }

            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                ExceptionHander.WriteException(ex);
                return false;
            }
        }

      

        public virtual SysAddressModel GetById(string id)
        {
            if (IsExists(id))
            {
                SysAddress entity = m_Rep.GetById(id);
                SysAddressModel model = new SysAddressModel();
                              
				model.Id = entity.Id;

				model.UserId = entity.UserId;

				model.Name = entity.Name;

				model.Mobile = entity.Mobile;

				model.Province = entity.Province;

				model.City = entity.City;

				model.Street = entity.Street;

				model.House = entity.House;

				model.Typs = entity.Typs;

				model.IsDefault = entity.IsDefault;

				model.CreateTime = entity.CreateTime;

				model.UpdateTime = entity.UpdateTime;
 
                return model;
            }
            else
            {
                return null;
            }
        }

        public virtual bool IsExists(string id)
        {
            return m_Rep.IsExist(id);
        }
		  public void Dispose()
        { 
            
        }

	}

}
