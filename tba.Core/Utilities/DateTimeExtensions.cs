using System;

namespace tba.Core.Utilities
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert a DateTime to Unix timestamp (seconds since the Unix Epoch)
        /// </summary>
        /// <param name="date">A System.DateTime</param>
        /// <returns>long unix timestamp</returns>
        public static long ToUnixTimestamp(this DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

        /// <summary>
        /// Convert Unix timestamp (seconds since the Unix Epoch) to System.DateTime
        /// </summary>
        /// <param name="unixTimestamp">long unix timestamp</param>
        /// <returns>A System.DateTime</returns>
        public static DateTime FromUnixTimestamp(long unixTimestamp)
        {
            var unixYear0 = new DateTime(1970, 1, 1);
            var unixTimeStampInTicks = unixTimestamp * TimeSpan.TicksPerSecond;
            var dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }
        /// <summary>
        /// Convert Unix timestamp (seconds since the Unix Epoch) to System.DateTime
        /// </summary>
        /// <param name="unixTimestamp">long unix timestamp</param>
        /// <returns>A System.DateTime</returns>
        public static DateTime? FromUnixTimestamp(long? unixTimestamp)
        {
            if (unixTimestamp == null) return null;
            return FromUnixTimestamp(unixTimestamp.Value);
        }
    }
}