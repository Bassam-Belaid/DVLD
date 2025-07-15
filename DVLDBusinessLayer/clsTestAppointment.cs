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
        public static DataTable GetAllTestAppointmentsForLocalDrivingLicenseApplication(int LDLAppID, string TestTypeTitle) 
        {
            return clsTestAppointmentData.GetAllTestAppointmentsForLocalDrivingLicenseApplication(LDLAppID, TestTypeTitle);
        }

        public static bool IsHasATestAppointmentForLocalDrivingLicenseApplication(int LDLAppID, string TestTypeTitle) 
        {
            return clsTestAppointmentData.IsHasATestAppointmentForLocalDrivingLicenseApplication(LDLAppID, TestTypeTitle);
        }
    }
}
