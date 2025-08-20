
using DVLDDataAccessLayer;
using System.Data;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
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
    }
}
