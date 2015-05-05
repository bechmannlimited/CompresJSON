using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompresJSON
{
    public class CompresJSONDefaults
    {
        public static string EncryptionKey = "password";

        public static CompressionMethod compressionMethod = CompressionMethod.LZ77;
        public static EncodingMethod encodingMethod = EncodingMethod.Base64;
        public static Encoding encoder = UTF8Encoding.UTF8;
    }
}