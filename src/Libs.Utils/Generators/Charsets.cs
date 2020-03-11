namespace Libs.Utils.Generators
{
    public static class Charsets
    {
        public const string Numeric = "0123456789";

        public const string Alphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const string AlphaNumeric = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Consists only of characters easily distinguishable from one another
        /// </summary>
        public const string SafeAlphaNumeric = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
    }
}
