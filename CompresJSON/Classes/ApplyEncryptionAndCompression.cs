﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CompresJSON
{
    public class ApplyEncryptionAndCompression : ActionFilterAttribute
    {

        //after
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is JsonResult)
            {
                JsonResult result = (JsonResult)filterContext.Result;

                string serializedString = (new JavaScriptSerializer()).Serialize(result.Data);

                //compress
                string compressedString = Compressor.Compress(serializedString).encodedOutput;

                //encrypt
                var encryptedString = Encrypter.Encrypt(compressedString);

                var rc = new Dictionary<string, object>();
                rc["data"] = encryptedString;

                filterContext.Result = new JsonResult()
                {
                    Data = rc,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            base.OnActionExecuted(filterContext);
        }

        
    }

    public class ApplyDecryptionAndDecompression : ActionFilterAttribute
    {
        //before
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Stream req = filterContext.HttpContext.Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string httpbody = new StreamReader(req).ReadToEnd();
            //httpbody = Encrypter.Decrypt(httpbody);

            Dictionary<string, string> httpBodyDictionary = Converter.QueryStringToDictionary(httpbody);

            if (httpBodyDictionary.ContainsKey("data") && httpBodyDictionary["data"] != null)
            {
                //assume encrypted + compressed for now

                //decrypt
                string decryptedString = Encrypter.Decrypt(httpBodyDictionary["data"]);

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