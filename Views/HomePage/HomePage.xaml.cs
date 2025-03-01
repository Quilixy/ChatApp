using ChatApp.Models;
using ChatApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChatApp.Views
{
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<ConversationModel> Conversations { get; set; } = new ObservableCollection<ConversationModel>();

        public HomePage()
        {
            InitializeComponent();

            // Veritabanından sohbet geçmişini yükle
            LoadConversations();

             MessagingCenter.Subscribe<ChatPage, ObservableCollection<ConversationModel>>(this, "MessagesUpdated", (sender, updatedConversations) =>
            {
                Conversations = updatedConversations;
                ConversationsListView.ItemsSource = Conversations;
            });
        }

        private async void LoadConversations()
        {
            var conversations = await App.DatabaseService.GetConversationsAsync();
            Conversations = new ObservableCollection<ConversationModel>(conversations ?? new List<ConversationModel>());
            ConversationsListView.ItemsSource = Conversations;
            
            // Eğer conversations null dönerse boş bir liste kullan
           // Conversations.Clear();
            //if (conversations != null)
            //{
            //    foreach (var conversation in conversations)
            //    {
            //        Conversations.Add(conversation); // Yeni sohbetleri ekle
            //    }
            //}

            // Listeyi yeniden bağlama işlemi
            ConversationsListView.ItemsSource = Conversations;
        }

        private async void OnConversationTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedConversation = e.Item as ConversationModel;

            // Seçilen sohbet null değilse işlemi yap
            if (selectedConversation != null)
            {
                string chatId = selectedConversation.UserName; // Veya chat ID
                string receiverUsername = selectedConversation.UserName; // Konuşulan kişi
                
                // Seçilen sohbeti aç (ChatPage)
                await Navigation.PushAsync(new ChatPage(chatId, receiverUsername));
            }
        }

        private async void OnFindNearbyUsersClicked(object sender, EventArgs e)
        {
            // Yakındaki Wi-Fi kullanıcılarını al
            var nearbyUsers = await WiFiService.GetNearbyUsers();
            
            if (nearbyUsers != null && nearbyUsers.Count > 0)
            {
                // UserSelectionPage'e yakındaki kullanıcıları gönder
                await Navigation.PushAsync(new UserSelectionPage(nearbyUsers));
            }
            else
            {
                await DisplayAlert("No Users", "No nearby users found.", "OK");
            }
        }
    }
}
