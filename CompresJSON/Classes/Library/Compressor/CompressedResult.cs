using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompresJSON
{
    public class CompressedResult
    {
        public string originalString = "";

        public CompressionMethod compressionMethod = CompressionMethod.None;
        public EncodingMethod encodingMethod = EncodingMethod.None;

        public byte[] compressedOutput { get; set; }
        public string encodedOutput = "";
    }

    public class DecompressedResult
    {
        public CompressedResult compressedResult;
        public byte[] decompressedData { get; set; }
        public string decompressedOutput = "";
    }
}
