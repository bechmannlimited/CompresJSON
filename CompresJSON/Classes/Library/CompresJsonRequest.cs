using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public class CompresJsonRerouteDictionary: Dictionary<string, object>
    {
        public string data { get; set; }
        public string redirectToAction { get; set; }
        public string redirectToController { get; set; }

        public CompresJsonRerouteDictionary(string action, string controller)
        {
            redirectToAction = action;
            redirectToController = controller;
        }
    }
}