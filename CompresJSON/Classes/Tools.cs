using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
    }
}