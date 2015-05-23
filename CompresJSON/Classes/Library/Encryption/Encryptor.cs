using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AESCryptoTest;

namespace CompresJSON
{
    public class Encryptor
    {

        public static string Encrypt(string str)
        {
            //Dictionary<string, object> args = new Dictionary<string, object>() {
            //    { "x" , str },
            //    { "y", CompresJSONSettings.EncryptionKey}
            //};
            //return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Encrypt", args).ToString();

            var p = new CryptoJS();
            var encrypted = p.OpenSSLEncrypt(str, CompresJSONSettings.EncryptionKey);
            return Converter.Base64Encode(encrypted);
        }

        public static string Decrypt(string str)
        {
            //Dictionary<string, object> args = new Dictionary<string, object>() {
            //    { "x" , str },
            //    { "y", CompresJSONSettings.EncryptionKey}
            //};
            //return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Decrypt", args).ToString();

            var p = new CryptoJS();
            var decoded = Converter.Base64Decode(str);
            return p.OpenSSLDecrypt(decoded, CompresJSONSettings.EncryptionKey);
        }
      

    }
}