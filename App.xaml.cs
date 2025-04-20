using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using ChatApp.Views;
using ChatApp.Services;
using ChatApp.Models;

namespace ChatApp
{
    public partial class App : Application
    {
        public static SQLiteDatabaseService DatabaseService { get; private set; }
        public static UserModel CurrentUser { get; set; } 
        private readonly UdpService _udpService;
        public static ObservableCollection<MessageModel> MessageList { get; set; } = new ObservableCollection<MessageModel>();


        public App()
        {
            InitializeComponent();
            var chatService = new ChatService();
            _udpService = new UdpService(chatService);
            StartUdpListener();
            
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "chatapp.db");
            DatabaseService = new SQLiteDatabaseService(dbPath);
            Console.WriteLine($"Veritabanı dosyası mevcut: {dbPath}");
            MainPage = new NavigationPage(new LoginPage());
        }
        private void StartUdpListener()
        {
            Task.Run(async () =>
            {
                await _udpService.StartListening();
            });
        }
        
    }
}
