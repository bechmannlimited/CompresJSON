using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CompresJSON.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/compresjson").Include(
                "~/Scripts/compresjson_scripts/base64-string.js",
                "~/Scripts/compresjson_scripts/Base64.js",
                "~/Scripts/compresjson_scripts/lz-string.js",
                "~/Scripts/compresjson_scripts/aes.js",
                "~/Scripts/compresjson_scripts/pbkdf2.js",
                "~/Scripts/compresjson_scripts/Compress_Encrypt.js"
           ));

            BundleTable.EnableOptimizations = true;
        }
    }
}