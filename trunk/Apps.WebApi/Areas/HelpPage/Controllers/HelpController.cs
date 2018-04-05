using System;
using System.Web.Http;
using System.Web.Mvc;
using Apps.WebApi.Areas.HelpPage.Models;
using Microsoft.Practices.Unity;
using Apps.IBLL;

namespace Apps.WebApi.Areas.HelpPage.Controllers
{
    
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        [Dependency]
        public ISysModuleBLL m_BLL2 { get; set; }
        [Dependency]
        public ISysModuleOperateBLL operateBLL2 { get; set; }
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        public HelpController(HttpConfiguration config)
        {
            Configuration = config;
        }

        public HttpConfiguration Configuration { get; private set; }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return View(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (!String.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return View(apiModel);
                }
            }

            return View("Error");
        }
    }
}