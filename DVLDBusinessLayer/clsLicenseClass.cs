using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public static string _DefaultSelectedClass = "Class 3 - Ordinary driving license";

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }

        public static int GetLicenseClassIDByLicenseClassName(string LicenseClassName) 
        {
            return clsLicenseClassData.GetLicenseClassIDByLicenseClassName(LicenseClassName);
        }

        public static string GetLicenseClassNameByLicenseClassID(int LicenseClassID) 
        {
            return clsLicenseClassData.GetLicenseClassNameByLicenseClassID(LicenseClassID);
        }
    }
}
