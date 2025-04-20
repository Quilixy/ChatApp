using SQLite;

namespace ChatApp.Models
{
    [Table("Conversations")]
    public class ConversationModel
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Sender { get; set; } 
        public DateTime LastMessageTime { get; set; } 
        public string LastMessage { get; set; } 
        public string ReceiverUserName => UserName == App.CurrentUser.Username ? Sender : UserName;
    }
}