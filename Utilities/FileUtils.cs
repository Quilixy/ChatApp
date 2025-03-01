using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ChatApp.Utilities
{
    public static class FileUtils
    {
        // Dosya içeriklerini okur
        public static string ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            return string.Empty;
        }

        // Dosya içeriklerini yazar
        public static void WriteToFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

        // Dosya var mı diye kontrol eder
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}