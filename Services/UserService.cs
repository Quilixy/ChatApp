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
        public static async Task<UserModel> LoginAsync(string username, string password)
        {
            var user = await App.DatabaseService.GetUserAsync(username); 

            if (user == null)
            {
                return null;
            }
            
            string hashedPassword = await EncryptionService.Encrypt(password);
            if (hashedPassword == user.PasswordHash)
            {
                return user;
            }
            
            return null; 
        }
        public static async Task<bool> RegisterAsync(string username, string password)
        {
            var existingUser = await App.DatabaseService.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                return false; 
            }
            
            string passwordHash = await EncryptionService.Encrypt(password);
            
            var newUser = new UserModel
            {
                Username = username,
                PasswordHash = passwordHash
            };
            
            await App.DatabaseService.AddUserAsync(newUser);
            return true;
        }
        
    }
}