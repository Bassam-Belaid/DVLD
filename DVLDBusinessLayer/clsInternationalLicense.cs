using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        private static string _DefaultApplicationType = "New International License";
        public static decimal ApplicationFees = clsApplicationType.GetApplicationTypeFeesByApplicationTypeTitle(_DefaultApplicationType);

        private int _InternationalLicenseID;
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public int GetInternationalLicenseID()
        {
            return _InternationalLicenseID;
        }
        public int GetApplicationID()
        {
            return this.ApplicationID;
        }

        public clsInternationalLicense()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.MinValue;
            this.ApplicationTypeID = clsApplicationType.GetApplicationTypeIDByApplicationTypeTitle(_DefaultApplicationType);
            this.ApplicationStatus = 1;
            this.LastStatusDate = DateTime.MinValue;
            this.PaidFees = ApplicationFees;
            this.CreatedByUserID = clsGlobal.CurrentUser.GetUserID();
            this._InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.IsActive = false;
        }

        public clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
                  DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this._InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _AddNewInternationalDrivingApplication()
        {
            if(clsInternationalLicenseData.AddNewInternationalLicense(ref this.ApplicationID, ref this._InternationalLicenseID, this.ApplicantPersonID, this.ApplicationTypeID, this.PaidFees, this.CreatedByUserID, this.DriverID, this.IssuedUsingLocalLicenseID))
            {
                return (this.ApplicationID != -1 && this._InternationalLicenseID != -1);
            }

            return false;
        }

        public bool Save()
        {
            return _AddNewInternationalDrivingApplication();
        }

        public static int IsApplicantHasAnActiveInternationalLicense(int IssuedUsingLocalLicenseID) 
        {
            return clsInternationalLicenseData.IsApplicantHasAnActiveInternationalLicense(IssuedUsingLocalLicenseID);
        }

        public static clsInternationalLicense GetInternationalLicenseByInternationalLicenseID(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            byte IssueReason = 0;
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue;
            bool IsActive = false;

            if (clsInternationalLicenseData.GetInternationalLicenseByInternationalLicenseID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            else
                return null;
        }

        public static DataTable GetAllInternationalLicensesForApplicant(int ApplicantPersonID) 
        {
            return clsInternationalLicenseData.GetAllInternationalLicensesForApplicant(ApplicantPersonID);
        }

        public static DataTable GetAllInternationalLicenseApllications() 
        {
            return clsInternationalLicenseData.GetAllInternationalLicenseApllications();
        }

        public static DataTable GetInternationalLicenseApplicationsFilteredByIntLicenseID(DataTable InternationalLicenseApplications, int eIntLicenseID)
        {
            string FilterExpression = $"[Int.License ID] = {eIntLicenseID}";

            DataRow[] SelectedRows = InternationalLicenseApplications.Select(FilterExpression);

            DataTable FilteredTable = InternationalLicenseApplications.Clone(); // Clone the structure

            foreach (DataRow row in SelectedRows)
            {
                FilteredTable.ImportRow(row);
            }

            return FilteredTable;
        }

        public static DataTable GetInternationalLicenseApplicationsFilteredByDriverID(DataTable InternationalLicenseApplications, int DriverID)
        {
            string FilterExpression = $"[Driver ID] = {DriverID}";

            DataRow[] SelectedRows = InternationalLicenseApplications.Select(FilterExpression);

            DataTable FilteredTable = InternationalLicenseApplications.Clone();

            foreach (DataRow row in SelectedRows)
            {
                FilteredTable.ImportRow(row);
            }

            return FilteredTable;
        }

        public static DataTable GetInternationalLicenseApplicationsFilteredByLLicenseID(DataTable InternationalLicenseApplications, int LLicenseID)
        {
            string FilterExpression = $"[L.License ID] = {LLicenseID}";

            DataRow[] SelectedRows = InternationalLicenseApplications.Select(FilterExpression);

            DataTable FilteredTable = InternationalLicenseApplications.Clone();

            foreach (DataRow row in SelectedRows)
            {
                FilteredTable.ImportRow(row);
            }

            return FilteredTable;
        }

    }
}
