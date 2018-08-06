using Apps.WebApi.App_Start;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Apps.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
                     

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action="index", id = RouteParameter.Optional }
            );
            //WebApi areas 中api路径
            config.Routes.MapHttpRoute(
               name: "DefaultApi2",
               routeTemplate: "api/{area}/{controller}/{action}/{id}",
               defaults: new { action = "Index", id = RouteParameter.Optional }
           );
            
            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

        }
    }
    
}
