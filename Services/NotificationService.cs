using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class NotificationService
    {
        // Kullanıcıyı yeni mesaj hakkında bilgilendirme
        public static async Task NotifyNewMessage(string message)
        {
            // Gerçek uygulamada burada bildirim gönderebilirsin
            await Task.Delay(500); // Simülasyon için gecikme
            System.Diagnostics.Debug.WriteLine($"New message notification: {message}");
        }
    }
}