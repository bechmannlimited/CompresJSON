using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompresJSON
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Hide",
            //    url: Encrypter.Encrypt("hideurl") + "/{c}/{a}/{id}",
            //    defaults: new { controller = "", action = "", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Receiver", action = "sendEncryptedData", id = UrlParameter.Optional }
            );
        }
    }
}
