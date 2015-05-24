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
            var original = "jkasldfj kla;sjdf;kl ajsdl;f jasd;lfk jaskl;df jaw3iorj2oifj{{{ {} } } } }\" z\" ()89023849024 :  KLFJD:LKF J:LF J:L J:: :) :) :)";
            var d = Compressor.Compress(original);
            ViewBag.compressed = d;
            ViewBag.original = original;
            var x = Converter.Base64Encode(d);
            //var y = Compressor.Decompress(d);
            //var x = Compressor.Decompress("VTJGc2RHVmtYMStiOHZGMEJFNW9oR2FiNWpJajVrcUpub1FEelYvV3dIVHFBQ2dvSHNyZVlZRXkvMkNia3JUaFZQZFZBT2xYVGZNa1hQM3RQWnVUeGcvYXBkWjVuRFd4QnhKRitRMlRPTHFmSC84TGR3VWNWVG9kalhwN0plc0lSeWFSRDJNV01LS1lsYWxway9KQUEwZXJJazZydlpmdWtsODMveC9JVXR4YTF1RldGVEhyM3AyRW9KbTZpZmJxV29DckNiWkQwUTN4bS9IMnJEQkhqQT09");

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