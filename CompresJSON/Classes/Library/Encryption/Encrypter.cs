using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AESCryptoTest;

namespace CompresJSON
{
    public class Encrypter
    {

        public static string Encrypt(string str)
        {
            //Dictionary<string, object> args = new Dictionary<string, object>() {
            //    { "x" , str },
            //    { "y", CompresJSONSettings.EncryptionKey}
            //};
            //return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Encrypt", args).ToString();

            var p = new CryptoJS();
            return p.OpenSSLEncrypt(str, CompresJSONSettings.EncryptionKey);
        }

        public static string Decrypt(string str)
        {
            //Dictionary<string, object> args = new Dictionary<string, object>() {
            //    { "x" , str },
            //    { "y", CompresJSONSettings.EncryptionKey}
            //};
            //return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Decrypt", args).ToString();
            var p = new CryptoJS();
            return p.OpenSSLDecrypt(str, CompresJSONSettings.EncryptionKey);
        }
      

    }
}