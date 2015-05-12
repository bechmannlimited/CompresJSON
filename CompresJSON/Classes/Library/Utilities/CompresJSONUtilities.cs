using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompresJSON
{
    public class CompresJSONUtilities
    {
        
        public static string EncryptAndCompressAsNecessary(string str)
        {
            //compress
            string compressedString = Compressor.Compress(str);

            //encrypt
            return Encrypter.Encrypt(compressedString);

            ////encrypt
            //var encryptedString = Encrypter.Encrypt(str);

            ////compress
            //return Compressor.Compress(encryptedString).encodedOutput;
        }

        public static string DecryptAndDecompressAsNecessary(string str)
        {
            //decrypt
            string decryptedString = Encrypter.Decrypt(str);

            //decompress
            //return Compressor.Decompress(new CompressedResult
            //{
            //    encodingMethod = CompresJSONSettings.encodingMethod,
            //    compressionMethod = CompresJSONSettings.compressionMethod,
            //    encodedOutput = decryptedString
            //}).decompressedOutput;
            return Compressor.Decompress(decryptedString);

            ////decompress
            //var decompressedString = Compressor.Decompress(new CompressedResult
            //{
            //    encodingMethod = CompresJSONSettings.encodingMethod,
            //    compressionMethod = CompresJSONSettings.compressionMethod,
            //    encodedOutput = str
            //}).decompressedOutput;

            ////decrypt
            //return Encrypter.Decrypt(decompressedString);
        }

    }
}