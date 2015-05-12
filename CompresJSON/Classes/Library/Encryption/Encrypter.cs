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
            var p = new CryptoJS();
            return p.OpenSSLEncrypt(str, CompresJSONSettings.EncryptionKey);
            //return EncryptedString.EncryptString(str, CompresJSONSettings.EncryptionKey);
        }

        public static string Decrypt(string str)
        {
            var p = new CryptoJS();
            return p.OpenSSLDecrypt(str, CompresJSONSettings.EncryptionKey);
            //return EncryptedString.DecryptString(str, CompresJSONSettings.EncryptionKey);
        }
      

    }
}