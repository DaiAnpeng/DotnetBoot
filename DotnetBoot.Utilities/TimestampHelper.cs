using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetBoot.Utilities
{
    public static class TimestampHelper
    {
        private static readonly DateTime utc = DateTime.Parse("1970-1-1 0:0:0");
        public static long ToLongTimestamp(this DateTime time, out string error)
        {
            error = null;
            try
            {
                return time.Subtract(utc.ToLocalTime()).Milliseconds;
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
                return time.Subtract(utc.ToLocalTime()).Seconds;
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
                return utc.AddMilliseconds(timestamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return utc;
            }
        }
        public static DateTime ToTime(this int timestamp, out string error)
        {
            error = null;
            try
            {
                return utc.AddSeconds(timestamp).ToLocalTime();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return utc;
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
