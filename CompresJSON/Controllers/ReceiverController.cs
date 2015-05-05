using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompresJSON.Controllers
{
    public class ReceiverController : Controller
    {
        // GET: Receiver
        public JsonResult Index(string a, string c)
        {
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string httpbody = new StreamReader(req).ReadToEnd();
            httpbody = Encrypter.Decrypt(httpbody);

            Dictionary<string, string> data = Converter.QueryStringToDictionary(httpbody);
            return Json(data);



            //var decryptedHTTPBody = GetDocumentContents(Request);

            //Dictionary<string, object> data = new Dictionary<string, object>();
            //data.redirectToAction = a;
            //data.redirectToController = c;

            //foreach (var key in Request.Form.AllKeys)
            //{
            //    data[key] = Request.Form[key];
            //}
            //foreach (var key in Request.QueryString.AllKeys)
            //{
            //    data[key] = Request.QueryString[key];
            //}

            //return RedirectToAction(data.redirectToAction, new RouteValueDictionary(data));
        }

        public JsonResult LookAtUser(User user)
        {
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult sendEncryptedData()
        {
            return View();
        }

        //[EncryptHttpBody]
        //public ActionResult SendEncryptedData()
        //{
        //    var key = Encrypter.Encrypt("hello");
        //    var value = Encrypter.Encrypt("hasdfaslkdf");

        //    return RedirectToAction("ReceiveEncryptedData", new { key = value });
        //}

        [EncryptHttpBody]
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

    public class User
    {
        public int UserID { get; set; }
        public DateTime dob { get; set; }
    }

    public class EncryptHttpBody : ActionFilterAttribute
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

            base.OnActionExecuted(filterContext);
        }

        //before
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var de = Encrypter.Encrypt("hello");
            var ve = Encrypter.Encrypt("there");

            Stream req = filterContext.HttpContext.Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);
            string httpbody = new StreamReader(req).ReadToEnd();
            //httpbody = Encrypter.Decrypt(httpbody);

            Dictionary<string, string> httpBodyDictionary = Converter.QueryStringToDictionary(httpbody);

            foreach (var k in httpBodyDictionary.Keys)
            {
                var key = Encrypter.Decrypt(k);
                var value = Encrypter.Decrypt(httpBodyDictionary[k]);
                filterContext.ActionParameters[key] = value;
            }

            base.OnActionExecuting(filterContext);
        }

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


