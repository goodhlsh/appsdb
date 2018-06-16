
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
	public class Virtual_SysLevelsBLL
	{
        [Dependency]
        public ISysLevelsRepository m_Rep { get; set; }

		public virtual List<SysLevelsModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysLevels> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
				
				a=>a.Id.Contains(queryStr)
				

				|| a.Name.Contains(queryStr)
				

				|| a.Code.Contains(queryStr)
				

				
				

				
				

				
				

				|| a.Other.Contains(queryStr)
				

				
				

				
				

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
        public virtual List<SysLevelsModel> CreateModelList(ref IQueryable<SysLevels> queryData)
        {

            List<SysLevelsModel> modelList = (from r in queryData
                                              select new SysLevelsModel
                                              {

													Id = r.Id,

													Name = r.Name,

													Code = r.Code,

													FirstDiscout = r.FirstDiscout,

													SecondDiscout = r.SecondDiscout,

													ThirdDiscout = r.ThirdDiscout,

													Other = r.Other,

													CreateTime = r.CreateTime,

													UpdateTime = r.UpdateTime,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysLevelsModel model)
        {
            try
            {
                SysLevels entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysLevels();
               
				entity.Id = model.Id;

				entity.Name = model.Name;

				entity.Code = model.Code;

				entity.FirstDiscout = model.FirstDiscout;

				entity.SecondDiscout = model.SecondDiscout;

				entity.ThirdDiscout = model.ThirdDiscout;

				entity.Other = model.Other;

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysLevelsModel model)
        {
            try
            {
                SysLevels entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              
				entity.Id = model.Id;

				entity.Name = model.Name;

				entity.Code = model.Code;

				entity.FirstDiscout = model.FirstDiscout;

				entity.SecondDiscout = model.SecondDiscout;

				entity.ThirdDiscout = model.ThirdDiscout;

				entity.Other = model.Other;

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

      

        public virtual SysLevelsModel GetById(string id)
        {
            if (IsExists(id))
            {
                SysLevels entity = m_Rep.GetById(id);
                SysLevelsModel model = new SysLevelsModel();
                              
				model.Id = entity.Id;

				model.Name = entity.Name;

				model.Code = entity.Code;

				model.FirstDiscout = entity.FirstDiscout;

				model.SecondDiscout = entity.SecondDiscout;

				model.ThirdDiscout = entity.ThirdDiscout;

				model.Other = entity.Other;

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
