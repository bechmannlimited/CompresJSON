using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompresJSON
{
    public class CompresJSONSettings
    {
        public static string EncryptionKey = "7e4bac048ef766e83f0ec8c079e1f90c2eb690a9";

        public static bool shouldCompress = true;
        public static bool shouldEncrypt = true;

        public static CompressionMethod compressionMethod = CompressionMethod.GZip;
        public static string DateFormat = DateFormatter.Date;
    }
}