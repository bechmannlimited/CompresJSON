using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AESCryptoTest;

namespace CompresJSON.Controllers
{
    public class SpeedTestsController : Controller
    {
        // GET: SpeedTests
        public ActionResult Index()
        {
            var s = Encrypter.Encrypt("hello");
            var d = Encrypter.Decrypt(s);

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