using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompresJSON
{
    public class CompresJSONRouteManager
    {
        public static string SecretUrlPrefix = Encrypter.Encrypt("hide");

        //MVC route setup
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add(
            new Route(
                    SecretUrlPrefix + "/{c}/{a}/{id}",
                    new RouteValueDictionary(new { controller = "", action = "", id = UrlParameter.Optional }),
                    new CustomRouteHandler()));
        }

        //Web Api Route setup
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Web API secret route
            config.Routes.MapHttpRoute(
                name: "SecretDefaultApi",
                routeTemplate: SecretUrlPrefix + "/api/{c}/{a}/{id}",
                defaults: new RouteValueDictionary(new { controller = "", action = "", id = UrlParameter.Optional }),
                constraints: new CustomRouteHandler()
                );
        }

        public static string EncryptSecretUrlComponent(string str)
        {
            return HttpUtility.UrlEncode(Encrypter.Encrypt(str)).Replace("%", "!");
        }

        public static string DecryptSecretUrlComponent(string str)
        {
            return HttpUtility.UrlDecode(str.Replace("!", "%"));
        }
    }

    public class CustomRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            var c = CompresJSONRouteManager.DecryptSecretUrlComponent(routeValues["c"].ToString());
            var a = CompresJSONRouteManager.DecryptSecretUrlComponent(routeValues["a"].ToString());

            var controller = Encrypter.Decrypt(c);
            var action = Encrypter.Decrypt(a);

            routeValues["Controller"] = controller;
            routeValues["Action"] = action;

            var mvcRouteHandler = new MvcRouteHandler();
            return (mvcRouteHandler as IRouteHandler).GetHttpHandler(requestContext);
        }
    }
}