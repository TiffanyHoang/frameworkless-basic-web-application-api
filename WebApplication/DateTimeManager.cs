using System;

namespace WebApplication
{
    public class DateTimeManager
    {
        private DateTime time;
        public DateTimeManager() => time = new DateTime();
        public string GetCurrentTime()
        {
            time = DateTime.Now.ToUniversalTime();
            string currentTimeFormat = "HH:mm";
            return time.ToString(currentTimeFormat);
        }
        public string GetCurrentDate()
        {
            time = DateTime.Now.ToUniversalTime();
            string currentDateFormat = "dd MMM yyyy";
            return time.ToString(currentDateFormat);
        }
    }
}
