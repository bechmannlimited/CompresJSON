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
            //filterContext.HttpContext.Request.InputStream
            //Stream req = filterContext.HttpContext.Request.InputStream;
            //req.Seek(0, System.IO.SeekOrigin.Begin);
            //string httpbody = new StreamReader(req).ReadToEnd();
            //httpbody = Encrypter.Decrypt(httpbody);

            //using (Stream s = GenerateStreamFromString(httpbody))
            //{
            //    filterContext.HttpContext.Request.InputStream = s;
            //}

            if (filterContext.Result is JsonResult)
            {
                JsonResult result = (JsonResult)filterContext.Result;
                var str = result.Data.ToString();

                //var str = new JsonSerializer().dese
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

        public class Generic<T>
        {
            public Generic()
            {
                Console.WriteLine("T={0}", typeof(T));
            }
        }

        //THIS ONE DECRYPTS ALL KEYS SEPERATELY
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{

        //    var de = Encrypter.Encrypt("hello");
        //    var ve = Encrypter.Encrypt("there");

        //    Stream req = filterContext.HttpContext.Request.InputStream;
        //    req.Seek(0, System.IO.SeekOrigin.Begin);
        //    string httpbody = new StreamReader(req).ReadToEnd();
        //    //httpbody = Encrypter.Decrypt(httpbody);

        //    Dictionary<string, string> httpBodyDictionary = Converter.QueryStringToDictionary(httpbody);

        //    foreach (var k in httpBodyDictionary.Keys)
        //    {
        //        var key = Encrypter.Decrypt(k);
        //        var value = Encrypter.Decrypt(httpBodyDictionary[k]);
        //        filterContext.ActionParameters[key] = value;
        //    }

        //    base.OnActionExecuting(filterContext);
        //}

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}