using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompresJSON
{
    public class CompresJSONSettings
    {
        public static string EncryptionKey = "7e4bac048ef766e83f0ec8c079e1f90c2eb690a9"; //"1234567891123456";

        public static bool shouldCompress = true;
        public static bool shouldEncrypt = true;

        public static CompressionMethod compressionMethod = CompressionMethod.LZ77;
        public static EncodingMethod encodingMethod = EncodingMethod.Base64;
        public static Encoding encoder = UTF8Encoding.UTF8;
    }
}