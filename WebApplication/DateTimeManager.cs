using System;

namespace WebApplication
{
    public class DateTimeManager
    {
        private DateTime time;
        public DateTimeManager() => time = new DateTime();
        public string GetCurrentTime()
        {
            time = DateTime.Now;
            string currentTimeFormat = "HH:mm";
            return time.ToString(currentTimeFormat);
        }
        public string GetCurrentDate()
        {
            time = DateTime.Now;
            string currentDateFormat = "dd MMM yyyy";
            return time.ToString(currentDateFormat);
        }
    }
}
