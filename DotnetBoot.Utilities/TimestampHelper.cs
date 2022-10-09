using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetBoot.Utilities
{
    public static class TimestampHelper
    {
        private static readonly DateTime utcZero = new DateTime(1970, 1, 1, 0, 0, 0);
        public static long ToLongTimestamp(this DateTime time, out string error)
        {
            error = null;
            try
            {
                return Convert.ToInt64(time.Subtract(utcZero).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return 0;
            }
        }
        public static int ToTimestamp(this DateTime time, out string error)
        {
            error = null;
            try
            {
                return Convert.ToInt32(time.Subtract(utcZero).TotalSeconds);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return 0;
            }
        }
        public static DateTime ToTime(this long timestamp, out string error)
        {
            error = null;
            try
            {
                return utcZero.AddMilliseconds(timestamp);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return utcZero;
            }
        }
        public static DateTime ToTime(this int timestamp, out string error)
        {
            error = null;
            try
            {
                return utcZero.AddSeconds(timestamp);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return utcZero;
            }
        }
        public enum TimeZoneEnum
        {
            UTC,
            GMT,
            CST,
            CET
        }
    }
}
