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

            //compress
            string compressedString = Compressor.Compress(serializedString).encodedOutput;

            //encrypt
            var encryptedString = Encrypter.Encrypt(compressedString);

            var rc = new Dictionary<string, object>();
            rc["encryptedData"] = encryptedString;

            actionExecutedContext.Response.Content = new StringContent((new JavaScriptSerializer()).Serialize(rc));

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

                //decrypt
                string decryptedString = Encrypter.Decrypt(httpBodyDictionary["encryptedData"]);

                //decompress
                string json = Compressor.Decompress(new CompressedResult
                {
                    encodingMethod = CompresJSONDefaults.encodingMethod,
                    compressionMethod = CompresJSONDefaults.compressionMethod,
                    encodedOutput = decryptedString
                }).decompressedOutput;

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