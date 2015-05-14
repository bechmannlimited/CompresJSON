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

            //message = CompresJSON.EncryptAndCompressAsNecessary(message);

            var rc = new Dictionary<string, object>();
            rc["original"] = message;
            rc["compr"] = Compressor.Compress(message);
            rc["encr"] = Encrypter.Encrypt(message);
            rc["all"] = CompresJSON.EncryptAndCompressAsNecessary(message);
            //return Json(CompresJSON.DecryptAndDecompressAsNecessary("U2FsdGVkX19D1Ho3AHgtNTPtSK22whMKAapnsi5HY8AeUO4mbjIuCp7Edfj07QZo9/Gg9o+VSmzK42LYGZCnu1um7i8NXRjZB5Gece9uMPIEWpqHN9X7SGZ3wPR/8NJRWGOJ1jpNx3ICBg9NUU+GCw=="), JsonRequestBehavior.AllowGet);
            return View();
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
            return Json(CompresJSON.EncryptAndCompressAsNecessary(str), JsonRequestBehavior.AllowGet);
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