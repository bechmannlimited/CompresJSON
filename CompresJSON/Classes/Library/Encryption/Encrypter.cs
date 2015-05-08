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
            return EncryptedString.EncryptString(str, CompresJSONSettings.EncryptionKey);
        }

        public static string Decrypt(string str)
        {
            return EncryptedString.DecryptString(str, CompresJSONSettings.EncryptionKey);
        }
      

    }
}