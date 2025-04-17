using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class WiFiService
    {
        public static string GetLocalIpAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    !ni.Description.ToLower().Contains("virtual") && 
                    !ni.Name.ToLower().Contains("virtual") &&
                    !ni.Description.ToLower().Contains("vmware"))
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            throw new Exception("IPv4 adresi bulunamadı!");
        }

        public static async Task<List<(string Ip, string Hostname)>> GetNearbyUsers()
        {
            string subnet = "192.168.1";
            List<(string Ip, string Hostname)> activeDevices = new();
            List<Task> tasks = new();

            for (int i = 1; i <= 254; i++)
            {
                string ip = $"{subnet}.{i}";

                tasks.Add(Task.Run(async () =>
                {
                    using (Ping ping = new Ping())
                    {
                        try
                        {
                            PingReply reply = await ping.SendPingAsync(ip, 50);

                            if (reply.Status == IPStatus.Success)
                            {
                                string hostname = GetHostName(ip);

                                lock (activeDevices)
                                {
                                    activeDevices.Add((ip, hostname));
                                }
                            }
                        }
                        catch
                        {
                            // hataları yut
                        }
                    }
                }));
            }

            await Task.WhenAll(tasks);
            return activeDevices;
        }
        
        static string GetHostName(string ip)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ip);
                return entry.HostName;
            }
            catch
            {
                return "Hostname bulunamadı";
            }
        }
    }
}
