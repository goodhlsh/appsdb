﻿using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.Spl.IBLL;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Spl;
using Microsoft.Practices.Unity;

namespace Apps.Web.Areas.Spl.Controllers
{
    public class WareController : BaseController
    {
        [Dependency]
        public ISpl_WareBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<Spl_WareModel> list = m_BLL.GetList(ref pager, queryStr);
            GridRows<Spl_WareModel> grs = new GridRows<Spl_WareModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Spl_WareModel model)
        {
            model.id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",Name" + model.Name, "成功", "创建", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(string id)
        {
            ViewBag.Perm = GetPermission();
            Spl_WareModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Spl_WareModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",Name" + model.Name, "成功", "修改", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(string id)
        {
            ViewBag.Perm = GetPermission();
            Spl_WareModel entity = m_BLL.GetById(id);
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
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + id + "," + ErrorCol, "失败", "删除", "Spl_Ware");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }


        }
        #endregion
    }
}
