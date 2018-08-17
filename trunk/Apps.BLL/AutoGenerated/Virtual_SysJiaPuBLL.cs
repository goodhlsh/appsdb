
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
	public class Virtual_SysJiaPuBLL
	{
        [Dependency]
        public ISysJiaPuRepository m_Rep { get; set; }

		public virtual List<SysJiaPuModel> GetList(ref GridPager pager, string queryStr)
        {

            IQueryable<SysJiaPu> queryData = null;
            if (!string.IsNullOrWhiteSpace(queryStr))
            {
                queryData = m_Rep.GetList(
				
				a=>a.Id.Contains(queryStr)
				

				|| a.UserId.Contains(queryStr)
				

				|| a.ParentId.Contains(queryStr)
				

				|| a.PPId.Contains(queryStr)
				

				|| a.ZiMu.Contains(queryStr)
				

				|| a.ShuZi.Contains(queryStr)
				

				
				

				
				

				|| a.LevelId.Contains(queryStr)
				

				|| a.ZMP15.Contains(queryStr)
				

				|| a.ZMPA2.Contains(queryStr)
				

				|| a.ZMPB2.Contains(queryStr)
				

				|| a.ZMPC2.Contains(queryStr)
				

				|| a.ZMPD2.Contains(queryStr)
				

				|| a.ZMPE2.Contains(queryStr)
				

				|| a.ZMPF2.Contains(queryStr)
				

				|| a.ZMPG2.Contains(queryStr)
				

				|| a.ZMPH2.Contains(queryStr)
				

				|| a.ZMPI2.Contains(queryStr)
				

				|| a.ZMPJ2.Contains(queryStr)
				

				|| a.Comment.Contains(queryStr)
				

				
				

				
				

				|| a.TopId.Contains(queryStr)
				

				|| a.TId.Contains(queryStr)
				

				
				

				
				

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
        public virtual List<SysJiaPuModel> CreateModelList(ref IQueryable<SysJiaPu> queryData)
        {

            List<SysJiaPuModel> modelList = (from r in queryData
                                              select new SysJiaPuModel
                                              {

													Id = r.Id,

													UserId = r.UserId,

													ParentId = r.ParentId,

													PPId = r.PPId,

													ZiMu = r.ZiMu,

													ShuZi = r.ShuZi,

													ErZiShu = r.ErZiShu,

													FirstJinE = r.FirstJinE,

													LevelId = r.LevelId,

													ZMP15 = r.ZMP15,

													ZMPA2 = r.ZMPA2,

													ZMPB2 = r.ZMPB2,

													ZMPC2 = r.ZMPC2,

													ZMPD2 = r.ZMPD2,

													ZMPE2 = r.ZMPE2,

													ZMPF2 = r.ZMPF2,

													ZMPG2 = r.ZMPG2,

													ZMPH2 = r.ZMPH2,

													ZMPI2 = r.ZMPI2,

													ZMPJ2 = r.ZMPJ2,

													Comment = r.Comment,

													CreateTime = r.CreateTime,

													UpdateTime = r.UpdateTime,

													TopId = r.TopId,

													TId = r.TId,

													UpTimes = r.UpTimes,

													FrozenMoney = r.FrozenMoney,
          
                                              }).ToList();

            return modelList;
        }

        public virtual bool Create(ref ValidationErrors errors, SysJiaPuModel model)
        {
            try
            {
                SysJiaPu entity = m_Rep.GetById(model.Id);
                if (entity != null)
                {
                    errors.Add(Resource.PrimaryRepeat);
                    return false;
                }
                entity = new SysJiaPu();
               
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.ParentId = model.ParentId;

				entity.PPId = model.PPId;

				entity.ZiMu = model.ZiMu;

				entity.ShuZi = model.ShuZi;

				entity.ErZiShu = model.ErZiShu;

				entity.FirstJinE = model.FirstJinE;

				entity.LevelId = model.LevelId;

				entity.ZMP15 = model.ZMP15;

				entity.ZMPA2 = model.ZMPA2;

				entity.ZMPB2 = model.ZMPB2;

				entity.ZMPC2 = model.ZMPC2;

				entity.ZMPD2 = model.ZMPD2;

				entity.ZMPE2 = model.ZMPE2;

				entity.ZMPF2 = model.ZMPF2;

				entity.ZMPG2 = model.ZMPG2;

				entity.ZMPH2 = model.ZMPH2;

				entity.ZMPI2 = model.ZMPI2;

				entity.ZMPJ2 = model.ZMPJ2;

				entity.Comment = model.Comment;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;

				entity.TopId = model.TopId;

				entity.TId = model.TId;

				entity.UpTimes = model.UpTimes;

				entity.FrozenMoney = model.FrozenMoney;
  

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

		
       

        public virtual bool Edit(ref ValidationErrors errors, SysJiaPuModel model)
        {
            try
            {
                SysJiaPu entity = m_Rep.GetById(model.Id);
                if (entity == null)
                {
                    errors.Add(Resource.Disable);
                    return false;
                }
                              
				entity.Id = model.Id;

				entity.UserId = model.UserId;

				entity.ParentId = model.ParentId;

				entity.PPId = model.PPId;

				entity.ZiMu = model.ZiMu;

				entity.ShuZi = model.ShuZi;

				entity.ErZiShu = model.ErZiShu;

				entity.FirstJinE = model.FirstJinE;

				entity.LevelId = model.LevelId;

				entity.ZMP15 = model.ZMP15;

				entity.ZMPA2 = model.ZMPA2;

				entity.ZMPB2 = model.ZMPB2;

				entity.ZMPC2 = model.ZMPC2;

				entity.ZMPD2 = model.ZMPD2;

				entity.ZMPE2 = model.ZMPE2;

				entity.ZMPF2 = model.ZMPF2;

				entity.ZMPG2 = model.ZMPG2;

				entity.ZMPH2 = model.ZMPH2;

				entity.ZMPI2 = model.ZMPI2;

				entity.ZMPJ2 = model.ZMPJ2;

				entity.Comment = model.Comment;

				entity.CreateTime = model.CreateTime;

				entity.UpdateTime = model.UpdateTime;

				entity.TopId = model.TopId;

				entity.TId = model.TId;

				entity.UpTimes = model.UpTimes;

				entity.FrozenMoney = model.FrozenMoney;
 


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

      

        public virtual SysJiaPuModel GetById(string id)
        {
            if (IsExists(id))
            {
                SysJiaPu entity = m_Rep.GetById(id);
                SysJiaPuModel model = new SysJiaPuModel();
                              
				model.Id = entity.Id;

				model.UserId = entity.UserId;

				model.ParentId = entity.ParentId;

				model.PPId = entity.PPId;

				model.ZiMu = entity.ZiMu;

				model.ShuZi = entity.ShuZi;

				model.ErZiShu = entity.ErZiShu;

				model.FirstJinE = entity.FirstJinE;

				model.LevelId = entity.LevelId;

				model.ZMP15 = entity.ZMP15;

				model.ZMPA2 = entity.ZMPA2;

				model.ZMPB2 = entity.ZMPB2;

				model.ZMPC2 = entity.ZMPC2;

				model.ZMPD2 = entity.ZMPD2;

				model.ZMPE2 = entity.ZMPE2;

				model.ZMPF2 = entity.ZMPF2;

				model.ZMPG2 = entity.ZMPG2;

				model.ZMPH2 = entity.ZMPH2;

				model.ZMPI2 = entity.ZMPI2;

				model.ZMPJ2 = entity.ZMPJ2;

				model.Comment = entity.Comment;

				model.CreateTime = entity.CreateTime;

				model.UpdateTime = entity.UpdateTime;

				model.TopId = entity.TopId;

				model.TId = entity.TId;

				model.UpTimes = entity.UpTimes;

				model.FrozenMoney = entity.FrozenMoney;
 
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
