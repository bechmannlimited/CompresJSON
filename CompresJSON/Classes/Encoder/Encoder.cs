using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CompresJSON
{
    public class Encoder
    {

        public static string StringFromBytes(byte[] data, EncodingMethod encodingMethod)
        {
            var rc = "";

            switch (encodingMethod)
            {
                case EncodingMethod.None:
                    break;
                case EncodingMethod.ASCII:

                    rc = Encoding.ASCII.GetString(data);

                    break;
                case EncodingMethod.Base64:

                    rc = Convert.ToBase64String(data);

                    break;

                case EncodingMethod.UTF8:

                    rc = Encoding.UTF8.GetString(data);

                    break;

                default:
                    break;
            }

            return rc;
        }

        public static byte[] BytesFromString(string str, EncodingMethod encodingMethod)
        {
            var rc = Converter.StringToBytes("");

            switch (encodingMethod)
            {
                case EncodingMethod.None:
                    break;

                case EncodingMethod.ASCII:

                    rc = Encoding.ASCII.GetBytes(str);
                    break;

                case EncodingMethod.Base64:

                    rc = Convert.FromBase64String(str);
                    break;

                case EncodingMethod.UTF8:

                    rc = Encoding.UTF8.GetBytes(str);
                    break;

                default:
                    break;
            }

            return rc;
        }

    }
}