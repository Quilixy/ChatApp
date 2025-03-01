using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class EncryptionService
    {
        // Mesajı şifrelemek
        public static async Task<string> EncryptMessage(string message)
        {
            // Burada Playfair algoritması ile şifreleme işlemi yapılabilir
            // Örnek olarak mesajın tersini döndürüyoruz
            await Task.Delay(100); // Simülasyon amaçlı bekleme
            char[] messageArray = message.ToCharArray();
            Array.Reverse(messageArray);
            return new string(messageArray); // Şifreli mesaj
        }

        // Şifreyi çözme
        public static async Task<string> DecryptMessage(string encryptedMessage)
        {
            // Burada Playfair algoritması ile çözme yapılabilir
            // Örnek olarak tersini çözerek geri alıyoruz
            await Task.Delay(100); // Simülasyon amaçlı bekleme
            char[] messageArray = encryptedMessage.ToCharArray();
            Array.Reverse(messageArray);
            return new string(messageArray); // Çözülmüş mesaj
        }
    }
}