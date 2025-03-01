using Microsoft.Maui.Controls;
using ChatApp.Views;
using ChatApp.Services;
using ChatApp.Models;

namespace ChatApp
{
    public partial class App : Application
    {
        public static SQLiteDatabaseService DatabaseService { get; private set; }
        public static UserModel CurrentUser { get; set; } // 🔥 Burada oturum açan kullanıcı saklanıyor

        public App()
        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "chatapp.db");
            DatabaseService = new SQLiteDatabaseService(dbPath);
            Console.WriteLine($"Veritabanı dosyası mevcut: {dbPath}");
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
