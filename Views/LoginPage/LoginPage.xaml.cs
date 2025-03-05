using ChatApp.Models;
using ChatApp.Services;
using Microsoft.Maui.Controls;
using System;

namespace ChatApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            var user = await UserService.LoginAsync(username, password);

            if (user != null)
            {
                App.CurrentUser = user; // ✅ Kullanıcı bilgisini kaydet
                if (App.CurrentUser != null)
                {
                    Console.WriteLine($"Giriş yapan kullanıcı: {App.CurrentUser.Username}");
                }
                else
                {
                    Console.WriteLine("Giriş başarısız! CurrentUser null.");
                }

                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Hata", "Geçersiz kullanıcı adı veya şifre", "Tamam");
            }
        }
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }


    }
}
