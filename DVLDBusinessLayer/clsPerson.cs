using DVLDDataAccessLayer;
using System;
using System.Data;
using Util;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {

        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private int _PersonID;
        public int GetPersonID()
        {
            return _PersonID;
        }
        public int NationalityCountryID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }

        public clsPerson()
        {

            _PersonID = -1;
            NationalityCountryID = -1;
            NationalNo = null;
            FirstName = null;
            SecondName = null;
            ThirdName = null;
            LastName = null;
            DateOfBirth = DateTime.MinValue;
            Gender = true;
            Address = null;
            Phone = null;
            Email = null;
            ImagePath = null;
            _Mode = enMode.eAddNew;

        }

        public clsPerson(int PersonID, int NationalityCountryID, string NationalNo, string FirstName,
                      string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
                      bool Gender, string Address, string Phone, string Email, string ImagePath)
        {

            _PersonID = PersonID;
            this.NationalityCountryID = NationalityCountryID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ImagePath = ImagePath;
            _Mode = enMode.eUpdate;

        }

        public string GetFullName()
        {
            return this.FirstName + ' ' + this.SecondName + ' ' + (!(string.IsNullOrEmpty(this.ThirdName)) ? this.ThirdName + ' ' : "") + this.LastName;
        }

        public string GetPersonGender()
        {

            return (this.Gender) ? "Male" : "Female";

        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static DataTable GetPeopleFilteredByPersonID(int PersonID)
        {
            return clsPersonData.GetPeopleFilteredByPersonID(PersonID);
        }

        public static DataTable GetPeopleFilteredByNationalNo(string NationalNo)
        {

            NationalNo += "%";
            return clsPersonData.GetPeopleFilteredByNationalNo(NationalNo);

        }

        public static DataTable GetPeopleFilteredByFirstName(string FirstName)
        {

            FirstName += "%";
            return clsPersonData.GetPeopleFilteredByFirstName(FirstName);

        }

        public static DataTable GetPeopleFilteredBySecondName(string SecondName)
        {

            SecondName += "%";
            return clsPersonData.GetPeopleFilteredBySecondName(SecondName);

        }

        public static DataTable GetPeopleFilteredByThirdName(string ThirdName)
        {

            ThirdName += "%";
            return clsPersonData.GetPeopleFilteredByThirdName(ThirdName);

        }

        public static DataTable GetPeopleFilteredByLastName(string LastName)
        {

            LastName += "%";
            return clsPersonData.GetPeopleFilteredByLastName(LastName);

        }

        public static DataTable GetPeopleFilteredByNationality(string Nationality)
        {

            Nationality += "%";
            return clsPersonData.GetPeopleFilteredByNationality(Nationality);

        }

        public static DataTable GetPeopleFilteredByGender(string Gender)
        {

            Gender += "%";
            return clsPersonData.GetPeopleFilteredByGender(Gender);

        }

        public static DataTable GetPeopleFilteredByPhone(string Phone)
        {

            Phone += "%";
            return clsPersonData.GetPeopleFilteredByPhone(Phone);

        }

        public static DataTable GetPeopleFilteredByEmail(string Email)
        {

            Email += "%";
            return clsPersonData.GetPeopleFilteredByEmail(Email);

        }

        public static clsPerson GetPersonInfoByPersonID(int PersonID)
        {

            int NationalityCountryID = -1;
            string NationalNo = null, FirstName = null, SecondName = null, ThirdName = null, LastName = null, Address = null, Phone = null, Email = null, ImagePath = null;
            DateTime DateOfBirth = DateTime.Now;
            bool Gender = false;

            if (clsPersonData.GetPersonInfoByPersonID(PersonID, ref NationalityCountryID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref ImagePath))
            {

                return new clsPerson(PersonID, NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender,
                    Address, Phone, Email, ImagePath);

            }

            else
                return null;

        }

        public static clsPerson GetPersonInfoByNationalNo(string NationalNo)
        {
            int PersonID = -1, NationalityCountryID = -1;
            string FirstName = null, SecondName = null, ThirdName = null, LastName = null, Address = null, Phone = null, Email = null, ImagePath = null;
            DateTime DateOfBirth = DateTime.Now;
            bool Gender = false;

            if (clsPersonData.GetPersonInfoByNationalNo(ref PersonID, ref NationalityCountryID, NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref ImagePath))
            {

                return new clsPerson(PersonID, NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender,
                    Address, Phone, Email, ImagePath);

            }

            else
                return null;
        }

        public static clsPerson GetPersonInfoByEmail(string Email)
        {
            int PersonID = -1, NationalityCountryID = -1;
            string NationalNo = null, FirstName = null, SecondName = null, ThirdName = null, LastName = null, Address = null, Phone = null, ImagePath = null;
            DateTime DateOfBirth = DateTime.Now;
            bool Gender = false;

            if (clsPersonData.GetPersonInfoByEmail(ref PersonID, ref NationalityCountryID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, Email, ref ImagePath))
            {

                return new clsPerson(PersonID, NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender,
                    Address, Phone, Email, ImagePath);

            }

            else
                return null;
        }

        public static clsPerson GetPersonInfoByPhone(string Phone)
        {
            int PersonID = -1, NationalityCountryID = -1;
            string NationalNo = null, FirstName = null, SecondName = null, ThirdName = null, LastName = null, Address = null, Email = null, ImagePath = null;
            DateTime DateOfBirth = DateTime.Now;
            bool Gender = false;

            if (clsPersonData.GetPersonInfoByPhone(ref PersonID, ref NationalityCountryID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref Gender, ref Address, Phone, ref Email, ref ImagePath))
            {

                return new clsPerson(PersonID, NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender,
                    Address, Phone, Email, ImagePath);

            }

            else
                return null;
        }

        public static bool IsPersonExistsByPersonID(int PersonID)
        {
            return clsPersonData.IsPersonExistsByPersonID(PersonID);
        }

        public static bool IsPersonExistsByNationalNo(string NationalNo)
        {
            return clsPersonData.IsPersonExistsByNationalNo(NationalNo);
        }

        public static bool IsPersonExistsByEmail(string Email)
        {
            return clsPersonData.IsPersonExistsByEmail(Email);
        }

        public static bool IsPersonExistsByPhone(string Phone)
        {
            return clsPersonData.IsPersonExistsByPhone(Phone);
        }

        private bool _AddNewPerson()
        {
            if (ImagePath != null)
                ImagePath = clsImageHandler.SaveImageInStorage(ImagePath);

            _PersonID = clsPersonData.AddNewPerson(NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName,
                                                   LastName, DateOfBirth, Gender, Address, Phone, Email, ImagePath);

            return (_PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            string CurrentImagePath = GetImagePathByPersonID(_PersonID);

            if (CurrentImagePath != ImagePath)
            {
                if (!string.IsNullOrEmpty(CurrentImagePath))
                    clsImageHandler.DeleteImageInStorage(CurrentImagePath);

                if (!string.IsNullOrEmpty(ImagePath))
                    ImagePath = clsImageHandler.SaveImageInStorage(ImagePath);
            }

            return clsPersonData.UpdatePerson(_PersonID, NationalityCountryID, NationalNo, FirstName, SecondName, ThirdName,
                                                   LastName, DateOfBirth, Gender, Address, Phone, Email, ImagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            string ImagePath = GetImagePathByPersonID(PersonID);
            
            if(!string.IsNullOrEmpty(ImagePath))
                clsImageHandler.DeleteImageInStorage(ImagePath);

            return clsPersonData.DeletePerson(PersonID);
        }

        public static string GetImagePathByPersonID(int PersonID)
        {
            return clsPersonData.GetImagePathByPersonID(PersonID);
        }
        
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.eUpdate:
                    if (_UpdatePerson())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }

            return false;
        }

        public static string GetPersonFullNameByPersonID(int PersonID) 
        {
            return clsPersonData.GetPersonFullNameByPersonID(PersonID);
        }
    }
}
