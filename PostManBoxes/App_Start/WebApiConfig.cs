﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PostManBoxes
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{key}",
                defaults: new { key = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SendOrder",
                routeTemplate: "api/Boxes/Postboxes"
           
            );
        }
    }
}
