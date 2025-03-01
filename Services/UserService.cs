using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Models;

namespace ChatApp.Services
{
   public static class UserService
    {
        // Kullanıcıyı doğrulama
        //public static async Task<bool> AuthenticateUser(string username, string password)
        //{
        //    // Şifreli tutulan kullanıcı verileri burada doğrulanabilir
        //    // Örnek: Veritabanına bağlanıp kullanıcıyı kontrol etme
        //    if (username == "test" && password == "password") // Basit kontrol
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public static async Task<UserModel> LoginAsync(string username, string password)
        {
            // Veritabanında kullanıcıyı al
            var user = await App.DatabaseService.GetUserAsync(username); // Bu satırda 'user' null dönebilir.

            if (user == null)
            {
                return null; // Eğer kullanıcı bulunamazsa null dönecek
            }

            // Şifre doğrulama
            string hashedPassword = HashPassword(password);
            if (hashedPassword == user.PasswordHash)
            {
                return user; // Giriş başarılı
            }
            
            return null; // Şifre yanlış
        }

        private static bool VerifyPassword(string storedHash, string enteredPassword)
        {
            // Şifre karşılaştırmasını hash ile yapmalısınız
            // Örnek: Hash'leme kullanıyorsanız burada şifre karşılaştırması yapılmalı
            return storedHash == enteredPassword; // Bu basit bir örnek, hash karşılaştırması yapılmalıdır.
        }

        public static async Task<bool> RegisterAsync(string username, string password)
        {
            // Kullanıcının olup olmadığını kontrol et
            var existingUser = await App.DatabaseService.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return false; // Kullanıcı zaten varsa kayıt olamaz
            }

            // Şifreyi hash'le
            string passwordHash = HashPassword(password);

            // Kullanıcıyı oluştur
            var newUser = new UserModel
            {
                Username = username,
                PasswordHash = passwordHash
            };

            // Veritabanına ekle
            await App.DatabaseService.AddUserAsync(newUser);
            return true;
        }

        // Şifreyi SHA256 ile hash'leme fonksiyonu
        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}