using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace labb1._1dv449
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
                routeTemplate: "api/{controller}/{force}",
                defaults: new { force = RouteParameter.Optional }
            );

        }
    }
}
