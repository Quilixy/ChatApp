using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace ChatApp.Models
{
    [Table("Chats")]
    public class ChatModel
    {
        [PrimaryKey]
        public string ChatId { get; set; } // Sohbetin benzersiz id'si
        public List<MessageModel> Messages { get; set; } // Mesajlar listesi
        public List<string> Participants { get; set; } // Katılımcıların listesi (grup sohbeti için)
    }
}
