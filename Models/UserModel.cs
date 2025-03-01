using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace ChatApp.Models
{
    [Table("Users")]
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Otomatik artan id
        [Unique]
        public string Username { get; set; } // Kullanıcı adı
        public string PasswordHash { get; set; } // Şifre hash
        public string WifiIp { get; set; } // Wi-Fi IP
    }
}
