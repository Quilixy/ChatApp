using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace ChatApp.Models
{
    [Table("Conversations")]
    public class ConversationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Konuşmanın benzersiz id'si
        public string UserName { get; set; } // Kullanıcı adı
        public DateTime LastMessageTime { get; set; } // Son mesaj zamanı
        public string LastMessage { get; set; } // Son mesajın şifreli içeriği
    }
}
