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
    public class SysJiaPuController : BaseController
    {
        [Dependency]
        public ISysJiaPuBLL m_BLL { get; set; }
        [Dependency]
        public ISysUserBLL ms_BLL { get; set; }
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
            List<SysJiaPuModel> list = m_BLL.GetList(ref pager, queryStr);
            GridRows<SysJiaPuModel> grs = new GridRows<SysJiaPuModel>();

            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            ViewBag.Perm = GetPermission();

            SysJiaPuModel model = new SysJiaPuModel()
            {
                CreateTime = ResultHelper.NowTime
            };


            return View(model);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(SysJiaPuModel model)
        {
            model.id = ResultHelper.NewId;
            SysUserModel userModel = new SysUserModel();
            if (ms_BLL.GetById(model.UserId) != null)
            {
                userModel = ms_BLL.GetById(model.UserId);
                model.TId = userModel.RecommendID;
            }

            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (ms_BLL.IntoSysJiaPu(model.UserId, model.TId, model.ParentId, model.ZMPA2, (decimal)model.FirstJinE) >= 0)
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",UserId" + model.TrueName, "成功", "创建", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",UserId" + model.TrueName + "," + ErrorCol, "失败", "创建", "SysJiaPu");
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
            SysJiaPuModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(SysJiaPuModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",UserId" + model.TrueName, "成功", "修改", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + model.id + ",UserId" + model.TrueName + "," + ErrorCol, "失败", "修改", "SysJiaPu");
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
            SysJiaPuModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "id" + id + "," + ErrorCol, "失败", "删除", "SysJiaPu");
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
