using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingApplication : clsApplication
    {
        private static string _DefaultApplicationType = "New Local Driving License Service";
        public static decimal ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle(_DefaultApplicationType);

        private int _LocalDrivingApplicationID;
        
        public int LicenseClassID { get; set; }

        public int GetLocalDrivingApplicationID()
        {
            return _LocalDrivingApplicationID;
        }

        public clsLocalDrivingApplication()
        {
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDByApplicationTypeTitle(_DefaultApplicationType);
            this.ApplicationStatus = 1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = ApplicationFees;
            this.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();
            this._LocalDrivingApplicationID = -1;
            this.LicenseClassID = -1;
        }

        private bool _AddNewLocalDrivingApplication() 
        {
            this._LocalDrivingApplicationID = clsLocalDrivingApplicationData.AddNewLocalDrivingApplication(this.ApplicantPersonID, this.ApplicationTypeID, this.PaidFees, this.LicenseClassID, this.CreatedByUserID);

            return (this._LocalDrivingApplicationID != -1);
        }

        public static DataTable GetAllLocalDrivingLicenseApplications() 
        {
            return clsLocalDrivingApplicationData.GetAllLocalDrivingLicenseApplications();
        }

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByLDLAppID(int LDLAppID)
        {
            return clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationsFilteredByLDLAppID(LDLAppID);
        }

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByNationalNo(string NationalNo) 
        {
            NationalNo += "%";
            return clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationsFilteredByNationalNo(NationalNo);
        }

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByFullName(string FullName) 
        {
            FullName += "%";
            return clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationsFilteredByFullName(FullName);
        }

        public static DataTable GetLocalDrivingLicenseApplicationsFilteredByStatus(string Status) 
        {
            Status += "%";
            return clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationsFilteredByStatus(Status);
        }

        public static int IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(int ApplicantPersonID, string ClassName) 
        {
            return clsLocalDrivingApplicationData.IsApplicantHasAnActiveLocalDrivingLicenseApplicationWithSameLicenseClass(ApplicantPersonID, ClassName);
        }

        public static bool CancelLocalDrivingLicenseApplication(int LDLAppID) 
        {
            return clsLocalDrivingApplicationData.CancelLocalDrivingLicenseApplication(LDLAppID);
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LDLAppID)
        {
            return clsLocalDrivingApplicationData.DeleteLocalDrivingLicenseApplication(LDLAppID);
        }

        public static int NumberOfTestsThatTakenByLocalDrivingLicenseApplication(int LDLAppID) 
        {
            return clsLocalDrivingApplicationData.NumberOfTestsThatTakenByLocalDrivingLicenseApplication(LDLAppID);
        }

        public bool Save() 
        {
            return _AddNewLocalDrivingApplication();
        }

    }
}
