using System;
using System.Collections.Generic;

namespace Libs.Core
{
    public struct Interval<T> : IFormattable, IEquatable<Interval<T>> where T : struct, IComparable<T>, IFormattable
    {
        public T? Start { get; }

        public T? End { get; set; }

        public bool IsLeftClosed { get; }

        public bool IsRightClosed { get; }

        public Interval(T? start, T? end, bool leftInclusive = false, bool rightInclusive = false)
        {
            Start = start;
            End = end;
            IsLeftClosed = leftInclusive;
            IsRightClosed = rightInclusive;
        }

        public bool IsInRange(T value)
            => IsGreaterThanStart(value) && IsLessThanEnd(value);

        public override string ToString()
            => ToString(null, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var leftValue = Start.HasValue ? Start.Value.ToString(format, formatProvider) : "-∞";
            var rightValue = End.HasValue ? End.Value.ToString(format, formatProvider) : "+∞";
            return $"{(IsLeftClosed ? '[' : '(')}{leftValue}, {rightValue}{(IsRightClosed ? ']' : ')')}";
        }

        private bool IsGreaterThanStart(T value)
            => !Start.HasValue || (IsLeftClosed && value.Equals(Start.Value)) || value.CompareTo(Start.Value) > 0;

        private bool IsLessThanEnd(T value)
            => !End.HasValue || (IsRightClosed && value.Equals(End.Value)) || value.CompareTo(End.Value) < 0;

        public override bool Equals(object obj)
        {
            return obj is Interval<T> interval && Equals(interval);
        }

        public bool Equals(Interval<T> other)
        {
            return EqualityComparer<T?>.Default.Equals(Start, other.Start) &&
                   EqualityComparer<T?>.Default.Equals(End, other.End) &&
                   IsLeftClosed == other.IsLeftClosed &&
                   IsRightClosed == other.IsRightClosed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End, IsLeftClosed, IsRightClosed);
        }

        public static bool operator ==(Interval<T> left, Interval<T> right) => left.Equals(right);

        public static bool operator !=(Interval<T> left, Interval<T> right) => !(left == right);
    }
}
