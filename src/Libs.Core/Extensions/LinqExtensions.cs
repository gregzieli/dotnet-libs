using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on multiple predicates evaluated in an "AndAlso" fashion.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to filter.</param>
        /// <param name="predicates">Functions to test each element for a different condition.</param>
        /// <returns>An <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the conditions specified by the predicates.</returns>
        /// <exception cref="ArgumentNullException">source or predicate is null</exception>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, IEnumerable<Expression<Func<TSource, bool>>> predicates)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicates is null)
            {
                throw new ArgumentNullException(nameof(predicates));
            }

            foreach (var predicate in predicates)
            {
                source = source.Where(predicate);
            }

            return source;
        }
    }
}
