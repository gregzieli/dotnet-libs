using System;

namespace Libs.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Indicates whether a <see cref="TimeSpan"/> value is between two other <see cref="TimeSpan"/> values.
        /// </summary>
        /// <param name="time">A value to compare to.</param>
        /// <param name="start">A value that starts the period.</param>
        /// <param name="end">A value that ends the period.</param>
        /// <returns></returns>
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
    }
}
