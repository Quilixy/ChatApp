using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatApp.Utilities;

namespace ChatApp.Services;

public class UdpService
{
    private readonly ChatService _chatService;
    private UdpClient udpClient;
    private int localPort = 11000;
    private bool _listening = false;

    public UdpService(ChatService chatService)
    {
        _chatService = chatService;
    }

    
    public async Task SendUdpMessageAsync(string fullMessage, string receiver)
    {
        string[] splittedMessage = fullMessage.Split(':', 2);
        string sender = splittedMessage.Length > 1 ? splittedMessage[0] : "Bilinmiyor";
        string message = splittedMessage.Length > 1 ? splittedMessage[1] : "Bilinmiyor";
        string ipAddress = receiver;
        Console.WriteLine($"Gönderme --> Full Message:{fullMessage}");
        Console.WriteLine($"Gönderme --> Sender:{splittedMessage[0]}");
        Console.WriteLine($"Gönderme --> Sender is:{sender}");
        Console.WriteLine($"Gönderme --> Message:{splittedMessage[1]}");
        Console.WriteLine($"Gönderme --> Message is:{message}");

        await _chatService.SendMessageAsync(sender, receiver, message);

        var remoteEndpoint = new IPEndPoint(IPAddress.Parse(ipAddress), localPort);
        byte[] sendBytes = Encoding.UTF8.GetBytes(fullMessage);

        using (UdpClient senderClient = new UdpClient())
        {
            await senderClient.SendAsync(sendBytes, sendBytes.Length, remoteEndpoint);
        }
    }

    public async Task StartListening()
    {
        
        if(_listening) return;
        _listening = true;
        var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        
        try
        {
            udpClient = new UdpClient
            {
                Client = udpSocket
            };
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, localPort));
            
            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                string receivedFullMessage = Encoding.UTF8.GetString(result.Buffer);
                string[] splittedMessage = receivedFullMessage.Split(':', 2);
                string senderUserName = splittedMessage.Length > 1 ? splittedMessage[0] : "Bilinmiyor";
                string receivedMessage = splittedMessage.Length > 1 ? splittedMessage[1] : "Bilinmiyor";
                string senderIp = result.RemoteEndPoint.Address.ToString();

                Console.WriteLine($"Alma --> Full Message:{receivedFullMessage}");
                Console.WriteLine($"Alma --> Sender:{splittedMessage[0]}");
                Console.WriteLine($"Alma --> Sender is:{senderUserName}");
                Console.WriteLine($"Alma --> Message:{splittedMessage[1]}");
                Console.WriteLine($"Alma --> Message is:{receivedMessage}"); 
                Console.WriteLine($"Alma --> Sender IP:{senderIp}");
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _ = _chatService.SendMessageAsync(senderUserName, senderIp, receivedMessage);  
                });
                
            }
        }
        catch (Exception ex)
        {
            _listening = false;
            await AlertUtils.ShowAlertAsync("Hata", ex.Message, "Tamam");
        }
    }
}
