using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models;
using ChatApp.Utilities;

namespace ChatApp.Services
{
    public class ChatService
    {
        public async Task<ObservableCollection<MessageModel>> LoadMessagesAsync(string chatId)
        {
            var messages = await App.DatabaseService.GetMessagesForChatAsync(chatId);
            var decrpytedMessages = new ObservableCollection<MessageModel>();
            if (messages != null )
            {
                foreach (var message in messages)
                {
                    message.Content = await EncryptionService.Decrypt(message.Content);
                    decrpytedMessages.Add(message);
                }
            }
            return decrpytedMessages;
        }
        public async Task SendMessageAsync(string sender, string receiver, string content)
        {
            if (App.CurrentUser == null)
            {
                await AlertUtils.ShowAlertAsync("Hata","Kullanıcı Bilgisi Alınamadı","Tamam");
                
            }
            else if(string.IsNullOrEmpty(content))
            {
                await AlertUtils.ShowAlertAsync("Hata","Boş Mesaj Girilemez","Tamam");
            }
            else
            {
                string encryptedContent = await EncryptionService.Encrypt(content);
                var message = new MessageModel
                {
                    Sender = sender,
                    Receiver = receiver,
                    Content = encryptedContent,
                    ChatId =await App.DatabaseService.GetOrCreateChatIdAsync(sender, receiver),
                    Timestamp = DateTime.Now
                };
                await App.DatabaseService.SaveMessageAsync(message);
                //await App.DatabaseService.UpdateConversationAsync(message.Sender, message.Receiver, message.Content);
            }
        }


        public async Task SendGroupMessage(string Content)
        {
            
        }
    }
    
}