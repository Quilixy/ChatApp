using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Utilities
{
    public static class StringUtils
    {
        // Boşlukları temizler ve string'i normalize eder
        public static string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input.Trim().ToLower();
        }
    

        // Şifreyi doğrulama
        public static bool IsValidPassword(string password)
        {
            // En az bir rakam, bir büyük harf, bir küçük harf ve minimum 8 karakter içermeli
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit);
        }
    }
}