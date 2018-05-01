using System;
using System.Collections.Generic;
using System.Linq;
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
            var jsonFormatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
            config.Services.Replace(typeof(System.Net.Http.Formatting.IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
    public class JsonContentNegotiator : System.Net.Http.Formatting.IContentNegotiator
    {
        private readonly System.Net.Http.Formatting.JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(System.Net.Http.Formatting.JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }

        public System.Net.Http.Formatting.ContentNegotiationResult Negotiate(Type type, System.Net.Http.HttpRequestMessage request, IEnumerable<System.Net.Http.Formatting.MediaTypeFormatter> formatters)
        {
            var result = new System.Net.Http.Formatting.ContentNegotiationResult(_jsonFormatter, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            return result;
        }
    }
}
