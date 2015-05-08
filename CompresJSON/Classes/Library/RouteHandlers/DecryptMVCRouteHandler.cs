using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompresJSON.Classes.Action_filters
{
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