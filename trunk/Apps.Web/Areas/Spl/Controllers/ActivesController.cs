using System.Collections.Generic;
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
    public class ActivesController : BaseController
    {
        [Dependency]
        public ISpl_ActivesBLL m_BLL { get; set; }
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
            List<Spl_ActivesModel> list = m_BLL.GetList(ref pager, queryStr);
            GridRows<Spl_ActivesModel> grs = new GridRows<Spl_ActivesModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            var actList =new List<Spl_ActType>{
                new Spl_ActType(){ActID="0",ActType="首单"},
                new Spl_ActType(){ActID="1",ActType="新会员"},
                new Spl_ActType(){ActID="2",ActType="满减"},
                new Spl_ActType(){ActID="3",ActType="红包"},
                new Spl_ActType(){ActID="4",ActType="推广奖励"},
                new Spl_ActType(){ActID="5",ActType="平台奖励"},
            };
            var actSelectList = new SelectList(actList, "ActType", "ActType");
            ViewData["ActSelect"] = actSelectList;
            ViewBag.Perm = GetPermission();
            Spl_ActivesModel model = new Spl_ActivesModel()
            {
                BeginDate = ResultHelper.NowTime
            };
            
            return View(model);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Spl_ActivesModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Title" + model.Title, "成功", "创建", "Spl_Actives");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Title" + model.Title + "," + ErrorCol, "失败", "创建", "Spl_Actives");
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
            var actList = new List<Spl_ActType>{
                new Spl_ActType(){ActID="0",ActType="首单"},
                new Spl_ActType(){ActID="1",ActType="新会员"},
                new Spl_ActType(){ActID="2",ActType="满减"},
                new Spl_ActType(){ActID="3",ActType="红包"},
                new Spl_ActType(){ActID="4",ActType="推广奖励"},
                new Spl_ActType(){ActID="5",ActType="平台奖励"},
            };
            var actSelectList = new SelectList(actList, "ActType", "ActType");
            ViewData["ActSelect"] = actSelectList;
            ViewBag.Perm = GetPermission();
            Spl_ActivesModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Spl_ActivesModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Title" + model.Title, "成功", "修改", "Spl_Actives");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Title" + model.Title + "," + ErrorCol, "失败", "修改", "Spl_Actives");
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
            Spl_ActivesModel entity = m_BLL.GetById(id);
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Spl_Actives");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "Spl_Actives");
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
