using Microsoft.Maui.Controls;
using System.Collections.Generic;
using ChatApp.Models;

namespace ChatApp.Views
{
    public partial class UserSelectionPage : ContentPage
    {

        public List<string> NearbyUsers { get; set; }

        // Constructor: List<string> türünde kullanıcıları alacak şekilde düzenlendi
        public UserSelectionPage(List<string> nearbyUsers)
        {
            InitializeComponent();
            NearbyUsers = nearbyUsers;
            UsersListView.ItemsSource = NearbyUsers; // Kullanıcıları listele
        }

        private async void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedUser = e.Item as string;
            if (selectedUser != null)
            {
                // Seçilen kullanıcıya ait chatId'yi burada alıyoruz. Genellikle chatId, kullanıcı adı ile aynı olabilir
                string chatId = selectedUser; // Burada chatId'nin kullanıcı adı olduğunu varsayıyoruz
                string receiverUsername = selectedUser; // Alıcı kişi (seçilen kullanıcı)

                // Seçilen kullanıcı ile sohbet başlat (ChatPage)
                await Navigation.PushAsync(new ChatPage(chatId, receiverUsername));
            }
        }
    }
}