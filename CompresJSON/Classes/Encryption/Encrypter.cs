using com.pakhee.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public class Encrypter
    {

        public static string Encrypt(string str){

            CryptLib _crypt = new CryptLib();
            string plainText = str;
            String iv = "EzJ9RvaqpLf-OZSR"; // CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
            string key = CryptLib.getHashSha256(CompresJSONDefaults.EncryptionKey, 32); //32 bytes = 256 bits


            String cypherText = _crypt.encrypt(plainText, key, iv);
            Console.WriteLine("iv=" + iv);
            Console.WriteLine("key=" + key);
            Console.WriteLine("Cypher text=" + cypherText);
            Console.WriteLine("Plain text =" + _crypt.decrypt(cypherText, key, iv));

            var outasdf = _crypt.decrypt(cypherText, key, iv);

            return cypherText;
        }

        public static string Decrypt(string str)
        {
            CryptLib _crypt = new CryptLib();
            string cypherText = str;
            String iv = "EzJ9RvaqpLf-OZSR"; //CryptLib.GenerateRandomIV(16); //16 bytes = 128 bits
            string key = CryptLib.getHashSha256(CompresJSONDefaults.EncryptionKey, 32); //32 bytes = 256 bits

            return _crypt.decrypt(cypherText, key, iv);
        }
        

    }
}