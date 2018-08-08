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
    public class WareController : BaseController
    {
        [Dependency]
        public ISpl_WareBLL m_BLL { get; set; }
        [Dependency]
        public ISpl_ProductCategorySBLL mp_BLL { get; set; }
        [Dependency]
        public ISpl_WareInfoBLL mi_BLL { get; set; }

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
            GridPager pager = new GridPager();
            pager.page = 1;
            pager.rows = 15;
            pager.order = "desc";
            List<Spl_ProductCategorySModel> spl_pros = new List<Spl_ProductCategorySModel>();
            spl_pros = mp_BLL.GetList(ref pager,"");
            List<Spl_ProCateSModel> spl_ProCates = new List<Spl_ProCateSModel>();
            foreach (Spl_ProductCategorySModel item in spl_pros)
            {
                spl_ProCates.Add(new Spl_ProCateSModel()
                {
                    ProductCategoryId=item.Id,
                    ProductCategoryName=item.SonTypeName
                });
            }
            var pcSelect = new SelectList(spl_ProCates, "ProductCategoryId", "ProductCategoryName");
            ViewData["pcSelect"] = pcSelect;
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Spl_WareModel model)
        {
            model.Id = ResultHelper.NewId;
            model.BrandId = "210A2825675248908A6DEAD8F2686EEER";
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {
                if (m_BLL.Create(ref errors, model))
                {
                    Spl_WareInfoModel infoModel = new Spl_WareInfoModel();
                    infoModel.Id = ResultHelper.NewId;
                    infoModel.WareId = model.Id;
                    infoModel.Picture0 = model.Picture0;
                    infoModel.Picture1 = model.Picture1;
                    infoModel.Picture2 = model.Picture2;
                    infoModel.Picture3 = model.Picture3;
                    infoModel.Picture4 = model.Picture4;
                    infoModel.Picture5 = model.Picture5;
                    infoModel.ToTop = model.ToTop;
                    infoModel.CreateTime = model.CreateTime;
                    if (mi_BLL.Create(ref errors, infoModel))
                    {
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "创建", "Spl_Ware");
                        return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                    }
                    else
                    {
                        string ErrorCol = errors.Error;
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Spl_Ware");
                        return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                    }
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "创建", "Spl_Ware");
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
            GridPager pager = new GridPager();
            pager.page = 1;
            pager.rows = 15;
            pager.order = "desc";
            List<Spl_ProductCategorySModel> spl_pros = new List<Spl_ProductCategorySModel>();
            spl_pros = mp_BLL.GetList(ref pager, "");
            List<Spl_ProCateSModel> spl_ProCates = new List<Spl_ProCateSModel>();
            foreach (Spl_ProductCategorySModel item in spl_pros)
            {
                spl_ProCates.Add(new Spl_ProCateSModel()
                {
                    ProductCategoryId = item.Id,
                    ProductCategoryName = item.SonTypeName
                });
            }
            var pcSelect = new SelectList(spl_ProCates, "ProductCategoryId", "ProductCategoryName");
            ViewData["pcSelect"] = pcSelect;
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
                model.UpdateTime = ResultHelper.NowTime;
                if (m_BLL.Edit(ref errors, model))
                {
                    Spl_WareInfoModel infoModel = new Spl_WareInfoModel();
                    infoModel.Id = model.WareInfoId;
                    infoModel.WareId = model.Id;
                    infoModel.Picture0 = model.Picture0;
                    infoModel.Picture1 = model.Picture1;
                    infoModel.Picture2 = model.Picture2;
                    infoModel.Picture3 = model.Picture3;
                    infoModel.Picture4 = model.Picture4;
                    infoModel.Picture5 = model.Picture5;
                    infoModel.ToTop = model.ToTop;
                    infoModel.CreateTime = model.CreateTime;
                    if (mi_BLL.Edit(ref errors, infoModel))
                    {
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name, "成功", "修改", "Spl_Ware");
                        return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                    }
                    else
                    {
                        string ErrorCol = errors.Error;
                        LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "Spl_Ware");
                        return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));

                    }
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",Name" + model.Name + "," + ErrorCol, "失败", "修改", "Spl_Ware");
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
