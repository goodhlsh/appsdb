
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
using Apps.BLL.Core;
using Apps.Locale;
using Apps.Spl.IDAL;
using Apps.Models.Spl;
namespace Apps.Spl.BLL
{
	public class Virtual_Spl_Order_WareBLL
	{
        [Dependency]
        public ISpl_Order_WareRepository m_Rep { get; set; }

		public virtual List<Spl_Order_WareModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<Spl_Order_Ware> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
				
				a=>a.Id.Contains(queryStr)
				

				|| a.OrderID.Contains(queryStr)
				

				|| a.WaresId.Contains(queryStr)
				

				|| a.Name.Contains(queryStr)
				

				
				

				
				

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
        public virtual List<Spl_Order_WareModel> CreateModelList(ref IQueryable<Spl_Order_Ware> queryData)
        {

            List<Spl_Order_WareModel> modelList = (from r in queryData
                                              select new Spl_Order_WareModel
                                              {

													Id = r.Id,

													OrderID = r.OrderID,

													WaresId = r.WaresId,

													Name = r.Name,

													Amount = r.Amount,

													SumJinE = r.SumJinE,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, Spl_Order_WareModel model)
        {
            try
            {
			    Spl_Order_Ware entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new Spl_Order_Ware(); 

				entity.Id = model.Id;

				entity.OrderID = model.OrderID;

				entity.WaresId = model.WaresId;

				entity.Name = model.Name;

				entity.Amount = model.Amount;

				entity.SumJinE = model.SumJinE;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, Spl_Order_WareModel model)
        {
            try
            {
                Spl_Order_Ware entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              
				entity.Id = model.Id;

				entity.OrderID = model.OrderID;

				entity.WaresId = model.WaresId;

				entity.Name = model.Name;

				entity.Amount = model.Amount;

				entity.SumJinE = model.SumJinE;
 


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

      

        public virtual Spl_Order_WareModel GetById(string id)
        {
            if (IsExists(id))
            {
                Spl_Order_Ware entity = m_Rep.GetById(id);
                Spl_Order_WareModel model = new Spl_Order_WareModel();
                              
				model.Id = entity.Id;

				model.OrderID = entity.OrderID;

				model.WaresId = entity.WaresId;

				model.Name = entity.Name;

				model.Amount = entity.Amount;

				model.SumJinE = entity.SumJinE;
 
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
