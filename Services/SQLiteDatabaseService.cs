using ChatApp.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class SQLiteDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public SQLiteDatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<MessageModel>().Wait(); // Message table
            _database.CreateTableAsync<ConversationModel>().Wait(); // Conversation table
            _database.CreateTableAsync<UserModel>().Wait(); // User table
        }
        
        public async Task<UserModel> GetUserAsync(string username)
        {
            return await _database.Table<UserModel>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

         
        public async Task<int> AddUserAsync(UserModel user)
        {
            try
            {
                return await _database.InsertAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Kullanıcı eklenirken hata oluştu: " + ex.Message);
            }
        }

        // Get participants for a chat
        public async Task<List<string>> GetParticipantsForChatAsync(string chatId)
        {
            // Mesajları alıp, tüm katılımcıları ayıkla
            var messages = await _database.Table<MessageModel>()
                                           .Where(m => m.ChatId == chatId)
                                           .ToListAsync();

            var participants = new HashSet<string>();

            foreach (var message in messages)
            {
                participants.Add(message.Sender);
                participants.Add(message.Receiver);
            }

            return new List<string>(participants);
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _database.Table<UserModel>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

//-------------------------------------------------------------------------------------------------------------------------------------------   

        // Get messages for a specific chat
        
        public async Task<string> GetOrCreateChatIdAsync(string sender, string receiver)
        {
            // İki kullanıcı arasında bir konuşma varsa, o konuşmaya ait bir ChatId döndürüyoruz
            var existingConversation = await _database.Table<MessageModel>()
                .Where(m => (m.Sender == sender && m.Receiver == receiver) || 
                            (m.Sender == receiver && m.Receiver == sender))
                .FirstOrDefaultAsync();

            if (existingConversation != null)
            {
                // Eğer böyle bir konuşma varsa, mevcut ChatId'yi döndürüyoruz
                return existingConversation.ChatId;
            }

            // Eğer böyle bir konuşma yoksa, yeni bir ChatId oluşturuyoruz
            string newChatId = await GenerateNewChatIdAsync();

            return newChatId;
        }
        // Generate a new ChatId for the conversation between sender and receiver
        public async Task<string> GenerateNewChatIdAsync()
        {
            // En son eklenen mesajın ID'sini alıyoruz ve onu artırarak yeni bir ChatId oluşturuyoruz
            var lastMessage = await _database.Table<MessageModel>()
                .OrderByDescending(m => m.Id)
                .FirstOrDefaultAsync();

            int newId = lastMessage == null ? 1 : lastMessage.Id + 1;

            return newId.ToString();
        }
        public async Task<List<MessageModel>> GetMessagesForChatAsync(string chatId)
        {
            try
            {
                var messages = await _database.Table<MessageModel>()
                                    .Where(m => m.ChatId == chatId ) 
                                    .OrderBy(m => m.Timestamp)                              
                                    .ToListAsync();
                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return new List<MessageModel>();
            }
        }
        // Save a new message to the database
        public async Task SaveMessageAsync(MessageModel message)
        {
            // Yeni mesajı veritabanına kaydet
            await _database.InsertAsync(message);
            await App.DatabaseService.UpdateConversationAsync(message.Sender, message.Receiver, message.Content);
        }

//-------------------------------------------------------------------------------------------------------------------------------------------     
        
        // Get conversations (chat list)
         public async Task<List<ConversationModel>> GetConversationsAsync(string sender)
        {
            return await _database.Table<ConversationModel>()
                .Where(c => c.Sender == sender || c.UserName == sender)
                .ToListAsync();
        }
        public async Task UpdateConversationAsync(string sender, string receiver, string lastMessage)
        {
            var existingConversation = await _database.Table<ConversationModel>()
                .Where(c => c.UserName == receiver && c.Sender == sender)
                .FirstOrDefaultAsync();

            if (existingConversation != null)
            {
                // Eğer konuşma zaten varsa, son mesajı ve zamanı güncelle
                existingConversation.LastMessage = lastMessage;
                existingConversation.LastMessageTime = DateTime.Now;
                await _database.UpdateAsync(existingConversation);
            }
            else
            {
                // Eğer konuşma yoksa, yeni bir konuşma oluştur
                var newConversation = new ConversationModel
                {
                    UserName = receiver,
                    LastMessage = lastMessage,
                    LastMessageTime = DateTime.Now,
                    Sender = sender
                };
                await _database.InsertAsync(newConversation);
            }
        }
    }
}
