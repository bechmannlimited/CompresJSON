using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;

namespace CompresJSON
{
    public class WebApiApplyEncryptionAndCompression : ActionFilterAttribute
    {

        //after
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var data = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;

            string serializedString = (new JavaScriptSerializer()).Serialize(data);
            var encryptedString = Encrypter.Encrypt(serializedString);

            var rc = new Dictionary<string, object>();
            rc["encryptedData"] = encryptedString;

            //var jsonresult = new System.Web.Mvc.JsonResult()
            //{
            //    Data = rc,
            //    JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            //};

            //actionExecutedContext.Response.Content = new StringContent("{ \"EncryptedData\": \"" + rc["encryptedData"] + "\" }"); // new System.Net.Http.ByteArrayContent(
            actionExecutedContext.Response.Content = new StringContent((new JavaScriptSerializer()).Serialize(rc));

                //.Data);

            //var xx = new ByteArrayContent(jsonresult.Data);
            


            //if (actionExecutedContext.Response is System.Web.Mvc.JsonResult)
            //{
            //    System.Web.Mvc.JsonResult result = (System.Web.Mvc.JsonResult)filterContext.Result;

            //    string serializedString = (new JavaScriptSerializer()).Serialize(result.Data);
            //    var encryptedString = Encrypter.Encrypt(serializedString);

            //    var rc = new Dictionary<string, object>();
            //    rc["encryptedData"] = encryptedString;

            //    filterContext.Result = new System.Web.Mvc.JsonResult()
            //    {
            //        Data = rc,
            //        JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            //    };
            //}

            base.OnActionExecuted(actionExecutedContext);
        }


    }

    public class WebApiApplyDecryptionAndDecompression : ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            NameValueCollection postedParams = HttpContext.Current.Request.Params;
            Dictionary<string, string> httpBodyDictionary = new Dictionary<string, string>();

            foreach (var key in postedParams.AllKeys)
            {
                httpBodyDictionary[key] = postedParams[key].ToString();
            }

            if (httpBodyDictionary.ContainsKey("encryptedData") && httpBodyDictionary["encryptedData"] != null)
            {
                //assume encrypted + compressed for now
                string json = Encrypter.Decrypt(httpBodyDictionary["encryptedData"]);
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);

                foreach (var key in dict.Keys)
                {
                    actionContext.ActionArguments[key] = dict[key];
                    actionContext.ControllerContext.RouteData.Values[key] = dict[key];
                }

                var mvcActionModelParameters = actionContext.ActionDescriptor.GetParameters();

                foreach (var parameter in mvcActionModelParameters)
                {
                    string typeName = parameter.ParameterType.FullName; // "System.String";
                    var o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(typeName);
                    o = Tools.ToObject(actionContext.ControllerContext.RouteData.Values, o);

                    if (o != null)
                    {
                        actionContext.ActionArguments[parameter.ParameterName] = o;
                        actionContext.ControllerContext.RouteData.Values[parameter.ParameterName] = o;
                    }
                }
            }

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

    }
}