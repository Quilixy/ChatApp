using Microsoft.Maui.Controls;
using System.Collections.Generic;
using ChatApp.Models;

namespace ChatApp.Views
{
    public partial class UserSelectionPage : ContentPage
    {
        private string _chatId;
        public List<string> NearbyUsers { get; set; }
        
        public UserSelectionPage(List<string> nearbyUsers)
        {
            InitializeComponent();
            NearbyUsers = nearbyUsers;
            UsersListView.ItemsSource = NearbyUsers; 
        }
        private async void OnUserTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedUser = e.Item as string;
            if (selectedUser != null)
            {
                string receiverUsername = selectedUser; // Alıcı kişi (seçilen kullanıcı)
                
                await Navigation.PushAsync(new ChatPage(receiverUsername));
            }
        }

        //private void CreateChatIDOnTapped(object sender, ItemTappedEventArgs e){}
        
    }
}