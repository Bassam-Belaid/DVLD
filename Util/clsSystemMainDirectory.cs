

using System;
using System.IO;

namespace Util
{
    public static class clsSystemMainDirectory
    {
        private static string _MainDirectory = @"C:\DVLD";

        public static void CheckMainDirectoryStatus() 
        {
            try
            {
                if (!Directory.Exists(_MainDirectory))
                {
                    Directory.CreateDirectory(_MainDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
