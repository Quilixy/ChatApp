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
            
            
             MessagingCenter.Subscribe<ChatPage, ObservableCollection<ConversationModel>>(this, "MessagesUpdated", (sender, updatedConversations) =>
            {
                Conversations = updatedConversations;
                ConversationsListView.ItemsSource = Conversations;
            });
            LoadConversations();
        }

        private async void LoadConversations()
        {
            string sender = App.CurrentUser.Username;

            if (string.IsNullOrEmpty(sender))
            {
                Conversations = new ObservableCollection<ConversationModel>();
            }
            else
            {
                var conversations = await App.DatabaseService.GetConversationsAsync(sender);
                if(conversations != null)
                {
                    foreach(var conversation in conversations)
                    {
                        if (!string.IsNullOrEmpty(conversation.LastMessage))
                        {
                            // Şifre çözme işlemi
                            conversation.LastMessage = await EncryptionService.Decrypt(conversation.LastMessage);
                        }
                    }
                    Conversations = new ObservableCollection<ConversationModel>(conversations);
                }
                else
                {
                    Conversations = new ObservableCollection<ConversationModel>();
                }
                
                ConversationsListView.ItemsSource = Conversations;
            }          
        }

        private async void OnConversationTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedConversation = e.Item as ConversationModel;
            if (selectedConversation != null)
            {
                string chatId = selectedConversation.UserName;
                string receiverUsername = selectedConversation.UserName;
                string senderUserName = selectedConversation.Sender;
                if(receiverUsername != App.CurrentUser.Username)
                {
                    await Navigation.PushAsync(new ChatPage(receiverUsername));
                }
                else
                {
                    receiverUsername = senderUserName;
                    await Navigation.PushAsync(new ChatPage(receiverUsername));
                }
                
            }
        }

        private async void OnFindNearbyUsersClicked(object sender, EventArgs e)
        {
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
