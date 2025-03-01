using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ChatApp.Utilities
{
    public static class AlertUtils
    {
        // Kullanıcıya bir uyarı mesajı gösterir
        public static async Task ShowAlertAsync(string title, string message, string buttonText = "OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
        }

        // Kullanıcıya bir hata mesajı gösterir
        public static async Task ShowErrorAsync(string message)
        {
            await ShowAlertAsync("Error", message, "Close");
        }

        // Kullanıcıya başarı mesajı gösterir
        public static async Task ShowSuccessAsync(string message)
        {
            await ShowAlertAsync("Success", message, "Great");
        }
    }
}