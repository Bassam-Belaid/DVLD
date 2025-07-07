using System;
using System.Collections.Generic;
using System.IO;

namespace Util
{
    public static class clsFileHandler
    {
        private static string FilePath = @"C:\DVLD\LogedIn_User.txt";

        public static bool IsDataInFile()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.Create(FilePath).Close();
                    return false;
                }
                
                    return new FileInfo(FilePath).Length > 0;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error Checking Data: {ex.Message}");
                return false;
            }
        }

        public static void SaveDataToFile(string Username, string Password)
        {
            ClearFileData();
            
            try
            {
                using (StreamWriter Writer = new StreamWriter(FilePath, true))
                {
                    Writer.WriteLine(Username);
                    Writer.WriteLine(Password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Saving Data: {ex.Message}");
            }
        }

        public static List<string> LoadDataFromFile()
        {
            List<string> Data = new List<string>();

            try
            {
                using (StreamReader Reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = Reader.ReadLine()) != null)
                    {
                        Data.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Loading Data: {ex.Message}");
            }

            return Data;
        }

        public static void ClearFileData()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath, false)) 
                {
                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Clearing Data: {ex.Message}");
            }
        }
    }
}
