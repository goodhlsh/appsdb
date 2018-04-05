using System.Web.Http;
using System.Web.Mvc;

namespace Apps.WebApi.Areas.Spl2
{
    public class Spl2AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Spl2";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            var areaName = this.AreaName;
            context.Routes.MapHttpRoute(
                areaName+"_area",
                string.Format("api/{0}",areaName)+"/{controller}/{action}/{id}",
                defaults:new { area = areaName, actoin = "index", id = RouteParameter.Optional });
            context.MapRoute(
                areaName+"_default",
                areaName+"/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}