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
        public static DataTable GetAllTestAppointmentsForLocalDrivingLicenseApplication(int LDLAppID, clsTestType.enTestTypes TestType) 
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForLocalDrivingLicenseApplication(LDLAppID, (int)TestType);
        }

        public static bool IsHasATestAppointmentForLocalDrivingLicenseApplication(int LDLAppID, clsTestType.enTestTypes TestType) 
        {
            return clsTestAppointmentData.IsHasATestAppointmentForLocalDrivingLicenseApplication(LDLAppID, (int)TestType);
        }
    }
}
