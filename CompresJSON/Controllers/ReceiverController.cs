using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace CompresJSON.Controllers
{
    public class ReceiverController : Controller
    {
        // GET: Receiver
        public ActionResult Index(string a, string c)
        {
            Dictionary<string, string> routeValues = new Dictionary<string, string>();
            string action = "RouteErrorJsonResult";
            string controller = "Receiver";

            Stream req = Request.InputStream;
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
                    if (key != "c" && key != "a")
                    {
                        routeValues.Add(key, dict[key]);
                    }
                    else if (key == "c")
                    {
                        controller = dict[key];
                    }
                    else if (key == "a")
                    {
                        action = dict[key];
                    }
                }
            }

            return RedirectToAction(action, controller, new RouteValueDictionary(routeValues));
        }

        public JsonResult RouteErrorJsonResult()
        {
            var rc = new Dictionary<string, object>();
            rc["error"] = "Route value failure";
            return Json(rc);
        }

        [EncryptOrDecryptHttpBody]
        public JsonResult LookAtUser(User user, FormCollection formc, string testString)
        {
            var rc = new Dictionary<string, object>();
            rc["user"] = user;
            rc["test"] = testString;
            rc["formc"] = formc;
            return Json(rc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sendEncryptedData()
        {
            var json = Encrypter.Encrypt("{ \"UserID\": 5, \"Name\": \"Alex\", \"testString\": \"hellooooo\", \"c\": \"Receiver\", \"a\": \"LookAtUser\" }");
            ViewBag.Json = json;
            return View();
        }

        //[EncryptHttpBody]
        //public ActionResult SendEncryptedData()
        //{
        //    var key = Encrypter.Encrypt("hello");
        //    var value = Encrypter.Encrypt("hasdfaslkdf");

        //    return RedirectToAction("ReceiveEncryptedData", new { key = value });
        //}

        [EncryptOrDecryptHttpBody]
        public JsonResult ReceiveEncryptedData(string hello)
        {
            return Json(hello, JsonRequestBehavior.AllowGet);
        }




        private string GetDocumentContents(System.Web.HttpRequestBase Request)
        {
            string documentContents;
            using (Stream receiveStream = Request.InputStream)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }
            return documentContents;
        }

        
    }
}


