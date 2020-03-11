using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Libs.Storage.Query
{
    /// <summary>
    /// Basic interface for the query object.
    /// </summary>
    /// <remarks>This is an implementation if the Query Object Pattern.</remarks>
    /// <typeparam name="T">Source or result of the query. Can be a domain entity or an api/view model.</typeparam>
    public interface IQuery<T>
    {
        IEnumerable<Expression<Func<T, bool>>> AllFilters { get; }
    }
}
