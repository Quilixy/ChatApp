using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Utilities
{
    public static class ValidationUtils
    {
        // Boş alanları kontrol eder
        public static bool IsNotEmpty(string input, string fieldName)
        {
            if (string.IsNullOrEmpty(input))
            {
                AlertUtils.ShowErrorAsync($"{fieldName} cannot be empty.");
                return false;
            }
            return true;
        }

        // Geçerli bir telefon numarası olup olmadığını kontrol eder
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Telefon numarası doğrulama regex
            var phoneRegex = @"^\d{10}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, phoneRegex))
            {
                AlertUtils.ShowErrorAsync("Invalid phone number.");
                return false;
            }
            return true;
        }
    }
}