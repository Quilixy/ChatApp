using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Utilities
{
    public static class DateTimeUtils
    {
        // Şu anki zamanı belirli bir formatta döndürür
        public static string GetFormattedCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // Tarih farkını gün cinsinden hesaplar
        public static int GetDateDifferenceInDays(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days;
        }

        // Tarihi belirli bir formatta döndürür
        public static string FormatDate(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }
    }
}