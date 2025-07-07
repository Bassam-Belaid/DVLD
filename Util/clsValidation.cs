using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Util
{
    public static class clsValidation
    {
        private static bool _IsNumberInRange(int Number, int From, int To)
        {
            return (Number >= From && Number <= To);
        }
        private static bool _IsDigit(int CharacterNumber)
        {
            return _IsNumberInRange(CharacterNumber, 48, 57);
        }
        private static bool _IsLetter(int CharacterNumber)
        {
            return _IsNumberInRange(CharacterNumber, 97, 122) || _IsNumberInRange(CharacterNumber, 65, 90); 
        }
        public static bool IsValidName(string Name)
        {
            for (int i = 0; i < Name.Length; i++) 
            {
                if(!_IsLetter((int)Name[i]))
                    return false;
            }
            return true;
        }
        public static bool IsValidPhone(string Phone)
        {
            for (int i = 0; i < Phone.Length; i++)
            {
                if (!_IsDigit((int)Phone[i]))
                    return false;
            }
            return true;
        }
        public static bool IsValidEmail(string Email)
        {
            if (string.IsNullOrWhiteSpace(Email))
                return false;

            string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex Regex = new Regex(Pattern);

            return Regex.IsMatch(Email);
        }
    }
}
