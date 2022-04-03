using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapaCidade.Test.Helpers
{
   public static class RandomHelper
    {
        private static readonly Random _random = new();
        public static int GetInt() => _random.Next();
        public static double GetDouble() => _random.Next();
        public static string GetString() => Guid.NewGuid().ToString();
        public static string GetString(int length)
        {
            var s = string.Empty;
            while (s.Length < length)
            {
                s += Guid.NewGuid().ToString();
                if (s.Length > length)
                {
                    s = s[..length];
                }
            }
            return s;
        }
    }
}
