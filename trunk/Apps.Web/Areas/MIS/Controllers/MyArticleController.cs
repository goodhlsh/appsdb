﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2013-04-25 15:26:22 by YmNets
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Apps.IBLL;
using Apps.Common;
using Apps.Models;
using Apps.Models.MIS;
using Apps.Web.Core;

using Apps.MIS.IBLL;
using Apps.Locale;
namespace Apps.Web.Areas.MIS.Controllers
{
    public class MyArticleController : BaseController
    {
        /// <summary>
        /// 业务层注入
        /// </summary>
        [Dependency]
        public IMIS_ArticleBLL m_BLL { get; set; }
        [Dependency]
        public IMIS_Article_CategoryBLL categoryBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns>视图</returns>
        [SupportFilter]
        public ActionResult Index()
        {
            
            return View();

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pager">分页</param>
        /// <param name="queryStr">查询条件</param>
        /// <returns></returns>
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr, int checkFlag = 1)
        {
            List<MIS_ArticleModel> list = m_BLL.GetList(ref pager, queryStr,"", false, GetUserId(), checkFlag);
            var json = new
            {
                total = pager.totalRows,
                rows = (from r in list
                        select new MIS_ArticleModel()
                        {

                            Id = r.Id,
                            ChannelId = r.ChannelId,
                            CategoryId = r.CategoryId,
                            CategoryName = r.CategoryName,
                            Title = r.Title,
                            ImgUrl =r.ImgUrl,
                            //BodyContent = r.BodyContent,
                            Sort = r.Sort,
                            Click = r.Click,
                            CheckFlag = r.CheckFlag,
                            Checker = r.Checker,
                            CheckerName = r.CheckerName,
                            CheckDateTime = r.CheckDateTime,
                            Creater = r.Creater,
                            CreaterName = r.CreaterName,
                            CreateTime = r.CreateTime,
                            IsType = r.IsType
                        }).ToArray()

            };

            return Json(json);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            
            ViewBag.Category = new SelectList(categoryBLL.GetList("0"), "Id", "Name");
            return View();
        }

        [HttpPost]
        [SupportFilter]
        [ValidateInput(false)]
        public JsonResult Create(MIS_ArticleModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                model.Id = ResultHelper.NewId;
                model.ChannelId = 0;
                model.Creater = GetUserId();
                model.CreateTime = ResultHelper.NowTime;
                model.CheckFlag = 0;
                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title, "成功", "创建", "信息中心");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title + "," + ErrorCol, "失败", "创建", "信息中心");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            
            MIS_ArticleModel entity = m_BLL.GetById(id);
            ViewBag.Category = new SelectList(categoryBLL.GetList("0"), "Id", "Name", entity.CategoryIdParent);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        [ValidateInput(false)]
        public JsonResult Edit(MIS_ArticleModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title, "成功", "修改", "信息中心");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + model.Id + ",Title:" + model.Title + "," + ErrorCol, "失败", "修改", "信息中心");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ":"+ErrorCol), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            
            MIS_ArticleModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
    

        [HttpPost]
        [SupportFilter]
        public JsonResult Delete(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (m_BLL.Delete(ref errors, id,GetUserId()))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "信息中心");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "信息中心");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }


        }

        #endregion

        #region 审核/反审核
        [HttpPost]
        [SupportFilter]
        public JsonResult Check(string Id)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {

                int Flag = 1;
                if (m_BLL.Check(ref errors, Id, Flag, GetUserId()))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + Id, "成功", "审核", "信息中心");
                    return Json(JsonHandler.CreateMessage(1, Resource.CheckSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + Id + "," + ErrorCol, "失败", "审核", "信息中心");
                    return Json(JsonHandler.CreateMessage(0, Resource.CheckFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.CheckFail));
            }
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult UnCheck(string Id)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {

                int Flag = 0;
                if (m_BLL.Check(ref errors, Id, Flag, GetUserId()))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + Id, "成功", "反审核", "信息中心");
                    return Json(JsonHandler.CreateMessage(1, Resource.UnCheckSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + Id + "," + ErrorCol, "失败", "反审核", "信息中心");
                    return Json(JsonHandler.CreateMessage(0, Resource.UnCheckFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.UnCheckFail));
            }
        }
        #endregion

    }
}

