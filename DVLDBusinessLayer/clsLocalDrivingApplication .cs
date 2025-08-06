using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingApplication : clsApplication
    {
        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private static string _DefaultApplicationType = "New Local Driving License Service";
        public static decimal ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle(_DefaultApplicationType);

        private int _LocalDrivingApplicationID;
        
        public int LicenseClassID { get; set; }

        public int GetLocalDrivingApplicationID()
        {
            return _LocalDrivingApplicationID;
        }
        public int GetApplicationID()
        {
            return this.ApplicationID;
        }

        public clsLocalDrivingApplication()
        {
            this._Mode = enMode.eAddNew;
            this.ApplicationID = -1;
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

        public clsLocalDrivingApplication(int LDLAppID, int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate,
            byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID, int LicenseClassID)
        {
            this._Mode = enMode.eUpdate;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDByApplicationTypeTitle(_DefaultApplicationType);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this._LocalDrivingApplicationID = LDLAppID;
            this.LicenseClassID = LicenseClassID;
        }

        private bool _AddNewLocalDrivingApplication() 
        {
            this._LocalDrivingApplicationID = clsLocalDrivingApplicationData.AddNewLocalDrivingApplication(this.ApplicantPersonID, this.ApplicationTypeID, this.PaidFees, this.LicenseClassID, this.CreatedByUserID);

            return (this._LocalDrivingApplicationID != -1);
        }

        private bool _UpdateLocalDrivingApplication()
        {
            return clsLocalDrivingApplicationData.UpdateLocalDrivingApplication(this.GetLocalDrivingApplicationID(), this.LicenseClassID, this.CreatedByUserID);
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

        public static byte NumberOfTestsThatTakenByApplicantForLocalDrivingLicenseApplication(int LDLAppID) 
        {
            return clsLocalDrivingApplicationData.NumberOfTestsThatTakenByApplicantForLocalDrivingLicenseApplication(LDLAppID);
        }

        public bool Save() 
        {
            switch(this._Mode) 
            {
                case enMode.eAddNew:
                    if (_AddNewLocalDrivingApplication())
                    {
                        this._Mode = enMode.eUpdate;
                        return true;
                    }
                    else
                        return false;

                case enMode.eUpdate:
                    return _UpdateLocalDrivingApplication();

            }

            return false;
        }

        public static clsLocalDrivingApplication GetLocalDrivingLicenseApplicationInfoByLDLAppID(int LDLAppID)
        {
            int ApplicationID = -1, ApplicantPersonID = -1, CreatedByUserID = -1, LicenseClassID = -1;
            byte ApplicationStatus = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            decimal PaidFees = 0;

            if (clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByLDLAppID(LDLAppID, ref ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID, ref LicenseClassID))
                return new clsLocalDrivingApplication(LDLAppID, ApplicationID, ApplicantPersonID, ApplicationDate,
                    ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID, LicenseClassID);

            else
                return null;
        }

        public string GetApplicationStatus()
        {
            switch ((enApplicationStatus)this.ApplicationStatus)
            {
                case enApplicationStatus.eNew:
                    return "New";

                case enApplicationStatus.eCanceled:
                    return "Canceled";

                default:
                    return "Completed";
            }
        }

        public static bool IsLocalDrivingLicenseApplicationCanceled(int LDLAppID) 
        {
            return clsLocalDrivingApplicationData.IsLocalDrivingLicenseApplicationCanceled(LDLAppID);
        }
    }
}
