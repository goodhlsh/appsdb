using Apps.MIS.IBLL;
using Apps.Models.MIS;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.MIS.Controllers
{
    public class MyArticleController : ApiController
    {
        [Dependency]
        public IMIS_ArticleBLL m_BLL { get; set; }
        [Dependency]
        public IMIS_Article_CategoryBLL mc_BLL { get; set; }

        Common.ValidationErrors errors = new Common.ValidationErrors();
        [HttpGet]
        public object GetCategoryList(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "0";
            if (JObject.Parse(opc["where"].ToString())["id"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["id"].ToString();
            }
            List<MIS_Article_CategoryModel> category = new List<MIS_Article_CategoryModel>();
            List<MIS_Article_CategoryModel> category1 = mc_BLL.GetList(queryStr);
            List<MIS_Article_CategoryModel> category2 = new List<MIS_Article_CategoryModel>();
            foreach (var model1 in category1)
            {
                category2 = mc_BLL.GetList(model1.Id);
                model1.children = new List<MIS_Article_CategoryModel>();
                foreach (var model2 in category2)
                {
                    model1.children.Add(model2);
                }
                category.Add(model1);
            }
            return Json(category);
        }
        [HttpGet]
        public object GetListByCategoryID(string filter)
        {
            JObject opc = JObject.Parse(filter);
            var queryStr = "";
            if (JObject.Parse(opc["where"].ToString())["title"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["title"].ToString();
            }

            List<MIS_ArticleModel> list = m_BLL.GetListByIsType(queryStr, JObject.Parse(opc["where"].ToString())["categoryid"].ToString(), int.Parse(opc["istype"].ToString()), int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));

            return Json(list);
        }
        [HttpGet]
        public object GetById(string filter)
        {
            JObject opc = JObject.Parse(filter);
            if (JObject.Parse(opc["where"].ToString())["id"] == null)
            {
                return Json("");
            }

            MIS_ArticleModel model = new MIS_ArticleModel();
            model = m_BLL.GetById(JObject.Parse(opc["where"].ToString())["id"].ToString());
            if (model != null)
            {
                return Json(model);
            }
            else
            {
                return Json("");
            }

        }
    }
}
