using System.Data;
using DVLDDataAccessLayer;
using Util;

namespace DVLDBusinessLayer
{
    public class clsUser
    {

        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private int _UserID;
        public int PersonID { get; set; }
        public int GetUserID()
        {
            return _UserID;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permissions { get; set; }

        public clsUser() 
        {
            _Mode = enMode.eAddNew;
            _UserID = -1;
            PersonID = -1;
            UserName = null;
            Password = null;
            IsActive = false;
            Permissions = -1;
        }

        public clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive, int Permissions) 
        {
            _Mode = enMode.eUpdate;
            _UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.Permissions = Permissions;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static DataTable GetUsersFilteredByPersonID(int PersonID)
        {
            return clsUserData.GetUsersFilteredByPersonID(PersonID);
        }

        public static DataTable GetUsersFilteredByUserID(int UserID)
        {
            return clsUserData.GetUsersFilteredByUserID(UserID);
        }

        public static DataTable GetUsersFilteredByFullName(string FullName)
        {
            FullName += "%";
            return clsUserData.GetUsersFilteredByFullName(FullName);
        }

        public static DataTable GetUsersFilteredByUserName(string UserName)
        {
            UserName += "%";
            return clsUserData.GetUsersFilteredByUserName(UserName);
        }

        public static DataTable GetUsersFilteredByActivationStatus(bool ActivationStatus)
        {
            return clsUserData.GetUsersFilteredByActivationStatus(ActivationStatus);
        }

        public static clsUser GetUserInfoByUserID(int UserID)
        {
            int PersonID = -1, Permissions = -1;
            string UserName = null, Password = null;
            bool IsActive = false;

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive, ref Permissions))
                return new clsUser(UserID, PersonID, UserName, clsPasswordEncryption.DecryptPassword(Password), IsActive, Permissions);

            else
                return null;
        }

        public static clsUser GetUserInfoByUserName(string UserName)
        {
            int UserID = -1, PersonID = -1, Permissions = -1;
            string Password = null;
            bool IsActive = false;

            if(clsUserData.GetUserInfoByUserName(ref UserID, ref PersonID, UserName, ref Password, ref IsActive, ref Permissions))
                return new clsUser(UserID, PersonID, UserName, clsPasswordEncryption.DecryptPassword(Password), IsActive, Permissions);

            else
                return null;
        }

        public static string GetUserPasswordByUserID(int UserID)
        {
            return clsPasswordEncryption.DecryptPassword(clsUserData.GetUserPasswordByUserID(UserID));
        }

        public static bool IsUserExistsByUserName(string UserName)
        {
            return clsUserData.IsUserExistsByUserName(UserName);
        }

        public static bool IsUserExistsByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistsByPersonID(PersonID);
        }

        public static bool IsPasswordMatchesCurrentPassword(int UserID, string Password)
        {
            return clsUserData.IsPasswordMatchesCurrentPassword(UserID, clsPasswordEncryption.EncryptPassword(Password));
        }

        private bool _AddNewUser()
        {
            _UserID = clsUserData.AddNewUser(PersonID, UserName, clsPasswordEncryption.EncryptPassword(Password), IsActive, Permissions);
         
            return (_UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this._UserID, this.UserName, clsPasswordEncryption.EncryptPassword(this.Password), this.IsActive, this.Permissions);
        }

        public static bool UpdateUserPassword(int UserID, string Password)
        {
            return clsUserData.UpdateUserPassword(UserID, clsPasswordEncryption.EncryptPassword(Password));
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.eAddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.eUpdate:
                    if (_UpdateUser())
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
    }
}
