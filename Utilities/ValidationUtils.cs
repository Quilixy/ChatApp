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
    }
}