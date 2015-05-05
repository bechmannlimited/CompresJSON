using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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

        //public static Dictionary<string, object> ConvertToDictionary(object obj)
        //{
        //    return obj.GetType()
        //    .GetProperties()
        //    .Select(pi => new { Name = pi.Name, Value = pi.GetValue(this, null) })
        //    .Union(
        //        obj.GetType()
        //        .GetFields()
        //        .Select(fi => new { Name = fi.Name, Value = fi.GetValue(this) })
        //     )
        //    .ToDictionary(ks => ks.Name, vs => vs.Value);
        //}

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

                        if (targetProperty.PropertyType == typeof(string))
                        {
                            targetProperty.SetValue(someObject, item.Value);
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


        public static void setParametersFromDictionary(object obj, Dictionary<string, object> dict)
        {

        }
    }
}