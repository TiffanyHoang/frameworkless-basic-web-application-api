using System;

namespace WebApplication
{
    public static class DateTimeManager
    {
        public static string GetCurrentTime()
        {
            var time = DateTime.Now.ToUniversalTime();
            string currentTimeFormat = "HH:mm";
            return time.ToString(currentTimeFormat);
        }
        public static string GetCurrentDate()
        {
            var time = DateTime.Now.ToUniversalTime();
            string currentDateFormat = "dd MMM yyyy";
            return time.ToString(currentDateFormat);
        }
    }
}
