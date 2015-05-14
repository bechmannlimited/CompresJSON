using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using lz_string_csharp;
using System.IO;
using Jint;

namespace CompresJSON
{
    public class Compressor
    {
        //public static CompressedResult Compress(string str)
        //{
        //    byte[] dataBytes = CompresJSONSettings.encoder.GetBytes(str);
        //    byte[] compressedData = Converter.StringToBytes("");

        //    var compressionMethod = CompresJSONSettings.compressionMethod;
        //    var encodingMethod = CompresJSONSettings.encodingMethod;

        //    return Compress(str, compressionMethod, encodingMethod);
        //}

        //public static CompressedResult Compress(string str, CompressionMethod compressionMethod, EncodingMethod encodingMethod)
        //{
        //    byte[] dataBytes = CompresJSONSettings.encoder.GetBytes(str);
        //    byte[] compressedData = compressData(dataBytes, compressionMethod);

        //    var result = new CompressedResult();

        //    result.compressedOutput = compressedData;
        //    result.encodedOutput = Encoder.StringFromBytes(compressedData, encodingMethod);
        //    result.compressionMethod = compressionMethod;
        //    result.encodingMethod = encodingMethod;

        //    return result;
        //}

        //public static DecompressedResult Decompress(CompressedResult compressedResult)
        //{
        //    byte[] dataBytes = Encoder.BytesFromString(compressedResult.encodedOutput, compressedResult.encodingMethod);
        //    byte[] decompressedData = decompressData(dataBytes, compressedResult.compressionMethod);

        //    var result = new DecompressedResult();

        //    result.decompressedData = decompressedData;
        //    result.decompressedOutput = Converter.BytesToString(decompressedData);
        //    result.compressedResult = compressedResult;

        //    return result;
        //}

        //private static byte[] decompressData(byte[] data, CompressionMethod compressionMethod)
        //{
        //    var decompressedData = Converter.StringToBytes("");

        //    switch (compressionMethod)
        //    {
        //        case CompressionMethod.GZip:

        //            decompressedData = GZip.Decompress(data);
        //            break;

        //        case CompressionMethod.LZ77:

        //            decompressedData = LZ77.Decompress(data);
        //            break;

        //        default: break;
        //    }

        //    return decompressedData;
        //}

        //private static byte[] compressData(byte[] data, CompressionMethod compressionMethod)
        //{
        //    var compressedData = Converter.StringToBytes("");

        //    switch (compressionMethod)
        //    {
        //        case CompressionMethod.GZip:

        //            compressedData = GZip.Compress(data);
        //            break;

        //        case CompressionMethod.LZ77:

        //            compressedData = LZ77.Compress(data);
        //            break;


        //        //case CompressionMethod.LZString:
        //        //    compressedData = LZString.c;
        //        //    break;

        //        default:

        //            //compressedData = ZLib.Compress(data);

        //            break;
        //    }

        //    return compressedData;
        //}

        public static string Compress(string str)
        {
            StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Scripts/compresjson_scripts/encryptor_compressor.js");
            string script = streamReader.ReadToEnd();
            streamReader.Close();

            var js = new Engine()
                .SetValue("x", str) // define a new variable
                .Execute(script + " Compress(x);") // execute a statement
                .GetCompletionValue() // get the latest statement completion value
                .ToObject() // converts the value to .NET
            ;

            return js.ToString();

            var compressed = LZString.compress(str);
            var compressedData = Converter.StringToBytes(compressed);
            return Convert.ToBase64String(compressedData);
        }

        public static string Decompress(string str)
        {
            StreamReader streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Scripts/compresjson_scripts/encryptor_compressor.js");
            string script = streamReader.ReadToEnd();
            streamReader.Close();

            var js = new Engine()
                .SetValue("x", str) // define a new variable
                .Execute(script + " Decompress(x);") // execute a statement
                .GetCompletionValue() // get the latest statement completion value
                .ToObject() // converts the value to .NET
            ;

            return js.ToString();

            var compressedData = Convert.FromBase64String(str);
            var compressed = Converter.BytesToString(compressedData);
            return LZString.decompress(compressed);
        }

        
    }
}
