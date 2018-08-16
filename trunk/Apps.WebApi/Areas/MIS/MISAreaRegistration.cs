using System.Web.Http;
using System.Web.Mvc;

namespace Apps.WebApi.Areas.MIS
{
    public class MISAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MIS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            var areaName = this.AreaName;
            context.Routes.MapHttpRoute(
                areaName + "_area",
                string.Format("api/{0}", areaName) + "/{controller}/{action}/{id}",
                defaults: new { area = areaName, actoin = "index", id = RouteParameter.Optional });
            context.MapRoute(
                areaName + "_default",
                areaName + "/{controller}/{action}/{id}",
                new { action = "index", id = UrlParameter.Optional }
            );
        }
    }
}