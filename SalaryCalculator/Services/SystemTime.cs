using System;

namespace Domain.Core.Services
{
    public class SystemTime
    {
        private static DateTime _date;

        public static void Set(DateTime date)
        {
            _date = date;
        }

        public static void Reset()
        {
            _date = DateTime.MinValue;
        }

        public static DateTime Now => _date != DateTime.MinValue ? _date : DateTime.Now;
    }
}