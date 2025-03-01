using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class ChatService
    {
        // Kişisel sohbet mesajı gönderme
        public static async Task SendMessage(string encryptedMessage)
        {
            // Burada şifreli mesajın hedefe gönderilmesi işlemi yapılır.
            // Örnek: WebSocket veya SignalR kullanarak mesaj iletme
            await Task.Delay(500); // Simülasyon için gecikme
            // Mesaj gönderildiği bildirisi
            System.Diagnostics.Debug.WriteLine($"Message sent: {encryptedMessage}");
        }

        // Grup sohbeti için mesaj gönderme
        public static async Task SendGroupMessage(string encryptedMessage)
        {
            // Aynı şekilde grup mesajları da burada gönderilebilir
            await Task.Delay(500); // Simülasyon için gecikme
            // Grup mesajı gönderildiği bildirisi
            System.Diagnostics.Debug.WriteLine($"Group message sent: {encryptedMessage}");
        }
    }
}