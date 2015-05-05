using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompresJSON.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        [CompressResult]
        public JsonResult Index()
        {
            var rc = new Dictionary<string, object>();

            var str = "akjsfl;asjdf;lasjd fl;jaskldfjalsdjfklsjfdklsjfklsfdjl";

            var result = Compressor.Compress(str);
            var decoded = Compressor.Decompress(result);

            var test1 = Compressor.Compress(str, CompressionMethod.LZ77, EncodingMethod.Base64);
            var test2 = Compressor.Compress(str, CompressionMethod.LZ77, EncodingMethod.UTF8);

            rc[test1.encodingMethod.ToString()] = test1.encodedOutput;
            rc[test2.encodingMethod.ToString()] = test2.encodedOutput;

            rc["decode1"] = Compressor.Decompress(test1).decompressedOutput;
            //rc["decode2"] = Compressor.Decompress(test2).decompressedOutput;

            return Json(rc, JsonRequestBehavior.AllowGet);
        }

        [CompressResult]
        public JsonResult compressWithFilter()
        {
            Dictionary<string, object> rc = new Dictionary<string, object>();

            for (int i = 0; i < 10000; i++)
            {
                rc[i.ToString()] = "askdljfaklsdjfklsafjkaklsafljk";
            }

            return Json(rc, JsonRequestBehavior.AllowGet);
        }

        [CompressResult]
        public JsonResult compressManually(string str)
        {
            var result = Compressor.Compress(str, CompressionMethod.LZ77, EncodingMethod.ASCII);

            return Json(result.encodedOutput, JsonRequestBehavior.AllowGet);
        }

        [Decompress]
        public JsonResult decompress(string str)
        {

            return Json(str);
        }
    }
}