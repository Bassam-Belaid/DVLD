using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
       
        public static DataTable GetAllTestAppointmentsForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID) 
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForLocalDrivingLicenseApplication((int)TestType, LDLAppID);
        }

        public static bool IsHasATestAppointmentForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID) 
        {
            return clsTestAppointmentData.IsHasATestAppointmentForLocalDrivingLicenseApplication((int)TestType, LDLAppID);
        }
       
        public static bool IsHasPassedTestForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID) 
        {
            return clsTestAppointmentData.IsHasPassedTestForLocalDrivingLicenseApplication((int)TestType, LDLAppID);
        }

        public static bool IsTestAppointmentForLocalDrivingLicenseApplicationIsActive(int TestAppointmentID)
        {
            return clsTestAppointmentData.IsTestAppointmentForLocalDrivingLicenseApplicationIsActive((int)TestAppointmentID);
        }

        public static bool AddNewTestAppointmentForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID) 
        {
            return clsTestAppointmentData.AddNewTestAppointmentForLocalDrivingLicenseApplication((int)TestType, LDLAppID, AppointmentDate, PaidFees, CreatedByUserID);
        }

        public static DateTime GetTestAppointmentDateForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID) 
        {
            return clsTestAppointmentData.GetTestAppointmentDateForLocalDrivingLicenseApplication((int)TestType, LDLAppID);
        }

        public static bool UpdateTestAppointmentForLocalDrivingLicenseApplication(clsTestType.enTestTypes TestType, int LDLAppID, DateTime NewAppointmentDate) 
        {
            return clsTestAppointmentData.UpdateTestAppointmentForLocalDrivingLicenseApplication((int)TestType, LDLAppID, NewAppointmentDate);
        }
    }
}
