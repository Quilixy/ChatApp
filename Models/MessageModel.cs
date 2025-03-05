using SQLite;

namespace ChatApp.Models
{
    public class MessageModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Mesajın benzersiz ID'si
        public string ChatId { get; set; } // Mesajın ait olduğu sohbetin ID'si
        public string Sender { get; set; } // Gönderen kullanıcı adı
        public string Receiver { get; set; } // Alıcı kullanıcı adı
        public string Content { get; set; } // Şifreli mesaj içeriği
        public DateTime Timestamp { get; set; } // Mesajın gönderilme zamanı
    }
}
