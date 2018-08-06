
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
	public class Virtual_SysFirstBLL
	{
        [Dependency]
        public ISysFirstRepository m_Rep { get; set; }

		public virtual List<SysFirstModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysFirst> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
				
				a=>a.Id.Contains(queryStr)
				

				|| a.UserId.Contains(queryStr)
				

				|| a.OrderId.Contains(queryStr)
				

				
				

				
				

				
				

				
				

				|| a.Note.Contains(queryStr)
				

				
				

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
        public virtual List<SysFirstModel> CreateModelList(ref IQueryable<SysFirst> queryData)
        {

            List<SysFirstModel> modelList = (from r in queryData
                                              select new SysFirstModel
                                              {

													Id = r.Id,

													UserId = r.UserId,

													OrderId = r.OrderId,

													JinE = r.JinE,

													CurrentFirst = r.CurrentFirst,

													CreateTime = r.CreateTime,

													UpdateTime = r.UpdateTime,

													Note = r.Note,

													ShunXu = r.ShunXu,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysFirstModel model)
        {
            try
            {
                SysFirst entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysFirst();
               
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.OrderId = model.OrderId;

				entity.JinE = model.JinE;

				entity.CurrentFirst = model.CurrentFirst;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;

				entity.Note = model.Note;

				entity.ShunXu = model.ShunXu;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysFirstModel model)
        {
            try
            {
                SysFirst entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.OrderId = model.OrderId;

				entity.JinE = model.JinE;

				entity.CurrentFirst = model.CurrentFirst;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;

				entity.Note = model.Note;

				entity.ShunXu = model.ShunXu;
 


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

      

        public virtual SysFirstModel GetById(string id)
        {
            if (IsExists(id))
            {
                SysFirst entity = m_Rep.GetById(id);
                SysFirstModel model = new SysFirstModel();
                              
				model.Id = entity.Id;

				model.UserId = entity.UserId;

				model.OrderId = entity.OrderId;

				model.JinE = entity.JinE;

				model.CurrentFirst = entity.CurrentFirst;

				model.CreateTime = entity.CreateTime;

				model.UpdateTime = entity.UpdateTime;

				model.Note = entity.Note;

				model.ShunXu = entity.ShunXu;
 
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
