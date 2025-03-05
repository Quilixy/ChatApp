using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class WiFiService
    {
        // Wi-Fi üzerinden yakın kullanıcıları bulma (simülatif)
        public static async Task<List<string>> GetNearbyUsers()
        {
            // Burada Wi-Fi üzerinden bağlanmış cihazları tespit etme işlemi simüle ediliyor.
            // Gerçek uygulamada burada Wi-Fi bağlantısı üzerinden cihazları tespit edebilirsin.

            await Task.Delay(1000); // Simülasyon amaçlı bekleme
            return new List<string> { "User1", "User2", "User3", "User4","User5" }; // Simüle edilmiş kullanıcı listesi
        }
    }
}
