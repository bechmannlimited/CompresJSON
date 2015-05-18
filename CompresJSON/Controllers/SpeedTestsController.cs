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
            return View();
        }

        //Results

        public JsonResult GetItemsUnencrypted(int take)
        {
            return Json(ManyItems(take));
        }

        [EncryptAndCompressAsNecessary]
        public JsonResult GetItemsEncrypted(int take)
        {
            return Json(ManyItems(take));
        }


        //Factory

        private CardDesignItem OneItem()
        {
            return AlexDbEntities.JsonDB().CardDesignItems.Take(100).FirstOrDefault();
        }

        private List<CardDesignItem> ManyItems(int take)
        {
            return AlexDbEntities.JsonDB().CardDesignItems.Take(take).ToList();

            //var rc = AlexDbEntities.JsonDB().Customers.Take(take).ToList();
            //rc.ForEach(x => x.Orders = AlexDbEntities.JsonDB().Orders.Take(20).ToList());
            //return rc;
        }

        public JsonResult decrypt(String str)
        {
            //str = HttpUtility.UrlDecode(str);
            return Json(CompresJSON.DecryptAndDecompressAsNecessary(str), JsonRequestBehavior.AllowGet);
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
        public int take = 0;
    }

    public class SpeedTestWebApi
    {
        public string description = "";
        public string table = "";
        //public bool encryptResponse = false;
        public bool encryptUrl = false;
        public string data = "";
        public string type = "POST";
        public int take = 0;
    }
}