using System.Collections.Generic;

namespace Libs.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static string ToHumanString<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source, string separator = "; ")
        {
            return string.Join(separator, source);
        }

        public static string ToHumanString<T>(this ICollection<T> source, string separator = "; ")
        {
            return string.Join(separator, source);
        }
    }
}
