using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public static class EncryptionService
    {
        public static async Task<string> Encrypt(string message)
        {
            
            char[] messageArray = message.ToCharArray();
            Array.Reverse(messageArray);
            return new string(messageArray); 
        }
        
        public static async Task<string> Decrypt(string content)
        {
            char[] messageArray = content.ToCharArray();
            Array.Reverse(messageArray);
            return new string(messageArray); 
        }
    }
}