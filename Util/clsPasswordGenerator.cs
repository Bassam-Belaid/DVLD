using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public static class clsPasswordGenerator
    {
        private enum _enLetterType { eDigit = 0, eCapitalLetter = 1, eSmallLetter = 2, eSpecialCharacter = 4 }

        private class _clsPasswordCriteria
        {
            public _enLetterType LetterType { set; get; }
            public int LetterCounter { set; get; }

            public _clsPasswordCriteria(_enLetterType LetterType, int LetterCounter)
            {
                this.LetterType = LetterType;
                this.LetterCounter = LetterCounter;
            }
        }

        private static _enLetterType[] _DefaultPasswordCriteria = new _enLetterType[]
        {
                _enLetterType.eDigit,
                _enLetterType.eCapitalLetter,
                _enLetterType.eSmallLetter,
                _enLetterType.eSpecialCharacter
        };

        private static Random RandomNumber = new Random(DateTime.Now.Millisecond);

        private static char[] SpecialCharacters = new char[]
         {
                '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
                ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'
         };

        private static int _MinimumPasswordLength = 12;

        private static int _MaximumPasswordLength = 20;

        private static int _PasswordLength = _MinimumPasswordLength;

        public static int GetMinimumPasswordLength()
        {
            return _MinimumPasswordLength;
        }

        public static int GetMaximumPasswordLength()
        {
            return _MaximumPasswordLength;
        }

        public static void SetPasswordLength(int Length)
        {
            if (Length < _MinimumPasswordLength || Length > _MaximumPasswordLength)
                return;

            else
                _PasswordLength = Length;
        }

        private static int _GetRandomNumberInRange(int From, int To)
        {
            return RandomNumber.Next(From, To);
        }

        private static char _GetRandomDigit()
        {
            return (char)_GetRandomNumberInRange(48, 58);
        }

        private static char _GetRandomCapitalLetter()
        {
            return (char)_GetRandomNumberInRange(65, 91);
        }

        private static char _GetRandomSmallLetter()
        {
            return (char)_GetRandomNumberInRange(97, 123);
        }

        private static char _GetRandomSpecialCharacter()
        {
            return SpecialCharacters[_GetRandomNumberInRange(0, SpecialCharacters.Length)];
        }

        private static char _GetRandomLetter(_enLetterType LetterType)
        {
            switch (LetterType)
            {
                case _enLetterType.eDigit:
                    return _GetRandomDigit();

                case _enLetterType.eCapitalLetter:
                    return _GetRandomCapitalLetter();

                case _enLetterType.eSmallLetter:
                    return _GetRandomSmallLetter();

                default:
                    return _GetRandomSpecialCharacter();
            }
        }

        private static List<_clsPasswordCriteria> _GetPasswordCriteria(int Length)
        {
            int NumberOfLetterTypes = _DefaultPasswordCriteria.Length, MaxLength = -1, LetterTypeCounter = -1;

            _clsPasswordCriteria PasswordCriteria = null;

            List<_clsPasswordCriteria> PasswordCriteriaList = new List<_clsPasswordCriteria>();

            for (int i = 0; i < (_DefaultPasswordCriteria.Length - 1); i++)
            {
                MaxLength = Length - (NumberOfLetterTypes - 1) + 1;

                LetterTypeCounter = _GetRandomNumberInRange(1, MaxLength);

                PasswordCriteria = new _clsPasswordCriteria(_DefaultPasswordCriteria[i], LetterTypeCounter);

                PasswordCriteriaList.Add(PasswordCriteria);

                Length -= LetterTypeCounter;

                NumberOfLetterTypes--;
            }

            PasswordCriteria = new _clsPasswordCriteria(_DefaultPasswordCriteria[_DefaultPasswordCriteria.Length - 1], Length);

            PasswordCriteriaList.Add(PasswordCriteria);

            return PasswordCriteriaList;
        }

        public static string GeneratePassword()
        {
            StringBuilder SB = new StringBuilder(_PasswordLength);

            List<_clsPasswordCriteria> PasswordCriteriaList = _GetPasswordCriteria(_PasswordLength);

            int Index = -1;

            for (int i = 0; i < _PasswordLength; i++)
            {
                Index = _GetRandomNumberInRange(0, PasswordCriteriaList.Count);

                SB.Append(_GetRandomLetter(PasswordCriteriaList[Index].LetterType));

                PasswordCriteriaList[Index].LetterCounter--;

                if (PasswordCriteriaList[Index].LetterCounter == 0)
                    PasswordCriteriaList.RemoveAt(Index);
            }

            return SB.ToString();
        }
    }
}
