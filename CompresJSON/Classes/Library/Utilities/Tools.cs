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

        public static object ToObject(this IDictionary<string, object> source, object someObject)
        {
            //var someObject = new T();
            if (someObject != null)
            {
                var someObjectType = someObject.GetType();

                foreach (var item in source)
                {
                    var key = char.ToUpper(item.Key[0]) + item.Key.Substring(1);
                    var targetProperty = someObjectType.GetProperty(key);

                    if (targetProperty != null)
                    {
                        if (targetProperty.PropertyType.FullName == "System.String") // == typeof(string))
                        {
                            targetProperty.SetValue(someObject, item.Value);
                        }
                        else if (targetProperty.PropertyType.GenericTypeArguments.Count() > 0)
                        {
                            if (targetProperty.PropertyType.GenericTypeArguments.FirstOrDefault().FullName == "System.Int32")
                            {
                                targetProperty.SetValue(someObject, Convert.ToInt32(item.Value));
                            }
                        }
                        else
                        {

                            var parseMethod = targetProperty.PropertyType.GetMethod("TryParse",
                                BindingFlags.Public | BindingFlags.Static, null,
                                new[] { typeof(string), targetProperty.PropertyType.MakeByRefType() }, null);

                            if (parseMethod != null)
                            {
                                var parameters = new[] { item.Value, null };
                                var success = (bool)parseMethod.Invoke(null, parameters);
                                if (success)
                                {
                                    targetProperty.SetValue(someObject, parameters[1]);
                                }

                            }
                        }
                    }
                }

                return someObject;
            }

            return null;
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