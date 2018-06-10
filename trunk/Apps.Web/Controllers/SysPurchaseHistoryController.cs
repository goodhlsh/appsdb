using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.IBLL;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;

namespace Apps.Web.Controllers
{
    public class SysPurchaseHistoryController : BaseController
    {
        [Dependency]
        public ISysPurchaseHistoryBLL m_BLL { get; set; }
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
            List<SysPurchaseHistoryModel> datalist = m_BLL.GetList(queryStr);
            List<SysPurchaseHistoryModel> list = datalist.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            int totalRecords = datalist.Count();
            var json = new
            {
                total = totalRecords,
                rows = (from model in list
                        select new SysPurchaseHistoryModel()
                        {
                            Id = model.Id,
                            UserId = model.UserId,
                            Froms = model.Froms,
                            ShouRu = model.ShouRu,
                            CreateTime = model.CreateTime,
                            IsShow = model.IsShow,
                            Note = model.Note,
                            TrueName = model.TrueName,
                            UserName = model.UserName
                        }).ToArray()

            };
            return Json(json);


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
        public JsonResult Create(SysPurchaseHistoryModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.UserId, "成功", "创建", "SysPurchaseHistory");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.UserId + "," + ErrorCol, "失败", "创建", "SysPurchaseHistory");
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
            SysPurchaseHistoryModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysPurchaseHistoryModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.UserId, "成功", "修改", "SysPurchaseHistory");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.UserId + "," + ErrorCol, "失败", "修改", "SysPurchaseHistory");
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
            SysPurchaseHistoryModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysPurchaseHistory");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + id + "," + ErrorCol, "失败", "删除", "SysPurchaseHistory");
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
