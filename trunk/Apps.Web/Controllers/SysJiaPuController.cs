﻿using System.Collections.Generic;
using Apps.Web.Core;
using Apps.IBLL;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.Models.Sys;
using Microsoft.Practices.Unity;
using Apps.Models;
using System.Text;

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
        public JsonResult GetTree(SysJiaPuRModel node, string queryStr)
        {
            SysJiaPuRModel sysJiaPuRModel = new SysJiaPuRModel();
            sysJiaPuRModel = m_BLL.CreateTree(node, queryStr);
            return Json(sysJiaPuRModel);
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
            SysJiaPuModel sysJiaPu = new SysJiaPuModel();
            if (string.IsNullOrEmpty(model.UserId))
            {
                return null;
            }
            sysJiaPu = m_BLL.GetRefSysJiaPu(model.UserId);
            if (sysJiaPu != null)
            {
                return null;
            }
            model.Id = ResultHelper.NewId;
            if (!string.IsNullOrEmpty(model.ZMP15))
            {
                int z = int.Parse(model.ZMP15.Substring(0, 1))+65;
                model.ZMPA2 = ((char)z).ToString()+model.ZMP15.Substring(2);
            }
            else
            {
                model.ZMPA2 = "Y2";
            }
            
            
            model.CreateTime = ResultHelper.NowTime;
            if (model.UserId != null&&model.FirstJinE>=398 && ModelState.IsValid)
            {
                if (m_BLL.IntoSysJiaPu(model.UserId, model.TId, model.ParentId, model.ZMPA2, (decimal)model.FirstJinE) >= 0)
                {
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.TrueName, "成功", "创建", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.TrueName + "," + ErrorCol, "失败", "创建", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        public ActionResult GetLocal(string uid)
        {
            List<string> ss = new List<string>();
            ss = GetRealSons(uid);

            string[] s = ss.ToArray();// new string[] { "1_1", "2_1", "3_2", "6_1", "8_2" };
            ViewBag.RealSons = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            return View();
        }
        #region  获取用户的儿子位置真实情况

        public List<string> GetRealSons(string userID)
        {
            List<string> sons = new List<string>();
            sons = m_BLL.GetRealSon(userID);            
            return sons;
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
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.TrueName, "成功", "修改", "SysJiaPu");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserId(), "Id" + model.Id + ",UserId" + model.TrueName + "," + ErrorCol, "失败", "修改", "SysJiaPu");
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
        [HttpPost]
        public JsonResult GetOptionByBarChart(GridPager pager, string queryStr)
        {
            List<SysJiaPuModel> list = m_BLL.GetList(ref pager, queryStr);
            SysJiaPuRModel sysJiaPuRModel = new SysJiaPuRModel();
            SysJiaPuRModel node = new SysJiaPuRModel();
            node.ParentId = "0";
         
            sysJiaPuRModel = m_BLL.CreateTree(node, queryStr);
            //List<decimal?> costPrice = new List<decimal?>();
            //list.ForEach(a => costPrice.Add(a.UserId));
            //List<decimal?> price = new List<decimal?>();
            //list.ForEach(a => price.Add(a.Price));
            //List<string> names = new List<string>();
            //list.ForEach(a => names.Add(a.Name));
            //List<ChartSeriesModel> seriesList = new List<ChartSeriesModel>();
            //ChartSeriesModel series1 = new ChartSeriesModel()
            //{
            //    name = "成本价",
            //    type = "bar",
            //    data = costPrice
            //};
            //ChartSeriesModel series2 = new ChartSeriesModel()
            //{
            //    name = "零售价",
            //    type = "bar",
            //    data = price
            //};
            //seriesList.Add(series1);
            //seriesList.Add(series2);
            var option = new
            {
                title = new { text = "成本价零售价对照表" },
                tooltip = new { trigger="item",triggerOn="mousemove"},
                legend = new { data = "成本价零售价对照表" },               
                series =   new   {
                 type="tree",

                data= Json(sysJiaPuRModel),

                top="18%",
                bottom="14%",

                layout="radial",

                symbol="emptyCircle",

                symbolSize=7,

                initialTreeDepth=3,

                animationDurationUpdate=750

            }
        
                
            };
            return Json(option);
        }
    }
}
