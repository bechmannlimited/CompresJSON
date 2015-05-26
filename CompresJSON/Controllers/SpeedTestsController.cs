using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
        }
    }
}
