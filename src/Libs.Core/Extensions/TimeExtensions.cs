using System;

namespace Libs.Core.Extensions
{
    public static class TimeExtensions
    {
        /// <summary>
        /// Indicates whether a time value is between two other time values, bounds included.
        /// </summary>
        /// <remarks>
        /// Since time is continuous, to create the 24h range <c start/> must equal <c end/>.
        /// </remarks>
        /// <param name="time">A value to compare to.</param>
        /// <param name="start">A value that starts the period.</param>
        /// <param name="end">A value that ends the period.</param>
        /// <returns>true, if the given <see cref="TimeSpan"/> is in range of the other two values.</returns>
        public static bool IsBetween(this TimeSpan time, TimeSpan start, TimeSpan end)
        {
            return start < end
                ? start <= time && time <= end
                : !(end < time && time < start);
        }

        /// <summary>
        /// Calculates the difference between two <see cref="TimeSpan"/>s, where the target is in the future.
        /// </summary>
        /// <param name="time">Start point.</param>
        /// <param name="target">Future point.</param>
        /// <returns>The amount of time from the starting point to the future point.</returns>
        public static TimeSpan GetRemaining(this TimeSpan time, TimeSpan target)
        {
            var difference = target - time;

            if (target < time)
            {
                // negative result
                difference = TimeSpan.FromDays(1) - difference.Duration();
            }

            return difference;
        }

        /// <summary>
        /// Converts the value of the <see cref="TimeSpan"/> object into its cron representation.
        /// </summary>
        /// <param name="time"><see cref="TimeSpan"/> to convert</param>
        /// <returns>Given time in the cron format.</returns>
        public static string ToCron(this TimeSpan time)
            => $"{time:m\\ h} * * *";

        public static bool IsAfter(this DateTime dateTime, DateTime start, bool inclusive = true)
            => (inclusive && dateTime == start) || dateTime > start;

        public static bool IsAfter(this DateTime dateTime, DateTime? start, bool inclusive = true)
            => !start.HasValue || dateTime.IsAfter(start.Value, inclusive);

        public static bool IsBefore(this DateTime dateTime, DateTime end, bool inclusive = true)
            => (inclusive && dateTime == end) || dateTime < end;

        public static bool IsBefore(this DateTime dateTime, DateTime? end, bool inclusive = true)
            => !end.HasValue || dateTime.IsBefore(end.Value, inclusive);
    }
}
