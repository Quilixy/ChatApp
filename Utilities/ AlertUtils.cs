using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ChatApp.Utilities
{
    public static class AlertUtils
    {
        
        public static async Task ShowAlertAsync(string title, string message, string buttonText = "OK")
        {
            await Application.Current.MainPage.DisplayAlert(title, message, buttonText);
        }
        
        public static async Task ShowErrorAsync(string message)
        {
            await ShowAlertAsync("Error", message, "Close");
        }
        
        public static async Task ShowSuccessAsync(string message)
        {
            await ShowAlertAsync("Success", message, "Great");
        }
    }
}