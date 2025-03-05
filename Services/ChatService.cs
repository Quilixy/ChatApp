using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models;

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
            if (string.IsNullOrEmpty(content) && App.CurrentUser == null)
            {
                Console.WriteLine("Hata: Mesaj veya kullanıcı bilgisi alınamadı");;
                
            }
            //if (chatId == null)
            //{
               // await App.DatabaseService.GenerateNewChatIdAsync();
            //}
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
                             await App.DatabaseService.UpdateConversationAsync(message.Sender, message.Receiver, message.Content);
            }
        }

        // Grup sohbeti için mesaj gönderme
        public async Task SendGroupMessage(string Content)
        {
            
        }
    }
    
}