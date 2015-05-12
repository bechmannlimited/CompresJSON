using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AESCryptoTest;
using lz_string_csharp;

namespace CompresJSON.Controllers
{
    public class SpeedTestsController : Controller
    {
        // GET: SpeedTests
        public ActionResult Index()
        {
            var message = "hello really";

            //message = CompresJSONUtilities.EncryptAndCompressAsNecessary(message);

            var rc = new Dictionary<string, object>();
            rc["original"] = message;
            rc["compr"] = Compressor.Compress(message);
            rc["encr"] = Encrypter.Encrypt(message);
            rc["all"] = CompresJSONUtilities.EncryptAndCompressAsNecessary(message);
            return Json(CompresJSONUtilities.DecryptAndDecompressAsNecessary("U2FsdGVkX1+sB1LqUdw2UEGijQ/LDKFzMgOjkbc5Pju5NU3uL4WMEnkzsX0q7h4E"), JsonRequestBehavior.AllowGet);
            //return View();
        }

        //Results

        public JsonResult GetOneUserUnencrypted()
        {
            return Json(OneUser());
        }

        [EncryptAndCompressAsNecessary]
        public JsonResult GetOneUserEncrypted()
        {
            return Json(OneUser());
        }

        public JsonResult GetManyUsersUnencrypted()
        {
            return Json(ManyUsers());
        }

        [EncryptAndCompressAsNecessary]
        public JsonResult GetManyUsersEncrypted()
        {
            return Json(ManyUsers());
        }


        //Factory

        private User OneUser()
        {
            return new User
            {
                Name = "Alex",
                AdLine1 = "Address line 1",
                UserID = 4934
            };
        }

        private List<User> ManyUsers()
        {
            var rc = new List<User>();

            for (int i = 0; i < 3000; i++)
            {
                rc.Add(OneUser());
            }

            return rc;
        }

        public JsonResult decrypt(String str)
        {
            //str = HttpUtility.UrlDecode(str);
            return Json(Encrypter.Decrypt(str), JsonRequestBehavior.AllowGet);
        }
    }
}

namespace CompresJSON
{
    public class SpeedTest
    {
        public string description = "";
        public string action = "";
        public bool encryptResponse = false;
        public bool encryptUrl = false;
        public string type = "POST";
    }

    public class SpeedTestWebApi
    {
        public string description = "";
        public string table = "";
        //public bool encryptResponse = false;
        public bool encryptUrl = false;
        public string data = "";
        public string type = "POST";
    }
}