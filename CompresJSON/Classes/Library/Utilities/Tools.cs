using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CompresJSON
{
    public static class Tools
    {

        public static Dictionary<string, string> ToDictionary<TKey, TValue>(this NameValueCollection col)
        {
            var dict = new Dictionary<string, string>();

            foreach (string key in col)
            {
                dict.Add(key, col[key]);
            }

            return dict;
        }

        public static string Domain(System.Web.Routing.RequestContext context)
        {
            var domain = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            var extra = new UrlHelper(context).Action("A", "C").Replace("/C/A", "");

            var rc = (domain + extra);
            rc = RemoveFromEnd(rc, "/");
            return rc == null ? "" : rc;
        }

        public static string RemoveFromEnd(this string s, string suffix)
        {
            if (s.EndsWith(suffix))
            {
                return s.Substring(0, s.Length - suffix.Length);
            }
            else
            {
                return s;
            }
        }
    }
}