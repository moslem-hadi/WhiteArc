using System;
using System.Net;
using System.Text;

namespace DNT.Extensions
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Count all words in a given string. 
        /// Raya Farayand Method, Example :
        /// Uri longUri = new Uri("http://www.google.com");
        /// Uri shortUri = longUri.ToTiny();
        /// </summary>
        /// <param name="longUri">Uri</param>
        /// <returns>Uri</returns>
        public static Uri ToTiny(this Uri longUri)
        {
            var request =
                WebRequest.Create(String.Format("http://tinyurl.com/api-create.php?url={0}",
                                                UrlEncode(longUri.ToString())));
            var response = request.GetResponse();
            Uri returnUri;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                returnUri = new Uri(reader.ReadToEnd());
            }
            return returnUri;
        }

        private static string UrlEncode(string str)
        {
            return str == null ? null : UrlEncode(str, Encoding.UTF8);
        }

        private static string UrlEncode(string str, Encoding e)
        {
            return str == null ? null : Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }

        private static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            var bytes = e.GetBytes(str);
            return UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
        }

        private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
        {
            var num = 0;
            var num2 = 0;
            for (int i = 0; i < count; i++)
            {
                var ch = (char)bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsSafe(ch))
                {
                    num2++;
                }
            }
            if ((!alwaysCreateReturnValue && (num == 0)) && (num2 == 0))
            {
                return bytes;
            }
            var buffer = new byte[count + (num2 * 2)];
            var num4 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                var ch2 = (char)num6;
                if (IsSafe(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num4++] = 0x2b;
                }
                else
                {
                    buffer[num4++] = 0x25;
                    buffer[num4++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte)IntToHex(num6 & 15);
                }
            }
            return buffer;
        }

        internal static bool IsSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '\'':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }

        internal static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x61);
        }
        
    }
}
