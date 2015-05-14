using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using lz_string_csharp;
using System.IO;
using Jint;

namespace CompresJSON
{
    public class Compressor
    {

        public static string Compress(string str)
        {
            Dictionary<string, object> args = new Dictionary<string, object>() {
                { "x" , str }
            };
            return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Compress", args).ToString();
        }

        public static string Decompress(string str)
        {
            Dictionary<string, object> args = new Dictionary<string, object>() {
                { "x" , str }
            };
            return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Decompress", args).ToString();
        }

        
    }
}
