using Apps.Common;
using Apps.Models.Spl;
using Apps.Spl.IBLL;
using Apps.WebApi.Core;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apps.WebApi.Areas.Ware.Controllers
{
    public class Spl_FranchiseeController : BaseApiController
    {
        [Dependency]
        public ISpl_FranchiseeBLL m_FracBLL { get; set; }
        ValidationErrors errors = new ValidationErrors();
        [HttpGet]
        public object GetFranchiseeList(string filter)
        {
            List<Spl_FranchiseeModel> spl_Franchisees = new List<Spl_FranchiseeModel>();
            JObject opc = JObject.Parse(filter);
            spl_Franchisees = m_FracBLL.GetFranchiseeList(int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (spl_Franchisees != null)
            {
                return Json(spl_Franchisees);
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public object GetFranchiseeListByQueryStr(string filter)
        {
            List<Spl_FranchiseeModel> spl_Franchisees = new List<Spl_FranchiseeModel>();
            JObject opc = JObject.Parse(filter);
            string queryStr="";
            if (JObject.Parse(opc["where"].ToString())["querystr"] != null)
            {
                queryStr = JObject.Parse(opc["where"].ToString())["querystr"].ToString();
            }
            spl_Franchisees = m_FracBLL.GetFranchiseeListByQueryStr(queryStr,int.Parse(opc["skip"].ToString()), int.Parse(opc["limit"].ToString()));
            if (spl_Franchisees != null)
            {
                return Json(spl_Franchisees);
            }
            else
            {
                return null;
            }
        }
    }
}
