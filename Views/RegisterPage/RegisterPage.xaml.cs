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
        
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var username = UsernameEntry.Text;
            var password = PasswordEntry.Text;
            var confirmPassword = ConfirmPasswordEntry.Text;
            
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Hata", "Tüm alanları doldurduğunuzdan emin olun!", "Tamam");
                return;
            }
            
            if (password != confirmPassword)
            {
                await DisplayAlert("Hata", "Şifreler uyuşmuyor!", "Tamam");
                return;
            }
            await UserService.RegisterAsync(username, password);
            
        }

        
        
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
