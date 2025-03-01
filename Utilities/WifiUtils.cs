using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Utilities
{
    public static class WifiUtils
    {
        // Wi-Fi ağına bağlı olup olmadığını kontrol eder
        public static bool IsWifiConnected()
        {
            var connectivity = Microsoft.Maui.Networking.Connectivity.NetworkAccess;
            return connectivity == Microsoft.Maui.Networking.NetworkAccess.Internet;
        }
    }
}
