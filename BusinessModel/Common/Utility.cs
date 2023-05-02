using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BusinessModel.Common
{
    public class Utility
    {
        public static string GetStan()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString();
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                if (IsNumeric(lstrString.Substring(lintCnt, 1)) == true)
                {
                    lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                    if (lstrNumber.Length > 5)
                    {
                        break;
                    }
                }
            }
            return lstrNumber.PadRight(6, '0').ToString();
        }

        public static string GetAlphaChar()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                if (IsNumeric(lstrString.Substring(lintCnt, 1)) != true)
                {
                    lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                    if (lstrNumber.Length > 5)
                    {
                        break;
                    }
                }
            }
            return lstrNumber.PadRight(6, 's').ToString();
        }

        public static string GetAESKEY()
        {
            string lstrString = "";
            string lstrNumber = "";
            int lintCnt = 0;

            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            for (lintCnt = 1; lintCnt <= lstrString.Length - 1; lintCnt++)
            {
                lstrNumber = lstrNumber + lstrString.Substring(lintCnt, 1);
                if (lstrNumber.Length > 15)
                {
                    break;
                }
            }
            return lstrNumber.PadRight(16, '0').ToString();
        }

        public static string GetConsumerSecret()
        {
            string lstrString = "";
            lstrString = Guid.NewGuid().ToString().Replace("-", "");
            return lstrString;
        }

        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        public static string ConvertToJulian()
        {
            DateTime firstJan = new DateTime(DateTime.Now.Year, 1, 1);
            string daysSinceFirstJan = Convert.ToString((DateTime.Now - firstJan).Days + 1);
            return DateTime.Now.Year.ToString().Substring(3, 1) + daysSinceFirstJan.PadLeft(3, '0') + DateTime.Now.ToString("HH");
        }

        public static string GetTransID()
        {
            return ConvertToJulian() + GetStan() + DateTime.Now.ToString("ssffff");
        }

        public static string GetCurrentMilliSec()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        }
        private static readonly Random _rdm = new Random();
        public static string RanNum(int digits)
        {
            if (digits <= 1) return "";

            var _min = (int)Math.Pow(10, digits - 1);
            var _max = (int)Math.Pow(10, digits) - 1;
            return _rdm.Next(_min, _max).ToString();
        }

        public static byte[] HashHMAC256(string message, string key)
        {
            var hash = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(key));
            return hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
        }

        public static byte[] HashHMAC512(string message, string key)
        {
            var hash = new HMACSHA512(System.Text.Encoding.UTF8.GetBytes(key));
            return hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(message));
        }
    }
}






