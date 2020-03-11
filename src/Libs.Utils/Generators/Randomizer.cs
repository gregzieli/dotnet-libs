using System;
using System.Linq;

namespace Libs.Utils.Generators
{
    public static class Randomizer
    {
        private static readonly Random Random = new Random();

        public static string RandomString(int length)
          => new string(Enumerable.Repeat(Charsets.Alphabetic, length).Select(s => s[Random.Next(s.Length)]).ToArray());

        public static string RandomString(string pattern, char wildcard, string charset)
        {
            var chars = pattern.Select(x => x == wildcard ? charset[Random.Next(charset.Length)] : x);
            return new string(chars.ToArray());
        }


        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minVal">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxVal">The INCLUSIVE upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less OR EQUAL TO maxValue.</returns>
        public static int RandomInt(int minVal, int maxVal)
          => Random.Next(minVal, maxVal + 1);
    }
}
