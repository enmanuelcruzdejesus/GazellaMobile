using System;
using System.Text;
namespace GazellaMobile.Utils.System
{
    public static class StringHelper
    {
        public static string EncodeBase64(this string plainText)
        {
            var base64EncodedBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(base64EncodedBytes);
        }

        public static string DecodedBase64(this string encodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(encodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes,0,base64EncodedBytes.Length);
        }
    }
}
