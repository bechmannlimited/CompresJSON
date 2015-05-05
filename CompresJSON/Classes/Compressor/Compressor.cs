using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace CompresJSON
{
    public class Compressor
    {
        public static CompressedResult Compress(string str)
        {
            byte[] dataBytes = CompresJSONDefaults.encoder.GetBytes(str);
            byte[] compressedData = Converter.StringToBytes("");

            var compressionMethod = CompresJSONDefaults.compressionMethod;
            var encodingMethod = CompresJSONDefaults.encodingMethod;

            return Compress(str, compressionMethod, encodingMethod);
        }

        public static CompressedResult Compress(string str, CompressionMethod compressionMethod, EncodingMethod encodingMethod)
        {
            byte[] dataBytes = CompresJSONDefaults.encoder.GetBytes(str);
            byte[] compressedData = compressData(dataBytes, compressionMethod);

            var result = new CompressedResult();

            result.compressedOutput = compressedData;
            result.encodedOutput = Encoder.StringFromBytes(compressedData, encodingMethod);
            result.compressionMethod = compressionMethod;
            result.encodingMethod = encodingMethod;

            return result;
        }

        public static DecompressedResult Decompress(CompressedResult compressedResult)
        {
            byte[] dataBytes = Encoder.BytesFromString(compressedResult.encodedOutput, compressedResult.encodingMethod);
            byte[] decompressedData = decompressData(dataBytes, compressedResult.compressionMethod);

            var result = new DecompressedResult();

            result.decompressedData = decompressedData;
            result.decompressedOutput = Converter.BytesToString(decompressedData);
            result.compressedResult = compressedResult;

            return result;
        }

        private static byte[] decompressData(byte[] data, CompressionMethod compressionMethod)
        {
            var decompressedData = Converter.StringToBytes("");

            switch (compressionMethod)
            {
                case CompressionMethod.GZip:

                    decompressedData = GZip.Decompress(data);
                    break;

                case CompressionMethod.LZ77:

                    decompressedData = LZ77.Decompress(data);
                    break;

                default: break;
            }

            return decompressedData;
        }

        private static byte[] compressData(byte[] data, CompressionMethod compressionMethod)
        {
            var compressedData = Converter.StringToBytes("");

            switch (compressionMethod)
            {
                case CompressionMethod.GZip:

                    compressedData = GZip.Compress(data);
                    break;

                case CompressionMethod.LZ77:

                    compressedData = LZ77.Compress(data);
                    break;

                default: 
                    break;
            }

            return compressedData;
        }
    }
}
