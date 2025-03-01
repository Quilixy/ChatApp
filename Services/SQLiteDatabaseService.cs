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

        

        

        
       

        // Get user by username
        public async Task<UserModel> GetUserAsync(string username)
        {
            return await _database.Table<UserModel>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }

         // Kullanıcı ekleme metodu
        public async Task<int> AddUserAsync(UserModel user)
        {
            try
            {
                // Veritabanına kullanıcıyı ekliyoruz
                return await _database.InsertAsync(user);
            }
            catch (Exception ex)
            {
                // Hata yakalama
                throw new Exception("Kullanıcı eklenirken hata oluştu: " + ex.Message);
            }
        }

        // Verify password for a user
        public async Task<bool> VerifyPasswordAsync(UserModel user, string password)
        {
            return user.PasswordHash == password; // Şifre doğrulama
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
        public async Task<List<MessageModel>> GetMessagesForChatAsync(string chatId)
        {
            // ChatId'ye göre mesajları çek
            //return await _database.Table<MessageModel>()
                                   //.Where(m => m.ChatId == chatId)
                                   //.ToListAsync();
        
        try
        {
            
            var messages = await _database.Table<MessageModel>()
                                .Where(m => m.Receiver == chatId || m.Sender == chatId) //  Alıcı veya gönderici olarak filtrele
                                .OrderBy(m => m.Timestamp)                              // 🔥 Mesajları zaman sırasına göre getir
                                .ToListAsync();

            //Console.WriteLine($"DB'den Gelen Mesaj Sayısı: {messages.Count}");        //  Debug için log ekle
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
        }

//-------------------------------------------------------------------------------------------------------------------------------------------     
        
        // Get conversations (chat list)
         public async Task<List<ConversationModel>> GetConversationsAsync()
        {
            return await _database.Table<ConversationModel>().ToListAsync();
        }
        
        public async Task UpdateConversationAsync(string sender, string receiver, string lastMessage)
        {
            var existingConversation = await _database.Table<ConversationModel>()
                .Where(c => c.UserName == receiver || c.UserName == sender)
                .FirstOrDefaultAsync();

            if (existingConversation != null)
            {
                // Eğer konuşma zaten varsa, son mesajı ve zamanı güncelle
                existingConversation.LastMessage = lastMessage;
                existingConversation.LastMessageTime = DateTime.UtcNow;
                await _database.UpdateAsync(existingConversation);
            }
            else
            {
                // Eğer konuşma yoksa, yeni bir konuşma oluştur
                var newConversation = new ConversationModel
                {
                    UserName = receiver,
                    LastMessage = lastMessage,
                    LastMessageTime = DateTime.UtcNow
                };
                await _database.InsertAsync(newConversation);
            }
        }


    }
}
