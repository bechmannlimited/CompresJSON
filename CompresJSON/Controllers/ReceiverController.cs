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
        public JsonResult RouteErrorJsonResult()
        {
            var rc = new Dictionary<string, object>();
            rc["error"] = "Route value failure";
            return Json(rc);
        }

        [ApplyDecryptionAndDecompression]
        //[ApplyEncryptionAndCompression]
        public JsonResult LookAtUser(User user, FormCollection formc, string testString)
        {
            var rc = new Dictionary<string, object>();
            rc["user"] = user;
            rc["test"] = testString;
            rc["formc"] = formc;
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sendEncryptedData()
        {
            var json = Encrypter.Encrypt("{ \"UserID\": 5, \"Name\": \"Alex\", \"testString\": \"hellooooo\", \"c\": \"Receiver\", \"a\": \"LookAtUser\" }");
            ViewBag.Json = json;
            return View();
        }

    }
}


