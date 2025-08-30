
using DVLDDataAccessLayer;
using System;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        private int _DriverID;
        private int _PersonID;
        private int _CreatedByUserID;
        private DateTime _CreatedDate;

        public int GetDriverID() { return _DriverID; }
        public int GetPersonID() { return _PersonID; }
        public int GetCreatedByUserIDID() { return _CreatedByUserID; }
        public DateTime GetCreatedDate() { return _CreatedDate; }


        public clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate) 
        {
            this._DriverID = DriverID;
            this._PersonID = PersonID;
            this._CreatedByUserID = CreatedByUserID;
            this._CreatedDate = CreatedDate;
        }

        public static DataTable GetAllDrivers() 
        {
            return clsDriverData.GetAllDrivers();
        }

        public static DataTable GetDriversFilteredByDriverID(int DriverID) 
        {
            return clsDriverData.GetDriversFilteredByDriverID(DriverID);
        }

        public static DataTable GetDriversFilteredByPersonID(int PersonID) 
        {
            return clsDriverData.GetDriversFilteredByPersonID((PersonID));
        }

        public static DataTable GetPeopleFilteredByNationalNo(string NationalNo) 
        {
            NationalNo += "%";

            return clsDriverData.GetPeopleFilteredByNationalNo((NationalNo));
        }

        public static DataTable GetPeopleFilteredByFullname(string FullName)
        {
            FullName += "%";

            return clsDriverData.GetPeopleFilteredByFullName((FullName));
        }

        public static clsDriver GetDriverInfoByDriverID(int DriverID) 
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MinValue;

            if(clsDriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);

            else
                return null;
        }
    }
}
