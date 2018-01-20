using System;

namespace Domain.Core.Services
{
    /// <summary>
    /// Обертка над DateTime с возможностью подмены текущего времени в целях тестирования
    /// </summary>
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

        /// <summary>
        /// Возвращает целое количество прошедних лет между двумя датами
        /// </summary>
        public static int PassedYears(DateTime startDate, DateTime endDate)
        {
            // По хорошему стояло для работы со вмеренем использовать готовую библиотеку вроде NodaTime
            return (endDate.Year - startDate.Year - 1) +
                   (((endDate.Month > startDate.Month) ||
                     ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }
    }
}