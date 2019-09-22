using System;

namespace WebStore.Infrasructure.Helpers
{
    public class DateHelper
    {
        /// <summary>
        /// Возвращает количество лет между двумя датами
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static int GetAge(DateTime d1, DateTime d2)
        {
            var r = d2.Year - d1.Year;
            return d1.AddYears(r) <= d2 ? r : r - 1;
        }
    }
}
