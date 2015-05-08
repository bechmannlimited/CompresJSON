using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompresJSON
{
    public class CompresJSONSettings
    {
        public static string EncryptionKey = "1234567891123456";

        public static CompressionMethod compressionMethod = CompressionMethod.LZ77;
        public static EncodingMethod encodingMethod = EncodingMethod.Base64;
        public static Encoding encoder = UTF8Encoding.UTF8;
    }
}