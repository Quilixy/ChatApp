using ChatApp.Models;
using ChatApp.Services;
using System;
using Microsoft.Maui.Controls;

namespace ChatApp.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        // Kayıt butonuna tıklanınca çağrılacak
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text; // Kullanıcı adı alınıyor
            var password = PasswordEntry.Text; // Şifre alınıyor
            var confirmPassword = ConfirmPasswordEntry.Text; // Şifre tekrar alınıyor

            // Boş girişler kontrol ediliyor
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Hata", "Tüm alanları doldurduğunuzdan emin olun!", "Tamam");
                return;
            }

            // Şifrelerin uyuşup uyuşmadığı kontrol ediliyor
            if (password != confirmPassword)
            {
                await DisplayAlert("Hata", "Şifreler uyuşmuyor!", "Tamam");
                return;
            }

            // Şifreyi hashle
            var hashedPassword = HashPassword(password);

            // Kullanıcı modelini oluşturuyoruz
            var newUser = new UserModel
            {
                Username = username,
                PasswordHash = hashedPassword,
                WifiIp = "192.168.1.1"  // Örnek IP, gerçekte bu Wi-Fi'den alınabilir
            };

            try
            {
                // Veritabanına kullanıcı ekliyoruz
                var result = await App.DatabaseService.AddUserAsync(newUser);
                if (result > 0)
                {
                    // Başarıyla kayıt oldu
                    await DisplayAlert("Başarılı", "Kayıt başarılı!", "Tamam");
                    await Navigation.PushAsync(new LoginPage()); // LoginPage'e yönlendir
                }
                else
                {
                    await DisplayAlert("Hata", "Kullanıcı kaydedilemedi!", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", ex.Message, "Tamam");
            }
        }

        // Şifreyi hashleme fonksiyonu
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
