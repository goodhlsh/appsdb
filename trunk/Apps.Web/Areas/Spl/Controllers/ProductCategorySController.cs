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
    public class ProductCategorySController : BaseController
    {
        [Dependency]
        public ISpl_ProductCategorySBLL ms_BLL { get; set; }
        [Dependency]
        public ISpl_ProductCategoryBLL m_BLL { get; set; }
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
            List<Spl_ProductCategorySModel> list_tmp = ms_BLL.GetList(ref pager, queryStr);
            List<Spl_ProductCategorySModel> list = new List<Spl_ProductCategorySModel>();
            foreach (Spl_ProductCategorySModel item in list_tmp)
            {
                list.Add(new Spl_ProductCategorySModel()
                {
                    Id = item.Id,
                    SupID = item.SupID,
                    SupName = m_BLL.GetById(item.SupID).TypeName,
                    SonTypeName = item.SonTypeName,
                    PicShow = item.PicShow,
                    Note = item.Note,
                    CreateTime = item.CreateTime,
                    Promoted = item.Promoted
                });
            }


            GridRows<Spl_ProductCategorySModel> grs = new GridRows<Spl_ProductCategorySModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            /* DropDownList()方法 一般用于页面没有绑定模型之时
             * var provinceList = new List<Spl_ProductCategoryModel>
             {
                 new Spl_ProductCategoryModel(){ Id="4E116CAA9382423A9DB8E483F5832948",TypeName="化妆品"},
                 new Spl_ProductCategoryModel(){ Id="56c80da883af652643474b6b",TypeName="家居用品"}
             };
                 //["4E116CAA9382423A9DB8E483F5832948", "化妆品" ];
             var provinceSelectList = new SelectList(provinceList, "Id", "TypeName");
             ViewData["provinceSelectList"] = provinceSelectList;
             */
            //该方法用于页面上有绑定模型之时，比如在产品的修改或者创建页面，我们都一般会绑定到一个模型，这里就会使用DropDownListFor()方法
            List<Spl_ProductCategoryModel> models = new List<Spl_ProductCategoryModel>();
            models = m_BLL.GetPage("", 0, 100);
            List<Spl_ProCateModel> spl_Pros = new List<Spl_ProCateModel>();
            foreach (Spl_ProductCategoryModel item in models)
            {
                spl_Pros.Add(
                new Spl_ProCateModel (){ SupName = item.TypeName, SupID = item.Id});
            };
            var pcSelect = new SelectList(spl_Pros, "SupID", "SupName");
            ViewData["pcSelect"] = pcSelect;
            ViewBag.Perm = GetPermission();
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(Spl_ProductCategorySModel model)
        {
            model.Id = ResultHelper.NewId;
            model.CreateTime = ResultHelper.NowTime;
            if (model != null && ModelState.IsValid)
            {

                if (ms_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SonTypeName" + model.SonTypeName, "成功", "创建", "Spl_ProductCategoryS");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SonTypeName" + model.SonTypeName + "," + ErrorCol, "失败", "创建", "Spl_ProductCategoryS");
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
            Spl_ProductCategorySModel entity = ms_BLL.GetById(id);
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(Spl_ProductCategorySModel model)
        {
            if (model != null && ModelState.IsValid)
            {

                if (ms_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SonTypeName" + model.SonTypeName, "成功", "修改", "Spl_ProductCategoryS");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",SonTypeName" + model.SonTypeName + "," + ErrorCol, "失败", "修改", "Spl_ProductCategoryS");
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
            Spl_ProductCategorySModel entity = ms_BLL.GetById(id);
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
                if (ms_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id:" + id, "成功", "删除", "Spl_ProductCategoryS");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + id + "," + ErrorCol, "失败", "删除", "Spl_ProductCategoryS");
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
