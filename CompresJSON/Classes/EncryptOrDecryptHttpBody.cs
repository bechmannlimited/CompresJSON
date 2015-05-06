using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CompresJSON
{
    public class EncryptOrDecryptHttpBody : ActionFilterAttribute
    {

        //after
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is JsonResult)
            {
                JsonResult result = (JsonResult)filterContext.Result;
                
                var dict = (Dictionary<string, object>)result.Data;
                foreach (var key in dict.Keys)
                {
                    if (dict[key] is object) // string is object????
                    {
                        var d = "";
                        // turn into dictionary
                        //set parameters to result.Data[]
                    }
                }

                string str = (new JavaScriptSerializer()).Serialize(result.Data);

                var encryptedString = Encrypter.Encrypt(str);

                var rc = new Dictionary<string, object>();
                rc["encryptedData"] = encryptedString;

                filterContext.Result = new JsonResult()
                {
                    Data = rc,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            base.OnActionExecuted(filterContext);
        }

        //before
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Stream req = filterContext.HttpContext.Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string httpbody = new StreamReader(req).ReadToEnd();
            //httpbody = Encrypter.Decrypt(httpbody);

            Dictionary<string, string> httpBodyDictionary = Converter.QueryStringToDictionary(httpbody);

            if (httpBodyDictionary["encryptedData"] != null)
            {
                //assume encrypted + compressed for now
                string json = Encrypter.Decrypt(httpBodyDictionary["encryptedData"]);
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);

                foreach (var key in dict.Keys)
                {
                    filterContext.ActionParameters[key] = dict[key];
                }

                var mvcActionModelParameters = filterContext.ActionDescriptor.GetParameters();

                foreach (var parameter in mvcActionModelParameters)
                {
                    string typeName = parameter.ParameterType.FullName; // "System.String";
                    var o = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(typeName);
                    o = Tools.ToObject(filterContext.ActionParameters, o);

                    if (o != null)
                    {
                        filterContext.ActionParameters[parameter.ParameterName] = o;
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}