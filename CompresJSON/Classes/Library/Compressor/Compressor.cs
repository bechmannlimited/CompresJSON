﻿using System;
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

        public static string Compress(string str)
        {
            if (CompresJSONSettings.compressionMethod == CompressionMethod.LZ77)
            {
                var data = Converter.StringToBytes(str);
                var compressedData = LZ77.Compress(data);
                return Convert.ToBase64String(compressedData);
            }

            else if (CompresJSONSettings.compressionMethod == CompressionMethod.GZip)
            {
                var data = Converter.StringToBytes(str);
                var compressedData = GZip.Compress(data);
                return Convert.ToBase64String(compressedData);
            }

            else if (CompresJSONSettings.compressionMethod == CompressionMethod.LZString)
            {
                Dictionary<string, object> args = new Dictionary<string, object>() {
                    { "x" , str }
                };
                return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Compress", args).ToString();
            }
            else if (CompresJSONSettings.compressionMethod == CompressionMethod.GZipAndLZString) 
            {
                var data = Converter.StringToBytes(str);
                var compressedData = GZip.Compress(data);
                return Convert.ToBase64String(compressedData);
            }

            return "";
        }

        public static string Decompress(string str)
        {
            if (CompresJSONSettings.compressionMethod == CompressionMethod.LZ77)
            {
                var data = Convert.FromBase64String(str);
                var decompressedData = LZ77.Decompress(data);
                return Converter.BytesToString(decompressedData);
            }

            else if (CompresJSONSettings.compressionMethod == CompressionMethod.GZip)
            {
                var data = Convert.FromBase64String(str);
                var decompressedData = GZip.Decompress(data);
                return Converter.BytesToString(decompressedData);
            }

            else if (CompresJSONSettings.compressionMethod == CompressionMethod.LZString)
            {
                Dictionary<string, object> args = new Dictionary<string, object>() {
                    { "x" , str }
                };
                return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Decompress", args).ToString();
            }
            else if (CompresJSONSettings.compressionMethod == CompressionMethod.GZipAndLZString)
            {
                Dictionary<string, object> args = new Dictionary<string, object>() {
                    { "x" , str }
                };
                return JavaScriptAnalyzer.runJavaScriptFunctionWithArgs("Decompress", args).ToString();
            }

            return "";
        } 
    }
}
