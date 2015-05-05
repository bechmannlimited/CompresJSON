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
}