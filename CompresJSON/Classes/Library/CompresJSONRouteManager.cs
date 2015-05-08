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
                null,
                new DecryptMVCRouteHandler())
            );
        }

        //Web Api Route setup
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "SecretDefaultApi",
                routeTemplate: "apih/{c}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new DecryptWebApiRouteHandler(GlobalConfiguration.Configuration)
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

    public class DecryptMVCRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            var c = CompresJSONRouteManager.DecryptSecretUrlComponent(routeValues["c"].ToString());
            var a = CompresJSONRouteManager.DecryptSecretUrlComponent(routeValues["a"].ToString());

            routeValues["c"] = null;
            routeValues["a"] = null;

            routeValues["Controller"] = Encrypter.Decrypt(c);
            routeValues["Action"] = Encrypter.Decrypt(a);

            var mvcRouteHandler = new MvcRouteHandler();
            return (mvcRouteHandler as IRouteHandler).GetHttpHandler(requestContext);
        }
    }
}