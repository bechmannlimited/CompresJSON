using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Net.Http;
using System.IO;
using System.Text;


namespace CompresJSON
{
    public class CompressResult : System.Web.Mvc.ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["JSON-Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            
            if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }

            //

            //Stream req = request.InputStream;
            //req.Seek(0, System.IO.SeekOrigin.Begin);
            //string httpbody = new StreamReader(req).ReadToEnd();

            //Dictionary<string, string> data = Converter.QueryStringToDictionary(httpbody);
           // request.InputStream = new Stream(Converter.StringToBytes(data));


            //response.Filter = Cryptography.Encrypt(response.Filter, "password");

            //response.
            //var r = filterContext.HttpContext.Request.;
        }

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //}
    }

    public class Decompress : System.Web.Mvc.ActionFilterAttribute
    {

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var c = (JsonResult)filterContext.Result;
            var d = c.Data;

            base.OnActionExecuted(filterContext);
        }


        //public override void OnActionExecuted(System.Web.Mvc.ActionExecutingContext actContext)
        //{
        //    var content = actContext.HttpContext.Response.Content;
        //    var bytes = content == null ? null : content.ReadAsByteArrayAsync().Result;

        //    //CompressedResult compressedResult = new CompressedResult{
        //    //    compressionMethod = CompressionMethod.LZ77,
        //    //    encodedOutput = Converter.BytesToString(bytes),
        //    //    encodingMethod = EncodingMethod.Base64
        //    //};

        //    actContext.Response.Content = new ByteArrayContent(LZ77.Decompress(bytes));

        //    //var zlibbedContent = bytes == null ? new byte[0] : Compressor.Decompress(; //CompressionHelper.DeflateByte(bytes);
        //    //actContext.Response.Content = new ByteArrayContent(zlibbedContent);
        //    actContext.Response.Content.Headers.Remove("Content-Type");
        //    actContext.Response.Content.Headers.Add("Content-encoding", "deflate");
        //    actContext.Response.Content.Headers.Add("Content-Type", "application/json");
        //    base.OnActionExecuted(actContext);
        //}
    }
}