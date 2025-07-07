using System;
using System.IO;

namespace Util
{
    public class clsImageHandler
    {
        private static string _DestinationDirectory = @"C:\DVLD\DVLD_Person_Images";

        public static void DeleteImageInStorage(string ImagePath)
        {
            try
            {
                if (File.Exists(ImagePath))
                {
                    File.Delete(ImagePath);
                    Console.WriteLine($"Successfully Deleted: {ImagePath}");
                }
                else
                {
                    Console.WriteLine($"File Not Found: {ImagePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static string SaveImageInStorage(string ImagePath)
        {
            string DestinationFilePath = null;

            try
            {
                if (!Directory.Exists(_DestinationDirectory))
                {
                    Directory.CreateDirectory(_DestinationDirectory);
                }
                string NewImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImagePath);
                DestinationFilePath = Path.Combine(_DestinationDirectory, NewImageName);

                File.Copy(ImagePath, DestinationFilePath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return DestinationFilePath;
        }

    }
}
