using ChatApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatApp.Views
{
    public partial class ChatPage : ContentPage
    {
        private string _chatId;
        private string _receiverUsername;
        public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>(); 

        public ChatPage(string chatId, string receiverUsername )
        {
            InitializeComponent();
            _chatId = chatId;
            _receiverUsername = receiverUsername;
            _receiverUsername = string.IsNullOrEmpty(receiverUsername) ? "Unknown" : receiverUsername;
            UserNameLabel.Text = $"Chatting with {_receiverUsername}";

            if (App.CurrentUser == null)
            {
                DisplayAlert("Hata", "Kullanıcı bilgisi bulunamadı. Lütfen tekrar giriş yapın.", "Tamam");
                Navigation.PushAsync(new LoginPage());
                return;
            }

            BindingContext = this; 
            LoadMessages();
        }

        private async void LoadMessages()
        {
            var messages = await App.DatabaseService.GetMessagesForChatAsync(_chatId);
            Messages.Clear(); // Eski mesajları temizle

            if (messages != null)
            {
                foreach (var message in messages)
                {
                    Messages.Add(message); // Yeni mesajları ekle
                }
            }

            MessagesListView.ItemsSource = Messages;
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string messageContent = MessageEntry.Text;
            if (!string.IsNullOrEmpty(messageContent) && App.CurrentUser != null)
            {
                var message = new MessageModel
                {
                    Sender = App.CurrentUser.Username,
                    Receiver = _receiverUsername,
                    Content = messageContent,
                    Timestamp = DateTime.UtcNow
                };

                await App.DatabaseService.SaveMessageAsync(message);
                MessageEntry.Text = "";  // Mesaj kutusunu temizle

                await App.DatabaseService.UpdateConversationAsync(message.Sender, message.Receiver, message.Content);
                LoadMessages();
            }
        }
    }
}
