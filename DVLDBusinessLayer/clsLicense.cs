using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLDBusinessLayer
{
    public class clsLicense
    {
        private enum enIssueReasons { eNew = 1, eLostReplacement = 2, eDamagedReplacement = 3, eRenewal = 4 }

        private enum enMode { eAddNew = 0, eUpdate = 1 }

        private enMode _Mode;

        private int _LicenseID;

        public int GetLicenseID() { return _LicenseID; }

        public int ApplicationID { set; get; }

        public int DriverID { set; get; }

        public int LicenseClassID { set; get; }

        public DateTime IssueDate { set; get; }

        public DateTime ExpirationDate { set; get; }

        public string Notes { set; get; }

        public decimal PaidFees { set; get; }

        public bool IsActive { set; get; }

        private enIssueReasons IssueReason { set; get; }

        public int CreatedByUserID { set; get; }

        public clsLicense() 
        {
            this._Mode = enMode.eAddNew;
            this._LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClassID = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReasons.eNew;
            this.CreatedByUserID = -1;
        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
                  DateTime IssueDate, DateTime ExpirationDate, string Notes,
                  decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this._Mode = enMode.eUpdate;
            this._LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = (enIssueReasons)IssueReason;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _IssueNewsLicense() 
        {
            this._LicenseID = clsLicenseData.IssueNewsLicense(this.ApplicationID, this.LicenseClassID, this.Notes, this.CreatedByUserID);

            return (this._LicenseID != -1);
        }

        public bool Save() 
        {
            switch (this._Mode) 
            {
                case enMode.eAddNew:
                    if(_IssueNewsLicense()) 
                    {
                        this._Mode = enMode.eUpdate;
                        return true;
                    }

                    else
                        return false;
            }

            return false;
        }

        public static clsLicense GetLicenseByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseID = -1, ApplicationID = -1, DriverID = -1, LicenseClassID = -1, CreatedByUserID = -1;
            byte IssueReason = 0;
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;

            if (clsLicenseData.GetLicenseByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID, ref LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            else 
                return null;
        }

        public string GetIssueReason() 
        {
            switch (this.IssueReason) 
            {
                case enIssueReasons.eNew:
                    return "First Time";

                case enIssueReasons.eLostReplacement:
                    return "Lost Replacement";

                case enIssueReasons.eDamagedReplacement:
                    return "Damaged Replacement";

                case enIssueReasons.eRenewal:
                    return "Renewal";

                default:
                    return "";
            }
        }

        public static DataTable GetAllLocalLicensesForApplicant(int ApplicantPersonID) 
        {
            return clsLicenseData.GetAllLocalLicensesForApplicant(ApplicantPersonID);
        }

        public static clsLicense GetLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClassID = -1, CreatedByUserID = -1;
            byte IssueReason = 0;
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;

            if (clsLicenseData.GetLicenseByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            else
                return null;
        }

        public bool IsExpired() 
        {
            return (DateTime.Now > this.ExpirationDate);
        }

        public bool IsLicenseAnOrdinaryDrivingLicense()
        {
            return (clsLicenseClass.GetLicenseClassNameByLicenseClassID(this.LicenseClassID)) == ("Class 3 - Ordinary driving license");
        }

    }
}
