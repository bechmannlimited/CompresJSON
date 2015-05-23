using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public class CompresJSON
    {
        
        public static string EncryptAndCompressAsNecessary(string str)
        {
            //compress

            if (CompresJSONSettings.shouldCompress)
            {
                str = Compressor.Compress(str);
            }

            //encrypt
            if (CompresJSONSettings.shouldEncrypt)
            {
                str = Encryptor.Encrypt(str);
            }


            return str;
        }

        public static string DecryptAndDecompressAsNecessary(string str)
        {
            //decrypt
            if (CompresJSONSettings.shouldEncrypt)
            {
                str = Encryptor.Decrypt(str);
            }

            //decompress
            if (CompresJSONSettings.shouldCompress)
            {
                str = Compressor.Decompress(str);
            }

            return str;
        }

    }
}