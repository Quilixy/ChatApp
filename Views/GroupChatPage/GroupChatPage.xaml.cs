using ChatApp.Models;
using ChatApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace ChatApp.Views
{
    public partial class GroupChatPage : ContentPage
    {
        private string _chatId;
        public List<MessageModel> Messages { get; set; }
        public List<string> Participants { get; set; }

        public GroupChatPage(string chatId)
        {
            InitializeComponent();
            _chatId = chatId;

            // Veritabanından mesajları ve katılımcıları çek
            LoadChatData();
        }

        private async void LoadChatData()
        {
            // Veritabanından mesajları çek
            var messages = await App.DatabaseService.GetMessagesForChatAsync(_chatId);
            Messages = messages ?? new List<MessageModel>();

            // Katılımcıları veritabanından çek
            var participants = await App.DatabaseService.GetParticipantsForChatAsync(_chatId);
            Participants = participants ?? new List<string>();

            // Mesajları ve katılımcıları ekranda göster
            MessagesListView.ItemsSource = Messages;
            ParticipantsListView.ItemsSource = Participants; // Now this will work because ParticipantsListView is defined in XAML
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            string messageContent = MessageEntry.Text;
            if (!string.IsNullOrEmpty(messageContent))
            {
                var message = new MessageModel
                {
                    Sender = "CurrentUser", // Burada geçerli kullanıcıyı dinamik olarak alabilirsiniz
                    Receiver = "GroupChat", // Grup sohbetinde alıcı gruptur
                    Content = messageContent, // Şifreli mesajı burada ekleyebilirsiniz
                    Timestamp = DateTime.UtcNow
                };

                // Mesajı veritabanına kaydet
                await App.DatabaseService.SaveMessageAsync(message);

                // Mesajları güncelle
                LoadChatData();
            }
        }
    }
}
