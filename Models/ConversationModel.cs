using SQLite;

namespace ChatApp.Models
{
    [Table("Conversations")]
    public class ConversationModel
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; } // Konuşmanın benzersiz id'si
        public string UserName { get; set; } // Kullanıcı adı

        public string Sender { get; set; } // Gönderen
        public DateTime LastMessageTime { get; set; } // Son mesaj zamanı
        public string LastMessage { get; set; } // Son mesajın şifreli içeriği
        public string ReceiverUserName => UserName == App.CurrentUser.Username ? Sender : UserName;
    }
}