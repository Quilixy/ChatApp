using Microsoft.Maui.Controls;
using System.Collections.Generic;
using ChatApp.Models;
using ChatApp.Services;

namespace ChatApp.Views
{
    public partial class UserSelectionPage : ContentPage
    {
        private string _chatId;
        public string LocalIp { get; set; }
        public List<(string Ip, string Hostname)> NearbyUsers { get; set; }
        
        public UserSelectionPage(List<(string Ip, string Hostname)> nearbyUsers)
        {
            InitializeComponent();
            LocalIp  = WiFiService.GetLocalIpAddress();
            NearbyUsers = nearbyUsers;
            BindingContext = this;
            UsersListView.ItemsSource = NearbyUsers; 
            
        }
        private async void OnUserTapped(object? sender, ItemTappedEventArgs e)
        {
            if (e.Item is ValueTuple<string, string> selectedTuple)
            {
                string recieverUsername = selectedTuple.Item1;
                string recieverHostname = selectedTuple.Item2;
                
                await Navigation.PushAsync(new ChatPage(recieverUsername));
            }
            
            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
        }
        private async void OnHomePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}