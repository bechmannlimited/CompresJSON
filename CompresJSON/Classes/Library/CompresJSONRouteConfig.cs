using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompresJSON
{
    public class CompresJSONRouteConfig
    {
        //MVC route setup
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(
            new Route(
                    "hide/{c}/{a}/{id}",
                    new RouteValueDictionary(new { controller = "", action = "", id = UrlParameter.Optional }),
                    new CustomRouteHandler()));
        }

        //Web Api Route setup
        public static void Register(HttpConfiguration config)
        {
            // Web API secret route
            config.Routes.MapHttpRoute(
                name: "SecretDefaultApi",
                routeTemplate: "hide/api/{c}/{a}/{id}",
                defaults: new RouteValueDictionary(new { controller = "", action = "", id = UrlParameter.Optional }),
                constraints: new CustomRouteHandler()
                );
        }
    }

    public class CustomRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            var c = HttpUtility.UrlDecode(routeValues["c"].ToString().Replace("!", "%"));
            var a = HttpUtility.UrlDecode(routeValues["a"].ToString().Replace("!", "%"));

            var controller = Encrypter.Decrypt(c);
            var action = Encrypter.Decrypt(a);

            routeValues["Controller"] = controller;
            routeValues["Action"] = action;

            var mvcRouteHandler = new MvcRouteHandler();
            return (mvcRouteHandler as IRouteHandler).GetHttpHandler(requestContext);
        }
    }
}