using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Services;

namespace ChatApp.Utilities
{
    public class EncryptionHelper
    {
        public static async Task<string> EncryptMessage(string message)
        {
            return await EncryptionService.Encrypt(message);
        }

        public static async Task<string> DecryptMessage(string encryptedMessage)
        {
            return await EncryptionService.Decrypt(encryptedMessage);
        }
    }
}
